using Lib_File;
using Newtonsoft.Json.Linq;
using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SmartDyeing.FADM_Auto
{
    internal class MyBrew
    {
        //读取错误累计次数
        public static int _i_errCount = 0;
        public static void Brew()
        {
            try
            {
                //用于标记是否有发送过
                FADM_Object.Communal._tcpModBusBrew._b_Connect=false;
                _i_errCount = 0;
                //开料机通讯
                while (true)
                {
                    
                    //新款开料机，支持中文
                    //if (0 == Lib_Card.Configure.Parameter.Machine_Opening_Type)
                    {
                        for (int i = 1; i < 6; i++)
                        {

                            int[] ia_array = new int[12];
                            int i_state = FADM_Object.Communal._tcpModBusBrew.Read(10039 + i * 100, 12,ref ia_array);
                            if (i_state != -1)
                            {
                                //FADM_Object.Communal._tcpModBusBrew._b_Connect = true;
                                _i_errCount = 0;

                                if (Communal._b_isexpired)
                                {
                                    //发送过期信号
                                    int[] ia_array1 = { 0 };
                                    int i_state1 = FADM_Object.Communal._tcpModBusBrew.Write(11000, ia_array1);
                                    if (i_state1 == -1)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            throw new Exception("软件已过期");
                                        else
                                            throw new Exception("The software has expired");
                                    }
                                }
                                else if (Communal._b_closebrew)
                                {
                                    //发送过期信号
                                    int[] ia_array1 = { 0 };
                                    int i_state1 = FADM_Object.Communal._tcpModBusBrew.Write(11000, ia_array1);
                                    if (i_state1 == -1)
                                    {
                                    }
                                }
                                else
                                {
                                    //没发送过，发送正常信号，就发送一次
                                    if(!Communal._tcpModBusBrew._b_Connect)
                                    {
                                        int[] ia_array1 = { 1 };
                                        int i_state1 = FADM_Object.Communal._tcpModBusBrew.Write(11000, ia_array1);
                                        if (i_state1 != -1)
                                        {
                                            Communal._tcpModBusBrew._b_Connect = true;
                                        }
                                    }
                                }


                            }
                            else
                            {
                                //FADM_Object.Communal._tcpModBusBrew._b_Connect = false;
                                FADM_Object.Communal._tcpModBusBrew.ReConnect();
                                _i_errCount++;
                                if (_i_errCount > 5)
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        throw new Exception("开料机通讯异常");
                                    else
                                        throw new Exception("Abnormal communication of the cutting machine");
                                }
                                else
                                {
                                    continue;
                                }
                            }


                            //工位3,4位
                            int i_no = ia_array[0];

                            //瓶号5,6
                            int i_bottle = ia_array[1];

                            //真实浓度7,8,9,10
                            int i_h = 0;
                            int i_l = 0;
                            int i_value;
                            i_h = ia_array[2];
                            i_l = ia_array[3];
                            if (i_h < 0)
                            {
                                i_value = (((i_l + 1) * 65536 + i_h));
                            }
                            else
                            {
                                i_value = ((i_l * 65536 + i_h));
                            }
                            int i_realcon = i_value;

                            //真实重量11,12,13,14
                            i_h = ia_array[4];
                            i_l = ia_array[5];
                            if (i_h < 0)
                            {
                                i_value = (((i_l + 1) * 65536 + i_h));
                            }
                            else
                            {
                                i_value = ((i_l * 65536 + i_h));
                            }
                            int i_weight = i_value;

                            i_weight = i_weight < 0 ? 0 : i_weight;
                            //瓶号输入完成状态15,16
                            int i_bottlefinish = ia_array[6];

                            //泡制完成状态17,18
                            int i_brewfinish = ia_array[7];

                            //19,20,21,22
                            i_h = ia_array[8];
                            i_l = ia_array[9];
                            if (i_h < 0)
                            {
                                i_value = (((i_l + 1) * 65536 + i_h));
                            }
                            else
                            {
                                i_value = ((i_l * 65536 + i_h));
                            }
                            int i_oweight = i_value;

                            //是否备料23,24
                            int i_prebrew = ia_array[10];

                            //是否手动输入25,26
                            int i_scan = ia_array[11];


                            if (i_brewfinish == 1)
                            {
                                if (i_bottle == 0)
                                {
                                    break;
                                }
                                if (i_weight == 0)
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    {
                                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("返回当前液量为0，请注意检查（查看开料机面板值，如果确实是0请点是，重新获取值请点否）", "母液泡制", MessageBoxButtons.YesNo, true);
                                        if (dialogResult == DialogResult.No)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Return the current liquid volume to 0, please check carefully (check the value on the cutting machine panel, click Yes if it is indeed 0, click No to retrieve the value)", "Mother liquor soaking", MessageBoxButtons.YesNo, true);
                                        if (dialogResult == DialogResult.No)
                                        {
                                            break;
                                        }
                                    }
                                }

                                //复位开料机完成标志位
                                

                                ia_array= new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(10046 + 100 * i, ia_array);

                                //泡制完成

                                int i_oribottle = i_bottle;
                                System.DateTime P_time = System.DateTime.Now;
                                //bool P_bl = false;
                                //again:
                                //    string s_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + i_oribottle + ";";
                                //    DataTable P_dt_bottle = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                //    if (P_dt_bottle.Rows.Count > 0)
                                //    {

                                //        i_oribottle = Convert.ToInt16(P_dt_bottle.Rows[0]["OriginalBottleNum"]);
                                //        if (i_oribottle > 0)
                                //        {
                                //            P_bl = true;
                                //            goto again;
                                //        }
                                //        else
                                //        {
                                //            if (P_bl)
                                //            {


                                //                int maxB = Lib_Card.Configure.Parameter.Machine_Bottle_Total;
                                //                if (Convert.ToInt16(P_dt_bottle.Rows[0]["BottleNum"]) <= maxB)
                                //                {
                                //                    P_time = Convert.ToDateTime(P_dt_bottle.Rows[0]["BrewingData"]);
                                //                }
                                //            }

                                //        }
                                //    }

                                //if (i_bottle == Class_SemiAuto.MyMove_XY.Move_XY_BottleNum)
                                //{
                                //    Class_Module.MyModule.Module_Update = true;
                                //}

                                string s_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + i_bottle + ";";
                                DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                if (dt_bottle_details.Rows.Count == 0)
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Form.CustomMessageBox.Show("未找到" + i_bottle + "号母液瓶资料", "母液泡制",MessageBoxButtons.OK,false);
                                    else
                                        FADM_Form.CustomMessageBox.Show("not found " + i_bottle + " Number mother liquor bottle information", "Mother liquor soaking", MessageBoxButtons.OK, false);
                                    break;
                                }
                                //设定浓度
                                int i_setcon = Convert.ToInt32(Convert.ToDouble(dt_bottle_details.Rows[0][dt_bottle_details.Columns["SettingConcentration"]]) * 1000000.00);

                                double d_realcon = Convert.ToDouble(i_realcon / 1000000.00);
                                double d_setcon = Convert.ToDouble(i_setcon / 1000000.00);

                                double d_bl_err = Convert.ToDouble(string.Format("{0:F2}", (((d_realcon - d_setcon) / d_setcon) * 100.00)));
                                d_bl_err = d_bl_err < 0 ? -d_bl_err : d_bl_err;

                                if (d_bl_err > 3.00)
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Form.CustomMessageBox.Show(i_bottle + "号瓶设定浓度：" +
                                            Convert.ToDouble(dt_bottle_details.Rows[0][dt_bottle_details.Columns["SettingConcentration"]]) +
                                            ",实际浓度：" + string.Format("{0:F6}", Convert.ToDouble(i_realcon) / 1000000.00) + ",误差过大", "母液泡制", MessageBoxButtons.OK, false);
                                    else
                                        FADM_Form.CustomMessageBox.Show(i_bottle + " bottle Set concentration ：" +
                                            Convert.ToDouble(dt_bottle_details.Rows[0][dt_bottle_details.Columns["SettingConcentration"]]) +
                                            ",Actual concentration：" + string.Format("{0:F6}", Convert.ToDouble(i_realcon) / 1000000.00) + ",Excessive error", "Mother liquor soaking", MessageBoxButtons.OK, false);

                                }
                                else
                                {
                                    //如果是备料，先记录备料表，不更新到母液瓶资料
                                    if (i_prebrew == 1)
                                    {
                                        //s_sql = "UPDATE pre_brew SET RealConcentration = '" +
                                        //                string.Format("{0:F6}", Convert.ToDouble(i_realcon) / 1000000.00) + "'" +
                                        //                ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(i_weight) / 1000.00) + "'" +
                                        //                ", BrewingData = '" + P_time + "', AdjustSuccess = 0" +
                                        //                " WHERE BottleNum = " + i_bottle + ";";

                                        //先判断备料表有无这个母液瓶备料记录，如果有先删除，保存最新记录
                                        s_sql = "Delete FROM pre_brew WHERE BottleNum = " + i_bottle + ";";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                        //保存最新记录
                                        s_sql = "Insert Into pre_brew(BottleNum,RealConcentration,CurrentWeight,BrewingData) values(" + i_bottle + "," + string.Format("{0:F6}", Convert.ToDouble(i_realcon) / 1000000.00) + ","
                                            + string.Format("{0:F6}", Convert.ToDouble(i_weight) / 1000.00) + ",'" + P_time + "');";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                    else
                                    {
                                        //需要针检
                                        if (FADM_Object.Communal._b_isNeedCheck)
                                        {
                                            //更新数据库
                                            s_sql = "UPDATE bottle_details SET RealConcentration = '" +
                                                        string.Format("{0:F6}", Convert.ToDouble(i_realcon) / 1000000.00) + "'" +
                                                        ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(i_weight) / 1000.00) + "'" +
                                                        ", BrewingData = '" + P_time + "', AdjustSuccess = 0" +
                                                        " WHERE BottleNum = " + i_bottle + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                        else
                                        {

                                            //更新数据库
                                            s_sql = "UPDATE bottle_details SET RealConcentration = '" +
                                                        string.Format("{0:F6}", Convert.ToDouble(i_realcon) / 1000000.00) + "'" +
                                                        ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(i_weight) / 1000.00) + "'" +
                                                        ", BrewingData = '" + P_time + "' " +
                                                        " WHERE BottleNum = " + i_bottle + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }
                                    }
                                }

                                s_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + i_bottle + ";";
                                dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                int i_oribottleNum = Convert.ToInt32(dt_bottle_details.Rows[0][dt_bottle_details.Columns["OriginalBottleNum"]]);


                                if (i_oribottleNum != 0)
                                {
                                    s_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + i_oribottleNum + ";";
                                    dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                    double d_bl_Ocurrent = Convert.ToDouble(dt_bottle_details.Rows[0]["CurrentWeight"]);

                                    d_bl_Ocurrent -= (Convert.ToDouble(i_oweight) / 100.00);
                                    d_bl_Ocurrent = (d_bl_Ocurrent < 0 ? 0 : d_bl_Ocurrent);
                                    s_sql = "UPDATE bottle_details SET CurrentWeight =  '" +
                                          string.Format("{0:F6}", d_bl_Ocurrent) + "'" +
                                          " WHERE BottleNum = " + i_oribottleNum + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                }

                                string s_inPut = "";
                                s_inPut += "开料瓶号：" + i_bottle.ToString() + ",";
                                s_inPut += "实际浓度：" + string.Format("{0:F6}", Convert.ToDouble(i_realcon) / 1000000.00) + ",";
                                s_inPut += "当前液量：" + string.Format("{0:F6}", Convert.ToDouble(i_weight) / 1000.00) + ",";

                                s_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + s_inPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                
                            }
                            else if (i_bottlefinish == 1)
                            {
                                //瓶号输入完成
                                if (i_bottle == 0)
                                {
                                    break;
                                }

                                //根据瓶号搜索对应资料
                                string s_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + i_bottle + ";";
                                DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                if (dt_bottle_details.Rows.Count == 0)
                                {
                                    //复位输入完成标记位
                                    

                                    ia_array = new int[1];
                                    ia_array[0] = 2;
                                    FADM_Object.Communal._tcpModBusBrew.Write(10045 + 100 * i, ia_array);

                                    //FADM_Form.CustomMessageBox.Show("未找到" + i_bottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        new FADM_Object.MyAlarm("未找到" + i_bottle + "号母液瓶资料", "母液泡制");
                                    else
                                        new FADM_Object.MyAlarm("not found " + i_bottle + " Number mother liquor bottle information", "Mother liquor soaking");
                                    //清除输入完成标志位

                                    break;
                                }

                                string s_inPut = "";
                                s_inPut += "开料瓶号：" + i_bottle.ToString() + ",";
                                s_inPut += "调液流程代码：" + Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["BrewingCode"]])+",";
                                s_inPut += "设定浓度：" + Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["SettingConcentration"]]) + ",";
                                s_inPut += "助剂代码：" + Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["AssistantCode"]]) + ",";
                                s_inPut += "允许最大调液量：" + Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["AllowMaxWeight"]]) + ",";
                                s_inPut += "开稀原瓶号：" + Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["OriginalBottleNum"]]) + ",";
                                if (i_scan == 0)
                                {
                                    s_inPut += "输入模式：手动输入,";
                                }
                                else if (i_scan == 1)
                                {
                                    s_inPut += "输入模式：扫描输入,";
                                }
                                else
                                {
                                    s_inPut += "输入模式：人工干预,";
                                }


                                //调液流程代码
                                string s_brewingCode = Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["BrewingCode"]]);

                                int i_setcon = Convert.ToInt32(Convert.ToDouble(dt_bottle_details.Rows[0][dt_bottle_details.Columns["SettingConcentration"]]) * 1000000.00);

                                string s_assistantCode = Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["AssistantCode"]]);

                                /*
                                 * $M100 最大调液量
                                 */

                                //允许最大调液量
                                int i_maxweight = Convert.ToInt32(dt_bottle_details.Rows[0][dt_bottle_details.Columns["AllowMaxWeight"]]);

                                /*
                                 * $M101 - $M102 原浓度
                                 */

                                //开稀原瓶号
                                int i_oribottle = Convert.ToInt32(dt_bottle_details.Rows[0][dt_bottle_details.Columns["OriginalBottleNum"]]);


                                //根据原瓶号找到原浓度
                                int i_oricon = 0;
                                if (i_oribottle != 0)
                                {
                                    s_sql = "SELECT RealConcentration FROM bottle_details WHERE BottleNum = " + i_oribottle + ";";
                                    dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                    if (dt_bottle_details.Rows.Count == 0)
                                    {
                                        //FADM_Form.CustomMessageBox.Show("未找到" + i_oribottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
                                        //复位输入完成标记位
                                       

                                        ia_array = new int[1];
                                        ia_array[0] = 2;
                                        FADM_Object.Communal._tcpModBusBrew.Write(10045 + 100 * i, ia_array);
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            new FADM_Object.MyAlarm("未找到" + i_oribottle + "号母液瓶资料", "母液泡制");
                                        else
                                            new FADM_Object.MyAlarm("not found " + i_oribottle + " Number mother liquor bottle information", "Mother liquor soaking");
                                        break;
                                    }
                                    i_oricon = Convert.ToInt32(Convert.ToDouble(dt_bottle_details.Rows[0][dt_bottle_details.Columns["RealConcentration"]]) * 1000000.00);

                                    s_inPut += "原瓶号浓度：" + Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["RealConcentration"]]) + ",";

                                }

                                /*
                                 * $M103 - $M104 设定浓度
                                 */

                                //设定浓度


                                /*
                                 * $M105 染助剂单位
                                 */

                                //根据染助剂代码找到单位
                                s_sql = "SELECT UnitOfAccount, AssistantName FROM" +
                                            " assistant_details WHERE AssistantCode = '" + s_assistantCode + "';";
                                dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                if (dt_bottle_details.Rows.Count == 0)
                                {
                                    //FADM_Form.CustomMessageBox.Show("未找到" + s_assistantCode + "染助剂资料", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    

                                    ia_array = new int[1];
                                    ia_array[0] = 2;
                                    FADM_Object.Communal._tcpModBusBrew.Write(10045 + 100 * i, ia_array);

                                    
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        new FADM_Object.MyAlarm("未找到" + s_assistantCode + "染助剂资料", "母液泡制");
                                    else
                                        new FADM_Object.MyAlarm("not found " + s_assistantCode + " Information on dyeing auxiliaries", "Mother liquor soaking");
                                    break;
                                }
                                int i_unitOfAccount = 0;
                                if (Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["UnitOfAccount"]]) == "%")
                                {
                                    i_unitOfAccount = 0;
                                }
                                else if (Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["UnitOfAccount"]]) == "g/l")
                                {
                                    i_unitOfAccount = 1;
                                }
                                else if (Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["UnitOfAccount"]]) == "G/L")
                                {
                                    i_unitOfAccount = 2;
                                }
                                else
                                {
                                    i_unitOfAccount = 3;
                                }

                                string s_assistantName = Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["AssistantName"]]);

                                s_inPut += "染助剂名称：" + s_assistantName + ",";

                                /*
                                 * $M106 总步数
                                 * $M107 接收完成标志位
                                 * $M108 步骤1类型
                                 * $M109 步骤1比例
                                 * $M110 步骤2类型
                                 * $M111 步骤2比例
                                 * $M112 步骤3类型
                                 * $M113 步骤3比例
                                 * $M114 步骤4类型
                                 * $M115 步骤4比例
                                 * $M116 步骤5类型
                                 * $M117 步骤5比例
                                 */


                                //根据调液流程代码找到调液流程
                                s_sql = "SELECT * FROM brewing_process WHERE BrewingCode = '" + s_brewingCode + "' ORDER BY StepNum;";
                                dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                if (dt_bottle_details.Rows.Count == 0)
                                {
                                    //FADM_Form.CustomMessageBox.Show("未找到" + s_brewingCode + "调液流程", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    

                                    ia_array = new int[1];
                                    ia_array[0] = 2;
                                    FADM_Object.Communal._tcpModBusBrew.Write(10045 + 100 * i, ia_array);
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        new FADM_Object.MyAlarm("未找到" + s_brewingCode + "调液流程", "母液泡制");
                                    else
                                        new FADM_Object.MyAlarm("not found " + s_brewingCode + " Liquid adjustment process", "Mother liquor soaking");
                                    break;
                                }
                                if (dt_bottle_details.Rows.Count > 5)
                                {
                                    //FADM_Form.CustomMessageBox.Show(s_brewingCode + "调液流程步骤超过5步", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    

                                    ia_array = new int[1];
                                    ia_array[0] = 2;
                                    FADM_Object.Communal._tcpModBusBrew.Write(10045 + 100 * i, ia_array);
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        new FADM_Object.MyAlarm(s_brewingCode + "调液流程步骤超过5步", "母液泡制");
                                    else
                                        new FADM_Object.MyAlarm(s_brewingCode + " The liquid adjustment process involves more than 5 steps", "Mother liquor soaking");
                                    break;
                                }

                                int[] ia_no_1 = { 0, 0, 0, 0, 0 };
                                int[] ia_data_1 = { 0, 0, 0, 0, 0 };
                                for (int j = 0; j < dt_bottle_details.Rows.Count; j++)
                                {
                                    string s_technologyName = Convert.ToString(dt_bottle_details.Rows[j][dt_bottle_details.Columns["TechnologyName"]]);
                                    int i_data = Convert.ToInt32(dt_bottle_details.Rows[j][dt_bottle_details.Columns["ProportionOrTime"]]);
                                    switch (s_technologyName)
                                    {

                                        case "加大冷水":
                                            //1

                                            ia_no_1[j] = 1;
                                            ia_data_1[j] = i_data;

                                            break;

                                        case "加小冷水":
                                            //2
                                            ia_no_1[j] = 2;
                                            ia_data_1[j] = i_data;

                                            break;

                                        case "加热水":
                                            //3
                                            ia_no_1[j] = 3;
                                            ia_data_1[j] = i_data;

                                            break;

                                        case "手动加染助剂":
                                            //4
                                            ia_no_1[j] = 4;
                                            ia_data_1[j] = i_data;

                                            break;
                                        case "搅拌":
                                            //5
                                            ia_no_1[j] = 5;
                                            ia_data_1[j] = i_data;

                                            break;

                                        default:

                                            break;
                                    }
                                }

                                /*
                                 * $M118 - $M125 染助剂名称
                                 * 
                                 * 
                                 */

                                string[] sa_name = { "000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D" };
                                byte[] byta_AssistantName = { 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D };
                                int i_k = 0;
                                for (int j = 0; j < s_assistantName.Length && j < sa_name.Length; j++)
                                {
                                    Encoding fromEcoding = Encoding.GetEncoding("UTF-8");//返回utf-8的编码
                                    Encoding toEcoding = Encoding.GetEncoding("gb2312");
                                    byte[] byta_fromBytes = fromEcoding.GetBytes(s_assistantName[j].ToString());
                                    byte[] byta_tobytes = Encoding.Convert(fromEcoding, toEcoding, byta_fromBytes);
                                    if (byta_tobytes.Length > 1)
                                    {
                                        sa_name[i_k] = byta_tobytes[1].ToString("X") + byta_tobytes[0].ToString("X");
                                        byta_AssistantName[2 * i_k] = byta_tobytes[1];
                                        byta_AssistantName[2 * i_k + 1] = byta_tobytes[0];
                                    }
                                    else if (byta_tobytes.Length == 1)
                                    {
                                        if (i_k - 1 >= 0)
                                        {
                                            string s_temp = (sa_name[i_k - 1]).Substring(0, 2);
                                            if (s_temp == "00")
                                            {
                                                sa_name[i_k - 1] = byta_tobytes[0].ToString("X") + sa_name[i_k - 1].Substring(2);
                                                //byta_AssistantName[2 * (i_k - 1) + 1] = byta_AssistantName[2 * (i_k - 1)];
                                                byta_AssistantName[2 * (i_k - 1)] = byta_tobytes[0];
                                                i_k--;
                                            }
                                            else
                                            {
                                                sa_name[i_k] = "00" + byta_tobytes[0].ToString("X");
                                                byta_AssistantName[2 * i_k] = 0x00;
                                                byta_AssistantName[2 * i_k + 1] = byta_tobytes[0];
                                            }
                                        }
                                        else
                                        {
                                            sa_name[i_k] = "00" + byta_tobytes[0].ToString("X");
                                            byta_AssistantName[2 * i_k] = 0x00;
                                            byta_AssistantName[2 * i_k + 1] = byta_tobytes[0];
                                        }
                                    }
                                    i_k++;
                                }


                                s_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','开始','" + s_inPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);


                                //情况输入完成标志位
                                ia_array = new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(10045 + 100 * i, ia_array);

                                ia_array = new int[28];
                                ia_array[0] = i_maxweight;
                                

                                int d_1 = 0;
                                int d_2 = 0;
                                d_2 = i_oricon;
                                d_1 = d_2 / 65536;
                                d_2 = d_2 % 65536;
                                ia_array[1] = d_2;
                                ia_array[2] = d_1;

                                d_2 = i_setcon;
                                d_1 = d_2 / 65536;
                                d_2 = d_2 % 65536;

                                ia_array[3] = d_2;
                                ia_array[4] = d_1;

                                ia_array[5] = i_unitOfAccount;
                                ia_array[6] = dt_bottle_details.Rows.Count;
                                ia_array[7] = 1;

                                ia_array[8] = ia_no_1[0];
                                ia_array[9] = ia_data_1[0];
                                ia_array[10] = ia_no_1[1];
                                ia_array[11] = ia_data_1[1];
                                ia_array[12] = ia_no_1[2];
                                ia_array[13] = ia_data_1[2];
                                ia_array[14] = ia_no_1[3];
                                ia_array[15] = ia_data_1[3];
                                ia_array[16] = ia_no_1[4];
                                ia_array[17] = ia_data_1[4];

                                ia_array[18] = i_oribottle;
                                ia_array[19] = 0;

                                ia_array[20] = byta_AssistantName[0]<<8| byta_AssistantName[1];
                                ia_array[21] = byta_AssistantName[2] << 8 | byta_AssistantName[3];
                                ia_array[22] = byta_AssistantName[4] << 8 | byta_AssistantName[5];
                                ia_array[23] = byta_AssistantName[6] << 8 | byta_AssistantName[7];
                                ia_array[24] = byta_AssistantName[8] << 8 | byta_AssistantName[9];
                                ia_array[25] = byta_AssistantName[10] << 8 | byta_AssistantName[11];
                                ia_array[26] = byta_AssistantName[12] << 8 | byta_AssistantName[13];
                                ia_array[27] = byta_AssistantName[14] << 8 | byta_AssistantName[15];
                                
                                FADM_Object.Communal._tcpModBusBrew.Write(9999 + 100 * i, ia_array);
                            }

                        }
                    }
                    

                    Thread.Sleep(1);

                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    new SmartDyeing.FADM_Object.MyAlarm(ex.Message, "开料机通讯" , false, 1);
                else
                    new SmartDyeing.FADM_Object.MyAlarm(ex.Message, "Cutting machine communication", false, 1);

                FADM_Object.Communal._b_brewErr = true;
            }
        }
    }
}
