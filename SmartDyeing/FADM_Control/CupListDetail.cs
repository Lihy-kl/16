using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class CupListDetail : UserControl
    {
        private List<CupInfo> _lis_cup = new List<CupInfo>();
        private List<CupInfo> _lis_cup1 = new List<CupInfo>();
        //分割显示杯号
        int _i_cup = 0;
        //是否显示第一页
        bool _b_first = true;
        public CupListDetail()
        {
            try
            {
                InitializeComponent();


                string s_sql = "SELECT *  FROM cup_details where Type = 3 order by CupNum;";
                DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_cup_details.Rows.Count > 36)
                {
                    _i_cup = Convert.ToInt16(dt_cup_details.Rows[35]["CupNum"].ToString());
                }
                else
                {
                    _i_cup = Convert.ToInt16(dt_cup_details.Rows[dt_cup_details.Rows.Count - 1]["CupNum"].ToString());
                }

                foreach (Control c in this.Controls)
                {
                    if (c is CupInfo)
                    {
                        _lis_cup.Add((CupInfo)c);
                    }
                }

                int i_number = 0;
                foreach (DataRow dr in dt_cup_details.Rows)
                {
                    SmartDyeing.FADM_Control.CupData cupData = new CupData();
                    cupData._s_cupNum = dr["CupNum"] is DBNull ? "" : dr["CupNum"].ToString();
                    cupData._s_formulaCode = dr["FormulaCode"] is DBNull ? "" : dr["FormulaCode"].ToString();
                    cupData._s_dyeingCode = dr["DyeingCode"] is DBNull ? "" : dr["DyeingCode"].ToString();
                    cupData._s_startTime = dr["StartTime"] is DBNull ? "" : dr["StartTime"].ToString();
                    cupData._s_realTemp = dr["RealTemp"] is DBNull ? "" : dr["RealTemp"].ToString();
                    cupData._s_totalWeight = dr["TotalWeight"] is DBNull ? "" : dr["TotalWeight"].ToString();
                    cupData._s_stepNum = dr["StepNum"] is DBNull ? "" : dr["StepNum"].ToString();
                    cupData._s_totalStep = dr["TotalStep"] is DBNull ? "" : dr["TotalStep"].ToString();
                    cupData._s_technologyName = dr["TechnologyName"] is DBNull ? "" : dr["TechnologyName"].ToString();
                    cupData._s_setTemp = dr["SetTemp"] is DBNull ? "" : dr["SetTemp"].ToString();
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        cupData._s_state = dr["Statues"] is DBNull ? "" : dr["Statues"].ToString();
                    else
                    {
                        if (dr["Statues"] is DBNull)
                        {
                            cupData._s_state = "";
                        }
                        else
                        {
                            if ("待机" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "standby";
                            }
                            else if ("上线" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "OnLine";
                            }
                            else if ("下线" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "OffLine";
                            }
                            else if ("检查待机状态" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Check standby mode";
                            }
                            else if ("检查历史状态" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Check historical status";
                            }
                            else if ("等待准备状态" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Waiting for preparation status";
                            }
                            else if ("停止中" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Stopping";
                            }
                            else if ("滴液" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Drip";
                            }
                            else if ("滴液成功" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Drip Success";
                            }
                            else if ("滴液失败" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Drip Fail";
                            }
                            else if ("前洗杯" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Front washing cup";
                            }
                            else if ("失败洗杯" == dr["SetTemp"].ToString())
                            {
                                cupData._s_state = "Fail washing cup";
                            }
                            else
                            {
                                cupData._s_state = dr["SetTemp"].ToString();
                            }
                        }
                    }
                    cupData._s_stepStartTime = dr["StepStartTime"] is DBNull ? "" : dr["StepStartTime"].ToString();
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        cupData._s_setTime = dr["SetTime"] is DBNull ? "" : dr["SetTime"].ToString() + "分";
                    else
                        cupData._s_setTime = dr["SetTime"] is DBNull ? "" : dr["SetTime"].ToString() + " Min";

                    _lis_cup[i_number].Update(cupData);
                    i_number++;

                    if (i_number == 36)
                    {
                        break;
                    }
                }
                timer1.Enabled = true;
                if (dt_cup_details.Rows.Count > 36)
                {
                    timer2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Lib_Log.Log.writeLogException("CupListDetail 构造函数：" + ex.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!_b_first)
                {
                    string s_sql = "SELECT *  FROM cup_details where Type = 3 and CupNum >" + _i_cup.ToString() + " order by CupNum;";
                    DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    int i_number = 0;
                    foreach (DataRow dr in dt_cup_details.Rows)
                    {
                        SmartDyeing.FADM_Control.CupData cupData = new CupData();
                        cupData._s_cupNum = dr["CupNum"] is DBNull ? "" : dr["CupNum"].ToString();
                        cupData._s_formulaCode = dr["FormulaCode"] is DBNull ? "" : dr["FormulaCode"].ToString();
                        cupData._s_dyeingCode = dr["DyeingCode"] is DBNull ? "" : dr["DyeingCode"].ToString();
                        cupData._s_startTime = dr["StartTime"] is DBNull ? "" : dr["StartTime"].ToString();
                        cupData._s_realTemp = dr["RealTemp"] is DBNull ? "" : dr["RealTemp"].ToString();
                        cupData._s_totalWeight = dr["TotalWeight"] is DBNull ? "" : dr["TotalWeight"].ToString();
                        cupData._s_stepNum = dr["StepNum"] is DBNull ? "" : dr["StepNum"].ToString();
                        cupData._s_totalStep = dr["TotalStep"] is DBNull ? "" : dr["TotalStep"].ToString();
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            cupData._s_technologyName = dr["TechnologyName"] is DBNull ? "" : dr["TechnologyName"].ToString();
                        else
                        {
                            if (dr["TechnologyName"] is DBNull)
                            {
                                cupData._s_technologyName = "";
                            }
                            else
                            {
                                if ("冷行" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Cool line";
                                }
                                else if ("温控" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Temperature control";
                                }
                                else if ("加药" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Dosing";
                                }
                                else if ("放布" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Entering the fabric";
                                }
                                else if ("出布" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Outgoing fabric";
                                }
                                else if ("排液" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Drainage";
                                }
                                else if ("洗杯" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Washing cups";
                                }
                                else if ("加水" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Water";
                                }
                                else if ("搅拌" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Stir";
                                }
                                else if ("待机保温" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Standby insulation";
                                }
                                else if ("快速冷却" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "rapid cooling";
                                }
                                else if ("保温运行" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Insulation operation";
                                }
                                else if ("停止中" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Stopping";
                                }
                                else if ("滴液" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Drip";
                                }
                                else
                                {
                                    cupData._s_technologyName = dr["TechnologyName"].ToString();
                                }
                            }
                        }
                        cupData._s_setTemp = dr["SetTemp"] is DBNull ? "" : dr["SetTemp"].ToString();
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            cupData._s_state = dr["Statues"] is DBNull ? "" : dr["Statues"].ToString();
                        else
                        {
                            if (dr["Statues"] is DBNull)
                            {
                                cupData._s_state = "";
                            }
                            else
                            {
                                if ("待机" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "standby";
                                }
                                else if ("上线" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "OnLine";
                                }
                                else if ("下线" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "OffLine";
                                }
                                else if ("检查待机状态" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Check standby mode";
                                }
                                else if ("检查历史状态" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Check historical status";
                                }
                                else if ("等待准备状态" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Waiting for preparation status";
                                }
                                else if ("停止中" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Stopping";
                                }
                                else if ("滴液" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Drip";
                                }
                                else if ("滴液成功" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Drip Success";
                                }
                                else if ("滴液失败" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Drip Fail";
                                }
                                else if ("前洗杯" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Front washing cup";
                                }
                                else if ("失败洗杯" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Fail washing cup";
                                }
                                else
                                {
                                    cupData._s_state = dr["SetTemp"].ToString();
                                }
                            }
                        }
                        cupData._s_stepStartTime = dr["StepStartTime"] is DBNull ? "" : dr["StepStartTime"].ToString();
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            cupData._s_setTime = dr["SetTime"] is DBNull ? "" : dr["SetTime"].ToString() + "分";
                        else
                            cupData._s_setTime = dr["SetTime"] is DBNull ? "" : dr["SetTime"].ToString() + " Min";

                        _lis_cup[i_number].Update(cupData);
                        i_number++;
                    }
                    for (; i_number < 36; i_number++)
                    {
                        SmartDyeing.FADM_Control.CupData cupData = new CupData();
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        {
                            cupData._s_cupNum = "杯号";
                        }
                        else
                        {
                            cupData._s_cupNum = "Cup";
                        }
                        cupData._s_formulaCode = "配方代码";
                        cupData._s_dyeingCode = "固染色代码";
                        cupData._s_state = "下线";
                        cupData._s_stepStartTime = "";
                        cupData._s_setTime = "";
                        cupData._s_startTime = "";

                        _lis_cup[i_number].Update(cupData);
                    }
                }
                else
                {
                    string s_sql = "SELECT *  FROM cup_details where Type = 3 and CupNum <=" + _i_cup.ToString() + " order by CupNum;";
                    DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    int i_number = 0;
                    foreach (DataRow dr in dt_cup_details.Rows)
                    {
                        SmartDyeing.FADM_Control.CupData cupData = new CupData();
                        cupData._s_cupNum = dr["CupNum"] is DBNull ? "" : dr["CupNum"].ToString();
                        cupData._s_formulaCode = dr["FormulaCode"] is DBNull ? "" : dr["FormulaCode"].ToString();
                        cupData._s_dyeingCode = dr["DyeingCode"] is DBNull ? "" : dr["DyeingCode"].ToString();
                        cupData._s_startTime = dr["StartTime"] is DBNull ? "" : dr["StartTime"].ToString();
                        cupData._s_realTemp = dr["RealTemp"] is DBNull ? "" : dr["RealTemp"].ToString();
                        cupData._s_totalWeight = dr["TotalWeight"] is DBNull ? "" : dr["TotalWeight"].ToString();
                        cupData._s_stepNum = dr["StepNum"] is DBNull ? "" : dr["StepNum"].ToString();
                        cupData._s_totalStep = dr["TotalStep"] is DBNull ? "" : dr["TotalStep"].ToString();
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            cupData._s_technologyName = dr["TechnologyName"] is DBNull ? "" : dr["TechnologyName"].ToString();
                        else
                        {
                            if (dr["TechnologyName"] is DBNull)
                            {
                                cupData._s_technologyName = "";
                            }
                            else
                            {
                                if ("冷行" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Cool line";
                                }
                                else if ("温控" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Temperature control";
                                }
                                else if ("加药" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Dosing";
                                }
                                else if ("放布" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Entering the fabric";
                                }
                                else if ("出布" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Outgoing fabric";
                                }
                                else if ("排液" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Drainage";
                                }
                                else if ("洗杯" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Washing cups";
                                }
                                else if ("加水" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Water";
                                }
                                else if ("搅拌" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Stir";
                                }
                                else if ("待机保温" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Standby insulation";
                                }
                                else if ("快速冷却" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "rapid cooling";
                                }
                                else if ("保温运行" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Insulation operation";
                                }
                                else if ("停止中" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Stopping";
                                }
                                else if ("滴液" == dr["TechnologyName"].ToString())
                                {
                                    cupData._s_technologyName = "Drip";
                                }
                                else
                                {
                                    cupData._s_technologyName = dr["TechnologyName"].ToString();
                                }
                            }
                        }
                        cupData._s_setTemp = dr["SetTemp"] is DBNull ? "" : dr["SetTemp"].ToString();
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            cupData._s_state = dr["Statues"] is DBNull ? "" : dr["Statues"].ToString();
                        else
                        {
                            if (dr["Statues"] is DBNull)
                            {
                                cupData._s_state = "";
                            }
                            else
                            {
                                if ("待机" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "standby";
                                }
                                else if ("上线" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "OnLine";
                                }
                                else if ("下线" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "OffLine";
                                }
                                else if ("检查待机状态" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Check standby mode";
                                }
                                else if ("检查历史状态" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Check historical status";
                                }
                                else if ("等待准备状态" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Waiting for preparation status";
                                }
                                else if ("停止中" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Stopping";
                                }
                                else if ("滴液" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Drip";
                                }
                                else if ("滴液成功" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Drip Success";
                                }
                                else if ("滴液失败" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Drip Fail";
                                }
                                else if ("前洗杯" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Front washing cup";
                                }
                                else if ("失败洗杯" == dr["Statues"].ToString())
                                {
                                    cupData._s_state = "Fail washing cup";
                                }
                                else
                                {
                                    cupData._s_state = dr["Statues"].ToString();
                                }
                            }
                        }
                        cupData._s_stepStartTime = dr["StepStartTime"] is DBNull ? "" : dr["StepStartTime"].ToString();
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            cupData._s_setTime = dr["SetTime"] is DBNull ? "" : dr["SetTime"].ToString() + "分";
                        else
                            cupData._s_setTime = dr["SetTime"] is DBNull ? "" : dr["SetTime"].ToString() + " Min";

                        _lis_cup[i_number].Update(cupData);
                        i_number++;
                    }
                }
            }
            catch (Exception ex)
            {
                Lib_Log.Log.writeLogException("CupListDetail timer1_Tick：" + ex.ToString());
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            _b_first = !_b_first;
        }
    }
}
