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
        ComboBox txt_DyeingCode = new ComboBox();
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
                        /*string s_fC = dgv_BatchData.CurrentRow.Cells[0].Value.ToString();
                        string s_vN = dgv_BatchData.CurrentRow.Cells[1].Value.ToString();
                        string s_bU = dgv_BatchData.CurrentRow.Cells[2].Value.ToString();
                        _s_type = s_bU;
                        Search(s_fC, s_vN, s_bU);*/
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

        private void txt_DyeingCode_SelectedIndexChanged(object sender, EventArgs e, string s_formulaCode, string s_versionNum)
        {
            for (int i = panel2.Controls.Count - 1; i >= 0; i--)
            {
                Control control = panel2.Controls[i];
                control.Dispose(); // 释放控件占用的资源
                control = null; // 解除引用，帮助垃圾回收器回收
            }
            lis_dg.Clear();
            lis_handleBathRatio.Clear();
            _lis_handleBathRatio.Clear();

            string s_sql = "SELECT * FROM dyeing_details where FormulaCode = '" + this.txt_FormulaCode.Text + "' and VersionNum = '" + this.txt_VersionNum.Text + "' ;";
            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_data != null && dt_data.Rows.Count > 0)
            {
                myShowConfigListView(this.txt_FormulaCode.Text, this.txt_VersionNum.Text);
                _s_stage = "后处理";
            }
            else
            {
                _s_stage = "滴液";
                //lab_HandleBathRatio.Visible = false;
                //txt_HandleBathRatio.Visible = false;
            }


            //构造工艺显示
            /*if (txt_DyeingCode.Text != "")
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
                            *//*DyeingHeadShow();
                            AddAssistantShow();*//*
                            myShowConfigListView(s_formulaCode, s_versionNum);
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
            }*/
        }
        public List<myDyeSelect> myDyeSelectList = new List<myDyeSelect>();
        public int Allcc = 0;
        //染色后处理DataGridView
        List<Control> _lis_dg = new List<Control>();
        //后处理浴比控件
        List<Control> _lis_handleBathRatio = new List<Control>();
        //染色后处理浴比值
        List<string> _lis_hBR = new List<string>();
        public Dictionary<string, FADM_Control.myDyeingConfiguration> mymap = new Dictionary<string, FADM_Control.myDyeingConfiguration>();
        int i_nNum = 1;
       /* private void myShowConfigListView(string txt_FormulaCode, string txt_VersionNum)
        {
            int pcc = 0;
            int i_nHeight = 80;
            string s_sql = "SELECT FormulaCode,VersionNum,StepNum,TechnologyName,Temp,TempSpeed,Time,RotorSpeed,Code, DyeType,AssistantCode,FormulaDosage,UnitOfAccount,BottleNum,SettingConcentration,RealConcentration,AssistantName,ObjectDropWeight,RealDropWeight,BottleSelection FROM dyeing_details where FormulaCode = '" + this.txt_FormulaCode.Text + "' and VersionNum = '" + this.txt_VersionNum.Text + "' order by StepNum asc ;";
            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            SortedDictionary<int, List<List<string>>> map = new SortedDictionary<int, List<List<string>>>();
            Dictionary<string, int> ccList = new Dictionary<string, int>();
            foreach (DataRow dr in dt_data.Rows)
            {
                List<string> strList = new List<string>();

                for (int i = 0; i < 20; i++)
                { //这个为一行
                    if (!ccList.ContainsKey(dr[8].ToString()))
                    { //不包含工艺名字
                        ccList.Add(dr[8].ToString(), pcc);//Code
                        pcc++;
                    }
                    object unknownTypeValue = dr[i];
                    string valueAsString = Convert.ChangeType(unknownTypeValue, typeof(string)) as string;
                    strList.Add(valueAsString);
                }
                int v = ccList[strList[8]];
                if (map.ContainsKey(v))
                {
                    map[v].Add(strList);
                }
                else
                {
                    List<List<string>> list = new List<List<string>>();
                    list.Add(strList);
                    map.Add(v, list);
                }
            }
            int index = 0;
            myDyeSelect sSelect = null;
            FADM_Control.myDyeingConfiguration s = null;
            Label ll = null;
            foreach (KeyValuePair<int, List<List<string>>> kvp in map)
            {
                List<List<string>> list = kvp.Value; //1个就是两个select 框+datagridview 展示步骤号和是否有加药
                //动态创建两个select
                sSelect = new myDyeSelect(); //前面面板内容全部清除 所以这里重新创建两个select搜索
                sSelect.Name = Allcc.ToString();
                sSelect.dy_type_comboBox1.Name = Allcc.ToString();
                sSelect.dy_type_comboBox1.Name = Allcc.ToString();
                sSelect.dy_type_comboBox1.Text = list[0][9].Equals("1") ? "染色工艺" : "后处理工艺";
                sSelect.dy_nodelist_comboBox2.Name = Allcc.ToString();

                myDyeSelectList.Add(sSelect);
                this.panel2.Controls.Add(sSelect);
                sSelect.dy_nodelist_comboBox2.Text = list[0][8].ToString(); //把工艺名称复制过去 先加载一遍数据
                Allcc++;

                s = new myDyeingConfiguration();//这一个对象就代表染色和染色加药
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题栏名称
                    string[] sa_lineName = { "步号", "操作类型", "温度", "速率", "百分比(%)/时间(s)", "转速" };
                    for (int i = 0; i < 6; i++)
                    {
                        s.dgv_dyconfiglisg.Columns[i].HeaderCell.Value = sa_lineName[i];
                        //设置标题宽度
                        s.dgv_dyconfiglisg.Columns[i].Width = (s.dgv_dyconfiglisg.Width - 2) / 6;
                        //关闭点击标题自动排序功能
                        s.dgv_dyconfiglisg.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    //设置标题字体
                    s.dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);
                    //设置内容字体
                    s.dgv_dyconfiglisg.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
                }
                else
                {
                    //设置标题栏名称
                    string[] sa_lineName = { "StepNumber", "OperationFlow", "SettingTemperature", "TemperatureRate", "Percentage(%)/time(s)", "Speed" };
                    for (int i = 0; i < 6; i++)
                    {
                        s.dgv_dyconfiglisg.Columns[i].HeaderCell.Value = sa_lineName[i];
                        //设置标题宽度
                        s.dgv_dyconfiglisg.Columns[i].Width = (s.dgv_dyconfiglisg.Width - 2) / 6;
                        //关闭点击标题自动排序功能
                        s.dgv_dyconfiglisg.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    //设置标题字体
                    s.dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);
                    //设置内容字体
                    s.dgv_dyconfiglisg.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                }
                //设置标题居中显示
                s.dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //设置内容居中显示
                s.dgv_dyconfiglisg.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //设置行高
                s.dgv_dyconfiglisg.RowTemplate.Height = 30;
                //设置标题居中显示
                s.dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //设置内容居中显示
                s.dgv_dyconfiglisg.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //设置行高
                s.dgv_dyconfiglisg.RowTemplate.Height = 30;
                s.dgv_dyconfiglisg.ColumnHeadersVisible = true;
                s.dgv_dyconfiglisg.ClearSelection();
                //s.dgv_dyconfiglisg.SelectionChanged += myTest;
                s.dgv_dyconfiglisg.Name = s.dgv_dyconfiglisg.Name + "_" + i_nNum.ToString();
                // s.dgv_dyconfiglisg.Name = i_nNum.ToString(); 不设名字，datagridview里通过这个名字判断
                s.txt_HandleBathRatio.Name = "txt_HBR_" + i_nNum.ToString();
                _lis_handleBathRatio.Add(s.txt_HandleBathRatio); //浴比这个数值其他方法已经获取到保存到集合里_lis_hBR
                if (index < lis_hBR.Count)
                {
                    _lis_handleBathRatio[index].Text = lis_hBR[index];
                    index++;
                }
                s.dgv_Dye.Name = i_nNum.ToString();
                s.dgv_Dye.AccessibleName = "dye";
                ll = new Label();
                ll.Name = i_nNum.ToString();
                ll.Text = "▼                                                                                  ";
                this.panel2.Controls.Add(ll);
                i_nNum++;
                _lis_dg.Add(s.dgv_Dye);
                this.panel2.Controls.Add(s);
                mymap.Add(sSelect.Name, s);

                //计算需要的行数
                int i_nAddNum = list.Count;
                int sin = 0;
                if (i_nAddNum == 1)
                {
                    sin = 30;
                }
                else if (i_nAddNum == 2 || i_nAddNum == 3)
                {
                    sin = 30;
                }
                else if (i_nAddNum == 4 || i_nAddNum == 5)
                {
                    sin = 20;
                }
                int fine = 0;
                if (i_nAddNum > 8)
                {
                    fine = 28;
                }
                else
                {
                    fine = 30;
                }

                s.Height = 60 + fine * i_nAddNum + 5 + sin;
                s.grp_Dye.Height = 60 + fine * i_nAddNum + 5 + sin + 3; //grp是分组框 dgv里面的数据框
                s.dgv_dyconfiglisg.Height = fine * i_nAddNum + sin;
                i_nHeight += s.Height + 10;
                s.grp_Dye.Text = (list[0][9].Equals("1") ? "染色工艺" + list[0][8].ToString() : "后处理工艺") + "(" + list[0][8].ToString() + ")";
                s.dgv_dyconfiglisg.Rows.Clear();

                List<List<string>> listYY = new List<List<string>>(); //存放加药
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[0][9].Equals("2"))//后处理
                    {
                        if (list[i][3].Trim().Equals("加A") || list[i][3].Trim().Equals("加B") || list[i][3].Trim().Equals("加C") || list[i][3].Trim().Equals("加D") || list[i][3].Trim().Equals("加E") || list[i][3].Trim().Equals("加F") || list[i][3].Trim().Equals("加G") || list[i][3].Trim().Equals("加H") || list[i][3].Trim().Equals("加I") || list[i][3].Trim().Equals("加J") || list[i][3].Trim().Equals("加K") || list[i][3].Trim().Equals("加L") || list[i][3].Trim().Equals("加M") || list[i][3].Trim().Equals("加N"))
                        {
                            if (list[i][3].Trim().Equals(list[i - 1][3].Trim()))//跟上一个相同
                            {
                                listYY.Add(list[i]);
                                continue;
                            }
                            s.dgv_dyconfiglisg.Rows.Add(list[i][2].Trim(), list[i][3].Trim(), list[i][4].Trim(), list[i][5].Trim(), list[i][6].Trim(), list[i][7].Trim());
                            listYY.Add(list[i]);
                            //看下上一个是否也是加药一样的名字
                        }
                        else
                        {
                            s.dgv_dyconfiglisg.Rows.Add(list[i][2].Trim(), list[i][3].Trim(), list[i][4].Trim(), list[i][5].Trim(), list[i][6].Trim(), list[i][7].Trim());
                        }
                    }
                    else
                    {
                        s.dgv_dyconfiglisg.Rows.Add(list[i][2].Trim(), list[i][3].Trim(), list[i][4].Trim(), list[i][5].Trim(), list[i][6].Trim(), list[i][7].Trim());
                        if (list[i][3].Trim().Equals("加A") || list[i][3].Trim().Equals("加B") || list[i][3].Trim().Equals("加C") || list[i][3].Trim().Equals("加D") || list[i][3].Trim().Equals("加E") || list[i][3].Trim().Equals("加F") || list[i][3].Trim().Equals("加G") || list[i][3].Trim().Equals("加H") || list[i][3].Trim().Equals("加I") || list[i][3].Trim().Equals("加J") || list[i][3].Trim().Equals("加K") || list[i][3].Trim().Equals("加L") || list[i][3].Trim().Equals("加M") || list[i][3].Trim().Equals("加N"))
                        {
                            listYY.Add(list[i]);
                        }
                    }

                }

                //处理加A加B...
                FADM_Object.MyDataGridView dgv_Dye = s.dgv_Dye;
                int i_nAddNum2 = listYY.Count;
                s.Height = s.Height + 30 * i_nAddNum2 + 5; //是整个组件的高度
                s.grp_Dye.Height = s.grp_Dye.Height + 30 * i_nAddNum2 - 5; //分组的高度
                s.dgv_Dye.Location = new System.Drawing.Point(s.dgv_dyconfiglisg.Location.X, s.dgv_dyconfiglisg.Location.Y + s.dgv_dyconfiglisg.Height);
                s.dgv_Dye.Height = 28 * i_nAddNum2;

                s.dgv_Dye.Rows.Clear();

                Dictionary<string, string> mm = new Dictionary<string, string>();
                for (int i = 0; i < listYY.Count; i++)
                {
                    string s_realDropWeight = "0.00";
                    *//*if (dgv_BatchData.CurrentRow != null && dgv_BatchData.CurrentRow.Selected)
                    {
                        string s_sql3 = "SELECT Sum(RealDropWeight) FROM dyeing_details where Code = '" + this.Text + "' and  FormulaCode = '" + txt_FormulaCode + "' and VersionNum = '" + txt_VersionNum + "' and TechnologyName = '"
                                        + listYY[i][3].ToString() + "' and CupNum = '" + dgv_BatchData.CurrentRow.Cells[0].Value.ToString() + "';";
                        DataTable dt_data3 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql3);
                        s_realDropWeight = dt_data3.Rows[0][0].ToString();
                    }*//*
                    //FormulaCode,VersionNum,StepNum,TechnologyName,Temp,TempSpeed,Time,RotorSpeed,Code, 
                    //   DyeType,AssistantCode,FormulaDosage,UnitOfAccount,BottleNum,SettingConcentration,
                    //   RealConcentration 15 ,AssistantName,ObjectDropWeight,RealDropWeight,BottleSelection
                    //listYY[i][12].ToString().Trim()
                    //改成重新去formula_handle_details 这个表查询 不是dyeing_details新步骤表
                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + list[0][8].ToString() + "' and  FormulaCode = '" + list[0][0].ToString() + "' and VersionNum = '" + list[0][1].ToString() + "' and AssistantCode = '" + listYY[i][10].ToString().Trim() + "' and TechnologyName = '" + listYY[i][3].ToString() + "' ;";
                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                    if (dt_data2.Rows.Count > 0)
                    {
                        s.dgv_Dye.Rows.Add(dt_data2.Rows[0]["TechnologyName"].ToString(),
                                   dt_data2.Rows[0]["AssistantCode"].ToString(),
                                   dt_data2.Rows[0]["AssistantName"].ToString(),
                                   dt_data2.Rows[0]["FormulaDosage"].ToString(),
                                   null,
                                   null,
                                   dt_data2.Rows[0]["SettingConcentration"].ToString(),
                                   dt_data2.Rows[0]["RealConcentration"].ToString(),
                                   dt_data2.Rows[0]["ObjectDropWeight"].ToString(),
                                   dt_data2.Rows[0]["RealDropWeight"].ToString());

                        mm.Add(dt_data2.Rows[0]["AssistantCode"].ToString().Trim(), dt_data2.Rows[0]["UnitOfAccount"].ToString().Trim());
                        mm.Add(dt_data2.Rows[0]["AssistantCode"].ToString().Trim() + "_old", dt_data2.Rows[0]["UnitOfAccount"].ToString().Trim());

                        //显示单位
                        string UnitOfAccount = listYY[i][12].ToString().Trim();
                        DataGridViewComboBoxCell dd_Unit = (DataGridViewComboBoxCell)s.dgv_Dye[4, i];
                        List<string> lis_UnitOfAccountNum = new List<string>();
                        if (UnitOfAccount.Equals("g/l"))
                        {  //代表是助剂 那就下拉框多个选择
                            lis_UnitOfAccountNum.Add("g/l");
                            lis_UnitOfAccountNum.Add("%");
                        }
                        else
                        {
                            lis_UnitOfAccountNum.Add(UnitOfAccount);
                        }
                        dd_Unit.DataSource = lis_UnitOfAccountNum;
                        dd_Unit.Value = lis_UnitOfAccountNum[0].ToString();


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

                            if ((listYY[i][13]).ToString() == s_num)
                            {
                                b_exist = true;
                            }

                        }
                        dd.Value = null;
                        dd.DataSource = lis_bottleNum;
                        if (b_exist)
                        {
                            dd.Value = (listYY[i][13]).ToString();
                        }
                        else
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show((listYY[i][13]).ToString() +
                                           "号母液瓶不存在", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show((listYY[i][13]).ToString() +
                                           " Mother liquor bottle number does not exist", "Tips", MessageBoxButtons.OK, false);
                        }


                        //显示是否手动选瓶
                        DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_Dye[10, i];
                        dc.Value = listYY[i][19].ToString() == "False" || listYY[i][19].ToString() == "0" ? 0 : 1;

                        Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
                    }
                }
            }


        }*/
        private void myShowConfigListView(string txt_FormulaCode, string txt_VersionNum)
        {
            int pcc = 0;
            int i_nHeight = 80;
            string s_sql = "SELECT FormulaCode,VersionNum,StepNum,TechnologyName,Temp,TempSpeed,Time,RotorSpeed,Code, DyeType,AssistantCode,FormulaDosage,UnitOfAccount,BottleNum,SettingConcentration,RealConcentration,AssistantName,ObjectDropWeight,RealDropWeight,BottleSelection FROM dyeing_details where FormulaCode = '" + this.txt_FormulaCode.Text + "' and VersionNum = '" + this.txt_VersionNum.Text + "' order by StepNum asc ;";
            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            SortedDictionary<int, List<List<string>>> map = new SortedDictionary<int, List<List<string>>>();
            Dictionary<string, int> ccList = new Dictionary<string, int>();
            List<string> strList = null;
            List<List<string>> list2 = null;
            foreach (DataRow dr in dt_data.Rows)
            {
                strList = new List<string>();

                for (int i = 0; i < 20; i++)
                { //这个为一行
                    if (!ccList.ContainsKey(dr[8].ToString()))
                    { //不包含工艺名字
                        ccList.Add(dr[8].ToString(), pcc);//Code
                        pcc++;
                    }
                    object unknownTypeValue = dr[i];
                    string valueAsString = Convert.ChangeType(unknownTypeValue, typeof(string)) as string;
                    strList.Add(valueAsString);
                }
                int v = ccList[strList[8]];
                if (map.ContainsKey(v))
                {
                    map[v].Add(strList);
                }
                else
                {
                    list2 = new List<List<string>>();
                    list2.Add(strList);
                    map.Add(v, list2);
                }
            }
            int index = 0;
            myDyeSelect sSelect = null;
            FADM_Control.myDyeingConfiguration s = null;
            Label ll = null;
            foreach (KeyValuePair<int, List<List<string>>> kvp in map)
            {
                List<List<string>> list = kvp.Value; //1个就是两个select 框+datagridview 展示步骤号和是否有加药
                //动态创建两个select
                sSelect = new myDyeSelect(); //前面面板内容全部清除 所以这里重新创建两个select搜索
                sSelect.Name = Allcc.ToString();
                sSelect.dy_type_comboBox1.Name = Allcc.ToString();
                sSelect.dy_type_comboBox1.Name = Allcc.ToString();
                sSelect.dy_type_comboBox1.Text = list[0][9].Equals("1") ? "染色工艺" : "后处理工艺";
                sSelect.dy_nodelist_comboBox2.Name = Allcc.ToString();
                


                myDyeSelectList.Add(sSelect);
                this.panel2.Controls.Add(sSelect);

                sSelect.dy_nodelist_comboBox2.Text = list[0][8].ToString(); //把工艺名称复制过去 先加载一遍数据
                Allcc++;

                s = new myDyeingConfiguration();//这一个对象就代表染色和染色加药



                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题栏名称
                    string[] sa_lineName = { "步号", "操作类型", "温度", "速率", "百分比(%)/时间(s)", "转速" };
                    for (int i = 0; i < 6; i++)
                    {
                        s.dgv_dyconfiglisg.Columns[i].HeaderCell.Value = sa_lineName[i];
                        //设置标题宽度
                        s.dgv_dyconfiglisg.Columns[i].Width = (s.dgv_dyconfiglisg.Width - 2) / 6;
                        //关闭点击标题自动排序功能
                        s.dgv_dyconfiglisg.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    //设置标题字体
                    s.dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);
                    //设置内容字体
                    s.dgv_dyconfiglisg.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
                }
                else
                {
                    //设置标题栏名称
                    string[] sa_lineName = { "StepNumber", "OperationFlow", "SettingTemperature", "TemperatureRate", "Percentage(%)/time(s)", "Speed" };
                    for (int i = 0; i < 6; i++)
                    {
                        s.dgv_dyconfiglisg.Columns[i].HeaderCell.Value = sa_lineName[i];
                        //设置标题宽度
                        s.dgv_dyconfiglisg.Columns[i].Width = (s.dgv_dyconfiglisg.Width - 2) / 6;
                        //关闭点击标题自动排序功能
                        s.dgv_dyconfiglisg.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    //设置标题字体
                    s.dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);
                    //设置内容字体
                    s.dgv_dyconfiglisg.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                }
                //设置标题居中显示
                s.dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //设置内容居中显示
                s.dgv_dyconfiglisg.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //设置行高
                s.dgv_dyconfiglisg.RowTemplate.Height = 30;
                //设置标题居中显示
                s.dgv_dyconfiglisg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //设置内容居中显示
                s.dgv_dyconfiglisg.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //设置行高
                s.dgv_dyconfiglisg.RowTemplate.Height = 30;
                s.dgv_dyconfiglisg.ColumnHeadersVisible = true;
                s.dgv_dyconfiglisg.ClearSelection();
                //s.dgv_dyconfiglisg.SelectionChanged += myTest;
                //s.dgv_dyconfiglisg.Leave += dgv_dyconfiglisgLeave;
                s.dgv_dyconfiglisg.Name = s.dgv_dyconfiglisg.Name + "_" + i_nNum.ToString();

                // s.dgv_dyconfiglisg.Name = i_nNum.ToString(); 不设名字，datagridview里通过这个名字判断
                s.txt_HandleBathRatio.Name = "txt_HBR_" + i_nNum.ToString();
                _lis_handleBathRatio.Add(s.txt_HandleBathRatio); //浴比这个数值其他方法已经获取到保存到集合里_lis_hBR
                if (index < lis_hBR.Count)
                {
                    _lis_handleBathRatio[index].Text = lis_hBR[index];
                    index++;
                }

                s.dgv_Dye.Name = i_nNum.ToString();
                s.dgv_Dye.AccessibleName = "dye";
                //ll = new Label();
                /*ll.Name = i_nNum.ToString();
                ll.Text = "▼                                                                                  ";
                ll.Click += DyeingConHS;*/
                s.label1.Name = i_nNum.ToString();
                s.label1.Click += DyeingConHS;
                i_nNum++;
                _lis_dg.Add(s.dgv_Dye);
                this.panel2.Controls.Add(s);
                mymap.Add(sSelect.Name, s);

                //计算需要的行数
                int i_nAddNum = list.Count;
                int sin = 0;
                if (i_nAddNum == 1)
                {
                    sin = 30;
                }
                else if (i_nAddNum == 2 || i_nAddNum == 3)
                {
                    sin = 30;
                }
                else if (i_nAddNum == 4 || i_nAddNum == 5)
                {
                    sin = 20;
                }
                int fine = 0;
                if (i_nAddNum > 8)
                {
                    fine = 28;
                }
                else
                {
                    fine = 30;
                }

                s.Height = 60 + fine * i_nAddNum + 5 + sin;
                s.grp_Dye.Height = 60 + fine * i_nAddNum + 5 + sin + 3; //grp是分组框 dgv里面的数据框
                s.dgv_dyconfiglisg.Height = fine * i_nAddNum + sin;
                i_nHeight += s.Height + 10;
                s.grp_Dye.Text = (list[0][9].Equals("1") ? "染色工艺" + list[0][8].ToString() : "后处理工艺") + "(" + list[0][8].ToString() + ")";
                s.dgv_dyconfiglisg.Rows.Clear();

                List<List<string>> listYY = new List<List<string>>(); //存放加药
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[0][9].Equals("2"))//后处理
                    {
                        if (list[i][3].Trim().Equals("加A") || list[i][3].Trim().Equals("加B") || list[i][3].Trim().Equals("加C") || list[i][3].Trim().Equals("加D") || list[i][3].Trim().Equals("加E") || list[i][3].Trim().Equals("加F") || list[i][3].Trim().Equals("加G") || list[i][3].Trim().Equals("加H") || list[i][3].Trim().Equals("加I") || list[i][3].Trim().Equals("加J") || list[i][3].Trim().Equals("加K") || list[i][3].Trim().Equals("加L") || list[i][3].Trim().Equals("加M") || list[i][3].Trim().Equals("加N"))
                        {
                            if (i != 0)
                            {
                                if (list[i][3].Trim().Equals(list[i - 1][3].Trim()))//跟上一个相同
                                {
                                    listYY.Add(list[i]);
                                    continue;
                                }
                            }

                            s.dgv_dyconfiglisg.Rows.Add(list[i][2].Trim(), list[i][3].Trim(), list[i][4].Trim(), list[i][5].Trim(), list[i][6].Trim(), list[i][7].Trim());
                            listYY.Add(list[i]);
                            //看下上一个是否也是加药一样的名字
                        }
                        else
                        {
                            s.dgv_dyconfiglisg.Rows.Add(list[i][2].Trim(), list[i][3].Trim(), list[i][4].Trim(), list[i][5].Trim(), list[i][6].Trim(), list[i][7].Trim());
                        }
                    }
                    else
                    {
                        s.dgv_dyconfiglisg.Rows.Add(list[i][2].Trim(), list[i][3].Trim(), list[i][4].Trim(), list[i][5].Trim(), list[i][6].Trim(), list[i][7].Trim());
                        if (list[i][3].Trim().Equals("加A") || list[i][3].Trim().Equals("加B") || list[i][3].Trim().Equals("加C") || list[i][3].Trim().Equals("加D") || list[i][3].Trim().Equals("加E") || list[i][3].Trim().Equals("加F") || list[i][3].Trim().Equals("加G") || list[i][3].Trim().Equals("加H") || list[i][3].Trim().Equals("加I") || list[i][3].Trim().Equals("加J") || list[i][3].Trim().Equals("加K") || list[i][3].Trim().Equals("加L") || list[i][3].Trim().Equals("加M") || list[i][3].Trim().Equals("加N"))
                        {
                            listYY.Add(list[i]);
                        }
                    }

                }

                //处理加A加B...
                FADM_Object.MyDataGridView dgv_Dye = s.dgv_Dye;
                int i_nAddNum2 = listYY.Count;
                s.Height = s.Height + 30 * i_nAddNum2 + 5; //是整个组件的高度
                s.grp_Dye.Height = s.grp_Dye.Height + 30 * i_nAddNum2 - 5; //分组的高度
                s.dgv_Dye.Location = new System.Drawing.Point(s.dgv_dyconfiglisg.Location.X, s.dgv_dyconfiglisg.Location.Y + s.dgv_dyconfiglisg.Height);
                s.dgv_Dye.Height = 28 * i_nAddNum2;
             
                s.dgv_Dye.Rows.Clear();

                for (int i = 0; i < listYY.Count; i++)
                {
                    string s_realDropWeight = "0.00";
                    if (dgv_BatchData.CurrentRow != null && dgv_BatchData.CurrentRow.Selected)
                    {
                        string s_sql3 = "SELECT Sum(RealDropWeight) FROM dye_details where Code = '" + listYY[i][8].ToString() + "' and  FormulaCode = '" + txt_FormulaCode + "' and VersionNum = '" + txt_VersionNum + "' and TechnologyName = '"
                                        + listYY[i][3].ToString() + "'  and CupNum = '" + dgv_BatchData.CurrentRow.Cells[0].Value.ToString() + "';";
                        DataTable dt_data3 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql3);
                        if (dt_data3.Rows.Count != 0)
                        {
                            s_realDropWeight = dt_data3.Rows[0][0].ToString();
                        }
                    }
                    //FormulaCode,VersionNum,StepNum,TechnologyName,Temp,TempSpeed,Time,RotorSpeed,Code, 
                    //   DyeType,AssistantCode,FormulaDosage,UnitOfAccount,BottleNum,SettingConcentration,
                    //   RealConcentration 15 ,AssistantName,ObjectDropWeight,RealDropWeight,BottleSelection
                    //listYY[i][12].ToString().Trim()
                    //改成重新去formula_handle_details 这个表查询 不是dyeing_details新步骤表
                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + list[0][8].ToString() + "' and  FormulaCode = '" + list[0][0].ToString() + "' and VersionNum = '" + list[0][1].ToString() + "' and AssistantCode = '" + listYY[i][10].ToString().Trim() + "' and TechnologyName = '" + listYY[i][3].ToString() + "' ;";
                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                    if (dt_data2.Rows.Count > 0)
                    {
                        s.dgv_Dye.Rows.Add(dt_data2.Rows[0]["TechnologyName"].ToString(),
                                   dt_data2.Rows[0]["AssistantCode"].ToString(),
                                   dt_data2.Rows[0]["AssistantName"].ToString(),
                                   dt_data2.Rows[0]["FormulaDosage"].ToString(),
                                   null,
                                   null,
                                   dt_data2.Rows[0]["SettingConcentration"].ToString(),
                                   dt_data2.Rows[0]["RealConcentration"].ToString(),
                                   dt_data2.Rows[0]["ObjectDropWeight"].ToString(),
                                   s_realDropWeight);

                       

                        //显示单位
                        string UnitOfAccount = listYY[i][12].ToString().Trim();
                        DataGridViewComboBoxCell dd_Unit = (DataGridViewComboBoxCell)s.dgv_Dye[4, i];
                        List<string> lis_UnitOfAccountNum = new List<string>();
                        if (UnitOfAccount.Equals("g/l"))
                        {  //代表是助剂 那就下拉框多个选择
                            lis_UnitOfAccountNum.Add("g/l");
                            lis_UnitOfAccountNum.Add("%");
                        }
                        else
                        {
                            lis_UnitOfAccountNum.Add("%");
                            lis_UnitOfAccountNum.Add("g/l");

                        }
                        dd_Unit.DataSource = lis_UnitOfAccountNum;
                        dd_Unit.Value = lis_UnitOfAccountNum[0].ToString();


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

                            if ((listYY[i][13]).ToString() == s_num)
                            {
                                b_exist = true;
                            }

                        }
                        dd.Value = null;
                        dd.DataSource = lis_bottleNum;
                        if (b_exist)
                        {
                            dd.Value = (listYY[i][13]).ToString();
                        }
                        else
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show((listYY[i][13]).ToString() +
                                           "号母液瓶不存在", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show((listYY[i][13]).ToString() +
                                           " Mother liquor bottle number does not exist", "Tips", MessageBoxButtons.OK, false);
                        }


                        //显示是否手动选瓶
                        DataGridViewCheckBoxCell dc = (DataGridViewCheckBoxCell)dgv_Dye[10, i];
                        dc.Value = listYY[i][19].ToString() == "False" || listYY[i][19].ToString() == "0" ? 0 : 1;

                        Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
                    }
                }

                //隐藏
                DyeingConHS(s.label1, null);
            }


        }

        private void DyeingConHS(object sender, EventArgs e)
        {
            Label la = (Label)sender;
            string s_temp = la.Name;
            if ("dgb_for_label1".Equals(s_temp))
            {
                if (this.dgv_FormulaData.Visible)
                {
                    this.dgv_FormulaData.Hide();
                    la.Text = "▲ 配方详情                                                                                   ";
                    this.grp_FormulaData.Height = this.grp_FormulaData.Height - this.dgv_FormulaData.Height;
                    this.panel2.Height = this.panel2.Height + this.dgv_FormulaData.Height;
                }
                else
                {
                    this.dgv_FormulaData.Show();
                    la.Text = "▼ 配方详情                                                                                   ";
                    this.grp_FormulaData.Height = this.grp_FormulaData.Height + this.dgv_FormulaData.Height;
                    this.panel2.Height = this.panel2.Height - this.dgv_FormulaData.Height;
                }

            }
            else
            {
                if (mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Visible)
                { //隐藏
                    mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Hide();
                    Point xy = mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Location;
                    mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_Dye.Location = xy;
                    mymap[(Convert.ToInt32(s_temp) - 1).ToString()].grp_Dye.Height = mymap[(Convert.ToInt32(s_temp) - 1).ToString()].grp_Dye.Height - mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Height;
                    mymap[(Convert.ToInt32(s_temp) - 1).ToString()].Height = mymap[(Convert.ToInt32(s_temp) - 1).ToString()].Height - mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Height;
                    la.Text = "▲                                                                                  ";
                }
                else
                {
                    mymap[(Convert.ToInt32(s_temp) - 1).ToString()].Height = mymap[(Convert.ToInt32(s_temp) - 1).ToString()].Height + mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Height;
                    mymap[(Convert.ToInt32(s_temp) - 1).ToString()].grp_Dye.Height = mymap[(Convert.ToInt32(s_temp) - 1).ToString()].grp_Dye.Height + mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Height;
                    mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Show();
                    mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_Dye.Location = new Point(mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_Dye.Location.X, mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Location.Y + mymap[(Convert.ToInt32(s_temp) - 1).ToString()].dgv_dyconfiglisg.Height);
                    la.Text = "▼                                                                                  ";
                }
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
                    this.panel2.Controls.Add(s);
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

                    txt_DyeingCode_SelectedIndexChanged(null, null, s_formulaCode, s_versionNum);

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

                    txt_DyeingCode_SelectedIndexChanged(null, null, s_formulaCode, s_versionNum);

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
