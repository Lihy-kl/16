using Lib_File;
using SmartDyeing.FADM_Form;
using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SmartDyeing.FADM_Auto.Dye;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace SmartDyeing.FADM_Control
{
    public partial class Formula : UserControl
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        //声明主窗体
        SmartDyeing.FADM_Form.Main _formMain = null;

        bool _b_frist = false;

        bool _b_newAdd = false;

        bool _b_isFlagGroup = true;
        bool _b_isFlagGroup2 = false;

        //处理工艺字典
        Dictionary<string, int> _dic_dyeCode = new Dictionary<string, int>();
        string _s_stage = "滴液";

        List<string> _lis_dyeingCode = new List<string>();
        List<string> _lis_fg = new List<string>();

        //构造函数
        public Formula(SmartDyeing.FADM_Form.Main _FormMain)
        {
            InitializeComponent();

            this._b_frist = true;
            this.tmr.Enabled = true;

            //更新当面界面编号
            //Class_Module.MyModule.Module_ConNum = 4;

            _formMain = _FormMain;
            cup_sort();
            if (Lib_Card.Configure.Parameter.Other_Language == 1)
            {
                if (FADM_Object.Communal._s_operator == "管理用户")
                {
                    txt_Operator.Text = "Administrator";
                }
                else if (FADM_Object.Communal._s_operator == "工程师")
                {
                    txt_Operator.Text = "Engineer";
                }
                else
                {
                    txt_Operator.Text = FADM_Object.Communal._s_operator;
                }
            }
            else
            {
                txt_Operator.Text = FADM_Object.Communal._s_operator;
            }

            string s_sql = "SELECT * FROM operator_table ;";
            DataTable dt_operator_table = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            foreach (DataRow dr in dt_operator_table.Rows)
            {
                txt_Browse_Operator.Items.Add(Convert.ToString(dr[0]));
            }
            //加个空字符串代表滴液
            txt_DyeingCode.Items.Add("");
            string s_sql1 = "SELECT DyeingCode FROM dyeing_code group by DyeingCode;";
            DataTable dt_dyeing_code = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);

            foreach (DataRow dr in dt_dyeing_code.Rows)
            {
                txt_DyeingCode.Items.Add(Convert.ToString(dr[0]));
                _lis_dyeingCode.Add(Convert.ToString(dr[0]));
                _dic_dyeCode.Add(Convert.ToString(dr[0]), 3);
            }

            string s_sql2 = "SELECT PretreatmentCode FROM pretreatment_code ;";
            DataTable dt_pretreatment_code = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);

            foreach (DataRow dr in dt_pretreatment_code.Rows)
            {
                txt_DyeingCode.Items.Add(Convert.ToString(dr[0]));
                _lis_dyeingCode.Add(Convert.ToString(dr[0]));
                _dic_dyeCode.Add(Convert.ToString(dr[0]), 1);
            }

            //if (_formMain.BtnUserSwitching.Text == "管理用户")
            //{
            //    btn_operator.Enabled = true;
            //}
            //else
            //{
            //    btn_operator.Enabled = false;

            //}

            s_sql = "SELECT group_Name FROM formula_group where node = 0 ;";
            txt_FormulaGroup.Items.Add("");
            dt_dyeing_code = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_dyeing_code.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_dyeing_code.Rows)
                {
                    txt_FormulaGroup.Items.Add(Convert.ToString(dr[0]));
                    _lis_fg.Add(Convert.ToString(dr[0]));
                }
            }
            else
            {
                _b_isFlagGroup = false;
            }

            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                //设置标题字体
                dgv_FormulaData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12F);

                //设置内容字体
                dgv_FormulaData.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
            }
            else
            {

                //设置标题字体
                dgv_FormulaData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);

                //设置内容字体
                dgv_FormulaData.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
            }


            //关联Label控件的点击事件
            foreach (Control c in this.grp_FormulaData.Controls)
            {
                if (c is Label)
                {
                    c.Click += Label_Click;
                }
                if (c is TextBox || c is CheckBox || c is ComboBox)
                {
                    c.KeyDown += TextBox_KeyDown;
                }
            }



        //获取TextBox控件的矢能
        again:
            s_sql = "SELECT * FROM enabled_set WHERE MyID = 1;";
            DataTable P_dt_enabled = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            if (P_dt_enabled.Rows.Count <= 0)
            {
                s_sql = "INSERT INTO enabled_set (MyID) VALUES(1);";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                goto again;
            }
            foreach (DataColumn mDc in P_dt_enabled.Columns)
            {
                if (mDc.Caption.ToString() != "MyID")
                {
                    string s_name = "lab_" + (mDc.Caption.ToString().Remove(0, 4));
                    foreach (Control c in this.grp_FormulaData.Controls)
                    {
                        if (c is Label && c.Name == s_name)
                        {
                            c.ForeColor = ((P_dt_enabled.Rows[0][mDc].ToString()) == "False" || (P_dt_enabled.Rows[0][mDc].ToString()) == "0" ?
                                          SystemColors.ButtonShadow : SystemColors.ControlText);
                        }
                    }
                }
            }
            _b_updateWait = true;

            btn_FormulaCodeAdd_Click(null, null);

            FormulaBrowseHeadShow("");
        }

        public Formula()
        {
        }

        void TextBox_KeyPress_Number(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberTextbox_KeyPress(e);
        }

        void TextBox_KeyPress_Double(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        //重写ProcessDialogKey函数
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F4:
                    if (txt_DyeingCode.Focused || txt_FormulaGroup.Focused)
                    {
                        btn_BatchAdd.Focus();
                        return false;
                    }
                    btn_BatchAdd_Click(null, null);
                    return false;
                case Keys.F2:
                    if (txt_DyeingCode.Focused || txt_FormulaGroup.Focused)
                    {
                        btn_Save.Focus();
                        return false;
                    }
                    btn_Save_Click(null, null);
                    return false;
                case Keys.F5:
                    btn_FormulaCodeAdd_Click(null, null);
                    return false;
                case Keys.F10:
                    btn_Start_Click(null, null);
                    return false;
                default:
                    return base.ProcessDialogKey(keyData);
            }

        }


        //label控件的点击事件
        void Label_Click(object sender, EventArgs e)
        {
            if (FADM_Object.Communal._s_operator == "管理用户" || FADM_Object.Communal._s_operator == "工程师")
            {
                Label lab = (Label)sender;
                if (lab.Name != "lab_FormulaCode" && lab.Name != "lab_TotalWeight" &&
                    lab.Name != "lab_CreateTime" && lab.Name != "lab_ClothWeight" &&
                    lab.Name != "lab_BathRatio" && lab.Name != "lab_Operator")
                {
                    string s = "txt_" + lab.Name.Remove(0, 4);
                    foreach (Control c in this.grp_FormulaData.Controls)
                    {
                        if ((c is TextBox || c is ComboBox) && c.Name == s)
                        {
                            string s_sql = "SELECT " + c.Name + " FROM enabled_set;";
                            string s_choose = (FADM_Object.Communal._fadmSqlserver.GetData(s_sql)).Rows[0][0].ToString();
                            if (s_choose == "0")
                            {
                                lab.ForeColor = SystemColors.ControlText;
                                s_sql = "UPDATE enabled_set SET" +
                                            " " + c.Name.ToString() + " = 1" +
                                            " WHERE MyID = 1;";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);


                            }
                            else
                            {
                                lab.ForeColor = SystemColors.ButtonShadow;
                                s_sql = "UPDATE enabled_set SET" +
                                           " " + c.Name.ToString() + " = 0" +
                                           " WHERE MyID = 1;";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            }

                        }
                    }

                }
            }
        }

        //查询是否存在的历史配方
        private int Search()
        {
            try
            {


                //设置矢能
                Enabled_set();

                //读取选中行对应的配方资料
                string s_sql = "SELECT Top 1 * FROM formula_head Where" +
                                                               " FormulaCode = '" + txt_FormulaCode.Text + "'" +
                                                               " ORDER BY VersionNum DESC;";

                DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                if (dt_formulahead.Rows.Count < 1)
                {
                    ////清空所有显示数据
                    txt_VersionNum.Text = "";
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        txt_State.Text = "尚未滴液";
                    else
                        txt_State.Text = "Undropped";
                    txt_FormulaGroup.Text = "";
                    //txt_FormulaGroup.Text = "";
                    //txt_FormulaName.Text = "";
                    //txt_ClothType.Text = "";
                    //txt_Customer.Text = "";
                    //txt_ClothWeight.Text = "";
                    //txt_BathRatio.Text = "";
                    //txt_AnhydrationWR.Text = Lib_Card.Configure.Parameter.Other_Default_AnhydrationWR.ToString();
                    //txt_Non_AnhydrationWR.Text = Lib_Card.Configure.Parameter.Other_Default_Non_AnhydrationWR.ToString();
                    //txt_CupNum.Text = "0";
                    //txt_Operator.Text = "";
                    //txt_CreateTime.Text = "";
                    //txt_DyeingCode.Text = "";
                    ////txt_DyeingCode_SelectedIndexChanged(null, null);
                    //txt_FormulaGroup.Enabled = true;
                    //chk_AddWaterChoose.Checked = true;

                    //dgv_FormulaData.Rows.Clear();
                    //dgv_Dye.Rows.Clear();
                    //dgv_Handle1.Rows.Clear();
                    //dgv_Handle2.Rows.Clear();
                    //dgv_Handle3.Rows.Clear();
                    //dgv_Handle4.Rows.Clear();
                    //dgv_Handle5.Rows.Clear();

                    //dgv_Dyeing.ClearSelection();
                    //dgv_Handle1.Visible = false;
                    //dgv_Handle2.Visible = false;
                    //dgv_Handle3.Visible = false;
                    //dgv_Handle4.Visible = false;
                    //dgv_Handle5.Visible = false;

                    //_s_stage = "滴液";
                    //lab_HandleBathRatio.Visible = false;
                    //txt_HandleBathRatio.Visible = false;


                    //dgv_Dyeing.Rows.Clear();
                    //dgv_Details.DataSource = null;

                    //无查询到配方
                    return 0;
                }

                string s_versionNum = dt_formulahead.Rows[0]["VersionNum"].ToString();

                s_sql = "SELECT * FROM formula_details" +
                            " Where FormulaCode = '" + txt_FormulaCode.Text + "'" +
                            " AND VersionNum = '" + s_versionNum + "' ORDER BY IndexNum ;";

                DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                string s_dyeingCode = dt_formulahead.Rows[0]["DyeingCode"] is DBNull ? "" : dt_formulahead.Rows[0]["DyeingCode"].ToString();

                string s_li= dt_formulahead.Rows[0]["HandleBRList"] is DBNull ? "" : dt_formulahead.Rows[0]["HandleBRList"].ToString();
                _lis_hBR.Clear();
                if (s_li != "")
                {
                    string[] sa_hBRList = s_li.Split('|');
                    _lis_hBR = sa_hBRList.ToList();
                }

                //显示表头
                foreach (DataColumn mDc in dt_formulahead.Columns)
                {
                    string s_name = "txt_" + mDc.Caption.ToString();
                    foreach (Control c in this.grp_FormulaData.Controls)
                    {
                        if ((c is TextBox || c is ComboBox) && c.Name == s_name)
                        {
                            c.Text = dt_formulahead.Rows[0][mDc].ToString();
                            break;
                        }
                    }
                    if (s_name == "txt_AddWaterChoose")
                    {
                        chk_AddWaterChoose.Checked = (dt_formulahead.Rows[0][mDc].ToString() == "False" || dt_formulahead.Rows[0][mDc].ToString() == "0" ? false : true);
                    }
                }

                if (Lib_Card.Configure.Parameter.Other_Language != 0)
                {
                    //中文换英文
                    if (txt_State.Text == "尚未滴液")
                    {
                        txt_State.Text = "Undropped";
                    }
                    else if (txt_State.Text == "已滴定配方")
                    {
                        txt_State.Text = "dropped";
                    }
                }

                txt_DyeingCode_SelectedIndexChanged(null, null);

                

                //清理详细资料表
                dgv_FormulaData.Rows.Clear();

                //显示详细信息
                for (int i = 0; i < dt_formuladetail.Rows.Count; i++)
                {
                    dgv_FormulaData.Rows.Add(dt_formuladetail.Rows[i]["IndexNum"].ToString(),
                                             dt_formuladetail.Rows[i]["AssistantCode"].ToString().Trim(),
                                             dt_formuladetail.Rows[i]["AssistantName"].ToString().Trim(),
                                             dt_formuladetail.Rows[i]["FormulaDosage"].ToString(),
                                             dt_formuladetail.Rows[i]["UnitOfAccount"].ToString(),
                                             null,
                                             dt_formuladetail.Rows[i]["SettingConcentration"].ToString(),
                                             dt_formuladetail.Rows[i]["RealConcentration"].ToString(),
                                             dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString(),
                                             dt_formuladetail.Rows[i]["RealDropWeight"].ToString());

                    //显示瓶号
                    s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv_FormulaData[1, i].Value.ToString() + "'" +
                                " AND RealConcentration != 0 ORDER BY BottleNum ;";
                    DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_FormulaData[5, i];
                    List<string> lis_bottleNum = new List<string>();

                    bool b_exist = false;
                    foreach (DataRow mdr in dt_bottlenum.Rows)
                    {
                        string i_num = mdr[0].ToString();

                        lis_bottleNum.Add(i_num);

                        if ((dt_formuladetail.Rows[i]["BottleNum"]).ToString() == i_num)
                        {
                            b_exist = true;
                        }

                    }

                    dd.Value = null;
                    dd.DataSource = lis_bottleNum;

                    if (b_exist)
                    {
                        dd.Value = (dt_formuladetail.Rows[i]["BottleNum"]).ToString();
                    }
                    else
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[i]["BottleNum"]).ToString() +
                                        "号母液瓶不存在", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[i]["BottleNum"]).ToString() +
                                        " mother liquor bottle does not exist", "Tips", MessageBoxButtons.OK, false);
                    }


                    //显示是否手动选瓶
                    DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[10, i];
                    dc.Value = dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "False" || dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "0" ? 0 : 1;
                }
                //dgv_FormulaData.ClearSelection();

                return 1;


            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("查询失败 ", "Search", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Query failed ", "Search", MessageBoxButtons.OK, false);
                return 2;
            }
        }

        //组合配方输入
        private void InsertFormula()
        {
            try
            {
                //查找对应组合配方
                if (txt_FormulaGroup.Text == "")
                {
                    return;
                }

                string s_groupName = txt_FormulaGroup.Text;
                string s_sql = "SELECT * FROM formula_group" +
                                 " WHERE   node = '1' AND group_Name = '" + s_groupName + "';";
                DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                dgv_FormulaData.Rows.Clear();
                for (int i = 0; i < dt_formulahead.Rows.Count; i++)
                {
                    dgv_FormulaData.Rows.Add((i + 1).ToString(),
                                             dt_formulahead.Rows[i]["AssistantCode"].ToString(),
                                             dt_formulahead.Rows[i]["AssistantName"].ToString(), "",
                                             dt_formulahead.Rows[i]["UnitOfAccount"].ToString()
                                             );

                    //bool b_temp = false;
                    //foreach (DataGridViewRow dgvr in dgv_FormulaData.Rows)
                    //{
                    //    if (dgvr.Cells[1].Value != null)
                    //    {
                    //        if (dgvr.Cells[1].Value.ToString() == dt_formulahead.Rows[i]["AssistantCode"].ToString())
                    //        {
                    //            b_temp = true;
                    //            break;
                    //        }
                    //    }
                    //}

                    //if(!b_temp)
                    //{
                    //    dgv_FormulaData.Rows.Add((dgv_FormulaData.Rows.Count + 1).ToString(),
                    //                         dt_formulahead.Rows[i]["AssistantCode"].ToString(),
                    //                         dt_formulahead.Rows[i]["AssistantName"].ToString()
                    //                         );
                    //}
                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("查询失败 ", "InsertFormula", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Query failed ", "InsertFormula", MessageBoxButtons.OK, false);

            }
        }

        void TextBox_HandelBRKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    try
                    {
                        TextBox txt = (TextBox)sender;
                        if(txt.Text !="0" && txt.Text != null && txt.Text != "")
                        {
                            string s_temp = txt.Name.Substring(8);
                            //判断对应是否要输入配方
                            FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)_lis_dg[Convert.ToInt32(s_temp)-1];
                            if (dgv_Dye.Rows.Count!=0)
                            {
                                dgv_Dye.Enabled = true;
                                dgv_Dye.CurrentCell = dgv_Dye[1, 0];
                                dgv_Dye.Focus();
                            }
                            else
                            {
                                if(Convert.ToInt32(s_temp)== _lis_dg.Count)
                                {
                                    btn_Save.Focus();
                                }
                                else
                                {
                                    _lis_handleBathRatio[Convert.ToInt32(s_temp)].Enabled = true;
                                    _lis_handleBathRatio[Convert.ToInt32(s_temp)].Focus();
                                }
                            }
                        }
                    }
                    catch { }
                    break;
                default:
                    break;
            }
        }

        //TextBox文本框按下Enter事件
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Control[] c = { txt_FormulaCode,txt_FormulaGroup, txt_FormulaName, txt_ClothType, txt_Customer,
                             txt_ClothWeight, txt_BathRatio,chk_AddWaterChoose,txt_Non_AnhydrationWR,txt_AnhydrationWR,txt_CupNum,txt_DyeingCode, txt_Operator,
                            dgv_FormulaData,/*dgv_Dye,txt_HandleBathRatio,dgv_Handle1,dgv_Handle2,dgv_Handle3,dgv_Handle4,dgv_Handle5*/};
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    for (int i = 0; i < c.Length; i++)
                    {
                        try
                        {
                            TextBox txt = (TextBox)sender;
                            if (txt.Text != null && txt.Text != "" && (((txt.Name == "txt_ClothWeight" ||
                                txt.Name == "txt_BathRatio") && txt.Text != "0") ||
                                (txt.Name != "txt_ClothWeight" && txt.Name != "txt_BathRatio")))
                            {
                                if (txt.Name == "txt_FormulaCode")
                                {
                                    if (txt.Text == "")
                                    { return; }
                                    dgv_BatchData.ClearSelection();
                                    dgv_BatchData.CurrentCell = null;
                                    dgv_FormulaBrowse.ClearSelection();
                                    dgv_FormulaBrowse.CurrentCell = null;
                                    int r = Search();
                                    //查不到数据，正常跳转 如果没有组合配方 则直接跳转下一步 没有组合isFlagGroup = false;
                                    if (r == 0 && _b_isFlagGroup)
                                    {
                                        txt_FormulaGroup.Enabled = true;
                                        txt_FormulaGroup.Focus();
                                        return;
                                    }
                                    else if (r == 0 && !_b_isFlagGroup)
                                    { //没有历史记录和没有组合配方 跳转
                                        _b_isFlagGroup2 = true;
                                        goto next;

                                    }
                                    else if (r == 1)
                                    {
                                        _b_isFlagGroup2 = true;
                                        goto next;
                                        //txt_FormulaGroup.Enabled = false;
                                        //dgv_FormulaData.Enabled = true;
                                        //dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                        //dgv_FormulaData.Focus();
                                        //return;
                                    }

                                }

                            next:
                                if (txt.Name == c[i].Name)
                                {
                                    for (int j = i; j < c.Length; j++)
                                    {
                                        string s_sql = "SELECT " + (c[j + 1].Name) + " FROM enabled_set;";
                                        string s_choose = (FADM_Object.Communal._fadmSqlserver.GetData(s_sql)).Rows[0][0].ToString();
                                        if (c[j + 1].Name.Equals("txt_FormulaGroup") && _b_isFlagGroup2)
                                        {
                                            continue;
                                        }
                                        else if (c[j + 1].Name.Equals("txt_Non_AnhydrationWR") && this.txt_Non_AnhydrationWR.Text != "" && Lib_Card.Configure.Parameter.Other_Default_N_A_Tpye.ToString().Equals("0"))
                                        {
                                            continue;
                                        }
                                        else if (c[j + 1].Name.Equals("txt_AnhydrationWR") && this.txt_AnhydrationWR.Text != "" && Lib_Card.Configure.Parameter.Other_Default_N_A_Tpye.ToString().Equals("0"))
                                        {
                                            continue;
                                        }
                                        if (s_choose != "0" && c[j + 1].Visible)
                                        {
                                            if (c[j + 1].Name == dgv_FormulaData.Name)
                                            {
                                                dgv_FormulaData.Enabled = true;
                                                dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                                dgv_FormulaData.Focus();
                                                return;
                                            }
                                            //else if (c[j + 1].Name == dgv_Dye.Name)
                                            //{
                                            //    if (dgv_Dye.Rows.Count != 0)
                                            //    {
                                            //        dgv_Dye.Enabled = true;
                                            //        dgv_Dye.CurrentCell = dgv_Dye[1, 0];
                                            //        dgv_Dye.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle1.Name)
                                            //{
                                            //    if (dgv_Handle1.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle1.Enabled = true;
                                            //        dgv_Handle1.CurrentCell = dgv_Handle1[1, 0];
                                            //        dgv_Handle1.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle2.Name)
                                            //{
                                            //    if (dgv_Handle2.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle2.Enabled = true;
                                            //        dgv_Handle2.CurrentCell = dgv_Handle2[1, 0];
                                            //        dgv_Handle2.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle3.Name)
                                            //{
                                            //    if (dgv_Handle3.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle3.Enabled = true;
                                            //        dgv_Handle3.CurrentCell = dgv_Handle3[1, 0];
                                            //        dgv_Handle3.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle4.Name)
                                            //{
                                            //    if (dgv_Handle4.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle4.Enabled = true;
                                            //        dgv_Handle4.CurrentCell = dgv_Handle4[1, 0];
                                            //        dgv_Handle4.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle5.Name)
                                            //{
                                            //    if (dgv_Handle5.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle5.Enabled = true;
                                            //        dgv_Handle5.CurrentCell = dgv_Handle5[1, 0];
                                            //        dgv_Handle5.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            else
                                            {
                                                c[j + 1].Enabled = true;
                                                c[j + 1].Focus();

                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                txt.Text = null;
                            }
                        }
                        catch
                        {
                            try
                            {
                                CheckBox chk = (CheckBox)sender;

                                if (chk.Name == c[i].Name)
                                {
                                    for (int j = i; j < c.Length; j++)
                                    {
                                        string s_sql = "SELECT " + (c[j + 1].Name) + " FROM enabled_set;";
                                        string s_choose = (FADM_Object.Communal._fadmSqlserver.GetData(s_sql)).Rows[0][0].ToString();
                                        if (c[j + 1].Name.Equals("txt_Non_AnhydrationWR") && this.txt_Non_AnhydrationWR.Text != "" && Lib_Card.Configure.Parameter.Other_Default_N_A_Tpye.ToString().Equals("0"))
                                        {
                                            continue;
                                        }
                                        else if (c[j + 1].Name.Equals("txt_AnhydrationWR") && this.txt_AnhydrationWR.Text != "" && Lib_Card.Configure.Parameter.Other_Default_N_A_Tpye.ToString().Equals("0"))
                                        {
                                            continue;
                                        }
                                        if (s_choose != "0" && c[j + 1].Visible)
                                        {
                                            if (c[j + 1].Name == dgv_FormulaData.Name)
                                            {
                                                dgv_FormulaData.Enabled = true;
                                                dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                                dgv_FormulaData.Focus();
                                                return;
                                            }
                                            //else if (c[j + 1].Name == dgv_Dye.Name)
                                            //{
                                            //    if (dgv_Dye.Rows.Count != 0)
                                            //    {
                                            //        dgv_Dye.Enabled = true;
                                            //        dgv_Dye.CurrentCell = dgv_Dye[1, 0];
                                            //        dgv_Dye.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle1.Name)
                                            //{
                                            //    if (dgv_Handle1.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle1.Enabled = true;
                                            //        dgv_Handle1.CurrentCell = dgv_Handle1[1, 0];
                                            //        dgv_Handle1.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle2.Name)
                                            //{
                                            //    if (dgv_Handle2.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle2.Enabled = true;
                                            //        dgv_Handle2.CurrentCell = dgv_Handle2[1, 0];
                                            //        dgv_Handle2.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle3.Name)
                                            //{
                                            //    if (dgv_Handle3.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle3.Enabled = true;
                                            //        dgv_Handle3.CurrentCell = dgv_Handle3[1, 0];
                                            //        dgv_Handle3.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle4.Name)
                                            //{
                                            //    if (dgv_Handle4.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle4.Enabled = true;
                                            //        dgv_Handle4.CurrentCell = dgv_Handle4[1, 0];
                                            //        dgv_Handle4.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            //else if (c[j + 1].Name == dgv_Handle5.Name)
                                            //{
                                            //    if (dgv_Handle5.Rows.Count != 0)
                                            //    {
                                            //        dgv_Handle5.Enabled = true;
                                            //        dgv_Handle5.CurrentCell = dgv_Handle5[1, 0];
                                            //        dgv_Handle5.Focus();
                                            //        return;
                                            //    }
                                            //}
                                            else
                                            {
                                                c[j + 1].Enabled = true;
                                                c[j + 1].Focus();

                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                try
                                {
                                    ComboBox cbo = (ComboBox)sender;
                                    if (cbo.Name == c[i].Name && ((cbo.Text != null && cbo.Text != "") || cbo.Name == "txt_DyeingCode" || cbo.Name == "txt_FormulaGroup"))
                                    {
                                        if (cbo.Name == "txt_DyeingCode")
                                        {
                                            bool b = false;
                                            if (cbo.Text != "")
                                            {
                                                for (int p = 0; p < _lis_dyeingCode.Count; p++)
                                                {
                                                    if (_lis_dyeingCode[p] == cbo.Text)
                                                    {
                                                        b = true; break;
                                                    }
                                                }
                                                if (!b)
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                        else if (cbo.Name == "txt_FormulaGroup")
                                        {
                                            //if (txt.Text == "")
                                            //{ return; }
                                            //InsertFormula();
                                            //goto next;
                                            bool b_temp = false;
                                            if (cbo.Text != "")
                                            {
                                                for (int p = 0; p < _lis_fg.Count; p++)
                                                {
                                                    if (_lis_fg[p] == cbo.Text)
                                                    {
                                                        b_temp = true; break;
                                                    }
                                                }
                                                if (!b_temp)
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                        for (int j = i; j < c.Length; j++)
                                        {
                                            string s_sql = "SELECT " + (c[j + 1].Name) + " FROM enabled_set;";
                                            string s_choose = (FADM_Object.Communal._fadmSqlserver.GetData(s_sql)).Rows[0][0].ToString();

                                            if (s_choose != "0" && c[j + 1].Visible)
                                            {
                                                if (c[j + 1].Name == dgv_FormulaData.Name)
                                                {
                                                    dgv_FormulaData.Enabled = true;
                                                    dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                                    dgv_FormulaData.Focus();
                                                    return;
                                                }
                                                //else if (c[j + 1].Name == dgv_Dye.Name)
                                                //{
                                                //    if (dgv_Dye.Rows.Count != 0)
                                                //    {
                                                //        dgv_Dye.Enabled = true;
                                                //        dgv_Dye.CurrentCell = dgv_Dye[1, 0];
                                                //        dgv_Dye.Focus();
                                                //        return;
                                                //    }
                                                //}
                                                //else if (c[j + 1].Name == dgv_Handle1.Name)
                                                //{
                                                //    if (dgv_Handle1.Rows.Count != 0)
                                                //    {
                                                //        dgv_Handle1.Enabled = true;
                                                //        dgv_Handle1.CurrentCell = dgv_Handle1[1, 0];
                                                //        dgv_Handle1.Focus();
                                                //        return;
                                                //    }
                                                //}
                                                //else if (c[j + 1].Name == dgv_Handle2.Name)
                                                //{
                                                //    if (dgv_Handle2.Rows.Count != 0)
                                                //    {
                                                //        dgv_Handle2.Enabled = true;
                                                //        dgv_Handle2.CurrentCell = dgv_Handle2[1, 0];
                                                //        dgv_Handle2.Focus();
                                                //        return;
                                                //    }
                                                //}
                                                //else if (c[j + 1].Name == dgv_Handle3.Name)
                                                //{
                                                //    if (dgv_Handle3.Rows.Count != 0)
                                                //    {
                                                //        dgv_Handle3.Enabled = true;
                                                //        dgv_Handle3.CurrentCell = dgv_Handle3[1, 0];
                                                //        dgv_Handle3.Focus();
                                                //        return;
                                                //    }
                                                //}
                                                //else if (c[j + 1].Name == dgv_Handle4.Name)
                                                //{
                                                //    if (dgv_Handle4.Rows.Count != 0)
                                                //    {
                                                //        dgv_Handle4.Enabled = true;
                                                //        dgv_Handle4.CurrentCell = dgv_Handle4[1, 0];
                                                //        dgv_Handle4.Focus();
                                                //        return;
                                                //    }
                                                //}
                                                //else if (c[j + 1].Name == dgv_Handle5.Name)
                                                //{
                                                //    if (dgv_Handle5.Rows.Count != 0)
                                                //    {
                                                //        dgv_Handle5.Enabled = true;
                                                //        dgv_Handle5.CurrentCell = dgv_Handle5[1, 0];
                                                //        dgv_Handle5.Focus();
                                                //        return;
                                                //    }
                                                //}
                                                else
                                                {
                                                    c[j + 1].Enabled = true;
                                                    c[j + 1].Focus();

                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    btn_Save.Focus();
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 显示批次资料
        /// </summary>
        private void BatchHeadShow(string _CupNum)
        {
            try
            {
                //获取批次资料表头
                string s_sql = "SELECT CupNum, FormulaCode, VersionNum" +
                                   " FROM drop_head ORDER BY CupNum ;";
                DataTable dt_formula = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //捆绑
                dgv_BatchData.DataSource = new DataView(dt_formula);

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题文字
                    dgv_BatchData.Columns[0].HeaderCell.Value = "杯号";
                    dgv_BatchData.Columns[1].HeaderCell.Value = "配方";
                    dgv_BatchData.Columns[2].HeaderCell.Value = "版本";
                    //设置标题字体
                    dgv_BatchData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                    //设置内容字体
                    dgv_BatchData.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

                }
                else
                {
                    //设置标题文字
                    dgv_BatchData.Columns[0].HeaderCell.Value = "CupNumber";
                    dgv_BatchData.Columns[1].HeaderCell.Value = "RecipeCode";
                    dgv_BatchData.Columns[2].HeaderCell.Value = "Version";
                    //设置标题字体
                    dgv_BatchData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);

                    //设置内容字体
                    dgv_BatchData.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                }

                //设置标题宽度
                dgv_BatchData.RowTemplate.Height = 30;
                dgv_BatchData.Columns[0].Width = 85;
                dgv_BatchData.Columns[1].Width = 140;
                if (dgv_BatchData.Rows.Count > dgv_BatchData.Height / dgv_BatchData.RowTemplate.Height)
                {
                    dgv_BatchData.Columns[2].Width = 55;
                }
                else
                {
                    dgv_BatchData.Columns[2].Width = 75;
                }

                //关闭自动排序功能
                dgv_BatchData.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_BatchData.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dgv_BatchData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                //设置内容居中显示
                dgv_BatchData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



                //设置行高
                dgv_BatchData.RowTemplate.Height = 30;

                //设置当前选中行
                if (_CupNum != "")
                {
                    for (int i = 0; i < dgv_BatchData.Rows.Count; i++)
                    {
                        string s_temp = dgv_BatchData.Rows[i].Cells[0].Value.ToString();
                        if (s_temp == _CupNum)
                        {
                            dgv_BatchData.CurrentCell = dgv_BatchData.Rows[i].Cells[0];
                            break;
                        }
                    }
                }

                dgv_BatchData.CurrentCell = null;


            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "BatchHeadShow", MessageBoxButtons.OK, true);
            }
        }

        //批次表绑定数据事件
        private void dgv_BatchData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_BatchData.ClearSelection();

            //更改颜色
            //string s_sql = "SELECT BatchName FROM enabled_set WHERE MyID = 1;";
            //DataTable dt_operator_table = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            //string s_batchNum = Convert.ToString(dt_operator_table.Rows[0][dt_operator_table.Columns[0]]);


            //s_sql = "SELECT DropAllowError, AddWaterAllowError, AddPowderAllowError FROM" +
            //                          " other_parameters WHERE MyID = 1;";

            //DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            double d_bl_drop_allow_err = Lib_Card.Configure.Parameter.Other_AErr_Drip;
            int i_watrt_allow_err = Convert.ToInt32(Lib_Card.Configure.Parameter.Other_AErr_DripWater);

            string s_sql = "SELECT * FROM drop_head  WHERE BatchName != '0' ORDER BY CupNum;";
            DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_drop_head.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_drop_head.Rows)
                {
                    int i_no_old = Convert.ToInt16(dr["CupNum"].ToString());
                    int i_finish = Convert.ToString(dr["CupFinish"].ToString()) == "False" || Convert.ToString(dr["CupFinish"].ToString()) == "0" ? 0 : 1;

                    double d_bl_objWater = Convert.ToDouble(dr["ObjectAddWaterWeight"]);
                    double d_bl_realWater = Convert.ToDouble(dr["RealAddWaterWeight"]);
                    double d_bl_TestTubeObjectAddWaterWeight = Convert.ToDouble(dr["TestTubeObjectAddWaterWeight"]);
                    double d_testTubeRealAddWaterWeight = Convert.ToDouble(dr["TestTubeRealAddWaterWeight"]);
                    double d_bl_TotalWeight = Convert.ToDouble(dr["TotalWeight"]);
                    string s_describeChar = dr["DescribeChar"] is DBNull ?"": dr["DescribeChar"].ToString();
                    foreach (DataGridViewRow dgvr in this.dgv_BatchData.Rows)
                    {
                        int i_cup = Convert.ToInt16(dgv_BatchData[0, dgvr.Index].Value);

                        if (i_cup == i_no_old)
                        {
                            if (i_finish == 0)
                            {
                                //未完成
                                dgvr.DefaultCellStyle.BackColor = Color.DarkGray;
                            }
                            else
                            {
                                //完成

                                //        //查找所有母液瓶
                                //        s_sql = "SELECT drop_details.CupNum as CupNum, drop_details.BottleNum as BottleNum, drop_details.ObjectDropWeight as ObjectDropWeight, drop_details.RealDropWeight as RealDropWeight,bottle_details.SyringeType as SyringeType  FROM drop_details left join bottle_details on bottle_details.BottleNum = drop_details.BottleNum WHERE" +
                                //"  CupNum = " + i_cup;
                                //        DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                //bool b_err = false;
                                //        //判断是否合格
                                //        foreach (DataRow dr1 in P_dt_data.Rows)
                                //        {
                                //            double d_bl_RealErr = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dr1["ObjectDropWeight"]) - Convert.ToDouble(dr1["RealDropWeight"])): string.Format("{0:F3}", Convert.ToDouble(dr1["ObjectDropWeight"]) - Convert.ToDouble(dr1["RealDropWeight"])));
                                //            d_bl_RealErr = d_bl_RealErr > 0 ? d_bl_RealErr : -d_bl_RealErr;
                                //            if (d_bl_RealErr > d_bl_drop_allow_err)
                                //            {
                                //                b_err = true;
                                //                break;
                                //            }

                                //        }
                                //        //如果母液添加合格，判断加水是否合格
                                //        if (!b_err)
                                //        {
                                //            double d_bl_realDif = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_bl_realWater - d_bl_objWater): string.Format("{0:F3}", d_bl_realWater - d_bl_objWater));
                                //            d_bl_realDif = d_bl_realDif < 0 ? -d_bl_realDif : d_bl_realDif;
                                //            double d_bl_allDif = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_bl_TotalWeight * Convert.ToDouble(i_watrt_allow_err / 100.00)): string.Format("{0:F3}", d_bl_TotalWeight * Convert.ToDouble(i_watrt_allow_err / 100.00)));
                                //            double d_bl_TestTube_err = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_bl_TestTubeObjectAddWaterWeight - d_testTubeRealAddWaterWeight) : string.Format("{0:F3}", d_bl_TestTubeObjectAddWaterWeight - d_testTubeRealAddWaterWeight));

                                //            d_bl_TestTube_err = d_bl_TestTube_err < 0 ? -d_bl_TestTube_err : d_bl_TestTube_err;

                                //            if (d_bl_allDif < d_bl_realDif || d_bl_TestTube_err > d_bl_drop_allow_err || (d_bl_realWater == 0.0 && d_bl_objWater != 0.0))
                                //            {
                                //                b_err = true;
                                //            }
                                //        }
                                if(s_describeChar.Contains("失败"))
                                //if (b_err)
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.Red;
                                }
                                else
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.Lime;

                                    //再次判断后处理是否有不合格项
                                    s_sql = "SELECT * from dye_details where AssistantCode is not null And Finish = 1 And  CupNum = " + i_cup;
                                    DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                    bool b_err = false;
                                    //判断是否合格
                                    foreach (DataRow dr1 in P_dt_data.Rows)
                                    {
                                        double d_bl_RealErr = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dr1["ObjectDropWeight"]) + (dr1["Compensation"] is DBNull ? 0.0 : Convert.ToDouble(dr1["Compensation"])) - Convert.ToDouble(dr1["RealDropWeight"])) : string.Format("{0:F3}", Convert.ToDouble(dr1["ObjectDropWeight"]) + (dr1["Compensation"] is DBNull ? 0.0 : Convert.ToDouble(dr1["Compensation"])) - Convert.ToDouble(dr1["RealDropWeight"]))); d_bl_RealErr = d_bl_RealErr > 0 ? d_bl_RealErr : -d_bl_RealErr;
                                        if (d_bl_RealErr > d_bl_drop_allow_err)
                                        {
                                            b_err = true;
                                            break;
                                        }

                                    }
                                    if (b_err)
                                    {
                                        dgvr.DefaultCellStyle.BackColor = Color.Red;
                                    }

                                }
                            }
                        }

                    }
                }
            }
        }

        //批次表当前行改变事件
        private void dgv_BatchData_CurrentCellChanged(object sender, EventArgs e)
        {
            lock (this)
            {
                try
                {
                    if (dgv_BatchData.CurrentRow == null)
                    {
                        return;
                    }

                    if (dgv_BatchData.SelectedRows.Count > 0)
                    {
                        //设置矢能
                        Enabled_set();



                        //读取选中行对应的配方资料
                        string s_cupNum = dgv_BatchData.CurrentRow.Cells[0].Value.ToString();
                        string s_sql = "SELECT * FROM drop_head Where CupNum = '" + s_cupNum + "';";
                        DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        s_sql = "SELECT * FROM drop_details Where CupNum = '" + s_cupNum + "'" + "  ORDER BY IndexNum;";
                        DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        string s_dyeingCode = dt_formulahead.Rows[0]["DyeingCode"] is DBNull ? "" : dt_formulahead.Rows[0]["DyeingCode"].ToString();

                        string s_li = dt_formulahead.Rows[0]["HandleBRList"] is DBNull ? "" : dt_formulahead.Rows[0]["HandleBRList"].ToString();
                        _lis_hBR.Clear();
                        if (s_li != "")
                        {
                            string[] sa_hBRList = s_li.Split('|');
                            _lis_hBR = sa_hBRList.ToList();
                        }

                        //显示表头
                        foreach (DataColumn mDc in dt_formulahead.Columns)
                        {
                            string s_name = "txt_" + mDc.Caption.ToString();
                            foreach (Control c in this.grp_FormulaData.Controls)
                            {
                                if ((c is TextBox || c is ComboBox) && c.Name == s_name)
                                {
                                    c.Text = dt_formulahead.Rows[0][mDc].ToString();
                                    break;
                                }
                            }
                            if (s_name == "txt_AddWaterChoose")
                            {
                                chk_AddWaterChoose.Checked = (dt_formulahead.Rows[0][mDc].ToString() == "False" || dt_formulahead.Rows[0][mDc].ToString() == "0" ? false : true);
                            }
                        }

                        

                        if (Lib_Card.Configure.Parameter.Other_Language != 0)
                        {
                            //中文换英文
                            if (txt_State.Text == "尚未滴液")
                            {
                                txt_State.Text = "Undropped";
                            }
                            else if (txt_State.Text == "已滴定配方")
                            {
                                txt_State.Text = "dropped";
                            }
                        }

                        txt_DyeingCode_SelectedIndexChanged(null, null);

                        //清理详细资料表
                        dgv_FormulaData.Rows.Clear();

                        //显示详细信息
                        for (int i = 0; i < dt_formuladetail.Rows.Count; i++)
                        {
                            dgv_FormulaData.Rows.Add(dt_formuladetail.Rows[i]["IndexNum"].ToString(),
                                                     dt_formuladetail.Rows[i]["AssistantCode"].ToString().Trim(),
                                                     dt_formuladetail.Rows[i]["AssistantName"].ToString().Trim(),
                                                     dt_formuladetail.Rows[i]["FormulaDosage"].ToString(),
                                                     dt_formuladetail.Rows[i]["UnitOfAccount"].ToString(),
                                                     null,
                                                     dt_formuladetail.Rows[i]["SettingConcentration"].ToString(),
                                                     dt_formuladetail.Rows[i]["RealConcentration"].ToString(),
                                                     dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString(),
                                                     dt_formuladetail.Rows[i]["RealDropWeight"].ToString());

                            //显示瓶号
                            s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                        " FROM bottle_details WHERE" +
                                        " AssistantCode = '" + dgv_FormulaData[1, i].Value.ToString() + "'" +
                                        " AND RealConcentration != 0 ORDER BY BottleNum ;";
                            DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


                            DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_FormulaData[5, i];
                            List<string> lis_bottleNum = new List<string>();
                            bool b_exist = false;
                            foreach (DataRow mdr in dt_bottlenum.Rows)
                            {
                                string s_num = mdr[0].ToString();

                                lis_bottleNum.Add(s_num);

                                if ((dt_formuladetail.Rows[i]["BottleNum"]).ToString() == s_num)
                                {
                                    b_exist = true;
                                }

                            }


                            dd.Value = null;
                            dd.DataSource = lis_bottleNum;
                            if (b_exist)
                            {
                                dd.Value = (dt_formuladetail.Rows[i]["BottleNum"]).ToString();
                            }
                            else
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[i]["BottleNum"]).ToString() +
                                               "号母液瓶不存在", "温馨提示", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[i]["BottleNum"]).ToString() +
                                               " The mother liquor bottle does not exist", "Tips", MessageBoxButtons.OK, false);
                            }


                            //显示是否手动选瓶
                            DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[10, i];
                            dc.Value = dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "False" || dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "0" ? 0 : 1;
                        }


                    }


                }
                catch
                {
                    //new FullAutomaticDripMachine.FADM_Object.MyAlarm(ex.Message, "批次表当前行改变事件", false);
                }
            }
        }

        int P_int_delect_index = -1;
        //批次表按下删除键事件
        private void dgv_BatchData_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    try
                    {
                        if (FADM_Object.Communal._b_isDripping)
                        {
                            return;
                        }
                        FADM_Object.Communal._b_isDripping = true;

                        P_int_delect_index = Convert.ToInt16(dgv_BatchData.CurrentRow.Index);
                        string s_sql = null;
                        foreach (DataGridViewRow dr in dgv_BatchData.SelectedRows)
                        {
                            if (dr.DefaultCellStyle.BackColor == Color.DarkGray ||
                                dr.DefaultCellStyle.BackColor == Color.Red ||
                                dr.DefaultCellStyle.BackColor == Color.Lime)
                            {
                                continue;
                            }

                            string s_temp = dr.Cells[0].Value.ToString();

                            //删除批次浏览表头资料
                            s_sql = "DELETE FROM drop_head WHERE CupNum = '" + s_temp + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            //删除批次浏览表详细资料
                            s_sql = "DELETE FROM drop_details WHERE CupNum = '" + s_temp + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            //删除批次浏览表详细资料
                            s_sql = "DELETE FROM dye_details WHERE CupNum = '" + s_temp + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            ////删除批次浏览表详细资料
                            //s_sql = "DELETE FROM dye_details WHERE CupNum = '" + s_temp + "';";
                            //FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            //更新杯号使用情况
                            s_sql = "Update cup_details set IsUsing = 0 where CupNum = '" + s_temp + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                        _b_updateWait = true;

                        P_bl_update = true;

                        if (SmartDyeing.FADM_Object.Communal._lis_dripCupNum.Count > 0)
                        {

                            //查找滴液配方
                            s_sql = "SELECT * FROM drop_head  where BatchName = '0' and (Stage = '滴液' or Stage is null)  order by CupNum  ;";
                            DataTable dt_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                            s_sql = "SELECT * FROM drop_head  where BatchName != '0' and Stage = '滴液'   order by CupNum  ;";
                            DataTable dt_head_Drip = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                            if (dt_head_Drip.Rows.Count != 0)
                            {
                                FADM_Object.Communal._b_isDripping = false;
                                return;
                            }

                            if (dt_head.Rows.Count > 0)
                            {
                                //先把所有杯状态置为没使用
                                FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 0 where Type = 2");
                            }

                            int i_n = 0;
                            foreach (DataRow dr1 in dt_head.Rows)
                            {
                                int i_cup = Convert.ToInt16(dr1["CupNum"].ToString());

                                string s_sql1 = "UPDATE drop_head SET CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n] + " WHERE CupNum = " + i_cup + ";";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql1);

                                s_sql1 = "UPDATE drop_details SET CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n] + " WHERE CupNum = " + i_cup + ";";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql1);
                                i_n++;
                            }

                            //使用等待列表的后补
                            int i_ndif = FADM_Object.Communal._lis_dripCupNum.Count - i_n;

                            s_sql = "SELECT FormulaCode,VersionNum,CupNum,IndexNum  FROM wait_list  where Type = 2 order by IndexNum;";
                            DataTable dt_waitList = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


                            foreach (DataRow Row in dt_waitList.Rows)
                            {

                                //加入批次
                                AddDropList a = new AddDropList(Row["FormulaCode"].ToString(), Row["VersionNum"].ToString(), FADM_Object.Communal._lis_dripCupNum[i_n].ToString(), 2);
                                //删除等待列表记录
                                FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 2 and IndexNum = " + Row["IndexNum"].ToString());

                                i_n++;
                                if (i_n == FADM_Object.Communal._lis_dripCupNum.Count)
                                {
                                    break;
                                }
                            }

                            if (i_n > 0)
                            {
                                //把对应杯位置为使用
                                FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 1 where Type = 2 and CupNum <=" + FADM_Object.Communal._lis_dripCupNum[i_n - 1]);
                            }
                            //
                        }
                        FADM_Object.Communal._b_isDripping = false;


                        _b_updateWait = true;

                        P_bl_update = true;
                    }
                    catch
                    {
                        FADM_Object.Communal._b_isDripping = false;
                    }

                    break;
                case Keys.Down:
                    try
                    {
                        if (dgv_BatchData.CurrentRow.Index == dgv_BatchData.Rows.Count - 1)
                        {
                            dgv_BatchData.ClearSelection();
                            btn_Start.Focus();
                        }
                    }
                    catch
                    {

                    }
                    break;
                case Keys.Insert:

                    dgv_BatchData_RowsAdded();
                    break;
                default:

                    break;
            }


        }

        //配液杯排序
        private void cup_sort()
        {
            try
            {
                //string s_sql = null;

                ////获取批次的批次表头
                //s_sql = "SELECT CupNum, s_formulaCode, s_versionNum, BatchName" +
                //            " FROM drop_head ORDER BY CupNum ;";

                //DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //if (dt_formulahead.Rows.Count > 0)
                //{

                //    if (dt_formulahead.Rows[0][dt_formulahead.Columns["BatchName"]].ToString() == "0")
                //    {
                //        //重新排杯号
                //        for (int i = 0; i < dt_formulahead.Rows.Count; i++)
                //        {
                //            //得到以前的杯号
                //            string oldCupNum = dt_formulahead.Rows[i][0].ToString();

                //            string code = dt_formulahead.Rows[i][1].ToString();

                //            string ver = dt_formulahead.Rows[i][2].ToString();

                //            //修改批次表头杯号
                //            s_sql = "UPDATE drop_head SET" +
                //                        " CupNum = '" + (i + 1) + "' where CupNum = '" + oldCupNum + "';";
                //            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                //            //修改批次详细表杯号
                //            s_sql = "UPDATE drop_details SET" +
                //                        " CupNum = '" + (i + 1) + "' where CupNum = '" + oldCupNum + "';";
                //            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                //        }
                //    }
                //}


                BatchHeadShow("");

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "cup_sort", MessageBoxButtons.OK, true);
            }

        }

        //批次表成为活动控件事件
        private void dgv_BatchData_Enter(object sender, EventArgs e)
        {
            try
            {
                //if (this._b_frist)
                //{
                //    dgv_BatchData.ClearSelection();
                //    this._b_frist = false;
                //    //dgv_FormulaBrowse.Focus();
                //}
                //else
                {
                    dgv_FormulaBrowse.ClearSelection();
                    dgv_FormulaBrowse.CurrentCell = null;

                }


            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_BatchData_Enter", MessageBoxButtons.OK, true);
            }
        }

        //配方浏览表成为活动控件事件
        private void dgv_FormulaBrowse_Enter(object sender, EventArgs e)
        {
            dgv_BatchData.ClearSelection();
            dgv_BatchData.CurrentCell = null;
            this._b_newAdd = false;
        }

        //是否加水成为活动控件
        private void chk_AddWaterChoose_Enter(object sender, EventArgs e)
        {
            chk_AddWaterChoose.ForeColor = SystemColors.Highlight;
        }

        //是否加水不是活动控件
        private void chk_AddWaterChoose_Leave(object sender, EventArgs e)
        {
            chk_AddWaterChoose.ForeColor = SystemColors.ControlText;
        }

        //布重输入检查
        private void txt_ClothWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        //布重文本框离开事件
        private void txt_ClothWeight_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_ClothWeight.Text != null && txt_ClothWeight.Text != "")
                {
                    if (Convert.ToDouble(txt_ClothWeight.Text) > Lib_Card.Configure.Parameter.Other_ClothAlarmWeight)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("布重超出预警值，请检查", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Cloth weight exceeds the warning value, please check", "Tips", MessageBoxButtons.OK, false);
                    }
                }

                if (txt_BathRatio.Text != null && txt_BathRatio.Text != "")
                {
                    //计算总浴量
                    txt_TotalWeight.Text = (Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_BathRatio.Text)).ToString();


                again:
                    foreach (DataGridViewRow dgvr in dgv_FormulaData.Rows)
                    {
                        if (dgvr.Index < dgv_FormulaData.Rows.Count - 1)
                        {
                            for (int i = 0; i < dgv_FormulaData.Columns.Count - 1; i++)
                            {

                                if (dgvr.Cells[i].Value == null || Convert.ToString(dgvr.Cells[i].Value) == "")
                                {
                                    try
                                    {
                                        //FADM_Form.CustomMessageBox.Show("滴液配方信息缺失，请核对配方！", "温馨提示", MessageBoxButtons.OK, false);
                                        return;
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                }

                            }
                        }
                    }

                    //重新计算滴液重
                    foreach (DataGridViewRow dr in dgv_FormulaData.Rows)
                    {
                        UpdataFormulaData(dr.Index);
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "txt_ClothWeight_Leave", MessageBoxButtons.OK, true);
            }
        }

        //浴比输入检查
        private void txt_BathRatio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        //浴比文本框离开事件
        private void txt_BathRatio_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_ClothWeight.Text != null && txt_ClothWeight.Text != "")
                {
                    //计算总浴量
                    txt_TotalWeight.Text = (Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_BathRatio.Text)).ToString();

                again:
                    foreach (DataGridViewRow dgvr in dgv_FormulaData.Rows)
                    {
                        if (dgvr.Index < dgv_FormulaData.Rows.Count - 1)
                        {
                            for (int i = 0; i < dgv_FormulaData.Columns.Count - 1; i++)
                            {

                                if (dgvr.Cells[i].Value == null || Convert.ToString(dgvr.Cells[i].Value) == "")
                                {
                                    try
                                    {
                                        //FADM_Form.CustomMessageBox.Show("滴液配方信息缺失，请核对配方！", "温馨提示", MessageBoxButtons.OK, false);
                                        return;
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //重新计算滴液重
                    foreach (DataGridViewRow dr in dgv_FormulaData.Rows)
                    {
                        UpdataFormulaData(dr.Index);
                    }

                    //判断后处理浴比是否为空,如果为空，就填入浴比值
                    //if (txt_HandleBathRatio.Text == "" || txt_HandleBathRatio.Text == "0.00" || txt_HandleBathRatio.Text == "0")
                    //{
                    //    txt_HandleBathRatio.Text = txt_BathRatio.Text;
                    //}

                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "txt_BathRatio_Leave", MessageBoxButtons.OK, true);
            }
        }

        private void txt_HandelBathRatio_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_ClothWeight.Text != null && txt_ClothWeight.Text != "")
                {
                    TextBox txt = (TextBox)sender;
                    if (txt.Text != "0" && txt.Text != null && txt.Text != "")
                    {
                        string sTemp = txt.Name.Substring(8);
                        FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)_lis_dg[Convert.ToInt32(sTemp)-1];
                        //重新计算滴液重
                        foreach (DataGridViewRow dr in dgv_Dye.Rows)
                        {
                            UpdataDyeAndHandle(dgv_Dye,dr.Index, Convert.ToDouble(_lis_handleBathRatio[Convert.ToInt32(dgv_Dye.Name) - 1].Text));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "txt_BathRatio_Leave", MessageBoxButtons.OK, true);
            }
        }


        //编辑单元格事件
        private void dgv_FormulaData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgv_FormulaData.CurrentCell.ColumnIndex == 5)
                {
                    ((DataGridViewComboBoxEditingControl)e.Control).SelectedValueChanged -= Page_Formula_SelectedValueChanged;
                    ((DataGridViewComboBoxEditingControl)e.Control).SelectedValueChanged += Page_Formula_SelectedValueChanged;
                    ((DataGridViewComboBoxEditingControl)e.Control).Enter -= new EventHandler(Page_Formula_Enter);
                    ((DataGridViewComboBoxEditingControl)e.Control).Enter += new EventHandler(Page_Formula_Enter);
                    ((DataGridViewComboBoxEditingControl)e.Control).DropDown -= Page_Formula_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).DropDown += Page_Formula_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).GotFocus -= Page_Formula_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).GotFocus += Page_Formula_DropDown;
                }
                if (dgv_FormulaData.CurrentCell.ColumnIndex == 3)
                {
                    ((DataGridViewTextBoxEditingControl)e.Control).KeyPress -= Page_Formula_KeyPress;
                    ((DataGridViewTextBoxEditingControl)e.Control).KeyPress += Page_Formula_KeyPress;
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_FormulaData_EditingControlShowing", MessageBoxButtons.OK, true);
            }
        }

        //声明瓶号列
        List<string> _lis_bottleNum = new List<string>();

        //瓶号成为当前单元格事件
        void Page_Formula_Enter(object sender, EventArgs e)
        {
            if (dgv_FormulaData.CurrentCell.ColumnIndex == 5)
            {
                try
                {

                    _lis_bottleNum.Clear();
                    _lis_bottleNum.Add(dgv_FormulaData.CurrentRow.Index.ToString());
                    if (dgv_FormulaData.CurrentRow.Cells[5].Value != null)
                    {
                        _lis_bottleNum.Add(dgv_FormulaData.CurrentRow.Cells[5].Value.ToString());
                    }
                }
                catch (Exception ex)
                {
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Page_Formula_Enter", MessageBoxButtons.OK, true);
                }
            }
        }

        //配方用量输入检查
        void Page_Formula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgv_FormulaData.CurrentCell.ColumnIndex == 3)
            {
                e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
            }
        }


        //配方数据单员格选择改变事件
        private void dgv_FormulaData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_FormulaData.Rows.Count > 1)
            {
                int i_col = dgv_FormulaData.CurrentCell.ColumnIndex;

                int i_row = dgv_FormulaData.CurrentCell.RowIndex;

                if (i_col == 3)
                {

                    i_col = 0;

                    if (i_row == dgv_FormulaData.NewRowIndex)
                    {
                        //判断上一行是否为空行
                        string s_1 = null;
                        string s_2 = null;
                        try
                        {
                            if (dgv_FormulaData.CurrentCell.Value != null)
                                s_1 = dgv_FormulaData.CurrentCell.Value.ToString();
                            if (dgv_FormulaData[1, dgv_FormulaData.CurrentRow.Index].Value != null)
                                s_2 = dgv_FormulaData[1, dgv_FormulaData.CurrentRow.Index].Value.ToString();
                        }
                        catch
                        {

                        }
                        if (s_1 == null || s_2 == null)
                        {
                            if (_lis_dg.Count != 0)
                            {
                                _lis_handleBathRatio[0].Enabled=true;
                                _lis_handleBathRatio[0].Focus();
                            }
                            else
                            {
                                btn_Save.Focus();
                            }
                        }

                    }

                }
            }

        }

        //离开当前行发生事件
        private void dgv_FormulaData_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.dgv_FormulaData.EndEdit();
            if (dgv_FormulaData[1, dgv_FormulaData.CurrentRow.Index].Value == null ||
                dgv_FormulaData[3, dgv_FormulaData.CurrentRow.Index].Value == null)
            {
                return;
            }
            UpdataFormulaData(dgv_FormulaData.CurrentRow.Index);

        }



        //Combobox下拉时事件
        void Page_Formula_DropDown(object sender, EventArgs e)
        {
            if (dgv_FormulaData.CurrentCell.ColumnIndex == 5)
            {
                DataGridViewComboBoxEditingControl dd = (DataGridViewComboBoxEditingControl)sender;
                dd.BackColor = Color.White;

            }
        }

        //瓶号选择修改事件
        void Page_Formula_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_FormulaData.CurrentCell.ColumnIndex == 5)
                {
                    DataGridViewComboBoxEditingControl dd = (DataGridViewComboBoxEditingControl)sender;
                    bool b_temp = false;

                    //获取当前染助剂所有母液瓶资料
                    string s_sql = "SELECT BottleNum, SettingConcentration ,RealConcentration, DropMinWeight" +
                                       " FROM bottle_details WHERE" +
                                       " AssistantCode = '" + dgv_FormulaData.CurrentRow.Cells[1].Value.ToString() + "'" +
                                       " AND RealConcentration != 0 ORDER BY BottleNum ;";
                    DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    if (dt_bottlenum.Rows.Count > 0)
                    {
                        foreach (DataRow mdr in dt_bottlenum.Rows)
                        {
                            if (_lis_bottleNum[1] == mdr[0].ToString())
                            {
                                b_temp = true;
                                break;
                            }
                        }
                        foreach (DataRow mdr in dt_bottlenum.Rows)
                        {
                            if (dd.Text.ToString() == mdr[0].ToString())
                            {
                                dgv_FormulaData.CurrentRow.Cells[5].Value = mdr[0].ToString();
                                dgv_FormulaData.CurrentRow.Cells[6].Value = mdr[1].ToString();
                                dgv_FormulaData.CurrentRow.Cells[7].Value = mdr[2].ToString();
                                break;
                            }
                        }



                        if (_lis_bottleNum[0] == dgv_FormulaData.CurrentRow.Index.ToString() && _lis_bottleNum[1] != dgv_FormulaData.CurrentRow.Cells[5].Value.ToString() && b_temp)
                        {
                            //设置手动选瓶标志位
                            dgv_FormulaData.CurrentRow.Cells[10].Value = 1;
                        }



                        //计算目标滴液量
                        double d_objectDropWeight = 0;
                        if (dgv_FormulaData.CurrentRow.Cells[4].Value.ToString() == "%")
                        {
                            //染料
                            d_objectDropWeight = (Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(dgv_FormulaData.CurrentRow.Cells[3].Value.ToString()) / Convert.ToDouble(dgv_FormulaData.CurrentRow.Cells[7].Value.ToString()));
                        }
                        else
                        {
                            //助剂
                            d_objectDropWeight = (Convert.ToDouble(txt_TotalWeight.Text) * Convert.ToDouble(dgv_FormulaData.CurrentRow.Cells[3].Value.ToString()) / Convert.ToDouble(dgv_FormulaData.CurrentRow.Cells[7].Value.ToString()));

                        }

                        dgv_FormulaData.CurrentRow.Cells[8].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight): String.Format("{0:F3}", d_objectDropWeight);
                    }

                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "Page_Formula_SelectedValueChanged", MessageBoxButtons.OK, true);

            }

        }




        //配方详细添加行事件
        private void dgv_FormulaData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgv_FormulaData.Rows.Count; i++)
                {
                    dgv_FormulaData[0, i].Value = i + 1;
                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_FormulaData_RowsAdded", MessageBoxButtons.OK, true);
            }
        }

        //配方详细表删除行事件
        private void dgv_FormulaData_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {

                for (int i = 0; i < dgv_FormulaData.Rows.Count; i++)
                {
                    dgv_FormulaData[0, i].Value = i + 1;
                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_FormulaData_RowsRemoved", MessageBoxButtons.OK, true);
            }
        }

        //配方详细表离开事件
        private void dgv_FormulaData_Leave(object sender, EventArgs e)
        {
            dgv_FormulaData.ClearSelection();
        }

        //新增按钮点击事件
        private void btn_FormulaCodeAdd_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.grp_FormulaData.Controls)
            {
                if (c is TextBox || c is DataGridView || c is ComboBox)
                {
                    c.Enabled = false;
                }
            }
            foreach (Control c in _lis_dg)
            {
                if (c is TextBox || c is DataGridView || c is ComboBox)
                {
                    c.Enabled = false;
                }
            }

            foreach (Control c in _lis_handleBathRatio)
            {
                if (c is TextBox || c is DataGridView || c is ComboBox)
                {
                    c.Enabled = false;
                }
            }

            txt_FormulaCode.Enabled = true;
            txt_FormulaCode.Focus();
            dgv_BatchData.ClearSelection();

            this._b_newAdd = true;

        }

        //保存按下回车键事件
        private void btn_Save_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    btn_Save_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //保存点击事件
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgv_FormulaData.EndEdit();
                //判断固染色工艺是否为空
                //if (txt_DyeingCode.Text == "" && _s_stage != "滴液")
                //{
                //     FADM_Form.CustomMessageBox.Show("工艺为空，请输入", "配方异常", MessageBoxButtons.OK,false);
                //    txt_DyeingCode.Focus();
                //    return;
                //}
                if (FADM_Object.Communal._s_operator == "123" || FADM_Object.Communal._s_operator == "管理用户" || FADM_Object.Communal._s_operator == "工程师")
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("当前账号不能保存！", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("The current account cannot be saved！", "Tips", MessageBoxButtons.OK, false);
                    return;
                }
                if (txt_Operator.Text != "")
                {
                    if (txt_Operator.Text != FADM_Object.Communal._s_operator)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        {
                            DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("当前操作员与输入操作员不一致，是否重置?(重置输入操作员请点是，不重置请点否)", "保存配方", MessageBoxButtons.YesNo, true);


                            if (dialogResult == DialogResult.Yes)
                            {
                                txt_Operator.Text = FADM_Object.Communal._s_operator;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("The current operator is inconsistent with the input operator. Do you want to reset it? (To reset the input operator, please click Yes. If not reset, please click No.)", "Save Recipe", MessageBoxButtons.YesNo, true);


                            if (dialogResult == DialogResult.Yes)
                            {
                                txt_Operator.Text = FADM_Object.Communal._s_operator;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                if (txt_TotalWeight.Text != "")
                {
                    if (_s_stage == "后处理")
                    {
                        if (Convert.ToDouble(txt_TotalWeight.Text) > Lib_Card.Configure.Parameter.Other_HandleMaxWeight)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("总浴量大于滴液杯容量，不能保存！", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("The total bath volume is greater than the capacity of the drip cup and cannot be stored！", "Tips", MessageBoxButtons.OK, false);

                            return;
                        }


                    }
                    //如果滴液
                    else
                    {
                        if (Convert.ToDouble(txt_TotalWeight.Text) > Lib_Card.Configure.Parameter.Other_DripMaxWeight)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("总浴量大于滴液杯容量，不能保存！", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("The total bath volume is greater than the capacity of the drip cup and cannot be stored！", "Tips", MessageBoxButtons.OK, false);

                            return;
                        }
                    }
                }
            again:
                foreach (DataGridViewRow dgvr in dgv_FormulaData.Rows)
                {
                    if (dgvr.Index < dgv_FormulaData.Rows.Count - 1)
                    {
                        if (dgvr.Cells[1].Value != null)
                        {
                            string s_sql = "SELECT *  FROM assistant_details WHERE" +
                                               " AssistantCode = '" + dgvr.Cells[1].Value.ToString() + "' ; ";

                            DataTable P_dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                            if (P_dt_assistant.Rows.Count <= 0)
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                {
                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                    "染助剂代码不存在,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                    "Dyeing agent code does not exist, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                    {
                                        return;
                                    }
                                }
                            }

                            //判断是否有重复助剂代码
                            for (int i = 0; i < dgvr.Index; i++)
                            {
                                if (dgv_FormulaData.Rows[i].Cells[1].Value != null)
                                {
                                    if (Convert.ToString(dgvr.Cells[1].Value) == Convert.ToString(dgv_FormulaData.Rows[i].Cells[1].Value))
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                    "染助剂代码重复，请检查！", "输入异常", MessageBoxButtons.OK, false))
                                            {
                                                return;
                                            }
                                            else
                                            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                    "Duplicate code for dyeing auxiliaries, please check！", "Input exception", MessageBoxButtons.OK, false))
                                            {
                                                return;
                                            }

                                    }
                                }
                            }
                        }
                    }
                    if (dgvr.Index < dgv_FormulaData.Rows.Count - 1)
                    {
                        for (int i = 0; i < dgv_FormulaData.Columns.Count - 1; i++)
                        {

                            if (dgvr.Cells[i].Value == null || Convert.ToString(dgvr.Cells[i].Value) == "")
                            {
                                try
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Form.CustomMessageBox.Show("滴液配方信息缺失，请核对配方！", "温馨提示", MessageBoxButtons.OK, false);
                                    else
                                        FADM_Form.CustomMessageBox.Show("Droplet formula information is missing, please verify the formula！", "Tips", MessageBoxButtons.OK, false);
                                    return;
                                }
                                catch
                                {
                                    break;
                                }
                            }
                            if (i == 8)
                            {
                                if (Convert.ToDouble(dgvr.Cells[8].Value) < 0.1)
                                {
                                    //dgv_FormulaBrowse.ClearSelection();
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Form.CustomMessageBox.Show("少于最低滴液量0.1，请核对配方！", "温馨提示", MessageBoxButtons.OK, false);
                                    else
                                        FADM_Form.CustomMessageBox.Show("Less than the minimum droplet volume of 0.1, please verify the formula！", "Tips", MessageBoxButtons.OK, false);
                                    return;
                                }
                            }
                        }
                    }
                }

                txt_ClothWeight_Leave(null, null);

                if (dgv_FormulaData.Rows.Count == 1)
                {
                    //dgv_FormulaBrowse.ClearSelection();
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("当前为空配方,禁止保存!", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("The current formula is empty, saving is prohibited!", "Tips", MessageBoxButtons.OK, false);

                    //dgv_FormulaBrowse_CurrentCellChanged(null, null);

                    return;
                }

                foreach (DataGridViewRow dgvr in dgv_FormulaData.Rows)
                {
                    UpdataFormulaData(dgvr.Index);
                }
                if (_s_stage == "后处理")
                {
                    //判断
                    for(int p = 0;p< _lis_dg.Count;p++)
                    {
                        FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)_lis_dg[p];
                        if (dgv_Dye.Rows.Count >= 1)
                        {
                            foreach (DataGridViewRow dgvr in dgv_Dye.Rows)
                            {
                                if (dgvr.Index < dgv_Dye.Rows.Count)
                                {
                                    if (dgvr.Cells[1].Value != null)
                                    {
                                        string s_sql = "SELECT *  FROM assistant_details WHERE" +
                                                           " AssistantCode = '" + dgvr.Cells[1].Value.ToString() + "' ; ";

                                        DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                        if (dt_assistant.Rows.Count <= 0)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            {
                                                if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                                "染助剂代码不存在,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                                {
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                                "Dyeing agent code does not exist, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                                {
                                                    return;
                                                }
                                            }
                                        }

                                        //判断是否有重复助剂代码
                                        for (int i = 0; i < dgvr.Index; i++)
                                        {
                                            if (dgv_Dye.Rows[i].Cells[1].Value != null)
                                            {
                                                if (Convert.ToString(dgvr.Cells[1].Value) == Convert.ToString(dgv_Dye.Rows[i].Cells[1].Value))
                                                {
                                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                                "染助剂代码重复，请检查！", "输入异常", MessageBoxButtons.OK, false))
                                                    {
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                for (int i = 0; i < dgv_Dye.Columns.Count - 1; i++)
                                {

                                    if (dgvr.Cells[i].Value == null || Convert.ToString(dgvr.Cells[i].Value) == "")
                                    {
                                        //try
                                        //{
                                        //    dgv_Dye.Rows.Remove(dgvr);
                                        //    goto again;
                                        //}
                                        //catch
                                        //{
                                        //    break;
                                        //}
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            FADM_Form.CustomMessageBox.Show("染色工艺配方信息缺失，请核对配方！", "温馨提示", MessageBoxButtons.OK, false);
                                        else
                                            FADM_Form.CustomMessageBox.Show("The dyeing process formula information is missing, please verify the formula！", "Tips", MessageBoxButtons.OK, false);
                                        return;
                                    }
                                    if (i == 8)
                                    {
                                        if (Convert.ToDouble(dgvr.Cells[8].Value) < 0.1)
                                        {
                                            //dgv_FormulaBrowse.ClearSelection();
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("少于最低滴液量0.1，请核对配方！", "温馨提示", MessageBoxButtons.OK, false);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Less than the minimum droplet volume of 0.1, please verify the formula！", "Tips", MessageBoxButtons.OK, false);
                                            return;
                                        }
                                    }
                                }
                                if (_lis_handleBathRatio[Convert.ToInt32(dgv_Dye.Name) - 1].Text != "")
                                    UpdataDyeAndHandle(dgv_Dye, dgvr.Index, Convert.ToDouble(_lis_handleBathRatio[Convert.ToInt32(dgv_Dye.Name) - 1].Text));
                            }

                        }
                    }
                }

                double d_allDropWeight = 0;

                string s_addWaterWeight = "0.00";
                string s_testTubeObjectAddWaterWeight = "0.00";
                if (_s_stage == "后处理")
                {
                    string s_sql = "select * from dyeing_code where DyeingCode ='" + txt_DyeingCode.Text + "' order by IndexNum;";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    if (dt_data.Rows.Count > 0)
                    {
                        if (dt_data.Rows[0][1].ToString() == "1")
                        {
                            FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)_lis_dg[0];
                            //计算染色滴液量,如果第一个是染色工艺，就把滴液量算出来用来扣减水
                            foreach (DataGridViewRow dr in dgv_Dye.Rows)
                            {

                                d_allDropWeight += Convert.ToDouble(dr.Cells[8].Value);

                            }
                        }
                    }
                }

                //遍历所有的目标滴液量
                foreach (DataGridViewRow dr in dgv_FormulaData.Rows)
                {
                    //不计算粉重
                    if (Convert.ToInt16(dr.Cells[5].Value) != 200 && Convert.ToInt16(dr.Cells[5].Value) != 201)
                    {
                        d_allDropWeight += Convert.ToDouble(dr.Cells[8].Value);
                    }
                }
                if (d_allDropWeight > Convert.ToDouble(txt_TotalWeight.Text))
                {
                    //dgv_FormulaBrowse.ClearSelection();
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("总目标滴液量大于总浴量,请检查配方", "配方异常", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("The total target droplet volume is greater than the total bath volume, please check the formula", "Formula abnormality", MessageBoxButtons.OK, false);
                    return;
                }

                //设置创建时间
                txt_CreateTime.Text = System.DateTime.Now.ToString();

                if (_s_stage == "后处理")
                {

                    for (int p = 0; p < _lis_dg.Count; p++)
                    {
                        if (_lis_handleBathRatio[p].Text == "" || Convert.ToDouble( _lis_handleBathRatio[p].Text)<=0)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("输入后处理浴比为空或0，请重新输入！", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Input post-processing bath ratio is empty or zero, please re-enter！", "Tips", MessageBoxButtons.OK, false);
                            _lis_handleBathRatio[p].Enabled = true;
                            _lis_handleBathRatio[p].Focus();
                            return;
                        }
                        else
                        {
                            if(Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(_lis_handleBathRatio[p].Text) > Lib_Card.Configure.Parameter.Other_HandleMaxWeight)
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("输入后处理浴比异常，请重新输入！", "温馨提示", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("Input post-processing bath ratio is abnormality, please re-enter！", "Tips", MessageBoxButtons.OK, false);
                                _lis_handleBathRatio[p].Enabled = true;
                                _lis_handleBathRatio[p].Focus();
                                return;
                            }

                        }
                    }
                }

                //计算加水重量
                if (chk_AddWaterChoose.Checked)
                {
                    s_addWaterWeight = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", Convert.ToDouble(txt_TotalWeight.Text) - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_AnhydrationWR.Text) - d_allDropWeight) : String.Format("{0:F3}", Convert.ToDouble(txt_TotalWeight.Text) - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_AnhydrationWR.Text) - d_allDropWeight);
                    if (Convert.ToDouble(s_addWaterWeight) < 0.0)
                    {
                        //dgv_FormulaBrowse.ClearSelection();
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("总目标滴液量大于总浴量,请检查配方", "配方异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("The total target droplet volume is greater than the total bath volume, please check the formula", "Formula abnormality", MessageBoxButtons.OK, false);
                        return;
                    }
                }
                //当前焦点在配方浏览表
                if (dgv_BatchData.SelectedRows != null)
                {
                    if (txt_State.Text == "尚未滴液"|| txt_State.Text == "Undropped")
                    {
                        //修改配方(先删除后添加)
                        string s_sql = "DELETE FROM formula_head WHERE" +
                                           " FormulaCode = '" + txt_FormulaCode.Text + "'" +
                                           " AND VersionNum = '" + txt_VersionNum.Text + "' ;";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        s_sql = "DELETE FROM formula_details WHERE" +
                                    " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                    " VersionNum = '" + txt_VersionNum.Text + "' ;";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        if (_s_stage == "后处理")
                        {
                            s_sql = "DELETE FROM formula_handle_details WHERE" +
                                    " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                    " VersionNum = '" + txt_VersionNum.Text + "' ;";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                    }
                    else
                    {
                        // 添加配方
                        if ((txt_State.Text == "已滴定配方"|| txt_State.Text == "dropped") && txt_VersionNum.Text != "")
                        {
                            //搜索当前配方最大版本号
                            string s_sql = "SELECT Top 1 VersionNum, State FROM formula_head WHERE" +
                                               " FormulaCode = '" + txt_FormulaCode.Text + "'" +
                                               " ORDER BY VersionNum DESC;";
                            DataTable dt_ver = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                            if (dt_ver.Rows.Count == 0)
                            {
                                txt_VersionNum.Text = "0";

                            }
                            else
                            {
                                string s_versionNum = (Convert.ToInt16(dt_ver.Rows[0][dt_ver.Columns[0]])).ToString();
                                string s_state = dt_ver.Rows[0][dt_ver.Columns[1]].ToString();

                                if (txt_VersionNum.Text == s_versionNum && s_state == "已滴定配方")
                                {
                                    txt_VersionNum.Text = (Convert.ToInt16(dt_ver.Rows[0][dt_ver.Columns[0]]) + 1).ToString();

                                }
                                else
                                {
                                    txt_VersionNum.Text = (Convert.ToInt16(dt_ver.Rows[0][dt_ver.Columns[0]])).ToString();
                                    s_sql = "DELETE FROM formula_details WHERE" +
                                               " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                               " VersionNum = '" + txt_VersionNum.Text + "' ;";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                    txt_VersionNum.Text = (Convert.ToInt16(dt_ver.Rows[0][dt_ver.Columns[0]])).ToString();
                                    if (_s_stage == "后处理")
                                    {
                                        s_sql = "DELETE FROM formula_handle_details WHERE" +
                                               " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                               " VersionNum = '" + txt_VersionNum.Text + "' ;";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                            }
                        }
                        else
                        {
                            txt_VersionNum.Text = "0";

                        }
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            txt_State.Text = "尚未滴液";
                        else
                            txt_State.Text = "Undropped";
                    }



                    double d_bl_bottleAlarmWeight = Lib_Card.Configure.Parameter.Other_Bottle_AlarmWeight;

                    int i_machineType = Lib_Card.Configure.Parameter.Machine_Bottle_Total;

                    string s_bottleLower = null;

                    //添加进配方浏览详细表
                    foreach (DataGridViewRow dr in dgv_FormulaData.Rows)
                    {
                        if (dr.Index < dgv_FormulaData.RowCount - 1)
                        {
                            List<string> lis_Detail = new List<string>();
                            lis_Detail.Add(txt_FormulaCode.Text);
                            lis_Detail.Add(txt_VersionNum.Text);
                            foreach (DataGridViewColumn dc in dgv_FormulaData.Columns)
                            {
                                try
                                {


                                    if (dc.Index == 10)
                                    {
                                        if (dgv_FormulaData[dc.Index, dr.Index].Value == null || dgv_FormulaData[dc.Index, dr.Index].Value.ToString() == "")
                                        {
                                            lis_Detail.Add("0");
                                            continue;
                                        }
                                        lis_Detail.Add(dgv_FormulaData[dc.Index, dr.Index].Value.ToString());
                                        continue;
                                    }
                                    else if (dc.Index == 9)
                                    {
                                        lis_Detail.Add("0.00");
                                        continue;
                                    }
                                    lis_Detail.Add(dgv_FormulaData[dc.Index, dr.Index].Value.ToString());
                                }
                                catch
                                {
                                    //存在空白行
                                    goto head;
                                }
                            }


                            string s_sql_0 = "INSERT INTO formula_details (" +
                                                 " FormulaCode, VersionNum, IndexNum, AssistantCode,AssistantName," +
                                                 " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                                 " RealConcentration,  ObjectDropWeight, RealDropWeight," +
                                                 " BottleSelection) VALUES( '" + lis_Detail[0] + "', '" + lis_Detail[1] + "'," +
                                                 " '" + lis_Detail[2] + "', '" + lis_Detail[3] + "', '" + lis_Detail[4] + "', '" + lis_Detail[5] + "'," +
                                                 " '" + lis_Detail[6] + "', '" + lis_Detail[7] + "', '" + lis_Detail[8] + "', '" + lis_Detail[9] + "'," +
                                                 " '" + lis_Detail[10] + "', '" + lis_Detail[11] + "', '" + lis_Detail[12] + "');";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);
                            if (Convert.ToInt16(lis_Detail[7]) <= i_machineType)
                            {

                                s_sql_0 = "SELECT CurrentWeight FROM bottle_details WHERE" +
                                              " BottleNum = '" + lis_Detail[7] + "';";

                                DataTable P_dt_CurrentWeight = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_0);

                                double d_bl_CurrentWeight = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", P_dt_CurrentWeight.Rows[0][0]) : string.Format("{0:F3}", P_dt_CurrentWeight.Rows[0][0]));

                                if (d_bl_CurrentWeight <= d_bl_bottleAlarmWeight)
                                {
                                    s_bottleLower += (lis_Detail[7] + " ");
                                }
                            }
                        }

                    }

                    if (_s_stage == "后处理")
                    {
                        string s_sql = "SELECT * FROM dyeing_code where DyeingCode = '" + txt_DyeingCode.Text + "' order by IndexNum;";
                        DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        for (int p = 0; p < _lis_dg.Count; p++)
                        {
                            FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)_lis_dg[p];
                            //添加进染固色详细信息表
                            foreach (DataGridViewRow dr in dgv_Dye.Rows)
                            {
                                if (dr.Index < dgv_Dye.RowCount)
                                {
                                    List<string> lis_detail = new List<string>();
                                    lis_detail.Add(txt_DyeingCode.Text);
                                    lis_detail.Add(dt_data.Rows[p][3].ToString());
                                    lis_detail.Add(txt_FormulaCode.Text);
                                    lis_detail.Add(txt_VersionNum.Text);
                                    foreach (DataGridViewColumn dc in dgv_Dye.Columns)
                                    {
                                        try
                                        {
                                            if (dc.Index == 10)
                                            {
                                                if (dgv_Dye[dc.Index, dr.Index].Value == null || dgv_Dye[dc.Index, dr.Index].Value.ToString() == "")
                                                {
                                                    lis_detail.Add("0");
                                                    continue;
                                                }
                                                lis_detail.Add(dgv_Dye[dc.Index, dr.Index].Value.ToString());
                                                continue;
                                            }
                                            else if (dc.Index == 9)
                                            {
                                                lis_detail.Add("0.00");
                                                continue;
                                            }
                                            lis_detail.Add(dgv_Dye[dc.Index, dr.Index].Value.ToString());
                                        }
                                        catch
                                        {
                                            //存在空白行

                                        }
                                    }


                                    string s_sql_0 = "INSERT INTO formula_handle_details (" +
                                                         " DyeingCode,Code,FormulaCode, VersionNum, TechnologyName, AssistantCode,AssistantName," +
                                                         " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                                         " RealConcentration,  ObjectDropWeight, RealDropWeight," +
                                                         " BottleSelection) VALUES( '" + lis_detail[0] + "', '" + lis_detail[1] + "'," +
                                                         " '" + lis_detail[2] + "', '" + lis_detail[3] + "', '" + lis_detail[4] + "', '" + lis_detail[5] + "'," +
                                                         " '" + lis_detail[6] + "', '" + lis_detail[7] + "', '" + lis_detail[8] + "', '" + lis_detail[9] + "'," +
                                                         " '" + lis_detail[10] + "', '" + lis_detail[11] + "', '" + lis_detail[12] + "', '" + lis_detail[13] + "', '" + lis_detail[14] + "');";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);
                                }

                            }
                        }
                    }

                head:
                    List<string> lis_head = new List<string>();
                    lis_head.Add(txt_FormulaCode.Text);
                    lis_head.Add(txt_VersionNum.Text);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        lis_head.Add(txt_State.Text);
                    else
                    {
                        if (txt_State.Text == "Undropped")
                        {
                            lis_head.Add("尚未滴液");
                        }
                        else
                        {
                            lis_head.Add("已滴定配方");
                        }
                    }
                    lis_head.Add(txt_FormulaName.Text);
                    lis_head.Add(txt_ClothType.Text);
                    lis_head.Add(txt_Customer.Text);
                    lis_head.Add(chk_AddWaterChoose.Checked == false ? "0" : "1");
                    lis_head.Add("0");
                    lis_head.Add(txt_ClothWeight.Text);
                    lis_head.Add(txt_BathRatio.Text);
                    lis_head.Add(txt_TotalWeight.Text);
                    lis_head.Add(txt_Operator.Text);
                    lis_head.Add("");
                    lis_head.Add(txt_CreateTime.Text);
                    lis_head.Add(s_addWaterWeight);
                    lis_head.Add(s_testTubeObjectAddWaterWeight);
                    lis_head.Add(txt_CupNum.Text);
                    lis_head.Add(txt_DyeingCode.Text);
                    lis_head.Add(txt_Non_AnhydrationWR.Text);
                    lis_head.Add(txt_AnhydrationWR.Text);
                    if (_s_stage == "后处理")
                    {

                        lis_head.Add("0"/*txt_HandleBathRatio.Text*/);
                        lis_head.Add( "0");
                        lis_head.Add( "0");
                        lis_head.Add( "0");
                        lis_head.Add("0");
                        lis_head.Add( "0");

                        lis_head.Add(_s_stage);
                        string s_hBRList = "";
                        for (int p = 0; p < _lis_dg.Count; p++)
                        {
                            s_hBRList += _lis_handleBathRatio[p].Text + "|";
                        }
                        //去掉最后一个分割符
                        s_hBRList = s_hBRList.Substring(0, s_hBRList.Length - 1);
                        lis_head.Add(s_hBRList);
                    }
                    else
                    {
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");

                        lis_head.Add(_s_stage);
                        lis_head.Add("");
                    }

                    // 添加进配方浏览表头
                    string s_sql_1 = "INSERT INTO formula_head (" +
                                         " FormulaCode, VersionNum, State, FormulaName," +
                                         " ClothType,Customer,AddWaterChoose,CompoundBoardChoose,ClothWeight," +
                                         " BathRatio,TotalWeight,Operator,CupCode,CreateTime," +
                                         " ObjectAddWaterWeight,TestTubeObjectAddWaterWeight,CupNum,DyeingCode,Non_AnhydrationWR,AnhydrationWR,HandleBathRatio,Handle_Rev1,Handle_Rev2,Handle_Rev3,Handle_Rev4,Handle_Rev5,Stage,HandleBRList) VALUES('" + lis_head[0] + "'," +
                                         " '" + lis_head[1] + "', '" + lis_head[2] + "', '" + lis_head[3] + "', " +
                                         " '" + lis_head[4] + "', '" + lis_head[5] + "', '" + lis_head[6] + "', " +
                                         " '" + lis_head[7] + "', '" + lis_head[8] + "', '" + lis_head[9] + "', " +
                                         " '" + lis_head[10] + "', '" + lis_head[11] + "', '" + lis_head[12] + "', " +
                                         " '" + lis_head[13] + "', '" + lis_head[14] + "', '" + lis_head[15] + "', '" +
                                         lis_head[16] + "', '" + lis_head[17] + "', '" + lis_head[18] + "', '" + lis_head[19]
                                         + "', '" + lis_head[20] + "', '" + lis_head[21] + "', '" + lis_head[22] + "', '" + lis_head[23] + "', '" + lis_head[24] + "', '" + lis_head[25] + "', '" + lis_head[26] + "', '" + lis_head[27] + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);




                    bool b_temp = false;

                    //遍历滴液表
                    foreach (DataGridViewRow dgvr in dgv_BatchData.Rows)
                    {
                        string s_cup = Convert.ToString(dgvr.Cells[0].Value);
                        string s_code = Convert.ToString(dgvr.Cells[1].Value);
                        string s_ver = Convert.ToString(dgvr.Cells[2].Value);
                        if (s_code == txt_FormulaCode.Text &&
                            dgvr.DefaultCellStyle.BackColor != Color.DarkGray &&
                            dgvr.DefaultCellStyle.BackColor != Color.Red &&
                            dgvr.DefaultCellStyle.BackColor != Color.Lime)
                        {
                            //先把没有滴液记录删除，再重新添加批次

                            //删除批次浏览表头资料
                            s_sql_1 = "DELETE FROM drop_head WHERE CupNum = '" + s_cup + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                            //删除批次浏览表详细资料
                            s_sql_1 = "DELETE FROM drop_details WHERE CupNum = '" + s_cup + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                            //删除批次浏览表详细资料
                            s_sql_1 = "DELETE FROM dye_details WHERE CupNum = '" + s_cup + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);


                            ////更新杯号使用情况
                            //s_sql_1 = "Update cup_details set IsUsing = 0 where CupNum = '" + s_cup + "';";
                            //FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                            if (SmartDyeing.FADM_Object.Communal._lis_dripCupNum.Contains(Convert.ToInt32(s_cup)) && _s_stage != "滴液")
                            {
                                b_temp = true;
                                FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 0 where CupNum = '" + s_cup + "';");
                            }
                            else if (!SmartDyeing.FADM_Object.Communal._lis_dripCupNum.Contains(Convert.ToInt32(s_cup)) && _s_stage == "滴液")
                            {
                                b_temp = true;
                                FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 0 where CupNum = '" + s_cup + "';");
                            }
                            else
                            {
                                ReBatchAdd(s_cup);
                            }
                        }


                    }

                    //删除等待列表对于配方不符合记录
                    if (_s_stage == "滴液")
                    {
                        FADM_Object.Communal._fadmSqlserver.ReviseData("DELETE FROM wait_list WHERE FormulaCode = '" + txt_FormulaCode.Text + "' and Type =3");
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.ReviseData("DELETE FROM wait_list WHERE FormulaCode = '" + txt_FormulaCode.Text + "' and Type =2");
                    }

                    if (s_bottleLower != null)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show(s_bottleLower + "号母液瓶液量不足！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show( "Insufficient liquid volume in the "+s_bottleLower +" mother liquor bottle！", "Tips", MessageBoxButtons.OK, false);
                    }
                    if (b_temp)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已删除滴液区后处理区不匹配批次数据！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Droplet area deleted, post-processing area mismatch batch data！", "Tips", MessageBoxButtons.OK, false);
                    }
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("保存完成", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Save completed", "Tips", MessageBoxButtons.OK, false);
                    FADM_Object.Communal._b_isUpdateNotDripList = true;
                    if (txt_VersionNum.Text == "")
                    {
                        txt_VersionNum.Text = "0";
                    }

                    if (Lib_Card.Configure.Parameter.Other_Language == 1)
                    {
                        if (FADM_Object.Communal._s_operator == "管理用户")
                        {
                            txt_Operator.Text = "Administrator";
                        }
                        else if (FADM_Object.Communal._s_operator == "工程师")
                        {
                            txt_Operator.Text = "Engineer";
                        }
                        else
                        {
                            txt_Operator.Text = FADM_Object.Communal._s_operator;
                        }
                    }
                    else
                    {
                        txt_Operator.Text = FADM_Object.Communal._s_operator;
                    }

                    //把当前配方之前版本移动到历史表
                    if (txt_VersionNum.Text != "0")
                    {
                        string s_sql = "SELECT FormulaCode,VersionNum  FROM formula_head WHERE FormulaCode = '" + txt_FormulaCode.Text + "' and  VersionNum  < " + txt_VersionNum.Text + ";";

                        DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        List<string> lis_ver = new List<string>();

                        //查找滴液列表和等待列表，看看是否存在低版本配方，如果有就先不移除
                        string s_sql_drop = "SELECT VersionNum  FROM drop_head WHERE FormulaCode = '" + txt_FormulaCode.Text + "';";

                        DataTable dt_data_drop = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_drop);
                        foreach (DataRow dr in dt_data_drop.Rows)
                        {
                            lis_ver.Add(dr[0].ToString());
                        }

                        string s_sql_Wait = "SELECT VersionNum  FROM wait_list WHERE FormulaCode = '" + txt_FormulaCode.Text + "';";

                        DataTable dt_data_wait = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_Wait);
                        foreach (DataRow dr in dt_data_wait.Rows)
                        {
                            lis_ver.Add(dr[0].ToString());
                        }


                        foreach (DataRow dr in dt_data.Rows)
                        {
                            if (!lis_ver.Contains(dr["VersionNum"].ToString()))
                            {
                                string s_temp;
                                s_temp = "insert into formula_details_temp select * from formula_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_temp);
                                s_temp = "delete from  formula_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_temp);
                                s_temp = "insert into formula_handle_details_temp select * from formula_handle_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_temp);
                                s_temp = "delete from  formula_handle_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_temp);
                                s_temp = "insert into formula_head_temp select * from formula_head where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_temp);
                                s_temp = "delete from formula_head where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_temp);
                            }
                        }
                    }
                    btn_FormulaCodeAdd_Click(null, null);
                    cup_sort();
                    ReSet_txt_FormulaCode();
                    _b_updateWait = true;
                }
                else
                {
                    //当前焦点在批次表
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "btn_Save_Click", MessageBoxButtons.OK, false);
            }

        }

        public static bool _b_updateWait = false;
        //加入批次按下事件
        private void btn_BatchAdd_Click(object sender, EventArgs e)
        {
            if (!FADM_Object.Communal._b_finshRun)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("正在保存，请稍后再重新点击", "操作异常", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Saving, please click again later", "Abnormal operation", MessageBoxButtons.OK, false);
                return;
            }

            //如果配方列表选中，就把选择配方加入批次
            if (dgv_FormulaBrowse.SelectedRows.Count > 0)
            {
                try
                {
                    for (int i = dgv_FormulaBrowse.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        //查询对应配方资料
                        string s_sql = "SELECT FormulaCode, VersionNum, CreateTime, CupNum,Stage from  formula_head WHERE FormulaCode ='" + dgv_FormulaBrowse.SelectedRows[i].Cells[0].Value.ToString()+
                                "' And  VersionNum = "+ dgv_FormulaBrowse.SelectedRows[i].Cells[1].Value.ToString()+";";
                        DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                        if (dt_data.Rows.Count > 0)
                        {
                            //先判断是否后处理
                            if (dt_data.Rows[0][4].ToString() == "后处理" || dt_data.Rows[0][4].ToString() == "Handle")
                            {
                                //判断是否固定杯位
                                if (dt_data.Rows[0][3].ToString() == "0")
                                {
                                    string s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0 and IsFixed = 0 and enable = 1 and Type = 3 order by CupNum ;";
                                    DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                                    if (dt_cup_details.Rows.Count > 0)
                                    {

                                        AddDropList a = new AddDropList(dt_data.Rows[0][0].ToString(), dt_data.Rows[0][1].ToString(), dt_cup_details.Rows[0][0].ToString(), 3);
                                    }
                                    //加入等待列表
                                    else
                                    {
                                        //加入等待列表
                                        s_sqltemp = "SELECT MAX(IndexNum) as maxnum  FROM wait_list  where Type = 3;";
                                        dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                                        int i_nIndex = 0;
                                        if (dt_cup_details.Rows[0]["maxnum"] is DBNull)
                                        {
                                            i_nIndex = 1;
                                        }
                                        else
                                        {
                                            i_nIndex = Convert.ToInt16(dt_cup_details.Rows[0]["maxnum"]) + 1;
                                        }

                                        s_sqltemp = "Insert into wait_list(FormulaCode,VersionNum,IndexNum,CupNum,Type)values('" + dt_data.Rows[0][0].ToString() + "','" + dt_data.Rows[0][1].ToString() + "'," + i_nIndex.ToString() + "," + dt_data.Rows[0][3].ToString() + ",3);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sqltemp);
                                    }
                                }
                                else
                                {
                                    string s_sqltemp = "SELECT  CupNum FROM cup_details WHERE  CupNum = '" + dt_data.Rows[0][3].ToString() + "' and IsUsing = 1 and enable = 1   and Type = 3;";
                                    DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                                    //没有空闲杯
                                    if (dt_cup_details.Rows.Count > 0)
                                    {
                                        //加入等待列表
                                        s_sqltemp = "SELECT MAX(IndexNum) as maxnum  FROM wait_list  where Type = 3;";
                                        dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                                        int i_nIndex = 0;
                                        if (dt_cup_details.Rows[0]["maxnum"] is DBNull)
                                        {
                                            i_nIndex = 1;
                                        }
                                        else
                                        {
                                            i_nIndex = Convert.ToInt16(dt_cup_details.Rows[0]["maxnum"]) + 1;
                                        }

                                        s_sqltemp = "Insert into wait_list(FormulaCode,VersionNum,IndexNum,CupNum,Type)values('" + dt_data.Rows[0][0].ToString() + "','" + dt_data.Rows[0][1].ToString() + "'," + i_nIndex.ToString() + "," + dt_data.Rows[0][3].ToString() + ",3);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sqltemp);
                                    }
                                    else
                                    {
                                        AddDropList a = new AddDropList(dt_data.Rows[0][0].ToString(), dt_data.Rows[0][1].ToString(), dt_data.Rows[0][3].ToString(), 3);
                                    }
                                }
                            }
                            else
                            {
                                string s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0  and enable = 1 and Type = 2 order by CupNum ;";
                                DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                                if (dt_cup_details.Rows.Count > 0)
                                {
                                    AddDropList a = new AddDropList(dt_data.Rows[0][0].ToString(), dt_data.Rows[0][1].ToString(), dt_cup_details.Rows[0][0].ToString(), 2);
                                }
                                else
                                {
                                    //加入等待列表
                                    s_sqltemp = "SELECT MAX(IndexNum) as maxnum  FROM wait_list  where Type = 2;";
                                    dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                                    int i_nIndex = 0;
                                    if (dt_cup_details.Rows[0]["maxnum"] is DBNull)
                                    {
                                        i_nIndex = 1;
                                    }
                                    else
                                    {
                                        i_nIndex = Convert.ToInt16(dt_cup_details.Rows[0]["maxnum"]) + 1;
                                    }

                                    s_sqltemp = "Insert into wait_list(FormulaCode,VersionNum,IndexNum,CupNum,Type)values('" + dt_data.Rows[0][0].ToString() + "','" + dt_data.Rows[0][1].ToString() + "'," + i_nIndex.ToString() + "," + dt_data.Rows[0][3].ToString() + ",2);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sqltemp);
                                }
                            }
                        }
                    }
                }
                catch { }
                FADM_Control.Formula._b_updateWait = true;
                
            }
            else
            {
                //判断当前输入杯号是否与数据库杯号一致，不一致就不加入
                string s_formulaHead = "SELECT  CupNum,DyeingCode FROM  formula_head where FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "';";
                DataTable dt_formulaHead = FADM_Object.Communal._fadmSqlserver.GetData(s_formulaHead);
                if (dt_formulaHead.Rows.Count == 0)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                    return;
                }
                else
                {
                    if (dt_formulaHead.Rows[0][0].ToString() != txt_CupNum.Text)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("杯号与保存数据库杯号不一致，请先保存再加入批次", "操作异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("The cup number is inconsistent with the saved database cup number. Please save it first before adding it to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                        return;
                    }
                    else
                    {
                        if (dt_formulaHead.Rows[0][1] is DBNull)
                        {
                            if (txt_DyeingCode.Text != "")
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("染固色工艺与保存数据库不一致，请保存后再添加", "操作异常", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("The dyeing and fixation process is inconsistent with the saved database. Please save it before adding it again", "Abnormal operation", MessageBoxButtons.OK, false);
                                return;
                            }
                        }
                        else
                        {
                            if (txt_DyeingCode.Text != dt_formulaHead.Rows[0][1].ToString())
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("染固色工艺与保存数据库不一致，请保存后再添加", "操作异常", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("The dyeing and fixation process is inconsistent with the saved database. Please save it before adding it again", "Abnormal operation", MessageBoxButtons.OK, false);
                                return;
                            }
                        }
                    }
                }


                string s_cup = null;
                bool b_addWaitList = false;
                try
                {

                    string s_stage = null;
                    if (_s_stage == "前处理")
                    {
                        s_stage = "1";
                    }
                    else if (_s_stage == "后处理")
                    {
                        s_stage = "3";
                    }
                    else
                    {
                        s_stage = "2";
                    }
                    //是否加入排队队列

                    string s_sqltemp = "";
                    DataTable dt_temp = new DataTable(); ;
                    string s_maxCupNum = "0";
                    //获取滴液杯信息
                    //判断是否指定杯号
                    //获取滴液杯信息
                    if (s_stage == "3")
                    {
                    //判断是否指定杯号
                    SelectCup:
                        if (txt_CupNum.Text != "" && txt_CupNum.Text != "0")
                        {
                            //判断此杯是否正在使用，如果使用就提示加入失败
                            s_sqltemp = "SELECT  CupNum FROM cup_details WHERE  CupNum = '" + txt_CupNum.Text + "' and (IsUsing = 1   or Statues != '待机') and enable = 1   and Type = " + s_stage + ";";
                            dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);

                            string s_sqltemp1 = "SELECT  * FROM wait_list where CupNum =" + txt_CupNum.Text + "  and Type = " + s_stage + ";";
                            DataTable dt_temp1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp1);

                            if (dt_temp.Rows.Count == 1)
                            {
                                if (s_stage == "3")
                                {
                                    b_addWaitList = true;

                                }
                                else
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Form.CustomMessageBox.Show("已存在杯号在操作,请空闲后再加入", "操作异常", MessageBoxButtons.OK, false);
                                    else
                                        FADM_Form.CustomMessageBox.Show("The cup number already exists for operation. Please join again when available", "Abnormal operation", MessageBoxButtons.OK, false);
                                    return;
                                }
                            }
                            //如果后备等待列表存在该固定色号杯子在排队，就把这个加入后备队列
                            else
                            {
                                if (dt_temp1.Rows.Count > 0)
                                {
                                    if (s_stage == "3")
                                    {
                                        b_addWaitList = true;
                                    }
                                    else
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            FADM_Form.CustomMessageBox.Show("已存在杯号在操作,请空闲后再加入", "操作异常", MessageBoxButtons.OK, false);
                                        else
                                            FADM_Form.CustomMessageBox.Show("The cup number already exists for operation. Please join again when available", "Abnormal operation", MessageBoxButtons.OK, false);
                                        return;
                                    }
                                }
                                else
                                {
                                    //判断当前杯是否可用
                                    string s_sqltemp2 = "SELECT  CupNum FROM cup_details WHERE  CupNum = '" + txt_CupNum.Text + "' and  enable = 1   and Type = " + s_stage + ";";
                                    DataTable dt_temp2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp2);
                                    //enable为0
                                    if (dt_temp2.Rows.Count == 0)
                                    {
                                        if (s_stage == "3")
                                        {
                                            b_addWaitList = true;

                                        }
                                        else
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("已存在杯号在操作,请空闲后再加入", "操作异常", MessageBoxButtons.OK, false);
                                            else
                                                FADM_Form.CustomMessageBox.Show("The cup number already exists for operation. Please join again when available", "Abnormal operation", MessageBoxButtons.OK, false);
                                            return;
                                        }
                                    }
                                }
                            }
                            if (!b_addWaitList)
                            {
                                //查询是否在drop_head里存在该杯
                                string s_head = "SELECT  * FROM drop_head WHERE  CupNum = " + txt_CupNum.Text + " ;";
                                DataTable dt_head1 = FADM_Object.Communal._fadmSqlserver.GetData(s_head);
                                if (dt_head1.Rows.Count == 0)
                                {
                                    s_maxCupNum = txt_CupNum.Text;
                                }
                                else
                                {
                                    //把杯号置为正在使用，重新选择
                                    FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 1 where CupNum = '" + txt_CupNum.Text + "';");
                                    goto SelectCup;
                                }
                            }
                        }
                        else
                        {
                            //进去随机分配杯号
                            s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0 and Statues = '待机' and IsFixed = 0 and enable = 1 and Type = " + s_stage + " order by CupNum ;";
                            dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);



                            if (dt_temp.Rows.Count < 1)
                            {
                                if (s_stage == "3")
                                {
                                    b_addWaitList = true;
                                }
                                else
                                {
                                    //没有空闲杯
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Form.CustomMessageBox.Show("所有杯号在操作,请空闲后再加入", "操作异常", MessageBoxButtons.OK, false);
                                    else
                                        FADM_Form.CustomMessageBox.Show("All cup numbers are in operation. Please add them when you have free time", "Abnormal operation", MessageBoxButtons.OK, false);
                                    return;
                                }
                            }
                            else
                            {
                                string s_sqltemp2 = "SELECT  * FROM wait_list   where Type = " + s_stage + " and CupNum=0;";
                                DataTable dt_temp2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp2);
                                //如果等待列表有数据，先加入等待列表
                                if (dt_temp2.Rows.Count > 0)
                                {
                                    b_addWaitList = true;
                                }
                                else
                                {
                                    //查询是否在drop_head里存在该杯
                                    string s_head = "SELECT  * FROM drop_head WHERE  CupNum = " + dt_temp.Rows[0][0].ToString() + " ;";
                                    DataTable dt_head1 = FADM_Object.Communal._fadmSqlserver.GetData(s_head);

                                    if (dt_head1.Rows.Count == 0)
                                    {
                                        //记录杯号，加入批次
                                        s_maxCupNum = dt_temp.Rows[0][0].ToString();
                                    }
                                    else
                                    {
                                        //把杯号置为正在使用，重新选择
                                        FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 1 where CupNum = '" + dt_temp.Rows[0][0].ToString() + "';");
                                        goto SelectCup;
                                    }
                                }
                            }
                        }
                    }
                    else if (s_stage == "2")
                    {
                    SelectCup2:
                        //进去随机分配杯号
                        s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0  and enable = 1 and Type = " + s_stage + " order by CupNum ;";
                        dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);

                        if (dt_temp.Rows.Count < 1)
                        {
                            b_addWaitList = true;
                        }
                        else
                        {
                            //判断当前区域是否有正在滴液记录，如果有就加入到待检
                            string s_sqltemp2 = "SELECT  * FROM drop_head   where BatchName != '0' and CupNum in (select CupNum from cup_details where Type = " + s_stage + ");";
                            DataTable dt_temp2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp2);

                            string s_sqltemp3 = "SELECT  * FROM wait_list   where Type = " + s_stage + ";";
                            DataTable dt_temp3 = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp3);

                            if (dt_temp2.Rows.Count > 0 /*|| dt_temp3.Rows.Count > 0*/)
                            {
                                b_addWaitList = true;
                            }
                            else
                            {
                                //查询是否在drop_head里存在该杯
                                string s_head = "SELECT  * FROM drop_head WHERE  CupNum = " + dt_temp.Rows[0][0].ToString() + " ;";
                                DataTable dt_head1 = FADM_Object.Communal._fadmSqlserver.GetData(s_head);

                                if (dt_head1.Rows.Count == 0)
                                {
                                    if (dt_temp3.Rows.Count > 0)
                                    {
                                        b_addWaitList = true;
                                    }
                                    else
                                    {
                                        //记录杯号，加入批次
                                        s_maxCupNum = dt_temp.Rows[0][0].ToString();
                                    }
                                }
                                else
                                {
                                    //如果只是占位，就直接使用杯号
                                    if (dt_head1.Rows[0]["FormulaCode"] == System.DBNull.Value)
                                    {
                                        //删除占位记录
                                        s_maxCupNum = dt_temp.Rows[0][0].ToString();
                                        FADM_Object.Communal._fadmSqlserver.ReviseData("Delete FROM drop_head WHERE  CupNum = " + dt_temp.Rows[0][0].ToString() + " ;");
                                    }
                                    else
                                    {
                                        //把杯号置为正在使用，重新选择
                                        FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 1 where CupNum = '" + dt_temp.Rows[0][0].ToString() + "';");
                                        goto SelectCup2;
                                    }
                                }
                            }
                        }
                    }
                    string s_sql = "";
                    DataTable dt_head = new DataTable();
                    DataTable dt_details = new DataTable();
                    if (txt_FormulaCode.Text != null && txt_FormulaCode.Text != "" && txt_VersionNum.Text != null && txt_VersionNum.Text != "")
                    {
                        s_sql = "SELECT * FROM formula_head WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND VersionNum = " + txt_VersionNum.Text + ";";

                        dt_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        if (dt_head.Rows.Count == 0)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                            return;
                        }

                        s_sql = "SELECT * FROM formula_details WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND VersionNum = " + txt_VersionNum.Text + " order by IndexNum;";
                        dt_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                        //不是单独滴液时，有可能是中控下发下来只做后处理的，先不判断
                        if (_s_stage != "后处理")
                        {
                            if (dt_details.Rows.Count == 0)
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                                return;
                            }
                        }

                    }
                    else
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                        return;
                    }

                    if (!b_addWaitList)
                    {
                        s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_maxCupNum + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_maxCupNum + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_maxCupNum + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }


                    List<string> lis_head = new List<string>();
                    lis_head.Add(s_maxCupNum);
                    lis_head.Add(dt_head.Rows[0]["FormulaCode"].ToString());
                    lis_head.Add(dt_head.Rows[0]["VersionNum"].ToString());
                    lis_head.Add(dt_head.Rows[0]["State"].ToString());
                    lis_head.Add(dt_head.Rows[0]["FormulaName"].ToString());
                    lis_head.Add(dt_head.Rows[0]["ClothType"].ToString());
                    lis_head.Add(dt_head.Rows[0]["Customer"].ToString());
                    lis_head.Add(dt_head.Rows[0]["AddWaterChoose"].ToString() == "False" || dt_head.Rows[0]["AddWaterChoose"].ToString() == "0" ? "0" : "1");
                    lis_head.Add(dt_head.Rows[0]["CompoundBoardChoose"].ToString() == "False" || dt_head.Rows[0]["CompoundBoardChoose"].ToString() == "0" ? "0" : "1");
                    lis_head.Add(dt_head.Rows[0]["ClothWeight"].ToString());
                    lis_head.Add(dt_head.Rows[0]["BathRatio"].ToString());
                    lis_head.Add(dt_head.Rows[0]["TotalWeight"].ToString());
                    lis_head.Add(dt_head.Rows[0]["Operator"].ToString());
                    lis_head.Add(dt_head.Rows[0]["CupCode"].ToString());
                    lis_head.Add(dt_head.Rows[0]["CreateTime"].ToString());
                    lis_head.Add(dt_head.Rows[0]["ObjectAddWaterWeight"].ToString());
                    lis_head.Add(dt_head.Rows[0]["TestTubeObjectAddWaterWeight"].ToString());
                    lis_head.Add(dt_head.Rows[0]["DyeingCode"].ToString());

                    lis_head.Add(txt_Non_AnhydrationWR.Text);
                    lis_head.Add(txt_AnhydrationWR.Text);

                    if (_s_stage == "后处理")
                    {

                        lis_head.Add("0"/*txt_HandleBathRatio.Text*/);
                        lis_head.Add( "0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add(_s_stage);
                    }
                    else
                    {
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");
                        lis_head.Add("0");

                        lis_head.Add(_s_stage);
                    }

                    lis_head.Add(dt_head.Rows[0]["HandleBRList"].ToString());


                    s_cup = lis_head[0];

                    if (!b_addWaitList)
                    {

                        // 添加进批次表头
                        s_sql = "INSERT INTO drop_head (" +
                                    " CupNum, FormulaCode, VersionNum, State ,FormulaName, ClothType," +
                                    " Customer, AddWaterChoose, CompoundBoardChoose, ClothWeight, BathRatio, TotalWeight," +
                                    " Operator, CupCode, CreateTime, ObjectAddWaterWeight, TestTubeObjectAddWaterWeight,DyeingCode,Non_AnhydrationWR,AnhydrationWR,HandleBathRatio,Handle_Rev1,Handle_Rev2,Handle_Rev3,Handle_Rev4,Handle_Rev5,Stage,HandleBRList) VALUES(" +
                                    " '" + lis_head[0] + "', '" + lis_head[1] + "', '" + lis_head[2] + "'," +
                                    " '" + lis_head[3] + "', '" + lis_head[4] + "', '" + lis_head[5] + "'," +
                                    " '" + lis_head[6] + "', '" + lis_head[7] + "', '" + lis_head[8] + "'," +
                                    " '" + lis_head[9] + "', '" + lis_head[10] + "', '" + lis_head[11] + "'," +
                                   " '" + lis_head[12] + "', '" + lis_head[13] + "', '" + lis_head[14] + "'," +
                                    " '" + lis_head[15] + "','" + lis_head[16] + "','" + lis_head[17] + "', '" + lis_head[18] + "', '" + lis_head[19]
                                             + "', '" + lis_head[20] + "', '" + lis_head[21] + "', '" + lis_head[22] + "', '" + lis_head[23] + "', '" + lis_head[24] + "', '" + lis_head[25] + "', '" + lis_head[26] + "', '" + lis_head[27] + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }


                    //添加进批次详细表
                    foreach (DataRow dr in dt_details.Rows)
                    {

                        List<string> lis_Detail = new List<string>();
                        lis_Detail.Add(s_maxCupNum);
                        foreach (DataColumn dc in dt_details.Columns)
                        {
                            if (dc.ColumnName == "BottleSelection")
                            {
                                lis_Detail.Add((dr[dc]).ToString() == "False" || (dr[dc]).ToString() == "0" ? "0" : "1");
                                continue;
                            }
                            lis_Detail.Add((dr[dc]).ToString());
                        }
                        if (!b_addWaitList)
                        {
                            //添加进滴液详细表
                            s_sql = "INSERT INTO drop_details (" +
                                    " CupNum, FormulaCode, VersionNum, IndexNum, AssistantCode," +
                                    " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                    " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                    " BottleSelection) VALUES( '" + lis_Detail[0] + "', '" + lis_Detail[1] + "'," +
                                    " '" + lis_Detail[2] + "', '" + lis_Detail[3] + "', '" + lis_Detail[4] + "'," +
                                    " '" + lis_Detail[5] + "', '" + lis_Detail[6] + "', '" + lis_Detail[7] + "'," +
                                    " '" + lis_Detail[8] + "', '" + lis_Detail[9] + "', '" + lis_Detail[10] + "'," +
                                    " '" + lis_Detail[11] + "', '" + string.Format("{0:F}", 0) + "', '" + lis_Detail[13] + "');";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                    }

                    //插入后处理详细步骤表
                    s_sql = "select * from dyeing_code where DyeingCode ='" + txt_DyeingCode.Text + "' order by IndexNum;";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    int i_num = 0;


                    if (_s_stage == "后处理")
                    {
                        int i_nNum = 0;
                        //先把助剂代码写入对应列表
                        foreach (DataRow dr in dt_data.Rows)
                        {
                            //判断第一个工艺是否染色，如果是，就先不补水
                            
                            if (dr[1].ToString() == "1" && i_nNum == 0)
                            {
                                string s_sql1 = "SELECT * FROM dyeing_process where Code = '" + dr[3].ToString() + "' order by StepNum;";
                                DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);

                                foreach (DataRow dr1 in dt_data1.Rows)
                                {
                                    i_num++;
                                    List<string> lis_dye_Detail = new List<string>();
                                    lis_dye_Detail.Add("0");
                                    lis_dye_Detail.Add(s_maxCupNum);
                                    lis_dye_Detail.Add(txt_FormulaCode.Text);//s_formulaCode
                                    lis_dye_Detail.Add(txt_VersionNum.Text);//s_versionNum
                                    lis_dye_Detail.Add(dr[3].ToString());//Code
                                    lis_dye_Detail.Add(i_num.ToString());//StepNum
                                    lis_dye_Detail.Add(dr1[1].ToString());//TechnologyName
                                    lis_dye_Detail.Add("0");//Finish
                                                             //RotorSpeed
                                    if (dr1[7] is DBNull)
                                    {
                                        lis_dye_Detail.Add("0");
                                    }
                                    else
                                    {
                                        lis_dye_Detail.Add(dr1[7].ToString());
                                    }
                                    if (dr1[1].ToString() == "温控" || dr1[1].ToString() == "Temperature control")
                                    {
                                        lis_dye_Detail.Add(dr1[4].ToString());//Temp
                                        lis_dye_Detail.Add(dr1[5].ToString());//TempSpeed
                                        lis_dye_Detail.Add(dr1[2].ToString());//Time
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum,FormulaCode,VersionNum, Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                        " Temp, TempSpeed, Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "'," +
                                        " '" + lis_dye_Detail[8] + "'," +
                                        " '" + lis_dye_Detail[9] + "'," +
                                        " '" + lis_dye_Detail[10] + "'," +
                                        " '" + lis_dye_Detail[11] + "',1);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                    else if (dr1[1].ToString() == "冷行" || dr1[1].ToString() == "洗杯" || dr1[1].ToString() == "排液" || dr1[1].ToString() == "搅拌"
                                        || dr1[1].ToString() == "Cool line" || dr1[1].ToString() == "Wash the cup" || dr1[1].ToString() == "Drainage" || dr1[1].ToString() == "Stir")
                                    {
                                        lis_dye_Detail.Add(dr1[2].ToString());//Time
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                        " Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "', '" + lis_dye_Detail[8] + "'," +
                                        " '" + lis_dye_Detail[9] + "',1);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                    else if ((dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                        || (dr1[1].ToString() == "Add A" || dr1[1].ToString() == "Add B" || dr1[1].ToString() == "Add C" || dr1[1].ToString() == "Add D" || dr1[1].ToString() == "Add E"))
                                    {
                                        string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                        DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                        //lis_dye_Detail.Add(dt_data2.Rows[0]["s_formulaCode"].ToString());
                                        //lis_dye_Detail.Add(dt_data2.Rows[0]["s_versionNum"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantCode"].ToString());
                                        lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["UnitOfAccount"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["BottleNum"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["SettingConcentration"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["RealConcentration"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantName"].ToString());
                                        lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["RealDropWeight"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["BottleSelection"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["MinWeight"].ToString());

                                        lis_dye_Detail.Add("0.0");
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,AssistantCode," +
                                        " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                        " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                        " BottleSelection,MinWeight,ObjectWaterWeight,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "'," +
                                        " '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "', '" + lis_dye_Detail[10] + "'," +
                                        " '" + lis_dye_Detail[11] + "', '" + lis_dye_Detail[12] + "', '" + lis_dye_Detail[13] + "', '" + lis_dye_Detail[14] + "', '"
                                        + lis_dye_Detail[15] + "', '" + lis_dye_Detail[16] + "', '" + lis_dye_Detail[17] + "', '" + lis_dye_Detail[18] + "', '" + lis_dye_Detail[19] + "'," +
                                        " '" + lis_dye_Detail[20] + "',1);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                    else if (dr1[1].ToString() == "加水" || dr1[1].ToString() == "Add Water")
                                    {
                                        double d_dropWater1 = 150 * Convert.ToDouble(dr1[2].ToString()) / 100 - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text);
                                        if (d_dropWater1 < 0.0)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("加水量异常，请检查配方！", "温馨提示", MessageBoxButtons.OK, false);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Abnormal water addition, please check the formula！", "Tips", MessageBoxButtons.OK, false);
                                            if (s_cup != null)
                                            {
                                                s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                BatchHeadShow("");
                                                return;
                                            }
                                        }
                                        lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_dropWater1) : string.Format("{0:F3}", d_dropWater1));
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,ObjectWaterWeight,DyeType" +
                                        " ) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "'," +
                                        " '" + lis_dye_Detail[6] + "'," +
                                        " '" + lis_dye_Detail[7] + "'," +
                                        " '" + lis_dye_Detail[8] + "'," +
                                        " '" + lis_dye_Detail[9] + "',1);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                    else
                                    {
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,DyeType" +
                                        " ) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "'," +
                                        " '" + lis_dye_Detail[6] + "'," +
                                        " '" + lis_dye_Detail[7] + "'," +
                                        " '" + lis_dye_Detail[8] + "',1);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                }
                            }
                            //染色后处理需要补水
                            else
                            {
                                string s_sql1 = "SELECT * FROM dyeing_process where Code = '" + dr[3].ToString() + "' order by StepNum;";
                                DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);
                                //先把加水量计算出来
                                double d_dropWeight = 0.0;
                                double d_dropWater = 0.0;
                                bool b_insert = false;
                                //判断现在第几次排液
                                int i_number = 0;
                                List<double> lis_dropWeight = new List<double>();
                                foreach (DataRow dr1 in dt_data1.Rows)
                                {
                                    if ((dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                        || (dr1[1].ToString() == "Add A" || dr1[1].ToString() == "Add B" || dr1[1].ToString() == "Add C" || dr1[1].ToString() == "Add D" || dr1[1].ToString() == "Add E"))
                                    {
                                        string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                        DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                        d_dropWeight += (Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1["ProportionOrTime"].ToString()) / 100.0);
                                    }
                                    else if (dr1[1].ToString() == "排液" || dr1[1].ToString() == "Drainage")
                                    {
                                        lis_dropWeight.Add(d_dropWeight);

                                        d_dropWater = Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(_lis_handleBathRatio[i_nNum].Text) - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text) - d_dropWeight;
                                        if (d_dropWater < 0.0)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("加水量异常，请检查配方！", "温馨提示", MessageBoxButtons.OK, false);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Abnormal water addition, please check the formula！", "Tips", MessageBoxButtons.OK, false);
                                            if (!b_addWaitList)
                                            {
                                                if (s_cup != null)
                                                {
                                                    s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                    s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                    s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                    BatchHeadShow("");
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }

                                        d_dropWeight = 0.0;
                                    }
                                }
                                if (lis_dropWeight.Count == 0)
                                {
                                    lis_dropWeight.Add(d_dropWeight);
                                }
                                foreach (DataRow dr1 in dt_data1.Rows)
                                {
                                    i_num++;
                                    List<string> lis_dye_Detail = new List<string>();
                                    lis_dye_Detail.Add("0");
                                    lis_dye_Detail.Add(s_maxCupNum);
                                    lis_dye_Detail.Add(txt_FormulaCode.Text);//s_formulaCode
                                    lis_dye_Detail.Add(txt_VersionNum.Text);//s_versionNum
                                    lis_dye_Detail.Add(dr[3].ToString());//Code
                                    lis_dye_Detail.Add(i_num.ToString());//StepNum
                                    lis_dye_Detail.Add(dr1[1].ToString());//TechnologyName
                                    lis_dye_Detail.Add("0");//Finish
                                                             //RotorSpeed
                                    if (dr1[7] is DBNull)
                                    {
                                        lis_dye_Detail.Add("0");
                                    }
                                    else
                                    {
                                        lis_dye_Detail.Add(dr1[7].ToString());
                                    }
                                    if (dr1[1].ToString() == "温控" || dr1[1].ToString() == "Temperature control")
                                    {
                                        lis_dye_Detail.Add(dr1[4].ToString());//Temp
                                        lis_dye_Detail.Add(dr1[5].ToString());//TempSpeed
                                        lis_dye_Detail.Add(dr1[2].ToString());//Time
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum,FormulaCode,VersionNum, Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                        " Temp, TempSpeed, Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "'," +
                                        " '" + lis_dye_Detail[8] + "'," +
                                        " '" + lis_dye_Detail[9] + "'," +
                                        " '" + lis_dye_Detail[10] + "'," +
                                        " '" + lis_dye_Detail[11] + "',2);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                    else if (dr1[1].ToString() == "冷行" || dr1[1].ToString() == "洗杯" || dr1[1].ToString() == "排液" || dr1[1].ToString() == "搅拌"
                                        || dr1[1].ToString() == "Cool line" || dr1[1].ToString() == "Wash the cup" || dr1[1].ToString() == "Drainage" || dr1[1].ToString() == "Stir")
                                    {
                                        if (dr1[1].ToString() == "排液")
                                        {
                                            b_insert = false;
                                            i_number++;
                                        }
                                        lis_dye_Detail.Add(dr1[2].ToString());//Time
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                        " Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "', '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "',2);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                    else if ((dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                        || (dr1[1].ToString() == "Add A" || dr1[1].ToString() == "Add B" || dr1[1].ToString() == "Add C" || dr1[1].ToString() == "Add D" || dr1[1].ToString() == "Add E"))
                                    {
                                        string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                        DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                        //lis_dye_Detail.Add(dt_data2.Rows[0]["s_formulaCode"].ToString());
                                        //lis_dye_Detail.Add(dt_data2.Rows[0]["s_versionNum"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantCode"].ToString());
                                        lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["UnitOfAccount"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["BottleNum"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["SettingConcentration"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["RealConcentration"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantName"].ToString());
                                        lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["RealDropWeight"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["BottleSelection"].ToString());
                                        lis_dye_Detail.Add(dt_data2.Rows[0]["MinWeight"].ToString());
                                        d_dropWater = Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(_lis_handleBathRatio[i_nNum].Text) - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text) - lis_dropWeight[i_number];
                                        if (!b_insert)
                                        {
                                            if (d_dropWater <= 0.0)
                                            {
                                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                    FADM_Form.CustomMessageBox.Show("加水量异常，请检查配方！", "温馨提示", MessageBoxButtons.OK, false);
                                                else
                                                    FADM_Form.CustomMessageBox.Show("Abnormal water addition, please check the formula！", "Tips", MessageBoxButtons.OK, false);
                                                if (!b_addWaitList)
                                                {
                                                    if (s_cup != null)
                                                    {
                                                        s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                        s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                        s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                        BatchHeadShow("");
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                        lis_dye_Detail.Add(!b_insert ? (Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_dropWater) : string.Format("{0:F3}", d_dropWater)) : "0.0");
                                        b_insert = true;
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,AssistantCode," +
                                        " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                        " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                        " BottleSelection,MinWeight,ObjectWaterWeight,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "'," +
                                        " '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "', '" + lis_dye_Detail[10] + "'," +
                                        " '" + lis_dye_Detail[11] + "', '" + lis_dye_Detail[12] + "', '" + lis_dye_Detail[13] + "', '" + lis_dye_Detail[14] + "', '"
                                        + lis_dye_Detail[15] + "', '" + lis_dye_Detail[16] + "', '" + lis_dye_Detail[17] + "', '" + lis_dye_Detail[18] + "', '" + lis_dye_Detail[19] + "', '" + lis_dye_Detail[20] + "',2);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                    else if (dr1[1].ToString() == "加水" || dr1[1].ToString() == "Add Water")
                                    {
                                        double d_dropWater1 = Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(_lis_handleBathRatio[i_nNum].Text) * Convert.ToDouble(dr1[2].ToString()) / 100 - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text);
                                        if (d_dropWater1 <= 0.0)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("加水量异常，请检查配方！", "温馨提示", MessageBoxButtons.OK, false);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Abnormal water addition, please check the formula！", "Tips", MessageBoxButtons.OK, false);
                                            if (!b_addWaitList)
                                            {
                                                if (s_cup != null)
                                                {
                                                    s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                    s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                    s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                    BatchHeadShow("");
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                        lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_dropWater1) : string.Format("{0:F3}", d_dropWater1));
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,ObjectWaterWeight,DyeType" +
                                        " ) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "'," +
                                        " '" + lis_dye_Detail[6] + "'," +
                                        " '" + lis_dye_Detail[7] + "'," +
                                        " '" + lis_dye_Detail[8] + "'," +
                                        " '" + lis_dye_Detail[9] + "',2);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                    else
                                    {
                                        if (!b_addWaitList)
                                        {
                                            s_sql = "INSERT INTO dye_details (" +
                                        " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,DyeType" +
                                        " ) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                        " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                        " '" + lis_dye_Detail[5] + "'," +
                                        " '" + lis_dye_Detail[6] + "'," +
                                        " '" + lis_dye_Detail[7] + "'," +
                                        " '" + lis_dye_Detail[8] + "',2);";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                }
                            }

                            i_nNum++;
                        }
                    }
                    if (!b_addWaitList)
                    {
                        //修改杯号正在使用
                        s_sql = "Update cup_details set IsUsing = 1 where CupNum = '" + s_maxCupNum + "';";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        //更新批次表
                        //BatchHeadShow((Convert.ToInt16(s_maxCupNum) + 1).ToString());
                        BatchHeadShow("");

                        if (this._b_newAdd)
                        {
                            btn_FormulaCodeAdd_Click(null, null);
                        }
                    }
                    else
                    {
                        //加入等待列表
                        s_sqltemp = "SELECT MAX(IndexNum) as maxnum  FROM wait_list  where Type = " + s_stage + ";";
                        dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                        int i_nIndex = 0;
                        if (dt_temp.Rows[0]["maxnum"] is DBNull)
                        {
                            i_nIndex = 1;
                        }
                        else
                        {
                            i_nIndex = Convert.ToInt16(dt_temp.Rows[0]["maxnum"]) + 1;
                        }

                        s_sqltemp = "Insert into wait_list(FormulaCode,VersionNum,IndexNum,CupNum,Type)values('" + txt_FormulaCode.Text + "','" + txt_VersionNum.Text + "'," + i_nIndex.ToString() + "," + txt_CupNum.Text + "," + s_stage.ToString() + ");";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sqltemp);
                        _b_updateWait = true;

                        BatchHeadShow("");
                    }
                }
                catch
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("当前配方存在空瓶号，请检查再加入！", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("The current formula has an empty bottle number. Please check before adding it！", "Tips", MessageBoxButtons.OK, false);
                    if (s_cup != null)
                    {
                        string s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        BatchHeadShow("");
                    }
                }
            }
        }

        //修改后加入
        private void ReBatchAdd(string sCup)
        {



            string s_cup = null;
            try
            {

                string s_stage = null;
                if (_s_stage == "前处理")
                {
                    s_stage = "1";
                }
                else if (_s_stage == "后处理")
                {
                    s_stage = "3";
                }
                else
                {
                    s_stage = "2";
                }

                string s_sqltemp = "";
                DataTable dt_temp = new DataTable(); ;
                string s_maxCupNum = "0";
                //获取滴液杯信息
                //判断是否指定杯号
                //if (txt_CupNum.Text != "" && txt_CupNum.Text != "0")
                //{
                //    //判断此杯是否正在使用，如果使用就提示加入失败
                //    s_sqltemp = "SELECT  CupNum FROM cup_details WHERE  CupNum = '" + txt_CupNum.Text + "' and IsUsing = 1  and Type = " + s_stage + ";";
                //    dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                //    if (dt_cup_details.Rows.Count == 1)
                //    {
                //         FADM_Form.CustomMessageBox.Show("已存在杯号在操作,请空闲后再加入", "操作异常", MessageBoxButtons.OK,false);
                //        return;
                //    }
                //    s_maxCupNum = txt_CupNum.Text;
                //}
                //else
                //{
                //    //进去随机分配杯号
                //    s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0 and IsFixed = 0 and enable = 1 and Type = " + s_stage + " order by CupNum ;";
                //    dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                //    if (dt_cup_details.Rows.Count < 1)
                //    {
                //        //没有空闲杯
                //         FADM_Form.CustomMessageBox.Show("所有杯号在操作,请空闲后再加入", "操作异常", MessageBoxButtons.OK,false);
                //        return;
                //    }
                //    else
                //    {
                //        //记录杯号，加入批次
                //        s_maxCupNum = dt_cup_details.Rows[0][0].ToString();
                //    }
                //}
                s_maxCupNum = sCup;

                string s_sql = "";
                DataTable dt_head = new DataTable();
                DataTable dt_details = new DataTable();
                if (txt_FormulaCode.Text != null && txt_FormulaCode.Text != "" && txt_VersionNum.Text != null && txt_VersionNum.Text != "")
                {
                    s_sql = "SELECT * FROM formula_head WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND VersionNum = " + txt_VersionNum.Text + ";";

                    dt_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    if (dt_head.Rows.Count == 0)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                        return;
                    }


                    s_sql = "SELECT * FROM formula_details WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND VersionNum = " + txt_VersionNum.Text + " order by IndexNum;";
                    dt_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    //不是单独滴液时，有可能是中控下发下来只做后处理的，先不判断
                    if (_s_stage != "后处理")
                    {
                        if (dt_details.Rows.Count == 0)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                            return;
                        }
                    }

                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                    return;
                }


                List<string> lis_head = new List<string>();
                lis_head.Add(s_maxCupNum);
                lis_head.Add(dt_head.Rows[0]["FormulaCode"].ToString());
                lis_head.Add(dt_head.Rows[0]["VersionNum"].ToString());
                lis_head.Add(dt_head.Rows[0]["State"].ToString());
                lis_head.Add(dt_head.Rows[0]["FormulaName"].ToString());
                lis_head.Add(dt_head.Rows[0]["ClothType"].ToString());
                lis_head.Add(dt_head.Rows[0]["Customer"].ToString());
                lis_head.Add(dt_head.Rows[0]["AddWaterChoose"].ToString() == "False" || dt_head.Rows[0]["AddWaterChoose"].ToString() == "0" ? "0" : "1");
                lis_head.Add(dt_head.Rows[0]["CompoundBoardChoose"].ToString() == "False" || dt_head.Rows[0]["CompoundBoardChoose"].ToString() == "0" ? "0" : "1");
                lis_head.Add(dt_head.Rows[0]["ClothWeight"].ToString());
                lis_head.Add(dt_head.Rows[0]["BathRatio"].ToString());
                lis_head.Add(dt_head.Rows[0]["TotalWeight"].ToString());
                lis_head.Add(dt_head.Rows[0]["Operator"].ToString());
                lis_head.Add(dt_head.Rows[0]["CupCode"].ToString());
                lis_head.Add(dt_head.Rows[0]["CreateTime"].ToString());
                lis_head.Add(dt_head.Rows[0]["ObjectAddWaterWeight"].ToString());
                lis_head.Add(dt_head.Rows[0]["TestTubeObjectAddWaterWeight"].ToString());
                lis_head.Add(dt_head.Rows[0]["DyeingCode"].ToString());

                lis_head.Add(txt_Non_AnhydrationWR.Text);
                lis_head.Add(txt_AnhydrationWR.Text);

                if (_s_stage == "后处理")
                {

                    lis_head.Add("0"/*txt_HandleBathRatio.Text*/);
                    lis_head.Add("0");
                    lis_head.Add("0");
                    lis_head.Add("0");
                    lis_head.Add("0");
                    lis_head.Add("0");
                    lis_head.Add(_s_stage);
                }
                else
                {
                    lis_head.Add("0");
                    lis_head.Add("0");
                    lis_head.Add("0");
                    lis_head.Add("0");
                    lis_head.Add("0");
                    lis_head.Add("0");

                    lis_head.Add(_s_stage);
                }

                lis_head.Add(dt_head.Rows[0]["HandleBRList"].ToString());


                s_cup = lis_head[0];

                // 添加进批次表头
                s_sql = "INSERT INTO drop_head (" +
                            " CupNum, FormulaCode, VersionNum, State ,FormulaName, ClothType," +
                            " Customer, AddWaterChoose, CompoundBoardChoose, ClothWeight, BathRatio, TotalWeight," +
                            " Operator, CupCode, CreateTime, ObjectAddWaterWeight, TestTubeObjectAddWaterWeight,DyeingCode,Non_AnhydrationWR,AnhydrationWR,HandleBathRatio,Handle_Rev1,Handle_Rev2,Handle_Rev3,Handle_Rev4,Handle_Rev5,Stage,HandleBRList) VALUES(" +
                            " '" + lis_head[0] + "', '" + lis_head[1] + "', '" + lis_head[2] + "'," +
                            " '" + lis_head[3] + "', '" + lis_head[4] + "', '" + lis_head[5] + "'," +
                            " '" + lis_head[6] + "', '" + lis_head[7] + "', '" + lis_head[8] + "'," +
                            " '" + lis_head[9] + "', '" + lis_head[10] + "', '" + lis_head[11] + "'," +
                           " '" + lis_head[12] + "', '" + lis_head[13] + "', '" + lis_head[14] + "'," +
                            " '" + lis_head[15] + "','" + lis_head[16] + "','" + lis_head[17] + "', '" + lis_head[18] + "', '" + lis_head[19]
                                     + "', '" + lis_head[20] + "', '" + lis_head[21] + "', '" + lis_head[22] + "', '" + lis_head[23] + "', '" + lis_head[24] + "', '" + lis_head[25] + "', '" + lis_head[26] + "', '" + lis_head[27] + "');";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);


                //添加进批次详细表
                foreach (DataRow dr in dt_details.Rows)
                {

                    List<string> lis_detail = new List<string>();
                    lis_detail.Add(s_maxCupNum);
                    foreach (DataColumn dc in dt_details.Columns)
                    {
                        if (dc.ColumnName == "BottleSelection")
                        {
                            lis_detail.Add((dr[dc]).ToString() == "False" || (dr[dc]).ToString() == "0" ? "0" : "1");
                            continue;
                        }
                        lis_detail.Add((dr[dc]).ToString());
                    }

                    //添加进滴液详细表
                    s_sql = "INSERT INTO drop_details (" +
                                " CupNum, FormulaCode, VersionNum, IndexNum, AssistantCode," +
                                " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                " BottleSelection) VALUES( '" + lis_detail[0] + "', '" + lis_detail[1] + "'," +
                                " '" + lis_detail[2] + "', '" + lis_detail[3] + "', '" + lis_detail[4] + "'," +
                                " '" + lis_detail[5] + "', '" + lis_detail[6] + "', '" + lis_detail[7] + "'," +
                                " '" + lis_detail[8] + "', '" + lis_detail[9] + "', '" + lis_detail[10] + "'," +
                                " '" + lis_detail[11] + "', '" + string.Format("{0:F}", 0) + "', '" + lis_detail[13] + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                }

                //插入后处理详细步骤表
                s_sql = "select * from dyeing_code where DyeingCode ='" + txt_DyeingCode.Text + "' order by IndexNum;";
                DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                int num = 0;


                if (_s_stage == "后处理")
                {
                    int i_nNum = 0;
                    //先把助剂代码写入对应列表
                    foreach (DataRow dr in dt_data.Rows)
                    {
                        //显示染色工艺
                        if (dr[1].ToString() == "1" && i_nNum == 0)
                        {
                            string s_sql1 = "SELECT * FROM dyeing_process where Code = '" + dr[3].ToString() + "' order by StepNum;";
                            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);

                            //先把加水量计算出来
                            double d_dropWeight = 0.0;
                            double d_dropWater = 0.0;
                            foreach (DataRow dr1 in dt_data1.Rows)
                            {
                                if ((dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                    || (dr1[1].ToString() == "Add A" || dr1[1].ToString() == "Add B" || dr1[1].ToString() == "Add C" || dr1[1].ToString() == "Add D" || dr1[1].ToString() == "Add E"))
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    d_dropWeight += Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString());
                                }
                            }
                            

                            foreach (DataRow dr1 in dt_data1.Rows)
                            {
                                num++;
                                List<string> lis_dye_Detail = new List<string>();
                                lis_dye_Detail.Add("0");
                                lis_dye_Detail.Add(s_maxCupNum);
                                lis_dye_Detail.Add(txt_FormulaCode.Text);//s_formulaCode
                                lis_dye_Detail.Add(txt_VersionNum.Text);//s_versionNum
                                lis_dye_Detail.Add(dr[3].ToString());//Code
                                lis_dye_Detail.Add(num.ToString());//StepNum
                                lis_dye_Detail.Add(dr1[1].ToString());//TechnologyName
                                lis_dye_Detail.Add("0");//Finish
                                //RotorSpeed
                                if (dr1[7] is DBNull)
                                {
                                    lis_dye_Detail.Add("0");
                                }
                                else
                                {
                                    lis_dye_Detail.Add(dr1[7].ToString());
                                }
                                if (dr1[1].ToString() == "温控" || dr1[1].ToString() == "Temperature control")
                                {
                                    lis_dye_Detail.Add(dr1[4].ToString());//Temp
                                    lis_dye_Detail.Add(dr1[5].ToString());//TempSpeed
                                    lis_dye_Detail.Add(dr1[2].ToString());//Time

                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum,FormulaCode,VersionNum, Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Temp, TempSpeed, Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "'," +
                                    " '" + lis_dye_Detail[8] + "'," +
                                    " '" + lis_dye_Detail[9] + "'," +
                                    " '" + lis_dye_Detail[10] + "'," +
                                    " '" + lis_dye_Detail[11] + "',1);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                                else if (dr1[1].ToString() == "冷行" || dr1[1].ToString() == "洗杯" || dr1[1].ToString() == "排液" || dr1[1].ToString() == "搅拌"
                                    || dr1[1].ToString() == "Cool line" || dr1[1].ToString() == "Wash the cup" || dr1[1].ToString() == "Drainage" || dr1[1].ToString() == "Stir")
                                {
                                    lis_dye_Detail.Add(dr1[2].ToString());//Time

                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "', '" + lis_dye_Detail[8] + "'," +
                                    " '" + lis_dye_Detail[9] + "',1);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                                else if ((dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                    || (dr1[1].ToString() == "Add A" || dr1[1].ToString() == "Add B" || dr1[1].ToString() == "Add C" || dr1[1].ToString() == "Add D" || dr1[1].ToString() == "Add E"))
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    //lis_dye_Detail.Add(dt_data2.Rows[0]["s_formulaCode"].ToString());
                                    //lis_dye_Detail.Add(dt_data2.Rows[0]["s_versionNum"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantCode"].ToString());
                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["UnitOfAccount"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["BottleNum"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["SettingConcentration"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["RealConcentration"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantName"].ToString());
                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100): string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["RealDropWeight"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["BottleSelection"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["MinWeight"].ToString());

                                    lis_dye_Detail.Add("0.0");

                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,AssistantCode," +
                                    " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                    " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                    " BottleSelection,MinWeight,ObjectWaterWeight,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "'," +
                                    " '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "', '" + lis_dye_Detail[10] + "'," +
                                    " '" + lis_dye_Detail[11] + "', '" + lis_dye_Detail[12] + "', '" + lis_dye_Detail[13] + "', '" + lis_dye_Detail[14] + "', '"
                                    + lis_dye_Detail[15] + "', '" + lis_dye_Detail[16] + "', '" + lis_dye_Detail[17] + "', '" + lis_dye_Detail[18] + "', '" + lis_dye_Detail[19] + "'," +
                                    " '" + lis_dye_Detail[20] + "',1);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                                else if (dr1[1].ToString() == "加水" || dr1[1].ToString() == "Add Water")
                                {
                                    //第一个染色无加水
                                    double d_dropWater1 = 150 * Convert.ToDouble(dr1[2].ToString()) / 100 - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text);
                                    if (d_dropWater1 <= 0.0)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            FADM_Form.CustomMessageBox.Show("加水量异常，请检查配方！", "温馨提示", MessageBoxButtons.OK, false);
                                        else
                                            FADM_Form.CustomMessageBox.Show("Abnormal water addition, please check the formula！", "Tips", MessageBoxButtons.OK, false);
                                        if (s_cup != null)
                                        {
                                            s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                            s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                            s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                            BatchHeadShow("");
                                            return;
                                        }
                                    }
                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_dropWater1): string.Format("{0:F3}", d_dropWater1));

                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,ObjectWaterWeight,DyeType" +
                                    " ) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "'," +
                                    " '" + lis_dye_Detail[6] + "'," +
                                    " '" + lis_dye_Detail[7] + "'," +
                                    " '" + lis_dye_Detail[8] + "'," +
                                    " '" + lis_dye_Detail[9] + "',1);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                                else
                                {
                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,DyeType" +
                                    " ) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "'," +
                                    " '" + lis_dye_Detail[6] + "'," +
                                    " '" + lis_dye_Detail[7] + "'," +
                                    " '" + lis_dye_Detail[8] + "',1);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                            }
                        }
                        else
                        {
                            string s_sql1 = "SELECT * FROM dyeing_process where Code = '" + dr[3].ToString() + "' order by StepNum;";
                            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);
                            //先把加水量计算出来
                            double d_dropWeight = 0.0;
                            double d_dropWater = 0.0;
                            bool b_insert = false;
                            foreach (DataRow dr1 in dt_data1.Rows)
                            {
                                if ((dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                    || (dr1[1].ToString() == "Add A" || dr1[1].ToString() == "Add B" || dr1[1].ToString() == "Add C" || dr1[1].ToString() == "Add D" || dr1[1].ToString() == "Add E"))
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    d_dropWeight += Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString());
                                }
                            }
                            d_dropWater = Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(_lis_handleBathRatio[i_nNum].Text) - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text) - d_dropWeight;
                            if (d_dropWater < 0.0)
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("加水量异常，请检查配方！", "温馨提示", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("Abnormal water addition, please check the formula！", "Tips", MessageBoxButtons.OK, false);

                                if (s_cup != null)
                                {
                                    s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    BatchHeadShow("");
                                    return;
                                }
                            }
                            foreach (DataRow dr1 in dt_data1.Rows)
                            {
                                num++;
                                List<string> lis_dye_Detail = new List<string>();
                                lis_dye_Detail.Add("0");
                                lis_dye_Detail.Add(s_maxCupNum);
                                lis_dye_Detail.Add(txt_FormulaCode.Text);//s_formulaCode
                                lis_dye_Detail.Add(txt_VersionNum.Text);//s_versionNum
                                lis_dye_Detail.Add(dr[3].ToString());//Code
                                lis_dye_Detail.Add(num.ToString());//StepNum
                                lis_dye_Detail.Add(dr1[1].ToString());//TechnologyName
                                lis_dye_Detail.Add("0");//Finish
                                //RotorSpeed
                                if (dr1[7] is DBNull)
                                {
                                    lis_dye_Detail.Add("0");
                                }
                                else
                                {
                                    lis_dye_Detail.Add(dr1[7].ToString());
                                }
                                if (dr1[1].ToString() == "温控" || dr1[1].ToString() == "Temperature control")
                                {
                                    lis_dye_Detail.Add(dr1[4].ToString());//Temp
                                    lis_dye_Detail.Add(dr1[5].ToString());//TempSpeed
                                    lis_dye_Detail.Add(dr1[2].ToString());//Time

                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum,FormulaCode,VersionNum, Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Temp, TempSpeed, Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "'," +
                                    " '" + lis_dye_Detail[8] + "'," +
                                    " '" + lis_dye_Detail[9] + "'," +
                                    " '" + lis_dye_Detail[10] + "'," +
                                    " '" + lis_dye_Detail[11] + "',2);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                                else if (dr1[1].ToString() == "冷行" || dr1[1].ToString() == "洗杯" || dr1[1].ToString() == "排液" || dr1[1].ToString() == "搅拌"
                                    || dr1[1].ToString() == "Cool line" || dr1[1].ToString() == "Wash the cup" || dr1[1].ToString() == "Drainage" || dr1[1].ToString() == "Stir")
                                {
                                    lis_dye_Detail.Add(dr1[2].ToString());//Time

                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "', '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "',2);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                                else if ((dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                    || (dr1[1].ToString() == "Add A" || dr1[1].ToString() == "Add B" || dr1[1].ToString() == "Add C" || dr1[1].ToString() == "Add D" || dr1[1].ToString() == "Add E"))
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    //lis_dye_Detail.Add(dt_data2.Rows[0]["s_formulaCode"].ToString());
                                    //lis_dye_Detail.Add(dt_data2.Rows[0]["s_versionNum"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantCode"].ToString());
                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["UnitOfAccount"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["BottleNum"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["SettingConcentration"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["RealConcentration"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantName"].ToString());
                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100): string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["RealDropWeight"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["BottleSelection"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["MinWeight"].ToString());

                                    lis_dye_Detail.Add(!b_insert ? Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_dropWater): string.Format("{0:F3}", d_dropWater) : "0.0");
                                    if (!b_insert)
                                    {
                                        if (d_dropWater <= 0.0)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("加水量异常，请检查配方！", "温馨提示", MessageBoxButtons.OK, false);
                                            else
                                                FADM_Form.CustomMessageBox.Show("Abnormal water addition, please check the formula！", "Tips", MessageBoxButtons.OK, false);

                                            if (s_cup != null)
                                            {
                                                s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                BatchHeadShow("");
                                                return;
                                            }

                                        }
                                    }
                                    b_insert = true;

                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,AssistantCode," +
                                    " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                    " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                    " BottleSelection,MinWeight,ObjectWaterWeight,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "'," +
                                    " '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "', '" + lis_dye_Detail[10] + "'," +
                                    " '" + lis_dye_Detail[11] + "', '" + lis_dye_Detail[12] + "', '" + lis_dye_Detail[13] + "', '" + lis_dye_Detail[14] + "', '"
                                    + lis_dye_Detail[15] + "', '" + lis_dye_Detail[16] + "', '" + lis_dye_Detail[17] + "', '" + lis_dye_Detail[18] + "', '" + lis_dye_Detail[19] + "', '" + lis_dye_Detail[20] + "',2);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                                else if (dr1[1].ToString() == "加水" || dr1[1].ToString() == "Add Water")
                                {
                                    double d_dropWater1 = Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(_lis_handleBathRatio[i_nNum].Text) * Convert.ToDouble(dr1[2].ToString()) / 100 - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text);
                                    if (d_dropWater1 <= 0.0)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            FADM_Form.CustomMessageBox.Show("加水量异常，请检查配方！", "温馨提示", MessageBoxButtons.OK, false);
                                        else
                                            FADM_Form.CustomMessageBox.Show("Abnormal water addition, please check the formula！", "Tips", MessageBoxButtons.OK, false);
                                        if (s_cup != null)
                                        {
                                            s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                            s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                            s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                            BatchHeadShow("");
                                            return;
                                        }
                                    }
                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_dropWater1) : string.Format("{0:F3}", d_dropWater1));

                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,ObjectWaterWeight,DyeType" +
                                    " ) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "'," +
                                    " '" + lis_dye_Detail[6] + "'," +
                                    " '" + lis_dye_Detail[7] + "'," +
                                    " '" + lis_dye_Detail[8] + "'," +
                                    " '" + lis_dye_Detail[9] + "',2);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                                else
                                {
                                    s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,DyeType" +
                                    " ) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "'," +
                                    " '" + lis_dye_Detail[6] + "'," +
                                    " '" + lis_dye_Detail[7] + "'," +
                                    " '" + lis_dye_Detail[8] + "',2);";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }
                            }
                        }
                    }
                }

                //修改杯号正在使用
                s_sql = "Update cup_details set IsUsing = 1 where CupNum = '" + s_maxCupNum + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                //更新批次表
                //BatchHeadShow((Convert.ToInt16(s_maxCupNum) + 1).ToString());
                BatchHeadShow("");

                if (this._b_newAdd)
                {
                    btn_FormulaCodeAdd_Click(null, null);
                }
            }
            catch
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("当前配方存在空瓶号，请检查再加入！", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("The current formula has an empty bottle number. Please check before adding it！", "Tips", MessageBoxButtons.OK, false); 
                if (s_cup != null)
                {
                    string s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_cup + ";";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    BatchHeadShow("");
                }
            }
        }

        /// <summary>
        /// 矢能设置
        /// </summary>
        private void Enabled_set()
        {
            try
            {
            //获取TextBox控件的矢能

            //txt_DyeingCode.SelectedIndex = -1;
            //dgv_Dyeing.Rows.Clear();
            //dgv_Details.DataSource = null;
            again:
                string s_sql = "SELECT * FROM enabled_set WHERE MyID = 1;";
                DataTable dt_enabled = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                if (dt_enabled.Rows.Count <= 0)
                {
                    s_sql = "INSERT INTO enabled_set (MyID) VALUES(1);";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    goto again;
                }
                foreach (DataColumn mDc in dt_enabled.Columns)
                {
                    if (mDc.Caption.ToString() != "MyID")
                    {
                        string s_name = mDc.Caption.ToString();
                        foreach (Control c in this.grp_FormulaData.Controls)
                        {
                            if ((c is TextBox || c is CheckBox || c is ComboBox) && c.Name == s_name && c.Name != "txt_FormulaGroup")
                            {
                                c.Enabled = ((dt_enabled.Rows[0][mDc].ToString()) == "0" ? false : true);
                            }
                        }
                    }
                }
                dgv_FormulaData.Enabled = true;
                txt_FormulaCode.Enabled = false;
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "Enabled_set", MessageBoxButtons.OK, true);
            }
        }

        /// <summary>
        /// 更新配方表
        /// </summary>
        /// <param s_name="_CurrentRowIndex">当前行号</param>
        private void UpdataFormulaData(int _CurrentRowIndex)
        {
            try
            {
                if (txt_ClothWeight.Text == "" || txt_TotalWeight.Text == "" || dgv_FormulaData[3, _CurrentRowIndex].Value == null || dgv_FormulaData[3, _CurrentRowIndex].Value.ToString() == "")
                {
                    return;
                }

                DataTable dt_bottlenum = new DataTable();

                if (_CurrentRowIndex >= dgv_FormulaData.Rows.Count - 1)
                {
                    return;
                }
                string s_sql = null;
                if (txt_VersionNum.Text != null)
                {
                    s_sql = "SELECT *  FROM formula_details WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                " VersionNum = '" + txt_VersionNum.Text + "' AND IndexNum ='" + dgv_FormulaData[0, _CurrentRowIndex].Value.ToString() + "'  order by IndexNum; ";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    if (dt_data.Rows.Count > 0)
                    {
                        string str_code = (dt_data.Rows[0][dt_data.Columns["AssistantCode"]]).ToString();
                        if (str_code != dgv_FormulaData[1, _CurrentRowIndex].Value.ToString())
                        {
                            s_sql = "SELECT *  FROM formula_details WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                " VersionNum = '" + txt_VersionNum.Text + "' AND AssistantCode ='" + dgv_FormulaData[1, _CurrentRowIndex].Value.ToString() + "'  order by IndexNum; ";
                            dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                            //判断原来有没这个助剂，如果有就不更新是否收到选瓶
                            if (dt_data.Rows.Count == 0)
                            {
                                dgv_FormulaData[10, _CurrentRowIndex].Value = 0;
                            }
                        }
                    }
                }



                //获取染助剂资料
                s_sql = "SELECT *  FROM assistant_details WHERE" +
                            " AssistantCode = '" + dgv_FormulaData[1, _CurrentRowIndex].Value.ToString() + "' ; ";

                DataTable dt_assistantdetails = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                if (dt_assistantdetails.Rows.Count > 0)
                {
                    dgv_FormulaData[4, _CurrentRowIndex].Value = (dt_assistantdetails.Rows[0][5].ToString());
                    dgv_FormulaData[2, _CurrentRowIndex].Value = dt_assistantdetails.Rows[0][3].ToString();
                    dgv_FormulaData[9, _CurrentRowIndex].Value = "0.00";
                    //获取当前染助剂所有母液瓶资料
                    s_sql = "SELECT BottleNum, SettingConcentration ,RealConcentration, DropMinWeight" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv_FormulaData[1, _CurrentRowIndex].Value.ToString() + "'" +
                                " AND RealConcentration != 0 ORDER BY BottleNum ;";
                    dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    //未找到一个合适的瓶
                    if (dt_bottlenum.Rows.Count == 0)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("当前染助剂代码未发现母液瓶！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("No mother liquor bottle found for the current dyeing agent code！", "Tips", MessageBoxButtons.OK, false);

                        for (int i = 1; i < dgv_FormulaData.Columns.Count - 1; i++)
                        {
                            dgv_FormulaData.CurrentRow.Cells[i].Value = null;
                        }

                        return;
                    }
                    List<string> lis_bottleNum = new List<string>();
                    foreach (DataRow mdr in dt_bottlenum.Rows)
                    {
                        lis_bottleNum.Add(mdr[0].ToString());
                    }
                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_FormulaData[5, _CurrentRowIndex];
                    for (int j = 0; j < lis_bottleNum.Count; j++)
                    {
                        for (int i = 0; i < dd.Items.Count; i++)
                        {
                            if (dd.Items[i].ToString() == lis_bottleNum[j])
                            {
                                goto next;
                            }
                        }
                        dd.Value = null;
                        dd.DataSource = lis_bottleNum;
                        break;
                    next:
                        continue;
                    }




                    dt_bottlenum.Clear();
                    //跟据设定浓度重新排序
                    s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight,SyringeType" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv_FormulaData[1, _CurrentRowIndex].Value.ToString() + "'" +
                                " AND RealConcentration != 0 ORDER BY SettingConcentration DESC;";

                    dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    for (int i = 0; i < dt_bottlenum.Rows.Count; i++)
                    {
                        double d_objectDropWeight = 0;
                        //判断是否需要自动选瓶
                        if (dgv_FormulaData[10, _CurrentRowIndex].Value == null ||
                            dgv_FormulaData[10, _CurrentRowIndex].Value.ToString() == "0")
                        {
                            //需要自动选瓶
                            if (dgv_FormulaData.Rows[_CurrentRowIndex].Cells[4].Value != null)
                            {
                                if (dgv_FormulaData.Rows[_CurrentRowIndex].Cells[4].Value.ToString() == "%")
                                {
                                    //染料
                                    d_objectDropWeight = (Convert.ToDouble(txt_ClothWeight.Text) *
                                        Convert.ToDouble(dgv_FormulaData[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(dt_bottlenum.Rows[i][2].ToString()));
                                }
                                else
                                {
                                    //助剂
                                    d_objectDropWeight = (Convert.ToDouble(txt_TotalWeight.Text) *
                                        Convert.ToDouble(dgv_FormulaData[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(dt_bottlenum.Rows[i][2].ToString()));

                                }
                                if (Convert.ToDouble(String.Format("{0:F3}", d_objectDropWeight)) >=
                                    Convert.ToDouble(String.Format("{0:F3}", dt_bottlenum.Rows[i][3])))
                                {

                                    dd.Value = dt_bottlenum.Rows[i][0].ToString();
                                    dgv_FormulaData[6, _CurrentRowIndex].Value = dt_bottlenum.Rows[i][1].ToString();
                                    dgv_FormulaData[7, _CurrentRowIndex].Value = dt_bottlenum.Rows[i][2].ToString();
                                    dgv_FormulaData[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight): String.Format("{0:F3}", d_objectDropWeight);
                                    break;
                                }
                                else
                                {
                                    if (i == dt_bottlenum.Rows.Count - 1)
                                    {
                                        if (d_objectDropWeight >= 0.1)
                                        {
                                            dd.Value = dt_bottlenum.Rows[i][0].ToString();
                                            dgv_FormulaData[6, _CurrentRowIndex].Value = dt_bottlenum.Rows[i][1].ToString();
                                            dgv_FormulaData[7, _CurrentRowIndex].Value = dt_bottlenum.Rows[i][2].ToString();
                                            dgv_FormulaData[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight) : String.Format("{0:F3}", d_objectDropWeight);
                                        }
                                        else
                                        {
                                            dd.Value = null;
                                            dgv_FormulaData[6, _CurrentRowIndex].Value = null;
                                            dgv_FormulaData[7, _CurrentRowIndex].Value = null;
                                            dgv_FormulaData[8, _CurrentRowIndex].Value = null;
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            //不需要自动选瓶

                            //获取当前染助剂所有母液瓶资料
                            foreach (DataRow mdr in dt_bottlenum.Rows)
                            {
                                if (dd.Value.ToString() == mdr[0].ToString())
                                {
                                    dgv_FormulaData[5, _CurrentRowIndex].Value = mdr[0].ToString();
                                    dgv_FormulaData[6, _CurrentRowIndex].Value = mdr[1].ToString();
                                    dgv_FormulaData[7, _CurrentRowIndex].Value = mdr[2].ToString();
                                    break;
                                }
                            }

                            //计算目标滴液量
                            if (dgv_FormulaData[4, _CurrentRowIndex].Value != null)
                            {
                                if (dgv_FormulaData[4, _CurrentRowIndex].Value.ToString() == "%")
                                {
                                    //染料
                                    d_objectDropWeight = (Convert.ToDouble(txt_ClothWeight.Text) *
                                        Convert.ToDouble(dgv_FormulaData[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(dgv_FormulaData[7, _CurrentRowIndex].Value.ToString()));
                                }
                                else
                                {
                                    //助剂
                                    d_objectDropWeight = (Convert.ToDouble(txt_TotalWeight.Text) *
                                        Convert.ToDouble(dgv_FormulaData[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(dgv_FormulaData[7, _CurrentRowIndex].Value.ToString()));

                                }

                                dgv_FormulaData[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight) : String.Format("{0:F3}", d_objectDropWeight);
                                break;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "UpdataFormulaData", MessageBoxButtons.OK, true);
            }
        }

        private void txt_StartCup_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberTextbox_KeyPress(e);
        }

        //开始滴液按钮点击事件
        public void btn_Start_Click(object sender, EventArgs e)
        {
            if (FADM_Object.Communal._b_isDripping)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("机台正在运行，请待机状态下点击开始滴液按钮！", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("The machine is running, please click the start drip button in standby mode！", "Tips", MessageBoxButtons.OK, false);
                return;
            }
            FADM_Object.Communal._b_isDripping = true;
            if (FADM_Object.Communal.ReadMachineStatus() != 0)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("机台正在运行，请待机状态下点击开始滴液按钮！", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("The machine is running, please click the start drip button in standby mode！", "Tips", MessageBoxButtons.OK, false);
                FADM_Object.Communal._b_isDripping = false;
                return;
            }

            if (FADM_Object.Communal._lis_dripSuccessCup.Count > 0)
            {
                string s_cup = null;
                for (int i = 0; i < FADM_Object.Communal._lis_dripSuccessCup.Count; i++)
                {
                    s_cup += FADM_Object.Communal._lis_dripSuccessCup[i].ToString() + ",";
                }
                //FADM_Form.CustomMessageBox.Show("当前滴液完成杯号" + s_cup, "温馨提示", MessageBoxButtons.OK, false);

            }
            //获取现在机台状态，判断是否存在正在滴液流程的记录
            string s_sql = "SELECT * FROM drop_head WHERE Step = 1;";
            DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_drop_head.Rows.Count > 0)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("存在正在滴液流程，请完成所有滴液后再进行点击开始！", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("There is a dripping process in progress. Please complete all dripping before clicking start！", "Tips", MessageBoxButtons.OK, false);
                FADM_Object.Communal._b_isDripping = false;
                return;
            }
            else
            {
                if (dgv_BatchData.Rows.Count <= 0)
                {
                    FADM_Object.Communal._b_isDripping = false;
                    return;
                }

                s_sql = "SELECT CupNum FROM drop_head WHERE BatchName ='0' ;";
                DataTable dt_newcup = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_newcup.Rows.Count == 0)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("请先添加配方再滴液！", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Please add the formula first before dripping the liquid！", "Tips", MessageBoxButtons.OK, false);
                    FADM_Object.Communal._b_isDripping = false;
                    return;
                }

                s_sql = "SELECT CupNum FROM drop_head WHERE FormulaCode IS NULL ;";
                dt_newcup = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_newcup.Rows.Count > 0)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("当前批次存在空配方，请手动删除再滴液！", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("There is an empty formula in the current batch, please manually delete it before dripping！", "Tips", MessageBoxButtons.OK, false);
                    FADM_Object.Communal._b_isDripping = false;
                    return;
                }

                //判断是否过期；
                int i_state = FADM_Object.MyRegister.overdue();
                if (i_state == -1)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("试用次数已到！请联系供应商注册!", "信息", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Trial attempts have reached! Please contact the supplier for registration!", "Info", MessageBoxButtons.OK, false);
                    return;
                }
                else if (i_state == 1)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("请勿修改系统时间,请改回正常时间,再运行!", "信息", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Please do not modify the system time. Please change it back to normal time before running again!", "Info", MessageBoxButtons.OK, false);
                    return;
                }
                else if (i_state == 1001)
                {
                    _formMain.countDown(); //主界面重新查询下倒计时
                }

                if (dgv_BatchData.RowCount > 1)
                {
                    //
                    if (dgv_BatchData.SelectedRows.Count == 1)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        {
                            DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("当前只选择了一个配方，是否继续?(确认只滴一杯请点是，否则请点否)", "开始滴液", MessageBoxButtons.YesNo, true);

                            if (dialogResult == DialogResult.No)
                            {
                                FADM_Object.Communal._b_isDripping = false;
                                return;
                            }
                        }
                        else
                        {
                            DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Currently, only one formula has been selected. Do you want to continue? (Confirm that only one cup is dropped, please click yes; otherwise, please click no)", "Start dripping liquid", MessageBoxButtons.YesNo, true);

                            if (dialogResult == DialogResult.No)
                            {
                                FADM_Object.Communal._b_isDripping = false;
                                return;
                            }
                        }
                    }
                }


                //获取之前批次号
                s_sql = "SELECT BatchName FROM enabled_set Where MyID = 1;";
                dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                string s_batchNum_last = Convert.ToString(dt_drop_head.Rows[0][dt_drop_head.Columns[0]]);

                //计算当前批次号
                string s_batchNum = null;
                if (s_batchNum_last == "0")
                {
                    //初始状态
                    int no = 1;
                    s_batchNum = DateTime.Now.ToString("yyyyMMdd") + no.ToString("d4");
                }
                else
                {
                    string s_date = s_batchNum_last.Substring(0, 8);
                    string s_no = s_batchNum_last.Substring(8, 4);

                    if (s_date == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        s_batchNum = s_date + (Convert.ToInt32(s_no) + 1).ToString("d4");
                    }
                    else
                    {
                        int i_no = 1;
                        s_batchNum = DateTime.Now.ToString("yyyyMMdd") + i_no.ToString("d4");
                    }
                }

                //修改当前批次号
                s_sql = "UPDATE enabled_set SET BatchName = '" + s_batchNum + "'" +
                            " WHERE MyID = 1;";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);


                if (dgv_BatchData.SelectedRows.Count > 0)
                {
                    //选择某几杯
                    foreach (DataGridViewRow dr in dgv_BatchData.SelectedRows)
                    {

                        int i_cup = Convert.ToInt16(dgv_BatchData[0, dr.Index].Value);
                        string s_code = Convert.ToString(dgv_BatchData[1, dr.Index].Value);
                        string s_ver = Convert.ToString(dgv_BatchData[2, dr.Index].Value);

                        //写入批次号

                        //写入批次表头批次号
                        s_sql = "UPDATE drop_head SET BatchName = '" + s_batchNum + "'," +
                                    " State = '已滴定配方',Step=1 WHERE CupNum = '" + i_cup + "' and BatchName = '0';";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        //写入批次表详细内容批次号
                        s_sql = "UPDATE drop_details SET BatchName = '" + s_batchNum + "'" +
                                    " WHERE CupNum = '" + i_cup + "' and BatchName = '0';";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        //写入配方浏览表
                        s_sql = "UPDATE formula_head SET State = '已滴定配方'" +
                                   " WHERE FormulaCode = '" + s_code + "' AND" +
                                   " VersionNum = " + s_ver + " ;";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        //写入批次表详细内容批次号
                        s_sql = "UPDATE dye_details SET BatchName = '" + s_batchNum + "'" +
                                    " WHERE CupNum = '" + i_cup + "' and BatchName = '0';";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }

                }

                else
                {
                    //选择所有杯

                    //写入批次号

                    //修改批次表头批次号
                    s_sql = "UPDATE drop_head SET BatchName = '" + s_batchNum + "', State = '已滴定配方',Step = 1 where  BatchName = '0';";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    //修改批次详细资料表批次号
                    s_sql = "UPDATE drop_details SET BatchName = '" + s_batchNum + "' where  BatchName = '0';";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    s_sql = "UPDATE dye_details SET BatchName = '" + s_batchNum + "' where  BatchName = '0';";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    foreach (DataGridViewRow dr in dgv_BatchData.Rows)
                    {

                        int P_int_cup = Convert.ToInt16(dgv_BatchData[0, dr.Index].Value);
                        string s_code = Convert.ToString(dgv_BatchData[1, dr.Index].Value);
                        string s_ver = Convert.ToString(dgv_BatchData[2, dr.Index].Value);

                        //写入配方浏览表
                        s_sql = "UPDATE formula_head SET State = '已滴定配方'" +
                                   " WHERE FormulaCode = '" + s_code + "' AND" +
                                   " VersionNum = " + s_ver + " ;";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    }
                }

                BatchHeadShow("");

                Thread P_thd_drop = new Thread(drop_liquid);
                P_thd_drop.IsBackground = true;
                P_thd_drop.Start(s_batchNum);

                FADM_Object.Communal._b_isDripping = false;

                ////跳转主界面
                _formMain.BtnMain_Click(null, null);
            }
        }

        public static bool P_bl_update = false;

        //滴液线程
        private void drop_liquid(object oBatchNum)
        {
            //滴液
            new FADM_Auto.Drip().DripLiquid(oBatchNum);


        }


        static Thread thread = null;
        //停止滴液按钮点击事件
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            btn_Stop.Enabled = false;

            DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(
                  "SELECT * FROM drop_head WHERE BatchName IN (SELECT BatchName FROM enabled_set WHERE MyID = 1);");
            if (0 < dt_drop_head.Rows.Count)
            {
                if (FADM_Object.Communal.ReadMachineStatus() == 7)
                {
                    //正在滴液中
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("确定要停止滴液吗?", "停止滴液", MessageBoxButtons.YesNo, true);

                        if (dialogResult == DialogResult.Yes)
                        {
                            if (thread == null)
                            {
                                thread = new Thread(DropStop);
                                thread.Start();
                            }
                            else
                            {
                                btn_Stop.Enabled = true;
                            }
                        }
                        else
                        {
                            btn_Stop.Enabled = true;
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Are you sure you want to clear the current batch data?", "Stop dripping liquid", MessageBoxButtons.YesNo, true);

                        if (dialogResult == DialogResult.Yes)
                        {
                            if (thread == null)
                            {
                                thread = new Thread(DropStop);
                                thread.Start();
                            }
                            else
                            {
                                btn_Stop.Enabled = true;
                            }
                        }
                        else
                        {
                            btn_Stop.Enabled = true;
                        }
                    }
                }
                else if (FADM_Object.Communal.ReadMachineStatus() == 0 || FADM_Object.Communal.ReadMachineStatus() == 8)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        //用于异常退出,清除数据用
                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("确定要停止滴液吗?", "停止滴液", MessageBoxButtons.YesNo, true);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DataTable dataTable1 = FADM_Object.Communal._fadmSqlserver.GetData(
                            "SELECT BatchName FROM enabled_set WHERE MyID = 1;");
                            FADM_Auto.Reset.MoveData(dataTable1.Rows[0]["BatchName"].ToString());
                            P_bl_update = true;
                        }
                    }
                    else
                    {
                        //用于异常退出,清除数据用
                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Are you sure you want to clear the current batch data?", "Stop dripping liquid", MessageBoxButtons.YesNo, true);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DataTable dataTable1 = FADM_Object.Communal._fadmSqlserver.GetData(
                            "SELECT BatchName FROM enabled_set WHERE MyID = 1;");
                            FADM_Auto.Reset.MoveData(dataTable1.Rows[0]["BatchName"].ToString());
                            P_bl_update = true;
                        }
                    }

                    btn_Stop.Enabled = true;

                }
                else
                {
                    btn_Stop.Enabled = true;
                }

            }
            else
            {
                btn_Stop.Enabled = true;
            }



        }


        bool bstopf = false;
        private void DropStop()
        {


            while (true)
            {
                Thread.Sleep(1);
                if (null == FADM_Object.Communal.ReadDyeThread() && FADM_Object.Communal.ReadDripWait() == false)
                {
                    // FADM_Object.Communal._b_stop = true;
                    FADM_Auto.Drip._b_dripStop = true;
                    break;
                }


            }
            bstopf = true;
            thread = null;
            //List<int> lUse = new List<int>();
            //DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(
            //   "SELECT * FROM drop_head WHERE BatchName IN (SELECT BatchName FROM enabled_set WHERE MyID = 1);");
            //foreach (DataRow dataRow in dt_drop_head.Rows)
            //{
            //    int iCupNo = Convert.ToInt16(dataRow["CupNum"]);
            //    lUse.Add(iCupNo);

            //}


            ////等待洗杯完成
            //FADM_Object.Communal._lis_dripFailCupFinish.Clear();
            //if (lUse.Count > 0)
            //{
            //    FADM_Object.Communal._lis_dripFailCup.AddRange(lUse);

            //    while (true)
            //    {
            //        if (0 == lUse.Count)
            //            break;

            //        for (int i = lUse.Count - 1; i >= 0; i--)
            //        {
            //            if (FADM_Object.Communal._lis_dripFailCupFinish.Contains(lUse[i]))
            //            {
            //                FADM_Object.Communal._lis_dripFailCupFinish.Remove(lUse[i]);
            //                lUse.Remove(lUse[i]);

            //            }
            //        }

            //        Thread.Sleep(1);
            //    }
            //}

            //FADM_Object.Communal._fadmSqlserver.ReviseData(
            //    "DELETE FROM drop_head WHERE BatchName IN (SELECT BatchName FROM enabled_set WHERE MyID = 1);");

            //FADM_Object.Communal._fadmSqlserver.ReviseData(
            //    "DELETE FROM drop_details WHERE BatchName IN (SELECT BatchName FROM enabled_set WHERE MyID = 1);");

            //FADM_Object.Communal._fadmSqlserver.ReviseData(
            //   "DELETE FROM dye_details WHERE BatchName IN (SELECT BatchName FROM enabled_set WHERE MyID = 1);");


        }


        //定时器
        private void tmr_Tick(object sender, EventArgs e)
        {

            if (P_bl_update)
            {

                //杯号排序
                this.cup_sort();
                if (P_int_delect_index >= 0)
                {
                    if (dgv_BatchData.Rows.Count > 0)
                    {
                        if (dgv_BatchData.Rows.Count - 1 >= P_int_delect_index)
                        {
                            dgv_BatchData.CurrentCell = dgv_BatchData.Rows[P_int_delect_index].Cells[0];
                            dgv_BatchData.CurrentCell.Selected = true;

                        }
                        else
                        {
                            dgv_BatchData.CurrentCell = dgv_BatchData.Rows[dgv_BatchData.Rows.Count - 1].Cells[0];
                            dgv_BatchData.CurrentCell.Selected = true;

                        }
                    }
                    else
                    {
                        //dgv_FormulaBrowse.Focus();
                        //if (dgv_FormulaBrowse.Rows.Count > 0)
                        //{
                        //    dgv_FormulaBrowse.CurrentCell = dgv_FormulaBrowse.Rows[0].Cells[0];
                        //    dgv_FormulaBrowse.CurrentCell.Selected = true;
                        //}
                    }
                    dgv_BatchData_CurrentCellChanged(null, null);
                    P_int_delect_index = -1;
                }



                //FormulaBrowseHeadShow("");
                P_bl_update = false;
            }

            if (_b_updateWait)
            {
                DataTable dt_wait = FADM_Object.Communal._fadmSqlserver.GetData(
                    "SELECT * FROM wait_list;");
                int i_num = dt_wait.Rows.Count;
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    Btn_WaitList.Text = "等待列表(" + i_num + ")";
                }
                else
                {

                    Btn_WaitList.Text = "WaitList(" + i_num + ")";
                }
                BatchHeadShow("");
                _b_updateWait = false;
            }

            if (bstopf)
            {
                btn_Stop.Enabled = true;
                bstopf = false;
            }
        }

        private void btn_operator_Click(object sender, EventArgs e)
        {
            //DropSystem.PerForm.F_Operator form_Operator = new DropSystem.PerForm.F_Operator(btn_operator);
            //form_Operator.Show();
            //btn_operator.Visible = false;
        }

        private void dgv_BatchData_RowsAdded()
        {
            try
            {
                if (FADM_Object.Communal._b_isDripping)
                {
                    return;
                }
                FADM_Object.Communal._b_isDripping = true;
                if (dgv_BatchData.CurrentRow != null &&
                    dgv_BatchData.CurrentRow.DefaultCellStyle.BackColor != Color.DarkGray &&
                    dgv_BatchData.CurrentRow.DefaultCellStyle.BackColor != Color.Red &&
                    dgv_BatchData.CurrentRow.DefaultCellStyle.BackColor != Color.Lime)
                {
                    int i_cup = Convert.ToInt16(dgv_BatchData.CurrentRow.Cells[0].Value);
                    //当插入是滴液位置时，生效
                    if (SmartDyeing.FADM_Object.Communal._lis_dripCupNum.Contains(i_cup))
                    {


                        string s_sql = "SELECT * FROM drop_head  where BatchName = '0' and (Stage = '滴液' or Stage is null)  order by CupNum  ;";
                        DataTable dt_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        s_sql = "SELECT * FROM drop_head  where BatchName != '0' and Stage = '滴液'   order by CupNum  ;";
                        DataTable dt_head_Drip = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        if (dt_head_Drip.Rows.Count != 0)
                        {
                            FADM_Object.Communal._b_isDripping = false;
                            return;
                        }

                        //判断当前加入批次滴液配方数量小于滴液总杯位时,使用最后一个杯号和滴液最大杯号比较
                        if (Convert.ToInt16(dt_head.Rows[dt_head.Rows.Count - 1]["CupNum"].ToString()) < SmartDyeing.FADM_Object.Communal._lis_dripCupNum[SmartDyeing.FADM_Object.Communal._lis_dripCupNum.Count - 1])
                        {
                            //记录添加行的杯号
                            int i_n = 0;
                            for (int i = 0; i < FADM_Object.Communal._lis_dripCupNum.Count; i++)
                            {
                                if (FADM_Object.Communal._lis_dripCupNum[i] == i_cup)
                                {
                                    i_n = i; break;
                                }
                            }
                            for (int i = dt_head.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToInt16(dt_head.Rows[i]["CupNum"].ToString()) >= FADM_Object.Communal._lis_dripCupNum[i_n])
                                {
                                    int i_n1 = 0;
                                    for (int p = 0; p < FADM_Object.Communal._lis_dripCupNum.Count; p++)
                                    {
                                        if (FADM_Object.Communal._lis_dripCupNum[p] == Convert.ToInt16(dt_head.Rows[i]["CupNum"].ToString()))
                                        {
                                            i_n1 = p; break;
                                        }
                                    }

                                    string s_sql1 = "UPDATE drop_head SET CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n1 + 1] + " WHERE CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n1] + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql1);

                                    s_sql1 = "UPDATE drop_details SET CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n1 + 1] + " WHERE CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n1] + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql1);

                                    FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 0 where CupNum =  " + FADM_Object.Communal._lis_dripCupNum[i_n1]);
                                    FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 1 where CupNum =  " + FADM_Object.Communal._lis_dripCupNum[i_n1 + 1]);
                                }
                            }

                            //s_sql = "Update cup_details set IsUsing = 0 where CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n] + ";";
                            //FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            ////查找最大杯号的下一杯
                            //int n2 = 0;
                            //for (int p = 0; p < FADM_Object.Communal._lis_dripCupNum.Count; p++)
                            //{
                            //    if (FADM_Object.Communal._lis_dripCupNum[p] == Convert.ToInt16(dt_head.Rows[dt_head.Rows.Count - 1]["CupNum"].ToString()))
                            //    {
                            //        n2 = p; break;
                            //    }
                            //}

                            //s_sql = "Update cup_details set IsUsing = 1 where CupNum = " + FADM_Object.Communal._lis_dripCupNum[n2+1] + ";";
                            //FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            s_sql = "INSERT INTO drop_head (CupNum) VALUES(" + i_cup + ");";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            //记录添加行的杯号
                            int i_n = 0;
                            for (int i = 0; i < FADM_Object.Communal._lis_dripCupNum.Count; i++)
                            {
                                if (FADM_Object.Communal._lis_dripCupNum[i] == i_cup)
                                {
                                    i_n = i; break;
                                }
                            }
                            //把原来等待列表序号后延一位
                            FADM_Object.Communal._fadmSqlserver.ReviseData("UPDATE wait_list Set IndexNum = IndexNum+1 where Type = 2");
                            if (dt_head.Rows[dt_head.Rows.Count - 1]["FormulaCode"] != System.DBNull.Value)
                            {
                                //把最后一个配方移入等待列表
                                string s_sql_0 = "INSERT INTO wait_list ( FormulaCode, VersionNum, IndexNum, CupNum,Type) values('" + dt_head.Rows[dt_head.Rows.Count - 1]["FormulaCode"].ToString() + "','" + dt_head.Rows[dt_head.Rows.Count - 1]["VersionNum"].ToString() + "',1,0,2);";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);
                            }

                            //删除最后一个配方
                            //删除批次浏览表头资料
                            s_sql = "DELETE FROM drop_head WHERE CupNum = '" + dt_head.Rows[dt_head.Rows.Count - 1]["CupNum"].ToString() + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            //删除批次浏览表详细资料
                            s_sql = "DELETE FROM drop_details WHERE CupNum = '" + dt_head.Rows[dt_head.Rows.Count - 1]["CupNum"].ToString() + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            //如果是最后一杯插入
                            if (i_cup == FADM_Object.Communal._lis_dripCupNum[FADM_Object.Communal._lis_dripCupNum.Count - 1])
                            {
                                FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 0 where CupNum =  " + i_cup);
                            }

                            for (int i = dt_head.Rows.Count - 2; i >= 0; i--)
                            {
                                if (Convert.ToInt16(dt_head.Rows[i]["CupNum"].ToString()) >= FADM_Object.Communal._lis_dripCupNum[i_n])
                                {
                                    int i_n1 = 0;
                                    for (int p = 0; p < FADM_Object.Communal._lis_dripCupNum.Count; p++)
                                    {
                                        if (FADM_Object.Communal._lis_dripCupNum[p] == Convert.ToInt16(dt_head.Rows[i]["CupNum"].ToString()))
                                        {
                                            i_n1 = p; break;
                                        }
                                    }

                                    string s_sql1 = "UPDATE drop_head SET CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n1 + 1] + " WHERE CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n1] + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql1);

                                    s_sql1 = "UPDATE drop_details SET CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n1 + 1] + " WHERE CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n1] + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql1);

                                    FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 0 where CupNum =  " + FADM_Object.Communal._lis_dripCupNum[i_n1]);
                                    FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 1 where CupNum =  " + FADM_Object.Communal._lis_dripCupNum[i_n1 + 1]);
                                }
                            }


                            ////把所有滴液杯子置为正在使用
                            //FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 1 where Type = 2 ");
                            //把插入杯子置为空闲
                            //FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 0 where CupNum =  " + i_cup);

                            s_sql = "INSERT INTO drop_head (CupNum) VALUES(" + i_cup + ");";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        _b_updateWait = true;
                        BatchHeadShow("");
                    }
                    //直接退出
                    else
                    {

                    }


                }
                FADM_Object.Communal._b_isDripping = false;

            }
            catch (Exception ex)
            {
                FADM_Object.Communal._b_isDripping = false;
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_BatchData_RowsAdded", MessageBoxButtons.OK, true);
            }
        }

        

        /// <summary>
        /// 打板助剂信息显示
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private void AddAssistantShow()
        {
            try
            {
                //dgv_Dye.Rows.Clear();
                //dgv_Handle1.Rows.Clear();
                //dgv_Handle2.Rows.Clear();
                //dgv_Handle3.Rows.Clear();
                //dgv_Handle4.Rows.Clear();
                //dgv_Handle5.Rows.Clear();
                //dgv_Dyeing.Rows.Clear();

                //if (txt_FormulaCode.Text == "" || txt_VersionNum.Text == "" || txt_DyeingCode.Text == "")
                //{
                //    return;
                //}
                //else
                {
                    string s_sql = "select * from dyeing_code where DyeingCode ='" + txt_DyeingCode.Text + "' order by IndexNum;";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    int i_nNum = 0;
                    //先把助剂代码写入对应列表
                    foreach (DataRow dr in dt_data.Rows)
                    {
                        FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)_lis_dg[i_nNum];
                        //显示染色工艺
                        //if (dr[1].ToString() == "1")
                        {
                            string s_sql1;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)

                                s_sql1 = "SELECT TechnologyName FROM dyeing_process where Code = '" + dr[3].ToString() + "' and TechnologyName in ('加A','加B','加C','加D','加E','加F','加G','加H','加I','加J','加K','加L','加M','加N')  group  by TechnologyName;";
                            else
                                s_sql1 = "SELECT TechnologyName FROM dyeing_process where Code = '" + dr[3].ToString() + "' and TechnologyName in ('Add A','Add B','Add C','Add D','Add E','Add F','Add G','Add H','Add I','Add J','Add K','Add L','Add M','Add N')  group  by TechnologyName;";

                            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);
                            if (i_nNum < _lis_hBR.Count)
                                _lis_handleBathRatio[i_nNum].Text = _lis_hBR[i_nNum];

                            for (int i = 0; i < dt_data1.Rows.Count; i++)
                            {
                                //查找对应数据
                                string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dt_data1.Rows[i][0].ToString() + "' and DyeingCode = '" + txt_DyeingCode.Text + "';";
                                DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                if (dt_data2.Rows.Count > 0)
                                {
                                    
                                    string s_realDropWeight = "0.00";
                                    if (dgv_BatchData.CurrentRow != null)
                                        if (dgv_BatchData.CurrentRow.Selected)
                                        {
                                            string s_sql3 = "SELECT Sum(RealDropWeight) FROM dye_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '"
                                                + dt_data1.Rows[i][0].ToString() + "' and CupNum = '" + dgv_BatchData.CurrentRow.Cells[0].Value.ToString() + "';";
                                            DataTable dt_data3 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql3);
                                            s_realDropWeight = dt_data3.Rows[0][0].ToString();
                                        }
                                    dgv_Dye.Rows.Add(dt_data1.Rows[i][0].ToString(),
                                             dt_data2.Rows[0]["AssistantCode"].ToString().Trim(),
                                             dt_data2.Rows[0]["AssistantName"].ToString().Trim(),
                                             dt_data2.Rows[0]["FormulaDosage"].ToString(),
                                             dt_data2.Rows[0]["UnitOfAccount"].ToString(),
                                             null,
                                             dt_data2.Rows[0]["SettingConcentration"].ToString(),
                                             dt_data2.Rows[0]["RealConcentration"].ToString(),
                                             dt_data2.Rows[0]["ObjectDropWeight"].ToString(),
                                             s_realDropWeight);

                                    //DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_Dye[4, i];
                                    //List<string> lis_bottleNum = new List<string>();
                                    //lis_bottleNum.Add(dt_data2.Rows[0]["BottleNum"].ToString());
                                    //dd.Value = null;
                                    //dd.DataSource = lis_bottleNum;
                                    //dd.Value = (dt_data2.Rows[0]["BottleNum"]).ToString();

                                    //显示瓶号
                                    s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                                " FROM bottle_details WHERE" +
                                                " AssistantCode = '" + dgv_Dye[1, i].Value.ToString() + "'" +
                                                " AND RealConcentration != 0 ORDER BY BottleNum ;";
                                    DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


                                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_Dye[5, i];
                                    List<string> lis_bottleNum = new List<string>();
                                    bool b_exist = false;
                                    foreach (DataRow mdr in dt_bottlenum.Rows)
                                    {
                                        string s_num = mdr[0].ToString();

                                        lis_bottleNum.Add(s_num);

                                        if ((dt_data2.Rows[0]["BottleNum"]).ToString() == s_num)
                                        {
                                            b_exist = true;
                                        }

                                    }


                                    dd.Value = null;
                                    dd.DataSource = lis_bottleNum;
                                    if (b_exist)
                                    {
                                        dd.Value = (dt_data2.Rows[0]["BottleNum"]).ToString();
                                    }
                                    else
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            FADM_Form.CustomMessageBox.Show((dt_data2.Rows[0]["BottleNum"]).ToString() +
                                                       "号母液瓶不存在", "温馨提示", MessageBoxButtons.OK, false);
                                        else
                                            FADM_Form.CustomMessageBox.Show((dt_data2.Rows[0]["BottleNum"]).ToString() +
                                                       " Mother liquor bottle number does not exist", "Tips", MessageBoxButtons.OK, false);
                                    }


                                    //显示是否手动选瓶
                                    DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_Dye[10, i];
                                    dc.Value = dt_data2.Rows[0]["BottleSelection"].ToString() == "False" || dt_data2.Rows[0]["BottleSelection"].ToString() == "0" ? 0 : 1;
                                }
                                else
                                {
                                    dgv_Dye.Rows.Add(dt_data1.Rows[i][0].ToString());
                                }
                            }

                        }

                        //判断是否为空,空就把浴比复制填写
                        if (_lis_handleBathRatio[i_nNum].Text == "")
                            _lis_handleBathRatio[i_nNum].Text = txt_BathRatio.Text;
                        i_nNum++;
                    }


                    ////没有历史记录，手动添加
                    //if (_dt_data.Rows.Count < 1)
                    //{
                    //    string s_sql1 = "SELECT TechnologyName FROM dyeing_process where DyeingCode = '" + txt_DyeingCode.Text + "' and TechnologyName in ('加A','加B','加C','加D','加E','加F','加G','加H','加I','加J','加K','加L','加M','加N')  group  by TechnologyName;";
                    //    DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);

                    //    //显示详细信息
                    //    for (int i = 0; i < dt_data1.Rows.Count; i++)
                    //    {
                    //        dgv_AddAssistant.Rows.Add(dt_data1.Rows[i]["TechnologyName"].ToString());
                    //    }
                    //}
                    ////存在历史数据，直接使用
                    //else
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "AddAssistantShow", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_Dyeing_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }

        private void dgv_AddAssistant_SelectionChanged(object sender, EventArgs e)
        {
            //if (dgv_AddAssistant.Rows.Count > 1)
            //{
            //    int i_col = dgv_AddAssistant.CurrentCell.ColumnIndex;

            //    int i_row = dgv_AddAssistant.CurrentCell.RowIndex;

            //    if (i_col == 2)
            //    {

            //        if (i_row == dgv_AddAssistant.Rows.Count - 2)
            //        {
            //            btn_Save.Focus();

            //        }

            //    }
            //}
        }

        private void txt_DyeingCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            _lis_dg.Clear();
            _lis_handleBathRatio.Clear();
            //构造工艺显示
            if (txt_DyeingCode.Text != "")
            {
                Dictionary<string, int>.KeyCollection keyColl = _dic_dyeCode.Keys;
                foreach (string s in keyColl)
                {
                    if (s == txt_DyeingCode.Text)
                    {
                        if (_dic_dyeCode[s] == 1)
                        {
                            _s_stage = "前处理";
                            //lab_HandleBathRatio.Visible = false;
                            //txt_HandleBathRatio.Visible = false;
                        }
                        else
                        {
                            _s_stage = "后处理";
                            //lab_HandleBathRatio.Visible = true;
                            //txt_HandleBathRatio.Visible = true;
                            DyeingHeadShow();
                            AddAssistantShow();
                        }
                        break;
                    }
                }

            }
            else
            {
                _s_stage = "滴液";
                //lab_HandleBathRatio.Visible = false;
                //txt_HandleBathRatio.Visible = false;
            }
        }

        /// <summary>
        /// 显示批次资料
        /// </summary>
        private void DetailsShow(string _type, string _code)
        {
        }

        private void dgv_Dyeing_CurrentCellChanged(object sender, EventArgs e)
        {
        }


        private void ReSet_txt_FormulaCode()
        {
            List<string> lis_data = new List<string>();

            //跟据设定浓度重新排序
            string s_sql = "SELECT  FormulaCode FROM formula_head group by FormulaCode;";

            DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            foreach (DataRow row in dt_formulahead.Rows)
            {
                lis_data.Add(row[0].ToString());
            }

            txt_FormulaCode.AutoCompleteCustomSource.Clear();
            txt_FormulaCode.AutoCompleteCustomSource.AddRange(lis_data.ToArray());
            txt_FormulaCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            txt_FormulaCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        //private void ReSet_txt_FormulaGroup()
        //{
        //    List<string> lis_data = new List<string>();

        //    //跟据设定浓度重新排序
        //    string s_sql = "SELECT  group_Name FROM formula_group where node = 0;";

        //    DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
        //    foreach (DataRow i_row in dt_formulahead.Rows)
        //    {
        //        lis_data.Add(i_row[0].ToString());
        //    }

        //    txt_FormulaGroup.AutoCompleteCustomSource.Clear();
        //    txt_FormulaGroup.AutoCompleteCustomSource.AddRange(lis_data.ToArray());
        //    txt_FormulaGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        //    txt_FormulaGroup.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //}

        private void Formula_Load(object sender, EventArgs e)
        {
            ReSet_txt_FormulaCode();
            //ReSet_txt_FormulaGroup();

            txt_AnhydrationWR.Text = Lib_Card.Configure.Parameter.Other_Default_AnhydrationWR.ToString();
            txt_Non_AnhydrationWR.Text = Lib_Card.Configure.Parameter.Other_Default_Non_AnhydrationWR.ToString();
            chk_AddWaterChoose.Checked = true;
            txt_CupNum.Text = "0";
        }


        private void txt_Non_AnhydrationWR_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        private void txt_AnhydrationWR_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        private void txt_CupNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberTextbox_KeyPress(e);
        }

        private void txt_HandleBathRatio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }


        private void txt_DyeingCode_Leave(object sender, EventArgs e)
        {
            //if (txt_DyeingCode.Text == "")
            {
                panel1.Controls.Clear();
                _lis_dg.Clear();
                _lis_handleBathRatio.Clear();

                //构造工艺显示
                if (txt_DyeingCode.Text != "")
                {
                    Dictionary<string, int>.KeyCollection keyColl = _dic_dyeCode.Keys;
                    foreach (string s in keyColl)
                    {
                        if (s == txt_DyeingCode.Text)
                        {
                            if (_dic_dyeCode[s] == 1)
                            {
                                _s_stage = "前处理";
                                //lab_HandleBathRatio.Visible = false;
                                //txt_HandleBathRatio.Visible = false;
                            }
                            else
                            {
                                _s_stage = "后处理";
                                //lab_HandleBathRatio.Visible = true;
                                //txt_HandleBathRatio.Visible = true;
                                DyeingHeadShow();
                                AddAssistantShow();
                            }
                            break;
                        }
                    }

                }
                else
                {
                    _s_stage = "滴液";
                    //lab_HandleBathRatio.Visible = false;
                    //txt_HandleBathRatio.Visible = false;


                }

            }
        }

        //染色后处理DataGridView
        List<Control> _lis_dg = new List<Control>();
        //后处理浴比控件
        List<Control> _lis_handleBathRatio = new List<Control>();
        //染色后处理浴比值
        List<string> _lis_hBR = new List<string>();

        /// <summary>
        /// 固染色工艺步骤
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private void DyeingHeadShow()
        {
            try
            {
                string s_sql = "SELECT * FROM dyeing_code where DyeingCode = '" + txt_DyeingCode.Text + "' order by IndexNum;";
                DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                int i_nNum = 1;
                int i_nHeight = 5;
                foreach (DataRow dr in dt_data.Rows)
                {
                    FADM_Control.DyeAndHandleFormulas s = new DyeAndHandleFormulas();
                    s.Location = new Point(5, i_nHeight);

                    //计算需要的行数
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)

                        s_sql = "SELECT TechnologyName FROM dyeing_process where Code = '" + dr[3].ToString() + "' and TechnologyName in ('加A','加B','加C','加D','加E','加F','加G','加H','加I','加J','加K','加L','加M','加N')  group  by TechnologyName;";
                    else
                        s_sql = "SELECT TechnologyName FROM dyeing_process where Code = '" + dr[3].ToString() + "' and TechnologyName in ('Add A','Add B','Add C','Add D','Add E','Add F','Add G','Add H','Add I','Add J','Add K','Add L','Add M','Add N')  group  by TechnologyName;";

                    DataTable dt_dataTemp = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    int i_nAddNum = dt_dataTemp.Rows.Count;
                    

                    
                    s.Height = 60 + 30 * i_nAddNum + 5;
                    s.grp_Dye.Height = 60 + 30 * i_nAddNum + 2;
                    s.dgv_Dye.Height = 28 * i_nAddNum;
                    i_nHeight += s.Height + 10;
                    this.panel1.Controls.Add(s);
                    s.dgv_Dye.Name = i_nNum.ToString();
                    string s_temp = dr["Type"].ToString();
                    s.grp_Dye.Text = (s_temp=="1"?"染色":"后处理")+"-" + dr["Code"].ToString();
                    s.dgv_Dye.SelectionChanged += dgv_Dye_SelectionChanged;
                    s.dgv_Dye.EditingControlShowing += dgv_Dye_EditingControlShowing;
                    s.dgv_Dye.RowLeave += dgv_Dye_RowLeave;
                    _lis_dg.Add(s.dgv_Dye);
                    s.txt_HandleBathRatio.Name = "txt_HBR_"+i_nNum.ToString();
                    s.txt_HandleBathRatio.KeyPress += txt_HandleBathRatio_KeyPress;
                    s.txt_HandleBathRatio.KeyDown += TextBox_HandelBRKeyDown;
                    s.txt_HandleBathRatio.Leave += txt_HandelBathRatio_Leave;
                    _lis_handleBathRatio.Add(s.txt_HandleBathRatio);
                    i_nNum++;
                }
                if (dt_data.Rows.Count == 0)
                {
                    string s_dyeingCode = txt_DyeingCode.Text;
                    txt_DyeingCode.Text = null;
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        throw new Exception(s_dyeingCode + "工艺为空，请核对后再选择。");
                    else
                        throw new Exception(s_dyeingCode + " Process is empty, please check before selecting");
                }
                else
                {
                    foreach (DataRow dr in dt_data.Rows)
                    {
                        //获取批次资料表头
                        s_sql = "SELECT StepNum,TechnologyName,ProportionOrTime  FROM dyeing_process WHERE" +
                                       " Code = '" + dr[3].ToString() + "' Order By StepNum ; ";

                        DataTable dt_formula = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                        if (dt_formula.Rows.Count == 0)
                        {
                            string s_dyeingCode = txt_DyeingCode.Text;
                            txt_DyeingCode.Text = null;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                throw new Exception(dr[3] + "工艺为空，请核对后再选择。");
                            else
                                throw new Exception(dr[3] + " Process is empty, please check before selecting");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "DyeingHeadShow", MessageBoxButtons.OK, true);
            }
        }

        private void Btn_WaitList_Click(object sender, EventArgs e)
        {
            //Bottle Bottle = (Bottle)sender;
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "排队列表");
            else
                ptr = FindWindow(null, "QueueList");
            if (ptr == IntPtr.Zero)
            {
                new WaitingListInfo().Show();
            }
        }

        private void txt_DyeingCode_TextUpdate(object sender, EventArgs e)
        {
            string s_dyeingCode = txt_DyeingCode.Text;
            List<string> lis_data = new List<string>();
            lis_data.AddRange(_lis_dyeingCode.ToArray());

            List<string> lis_newList = new List<string>();

            txt_DyeingCode.Items.Clear();

            foreach (var item in lis_data)
            {
                if (item.Contains(s_dyeingCode))
                {
                    lis_newList.Add(item);
                }
            }

            if (lis_newList.Count >= 1)
            {
                txt_DyeingCode.Items.AddRange(lis_newList.ToArray());
            }
            else
            {
                txt_DyeingCode.Items.Add(s_dyeingCode);
            }

            txt_DyeingCode.SelectionStart = txt_DyeingCode.Text.Length;
            Cursor = Cursors.Default;
            txt_DyeingCode.DroppedDown = true;
        }

        private void tsm_Delete_Click(object sender, EventArgs e)
        {
            //待机状态下才能删除
            if (0 == FADM_Object.Communal.ReadMachineStatus())
            {
                try
                {
                    if (FADM_Object.Communal._b_isDripping)
                    {
                        return;
                    }
                    FADM_Object.Communal._b_isDripping = true;

                    //P_int_delect_index = Convert.ToInt16(dgv_BatchData.CurrentRow.Index);
                    string s_sql = null;
                    foreach (DataGridViewRow dr in dgv_BatchData.SelectedRows)
                    {
                        string s_cup = dr.Cells[0].Value.ToString();

                        //当前删除是滴液配方时直接删除
                        if (SmartDyeing.FADM_Object.Communal._lis_dripCupNum.Contains(Convert.ToInt16(s_cup)))
                        {

                            //删除批次浏览表头资料
                            s_sql = "DELETE FROM drop_head WHERE CupNum = '" + s_cup + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            //删除批次浏览表详细资料
                            s_sql = "DELETE FROM drop_details WHERE CupNum = '" + s_cup + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            ////删除批次浏览表详细资料
                            //s_sql = "DELETE FROM dye_details WHERE CupNum = '" + s_temp + "';";
                            //FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            ////删除批次浏览表详细资料
                            //s_sql = "DELETE FROM dye_details WHERE CupNum = '" + s_temp + "';";
                            //FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            //更新杯号使用情况
                            s_sql = "Update cup_details set IsUsing = 0 where CupNum = '" + s_cup + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        //后处理杯号就发一个停止信号
                        else
                        {
                            FADM_Object.Communal._lis_dripStopCup.Add(Convert.ToInt16(s_cup));
                        }
                    }

                    _b_updateWait = true;

                    P_bl_update = true;

                    if (SmartDyeing.FADM_Object.Communal._lis_dripCupNum.Count > 0)
                    {

                        //查找滴液配方
                        s_sql = "SELECT * FROM drop_head  where BatchName = '0' and (Stage = '滴液' or Stage is null)  order by CupNum  ;";
                        DataTable dt_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        s_sql = "SELECT * FROM drop_head  where BatchName != '0' and Stage = '滴液'   order by CupNum  ;";
                        DataTable dt_head_Drip = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        if (dt_head_Drip.Rows.Count != 0)
                        {
                            FADM_Object.Communal._b_isDripping = false;
                            return;
                        }

                        if (dt_head.Rows.Count > 0)
                        {
                            //先把所有杯状态置为没使用
                            FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 0 where Type = 2");
                        }

                        int i_n = 0;
                        foreach (DataRow dr1 in dt_head.Rows)
                        {
                            int i_cup = Convert.ToInt16(dr1["CupNum"].ToString());

                            string s_sql1 = "UPDATE drop_head SET CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n] + " WHERE CupNum = " + i_cup + ";";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql1);

                            s_sql1 = "UPDATE drop_details SET CupNum = " + FADM_Object.Communal._lis_dripCupNum[i_n] + " WHERE CupNum = " + i_cup + ";";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql1);
                            i_n++;
                        }

                        //使用等待列表的后补
                        int ndif = FADM_Object.Communal._lis_dripCupNum.Count - i_n;

                        s_sql = "SELECT FormulaCode,VersionNum,CupNum,IndexNum  FROM wait_list  where Type = 2 order by IndexNum;";
                        DataTable P_dt_WaitList = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


                        foreach (DataRow Row in P_dt_WaitList.Rows)
                        {

                            //加入批次
                            AddDropList a = new AddDropList(Row["FormulaCode"].ToString(), Row["VersionNum"].ToString(), FADM_Object.Communal._lis_dripCupNum[i_n].ToString(), 2);
                            //删除等待列表记录
                            FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 2 and IndexNum = " + Row["IndexNum"].ToString());

                            i_n++;
                            if (i_n == FADM_Object.Communal._lis_dripCupNum.Count)
                            {
                                break;
                            }
                        }

                        if (i_n > 0)
                        {
                            //把对应杯位置为使用
                            FADM_Object.Communal._fadmSqlserver.ReviseData("Update cup_details set IsUsing = 1 where Type = 2 and CupNum <=" + FADM_Object.Communal._lis_dripCupNum[i_n - 1]);
                        }
                        //
                    }
                    FADM_Object.Communal._b_isDripping = false;


                    _b_updateWait = true;

                    P_bl_update = true;
                }
                catch
                {
                    FADM_Object.Communal._b_isDripping = false;
                }
            }
            else
            {
                //提示待机状态下才能删除
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("待机状态下才能删除", "温馨提示", MessageBoxButtons.OK, false))
                    {
                        return;
                    }
                }
                else
                {
                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("Can only be deleted in standby mode", "Tips", MessageBoxButtons.OK, false))
                    {
                        return;
                    }
                }
            }
        }

        private void dgv_BatchData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (FADM_Object.Communal._s_operator != "管理用户" && FADM_Object.Communal._s_operator != "工程师")
            {
                return;
            }
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (dgv_BatchData.Rows[e.RowIndex].Selected == false)
            {
                dgv_BatchData.ClearSelection();
                dgv_BatchData.Rows[e.RowIndex].Selected = true;

            }
            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
        }

        private void txt_FormulaGroup_TextUpdate(object sender, EventArgs e)
        {
            string s_fg = txt_FormulaGroup.Text;
            List<string> lis_data = new List<string>();
            lis_data.AddRange(_lis_fg.ToArray());

            List<string> lis_newList = new List<string>();

            txt_FormulaGroup.Items.Clear();

            foreach (var item in lis_data)
            {
                if (item.Contains(s_fg))
                {
                    lis_newList.Add(item);
                }
            }

            if (lis_newList.Count >= 1)
            {
                txt_FormulaGroup.Items.AddRange(lis_newList.ToArray());
            }
            else
            {
                txt_FormulaGroup.Items.Add(s_fg);
            }

            txt_FormulaGroup.SelectionStart = txt_FormulaGroup.Text.Length;
            Cursor = Cursors.Default;
            txt_FormulaGroup.DroppedDown = true;
        }

        private void txt_FormulaGroup_Leave(object sender, EventArgs e)
        {
            //InsertFormula();
            //如果布重和浴比都为空，代表原来空白数据页面
            if ((txt_ClothWeight.Text == null || txt_ClothWeight.Text == "") && (txt_BathRatio.Text == null || txt_BathRatio.Text == ""))
            {
                //txt_ClothWeight.Enabled = true;
                //txt_ClothWeight.Focus();
                //return;
            }
            else
            {
                //dgv_FormulaData.Enabled = true;
                //dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                //dgv_FormulaData.Focus();
                return;
            }
        }

        private void txt_FormulaGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertFormula();
        }

        private void btn_NotDrip_Click(object sender, EventArgs e)
        {
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "尚未滴液列表");
            else
                ptr = FindWindow(null, "NoDripList");
            if (ptr == IntPtr.Zero)
            {
                new NotDripList().Show();
            }
        }

        private void txt_CupNum_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_CupNum.Text != null && txt_CupNum.Text != "")
                {
                    if (Convert.ToInt16(txt_CupNum.Text) > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("杯号输入超过最大杯号，请检查", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Cup number input exceeds the maximum cup number, please check", "Tips", MessageBoxButtons.OK, false);
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "txt_CupNum_Leave", MessageBoxButtons.OK, true);
            }
        }

        private void rdo_Browse_All_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Browse_All.Checked)
            {
                txt_Browse_Operator.Enabled = false;
                txt_Browse_Code.Enabled = false;
                dt_Browse_Start.Enabled = false;
                dt_Browse_End.Enabled = false;
            }
        }

        private void rdo_Browse_NoDrop_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Browse_NoDrop.Checked)
            {
                txt_Browse_Operator.Enabled = false;
                txt_Browse_Code.Enabled = false;
                dt_Browse_Start.Enabled = false;
                dt_Browse_End.Enabled = false;
            }
        }

        private void rdo_Browse_condition_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Browse_condition.Checked)
            {
                txt_Browse_Operator.Enabled = true;
                txt_Browse_Code.Enabled = true;
                dt_Browse_Start.Enabled = true;
                dt_Browse_End.Enabled = true;
            }
        }

        private void btn_Browse_Select_Click(object sender, EventArgs e)
        {
            dgv_BatchData.ClearSelection();
            dgv_BatchData.CurrentCell = null;
            FormulaBrowseHeadShow("");
        }

        private void btn_Browse_Delete_Click(object sender, EventArgs e)
        {
            if (dgv_FormulaBrowse.CurrentRow != null)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("确定删除吗？", "删除配方", MessageBoxButtons.YesNo, true);
                    //while (true)
                    //{
                    //    if (myAlarm._i_alarm_Choose != 0)
                    //    {
                    //        break;
                    //    }
                    //    Thread.Sleep(1);
                    //}
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            string s_formulaCode = dgv_FormulaBrowse.CurrentRow.Cells[0].Value.ToString();
                            string s_versionNum = dgv_FormulaBrowse.CurrentRow.Cells[1].Value.ToString();

                            string s_sql = "DELETE FROM formula_head WHERE" +
                                               " FormulaCode = '" + s_formulaCode + "' AND" +
                                               " VersionNum = '" + s_versionNum + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            s_sql = "DELETE FROM formula_details WHERE" +
                                        " FormulaCode = '" + s_formulaCode + "' AND" +
                                        " VersionNum = '" + s_versionNum + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            s_sql = "delete from  formula_handle_details where FormulaCode='" + s_formulaCode + "' and VersionNum='" + s_versionNum + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            FormulaBrowseHeadShow("");
                        }
                        catch
                        {

                        }

                    }
                }
                else
                {
                    DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Are you sure to delete it？", "Delete Recipe", MessageBoxButtons.YesNo, true);
                    //while (true)
                    //{
                    //    if (myAlarm._i_alarm_Choose != 0)
                    //    {
                    //        break;
                    //    }
                    //    Thread.Sleep(1);
                    //}
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            string s_formulaCode = dgv_FormulaBrowse.CurrentRow.Cells[0].Value.ToString();
                            string s_versionNum = dgv_FormulaBrowse.CurrentRow.Cells[1].Value.ToString();

                            string s_sql = "DELETE FROM formula_head WHERE" +
                                               " FormulaCode = '" + s_formulaCode + "' AND" +
                                               " VersionNum = '" + s_versionNum + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            s_sql = "DELETE FROM formula_details WHERE" +
                                        " FormulaCode = '" + s_formulaCode + "' AND" +
                                        " VersionNum = '" + s_versionNum + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            s_sql = "delete from  formula_handle_details where FormulaCode='" + s_formulaCode + "' and VersionNum='" + s_versionNum +"';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            FormulaBrowseHeadShow("");
                        }
                        catch
                        {

                        }

                    }
                }
            }
        }

        /// <summary>
        /// 显示配方浏览资料
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private void FormulaBrowseHeadShow(string _FormulaCode)
        {
            try
            {
                //获取当前配方表
                string s_sql = null;
                DataTable dt_data = new DataTable();

                //获取配方浏览资料表头
                if (rdo_Browse_All.Checked && string.IsNullOrEmpty(txt_Browse_Operator.Text))
                {
                    s_sql = "SELECT FormulaCode, MAX(VersionNum) FROM" +
                                " formula_head GROUP BY FormulaCode" +
                                " ORDER BY MAX(CreateTime) DESC ;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else if (rdo_Browse_NoDrop.Checked)
                {
                    s_sql = "SELECT FormulaCode, VersionNum FROM" +
                                " formula_head Where State = '" +
                                "尚未滴液" + "' ";
                    if (txt_Browse_Operator.Text != null && txt_Browse_Operator.Text != "") // 有操作员的情况下
                    {
                        s_sql = s_sql + "AND Operator ='" + txt_Browse_Operator.Text + "' ";
                    }
                    s_sql = s_sql + " ORDER BY CreateTime DESC;";

                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else
                {
                    string s_str = null;
                    if (txt_Browse_Operator.Text != null && txt_Browse_Operator.Text != "")
                    {
                        s_str = (" Operator = '" + txt_Browse_Operator.Text + "' AND");
                    }
                    if (txt_Browse_Code.Text != null && txt_Browse_Code.Text != "")
                    {
                        s_str += (" FormulaCode = '" + txt_Browse_Code.Text + "' AND");
                    }
                    if (dt_Browse_Start.Text != null && dt_Browse_Start.Text != "")
                    {
                        s_str += (" CreateTime >= '" + dt_Browse_Start.Text + "' AND");
                    }
                    else
                    {
                        return;
                    }

                    if (dt_Browse_End.Text != null && dt_Browse_End.Text != "")
                    {
                        s_str += (" CreateTime <= '" + dt_Browse_End.Text + "' ");
                    }
                    else
                    {
                        return;
                    }

                    s_sql = "SELECT FormulaCode, VersionNum FROM" +
                                " formula_head Where" + s_str + "" +
                                " ORDER BY CreateTime DESC   ;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                }

                //捆绑
                dgv_FormulaBrowse.DataSource = new DataView(dt_data);

                //设置标题文字
                //设置标题文字
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    dgv_FormulaBrowse.Columns[0].HeaderCell.Value = "配方代码";
                    dgv_FormulaBrowse.Columns[1].HeaderCell.Value = "版本";
                    //设置标题字体
                    dgv_FormulaBrowse.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                    //设置内容字体
                    dgv_FormulaBrowse.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
                }
                else
                {
                    dgv_FormulaBrowse.Columns[0].HeaderCell.Value = "RecipeCode";
                    dgv_FormulaBrowse.Columns[1].HeaderCell.Value = "Version";
                    //设置标题字体
                    dgv_FormulaBrowse.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);

                    //设置内容字体
                    dgv_FormulaBrowse.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                }


                //设置标题宽度
                dgv_FormulaBrowse.Columns[0].Width = 150;
                if (dgv_FormulaBrowse.Rows.Count > 27)
                {
                    dgv_FormulaBrowse.Columns[1].Width = 88;
                }
                else
                {
                    dgv_FormulaBrowse.Columns[1].Width = 108;
                }

                //关闭自动排序功能
                dgv_FormulaBrowse.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_FormulaBrowse.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dgv_FormulaBrowse.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



                //设置内容居中显示
                dgv_FormulaBrowse.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



                //设置行高
                dgv_FormulaBrowse.RowTemplate.Height = 30;

                //设置当前选中行
                if (_FormulaCode != "")
                {
                    for (int i = 0; i < dgv_FormulaBrowse.Rows.Count; i++)
                    {
                        string s = dgv_FormulaBrowse.Rows[i].Cells[0].Value.ToString();
                        if (s == _FormulaCode)
                        {
                            dgv_FormulaBrowse.CurrentCell = dgv_FormulaBrowse.Rows[i].Cells[0];
                            break;
                        }
                    }
                }
                else
                {
                    //if (dgv_FormulaBrowse.Rows.Count > 0)
                    //{
                    //    dgv_FormulaBrowse.CurrentCell = dgv_FormulaBrowse.Rows[0].Cells[0];
                    //}

                }

                s_sql = "SELECT CreateTime FROM" +
                                 " formula_head Order BY CreateTime DESC ;";
                dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_data.Rows.Count > 0)
                {
                    try
                    {
                        dt_Browse_Start.MinDate = Convert.ToDateTime(dt_data.Rows[dt_data.Rows.Count - 1][dt_data.Columns[0]]);
                        dt_Browse_Start.MaxDate = Convert.ToDateTime(dt_data.Rows[0][dt_data.Columns[0]]);
                        dt_Browse_Start.Value = Convert.ToDateTime(dt_data.Rows[dt_data.Rows.Count - 1][dt_data.Columns[0]]);
                        dt_Browse_End.MinDate = Convert.ToDateTime(dt_data.Rows[dt_data.Rows.Count - 1][dt_data.Columns[0]]);
                        dt_Browse_End.MaxDate = Convert.ToDateTime(dt_data.Rows[0][dt_data.Columns[0]]);
                        dt_Browse_End.Value = Convert.ToDateTime(dt_data.Rows[0][dt_data.Columns[0]]);
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "显示配方浏览资料", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Display recipe browsing information", MessageBoxButtons.OK, false);
            }
        }

        private void dgv_FormulaBrowse_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_FormulaBrowse.CurrentRow == null)
                {
                    return;
                }

                if (dgv_FormulaBrowse.SelectedRows.Count > 0)
                {
                    //设置矢能
                    Enabled_set();



                    //读取选中行对应的配方资料
                    //读取选中行对应的配方资料
                    string s_formulaCode = dgv_FormulaBrowse.CurrentRow.Cells[0].Value.ToString();
                    string s_versionNum = dgv_FormulaBrowse.CurrentRow.Cells[1].Value.ToString();
                    string s_sql = "SELECT * FROM formula_head" +
                                       " Where FormulaCode = '" + s_formulaCode + "'" +
                                       " AND VersionNum = '" + s_versionNum + "';";
                    DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    s_sql = "SELECT * FROM formula_details" +
                                " Where FormulaCode = '" + s_formulaCode + "'" +
                                " AND VersionNum = '" + s_versionNum + "' order by IndexNum;";
                    DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    string s_dyeingCode = dt_formulahead.Rows[0]["DyeingCode"] is DBNull ? "" : dt_formulahead.Rows[0]["DyeingCode"].ToString();

                    string s_li = dt_formulahead.Rows[0]["HandleBRList"] is DBNull ? "" : dt_formulahead.Rows[0]["HandleBRList"].ToString();
                    _lis_hBR.Clear();
                    if (s_li != "")
                    {
                        string[] sa_hBRList = s_li.Split('|');
                        _lis_hBR = sa_hBRList.ToList();
                    }

                    //显示表头
                    foreach (DataColumn mDc in dt_formulahead.Columns)
                    {
                        string s_name = "txt_" + mDc.Caption.ToString();
                        foreach (Control c in this.grp_FormulaData.Controls)
                        {
                            if ((c is TextBox || c is ComboBox) && c.Name == s_name)
                            {
                                c.Text = dt_formulahead.Rows[0][mDc].ToString();
                                break;
                            }
                        }
                        if (s_name == "txt_AddWaterChoose")
                        {
                            chk_AddWaterChoose.Checked = (dt_formulahead.Rows[0][mDc].ToString() == "False" || dt_formulahead.Rows[0][mDc].ToString() == "0" ? false : true);
                        }
                    }

                    

                    if (Lib_Card.Configure.Parameter.Other_Language != 0)
                    {
                        //中文换英文
                        if (txt_State.Text == "尚未滴液")
                        {
                            txt_State.Text = "Undropped";
                        }
                        else if (txt_State.Text == "已滴定配方")
                        {
                            txt_State.Text = "dropped";
                        }
                    }

                    txt_DyeingCode_SelectedIndexChanged(null, null);

                    //清理详细资料表
                    dgv_FormulaData.Rows.Clear();

                    //显示详细信息
                    for (int i = 0; i < dt_formuladetail.Rows.Count; i++)
                    {
                        dgv_FormulaData.Rows.Add(dt_formuladetail.Rows[i]["IndexNum"].ToString(),
                                                 dt_formuladetail.Rows[i]["AssistantCode"].ToString().Trim(),
                                                 dt_formuladetail.Rows[i]["AssistantName"].ToString().Trim(),
                                                 dt_formuladetail.Rows[i]["FormulaDosage"].ToString(),
                                                 dt_formuladetail.Rows[i]["UnitOfAccount"].ToString(),
                                                 null,
                                                 dt_formuladetail.Rows[i]["SettingConcentration"].ToString(),
                                                 dt_formuladetail.Rows[i]["RealConcentration"].ToString(),
                                                 dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString(),
                                                 dt_formuladetail.Rows[i]["RealDropWeight"].ToString());

                        //显示瓶号
                        s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                    " FROM bottle_details WHERE" +
                                    " AssistantCode = '" + dgv_FormulaData[1, i].Value.ToString() + "'" +
                                    " AND RealConcentration != 0 ORDER BY BottleNum ;";
                        DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


                        DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_FormulaData[5, i];
                        List<string> lis_bottleNum = new List<string>();
                        bool b_exist = false;
                        foreach (DataRow mdr in dt_bottlenum.Rows)
                        {
                            string s_num = mdr[0].ToString();

                            lis_bottleNum.Add(s_num);

                            if ((dt_formuladetail.Rows[i]["BottleNum"]).ToString() == s_num)
                            {
                                b_exist = true;
                            }

                        }


                        dd.Value = null;
                        dd.DataSource = lis_bottleNum;
                        if (b_exist)
                        {
                            dd.Value = (dt_formuladetail.Rows[i]["BottleNum"]).ToString();
                        }
                        else
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[i]["BottleNum"]).ToString() +
                                           "号母液瓶不存在", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[i]["BottleNum"]).ToString() +
                                           " The mother liquor bottle does not exist", "Tips", MessageBoxButtons.OK, false);
                        }


                        //显示是否手动选瓶
                        DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[10, i];
                        dc.Value = dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "False" || dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "0" ? 0 : 1;
                    }


                }


            }
            catch
            {
                //new FullAutomaticDripMachine.FADM_Object.MyAlarm(ex.Message, "批次表当前行改变事件", false);
            }
        }

        private void dgv_FormulaBrowse_Enter_1(object sender, EventArgs e)
        {
            dgv_BatchData.ClearSelection();
            dgv_BatchData.CurrentCell = null;
            this._b_newAdd = false;
        }

        private void dgv_Dye_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)sender;
                if (((FADM_Object.MyDataGridView)sender).CurrentCell.ColumnIndex == 5)
                {
                    ((DataGridViewComboBoxEditingControl)e.Control).SelectedValueChanged -= dgv_Dye_SelectedValueChanged;
                    ((DataGridViewComboBoxEditingControl)e.Control).SelectedValueChanged += dgv_Dye_SelectedValueChanged;
                    //((DataGridViewComboBoxEditingControl)e.Control).Enter += new EventHandler(Page_Formula_Enter);
                    ((DataGridViewComboBoxEditingControl)e.Control).DropDown -= dgv_Dye_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).DropDown += dgv_Dye_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).GotFocus -= dgv_Dye_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).GotFocus += dgv_Dye_DropDown;
                }
                if (dgv_Dye.CurrentCell.ColumnIndex == 3)
                {
                    ((DataGridViewTextBoxEditingControl)e.Control).KeyPress -= dgv_Dye_KeyPress;
                    ((DataGridViewTextBoxEditingControl)e.Control).KeyPress += dgv_Dye_KeyPress;
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_Dye_EditingControlShowing", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_Dye_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)sender;
            dgv_Dye.EndEdit();
            if (dgv_Dye[1, dgv_Dye.CurrentRow.Index].Value == null ||
                dgv_Dye[3, dgv_Dye.CurrentRow.Index].Value == null || _lis_handleBathRatio[Convert.ToInt32(dgv_Dye.Name)-1].Text=="")
            {
                return;
            }
            UpdataDyeAndHandle(dgv_Dye, dgv_Dye.CurrentRow.Index,Convert.ToDouble(_lis_handleBathRatio[Convert.ToInt32(dgv_Dye.Name) - 1].Text));
        }

        //Combobox下拉时事件
        void dgv_Dye_DropDown(object sender, EventArgs e)
        {

        }

        //配方用量输入检查
        void dgv_Dye_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)(((DataGridViewTextBoxEditingControl)sender).Parent.Parent);
                if (dgv_Dye.CurrentCell.ColumnIndex == 3)
                {
                    e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
                }
            }
            catch { }
        }

        //瓶号选择修改事件
        void dgv_Dye_SelectedValueChanged(object sender, EventArgs e)
        {

            try
            {
                FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)(((DataGridViewComboBoxEditingControl)sender).Parent.Parent);
                if (dgv_Dye.CurrentCell.ColumnIndex == 5)
                {
                    DataGridViewComboBoxEditingControl dd = (DataGridViewComboBoxEditingControl)sender;

                    bool b_temp = false;

                    //获取当前染助剂所有母液瓶资料
                    string s_sql = "SELECT BottleNum, SettingConcentration ,RealConcentration, DropMinWeight" +
                                       " FROM bottle_details WHERE" +
                                       " AssistantCode = '" + dgv_Dye.CurrentRow.Cells[1].Value.ToString() + "'" +
                                       " AND RealConcentration != 0 ORDER BY BottleNum ;";
                    DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    if (dt_bottlenum.Rows.Count > 0)
                    {
                        foreach (DataRow mdr in dt_bottlenum.Rows)
                        {
                            if (_lis_bottleNum[1] == mdr[0].ToString())
                            {
                                b_temp = true;
                                break;
                            }
                        }

                        foreach (DataRow mdr in dt_bottlenum.Rows)
                        {
                            if (dd.Text.ToString() == mdr[0].ToString())
                            {
                                dgv_Dye.CurrentRow.Cells[5].Value = mdr[0].ToString();
                                dgv_Dye.CurrentRow.Cells[6].Value = mdr[1].ToString();
                                dgv_Dye.CurrentRow.Cells[7].Value = mdr[2].ToString();
                                break;
                            }
                        }



                        if (_lis_bottleNum[0] == dgv_Dye.CurrentRow.Index.ToString() && _lis_bottleNum[1] != dgv_Dye.CurrentRow.Cells[5].Value.ToString() && b_temp)
                        {
                            //设置手动选瓶标志位
                            dgv_Dye.CurrentRow.Cells[10].Value = 1;
                        }



                        //计算目标滴液量
                        double d_objectDropWeight = 0;
                        if (dgv_Dye.CurrentRow.Cells[4].Value.ToString() == "%")
                        {
                            //染料
                            d_objectDropWeight = (Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(dgv_Dye.CurrentRow.Cells[3].Value.ToString()) / Convert.ToDouble(dgv_Dye.CurrentRow.Cells[7].Value.ToString()));
                        }
                        else
                        {
                            //助剂
                            d_objectDropWeight = (Convert.ToDouble(txt_TotalWeight.Text) * Convert.ToDouble(dgv_Dye.CurrentRow.Cells[3].Value.ToString()) / Convert.ToDouble(dgv_Dye.CurrentRow.Cells[7].Value.ToString()));

                        }

                        dgv_Dye.CurrentRow.Cells[8].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight) : String.Format("{0:F3}", d_objectDropWeight);
                    }

                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_Dye_SelectedValueChanged", MessageBoxButtons.OK, true);
            }

        }

        private void UpdataDyeAndHandle(FADM_Object.MyDataGridView dgv, int _CurrentRowIndex,double handelBR)
        {
            try
            {
                if (txt_ClothWeight.Text == "" || /*txt_TotalWeight.Text == "" ||*/ dgv[3, _CurrentRowIndex].Value == null || dgv[3, _CurrentRowIndex].Value.ToString() == "")
                {
                    return;
                }

                DataTable P_dt_currentassistantcodeallbottlenum = new DataTable();

                if (_CurrentRowIndex >= dgv.Rows.Count)
                {
                    return;
                }
                string P_str_sql = null;



                //获取染助剂资料
                P_str_sql = "SELECT *  FROM assistant_details WHERE" +
                            " AssistantCode = '" + dgv[1, _CurrentRowIndex].Value.ToString() + "' ; ";

                DataTable dt_assistantdetails = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                if (dt_assistantdetails.Rows.Count > 0)
                {
                    dgv[4, _CurrentRowIndex].Value = (dt_assistantdetails.Rows[0][5].ToString());
                    dgv[2, _CurrentRowIndex].Value = dt_assistantdetails.Rows[0][3].ToString();
                    dgv[9, _CurrentRowIndex].Value = "0.00";
                    //获取当前染助剂所有母液瓶资料
                    P_str_sql = "SELECT  BottleNum, SettingConcentration ,RealConcentration, DropMinWeight" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv[1, _CurrentRowIndex].Value.ToString() + "'" +
                                "  AND RealConcentration != 0  ORDER BY BottleNum ;";
                    P_dt_currentassistantcodeallbottlenum = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                    //未找到一个合适的瓶
                    if (P_dt_currentassistantcodeallbottlenum.Rows.Count == 0)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("当前染助剂代码未发现母液瓶！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("No mother liquor bottle found for the current dyeing agent code！", "Tips", MessageBoxButtons.OK, false);

                        for (int i = 1; i < dgv.Columns.Count - 1; i++)
                        {
                            dgv.CurrentRow.Cells[i].Value = null;
                        }

                        return;
                    }
                    List<string> lis_bottleNum = new List<string>();
                    foreach (DataRow mdr in P_dt_currentassistantcodeallbottlenum.Rows)
                    {
                        lis_bottleNum.Add(mdr[0].ToString());
                    }
                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv[5, _CurrentRowIndex];
                    for (int j = 0; j < lis_bottleNum.Count; j++)
                    {
                        for (int i = 0; i < dd.Items.Count; i++)
                        {
                            if (dd.Items[i].ToString() == lis_bottleNum[j])
                            {
                                goto next;
                            }
                        }
                        dd.Value = null;
                        dd.DataSource = lis_bottleNum;
                        break;
                    next:
                        continue;
                    }

                    P_dt_currentassistantcodeallbottlenum.Clear();
                    //跟据设定浓度重新排序
                    P_str_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight,SyringeType" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv[1, _CurrentRowIndex].Value.ToString() + "'" +
                                " AND RealConcentration != 0 ORDER BY SettingConcentration DESC;";

                    P_dt_currentassistantcodeallbottlenum = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);



                    for (int i = 0; i < P_dt_currentassistantcodeallbottlenum.Rows.Count; i++)
                    {
                        double d_objectDropWeight = 0;
                        //判断是否需要自动选瓶
                        if (dgv[10, _CurrentRowIndex].Value == null ||
                            dgv[10, _CurrentRowIndex].Value.ToString() == "0")
                        {
                            //需要自动选瓶
                            if (dgv.Rows[_CurrentRowIndex].Cells[4].Value != null)
                            {
                                if (dgv.Rows[_CurrentRowIndex].Cells[4].Value.ToString() == "%")
                                {
                                    //染料
                                    d_objectDropWeight = (Convert.ToDouble(txt_ClothWeight.Text) *
                                        Convert.ToDouble(dgv[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(P_dt_currentassistantcodeallbottlenum.Rows[i][2].ToString()));
                                }
                                else
                                {
                                    //助剂
                                    d_objectDropWeight = (Convert.ToDouble(txt_ClothWeight.Text) * handelBR *
                                        Convert.ToDouble(dgv[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(P_dt_currentassistantcodeallbottlenum.Rows[i][2].ToString()));

                                }
                                if (Convert.ToDouble(String.Format("{0:F3}", d_objectDropWeight)) >=
                                    Convert.ToDouble(String.Format("{0:F3}", P_dt_currentassistantcodeallbottlenum.Rows[i][3])))
                                {

                                    dd.Value = P_dt_currentassistantcodeallbottlenum.Rows[i][0].ToString();
                                    dgv[6, _CurrentRowIndex].Value = P_dt_currentassistantcodeallbottlenum.Rows[i][1].ToString();
                                    dgv[7, _CurrentRowIndex].Value = P_dt_currentassistantcodeallbottlenum.Rows[i][2].ToString();
                                    dgv[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight) : String.Format("{0:F3}", d_objectDropWeight);
                                    break;
                                }
                                else
                                {
                                    if (i == P_dt_currentassistantcodeallbottlenum.Rows.Count - 1)
                                    {
                                        if (d_objectDropWeight >= 0.1)
                                        {
                                            dd.Value = P_dt_currentassistantcodeallbottlenum.Rows[i][0].ToString();
                                            dgv[6, _CurrentRowIndex].Value = P_dt_currentassistantcodeallbottlenum.Rows[i][1].ToString();
                                            dgv[7, _CurrentRowIndex].Value = P_dt_currentassistantcodeallbottlenum.Rows[i][2].ToString();
                                            dgv[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight) : String.Format("{0:F3}", d_objectDropWeight);
                                        }
                                        else
                                        {
                                            dd.Value = null;
                                            dgv[6, _CurrentRowIndex].Value = null;
                                            dgv[7, _CurrentRowIndex].Value = null;
                                            dgv[8, _CurrentRowIndex].Value = null;
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            //不需要自动选瓶

                            //获取当前染助剂所有母液瓶资料
                            foreach (DataRow mdr in P_dt_currentassistantcodeallbottlenum.Rows)
                            {
                                if (dd.Value.ToString() == mdr[0].ToString())
                                {
                                    dgv[5, _CurrentRowIndex].Value = mdr[0].ToString();
                                    dgv[6, _CurrentRowIndex].Value = mdr[1].ToString();
                                    dgv[7, _CurrentRowIndex].Value = mdr[2].ToString();
                                    break;
                                }
                            }

                            //计算目标滴液量
                            if (dgv[4, _CurrentRowIndex].Value != null)
                            {
                                if (dgv[4, _CurrentRowIndex].Value.ToString() == "%")
                                {
                                    //染料
                                    d_objectDropWeight = (Convert.ToDouble(txt_ClothWeight.Text) *
                                        Convert.ToDouble(dgv[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(dgv[7, _CurrentRowIndex].Value.ToString()));
                                }
                                else
                                {
                                    //助剂
                                    d_objectDropWeight = (Convert.ToDouble(txt_ClothWeight.Text) * handelBR *
                                        Convert.ToDouble(dgv[3, _CurrentRowIndex].Value.ToString()) /
                                        Convert.ToDouble(dgv[7, _CurrentRowIndex].Value.ToString()));

                                }

                                dgv[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight) : String.Format("{0:F3}", d_objectDropWeight);
                                break;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "UpdataDyeAndHandle", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_Dye_SelectionChanged(object sender, EventArgs e)
        {

            FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)sender;
            if (dgv_Dye.Rows.Count >= 1)
            {
                int i_col = dgv_Dye.CurrentCell.ColumnIndex;

                int i_row = dgv_Dye.CurrentCell.RowIndex;

                if (i_col == 3)
                {
                    if (i_row == dgv_Dye.Rows.Count - 1)
                    {
                        if (Convert.ToInt32(dgv_Dye.Name) < _lis_dg.Count)
                        {
                            //((FADM_Object.MyDataGridView)(lis_dg[Convert.ToInt32(dgv_Dye.Name)])).CurrentCell = ((FADM_Object.MyDataGridView)(lis_dg[Convert.ToInt32(dgv_Dye.Name)]))[1, 0];
                            //((FADM_Object.MyDataGridView)(lis_dg[Convert.ToInt32(dgv_Dye.Name)])).Focus();
                            _lis_handleBathRatio[Convert.ToInt32(dgv_Dye.Name)].Enabled=true;
                            _lis_handleBathRatio[Convert.ToInt32(dgv_Dye.Name)].Focus();


                        }
                        else
                        {
                            btn_Save.Focus();
                        }

                    }

                }
                if (dgv_Dye.CurrentCell.ColumnIndex == 5)
                {
                    try
                    {

                        _lis_bottleNum.Clear();
                        _lis_bottleNum.Add(dgv_Dye.CurrentRow.Index.ToString());
                        if (dgv_Dye.CurrentRow.Cells[5].Value != null)
                        {
                            _lis_bottleNum.Add(dgv_Dye.CurrentRow.Cells[5].Value.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_Dye_SelectionChanged", MessageBoxButtons.OK, true);
                    }
                }
            }

        }
    }


}
