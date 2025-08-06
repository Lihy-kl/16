using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using SmartDyeing.FADM_Form;
using System.Runtime.InteropServices;
using static System.Windows.Forms.AxHost;
using SmartDyeing.FADM_Object;

namespace SmartDyeing.FADM_Control
{
    public partial class P_Formula : UserControl
    {
        //声明主窗体
        SmartDyeing.FADM_Form.Main _formMain = null;

        bool _b_frist = false;

        bool _b_newAdd = false;

        bool _b_groupFlag = false;


        //构造函数
        public P_Formula(SmartDyeing.FADM_Form.Main _FormMain, string OperatorText) //多加个参数 操作员
        {

            InitializeComponent();

            //if (!string.IsNullOrEmpty(OperatorText) && OperatorText != null) //证明是操作员进来 先赋值
            //{
            //    this.txt_Record_Operator.Text = OperatorText;
            //}
            //else
            //{
            //    this.txt_Record_Operator.Text = "";
            //}



            this._b_frist = true;
            this.tmr.Enabled = true;


            _formMain = _FormMain;
            cup_sort();
            DropRecordHeadShow("");
            FormulaBrowseHeadShow("");


            string s_sql = "SELECT * FROM operator_table ;";
            DataTable dt_operator_table = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            foreach (DataRow dr in dt_operator_table.Rows)
            {
                //txt_Operator.Items.Add(Convert.ToString(dr[0])); //中间这里的操作员不加载
                txt_Browse_Operator.Items.Add(Convert.ToString(dr[0]));
                //txt_Record_Operator.Items.Add(Convert.ToString(dr[0]));
            }


            //if (_formMain.BtnUserSwitching.Text == "管理用户" || _formMain.BtnUserSwitching.Text == "工程师")
            //{
            //    this.btn_Save.Enabled = false;
            //    if (_formMain.BtnUserSwitching.Text.Equals("工程师"))
            //    {
            //        txt_Record_Operator.Items.Add("全部");
            //    }
            //}




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
                    string s_name = "lab_" + (mDc.Caption.ToString().Remove(0, 4));
                    foreach (Control c in this.grp_FormulaData.Controls)
                    {
                        if (c is Label && c.Name == s_name)
                        {
                            c.ForeColor = ((dt_enabled.Rows[0][mDc].ToString()) == "0" ?
                                          SystemColors.ButtonShadow : SystemColors.ControlText);
                        }
                    }
                }
            }





        }

        //重写ProcessDialogKey函数
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F4:
                    btn_BatchAdd_Click(null, null);
                    return false;
                case Keys.F2:
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
            if (FADM_Object.Communal._s_operator == "管理用户" || FADM_Object.Communal._s_operator.Equals("工程师"))
            {
                Label lab = (Label)sender;
                if (lab.Name != "lab_FormulaCode" && lab.Name != "lab_TotalWeight" &&
                    lab.Name != "lab_CreateTime" && lab.Name != "lab_ClothWeight" &&
                    lab.Name != "lab_BathRatio")
                {
                    string s_name = "txt_" + lab.Name.Remove(0, 4);
                    foreach (Control c in this.grp_FormulaData.Controls)
                    {
                        if ((c is TextBox || c is ComboBox) && c.Name == s_name)
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

        //TextBox文本框按下Enter事件
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Control[] c = { txt_FormulaCode, txt_FormulaName, txt_ClothType, txt_Customer,
                            chk_AddWaterChoose, txt_ClothWeight,
                            txt_BathRatio,txt_Non_AnhydrationWR, txt_Operator,txt_CupCode, dgv_FormulaData };
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    for (int i = 0; i < c.Length; i++)
                    {
                        try
                        {
                            TextBox txt = (TextBox)sender;
                            Boolean istrue = false;
                            if (txt.Name == "group")
                            { //组合框回车
                                istrue = true;
                            }

                            if (txt.Text != null && txt.Text != "" && (((txt.Name == "txt_ClothWeight" ||
                                txt.Name == "txt_BathRatio" || txt.Name == "txt_Non_AnhydrationWR") && txt.Text != "0") ||
                                (txt.Name != "txt_ClothWeight" && txt.Name != "txt_BathRatio")) || istrue)
                            {
                                if (txt.Name == "txt_FormulaCode")
                                {
                                    try
                                    {
                                        string s_formulaCode = null;
                                        string s_versionNum = null;
                                        //读取选中行对应的配方资料
                                        try
                                        {
                                            this.group.Text = "";
                                            s_formulaCode = txt.Text;


                                            string s_sql = "SELECT Top 1 * FROM formula_head Where" +
                                                               " FormulaCode = '" + s_formulaCode + "'" +
                                                               " ORDER BY VersionNum DESC;";

                                            DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                            s_versionNum = dt_formulahead.Rows[0]["VersionNum"].ToString();

                                            s_sql = "SELECT * FROM formula_details" +
                                                        " Where FormulaCode = '" + s_formulaCode + "'" +
                                                        " AND VersionNum = '" + s_versionNum + "'  order by IndexNum;";

                                            DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                        }
                                        catch
                                        {
                                            //配方浏览没有当前配方
                                            dgv_FormulaBrowse.ClearSelection();
                                            txt_VersionNum.Text = null;
                                            txt_State.Text = null;
                                            txt_CreateTime.Text = null;
                                            for (int j = 0; j < dgv_FormulaData.Rows.Count; j++)
                                            {
                                                DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[11, j];
                                                dc.Value = 0;
                                            }
                                            //跳转组合名称 判断是否有组合
                                            if (this._b_groupFlag)
                                            {
                                                this.group.Enabled = true;
                                                this.group.Focus();
                                                return;
                                            }
                                            else
                                            {
                                                goto next;
                                            }

                                            //  goto next;
                                        }

                                        bool b_have = false;

                                        foreach (DataGridViewRow dr in dgv_FormulaBrowse.Rows) //遍历右边配方浏览
                                        {
                                            if (dr.Cells[0].Value.ToString() == s_formulaCode) //如果右边配方浏览里有配方代码 则设置单元格活动 调到1169行 配方代码改变事件
                                            {
                                                DataGridViewCell dd = dgv_FormulaBrowse.CurrentCell;
                                                Console.WriteLine(dd.Value);
                                                if (dd.Value == dgv_FormulaBrowse.Rows[dr.Index].Cells[0].Value) //相等的情况下 不可能触发改变事件 所以导致原有填充的表格数据删掉后 不会重新填充
                                                {
                                                    dgv_FormulaBrowse.CurrentCell = dgv_FormulaBrowse.Rows[dr.Index].Cells[0];
                                                    break;
                                                }
                                                else
                                                {
                                                    dgv_FormulaBrowse.CurrentCell = dgv_FormulaBrowse.Rows[dr.Index].Cells[0];
                                                    b_have = true;
                                                    break;
                                                }


                                            }
                                        }

                                        if (b_have == false)
                                        {
                                            //设置矢能
                                            Enabled_set();

                                            //读取选中行对应的配方资料

                                            string s_sql = "SELECT * FROM formula_head" +
                                                               " Where FormulaCode = '" + s_formulaCode + "'" +
                                                               " AND VersionNum = '" + s_versionNum + "';";
                                            DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                            s_sql = "SELECT * FROM formula_details" +
                                                        " Where FormulaCode = '" + s_formulaCode + "'" +
                                                        " AND VersionNum = '" + s_versionNum + "' order by IndexNum;";
                                            DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                            //显示表头
                                            foreach (DataColumn mDc in dt_formulahead.Columns)
                                            {
                                                string s_name = "txt_" + mDc.Caption.ToString();
                                                foreach (Control c1 in this.grp_FormulaData.Controls)
                                                {
                                                    if ((c1 is TextBox || c1 is ComboBox) && c1.Name == s_name)
                                                    {
                                                        c1.Text = dt_formulahead.Rows[0][mDc].ToString();
                                                        break;
                                                    }
                                                }
                                                if (s_name == "txt_AddWaterChoose")
                                                {
                                                    chk_AddWaterChoose.Checked = (dt_formulahead.Rows[0][mDc].ToString() == "0" ? false : true);
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

                                            //清理详细资料表
                                            dgv_FormulaData.Rows.Clear();

                                            if (dt_formuladetail.Rows.Count > 0)
                                            {
                                                //显示详细信息
                                                for (int j = 0; j < dt_formuladetail.Rows.Count; j++)
                                                {
                                                    dgv_FormulaData.Rows.Add(dt_formuladetail.Rows[j]["IndexNum"].ToString(),
                                                                             dt_formuladetail.Rows[j]["AssistantCode"].ToString(),
                                                                             dt_formuladetail.Rows[j]["AssistantName"].ToString(),
                                                                             dt_formuladetail.Rows[j]["FormulaDosage"].ToString(),
                                                                             dt_formuladetail.Rows[j]["UnitOfAccount"].ToString(),
                                                                             null,
                                                                             dt_formuladetail.Rows[j]["SettingConcentration"].ToString(),
                                                                             dt_formuladetail.Rows[j]["RealConcentration"].ToString(),
                                                                             dt_formuladetail.Rows[j]["ObjectDropWeight"].ToString(),
                                                                             dt_formuladetail.Rows[j]["RealDropWeight"].ToString(), "0.00");

                                                    //显示瓶号
                                                    s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                                                " FROM bottle_details WHERE" +
                                                                " AssistantCode = '" + dgv_FormulaData[1, j].Value.ToString() + "'" +
                                                                " AND RealConcentration != 0 Order BY BottleNum ;";
                                                    DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_FormulaData[5, j];
                                                    List<string> lis_bottleNum = new List<string>();

                                                    bool b_exist = false;
                                                    foreach (DataRow mdr in dt_bottlenum.Rows)
                                                    {
                                                        string s_num = mdr[0].ToString();

                                                        lis_bottleNum.Add(s_num);

                                                        if ((dt_formuladetail.Rows[j]["BottleNum"]).ToString() == s_num)
                                                        {
                                                            b_exist = true;
                                                        }

                                                    }

                                                    dd.Value = null;
                                                    dd.DataSource = lis_bottleNum;

                                                    if (b_exist)
                                                    {
                                                        dd.Value = (dt_formuladetail.Rows[j]["BottleNum"]).ToString();
                                                    }
                                                    else
                                                    {
                                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                            FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[j]["BottleNum"]).ToString() +
                                                                         "号母液瓶不存在", "温馨提示",MessageBoxButtons.OK, false);
                                                        else
                                                            FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[j]["BottleNum"]).ToString() +
                                                                         " Mother liquor bottle number does not exist", "Tips", MessageBoxButtons.OK, false);
                                                    }


                                                    //显示是否手动选瓶
                                                    DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[11, j];
                                                    dc.Value = dt_formuladetail.Rows[j]["BottleSelection"].ToString() == "0" ? 0 : 1;
                                                }
                                                //dgv_FormulaData.Enabled = true;
                                                //dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                                //dgv_FormulaData.Focus();
                                                //return;
                                            }
                                            else
                                            {

                                            }

                                            dgv_FormulaData.ClearSelection();
                                            dgv_FormulaBrowse.ClearSelection();

                                        }


                                    }
                                    catch
                                    {

                                    }

                                }
                            next:
                                if (false) //组合配方回车了 不管有没有值 都跳转下一个聚焦框上"group".Equals(txt.Name)
                                {


                                }
                                else
                                {
                                    if (txt.Name == c[i].Name)
                                    {
                                        for (int j = i; j < c.Length; j++)
                                        {
                                            string s_sql = "SELECT " + (c[j + 1].Name) + " FROM enabled_set;";
                                            string s_choose = (FADM_Object.Communal._fadmSqlserver.GetData(s_sql)).Rows[0][0].ToString();
                                            if (s_choose != "0")
                                            {
                                                if (c[j + 1].Name != dgv_FormulaData.Name)
                                                {
                                                    c[j + 1].Enabled = true;
                                                    c[j + 1].Focus();

                                                    return;
                                                }
                                                else
                                                {
                                                    dgv_FormulaData.Enabled = true;
                                                    dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                                    dgv_FormulaData.Focus();
                                                    return;
                                                }
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

                                        if (s_choose != "0")
                                        {
                                            if (c[j + 1].Name != dgv_FormulaData.Name)
                                            {
                                                c[j + 1].Enabled = true;
                                                c[j + 1].Focus();
                                                return;
                                            }
                                            else
                                            {
                                                dgv_FormulaData.Enabled = true;
                                                dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                                dgv_FormulaData.Focus();
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                ComboBox cbo = (ComboBox)sender;
                                if (false) //回车的组合配方 并且有上一次的配方
                                {
                                    dgv_FormulaData.Enabled = true;
                                    dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                    dgv_FormulaData.Focus();
                                    return;

                                }
                                else if (cbo.Name.Equals("group"))
                                {
                                    for (int j = 0; j < c.Length; j++)
                                    {
                                        string s_sql = "SELECT " + (c[j + 1].Name) + " FROM enabled_set;";
                                        string s_choose = (FADM_Object.Communal._fadmSqlserver.GetData(s_sql)).Rows[0][0].ToString();
                                        if (s_choose != "0")
                                        {
                                            if (c[j + 1].Name != dgv_FormulaData.Name)
                                            {
                                                c[j + 1].Enabled = true;
                                                c[j + 1].Focus();

                                                return;
                                            }
                                            else
                                            {
                                                dgv_FormulaData.Enabled = true;
                                                dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                                dgv_FormulaData.Focus();
                                                return;
                                            }
                                        }
                                    }
                                    return;
                                }
                                if (cbo.Name == c[i].Name)
                                {
                                    for (int j = i; j < c.Length; j++)
                                    {
                                        string s_sql = "SELECT " + (c[j + 1].Name) + " FROM enabled_set;";
                                        string s_choose = (FADM_Object.Communal._fadmSqlserver.GetData(s_sql)).Rows[0][0].ToString();

                                        if (s_choose != "0")
                                        {
                                            if (c[j + 1].Name != dgv_FormulaData.Name)
                                            {
                                                c[j + 1].Enabled = true;
                                                c[j + 1].Focus();
                                                return;
                                            }
                                            else
                                            {
                                                dgv_FormulaData.Enabled = true;
                                                dgv_FormulaData.CurrentCell = dgv_FormulaData[1, 0];
                                                dgv_FormulaData.Focus();
                                                return;
                                            }
                                        }
                                    }
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
        private void BatchHeadShow(string s_cupNum)
        {
            try
            {
                //获取批次资料表头
                string s_sql = "SELECT CupNum, FormulaCode, VersionNum" +
                                   " FROM drop_head Order BY CupNum ;";
                DataTable dt_formula = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //捆绑
                dgv_BatchData.DataSource = new DataView(dt_formula);

                //设置标题文字
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    dgv_BatchData.Columns[0].HeaderCell.Value = "杯号";
                    dgv_BatchData.Columns[1].HeaderCell.Value = "配方";
                    dgv_BatchData.Columns[2].HeaderCell.Value = "版本";
                    //设置标题字体
                    dgv_BatchData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);
                    //设置内容字体
                    dgv_BatchData.RowsDefaultCellStyle.Font = new Font("宋体", 12.5F);
                }
                else
                {
                    dgv_BatchData.Columns[0].HeaderCell.Value = "CupNumber";
                    dgv_BatchData.Columns[1].HeaderCell.Value = "RecipeCode";
                    dgv_BatchData.Columns[2].HeaderCell.Value = "Version";

                    //设置标题字体
                    dgv_BatchData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);

                    //设置内容字体
                    dgv_BatchData.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                }

                //设置标题宽度
                dgv_BatchData.Columns[0].Width = 70;
                dgv_BatchData.Columns[1].Width = 240;
                if (dgv_BatchData.Rows.Count > 19)
                {
                    dgv_BatchData.Columns[2].Width = 57;
                }
                else
                {
                    dgv_BatchData.Columns[2].Width = 77;
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
                if (s_cupNum != "")
                {
                    for (int i = 0; i < dgv_BatchData.Rows.Count; i++)
                    {
                        string s = dgv_BatchData.Rows[i].Cells[0].Value.ToString();
                        if (s == s_cupNum)
                        {
                            dgv_BatchData.CurrentCell = dgv_BatchData.Rows[i].Cells[0];
                            break;
                        }
                    }
                }

                dgv_BatchData.CurrentCell = null;


            }
            catch //(Exception ex)
            {
                // new Class_Alarm.MyAlarm(ex.Message, "显示批次资料", false);
            }
        }

        //批次表绑定数据事件
        private void dgv_BatchData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            //更改颜色
            string s_sql = "SELECT BatchName FROM enabled_set WHERE MyID = 1;";
            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            string s_batchNum = Convert.ToString(dt_data.Rows[0][dt_data.Columns[0]]);


            //P_str_sql = "SELECT DropAllowError, AddWaterAllowError, AddPowderAllowError FROM" +
            //                          " other_parameters WHERE MyID = 1;";

            //DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

            double d_bl_drop_allow_err = Lib_Card.Configure.Parameter.Other_AErr_Drip;
            int i_watrt_allow_err = 8;// Convert.ToInt16(_dt_data.Rows[0][_dt_data.Columns[1]]);
            double d_bl_powder_allow_err = Lib_Card.Configure.Parameter.Other_AErr_Drip;

            s_sql = "SELECT * FROM drop_head WHERE" +
                        " BatchName = '" + s_batchNum + "' ORDER BY CupNum;";
            dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_data.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_data.Rows)
                {
                    int i_no_old = Convert.ToInt16(dr["CupNum"].ToString());
                    int i_finish = Convert.ToString(dr["CupFinish"].ToString()) == "0" ? 0 : 1;

                    double d_bl_objWater = Convert.ToDouble(dr["ObjectAddWaterWeight"]);
                    double d_bl_realWater = Convert.ToDouble(dr["RealAddWaterWeight"]);
                    double d_bl_TestTubeObjectAddWaterWeight = Convert.ToDouble(dr["TestTubeObjectAddWaterWeight"]);
                    double d_testTubeRealAddWaterWeight = Convert.ToDouble(dr["TestTubeRealAddWaterWeight"]);
                    string s_describeChar = dr["DescribeChar"] is DBNull ? "" : dr["DescribeChar"].ToString();

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

                                ////P_str_sql = "SELECT MachineType FROM machine_parameters WHERE MyID = 1;";
                                ////DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                //int i_machine = Lib_Card.Configure.Parameter.Machine_Bottle_Total;

                                //s_sql = "SELECT CupNum FROM drop_details WHERE" +
                                //            " BatchName = '" + s_batchNum + "' AND CupNum = " + i_cup + " AND" +
                                //            " ((ROUND(ABS(ObjectDropWeight * 100 - RealDropWeight * 100),2) > " + d_bl_drop_allow_err * 100 + " AND" +
                                //            " BottleNum > 0 AND BottleNum <= " + i_machine + ") OR" +
                                //            " (ROUND(ABS(ObjectDropWeight * 100 - RealDropWeight * 100),2) > " + d_bl_powder_allow_err * 100 + " AND" +
                                //            " (BottleNum = 200 OR BottleNum = 201))) Order BY CupNum;";
                                //DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                //bool b_err = false;
                                //foreach (DataRow dr1 in dt_data1.Rows)
                                //{
                                //    if (Convert.ToInt16(dr1[0]) == Convert.ToInt16(i_cup))
                                //    {
                                //        b_err = true;
                                //        break;
                                //    }
                                //}
                                //if (b_err)
                                //{
                                //    dgvr.DefaultCellStyle.BackColor = Color.Red;
                                //    b_err = false;
                                //}
                                //else
                                //{

                                //    double d_bl_realDif = Convert.ToDouble(string.Format("{0:F3}", d_bl_realWater - d_bl_objWater));
                                //    d_bl_realDif = d_bl_realDif < 0 ? -d_bl_realDif : d_bl_realDif;
                                //    double d_bl_allDif = Convert.ToDouble(string.Format("{0:F3}", d_bl_objWater * Convert.ToDouble(i_watrt_allow_err / 100.00)));
                                //    double d_bl_TestTube_err = Convert.ToDouble( string.Format("{0:F3}", d_bl_TestTubeObjectAddWaterWeight - d_testTubeRealAddWaterWeight));

                                //    d_bl_TestTube_err = d_bl_TestTube_err < 0 ? -d_bl_TestTube_err : d_bl_TestTube_err;

                                //    if (d_bl_allDif < d_bl_realDif || d_bl_TestTube_err > d_bl_drop_allow_err)
                                //    {
                                //        dgvr.DefaultCellStyle.BackColor = Color.Red;
                                //    }
                                //    else
                                //    {
                                //        dgvr.DefaultCellStyle.BackColor = Color.Lime;
                                //    }
                                //}


                                if (s_describeChar.Contains("失败"))
                                //if (b_err)
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.Red;
                                }
                                else
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.Lime;
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
                        DataTable P_dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        s_sql = "SELECT * FROM drop_details Where CupNum = '" + s_cupNum + "' order by IndexNum;";
                        DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        //显示表头
                        foreach (DataColumn mDc in P_dt_formulahead.Columns)
                        {
                            string s_name = "txt_" + mDc.Caption.ToString();
                            foreach (Control c in this.grp_FormulaData.Controls)
                            {
                                if ((c is TextBox || c is ComboBox) && c.Name == s_name)
                                {
                                    c.Text = P_dt_formulahead.Rows[0][mDc].ToString();
                                    break;
                                }
                            }
                            if (s_name == "txt_AddWaterChoose")
                            {
                                chk_AddWaterChoose.Checked = (P_dt_formulahead.Rows[0][mDc].ToString() == "0" ? false : true);
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

                        //清理详细资料表
                        dgv_FormulaData.Rows.Clear();

                        //显示详细信息
                        for (int i = 0; i < dt_formuladetail.Rows.Count; i++)
                        {
                            dgv_FormulaData.Rows.Add(dt_formuladetail.Rows[i]["IndexNum"].ToString(),
                                                     dt_formuladetail.Rows[i]["AssistantCode"].ToString(),
                                                     dt_formuladetail.Rows[i]["AssistantName"].ToString(),
                                                     dt_formuladetail.Rows[i]["FormulaDosage"].ToString(),
                                                     dt_formuladetail.Rows[i]["UnitOfAccount"].ToString(),
                                                     null,
                                                     dt_formuladetail.Rows[i]["SettingConcentration"].ToString(),
                                                     dt_formuladetail.Rows[i]["RealConcentration"].ToString(),
                                                     dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString(),
                                                     dt_formuladetail.Rows[i]["RealDropWeight"].ToString(),
                                                     "0.00");

                            //显示瓶号
                            s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                        " FROM bottle_details WHERE" +
                                        " AssistantCode = '" + dgv_FormulaData[1, i].Value.ToString() + "'" +
                                        " AND RealConcentration != 0 Order BY BottleNum ;";
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
                                                "号母液瓶不存在", "批次表当前行改变事件", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show((dt_formuladetail.Rows[i]["BottleNum"]).ToString() +
                                                " Mother liquor bottle number does not exist", "Batch table current row change event", MessageBoxButtons.OK, false);

                            }

                            if (dt_formuladetail.Rows[i]["Finish"].ToString() == "1")
                            {
                                if (dt_formuladetail.Rows[i]["ObjectDropWeight"] is DBNull || dt_formuladetail.Rows[i]["RealDropWeight"] is DBNull)
                                {
                                    dgv_FormulaData.Rows[i ].DefaultCellStyle.BackColor = Color.Red;
                                }
                                else
                                {
                                    double d_error = Lib_Card.Configure.Parameter.Other_AErr_Drip;
                                    if (!(dt_formuladetail.Rows[i]["StandError"] is DBNull))
                                    {
                                        d_error = Convert.ToDouble(dt_formuladetail.Rows[i]["StandError"]);
                                    }
                                    if (Math.Abs(Convert.ToDouble(dt_formuladetail.Rows[i]["ObjectDropWeight"]) - Convert.ToDouble(dt_formuladetail.Rows[i]["RealDropWeight"])) > d_error)
                                    {
                                        dgv_FormulaData.Rows[i ].DefaultCellStyle.BackColor = Color.Red;
                                    }
                                }
                            }


                            //显示是否手动选瓶
                            DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[11, i];
                            dc.Value = dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "0" ? 0 : 1;

                            DataGridViewTextBoxCell tx = (DataGridViewTextBoxCell)dgv_FormulaData[12, i];
                            tx.Value = "";
                        }


                    }


                }
                catch
                {

                }
            }
        }

        int _i_delect_index = -1;
        //批次表按下删除键事件
        private void dgv_BatchData_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    try
                    {
                        //string s_sql1 = "SELECT * FROM drop_head  where BatchName != '0' and Stage = '滴液'   order by CupNum  ;";
                        //DataTable dt_head_Drip = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);

                        //if (dt_head_Drip.Rows.Count > 0)
                        //{
                        //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        //        FADM_Form.CustomMessageBox.Show("滴液过程不能删除", "温馨提示", MessageBoxButtons.OK, false);
                        //    else
                        //        FADM_Form.CustomMessageBox.Show("The dripping process cannot be deleted", "Tips", MessageBoxButtons.OK, false);
                        //    return;
                        //}

                        _i_delect_index = Convert.ToInt16(dgv_BatchData.CurrentRow.Index);
                        string s_sql = null;
                        foreach (DataGridViewRow dr in dgv_BatchData.SelectedRows)
                        {
                            if (dr.DefaultCellStyle.BackColor == Color.DarkGray ||
                                dr.DefaultCellStyle.BackColor == Color.Red ||
                                dr.DefaultCellStyle.BackColor == Color.Lime)
                            {
                                continue;
                            }

                            string s_cup = dr.Cells[0].Value.ToString();

                            //删除批次浏览表头资料
                            s_sql = "DELETE FROM drop_head WHERE CupNum = '" + s_cup + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                            //删除批次浏览表详细资料
                            s_sql = "DELETE FROM drop_details WHERE CupNum = '" + s_cup + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        P_bl_update = true;
                    }
                    catch
                    {

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
                    //string s_sql2 = "SELECT * FROM drop_head  where BatchName != '0' and Stage = '滴液'   order by CupNum  ;";
                    //DataTable dt_head_Drip2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);

                    //if (dt_head_Drip2.Rows.Count > 0)
                    //{
                    //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    //        FADM_Form.CustomMessageBox.Show("滴液过程不能插入", "温馨提示", MessageBoxButtons.OK, false);
                    //    else
                    //        FADM_Form.CustomMessageBox.Show("The liquid drop process cannot be interrupted", "Tips", MessageBoxButtons.OK, false);
                    //    return;
                    //}
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



                string s_sql = null;

                //获取批次的批次表头
                s_sql = "SELECT CupNum, FormulaCode, VersionNum, BatchName" +
                            " FROM drop_head Order BY CupNum ;";

                DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                List<int> lis_cup = new List<int>();

                for (int i = 0; i < dt_formulahead.Rows.Count; i++)
                {
                    //把正在滴液的排除
                    if (dt_formulahead.Rows[i][dt_formulahead.Columns["BatchName"]].ToString() != "0")
                    {
                        lis_cup.Add(Convert.ToInt32( dt_formulahead.Rows[i][0].ToString()));
                    }
                }

                if (dt_formulahead.Rows.Count > 0)
                {
                    int i_cupNum = 1;
                    //重新排杯号
                    for (int i = 0; i < dt_formulahead.Rows.Count; i++)
                    {
                        if (dt_formulahead.Rows[i][dt_formulahead.Columns["BatchName"]].ToString() != "0")
                            continue;

                        //得到以前的杯号
                        string s_oldCupNum = dt_formulahead.Rows[i][0].ToString();

                        string s_code = dt_formulahead.Rows[i][1].ToString();

                        string s_ver = dt_formulahead.Rows[i][2].ToString();
                        la_rep:
                        if(lis_cup.Contains(i_cupNum))
                        {
                            i_cupNum++;
                            goto la_rep;
                        }

                        //修改批次表头杯号
                        s_sql = "UPDATE drop_head SET" +
                                    " CupNum = '" + i_cupNum + "' where CupNum = '" + s_oldCupNum + "';";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        //修改批次详细表杯号
                        s_sql = "UPDATE drop_details SET" +
                                    " CupNum = '" + i_cupNum + "' where CupNum = '" + s_oldCupNum + "';";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        //杯号加1
                        i_cupNum++;
                    }
                }


                BatchHeadShow("");

            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "配液杯排序", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Sorting of dispensing cups", MessageBoxButtons.OK, false);
            }

        }

        //批次表成为活动控件事件
        private void dgv_BatchData_Enter(object sender, EventArgs e)
        {
            try
            {
                dgv_DropRecord.ClearSelection();
                if (this._b_frist)
                {
                    dgv_BatchData.ClearSelection();
                    this._b_frist = false;
                    dgv_FormulaBrowse.Focus();
                }
                else
                {
                    dgv_FormulaBrowse.ClearSelection();
                    dgv_FormulaBrowse.CurrentCell = null;
                    dgv_DropRecord.CurrentCell = null;

                }


            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "批次表成为活动控件事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Batch table becomes an active control event", MessageBoxButtons.OK, false);
            }
        }

        /// <summary>
        /// 显示配方浏览资料
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private void FormulaBrowseHeadShow(string s_formulaCode)
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
                    dgv_FormulaBrowse.RowsDefaultCellStyle.Font = new Font("宋体", 12.5F);
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
                dgv_FormulaBrowse.Columns[0].Width = 240;
                if (dgv_FormulaBrowse.Rows.Count > 21)
                {
                    dgv_FormulaBrowse.Columns[1].Width = 68;
                }
                else
                {
                    dgv_FormulaBrowse.Columns[1].Width = 88;
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
                if (s_formulaCode != "")
                {
                    for (int i = 0; i < dgv_FormulaBrowse.Rows.Count; i++)
                    {
                        string s = dgv_FormulaBrowse.Rows[i].Cells[0].Value.ToString();
                        if (s == s_formulaCode)
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

        //配方浏览表当前行改变事件
        private void dgv_FormulaBrowse_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {

                if (dgv_FormulaBrowse.CurrentRow != null)
                {
                    this.group.Text = "";
                    //设置矢能
                    Enabled_set();

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
                            chk_AddWaterChoose.Checked = (dt_formulahead.Rows[0][mDc].ToString() == "0" ? false : true);
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

                    //清理详细资料表
                    dgv_FormulaData.Rows.Clear();

                    //显示详细信息
                    for (int i = 0; i < dt_formuladetail.Rows.Count; i++)
                    {
                        dgv_FormulaData.Rows.Add(dt_formuladetail.Rows[i]["IndexNum"].ToString(),
                                                 dt_formuladetail.Rows[i]["AssistantCode"].ToString(),
                                                 dt_formuladetail.Rows[i]["AssistantName"].ToString(),
                                                 dt_formuladetail.Rows[i]["FormulaDosage"].ToString(),
                                                 dt_formuladetail.Rows[i]["UnitOfAccount"].ToString(),
                                                 null,
                                                 dt_formuladetail.Rows[i]["SettingConcentration"].ToString(),
                                                 dt_formuladetail.Rows[i]["RealConcentration"].ToString(),
                                                 dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString(),
                                                 dt_formuladetail.Rows[i]["RealDropWeight"].ToString(),
                                                 "0.00");

                        //显示瓶号
                        s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                    " FROM bottle_details WHERE" +
                                    " AssistantCode = '" + dgv_FormulaData[1, i].Value.ToString() + "'" +
                                    " AND RealConcentration != 0 Order BY BottleNum ;";
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
                        DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[11, i];
                        dc.Value = dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "0" ? 0 : 1;

                        DataGridViewTextBoxCell tx = (DataGridViewTextBoxCell)dgv_FormulaData[12, i];
                        tx.Value = "";
                    }
                    dgv_FormulaData.ClearSelection();

                }
                else
                {  //没有的话要把
                    //清理详细资料表
                    dgv_FormulaData.Rows.Clear();
                    this.txt_FormulaCode.Text = "";
                    this.txt_VersionNum.Text = "";
                    this.txt_State.Text = "";
                    this.txt_FormulaName.Text = "";
                    this.txt_ClothType.Text = "";
                    this.txt_Customer.Text = "";
                    this.txt_ClothWeight.Text = "";
                    this.txt_BathRatio.Text = "";
                    this.txt_TotalWeight.Text = "";
                    this.txt_Non_AnhydrationWR.Text = "";
                    this.txt_CupCode.Text = "";
                    this.txt_CreateTime.Text = "";


                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "配方浏览表当前行改变事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Recipe browsing table current row change event", MessageBoxButtons.OK, false);
            }
        }

        //配方浏览表成为活动控件事件
        private void dgv_FormulaBrowse_Enter(object sender, EventArgs e)
        {
            dgv_BatchData.ClearSelection();
            dgv_DropRecord.ClearSelection();
            dgv_BatchData.CurrentCell = null;
            dgv_DropRecord.CurrentCell = null;
            this._b_newAdd = false;
        }

        /// <summary>
        /// 显示滴液记录资料
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private void DropRecordHeadShow(string s_formulaCode)
        {
            try
            {
                string s_sql = null;
                DataTable dt_data = new DataTable();


                //获取配方浏览资料表头
                //if (rdo_Record_Now.Checked)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        s_sql = "SELECT FormulaCode, VersionNum, FinishTime, CupNum," +
                                " DescribeChar FROM history_head WHERE" +
                                " FinishTime > CONVERT(varchar,GETDATE(),23)";
                    }
                    else
                    {
                        s_sql = "SELECT FormulaCode, VersionNum, FinishTime, CupNum," +
                                " DescribeChar_EN FROM history_head WHERE" +
                                " FinishTime > CONVERT(varchar,GETDATE(),23)";
                    }
                    //if (txt_Record_Operator.Text != null && txt_Record_Operator.Text != "")
                    //{
                    //    P_str_sql = P_str_sql + " AND Operator = '" + txt_Record_Operator.Text + "' ";
                    //}


                    s_sql = s_sql + " ORDER BY MyID DESC";

                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                //else if (rdo_Record_All.Checked || txt_Record_Operator.Text.Equals("全部")) //只有主管有全部
                //{
                //    P_str_sql = "SELECT FormulaCode, VersionNum, FinishTime, CupNum," +
                //                " DescribeChar FROM history_head" +
                //                " WHERE FinishTime != '' ";
                //    if (txt_Record_Operator.Text != null && txt_Record_Operator.Text != "")
                //    {
                //        P_str_sql = P_str_sql + " AND Operator = '" + txt_Record_Operator.Text + "' ";
                //    }

                //    P_str_sql = P_str_sql + " ORDER BY MyID DESC";

                //    _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                //}
                //else
                //{
                //    string P_str = null;
                //    if (txt_Record_Operator.Text != null && txt_Record_Operator.Text != "")
                //    {
                //        P_str = (" Operator = '" + txt_Record_Operator.Text + "' AND");
                //    }
                //    if (txt_Record_Code.Text != null && txt_Record_Code.Text != "")
                //    {
                //        P_str += (" FormulaCode = '" + txt_Record_Code.Text + "' AND");
                //    }
                //    if (dt_Record_Start.Text != null && dt_Record_Start.Text != "")
                //    {
                //        P_str += (" FinishTime >= '" + dt_Record_Start.Text + "' AND");
                //    }
                //    else
                //    {
                //        return;
                //    }

                //    if (dt_Record_End.Text != null && dt_Record_End.Text != "")
                //    {
                //        P_str += (" FinishTime <= '" + dt_Record_End.Text + "' ");
                //    }
                //    else
                //    {
                //        return;
                //    }

                //    P_str_sql = "SELECT FormulaCode, VersionNum, FinishTime, CupNum," +
                //               " DescribeChar FROM history_head Where" + P_str + "" +
                //               " ORDER BY MyID DESC;";
                //    Console.WriteLine(P_str_sql);
                //    _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                //}

                this.label7.Text = Convert.ToString(dt_data.Rows.Count);
                string[] sa_array = s_sql.Split(new string[] { "ORDER" }, StringSplitOptions.None);
                string s_mySuccess = sa_array[0] + " AND DescribeChar like '%成功%' ;";
                DataTable dt_newdata = FADM_Object.Communal._fadmSqlserver.GetData(s_mySuccess);
                this.label9.Text = Convert.ToString(dt_newdata.Rows.Count);

                s_mySuccess = sa_array[0] + " AND DescribeChar like '%失败%' ;";
                dt_newdata = FADM_Object.Communal._fadmSqlserver.GetData(s_mySuccess);
                this.label15.Text = Convert.ToString(dt_newdata.Rows.Count);

                //捆绑
                dgv_DropRecord.DataSource = new DataView(dt_data);

                //设置标题文字
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    dgv_DropRecord.Columns[0].HeaderCell.Value = "配方代码";
                    dgv_DropRecord.Columns[1].HeaderCell.Value = "版本";
                    dgv_DropRecord.Columns[2].HeaderCell.Value = "时间/时期";
                    dgv_DropRecord.Columns[3].HeaderCell.Value = "杯位";
                    dgv_DropRecord.Columns[4].HeaderCell.Value = "描述";

                    //设置标题字体
                    dgv_DropRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);
                    //设置内容字体
                    dgv_DropRecord.RowsDefaultCellStyle.Font = new Font("宋体", 12.5F);
                }
                else
                {
                    dgv_DropRecord.Columns[0].HeaderCell.Value = "RecipeCode";
                    dgv_DropRecord.Columns[1].HeaderCell.Value = "Version";
                    dgv_DropRecord.Columns[2].HeaderCell.Value = "Time";
                    dgv_DropRecord.Columns[3].HeaderCell.Value = "CupNumber";
                    dgv_DropRecord.Columns[4].HeaderCell.Value = "Describe";

                    //设置标题字体
                    dgv_DropRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);
                    //设置内容字体
                    dgv_DropRecord.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                }


                //设置标题宽度
                dgv_DropRecord.Columns[0].Width = 250;
                dgv_DropRecord.Columns[1].Width = 80;
                dgv_DropRecord.Columns[2].Width = 300;
                dgv_DropRecord.Columns[3].Width = 80;
                if (dgv_FormulaData.Rows.Count > 5)
                {
                    dgv_DropRecord.Columns[4].Width = 625;
                }
                else
                {
                    dgv_DropRecord.Columns[4].Width = 655;
                }


                //关闭自动排序功能
                for (int i = 0; i < dgv_DropRecord.Columns.Count; i++)
                {
                    dgv_DropRecord.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }


                //设置标题居中显示
                dgv_DropRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                

                //设置内容居中显示
                dgv_DropRecord.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                

                //设置行高
                dgv_DropRecord.RowTemplate.Height = 30;

                //设置当前选中行
                if (s_formulaCode != "")
                {
                    for (int i = 0; i < dgv_DropRecord.Rows.Count; i++)
                    {
                        string s = dgv_DropRecord.Rows[i].Cells[0].Value.ToString();
                        if (s == s_formulaCode)
                        {
                            dgv_DropRecord.CurrentCell = dgv_DropRecord.Rows[i].Cells[0];
                            break;
                        }
                    }
                }

                //P_str_sql = "SELECT FinishTime FROM" +
                //                  " history_head Order BY FinishTime DESC ;";
                //_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                //if (_dt_data.Rows.Count > 0)
                //{
                //    try
                //    {
                //        dt_Record_Start.MinDate = Convert.ToDateTime(_dt_data.Rows[_dt_data.Rows.Count - 1][_dt_data.Columns[0]]);
                //        dt_Record_Start.MaxDate = Convert.ToDateTime(_dt_data.Rows[0][_dt_data.Columns[0]]);
                //        dt_Record_Start.Value = Convert.ToDateTime(_dt_data.Rows[_dt_data.Rows.Count - 1][_dt_data.Columns[0]]);
                //        dt_Record_End.MinDate = Convert.ToDateTime(_dt_data.Rows[_dt_data.Rows.Count - 1][_dt_data.Columns[0]]);
                //        dt_Record_End.MaxDate = Convert.ToDateTime(_dt_data.Rows[0][_dt_data.Columns[0]]);
                //        dt_Record_End.Value = Convert.ToDateTime(_dt_data.Rows[0][_dt_data.Columns[0]]);
                //    }
                //    catch
                //    {

                //    }
                //}

            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "显示滴液记录资料", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Display droplet record data", MessageBoxButtons.OK, false);
            }
        }

        //滴液记录表绑定数据事件
        private void dgv_DropRecord_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_DropRecord.ClearSelection();
        }

        //滴液记录表选择行事件
        private void dgv_DropRecord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                //设置矢能
                Enabled_set();

                //读取选中行对应的配方资料
                string s_formulaCode = dgv_DropRecord.CurrentRow.Cells[0].Value.ToString();
                string s_versionNum = dgv_DropRecord.CurrentRow.Cells[1].Value.ToString();
                string s_finishtime = dgv_DropRecord.CurrentRow.Cells[2].Value.ToString();
                string s_cup = dgv_DropRecord.CurrentRow.Cells[3].Value.ToString();
                string s_sql = "SELECT * FROM history_head" +
                                   " Where FormulaCode = '" + s_formulaCode + "'" +
                                   " AND VersionNum = '" + s_versionNum + "'" +
                                   " AND FinishTime = '" + s_finishtime + "'" +
                                   " AND CupNum = " + s_cup + ";";
                DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                s_sql = "SELECT * FROM history_details" +
                            " Where FormulaCode = '" + s_formulaCode + "'" +
                            " AND VersionNum = '" + s_versionNum + "'" +
                            " AND BatchName = '" + (dt_formulahead.Rows[0]
                            [dt_formulahead.Columns["BatchName"]]).ToString() + "'" +
                            " AND CupNum = " + s_cup + " order by IndexNum;";
                DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


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
                        chk_AddWaterChoose.Checked = (dt_formulahead.Rows[0][mDc].ToString() == "0" ? false : true);
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

                //清理详细资料表
                dgv_FormulaData.Rows.Clear();

                //显示详细信息
                for (int i = 0; i < dt_formuladetail.Rows.Count; i++)
                {
                    string s_str = (((Convert.ToDouble(dt_formuladetail.Rows[i]["RealDropWeight"].ToString()) - Convert.ToDouble(dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString())) / Convert.ToDouble(dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString())) * 100).ToString("F2");
                    dgv_FormulaData.Rows.Add(dt_formuladetail.Rows[i]["IndexNum"].ToString(),
                                             dt_formuladetail.Rows[i]["AssistantCode"].ToString(),
                                             dt_formuladetail.Rows[i]["AssistantName"].ToString(),
                                             dt_formuladetail.Rows[i]["FormulaDosage"].ToString(),
                                             dt_formuladetail.Rows[i]["UnitOfAccount"].ToString(),
                                             null,
                                             dt_formuladetail.Rows[i]["SettingConcentration"].ToString(),
                                             dt_formuladetail.Rows[i]["RealConcentration"].ToString(),
                                             dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString(),
                                             dt_formuladetail.Rows[i]["RealDropWeight"].ToString(),
                                             s_str);

                    //显示瓶号

                    s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv_FormulaData[1, i].Value.ToString() + "'" +
                                " AND RealConcentration != 0 Order BY BottleNum ;";
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
                                         " Mother liquor bottle number does not exist", "Tips", MessageBoxButtons.OK, false);
                    }


                    //显示是否手动选瓶
                    DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[11, i];
                    dc.Value = dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "0" ? 0 : 1;

                    DataGridViewTextBoxCell tx = (DataGridViewTextBoxCell)dgv_FormulaData[12, i];
                    tx.Value = dt_formuladetail.Rows[i]["BrewingData"].ToString();

                    //if (dt_formuladetail.Rows[i]["Finish"].ToString() == "1")
                    {
                        if (dt_formuladetail.Rows[i]["ObjectDropWeight"] is DBNull || dt_formuladetail.Rows[i]["RealDropWeight"] is DBNull)
                        {
                            dgv_FormulaData.Rows[i ].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else
                        {
                            double d_error = Lib_Card.Configure.Parameter.Other_AErr_Drip;
                            if (!(dt_formuladetail.Rows[i]["StandError"] is DBNull))
                            {
                                d_error = Convert.ToDouble(dt_formuladetail.Rows[i]["StandError"]);
                            }
                            if (Math.Abs(Convert.ToDouble(dt_formuladetail.Rows[i]["ObjectDropWeight"]) - Convert.ToDouble(dt_formuladetail.Rows[i]["RealDropWeight"])) > d_error)
                            {
                                dgv_FormulaData.Rows[i ].DefaultCellStyle.BackColor = Color.Red;
                            }
                        }
                    }
                }
                dgv_FormulaData.ClearSelection();

            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "滴液记录表选择行事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Droplet record table selection row event", MessageBoxButtons.OK, false);
            }
        }

        //滴液记录表成为活动控件事件
        private void dgv_DropRecord_Enter(object sender, EventArgs e)
        {
            dgv_BatchData.ClearSelection();
            dgv_FormulaBrowse.ClearSelection();
            dgv_FormulaBrowse.CurrentCell = null;
            dgv_BatchData.CurrentCell = null;
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
                if (this.txt_ClothWeight.Text != null && txt_ClothWeight.Text != "")
                {
                    double d_clothWeight = Convert.ToDouble(this.txt_ClothWeight.Text);
                    if (d_clothWeight > 50)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("注意,布重大于50克", "警告", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Note that the weight of the fabric is greater than 50 grams", "warn", MessageBoxButtons.OK, false);
                    }

                }
                if (txt_BathRatio.Text != null && txt_BathRatio.Text != "")
                {
                    //计算总浴量
                    txt_TotalWeight.Text = (Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_BathRatio.Text)).ToString();


                again:
                    foreach (DataGridViewRow dgvr in dgv_FormulaData.Rows)
                    {
                        for (int i = 0; i < dgv_FormulaData.Columns.Count - 2; i++)
                        {

                            if ((dgvr.Cells[i].Value == null || Convert.ToString(dgvr.Cells[i].Value) == "") && this.group.Text == "")
                            {
                                try
                                {
                                    dgv_FormulaData.Rows.Remove(dgvr);
                                    goto again;
                                }
                                catch
                                {
                                    break;
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
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "布重文本框离开事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Layout text box departure event", MessageBoxButtons.OK, false);
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
                        for (int i = 0; i < dgv_FormulaData.Columns.Count - 2; i++)
                        {

                            if ((dgvr.Cells[i].Value == null || Convert.ToString(dgvr.Cells[i].Value) == "") && this.group.Text == "")
                            {
                                try
                                {
                                    dgv_FormulaData.Rows.Remove(dgvr);
                                    goto again;
                                }
                                catch
                                {
                                    break;
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
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "浴比文本框离开事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Bathroom text box departure event", MessageBoxButtons.OK, false);
            }
        }


        //编辑单元格事件
        private void dgv_FormulaData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgv_FormulaData.CurrentCell.ColumnIndex == 5)
                {
                    ((DataGridViewComboBoxEditingControl)e.Control).SelectedValueChanged += Page_Formula_SelectedValueChanged;
                    ((DataGridViewComboBoxEditingControl)e.Control).Enter += new EventHandler(Page_Formula_Enter);
                    ((DataGridViewComboBoxEditingControl)e.Control).DropDown += Page_Formula_DropDown;
                    ((DataGridViewComboBoxEditingControl)e.Control).GotFocus += Page_Formula_DropDown;
                }
                if (dgv_FormulaData.CurrentCell.ColumnIndex == 3)
                {
                    ((DataGridViewTextBoxEditingControl)e.Control).KeyPress += Page_Formula_KeyPress;
                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "编辑单元格事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "EditingControlShowing", MessageBoxButtons.OK, false);
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
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "瓶号成为当前单元格事件", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "The bottle number becomes the current cell event", MessageBoxButtons.OK, false);
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
                            s_1 = dgv_FormulaData.CurrentCell.Value.ToString();
                            s_2 = dgv_FormulaData[1, dgv_FormulaData.CurrentRow.Index].Value.ToString();
                        }
                        catch
                        {

                        }
                        if (s_1 == null || s_2 == null)
                        {
                            btn_Save.Focus();
                        }

                    }

                }
            }
        }

        //离开当前行发生事件
        private void dgv_FormulaData_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (!(dgv_DropRecord.SelectedRows.Count > 0))
            {
                this.dgv_FormulaData.EndEdit();
                if (dgv_FormulaData[1, dgv_FormulaData.CurrentRow.Index].Value == null ||
                    dgv_FormulaData[3, dgv_FormulaData.CurrentRow.Index].Value == null)
                {
                    return;
                }
                UpdataFormulaData(dgv_FormulaData.CurrentRow.Index);
            }
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

                    //获取当前染助剂所有母液瓶资料
                    string s_sql = "SELECT BottleNum, SettingConcentration ,RealConcentration, DropMinWeight" +
                                       " FROM bottle_details WHERE" +
                                       " AssistantCode = '" + dgv_FormulaData.CurrentRow.Cells[1].Value.ToString() + "'" +
                                       " AND RealConcentration != 0 Order BY BottleNum ;";
                    DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    if (dt_bottlenum.Rows.Count > 0)
                    {
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



                        if (_lis_bottleNum[0] == dgv_FormulaData.CurrentRow.Index.ToString() && _lis_bottleNum[1] != dgv_FormulaData.CurrentRow.Cells[5].Value.ToString())
                        {
                            //设置手动选瓶标志位
                            dgv_FormulaData.CurrentRow.Cells[11].Value = 1;
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
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "瓶号选择修改事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Bottle number selection modification event", MessageBoxButtons.OK, false);
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
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "配方详细添加行事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "RowsAdded", MessageBoxButtons.OK, false);
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
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "配方详细表删除行事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "RowsRemoved", MessageBoxButtons.OK, false);

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
                if (c.Name.Equals("group"))
                {
                    continue;
                }
                if (c is TextBox || c is DataGridView || c is ComboBox)
                {
                    c.Enabled = false;
                }
            }
            txt_FormulaCode.Enabled = true;
            txt_FormulaCode.Focus();
            dgv_BatchData.ClearSelection();
            dgv_DropRecord.ClearSelection();

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
            if (FADM_Object.Communal._s_operator == "工程师" || FADM_Object.Communal._s_operator.Equals("主管") || FADM_Object.Communal._s_operator.Equals("管理用户"))
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("当前账号不能保存！", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("The current account cannot be saved！", "Tips", MessageBoxButtons.OK, false);
                return;
            }

            try
            {
                string s_maxVerNum = "";
                this.dgv_FormulaData.EndEdit();
                Dictionary<string, string> dic_mydic = new Dictionary<string, string>();
            again:
                foreach (DataGridViewRow dgvr in dgv_FormulaData.Rows)
                {
                    if (dgvr.Index < dgv_FormulaData.Rows.Count - 1)
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

                            if (dic_mydic.ContainsKey(dgvr.Cells[1].Value.ToString()))
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                {
                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                                                    "染助剂代码重复,请重新输入！", "输入异常", MessageBoxButtons.OK, false))
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(dgvr.Cells[1].Value.ToString() +
                                                                    "Dyeing agent code is duplicate, please re-enter！", "Input exception", MessageBoxButtons.OK, false))
                                    {
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                dic_mydic.Add(dgvr.Cells[1].Value.ToString(), dgvr.Cells[1].Value.ToString());
                            }

                        }
                    }
                    for (int i = 0; i < dgv_FormulaData.Columns.Count - 2; i++)
                    {

                        if (dgvr.Cells[i].Value == null || Convert.ToString(dgvr.Cells[i].Value) == "")
                        {
                            try
                            {
                                dgv_FormulaData.Rows.Remove(dgvr);
                                goto again;
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
                                dgv_FormulaBrowse.ClearSelection();
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("少于最低滴液量0.1，请核对配方！", "温馨提示", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("Less than the minimum droplet volume of 0.1, please verify the formula！", "Tips", MessageBoxButtons.OK, false);
                                return;
                            }
                        }
                    }
                }

                txt_ClothWeight_Leave(null, null);

                if (dgv_FormulaData.Rows.Count == 1)
                {
                    dgv_FormulaBrowse.ClearSelection();
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("当前为空配方,禁止保存!", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("The current formula is empty, saving is prohibited!", "Tips", MessageBoxButtons.OK, false);
                    dgv_FormulaBrowse_CurrentCellChanged(null, null);

                    return;
                }

                foreach (DataGridViewRow dgvr in dgv_FormulaData.Rows)
                {
                    UpdataFormulaData(dgvr.Index);
                }

                double d_allDropWeight = 0;

                string d_addWaterWeight = "0.00";
                string s_testTubeObjectAddWaterWeight = "0.00";
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
                    dgv_FormulaBrowse.ClearSelection();
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("总目标滴液量大于总浴量,请检查配方", "配方异常", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("The total target droplet volume is greater than the total bath volume, please check the formula", "Formula abnormality", MessageBoxButtons.OK, false);
                    return;
                }

                //设置创建时间
                txt_CreateTime.Text = System.DateTime.Now.ToString();

                if (String.IsNullOrEmpty(txt_Non_AnhydrationWR.Text)) //最开始含水比="" 
                {
                    txt_Non_AnhydrationWR.Text = "0";
                }
                //计算加水重量
                if (chk_AddWaterChoose.Checked)
                {
                    d_addWaterWeight = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", Convert.ToDouble(txt_TotalWeight.Text) - d_allDropWeight - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text)): String.Format("{0:F3}", Convert.ToDouble(txt_TotalWeight.Text) - d_allDropWeight - Convert.ToDouble(txt_ClothWeight.Text) * Convert.ToDouble(txt_Non_AnhydrationWR.Text));
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

                    }
                    else
                    {
                        // 添加配方
                        if ((txt_State.Text == "已滴定配方"|| txt_State.Text == "dropped") && txt_VersionNum.Text != "")
                        {
                            //搜索当前配方最大版本号
                            string s_sql = "SELECT VersionNum, State FROM formula_head WHERE" +
                                               " FormulaCode = '" + txt_FormulaCode.Text + "'" +
                                               " ORDER BY VersionNum DESC ;";
                            DataTable dt_ver = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                            if (dt_ver.Rows.Count == 0)
                            {
                                txt_VersionNum.Text = "0";

                            }
                            else
                            {
                                string s_versionNum = (Convert.ToInt16(dt_ver.Rows[0][dt_ver.Columns[0]])).ToString();
                                string s_state = dt_ver.Rows[0][dt_ver.Columns[1]].ToString();

                                if (txt_VersionNum.Text == s_versionNum && (s_state == "已滴定配方"|| s_state == "dropped"))
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

                                    s_sql = "DELETE FROM formula_head WHERE" +
                                             " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                             " VersionNum = '" + txt_VersionNum.Text + "' ;";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    s_maxVerNum = txt_VersionNum.Text;
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

                lab_ag1:
                    string s_sql_agaon = "SELECT *  FROM formula_head  WHERE" +
                                               " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                               " VersionNum = '" + txt_VersionNum.Text + "' ;";
                    DataTable dt_again = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_agaon);


                    if (dt_again.Rows.Count > 0)
                    {
                        string s_sql_q = "DELETE FROM formula_head WHERE" +
                                           " FormulaCode = '" + txt_FormulaCode.Text + "'" +
                                           " AND VersionNum = '" + txt_VersionNum.Text + "' ;";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_q);

                        goto lab_ag1;
                    }

                lab_ag2:
                    s_sql_agaon = "SELECT *  FROM formula_details  WHERE" +
                                               " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                               " VersionNum = '" + txt_VersionNum.Text + "' ;";
                    dt_again = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_agaon);


                    if (dt_again.Rows.Count > 0)
                    {
                        string s_sql_q = "DELETE FROM formula_details WHERE" +
                                           " FormulaCode = '" + txt_FormulaCode.Text + "'" +
                                           " AND VersionNum = '" + txt_VersionNum.Text + "' ;";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_q);

                        goto lab_ag2;
                    }


                    //string P_str_sql_3 = "SELECT BottleMinWeight FROM other_parameters WHERE MyID = 1;";
                    //DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql_3);

                    double d_bl_bottleAlarmWeight = Lib_Card.Configure.Parameter.Other_Bottle_MinWeight;

                    //P_str_sql_3 = "SELECT MachineType FROM machine_parameters WHERE MyID = 1;";
                    //_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql_3);

                    int P_int_MachineType = Lib_Card.Configure.Parameter.Machine_Bottle_Total;

                    string s_bottleLower = null;
                    string s_logPastDue = ""; //超过有效时间的染助剂
                    //添加进配方浏览详细表
                    foreach (DataGridViewRow dr in dgv_FormulaData.Rows)
                    {
                        if (dr.Index < dgv_FormulaData.RowCount - 1)
                        {
                            List<string> lis_detail = new List<string>();
                            lis_detail.Add(txt_FormulaCode.Text);
                            lis_detail.Add(txt_VersionNum.Text);
                            foreach (DataGridViewColumn dc in dgv_FormulaData.Columns)
                            {
                                try
                                {


                                    if (dc.Index == 11)
                                    {
                                        if (dgv_FormulaData[dc.Index, dr.Index].Value == null || dgv_FormulaData[dc.Index, dr.Index].Value.ToString() == "")
                                        {
                                            lis_detail.Add("0");
                                            continue;
                                        }
                                        lis_detail.Add(dgv_FormulaData[dc.Index, dr.Index].Value.ToString());
                                        continue;
                                    }
                                    else if (dc.Index != 10 && dc.Index != 12)
                                        lis_detail.Add(dgv_FormulaData[dc.Index, dr.Index].Value.ToString());
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
                                                 " BottleSelection) VALUES( '" + lis_detail[0] + "', '" + lis_detail[1] + "'," +
                                                 " '" + lis_detail[2] + "', '" + lis_detail[3] + "', '" + lis_detail[4] + "', '" + lis_detail[5] + "'," +
                                                 " '" + lis_detail[6] + "', '" + lis_detail[7] + "', '" + lis_detail[8] + "', '" + lis_detail[9] + "'," +
                                                 " '" + lis_detail[10] + "', '" + lis_detail[11] + "', '" + lis_detail[12] + "');";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);

                            if (Convert.ToInt16(lis_detail[7]) <= P_int_MachineType)
                            {

                                s_sql_0 = "SELECT CurrentWeight FROM bottle_details WHERE" +
                                              " BottleNum = '" + lis_detail[7] + "';";

                                DataTable dt_currentWeight = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_0);

                                double d_bl_CurrentWeight = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", dt_currentWeight.Rows[0][0]): string.Format("{0:F}", dt_currentWeight.Rows[0][0]));

                                if (d_bl_CurrentWeight <= d_bl_bottleAlarmWeight)
                                {
                                    s_bottleLower += (lis_detail[7] + " ");
                                }

                                s_sql_0 = "SELECT BrewingData FROM bottle_details WHERE" +
                                              " BottleNum = '" + lis_detail[7] + "';";

                                DataTable BrewingData = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_0);
                                DateTime brewTime = Convert.ToDateTime(BrewingData.Rows[0][0]);   //调液日期 
                                s_sql_0 = "SELECT TermOfValidity  FROM assistant_details WHERE" +
                                             " AssistantCode = '" + lis_detail[3] + "' ; ";
                                DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_0);
                                string s_termOfValidity = dt_assistant.Rows[0][0].ToString();//染助剂有效期限
                                //获取当前时间
                                DateTime timeNow = DateTime.Now;
                                //计算时间差
                                UInt32 timeDifference = Convert.ToUInt32(timeNow.Subtract(brewTime).Duration().TotalSeconds);

                                if (timeDifference > Convert.ToUInt32(s_termOfValidity) * 60 * 60)
                                {
                                    s_logPastDue += (lis_detail[7] + "  ");
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
                    //list_Head.Add(txt_Operator.Text);
                    lis_head.Add(FADM_Object.Communal._s_operator);
                    lis_head.Add(txt_CupCode.Text);
                    lis_head.Add(txt_CreateTime.Text);
                    lis_head.Add(d_addWaterWeight);
                    lis_head.Add(s_testTubeObjectAddWaterWeight);
                    if (string.IsNullOrEmpty(txt_Non_AnhydrationWR.Text))
                    {
                        lis_head.Add("0");
                    }
                    else
                    {
                        lis_head.Add(txt_Non_AnhydrationWR.Text);
                    }
                    lis_head.Add("滴液");

                    // 添加进配方浏览表头
                    string s_sql_1 = "INSERT INTO formula_head (" +
                                         " FormulaCode, VersionNum, State, FormulaName," +
                                         " ClothType,Customer,AddWaterChoose,CompoundBoardChoose,ClothWeight," +
                                         " BathRatio,TotalWeight,Operator,CupCode,CreateTime," +
                                         " ObjectAddWaterWeight,TestTubeObjectAddWaterWeight,Non_AnhydrationWR,Stage) VALUES('" + lis_head[0] + "'," +
                                         " '" + lis_head[1] + "', '" + lis_head[2] + "', '" + lis_head[3] + "', " +
                                         " '" + lis_head[4] + "', '" + lis_head[5] + "', '" + lis_head[6] + "', " +
                                         " '" + lis_head[7] + "', '" + lis_head[8] + "', '" + lis_head[9] + "', " +
                                         " '" + lis_head[10] + "', '" + lis_head[11] + "', '" + lis_head[12] + "', " +
                                         " '" + lis_head[13] + "', '" + lis_head[14] + "', '" + lis_head[15] + "', '" + lis_head[16] + "', '" + lis_head[17] + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                    //新增完上面这条，更新下最大时间 
                    string s_mysql = "SELECT CreateTime FROM" +
                                 " formula_head Order BY CreateTime DESC ;";
                    DataTable dt_formula_head = FADM_Object.Communal._fadmSqlserver.GetData(s_mysql);
                    if (dt_formula_head.Rows.Count > 0)
                    {
                        try
                        {
                            dt_Browse_Start.MinDate = Convert.ToDateTime(dt_formula_head.Rows[dt_formula_head.Rows.Count - 1][dt_formula_head.Columns[0]]);
                            dt_Browse_Start.MaxDate = Convert.ToDateTime(dt_formula_head.Rows[0][dt_formula_head.Columns[0]]);
                            dt_Browse_Start.Value = Convert.ToDateTime(dt_formula_head.Rows[dt_formula_head.Rows.Count - 1][dt_formula_head.Columns[0]]);
                            dt_Browse_End.MinDate = Convert.ToDateTime(dt_formula_head.Rows[dt_formula_head.Rows.Count - 1][dt_formula_head.Columns[0]]);
                            dt_Browse_End.MaxDate = Convert.ToDateTime(dt_formula_head.Rows[0][dt_formula_head.Columns[0]]);
                            dt_Browse_End.Value = Convert.ToDateTime(dt_formula_head.Rows[0][dt_formula_head.Columns[0]]);
                        }
                        catch
                        {
                        }
                    }

                    //更新配方浏览表
                    FormulaBrowseHeadShow(txt_FormulaCode.Text);


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
                            lab_ag:
                            //删除滴液详情表当前数据
                            s_sql_1 = "DELETE FROM drop_details WHERE" +
                                          " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                          " CupNum = " + s_cup + " ;";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                             s_sql_agaon = "SELECT *  FROM drop_details WHERE CupNum = '" + s_cup + "';";
                             dt_again = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_agaon);

                            if (dt_again.Rows.Count > 0)
                                goto lab_ag;


                            //添加进滴液详细表
                            foreach (DataGridViewRow dr in dgv_FormulaData.Rows)
                            {
                                if (dr.Index < dgv_FormulaData.RowCount - 1)
                                {
                                    List<string> lis_detail = new List<string>();
                                    lis_detail.Add(txt_FormulaCode.Text);
                                    lis_detail.Add(txt_VersionNum.Text);
                                    foreach (DataGridViewColumn dc in dgv_FormulaData.Columns)
                                    {
                                        try
                                        {
                                            if (dc.Index == 11)
                                            {
                                                if (dgv_FormulaData[dc.Index, dr.Index].Value == null || dgv_FormulaData[dc.Index, dr.Index].Value.ToString() == "")
                                                {
                                                    lis_detail.Add("0");
                                                    continue;
                                                }
                                                lis_detail.Add(dgv_FormulaData[dc.Index, dr.Index].Value.ToString());
                                                continue;
                                            }
                                            else if (dc.Index != 10 && dc.Index != 12)
                                                lis_detail.Add(dgv_FormulaData[dc.Index, dr.Index].Value.ToString());
                                        }
                                        catch
                                        {
                                            //存在空白行
                                            goto head;
                                        }
                                    }


                                    string s_sql_0 = "INSERT INTO drop_details ( CupNum," +
                                                         " FormulaCode, VersionNum, IndexNum, AssistantCode,AssistantName," +
                                                         " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                                         " RealConcentration,  ObjectDropWeight, RealDropWeight," +
                                                         " BottleSelection) VALUES( " + s_cup + ",'" + lis_detail[0] + "', '" + lis_detail[1] + "'," +
                                                         " '" + lis_detail[2] + "', '" + lis_detail[3] + "', '" + lis_detail[4] + "', '" + lis_detail[5] + "'," +
                                                         " '" + lis_detail[6] + "', '" + lis_detail[7] + "', '" + lis_detail[8] + "', '" + lis_detail[9] + "'," +
                                                         " '" + lis_detail[10] + "', '" + lis_detail[11] + "', '" + lis_detail[12] + "');";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);

                                }

                            }


                            //修改滴液头
                            string s_sql = "UPDATE drop_head SET" +
                                               " VersionNum = '" + lis_head[1] + "'," +
                                               " State = '" + lis_head[2] + "'," +
                                               " FormulaName = '" + lis_head[3] + "'," +
                                               " ClothType = '" + lis_head[4] + "'," +
                                               " Customer = '" + lis_head[5] + "'," +
                                               " AddWaterChoose ='" + lis_head[6] + "'," +
                                               " CompoundBoardChoose ='" + lis_head[7] + "'," +
                                               " ClothWeight ='" + lis_head[8] + "'," +
                                               " BathRatio ='" + lis_head[9] + "'," +
                                               " TotalWeight ='" + lis_head[10] + "'," +
                                               " Operator ='" + lis_head[11] + "'," +
                                               " CupCode ='" + lis_head[12] + "'," +
                                               " CreateTime ='" + lis_head[13] + "'," +
                                               " ObjectAddWaterWeight ='" + lis_head[14] + "'," +
                                               " TestTubeObjectAddWaterWeight ='" + lis_head[15] + "'," +
                                               " Non_AnhydrationWR ='" + lis_head[16] + "'" +
                                               " WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                               " CupNum = " + s_cup + " ;";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        }


                    }

                    if (FADM_Object.Communal._b_isLowDrip)
                    {
                        if (s_bottleLower != null) //安全存量真才会进行弹框
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show(s_bottleLower + "号母液瓶液量不足！", "母液量不足", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show(" Insufficient liquid volume in the " + s_bottleLower + " mother liquor bottle！", "Insufficient mother liquor volume", MessageBoxButtons.OK, false);
                        }
                    }
                    if (FADM_Object.Communal._b_isOutDrip)
                    {
                        //这里加个生命周期检查 过期了就提示
                        if (!string.IsNullOrEmpty(s_logPastDue))
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show(s_logPastDue + "号母液瓶过期！", "过期", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show(s_logPastDue + "mother liquor bottle expired！", "expire", MessageBoxButtons.OK, false);
                        }
                    }

                    //查询配方是否存在重复瓶号
                    string s_temp1;
                    s_temp1 = "SELECT FormulaCode,VersionNum,BottleNum FROM formula_details   where FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum=" + txt_VersionNum.Text + "  GROUP BY FormulaCode,VersionNum,BottleNum HAVING COUNT(*) > 1;  ";
                    DataTable dt_data_detail = FADM_Object.Communal._fadmSqlserver.GetData(s_temp1);
                    if (dt_data_detail.Rows.Count > 0)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("保存失败，请重新保存", "btn_Save_Click", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Failed to save. Please try again", "btn_Save_Click", MessageBoxButtons.OK, false);
                        return;
                    }

                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("保存完成", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Save completed", "Tips", MessageBoxButtons.OK, false);

                    //把当前配方之前版本移动到历史表
                    if (txt_VersionNum.Text != "0")
                    {
                        string s_sql = "SELECT FormulaCode,VersionNum  FROM formula_head WHERE FormulaCode = '" + txt_FormulaCode.Text + "' and  VersionNum  < " + txt_VersionNum.Text + ";";

                        DataTable dt_data_1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        List<string> lis_ver = new List<string>();

                        //查找滴液列表和等待列表，看看是否存在低版本配方，如果有就先不移除
                        string s_sql_drop = "SELECT VersionNum  FROM drop_head WHERE FormulaCode = '" + txt_FormulaCode.Text + "';";

                        DataTable dt_data_drop = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_drop);
                        foreach (DataRow dr in dt_data_drop.Rows)
                        {
                            lis_ver.Add(dr[0].ToString());
                        }

                        string s_sql_Wait = "SELECT VersionNum  FROM wait_list WHERE FormulaCode = '" + txt_FormulaCode.Text + "';";

                        DataTable dt_data_Wait = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_Wait);
                        foreach (DataRow dr in dt_data_Wait.Rows)
                        {
                            lis_ver.Add(dr[0].ToString());
                        }


                        foreach (DataRow dr in dt_data_1.Rows)
                        {
                            if (!lis_ver.Contains(dr["VersionNum"].ToString()))
                            {
                                string s_str;
                                s_str = "insert into formula_details_temp select * from formula_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                                s_str = "delete from  formula_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                                s_str = "insert into formula_handle_details_temp select * from formula_handle_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                                s_str = "delete from  formula_handle_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                                s_str = "insert into formula_head_temp select * from formula_head where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                                s_str = "delete from formula_head where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                            }
                        }
                    }

                    btn_FormulaCodeAdd_Click(null, null);
                    cup_sort();
                }
                else
                {
                    //当前焦点在批次表
                }

                //////对以前的批次要删掉 移到新的表
                //if (!string.IsNullOrEmpty(maxVerNum) && !maxVerNum.Equals("0"))
                //{
                //    string sql = "INSERT INTO bak_formula_head SELECT * FROM formula_head WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND VersionNum < " + maxVerNum + ";";
                //    FADM_Object.Communal._fadmSqlserver.ReviseData(sql);//直接复制过去
                //    sql = "INSERT INTO bak_formula_details select *  FROM formula_details WHERE" +
                //                        " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                //                        " VersionNum < '" + maxVerNum + "' ;";
                //    FADM_Object.Communal._fadmSqlserver.ReviseData(sql); //直接复制过去


                //    sql = "DELETE FROM formula_head WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND VersionNum < " + maxVerNum + ";";
                //    FADM_Object.Communal._fadmSqlserver.ReviseData(sql);//直接复制过去

                //    sql = "DELETE FROM  formula_details WHERE" +
                //                        " FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                //                        " VersionNum < '" + maxVerNum + "' ;";
                //    FADM_Object.Communal._fadmSqlserver.ReviseData(sql); //直接复制过去
                //    Console.WriteLine("13");

                //}



            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "保存点击事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Save Click Event", MessageBoxButtons.OK, false);
            }

        }

        //加入批次按下事件
        private void btn_BatchAdd_Click(object sender, EventArgs e)
        {
            string s_cup = null;
            try
            {
                //判断是否多选

                if (dgv_FormulaBrowse.SelectedRows.Count > 1)
                {
                    for (int i = 0; i < dgv_FormulaBrowse.SelectedRows.Count; i++)
                    {
                        //判断是否有空杯
                        string s_sql = "SELECT CupNum FROM drop_head WHERE FormulaCode IS NULL ORDER BY CupNum ;";
                        DataTable dt_newcup = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


                        //读取当前最大杯号
                        string s_maxCupNum = "0";
                        s_sql = "SELECT CupNum FROM drop_head Order BY CupNum DESC ;";
                        DataTable dt_cupnum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                        if (dt_cupnum.Rows.Count > 0)
                        {
                            s_maxCupNum = dt_cupnum.Rows[0][0].ToString();
                        }

                        DataTable dt_head = new DataTable();
                        DataTable dt_details = new DataTable();
                        //if (txt_FormulaCode.Text != null && txt_FormulaCode.Text != "" && txt_VersionNum.Text != null && txt_VersionNum.Text != "")

                        s_sql = "SELECT * FROM formula_head WHERE FormulaCode = '" + dgv_FormulaBrowse.SelectedRows[i].Cells[0].Value.ToString() + "' AND VersionNum = " + dgv_FormulaBrowse.SelectedRows[i].Cells[1].Value.ToString() + ";";

                        dt_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        if (dt_head.Rows.Count == 0)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                            return;
                        }

                        s_sql = "SELECT * FROM formula_details WHERE FormulaCode = '" + dgv_FormulaBrowse.SelectedRows[i].Cells[0].Value.ToString() + "' AND VersionNum = " + dgv_FormulaBrowse.SelectedRows[i].Cells[1].Value.ToString() + "  order by IndexNum;";
                        dt_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        if (dt_details.Rows.Count == 0)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                            return;
                        }



                        List<string> lis_head = new List<string>();
                        if (dt_newcup.Rows.Count == 0)
                        {
                            lis_head.Add((Convert.ToInt16(s_maxCupNum) + 1).ToString());
                        }
                        else
                        {
                            lis_head.Add(Convert.ToString(dt_newcup.Rows[0][0]));
                            //删除之前的新瓶号
                            s_sql = "DELETE FROM drop_head WHERE CupNum = '" + Convert.ToString(dt_newcup.Rows[0][0]) + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        lis_head.Add(dt_head.Rows[0]["FormulaCode"].ToString());
                        lis_head.Add(dt_head.Rows[0]["VersionNum"].ToString());
                        lis_head.Add(dt_head.Rows[0]["State"].ToString());
                        lis_head.Add(dt_head.Rows[0]["FormulaName"].ToString());
                        lis_head.Add(dt_head.Rows[0]["ClothType"].ToString());
                        lis_head.Add(dt_head.Rows[0]["Customer"].ToString());
                        lis_head.Add(dt_head.Rows[0]["AddWaterChoose"].ToString() == "0" ? "0" : "1");
                        lis_head.Add(dt_head.Rows[0]["CompoundBoardChoose"].ToString() == "0" ? "0" : "1");
                        lis_head.Add(dt_head.Rows[0]["ClothWeight"].ToString());
                        lis_head.Add(dt_head.Rows[0]["BathRatio"].ToString());
                        lis_head.Add(dt_head.Rows[0]["TotalWeight"].ToString());
                        lis_head.Add(dt_head.Rows[0]["Operator"].ToString());
                        lis_head.Add(dt_head.Rows[0]["CupCode"].ToString());
                        lis_head.Add(dt_head.Rows[0]["CreateTime"].ToString());
                        lis_head.Add(dt_head.Rows[0]["ObjectAddWaterWeight"].ToString());
                        lis_head.Add(dt_head.Rows[0]["TestTubeObjectAddWaterWeight"].ToString());
                        lis_head.Add(dt_head.Rows[0]["Non_AnhydrationWR"].ToString());
                        lis_head.Add("滴液");

                        s_cup = lis_head[0];

                        // 添加进批次表头
                        s_sql = "INSERT INTO drop_head (" +
                                    " CupNum, FormulaCode, VersionNum, State ,FormulaName, ClothType," +
                                    " Customer, AddWaterChoose, CompoundBoardChoose, ClothWeight, BathRatio, TotalWeight," +
                                    " Operator, CupCode, CreateTime, ObjectAddWaterWeight, TestTubeObjectAddWaterWeight,Non_AnhydrationWR,Stage) VALUES(" +
                                    " '" + lis_head[0] + "', '" + lis_head[1] + "', '" + lis_head[2] + "'," +
                                    " '" + lis_head[3] + "', '" + lis_head[4] + "', '" + lis_head[5] + "'," +
                                    " '" + lis_head[6] + "', '" + lis_head[7] + "', '" + lis_head[8] + "'," +
                                    " '" + lis_head[9] + "', '" + lis_head[10] + "', '" + lis_head[11] + "'," +
                                    " '" + lis_head[12] + "', '" + lis_head[13] + "', '" + lis_head[14] + "'," +
                                    " '" + lis_head[15] + "','" + lis_head[16] + "','" + lis_head[17] + "','" + lis_head[18] + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);


                        //添加进批次详细表
                        foreach (DataRow dr in dt_details.Rows)
                        {

                            List<string> lis_detail = new List<string>();
                            if (dt_newcup.Rows.Count == 0)
                            {
                                lis_detail.Add((Convert.ToInt16(s_maxCupNum) + 1).ToString());
                            }
                            else
                            {
                                lis_detail.Add(Convert.ToString(dt_newcup.Rows[0][0]));
                            }

                            foreach (DataColumn dc in dt_details.Columns)
                            {
                                if (dc.ColumnName == "BottleSelection")
                                {
                                    lis_detail.Add((dr[dc]).ToString() == "0" ? "0" : "1");
                                    continue;
                                }
                                lis_detail.Add((dr[dc]).ToString());
                            }

                            //if (Class_Module.MyModule.BalanceType.Equals("rb"))
                            //{
                            //    //添加进滴液详细表
                            //    P_str_sql = "INSERT INTO drop_details (" +
                            //                " CupNum, FormulaCode, VersionNum, IndexNum, AssistantCode," +
                            //                " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                            //                " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                            //                " BottleSelection) VALUES( '" + list_Detail[0] + "', '" + list_Detail[1] + "'," +
                            //                " '" + list_Detail[2] + "', '" + list_Detail[3] + "', '" + list_Detail[4] + "'," +
                            //                " '" + list_Detail[5] + "', '" + list_Detail[6] + "', '" + list_Detail[7] + "'," +
                            //                " '" + list_Detail[8] + "', '" + list_Detail[9] + "', '" + list_Detail[10] + "'," +
                            //                " '" + list_Detail[11] + "', '" + string.Format("{0:F3}", 0) + "', '" + list_Detail[13] + "');";
                            //    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                            //}
                            //else
                            {
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







                        }

                        //更新批次表
                        BatchHeadShow((Convert.ToInt16(s_maxCupNum) + 1).ToString());

                        if (this._b_newAdd)
                        {
                            btn_FormulaCodeAdd_Click(null, null);
                        }
                    }

                }
                else
                {
                    if (dgv_FormulaBrowse.SelectedRows.Count != 0 &&
                        dgv_FormulaBrowse.CurrentRow.Cells[0].Value.ToString() == txt_FormulaCode.Text &&
                        dgv_FormulaBrowse.CurrentRow.Cells[1].Value.ToString() == txt_VersionNum.Text)
                    {
                        //判断是否有空杯
                        string s_sql = "SELECT CupNum FROM drop_head WHERE FormulaCode IS NULL ORDER BY CupNum ;";
                        DataTable dt_newcup = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


                        //读取当前最大杯号
                        string s_maxCupNum = "0";
                        s_sql = "SELECT CupNum FROM drop_head Order BY CupNum DESC ;";
                        DataTable dt_cupnum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                        if (dt_cupnum.Rows.Count > 0)
                        {
                            s_maxCupNum = dt_cupnum.Rows[0][0].ToString();
                        }
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

                            if (dt_details.Rows.Count == 0)
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                                return;
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
                        if (dt_newcup.Rows.Count == 0)
                        {
                            lis_head.Add((Convert.ToInt16(s_maxCupNum) + 1).ToString());
                        }
                        else
                        {
                            lis_head.Add(Convert.ToString(dt_newcup.Rows[0][0]));
                            //删除之前的新瓶号
                            s_sql = "DELETE FROM drop_head WHERE CupNum = '" + Convert.ToString(dt_newcup.Rows[0][0]) + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        lis_head.Add(dt_head.Rows[0]["FormulaCode"].ToString());
                        lis_head.Add(dt_head.Rows[0]["VersionNum"].ToString());
                        lis_head.Add(dt_head.Rows[0]["State"].ToString());
                        lis_head.Add(dt_head.Rows[0]["FormulaName"].ToString());
                        lis_head.Add(dt_head.Rows[0]["ClothType"].ToString());
                        lis_head.Add(dt_head.Rows[0]["Customer"].ToString());
                        lis_head.Add(dt_head.Rows[0]["AddWaterChoose"].ToString() == "0" ? "0" : "1");
                        lis_head.Add(dt_head.Rows[0]["CompoundBoardChoose"].ToString() == "0" ? "0" : "1");
                        lis_head.Add(dt_head.Rows[0]["ClothWeight"].ToString());
                        lis_head.Add(dt_head.Rows[0]["BathRatio"].ToString());
                        lis_head.Add(dt_head.Rows[0]["TotalWeight"].ToString());
                        lis_head.Add(dt_head.Rows[0]["Operator"].ToString());
                        lis_head.Add(dt_head.Rows[0]["CupCode"].ToString());
                        lis_head.Add(dt_head.Rows[0]["CreateTime"].ToString());
                        lis_head.Add(dt_head.Rows[0]["ObjectAddWaterWeight"].ToString());
                        lis_head.Add(dt_head.Rows[0]["TestTubeObjectAddWaterWeight"].ToString());
                        lis_head.Add(dt_head.Rows[0]["Non_AnhydrationWR"].ToString());
                        lis_head.Add("滴液");

                        s_cup = lis_head[0];

                        // 添加进批次表头
                        s_sql = "INSERT INTO drop_head (" +
                                    " CupNum, FormulaCode, VersionNum, State ,FormulaName, ClothType," +
                                    " Customer, AddWaterChoose, CompoundBoardChoose, ClothWeight, BathRatio, TotalWeight," +
                                    " Operator, CupCode, CreateTime, ObjectAddWaterWeight, TestTubeObjectAddWaterWeight,Non_AnhydrationWR,Stage) VALUES(" +
                                    " '" + lis_head[0] + "', '" + lis_head[1] + "', '" + lis_head[2] + "'," +
                                    " '" + lis_head[3] + "', '" + lis_head[4] + "', '" + lis_head[5] + "'," +
                                    " '" + lis_head[6] + "', '" + lis_head[7] + "', '" + lis_head[8] + "'," +
                                    " '" + lis_head[9] + "', '" + lis_head[10] + "', '" + lis_head[11] + "'," +
                                    " '" + lis_head[12] + "', '" + lis_head[13] + "', '" + lis_head[14] + "'," +
                                    " '" + lis_head[15] + "','" + lis_head[16] + "','" + lis_head[17] + "','" + lis_head[18] + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);


                        //添加进批次详细表
                        foreach (DataRow dr in dt_details.Rows)
                        {

                            List<string> lis_detail = new List<string>();
                            if (dt_newcup.Rows.Count == 0)
                            {
                                lis_detail.Add((Convert.ToInt16(s_maxCupNum) + 1).ToString());
                            }
                            else
                            {
                                lis_detail.Add(Convert.ToString(dt_newcup.Rows[0][0]));
                            }

                            foreach (DataColumn dc in dt_details.Columns)
                            {
                                if (dc.ColumnName == "BottleSelection")
                                {
                                    lis_detail.Add((dr[dc]).ToString() == "0" ? "0" : "1");
                                    continue;
                                }
                                lis_detail.Add((dr[dc]).ToString());
                            }


                            //if (Class_Module.MyModule.BalanceType.Equals("rb"))
                            //{
                            //    //添加进滴液详细表
                            //    P_str_sql = "INSERT INTO drop_details (" +
                            //                " CupNum, FormulaCode, VersionNum, IndexNum, AssistantCode," +
                            //                " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                            //                " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                            //                " BottleSelection) VALUES( '" + list_Detail[0] + "', '" + list_Detail[1] + "'," +
                            //                " '" + list_Detail[2] + "', '" + list_Detail[3] + "', '" + list_Detail[4] + "'," +
                            //                " '" + list_Detail[5] + "', '" + list_Detail[6] + "', '" + list_Detail[7] + "'," +
                            //                " '" + list_Detail[8] + "', '" + list_Detail[9] + "', '" + list_Detail[10] + "'," +
                            //                " '" + list_Detail[11] + "', '" + string.Format("{0:F3}", 0) + "', '" + list_Detail[13] + "');";
                            //    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                            //}
                            //else
                            {
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






                        }

                        //更新批次表
                        BatchHeadShow((Convert.ToInt16(s_maxCupNum) + 1).ToString());

                        if (this._b_newAdd)
                        {
                            btn_FormulaCodeAdd_Click(null, null);
                        }
                    }
                    else
                    {
                        if (dgv_FormulaBrowse.SelectedRows.Count != 0)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("请先保存,然后加入批次", "操作异常", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Please save first and then add to the batch", "Abnormal operation", MessageBoxButtons.OK, false);
                            return;
                        }
                        else
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("请勿在批次表中或历史记录表点击加入批次", "操作异常", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Do not click to add batch in the batch table or history table", "Abnormal operation", MessageBoxButtons.OK, false);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    FADM_Form.CustomMessageBox.Show("当前配方存在空瓶号，请检查再加入！", "温馨提示", MessageBoxButtons.OK, false);
                    FADM_Form.CustomMessageBox.Show(ex.Message, "温馨提示", MessageBoxButtons.OK, false);
                    if (s_cup != null)
                    {
                        string s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        BatchHeadShow("");
                    }
                }
                else
                {
                    FADM_Form.CustomMessageBox.Show("The current formula has an empty bottle number. Please check before adding it！", "Tips", MessageBoxButtons.OK, false);
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Tips", MessageBoxButtons.OK, false);
                    if (s_cup != null)
                    {
                        string s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_cup + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_cup + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        BatchHeadShow("");
                    }
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
                            if (c.Name.Equals("group"))
                            {
                                continue;
                            }
                            if ((c is TextBox || c is CheckBox || c is ComboBox) && c.Name == s_name)
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
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "矢能设置", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Arrow energy setting", MessageBoxButtons.OK, false);
            }
        }

        /// <summary>
        /// 更新配方表
        /// </summary>
        /// <param name="_CurrentRowIndex">当前行号</param>
        private void UpdataFormulaData(int _CurrentRowIndex)
        {
            try
            {

                DataTable dt_bottlenum = new DataTable();

                if (_CurrentRowIndex >= dgv_FormulaData.Rows.Count - 1)
                {
                    return;
                }
                if (this.group.Text != "" && dgv_FormulaData[3, _CurrentRowIndex].Value == null)
                {
                    return;
                }

                //if (txt_VersionNum.Text == "")
                //{
                //    return;
                //}
                string s_sql = null;
                if (txt_VersionNum.Text != null)
                {
                    s_sql = "SELECT *  FROM formula_details WHERE FormulaCode = '" + txt_FormulaCode.Text + "' AND" +
                                " VersionNum = '" + txt_VersionNum.Text + "' AND IndexNum ='" + dgv_FormulaData[0, _CurrentRowIndex].Value.ToString() + "'  order by IndexNum; ";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    if (dt_data.Rows.Count > 0)
                    {
                        string s_code = (dt_data.Rows[0][dt_data.Columns["AssistantCode"]]).ToString();
                        if (s_code != dgv_FormulaData[1, _CurrentRowIndex].Value.ToString())
                        {

                            dgv_FormulaData[10, _CurrentRowIndex].Value = 0;
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
                    dgv_FormulaData[10, _CurrentRowIndex].Value = "0.00";
                    //获取当前染助剂所有母液瓶资料
                    s_sql = "SELECT BottleNum, SettingConcentration ,RealConcentration, DropMinWeight" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv_FormulaData[1, _CurrentRowIndex].Value.ToString() + "'" +
                                " AND RealConcentration != 0 Order BY BottleNum ;";
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
                    s_sql = "SELECT BottleNum, SettingConcentration, RealConcentration, DropMinWeight" +
                                " FROM bottle_details WHERE" +
                                " AssistantCode = '" + dgv_FormulaData[1, _CurrentRowIndex].Value.ToString() + "'" +
                                " AND RealConcentration != 0 Order BY SettingConcentration DESC;";

                    dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    for (int i = 0; i < dt_bottlenum.Rows.Count; i++)
                    {
                        double d_objectDropWeight = 0;
                        //判断是否需要自动选瓶
                        if (dgv_FormulaData[11, _CurrentRowIndex].Value == null ||
                            dgv_FormulaData[11, _CurrentRowIndex].Value.ToString() == "0")
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
                                    dgv_FormulaData[8, _CurrentRowIndex].Value = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_objectDropWeight) : String.Format("{0:F3}", d_objectDropWeight);

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
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                FADM_Form.CustomMessageBox.Show("第" + dgv_FormulaData[0, _CurrentRowIndex].Value.ToString() + "行目标滴液量小于0.1!","温馨提示", MessageBoxButtons.OK, false);
                                            else
                                                FADM_Form.CustomMessageBox.Show( "The target droplet volume in line "+dgv_FormulaData[0, _CurrentRowIndex].Value.ToString() +"  is less than 0.1!", "温馨提示", MessageBoxButtons.OK, false);

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
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "更新配方表", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Update recipe table", MessageBoxButtons.OK, false);
            }
        }

        private void txt_StartCup_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberTextbox_KeyPress(e);
        }

        //开始滴液按钮点击事件
        private void btn_Start_Click(object sender, EventArgs e)
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
                //FADM_Form.CustomMessageBox.Show("当前滴液完成杯号" + strCup, "温馨提示", MessageBoxButtons.OK, false);

            }
            //获取现在机台状态，判断是否存在正在滴液流程的记录
            string s_sql = "SELECT * FROM drop_head WHERE Step = 1;";
            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_data.Rows.Count > 0)
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
                dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                string s_batchNum_last = Convert.ToString(dt_data.Rows[0][dt_data.Columns[0]]);

                //计算当前批次号
                string s_batchNum = null;
                if (s_batchNum_last == "0")
                {
                    //初始状态
                    int i_no = 1;
                    s_batchNum = DateTime.Now.ToString("yyyyMMdd") + i_no.ToString("d4");
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

                        if (i_cup <= Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {

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

                }

                else
                {
                    //选择所有杯

                    //写入批次号

                    //修改批次表头批次号
                    s_sql = "UPDATE drop_head SET BatchName = '" + s_batchNum + "', State = '已滴定配方',Step = 1 where  BatchName = '0' and  CupNum <=" + Lib_Card.Configure.Parameter.Machine_Cup_Total + ";";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    //修改批次详细资料表批次号
                    s_sql = "UPDATE drop_details SET BatchName = '" + s_batchNum + "' where  BatchName = '0' and  CupNum <="+ Lib_Card.Configure.Parameter.Machine_Cup_Total+";";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    s_sql = "UPDATE dye_details SET BatchName = '" + s_batchNum + "' where  BatchName = '0' and  CupNum <=" + Lib_Card.Configure.Parameter.Machine_Cup_Total + ";";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    foreach (DataGridViewRow dr in dgv_BatchData.Rows)
                    {

                        int i_cup = Convert.ToInt16(dgv_BatchData[0, dr.Index].Value);
                        string s_code = Convert.ToString(dgv_BatchData[1, dr.Index].Value);
                        string s_ver = Convert.ToString(dgv_BatchData[2, dr.Index].Value);

                        if (i_cup <= Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            //写入配方浏览表
                            s_sql = "UPDATE formula_head SET State = '已滴定配方'" +
                                   " WHERE FormulaCode = '" + s_code + "' AND" +
                                   " VersionNum = " + s_ver + " ;";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                    }
                }

                BatchHeadShow("");

                //Thread P_thd_drop_a = new Thread(drop_liquid_a);
                //P_thd_drop_a.IsBackground = true;
                //P_thd_drop_a.Start(s_batchNum);
                //FADM_Object.Communal._b_isDripping = false;
                //return;
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

        private void drop_liquid_a(object oBatchNum)
        {
            //滴液
            SmartDyeing.FADM_Auto.MyPowder.powder(oBatchNum.ToString());

        }

        private void mydrop_liquid(object oBatchNum)
        {

            new FADM_Auto.Drip().DripLiquid(oBatchNum);

        }

        ///// <summary>
        ///// 判断是否重滴
        ///// </summary>
        ///// <param name="P_str_batchnum">批次号</param>
        //private void drop_again(string P_str_batchnum)
        //{
        //    //更新完成时间
        //    string P_str_sql = "UPDATE history_head SET FinishTime =" +
        //                       " '" + System.DateTime.Now.ToString() + "' WHERE BatchName =" +
        //                       "'" + P_str_batchnum + "';";
        //    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

        //    //获取滴液允许误差和称粉允许误差和加水允许误差
        //    P_str_sql = "SELECT DropAllowError, AddWaterAllowError, AddPowderAllowError FROM" +
        //                " other_parameters WHERE MyID = 1;";

        //    DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

        //    double P_dbl_drop_allow_err = Convert.ToDouble(_dt_data.Rows[0][_dt_data.Columns[0]]);
        //    int P_int_watrt_allow_err = 8;// Convert.ToInt16(_dt_data.Rows[0][_dt_data.Columns[1]]);
        //    double P_dbl_powder_allow_err = Convert.ToDouble(_dt_data.Rows[0][_dt_data.Columns[2]]);

        //    P_str_sql = "SELECT MachineType FROM machine_parameters WHERE MyID = 1;";
        //    _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
        //    int P_int_machine = Convert.ToInt16(_dt_data.Rows[0][_dt_data.Columns[0]]);


        //    //添加描述

        //    //获取滴液不合格的杯号
        //    P_str_sql = "SELECT CupNum FROM drop_details WHERE" +
        //                " BatchName = '" + P_str_batchnum + "' AND" +
        //                " ((ROUND(ABS(ObjectDropWeight * 100 - RealDropWeight * 100),2) > " + P_dbl_drop_allow_err * 100 + " AND" +
        //                " BottleNum > 0 AND BottleNum <= " + P_int_machine + ") OR" +
        //                " (ROUND(ABS(ObjectDropWeight * 100 - RealDropWeight * 100),2) > " + P_dbl_powder_allow_err * 100 + " AND" +
        //                " (BottleNum = 200 OR BottleNum = 201))) GROUP BY CupNum;";
        //    _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);


        //    P_str_sql = "SELECT * FROM drop_head WHERE" +
        //                " BatchName = '" + P_str_batchnum + "';";
        //    DataTable P_dt_allcup = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

        //    string P_str_Describe = "";
        //    List<int> P_list_errCup = new List<int>();
        //    foreach (DataRow dr in P_dt_allcup.Rows)
        //    {
        //        int P_int_cup = Convert.ToInt16(dr["CupNum"]);
        //        double P_dbl_objWater = Convert.ToDouble(dr["ObjectAddWaterWeight"]);
        //        double P_dbl_realWater = Convert.ToDouble(dr["RealAddWaterWeight"]);
        //        double TotalWeight = Convert.ToDouble(dr["TotalWeight"]);
        //        double P_dbl_TestTubeObjectAddWaterWeight = Convert.ToDouble(dr["TestTubeObjectAddWaterWeight"]);
        //        double TestTubeRealAddWaterWeight = Convert.ToDouble(dr["TestTubeRealAddWaterWeight"]);

        //        double P_dbl_realDif = Convert.ToDouble( string.Format("{0:F}", P_dbl_realWater - P_dbl_objWater));
        //        P_dbl_realDif = P_dbl_realDif < 0 ? -P_dbl_realDif : P_dbl_realDif;
        //        double P_dbl_TestTube_err = Convert.ToDouble( string.Format("{0:F}", P_dbl_TestTubeObjectAddWaterWeight - TestTubeRealAddWaterWeight));

        //        P_dbl_TestTube_err = P_dbl_TestTube_err < 0 ? -P_dbl_TestTube_err : P_dbl_TestTube_err;


        //        double P_dbl_allDif = Convert.ToDouble( string.Format("{0:F}", P_dbl_objWater * Convert.ToDouble(P_int_watrt_allow_err / 100.00)));

        //        double P_dbl_water_allow_err = Convert.ToDouble(string.Format("{0:F}", P_int_watrt_allow_err));

        //        double P_dbl_water_real_err = Convert.ToDouble( string.Format("{0:F}", P_dbl_realDif / TotalWeight * 100));

        //        if (P_dbl_water_real_err > P_dbl_water_allow_err || P_dbl_TestTube_err > P_dbl_drop_allow_err)
        //        {
        //            //if (Class_Module.MyModule.BalanceType.Equals("rb"))
        //            //{
        //            //    P_str_Describe = "滴液失败!目标加水:" + string.Format("{0:F3}", P_dbl_objWater) +
        //            //                 ",实际加水:" + string.Format("{0:F3}", P_dbl_realWater);
        //            //    P_list_errCup.Add(P_int_cup);
        //            //}
        //            //else
        //            {
        //                P_str_Describe = "滴液失败!目标加水:" + string.Format("{0:F}", P_dbl_objWater) +
        //                                ",实际加水:" + string.Format("{0:F}", P_dbl_realWater);
        //                P_list_errCup.Add(P_int_cup);
        //            }

        //        }
        //        else
        //        {
        //            //加水成功

        //            bool P_bl_fail = false;
        //            foreach (DataRow dr1 in _dt_data.Rows)
        //            {
        //                int P_int_errCup = Convert.ToInt16(dr1["CupNum"]);
        //                if (P_int_cup == P_int_errCup)
        //                {
        //                    //滴液失败
        //                    P_bl_fail = true;
        //                    break;
        //                }
        //            }
        //            if (P_bl_fail)
        //            {
        //                if (Class_Module.MyModule.BalanceType.Equals("rb"))
        //                {
        //                    P_str_Describe = "滴液失败!目标加水:" + string.Format("{0:F3}", P_dbl_objWater) +
        //                                     ",实际加水:" + string.Format("{0:F3}", P_dbl_realWater);
        //                    P_list_errCup.Add(P_int_cup);
        //                }
        //                else
        //                {
        //                    P_str_Describe = "滴液失败!目标加水:" + string.Format("{0:F}", P_dbl_objWater) +
        //                                    ",实际加水:" + string.Format("{0:F}", P_dbl_realWater);
        //                    P_list_errCup.Add(P_int_cup);
        //                }

        //            }
        //            else
        //            {
        //                if (Class_Module.MyModule.BalanceType.Equals("rb"))
        //                {
        //                    P_str_Describe = "滴液成功!目标加水:" + string.Format("{0:F3}", P_dbl_objWater) +
        //                                     ",实际加水:" + string.Format("{0:F3}", P_dbl_realWater);
        //                }
        //                else
        //                {
        //                    P_str_Describe = "滴液成功!目标加水:" + string.Format("{0:F}", P_dbl_objWater) +
        //                                         ",实际加水:" + string.Format("{0:F}", P_dbl_realWater);
        //                }

        //            }

        //        }
        //        P_str_sql = "UPDATE history_head SET DescribeChar" +
        //                    " = '" + P_str_Describe + "' WHERE BatchName = " +
        //                    "'" + P_str_batchnum + "' AND CupNum = " + P_int_cup + ";";
        //        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
        //    }

        //    P_bl_update = true;

        //    if (Class_Module.MyModule.Module_Stop == false)
        //    {
        //        //有滴液错误的杯号
        //        if (P_list_errCup.Count > 0)
        //        {
        //            string P_str_errCup = null;
        //            foreach (int i in P_list_errCup)
        //            {
        //                P_str_errCup += (i + ",");
        //            }

        //            P_str_errCup = P_str_errCup.Remove(P_str_errCup.Length - 1);

        //            DialogResult dialogResult = FADM_Form.CustomMessageBox.Show(P_str_errCup + "号杯滴液失败，是否重滴？", "滴液失败", MessageBoxButtons.YesNo, true);

        //            if (dialogResult == DialogResult.Yes)
        //            {
        //                //需要重滴
        //                foreach (int i in P_list_errCup)
        //                {
        //                    P_str_sql = "UPDATE drop_head SET CupFinish = 0," +
        //                                " AddWaterFinish = 0, RealAddWaterWeight = 0.00," +
        //                                " TestTubeFinish = 0,TestTubeRealAddWaterWeight = 0," +
        //                                " TestTubeWaterLower = 0  WHERE BatchName =" +
        //                                " '" + P_str_batchnum + "' AND CupNum = " + i + ";";
        //                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

        //                    P_str_sql = "UPDATE drop_details SET Finish = 0," +
        //                               " MinWeight = 0, RealDropWeight = 0.00  WHERE BatchName =" +
        //                               " '" + P_str_batchnum + "' AND CupNum = " + i + ";";
        //                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

        //                    P_str_sql = "UPDATE history_head SET DescribeChar = NULL" +
        //                                " WHERE BatchName = '" + P_str_batchnum + "' AND CupNum = " + i + ";";
        //                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
        //                }

        //                Class_Auto.MyDrop_Liquid.Drop_Finish = false;
        //                Class_Module.MyModule.Module_State = 7;
        //                drop_liquid();
        //            }
        //            else
        //            {

        //                Class_Auto.MyDrop_Liquid.Drop_List.Remove(P_str_batchnum);
        //                new Class_SemiAuto.MyErr().err();

        //            }

        //        }
        //        else
        //        {
        //            if (DialogResult.OK == FADM_Form.CustomMessageBox.Show(P_str_batchnum + "批次完成", "温馨提示"))
        //            {
        //                Class_Auto.MyDrop_Liquid.Drop_List.Remove(P_str_batchnum);
        //            }
        //        }

        //        Class_Module.MyModule.Module_State = 0;
        //    }

        //}

        //停止滴液按钮点击事件
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            btn_Stop.Enabled = false;

            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                  "SELECT * FROM drop_head WHERE BatchName IN (SELECT BatchName FROM enabled_set WHERE MyID = 1);");
            if (0 < dt_data.Rows.Count)
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

                            //DataTable dataTable1 = FADM_Object.Communal._fadmSqlserver.GetData(
                            //"SELECT BatchName FROM enabled_set WHERE MyID = 1;");
                            //FADM_Auto.Reset.MoveData(dataTable1.Rows[0]["BatchName"].ToString());
                            P_bl_update = true;
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

                            //DataTable dataTable1 = FADM_Object.Communal._fadmSqlserver.GetData(
                            //"SELECT BatchName FROM enabled_set WHERE MyID = 1;");
                            //FADM_Auto.Reset.MoveData(dataTable1.Rows[0]["BatchName"].ToString());
                            P_bl_update = true;
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
                            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(
                            "SELECT BatchName FROM enabled_set WHERE MyID = 1;");
                            FADM_Auto.Reset.MoveData(dt_data1.Rows[0]["BatchName"].ToString());
                            P_bl_update = true;
                        }
                    }
                    else
                    {
                        //用于异常退出,清除数据用
                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Are you sure you want to clear the current batch data?", "Stop dripping liquid", MessageBoxButtons.YesNo, true);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(
                            "SELECT BatchName FROM enabled_set WHERE MyID = 1;");
                            FADM_Auto.Reset.MoveData(dt_data1.Rows[0]["BatchName"].ToString());
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
        static Thread thread = null;
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
            //DataTable dataTable = FADM_Object.Communal._fadmSqlserver.GetData(
            //   "SELECT * FROM drop_head WHERE BatchName IN (SELECT BatchName FROM enabled_set WHERE MyID = 1);");
            //foreach (DataRow dataRow in dataTable.Rows)
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
            //if (Class_Module.MyModule.Module_ConNum != 4)
            //{
            //    this.tmr.Enabled = false;
            //}
            if (P_bl_update)
            {
                //更新历史记录表
                DropRecordHeadShow("");

                //杯号排序
                this.cup_sort();
                try
                {
                    if (_i_delect_index >= 0)
                    {
                        if (dgv_BatchData.Rows.Count > 0)
                        {
                            if (dgv_BatchData.Rows.Count - 1 >= _i_delect_index)
                            {
                                dgv_BatchData.CurrentCell = dgv_BatchData.Rows[_i_delect_index].Cells[0];
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
                            dgv_FormulaBrowse.Focus();
                            if (dgv_FormulaBrowse.Rows.Count > 0)
                            {
                                dgv_FormulaBrowse.CurrentCell = dgv_FormulaBrowse.Rows[0].Cells[0];
                                dgv_FormulaBrowse.CurrentCell.Selected = true;
                            }
                        }
                        dgv_BatchData_CurrentCellChanged(null, null);
                        _i_delect_index = -1;
                    }


                }
                catch
                {

                }


                P_bl_update = false;
            }

        }

        //配方浏览表查询按钮点击事件
        private void btn_Browse_Select_Click(object sender, EventArgs e)
        {
            FormulaBrowseHeadShow("");
        }

        //配方浏览表选择全部事件
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
        //配方浏览表选择尚未滴液事件
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
        //配方浏览表选择条件查询事件
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
        //配方浏览表删除按钮点击事件
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

                            FormulaBrowseHeadShow("");
                        }
                        catch
                        {

                        }

                    }
                }
            }
        }

        private void rdo_Record_Now_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdo_Record_Now.Checked)
            //{
            //    txt_Record_Operator.Enabled = false;
            //    txt_Record_Code.Enabled = false;
            //    dt_Record_Start.Enabled = false;
            //    dt_Record_End.Enabled = false;
            //}
        }

        private void rdo_Record_All_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdo_Record_All.Checked)
            //{
            //    txt_Record_Operator.Enabled = false;
            //    txt_Record_Code.Enabled = false;
            //    dt_Record_Start.Enabled = false;
            //    dt_Record_End.Enabled = false;
            //}
        }

        private void rdo_Record_condition_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdo_Record_condition.Checked)
            //{
            //    txt_Record_Operator.Enabled = true;
            //    txt_Record_Code.Enabled = true;
            //    dt_Record_Start.Enabled = true;
            //    dt_Record_End.Enabled = true;
            //}
        }

        private void btn_Record_Select_Click(object sender, EventArgs e)
        {
            DropRecordHeadShow("");
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
                if (dgv_BatchData.CurrentRow != null &&
                    dgv_BatchData.CurrentRow.DefaultCellStyle.BackColor != Color.DarkGray &&
                    dgv_BatchData.CurrentRow.DefaultCellStyle.BackColor != Color.Red &&
                    dgv_BatchData.CurrentRow.DefaultCellStyle.BackColor != Color.Lime)
                {
                    int i_cup = Convert.ToInt16(dgv_BatchData.CurrentRow.Cells[0].Value);

                    //获取批次的批次表头
                    string s_sql = "SELECT CupNum, FormulaCode, VersionNum, BatchName" +
                                " FROM drop_head where CupNum>="+ i_cup + "  Order BY CupNum  desc;";

                    DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    List<int> lis_cup = new List<int>();
                    int i_cupInex = 1;
                    for (int i = 0; i < dt_formulahead.Rows.Count; i++)
                    {
                        //把正在滴液的排除
                        if (dt_formulahead.Rows[i][dt_formulahead.Columns["BatchName"]].ToString() != "0")
                        {
                            lis_cup.Add(Convert.ToInt32(dt_formulahead.Rows[i][0].ToString()));
                        }
                    }
                    
                    for (int i = 0; i < dt_formulahead.Rows.Count; i++)
                    {
                        if (dt_formulahead.Rows[i][dt_formulahead.Columns["BatchName"]].ToString() != "0")
                        {
                            i_cupInex++;
                            continue;
                        }

                        int i_cupNum= Convert.ToInt32(dt_formulahead.Rows[i][0].ToString());

                        s_sql = "UPDATE drop_head SET CupNum = "+(i_cupInex+ i_cupNum) +" WHERE CupNum = " + i_cupNum + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        s_sql = "UPDATE drop_details SET CupNum = "+(i_cupInex+ i_cupNum) +" WHERE CupNum = " + i_cupNum + ";";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        i_cupInex = 1;
                    }

                    s_sql = "INSERT INTO drop_head (CupNum) VALUES(" + i_cup + ");";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    BatchHeadShow("");
                }

            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "批次表添加行事件", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Batch Table Add Rows Event", MessageBoxButtons.OK, false);
            }
        }

        private void txt_Non_AnhydrationWR_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        //点击操作员进来
        public void setOperatorTextSelect(string enType)
        {
            //do_Browse_All
            //this.rdo_Browse_All.Checked = true; //默认全部

            this.rdo_Browse_NoDrop.Checked = true;

            //this.rdo_Browse_condition.Checked = true;
            //this.rdo_Browse_condition_CheckedChanged(null,null);
            Console.WriteLine("Test Test Test");

            //enType = enType.Replace("操作员:", "");
            this.txt_Browse_Operator.Text = enType;
            this.txt_Operator.Text = enType;

            //txt_Browse_Operator
            this.txt_Browse_Operator.Items.Clear();
            this.txt_Browse_Operator.Text = enType;

            //this.txt_Record_Operator.Items.Clear();
            //this.txt_Record_Operator.Text = enType;

            //this.txt_Operator.Items.Add(enType);
            //txt_Browse_Operator.Items.Clear(); //先清空
            //txt_Browse_Operator.Items.Add(enType);
            //把操作员给他赋值  然后再查询
            FormulaBrowseHeadShow("");
            DropRecordHeadShow("");

        }

        //导出
        private void button1_Click(object sender, EventArgs e)
        {

            //string P_str_sql = null;
            //DataTable _dt_data = new DataTable();
            //string selectField = "*";
            ////获取配方浏览资料表头
            //if (rdo_Record_Now.Checked)
            //{
            //    P_str_sql = "SELECT " + selectField + " FROM history_head WHERE" +
            //                " FinishTime > CURDATE() ORDER BY MyID DESC;";
            //    _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            //}
            //else if (rdo_Record_All.Checked || txt_Record_Operator.Text.Equals("全部")) //只有主管有全部
            //{
            //    P_str_sql = "SELECT " + selectField + " FROM history_head" +
            //                " WHERE FinishTime != '' ORDER BY MyID DESC;";
            //    _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            //}
            //else
            //{
            //    string P_str = null;
            //    if (txt_Record_Operator.Text != null && txt_Record_Operator.Text != "")
            //    {
            //        P_str = (" Operator = '" + txt_Record_Operator.Text + "' AND");
            //    }
            //    if (txt_Record_Code.Text != null && txt_Record_Code.Text != "")
            //    {
            //        P_str += (" FormulaCode = '" + txt_Record_Code.Text + "' AND");
            //    }
            //    if (dt_Record_Start.Text != null && dt_Record_Start.Text != "")
            //    {
            //        P_str += (" FinishTime >= '" + dt_Record_Start.Text + "' AND");
            //    }
            //    else
            //    {
            //        return;
            //    }

            //    if (dt_Record_End.Text != null && dt_Record_End.Text != "")
            //    {
            //        P_str += (" FinishTime <= '" + dt_Record_End.Text + "' ");
            //    }
            //    else
            //    {
            //        return;
            //    }
            //    P_str_sql = "SELECT " + selectField + " FROM history_head Where" + P_str + "" +
            //               " ORDER BY MyID DESC;";
            //    Console.WriteLine(P_str_sql);
            //    _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            //}

            //DateTime dt = DateTime.Now;
            //string name = dt.ToFileTime().ToString();

            //string path6 = System.Windows.Forms.Application.StartupPath;
            //FileStream fs = new FileStream(path6 + "\\" + name + ".txt", FileMode.Append);
            //StreamWriter wr = null;
            //wr = new StreamWriter(fs);
            //if (_dt_data.Rows.Count > 0)
            //{
            //    string value = "";
            //    foreach (DataRow msg in _dt_data.Rows)
            //    {
            //        //表头
            //        value = value + "500M"; //Record ID
            //        string FormulaCode = msg["FormulaCode"].ToString(); //Recipe Code
            //        if (FormulaCode.Length < 12)
            //        {
            //            for (int i = 0; i < 12 - FormulaCode.Length; i++)
            //            {
            //                value = value + " ";//补空格
            //            }
            //        }
            //        //Shor Code没有 直接不填 2个长度
            //        value = value + "  ";

            //        //Number of components 2个长度
            //        //查询  也就是配方 多少个染助剂
            //        P_str_sql = "SELECT * FROM formula_details" +
            //                   " Where FormulaCode = '" + FormulaCode + "'" +
            //                   " AND VersionNum = '" + msg["VersionNum"].ToString() + "';";
            //        DataTable P_dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            //        value = value + P_dt_formuladetail.Rows.Count + "F";

            //        //Recipe Unit 1个长度 翻译后 配方单位 
            //        value = value + " ";

            //        //Adapter Number 3个长度 翻译后 适配器数量
            //        value = value + "   ";


            //        //Recipe Name //配方名称 24个长度
            //        string FormulaName = msg["FormulaName"].ToString();
            //        if (FormulaName.Length < 24)
            //        {
            //            for (int i = 0; i < 24 - FormulaName.Length; i++)
            //            {
            //                value = value + " ";//补空格

            //            }
            //        }





            //        //表头结束
            //        wr.WriteLine();//换行
            //    }
            //    //Console.WriteLine("有数据" + _dt_data.Rows.Count);
            //    //DataRow row = _dt_data.Rows[0];
            //    //string ss = row["MyID"].ToString();
            //    // Console.WriteLine(ss);
            //}

            //wr.Close();
            //FADM_Form.CustomMessageBox.Show("导出成功!文件名是" + name, "成功!", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void P_Formula_Load(object sender, EventArgs e)
        {
            ReSet_txt_FormulaCade(); //加载组合配方下拉框

            ReSet_txt_FormulaGroup();


            

        }


        private void ReSet_txt_FormulaCade()
        {
            List<string> lis_data = new List<string>();

            //跟据设定浓度重新排序
            string s_sql = "SELECT  FormulaCode FROM formula_head group by FormulaCode;";
            DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            foreach (DataRow row in dt_formulahead.Rows)
            {

                lis_data.Add(row[0].ToString());
            }

            this.txt_FormulaCode.AutoCompleteCustomSource.Clear();
            this.txt_FormulaCode.AutoCompleteCustomSource.AddRange(lis_data.ToArray());
            this.txt_FormulaCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_FormulaCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ReSet_txt_FormulaGroup()
        {
            List<string> lis_data = new List<string>();

            //跟据设定浓度重新排序
            string s_sql = "SELECT group_Name FROM `formula_group` WHERE node = '0';";
            DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            if (dt_formulahead.Rows.Count == 0)
            {
                //this.label16.Visible = false;
                //this.group.Visible = false;
                //Point o = new Point(3, 30);
                //this.lab_FormulaCode.Location = o;

                //o = new Point(131, 28);
                //this.txt_FormulaCode.Location = o;

                //o = new Point(283, 28);
                //this.txt_VersionNum.Location = o;

                //o = new Point(320, 28);
                //this.txt_State.Location = o;

                //o = new Point(421, 30);
                //this.lab_FormulaName.Location = o;

                //o = new Point(549, 28);
                //this.txt_FormulaName.Location = o;
                //this.txt_FormulaName.Width = 275;

                //o = new Point(839, 30);
                //this.lab_ClothType.Location = o;

                //o = new Point(965, 30);
                //this.txt_ClothType.Location = o;
                //this.txt_ClothType.Width = 275;
            }
            else
            {
                _b_groupFlag = true;
                foreach (DataRow row in dt_formulahead.Rows)
                {
                    this.group.Items.Add(row[0].ToString());
                }

            }

        }

        private void group_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Console.WriteLine(this.group.Text);
                    string s_groupName = this.group.Text;
                    string s_sql = "SELECT * FROM formula_group" +
                                     " WHERE   node = '1' AND group_Name = '" + s_groupName + "';";
                    DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    if (dt_formulahead.Rows.Count > 0)
                    {
                        dgv_FormulaData.Rows.Clear();
                        for (int i = 0; i < dt_formulahead.Rows.Count; i++)
                        {
                            dgv_FormulaData.Rows.Add((i + 1).ToString(),
                                                     dt_formulahead.Rows[i]["AssistantCode"].ToString(),
                                                     dt_formulahead.Rows[i]["AssistantName"].ToString()
                                                     );
                        }
                    }
                    //把布重 浴比=0
                    break;
            }

        }

        private void group_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("group_TextChanged" + group.Text);

        }

        private void btn_Browse_Delete_Click_1(object sender, EventArgs e)
        {

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
                    }


                    FADM_Object.Communal._b_isDripping = false;

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
    }
}

