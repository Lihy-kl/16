using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class AllFormulas : UserControl
    {

        //处理工艺字典
        Dictionary<string, int> _dic_dyeCode = new Dictionary<string, int>();
        string _s_stage = "";
        string _s_type = "1";

        List<string> _lis_dyeingCode = new List<string>();

        public AllFormulas()
        {
            InitializeComponent();

            string s_sql = "SELECT * FROM operator_table ;";
            DataTable dt_operator = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            foreach (DataRow dr in dt_operator.Rows)
            {
                // txt_Operator.Items.Add(Convert.ToString(dr[0]));
            }
            // 加个空字符串代表滴液
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



            //关闭自动排序功能
            dgv_BatchData.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_BatchData.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_BatchData.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            //设置标题居中显示
            dgv_BatchData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

          

            //设置内容居中显示
            dgv_BatchData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置内容字体
            dgv_BatchData.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置行高
            dgv_BatchData.RowTemplate.Height = 30;

           
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                //设置标题字体    //设置内容字体
                dgv_BatchData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);
                dgv_BatchData.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

                dgv_FormulaData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);
                dgv_FormulaData.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

               
                dgv_Dyeing.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);
                dgv_Dyeing.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

                dgv_Details.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);
                dgv_Details.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

                //dgv_Dye.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle1.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle2.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle3.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle4.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle5.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
            }
            else
            {
                //设置标题字体   //设置内容字体
                dgv_BatchData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);
                dgv_BatchData.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

                dgv_FormulaData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);
                dgv_FormulaData.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

                
                dgv_Dyeing.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);
                dgv_Dyeing.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

                dgv_Details.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);
                dgv_Details.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

                //dgv_Dye.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle1.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle2.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle3.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle4.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                //dgv_Handle5.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);


            }
        }

        private void btn_Record_Select_Click(object sender, EventArgs e)
        {
            DropRecordHeadShow();
        }

        /// <summary>
        /// 显示配方资料
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private void DropRecordHeadShow()
        {
            try
            {
                dgv_BatchData.Rows.Clear();

                string s_sql = null;
                DataTable dt_data = new DataTable();
                DataTable dt_data_Temp = new DataTable();


                //获取配方浏览资料表头
                if (rdo_Record_Now.Checked)
                {
                    s_sql = "SELECT FormulaCode, VersionNum" +
                                "  FROM formula_head WHERE" +
                                " CreateTime > CONVERT(varchar,GETDATE(),23) ORDER BY CreateTime DESC;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else if (rdo_Record_All.Checked)
                {
                    s_sql = "SELECT FormulaCode, VersionNum FROM formula_head  ORDER BY CreateTime DESC;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else
                {
                    string s_str = null;
                    if (txt_Record_Operator.Text != null && txt_Record_Operator.Text != "")
                    {
                        s_str = (" Operator = '" + txt_Record_Operator.Text + "' AND");
                    }
                    if (txt_Record_Code.Text != null && txt_Record_Code.Text != "")
                    {
                        s_str += (" FormulaCode = '" + txt_Record_Code.Text + "' AND");
                    }
                    if (dt_Record_Start.Text != null && dt_Record_Start.Text != "")
                    {
                        s_str += (" CreateTime >= '" + dt_Record_Start.Text + "' AND");
                    }
                    else
                    {
                        return;
                    }

                    if (dt_Record_End.Text != null && dt_Record_End.Text != "")
                    {
                        s_str += (" CreateTime <= '" + dt_Record_End.Text + "' ");
                    }
                    else
                    {
                        return;
                    }

                    s_sql = "SELECT FormulaCode, VersionNum FROM formula_head Where" + s_str + "" +
                               "  ORDER BY CreateTime DESC;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }

                //获取配方浏览资料表头
                if (rdo_Record_Now.Checked)
                {
                    s_sql = "SELECT FormulaCode, VersionNum" +
                                "  FROM formula_head_temp WHERE" +
                                " CreateTime > CONVERT(varchar,GETDATE(),23) ORDER BY CreateTime DESC;";
                    dt_data_Temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else if (rdo_Record_All.Checked)
                {
                    s_sql = "SELECT FormulaCode, VersionNum FROM formula_head_temp  ORDER BY CreateTime DESC;";
                    dt_data_Temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else
                {
                    string s_str = null;
                    if (txt_Record_Operator.Text != null && txt_Record_Operator.Text != "")
                    {
                        s_str = (" Operator = '" + txt_Record_Operator.Text + "' AND");
                    }
                    if (txt_Record_Code.Text != null && txt_Record_Code.Text != "")
                    {
                        s_str += (" FormulaCode = '" + txt_Record_Code.Text + "' AND");
                    }
                    if (dt_Record_Start.Text != null && dt_Record_Start.Text != "")
                    {
                        s_str += (" CreateTime >= '" + dt_Record_Start.Text + "' AND");
                    }
                    else
                    {
                        return;
                    }

                    if (dt_Record_End.Text != null && dt_Record_End.Text != "")
                    {
                        s_str += (" CreateTime <= '" + dt_Record_End.Text + "' ");
                    }
                    else
                    {
                        return;
                    }

                    s_sql = "SELECT FormulaCode, VersionNum FROM formula_head_temp Where" + s_str + "" +
                               "  ORDER BY CreateTime DESC;";
                    dt_data_Temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }



                //捆绑
                for (int i = 0; i < dt_data.Rows.Count; i++)
                {
                    dgv_BatchData.Rows.Add(dt_data.Rows[i][0].ToString(), dt_data.Rows[i][1].ToString(), "1");
                }
                for (int i = 0; i < dt_data_Temp.Rows.Count; i++)
                {
                    dgv_BatchData.Rows.Add(dt_data_Temp.Rows[i][0].ToString(), dt_data_Temp.Rows[i][1].ToString(), "0");
                }
                dgv_BatchData.ClearSelection();



            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "DropRecordHeadShow", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_BatchData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

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
                        //读取选中行对应的配方资料
                        string s_fC = dgv_BatchData.CurrentRow.Cells[0].Value.ToString();
                        string s_vN = dgv_BatchData.CurrentRow.Cells[1].Value.ToString();
                        string s_bU = dgv_BatchData.CurrentRow.Cells[2].Value.ToString();
                        _s_type = s_bU;
                        Search(s_fC, s_vN, s_bU);
                    }


                }
                catch
                {
                    //new FullAutomaticDripMachine.FADM_Object.MyAlarm(ex.Message, "批次表当前行改变事件", false);
                }
            }
        }

        //染色后处理DataGridView
        List<Control> lis_dg = new List<Control>();
        //后处理浴比控件
        List<Control> lis_handleBathRatio = new List<Control>();
        //染色后处理浴比值
        List<string> lis_hBR = new List<string>();

        private void txt_DyeingCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            lis_dg.Clear();
            lis_handleBathRatio.Clear();
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
                    s.grp_Dye.Text = (s_temp == "1" ? "染色" : "后处理") + "-" + dr["Code"].ToString();
                    lis_dg.Add(s.dgv_Dye);
                    s.txt_HandleBathRatio.Name = "txt_HBR_" + i_nNum.ToString();
                    lis_handleBathRatio.Add(s.txt_HandleBathRatio);
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
                    string str_sql = "select * from dyeing_code where DyeingCode ='" + txt_DyeingCode.Text + "' order by IndexNum;";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(str_sql);

                    int i_nNum = 0;
                    //先把助剂代码写入对应列表
                    foreach (DataRow dr in dt_data.Rows)
                    {
                        FADM_Object.MyDataGridView dgv_Dye = (FADM_Object.MyDataGridView)lis_dg[i_nNum];
                        //显示染色工艺
                        //if (dr[1].ToString() == "1")
                        {
                            string s_sql1;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)

                                s_sql1 = "SELECT TechnologyName FROM dyeing_process where Code = '" + dr[3].ToString() + "' and TechnologyName in ('加A','加B','加C','加D','加E','加F','加G','加H','加I','加J','加K','加L','加M','加N')  group  by TechnologyName;";
                            else
                                s_sql1 = "SELECT TechnologyName FROM dyeing_process where Code = '" + dr[3].ToString() + "' and TechnologyName in ('Add A','Add B','Add C','Add D','Add E','Add F','Add G','Add H','Add I','Add J','Add K','Add L','Add M','Add N')  group  by TechnologyName;";

                            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);
                            if (i_nNum < lis_hBR.Count)
                                lis_handleBathRatio[i_nNum].Text = lis_hBR[i_nNum];

                            for (int i = 0; i < dt_data1.Rows.Count; i++)
                            {
                                //查找对应数据
                                string P_str_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + txt_FormulaCode.Text + "' and VersionNum = '" + txt_VersionNum.Text + "' and TechnologyName = '" + dt_data1.Rows[i][0].ToString() + "' and DyeingCode = '" + txt_DyeingCode.Text + "';";
                                DataTable P_dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql2);
                                if (P_dt_data2.Rows.Count > 0)
                                {

                                    string s_realDropWeight = "0.00";
                                    
                                    dgv_Dye.Rows.Add(dt_data1.Rows[i][0].ToString(),
                                             P_dt_data2.Rows[0]["AssistantCode"].ToString().Trim(),
                                             P_dt_data2.Rows[0]["AssistantName"].ToString().Trim(),
                                             P_dt_data2.Rows[0]["FormulaDosage"].ToString(),
                                             P_dt_data2.Rows[0]["UnitOfAccount"].ToString(),
                                             null,
                                             P_dt_data2.Rows[0]["SettingConcentration"].ToString(),
                                             P_dt_data2.Rows[0]["RealConcentration"].ToString(),
                                             P_dt_data2.Rows[0]["ObjectDropWeight"].ToString(),
                                             s_realDropWeight);

                                    //DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_Dye[4, i];
                                    //List<string> lis_bottleNum = new List<string>();
                                    //lis_bottleNum.Add(P_dt_data2.Rows[0]["BottleNum"].ToString());
                                    //dd.Value = null;
                                    //dd.DataSource = lis_bottleNum;
                                    //dd.Value = (P_dt_data2.Rows[0]["BottleNum"]).ToString();

                                    //显示瓶号
                                    str_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                                " FROM bottle_details WHERE" +
                                                " AssistantCode = '" + dgv_Dye[1, i].Value.ToString() + "'" +
                                                " AND RealConcentration != 0 ORDER BY BottleNum ;";
                                    DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(str_sql);


                                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_Dye[5, i];
                                    List<string> bottleNum = new List<string>();
                                    bool exist = false;
                                    foreach (DataRow mdr in dt_bottle_details.Rows)
                                    {
                                        string num = mdr[0].ToString();

                                        bottleNum.Add(num);

                                        if ((P_dt_data2.Rows[0]["BottleNum"]).ToString() == num)
                                        {
                                            exist = true;
                                        }

                                    }


                                    dd.Value = null;
                                    dd.DataSource = bottleNum;
                                    if (exist)
                                    {
                                        dd.Value = (P_dt_data2.Rows[0]["BottleNum"]).ToString();
                                    }
                                    else
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            FADM_Form.CustomMessageBox.Show((P_dt_data2.Rows[0]["BottleNum"]).ToString() +
                                                       "号母液瓶不存在", "温馨提示", MessageBoxButtons.OK, false);
                                        else
                                            FADM_Form.CustomMessageBox.Show((P_dt_data2.Rows[0]["BottleNum"]).ToString() +
                                                       " Mother liquor bottle number does not exist", "Tips", MessageBoxButtons.OK, false);
                                    }


                                    //显示是否手动选瓶
                                    DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_Dye[10, i];
                                    dc.Value = P_dt_data2.Rows[0]["BottleSelection"].ToString() == "False" || P_dt_data2.Rows[0]["BottleSelection"].ToString() == "0" ? 0 : 1;
                                }
                                else
                                {
                                    dgv_Dye.Rows.Add(dt_data1.Rows[i][0].ToString());
                                }
                            }

                        }

                        //判断是否为空,空就把浴比复制填写
                        if (lis_handleBathRatio[i_nNum].Text == "")
                            lis_handleBathRatio[i_nNum].Text = txt_BathRatio.Text;
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

        private void rdo_Record_Now_Click(object sender, EventArgs e)
        {
            if (rdo_Record_Now.Checked)
            {
                txt_Record_Operator.Enabled = false;
                txt_Record_Code.Enabled = false;
                dt_Record_Start.Enabled = false;
                dt_Record_End.Enabled = false;
            }
        }

        private void rdo_Record_All_Click(object sender, EventArgs e)
        {
            if (rdo_Record_All.Checked)
            {
                txt_Record_Operator.Enabled = false;
                txt_Record_Code.Enabled = false;
                dt_Record_Start.Enabled = false;
                dt_Record_End.Enabled = false;
            }
        }

        private void rdo_Record_condition_Click(object sender, EventArgs e)
        {
            if (rdo_Record_condition.Checked)
            {
                txt_Record_Operator.Enabled = true;
                txt_Record_Code.Enabled = true;
                dt_Record_Start.Enabled = true;
                dt_Record_End.Enabled = true;
            }
        }

        private void Search(string s_formulaCode, string s_versionNum, string s_backUp)
        {
            try
            {
                if (s_backUp == "1")
                {
                    //读取选中行对应的配方资料
                    string s_sql = "SELECT Top 1 * FROM formula_head Where" +
                                                                   " FormulaCode = '" + s_formulaCode + "'" +
                                                                   "  And VersionNum = '" + s_versionNum + "';";

                    DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    s_sql = "SELECT * FROM formula_details" +
                                " Where FormulaCode = '" + s_formulaCode + "'" +
                                " AND VersionNum = '" + s_versionNum + "' ;";

                    DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    string s_dyeingCode = dt_formulahead.Rows[0]["DyeingCode"] is DBNull ? "" : dt_formulahead.Rows[0]["DyeingCode"].ToString();

                    string s_li = dt_formulahead.Rows[0]["HandleBRList"] is DBNull ? "" : dt_formulahead.Rows[0]["HandleBRList"].ToString();
                    lis_hBR.Clear();
                    if (s_li != "")
                    {
                        string[] sa_hBRList = s_li.Split('|');
                        lis_hBR = sa_hBRList.ToList();
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

                    //foreach (DataColumn mDc in dt_formulahead.Columns)
                    //{
                    //    string s_name = "txt_" + mDc.Caption.ToString();
                    //    foreach (Control c in this.grp_Handle.Controls)
                    //    {
                    //        if ((c is TextBox || c is ComboBox) && c.Name == s_name)
                    //        {
                    //            c.Text = dt_formulahead.Rows[0][mDc].ToString();
                    //            break;
                    //        }
                    //    }
                    //}

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
                                                 dt_formuladetail.Rows[i]["FormulaDosage"].ToString(),
                                                 dt_formuladetail.Rows[i]["UnitOfAccount"].ToString(),
                                                 null,
                                                 dt_formuladetail.Rows[i]["SettingConcentration"].ToString(),
                                                 dt_formuladetail.Rows[i]["RealConcentration"].ToString(),
                                                 dt_formuladetail.Rows[i]["AssistantName"].ToString().Trim(),
                                                 dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString(),
                                                 dt_formuladetail.Rows[i]["RealDropWeight"].ToString());

                        //显示瓶号
                        s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                    " FROM bottle_details WHERE" +
                                    " AssistantCode = '" + dgv_FormulaData[1, i].Value.ToString() + "'" +
                                    " AND RealConcentration != 0 ORDER BY BottleNum ;";
                        DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_FormulaData[4, i];
                        List<string> lis_bottleNum = new List<string>();

                        bool b_exist = false;
                        foreach (DataRow mdr in dt_bottle_details.Rows)
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
                                            " mother liquor bottle does not exist", "Tips", MessageBoxButtons.OK, false);
                        }


                        //显示是否手动选瓶
                        DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[10, i];
                        dc.Value = dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "False" || dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "0" ? 0 : 1;
                    }
                    dgv_FormulaData.ClearSelection();
                }
                else
                {
                    //读取选中行对应的配方资料
                    string s_sql = "SELECT Top 1 * FROM formula_head_temp Where" +
                                                                   " FormulaCode = '" + s_formulaCode + "'" +
                                                                   "  And VersionNum = '" + s_versionNum + "';";

                    DataTable dt_formulahead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    s_sql = "SELECT * FROM formula_details_temp" +
                                " Where FormulaCode = '" + s_formulaCode + "'" +
                                " AND VersionNum = '" + s_versionNum + "' ;";

                    DataTable dt_formuladetail = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    string s_dyeingCode = dt_formulahead.Rows[0]["DyeingCode"] is DBNull ? "" : dt_formulahead.Rows[0]["DyeingCode"].ToString();

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

                    //foreach (DataColumn mDc in dt_formulahead.Columns)
                    //{
                    //    string s_name = "txt_" + mDc.Caption.ToString();
                    //    foreach (Control c in this.grp_Handle.Controls)
                    //    {
                    //        if ((c is TextBox || c is ComboBox) && c.Name == s_name)
                    //        {
                    //            c.Text = dt_formulahead.Rows[0][mDc].ToString();
                    //            break;
                    //        }
                    //    }
                    //}

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
                                                 dt_formuladetail.Rows[i]["FormulaDosage"].ToString(),
                                                 dt_formuladetail.Rows[i]["UnitOfAccount"].ToString(),
                                                 null,
                                                 dt_formuladetail.Rows[i]["SettingConcentration"].ToString(),
                                                 dt_formuladetail.Rows[i]["RealConcentration"].ToString(),
                                                 dt_formuladetail.Rows[i]["AssistantName"].ToString().Trim(),
                                                 dt_formuladetail.Rows[i]["ObjectDropWeight"].ToString(),
                                                 dt_formuladetail.Rows[i]["RealDropWeight"].ToString());

                        //显示瓶号
                        s_sql = "SELECT BottleNum,SettingConcentration,RealConcentration,DropMinWeight" +
                                    " FROM bottle_details WHERE" +
                                    " AssistantCode = '" + dgv_FormulaData[1, i].Value.ToString() + "'" +
                                    " AND RealConcentration != 0 ORDER BY BottleNum ;";
                        DataTable dt_bottlenum = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_FormulaData[4, i];
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
                                            " mother liquor bottle does not exist", "Tips", MessageBoxButtons.OK, false);
                        }


                        //显示是否手动选瓶
                        DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_FormulaData[10, i];
                        dc.Value = dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "False" || dt_formuladetail.Rows[i]["BottleSelection"].ToString() == "0" ? 0 : 1;
                    }
                    dgv_FormulaData.ClearSelection();
                }


            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.ToString(), "Search", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_Dyeing_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgv_Dyeing.CurrentRow == null)
            {
                return;
            }
            string s_type = dgv_Dyeing.CurrentRow.Cells[0].Value.ToString();
            string s_code = dgv_Dyeing.CurrentRow.Cells[2].Value.ToString();
            DetailsShow(s_type, s_code);
        }

        /// <summary>
        /// 显示批次资料
        /// </summary>
        private void DetailsShow(string s_type, string s_code)
        {
            try
            {
                string s_sql = null;
                if (s_type == "染色")
                {
                    s_sql = "SELECT StepNum,TechnologyName,ProportionOrTime  FROM dyeing_process WHERE" +
                                   " Code = '" + s_code + "' Order By StepNum ; ";
                }
                else
                {
                    //获取批次资料表头
                    s_sql = "SELECT StepNum,TechnologyName,ProportionOrTime  FROM dyeing_process WHERE" +
                                   " Code = '" + s_code + "' Order By StepNum ; ";
                }
                DataTable dt_formula = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //捆绑
                dgv_Details.DataSource = new DataView(dt_formula);

                //设置标题栏名称
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    string[] sa_lineName = { "步号", "操作类型", "参数" };
                    for (int i = 0; i < 3; i++)
                    {
                        dgv_Details.Columns[i].HeaderCell.Value = sa_lineName[i];
                        //设置标题宽度
                        dgv_Details.Columns[i].Width = (dgv_Details.Width - 2) / 3;
                        //关闭点击标题自动排序功能
                        dgv_Details.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                else
                {
                    string[] sa_lineName = { "StepNumber", "OperationType", "Parameter" };
                    for (int i = 0; i < 3; i++)
                    {
                        dgv_Details.Columns[i].HeaderCell.Value = sa_lineName[i];
                        //设置标题宽度
                        dgv_Details.Columns[i].Width = (dgv_Details.Width - 2) / 3;
                        //关闭点击标题自动排序功能
                        dgv_Details.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }

                //设置标题居中显示
                dgv_Details.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置标题字体
               

                //设置内容居中显示
                dgv_Details.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

              

                //设置行高
                dgv_Details.RowTemplate.Height = 30;

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "DetailsShow", MessageBoxButtons.OK, true);
            }
        }
    }
}
