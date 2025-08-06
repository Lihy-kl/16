using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public partial class DripCupDetails : Form
    {
        private int iCupNo;
        public DripCupDetails(int iCupNo)
        {
            InitializeComponent();
            this.iCupNo = iCupNo;
            txt_CupNum.Text = iCupNo.ToString();
        }

        private void DripCupDetails_Load(object sender, EventArgs e)
        {
            DataTable dt_data = new DataTable();
            DataRow dr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                //建立Column例，可以指明例的类型,这里用的是默认的string
                dt_data.Columns.Add(new DataColumn("类型"));
                dt_data.Columns.Add(new DataColumn("瓶号"));
                dt_data.Columns.Add(new DataColumn("配方用量"));
                dt_data.Columns.Add(new DataColumn("需加重量"));
                dt_data.Columns.Add(new DataColumn("实加重量"));

                //设置标题字体
                dgv_CupDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容字体
                dgv_CupDetails.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

            }
            else
            {
                dt_data.Columns.Add(new DataColumn("Type"));
                dt_data.Columns.Add(new DataColumn("BottleNum"));
                dt_data.Columns.Add(new DataColumn("DosageOfFormula"));
                dt_data.Columns.Add(new DataColumn("TargetVolume"));
                dt_data.Columns.Add(new DataColumn("ActualVolume"));

                //设置标题字体
                dgv_CupDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);

                //设置内容字体
                dgv_CupDetails.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
            }
            //获取要显示的行数

            //设置标题居中显示
            dgv_CupDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置标题字体
            dgv_CupDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置内容居中显示
            dgv_CupDetails.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置内容字体
            dgv_CupDetails.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置行高
            dgv_CupDetails.RowTemplate.Height = 30;

            ////获取当前批次号
            //string P_str_sql = "SELECT BatchName FROM enabled_set" +
            //                  " WHERE MyID = 1 ";

            //DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

            //string P_str_batch = Convert.ToString(_dt_data.Rows[0][_dt_data.Columns[0]]);

            //P_str_sql = "SELECT * FROM machine_parameters" +
            //            " WHERE MyID = 1 ";
            //_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            int i_maxcup = Lib_Card.Configure.Parameter.Machine_Cup_Total;
            int i_maxbottle = Lib_Card.Configure.Parameter.Machine_Bottle_Total;
            List<int> ints = new List<int>();
            //获取当前批次当前杯号信息
            //获取当前批次当前杯号信息
            string s_sql = "SELECT * FROM drop_details WHERE" +
                        " CupNum = '" + this.iCupNo +
                        "' AND (BottleNum > 0 AND ( BottleNum <= " + i_maxbottle + "" +
                        " ) Or BottleNum = 200 Or BottleNum =201) ORDER BY BottleNum;";

            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            
            int i_line = dt_data1.Rows.Count;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                for (int i = 1; i <= i_line; i++)
                {
                    dr = dt_data.NewRow();
                    dr[0] = "滴液";
                    dr[1] = dt_data1.Rows[i - 1]["BottleNum"];
                    dr[2] = dt_data1.Rows[i - 1]["FormulaDosage"];
                    dr[3] = dt_data1.Rows[i - 1]["ObjectDropWeight"];
                    dr[4] = dt_data1.Rows[i - 1]["RealDropWeight"];
                    dt_data.Rows.Add(dr);

                    //判断是否合格
                    if (dt_data1.Rows[i - 1]["Finish"].ToString() == "1")
                    {
                        if (dt_data1.Rows[i - 1]["ObjectDropWeight"] is DBNull || dt_data1.Rows[i - 1]["RealDropWeight"] is DBNull)
                        {
                            //dgv_FormulaData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            ints.Add(0);
                        }
                        else
                        {
                            double d_error = Lib_Card.Configure.Parameter.Other_AErr_Drip;
                            if (!(dt_data1.Rows[i - 1]["StandError"] is DBNull))
                            {
                                d_error = Convert.ToDouble(dt_data1.Rows[i - 1]["StandError"]);
                            }
                            else
                            {
                                d_error = (dt_data1.Rows[i - 1]["UnitOfAccount"].ToString() == "%" ? Lib_Card.Configure.Parameter.Other_AErr_Drip : Lib_Card.Configure.Parameter.Other_AssAErr_Drip);
                            }
                            if ((int)(Math.Abs(Convert.ToDouble(dt_data1.Rows[i - 1]["ObjectDropWeight"]) - Convert.ToDouble(dt_data1.Rows[i - 1]["RealDropWeight"])) * 1000) > (int)(d_error * 1000))
                            {
                                //dgv_FormulaData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                ints.Add(0);
                            }
                            else
                            {
                                ints.Add(1);
                            }
                        }
                    }
                    else
                    {
                        ints.Add(1);
                    }
                }
            }
            else
            {
                for (int i = 1; i <= i_line; i++)
                {
                    dr = dt_data.NewRow();
                    dr[0] = "Drip";
                    dr[1] = dt_data1.Rows[i - 1]["BottleNum"];
                    dr[2] = dt_data1.Rows[i - 1]["FormulaDosage"];
                    dr[3] = dt_data1.Rows[i - 1]["ObjectDropWeight"];
                    dr[4] = dt_data1.Rows[i - 1]["RealDropWeight"];
                    dt_data.Rows.Add(dr);

                    //判断是否合格
                    if (dt_data1.Rows[i - 1]["Finish"].ToString() == "1")
                    {
                        if (dt_data1.Rows[i - 1]["ObjectDropWeight"] is DBNull || dt_data1.Rows[i - 1]["RealDropWeight"] is DBNull)
                        {
                            //dgv_FormulaData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            ints.Add(0);
                        }
                        else
                        {
                            double d_error = Lib_Card.Configure.Parameter.Other_AErr_Drip;
                            if (!(dt_data1.Rows[i - 1]["StandError"] is DBNull))
                            {
                                d_error = Convert.ToDouble(dt_data1.Rows[i - 1]["StandError"]);
                            }
                            else
                            {
                                d_error = (dt_data1.Rows[i - 1]["UnitOfAccount"].ToString() == "%" ? Lib_Card.Configure.Parameter.Other_AErr_Drip : Lib_Card.Configure.Parameter.Other_AssAErr_Drip);
                            }
                            if ((int)(Math.Abs(Convert.ToDouble(dt_data1.Rows[i - 1]["ObjectDropWeight"]) - Convert.ToDouble(dt_data1.Rows[i - 1]["RealDropWeight"])) * 1000) > (int)(d_error * 1000))
                            {
                                //dgv_FormulaData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                ints.Add(0);
                            }
                            else
                            {
                                ints.Add(1);
                            }
                        }
                    }
                    else
                    {
                        ints.Add(1);
                    }
                }
            }

            ////获取当前批次当前杯号信息
            //P_str_sql = "SELECT * FROM dye_details WHERE" +
            //            " CupNum = '" + this.iCupNo +
            //            "' AND BottleNum > 0 AND ( BottleNum <= " + P_int_maxbottle + "" +
            //            " ) ORDER BY StepNum;";

            //_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

            //int Line1 = _dt_data.Rows.Count;
            //for (int i = 1; i <= Line1; i++)
            //{
            //    dr = dt.NewRow();
            //    dr[0] = _dt_data.Rows[i - 1]["Code"];
            //    dr[1] = _dt_data.Rows[i - 1]["BottleNum"];
            //    dr[2] = _dt_data.Rows[i - 1]["FormulaDosage"];
            //    dr[3] = _dt_data.Rows[i - 1]["ObjectDropWeight"];
            //    dr[4] = _dt_data.Rows[i - 1]["RealDropWeight"];
            //    dt.Rows.Add(dr);
            //}


            //捆绑
            dgv_CupDetails.DataSource = new DataView(dt_data);
            //预留滚动条空间
            int i_headspace = 1;
            if (i_line > 11)
            {
                i_headspace = 5;
            }
            for (int i = 0; i < 5; i++)
            {
                dgv_CupDetails.Columns[i].Width = dgv_CupDetails.Width / 5 - i_headspace;
            }
            dgv_CupDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_CupDetails.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            s_sql = "SELECT * FROM drop_head WHERE" +
                        " CupNum = '" + this.iCupNo + "' ;";

            dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            if (dt_data1.Rows.Count > 0)
            {

                txt_FormulaCode.Text = Convert.ToString(dt_data1.Rows[0][dt_data1.Columns["FormulaCode"]]);

                txt_VersionNum.Text = Convert.ToString(dt_data1.Rows[0][dt_data1.Columns["VersionNum"]]);

                txt_objectWater.Text = Convert.ToString(dt_data1.Rows[0][dt_data1.Columns["ObjectAddWaterWeight"]]);

                txt_realWater.Text = Convert.ToString(dt_data1.Rows[0][dt_data1.Columns["RealAddWaterWeight"]]);


                //P_str_sql = "SELECT SUM(CAST(ISNULL(ObjectDropWeight,0.00) as numeric(18,2))) FROM drop_details WHERE" +
                //            " CupNum = '" + this.iCupNo +
                //            "' AND BottleNum > " + P_int_maxbottle + ";";

                //_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                if (Convert.ToDouble(dt_data1.Rows[0][dt_data1.Columns["ObjectAddWaterWeight"]]) > 0)
                {
                    if (dt_data1.Rows[0]["AddWaterFinish"].ToString() == "1")
                    {
                        double d_error = Lib_Card.Configure.Parameter.Other_AErr_DripWater;
                        if (!(dt_data1.Rows[0]["WaterStandError"] is DBNull))
                        {
                            d_error = Convert.ToDouble(dt_data1.Rows[0]["WaterStandError"]);
                        }
                        double d_totalWeight = Convert.ToDouble(dt_data1.Rows[0]["TotalWeight"]);
                        double d_allDif = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}",
                                    d_totalWeight * Convert.ToDouble(d_error / 100.00)) : string.Format("{0:F3}",
                                    d_totalWeight * Convert.ToDouble(d_error / 100.00)));

                        if (d_allDif < Math.Abs(Convert.ToDouble(dt_data1.Rows[0][dt_data1.Columns["RealAddWaterWeight"]]) - Convert.ToDouble(dt_data1.Rows[0][dt_data1.Columns["ObjectAddWaterWeight"]])))
                        {
                            txt_realWater.BackColor = Color.Red;
                        }
                    }

                }

            }

            for (int i = 0; i < ints.Count; i++)
            {
                if (ints[i] == 0)
                    dgv_CupDetails.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            }


        }
    }
}
