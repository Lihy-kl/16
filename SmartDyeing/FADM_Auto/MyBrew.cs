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
                if (0 == Lib_Card.Configure.Parameter.Machine_Opening_Type|| 1 == Lib_Card.Configure.Parameter.Machine_Opening_Type)
                    //用于标记是否有发送过
                    FADM_Object.Communal._tcpModBusBrew._b_Connect=false;
                _i_errCount = 0;
                //开料机通讯
                while (true)
                {

                    //TCP触摸屏通讯
                    if (0 == Lib_Card.Configure.Parameter.Machine_Opening_Type)
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
                                    if (i_state1 != -1)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            throw new Exception("软件已过期");
                                        else
                                            throw new Exception("The software has expired");
                                    }
                                }
                                else if (Communal._b_closebrew)
                                {
                                    //发送关机锁止信号
                                    int[] ia_array1 = { 0 };
                                    int i_state1 = FADM_Object.Communal._tcpModBusBrew.Write(11000, ia_array1);
                                    if (i_state1 != -1)
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
                            else if (i_brewfinish == 2)
                            {
                                //复位开料机完成标志位

                                string s_inPut = "";
                                s_inPut += "开料瓶号：" + i_bottle.ToString() + ",";
                                s_inPut += "工位号：" + i_no.ToString() + " 开料失败";

                                string s_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + s_inPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);


                                ia_array = new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(10046 + 100 * i, ia_array);
                                ////清空瓶号
                                //FADM_Object.Communal._tcpModBusBrew.Write(10040 + 100 * i, ia_array);
                            }
                            else if (i_brewfinish == 3)
                            {
                                //复位开料机完成标志位


                                string s_inPut = "";
                                s_inPut += "开料瓶号：" + i_bottle.ToString() + ",";
                                s_inPut += "工位号：" + i_no.ToString() + " 停止开料";

                                string s_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + s_inPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                ia_array = new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(10046 + 100 * i, ia_array);
                                //Thread.Sleep(2000);
                                ////清空瓶号
                                //FADM_Object.Communal._tcpModBusBrew.Write(10040 + 100 * i, ia_array);
                            }
                            else if (i_brewfinish == 4)
                            {
                                //复位开料机完成标志位

                                string s_inPut = "";
                                s_inPut += "开料瓶号：" + i_bottle.ToString() + ",";
                                s_inPut += "工位号：" + i_no.ToString() + " 断电导致开料失败";

                                string s_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + s_inPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                ia_array = new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(10046 + 100 * i, ia_array);
                                ////清空瓶号
                                //FADM_Object.Communal._tcpModBusBrew.Write(10040 + 100 * i, ia_array);
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
                    //PLC通讯
                    else if (1 == Lib_Card.Configure.Parameter.Machine_Opening_Type)
                    {
                        for (int i = 1; i < 6; i++)
                        {

                            int[] ia_array = new int[12];
                            int i_state = FADM_Object.Communal._tcpModBusBrew.Read(2810 + i * 200, 12, ref ia_array);
                            if (i_state != -1)
                            {
                                //FADM_Object.Communal._tcpModBusBrew._b_Connect = true;
                                _i_errCount = 0;

                                if (Communal._b_isexpired)
                                {
                                    //发送过期信号
                                    int[] ia_array1 = { 0 };
                                    int i_state1 = FADM_Object.Communal._tcpModBusBrew.Write(2801, ia_array1);
                                    if (i_state1 != -1)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            throw new Exception("软件已过期");
                                        else
                                            throw new Exception("The software has expired");
                                    }
                                }
                                else if (Communal._b_closebrew)
                                {
                                    //发送关机锁止信号
                                    int[] ia_array1 = { 0 };
                                    int i_state1 = FADM_Object.Communal._tcpModBusBrew.Write(2801, ia_array1);
                                    if (i_state1 != -1)
                                    {
                                    }
                                }
                                else
                                {
                                    //没发送过，发送正常信号，就发送一次
                                    if (!Communal._tcpModBusBrew._b_Connect)
                                    {
                                        int[] ia_array1 = { 1 };
                                        int i_state1 = FADM_Object.Communal._tcpModBusBrew.Write(2801, ia_array1);
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

                            if(FADM_Object.Communal._s_brewVersion == "")
                            {
                                //获取开料机版本

                                int[] ia_array_v = new int[2];
                                int i_state_v = FADM_Object.Communal._tcpModBusBrew.Read(2802, 2, ref ia_array_v);
                                if (i_state_v != -1)
                                {
                                    FADM_Object.Communal._s_brewVersion = ia_array_v[0].ToString("d4")+ ia_array_v[1].ToString("d4");
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


                                ia_array = new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(2817 + 200 * i, ia_array);

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
                                        FADM_Form.CustomMessageBox.Show("未找到" + i_bottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
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
                            else if (i_brewfinish == 2)
                            {
                                //复位开料机完成标志位

                                string s_inPut = "";
                                s_inPut += "开料瓶号：" + i_bottle.ToString() + ",";
                                s_inPut += "工位号：" + i_no.ToString() + " 开料失败";

                                string s_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + s_inPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);


                                ia_array = new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(2817 + 200 * i, ia_array);
                                ////清空瓶号
                                //FADM_Object.Communal._tcpModBusBrew.Write(10040 + 100 * i, ia_array);
                            }
                            else if (i_brewfinish == 3)
                            {
                                //复位开料机完成标志位


                                string s_inPut = "";
                                s_inPut += "开料瓶号：" + i_bottle.ToString() + ",";
                                s_inPut += "工位号：" + i_no.ToString() + " 停止开料";

                                string s_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + s_inPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                ia_array = new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(2817 + 200 * i, ia_array);
                                //Thread.Sleep(2000);
                                ////清空瓶号
                                //FADM_Object.Communal._tcpModBusBrew.Write(10040 + 100 * i, ia_array);
                            }
                            else if (i_brewfinish == 4)
                            {
                                //复位开料机完成标志位

                                string s_inPut = "";
                                s_inPut += "开料瓶号：" + i_bottle.ToString() + ",";
                                s_inPut += "工位号：" + i_no.ToString() + " 断电导致开料失败";

                                string s_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + s_inPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                ia_array = new int[1];
                                ia_array[0] = 0;
                                FADM_Object.Communal._tcpModBusBrew.Write(2817 + 200 * i, ia_array);
                                ////清空瓶号
                                //FADM_Object.Communal._tcpModBusBrew.Write(10040 + 100 * i, ia_array);
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
                                    FADM_Object.Communal._tcpModBusBrew.Write(2816 + 200 * i, ia_array);

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
                                s_inPut += "调液流程代码：" + Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["BrewingCode"]]) + ",";
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
                                        FADM_Object.Communal._tcpModBusBrew.Write(2816 + 200 * i, ia_array);
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
                                    FADM_Object.Communal._tcpModBusBrew.Write(2816 + 200 * i, ia_array);


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
                                    FADM_Object.Communal._tcpModBusBrew.Write(2816 + 200 * i, ia_array);
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
                                    FADM_Object.Communal._tcpModBusBrew.Write(2816 + 200 * i, ia_array);
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

                                string[] sa_name = { "000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D"
                                ,"000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D"
                                ,"000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D"
                                ,"000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D"
                                ,"000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D"
                                ,"000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D"};
                                byte[] byta_AssistantName = { 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D
                                ,0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D
                                ,0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D
                                ,0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D
                                ,0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D
                                ,0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D
                                ,0x00, 0x0D, 0x00, 0x0D};
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
                                FADM_Object.Communal._tcpModBusBrew.Write(3016 + 200 * i, ia_array);

                                ia_array = new int[90];
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


                                ia_array[20] = 0;
                                ia_array[21] = 0;
                                ia_array[22] = 0;
                                ia_array[23] = 0;
                                ia_array[24] = 0;
                                ia_array[25] = 0;
                                ia_array[26] = 0;
                                ia_array[27] = 0;
                                ia_array[28] = 0;
                                ia_array[29] = 0;

                                ia_array[30] = 0;
                                ia_array[31] = 0;
                                ia_array[32] = 0;
                                ia_array[33] = 0;
                                ia_array[34] = 0;
                                ia_array[35] = 0;
                                ia_array[36] = 0;
                                ia_array[37] = 0;
                                ia_array[38] = 0;
                                ia_array[39] = 0;

                                ia_array[40] = byta_AssistantName[0] << 8 | byta_AssistantName[1];
                                ia_array[41] = byta_AssistantName[2] << 8 | byta_AssistantName[3];
                                ia_array[42] = byta_AssistantName[4] << 8 | byta_AssistantName[5];
                                ia_array[43] = byta_AssistantName[6] << 8 | byta_AssistantName[7];
                                ia_array[44] = byta_AssistantName[8] << 8 | byta_AssistantName[9];
                                ia_array[45] = byta_AssistantName[10] << 8 | byta_AssistantName[11];
                                ia_array[46] = byta_AssistantName[12] << 8 | byta_AssistantName[13];
                                ia_array[47] = byta_AssistantName[14] << 8 | byta_AssistantName[15];
                                ia_array[48] = byta_AssistantName[16] << 8 | byta_AssistantName[17];
                                ia_array[49] = byta_AssistantName[18] << 8 | byta_AssistantName[19];
                                ia_array[50] = byta_AssistantName[20] << 8 | byta_AssistantName[21];
                                ia_array[51] = byta_AssistantName[22] << 8 | byta_AssistantName[23];
                                ia_array[52] = byta_AssistantName[24] << 8 | byta_AssistantName[25];
                                ia_array[53] = byta_AssistantName[26] << 8 | byta_AssistantName[27];
                                ia_array[54] = byta_AssistantName[28] << 8 | byta_AssistantName[29];
                                ia_array[55] = byta_AssistantName[30] << 8 | byta_AssistantName[31];
                                ia_array[56] = byta_AssistantName[32] << 8 | byta_AssistantName[33];
                                ia_array[57] = byta_AssistantName[34] << 8 | byta_AssistantName[35];
                                ia_array[58] = byta_AssistantName[36] << 8 | byta_AssistantName[37];
                                ia_array[59] = byta_AssistantName[38] << 8 | byta_AssistantName[39];
                                ia_array[60] = byta_AssistantName[40] << 8 | byta_AssistantName[41];
                                ia_array[61] = byta_AssistantName[42] << 8 | byta_AssistantName[43];
                                ia_array[62] = byta_AssistantName[44] << 8 | byta_AssistantName[45];
                                ia_array[63] = byta_AssistantName[46] << 8 | byta_AssistantName[47];
                                ia_array[64] = byta_AssistantName[48] << 8 | byta_AssistantName[49];
                                ia_array[65] = byta_AssistantName[50] << 8 | byta_AssistantName[51];
                                ia_array[66] = byta_AssistantName[52] << 8 | byta_AssistantName[53];
                                ia_array[67] = byta_AssistantName[54] << 8 | byta_AssistantName[55];
                                ia_array[68] = byta_AssistantName[56] << 8 | byta_AssistantName[57];
                                ia_array[69] = byta_AssistantName[58] << 8 | byta_AssistantName[59];
                                ia_array[70] = byta_AssistantName[60] << 8 | byta_AssistantName[61];
                                ia_array[71] = byta_AssistantName[62] << 8 | byta_AssistantName[63];
                                ia_array[72] = byta_AssistantName[64] << 8 | byta_AssistantName[65];
                                ia_array[73] = byta_AssistantName[66] << 8 | byta_AssistantName[67];
                                ia_array[74] = byta_AssistantName[68] << 8 | byta_AssistantName[69];
                                ia_array[75] = byta_AssistantName[70] << 8 | byta_AssistantName[71];
                                ia_array[76] = byta_AssistantName[72] << 8 | byta_AssistantName[73];
                                ia_array[77] = byta_AssistantName[74] << 8 | byta_AssistantName[75];
                                ia_array[78] = byta_AssistantName[76] << 8 | byta_AssistantName[77];
                                ia_array[79] = byta_AssistantName[78] << 8 | byta_AssistantName[79];
                                ia_array[80] = byta_AssistantName[80] << 8 | byta_AssistantName[81];
                                ia_array[81] = byta_AssistantName[82] << 8 | byta_AssistantName[83];
                                ia_array[82] = byta_AssistantName[84] << 8 | byta_AssistantName[85];
                                ia_array[83] = byta_AssistantName[86] << 8 | byta_AssistantName[87];
                                ia_array[84] = byta_AssistantName[88] << 8 | byta_AssistantName[89];
                                ia_array[85] = byta_AssistantName[90] << 8 | byta_AssistantName[91];
                                ia_array[86] = byta_AssistantName[92] << 8 | byta_AssistantName[93];
                                ia_array[87] = byta_AssistantName[94] << 8 | byta_AssistantName[95];
                                ia_array[88] = byta_AssistantName[96] << 8 | byta_AssistantName[97];
                                ia_array[89] = byta_AssistantName[98] << 8 | byta_AssistantName[99];

                                FADM_Object.Communal._tcpModBusBrew.Write(2830 + 200 * i, ia_array);
                            }

                        }
                    }
                    //威纶
                    else if (2 == Lib_Card.Configure.Parameter.Machine_Opening_Type)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            byte[] b_send = new byte[6];
                            b_send[0] = 0x01;
                            b_send[1] = 0x03;
                            ;
                            b_send[2] = Convert.ToByte((10039 + i * 100) / 256);
                            b_send[3] = Convert.ToByte((10039 + i * 100) % 256);
                            b_send[4] = 0x00;
                            b_send[5] = 0x0B;

                            FADM_Object.Communal.MyBrew.WriteAndRead(b_send);
                            if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                            {
                                throw new Exception("开料机通讯异常");
                            }

                            if (FADM_Object.Communal.MyBrew.datapool.Count < 27)
                                continue;


                            //工位3,4位
                            int P_int_no = (FADM_Object.Communal.MyBrew.datapool[3] << 8 | FADM_Object.Communal.MyBrew.datapool[4]);

                            //瓶号5,6
                            int P_int_bottle = (FADM_Object.Communal.MyBrew.datapool[5] << 8 | FADM_Object.Communal.MyBrew.datapool[6]);

                            //真实浓度7,8,9,10
                            int P_int_realcon = (FADM_Object.Communal.MyBrew.datapool[9] << 24 | FADM_Object.Communal.MyBrew.datapool[10] << 16 | FADM_Object.Communal.MyBrew.datapool[7] << 8 | FADM_Object.Communal.MyBrew.datapool[8]);
                            //真实重量11,12,13,14

                            int P_int_weight = (FADM_Object.Communal.MyBrew.datapool[13] << 24 | FADM_Object.Communal.MyBrew.datapool[14] << 16 | FADM_Object.Communal.MyBrew.datapool[11] << 8 | FADM_Object.Communal.MyBrew.datapool[12]);

                            P_int_weight = P_int_weight < 0 ? 0 : P_int_weight;
                            //瓶号输入完成状态15,16
                            int P_int_bottlefinish = (FADM_Object.Communal.MyBrew.datapool[15] << 8 | FADM_Object.Communal.MyBrew.datapool[16]);

                            //泡制完成状态17,18
                            int P_int_brewfinish = (FADM_Object.Communal.MyBrew.datapool[17] << 8 | FADM_Object.Communal.MyBrew.datapool[18]);

                            //19,20,21,22
                            int P_int_Oweight = (FADM_Object.Communal.MyBrew.datapool[21] << 24 | FADM_Object.Communal.MyBrew.datapool[22] << 16 | FADM_Object.Communal.MyBrew.datapool[19] << 8 | FADM_Object.Communal.MyBrew.datapool[20]);

                            //是否备料23,24
                            int P_int_prebrew = (FADM_Object.Communal.MyBrew.datapool[23] << 8 | FADM_Object.Communal.MyBrew.datapool[24]);


                            if (P_int_brewfinish == 1)
                            {
                                if (P_int_bottle == 0)
                                {
                                    break;
                                }
                                if (P_int_weight == 0)
                                {
                                    DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("返回当前液量为0，请注意检查（查看开料机面板值，如果确实是0请点是，重新获取值请点否）", "母液泡制", MessageBoxButtons.YesNo, true);
                                    if (dialogResult == DialogResult.No)
                                    {
                                        break;
                                    }
                                }

                                //复位开料机完成标志位
                                byte[] b_send1 = new byte[6];
                                b_send1[0] = 0x01;
                                b_send1[1] = 0x06;
                                ;
                                b_send1[2] = Convert.ToByte((10046 + i * 100) / 256);
                                b_send1[3] = Convert.ToByte((10046 + i * 100) % 256);
                                b_send1[4] = 0x00;
                                b_send1[5] = 0x00;

                                FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                {
                                    //return;
                                }

                                //泡制完成

                                int P_int_oribottle = P_int_bottle;
                                System.DateTime P_time = System.DateTime.Now;
                                bool P_bl = false;
                                //again:
                                //    string P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + P_int_oribottle + ";";
                                //    DataTable P_dt_bottle = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                                //    if (P_dt_bottle.Rows.Count > 0)
                                //    {

                                //        P_int_oribottle = Convert.ToInt16(P_dt_bottle.Rows[0]["OriginalBottleNum"]);
                                //        if (P_int_oribottle > 0)
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

                                //if (P_int_bottle == Class_SemiAuto.MyMove_XY.Move_XY_BottleNum)
                                //{
                                //    Class_Module.MyModule.Module_Update = true;
                                //}

                                string P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + P_int_bottle + ";";
                                DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                                if (P_dt_data.Rows.Count == 0)
                                {
                                    FADM_Form.CustomMessageBox.Show("未找到" + P_int_bottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
                                    break;
                                }
                                //设定浓度
                                int P_int_setcon = Convert.ToInt32(Convert.ToDouble(P_dt_data.Rows[0][P_dt_data.Columns["SettingConcentration"]]) * 1000000.00);

                                double P_dbl_realcon = Convert.ToDouble(P_int_realcon / 1000000.00);
                                double P_dbl_setcon = Convert.ToDouble(P_int_setcon / 1000000.00);

                                double P_dbl_err = Convert.ToDouble(string.Format("{0:F2}", (((P_dbl_realcon - P_dbl_setcon) / P_dbl_setcon) * 100.00)));
                                P_dbl_err = P_dbl_err < 0 ? -P_dbl_err : P_dbl_err;

                                if (P_dbl_err > 3.00)
                                {
                                    FADM_Form.CustomMessageBox.Show(P_int_bottle + "号瓶设定浓度：" +
                                           Convert.ToDouble(P_dt_data.Rows[0][P_dt_data.Columns["SettingConcentration"]]) +
                                           ",实际浓度：" + string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + ",误差过大", "母液泡制", MessageBoxButtons.OK, false);

                                }
                                else
                                {
                                    //如果是备料，先记录备料表，不更新到母液瓶资料
                                    if (P_int_prebrew == 1)
                                    {
                                        //P_str_sql = "UPDATE pre_brew SET RealConcentration = '" +
                                        //                string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + "'" +
                                        //                ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + "'" +
                                        //                ", BrewingData = '" + P_time + "', AdjustSuccess = 0" +
                                        //                " WHERE BottleNum = " + P_int_bottle + ";";

                                        //先判断备料表有无这个母液瓶备料记录，如果有先删除，保存最新记录
                                        P_str_sql = "Delete FROM pre_brew WHERE BottleNum = " + P_int_bottle + ";";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                                        //保存最新记录
                                        P_str_sql = "Insert Into pre_brew(BottleNum,RealConcentration,CurrentWeight,BrewingData) values(" + P_int_bottle + "," + string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + ","
                                            + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + ",'" + P_time + "');";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                                    }
                                    else
                                    {
                                        //需要针检
                                        if (FADM_Object.Communal._b_isNeedCheck)
                                        {
                                            //更新数据库
                                            P_str_sql = "UPDATE bottle_details SET RealConcentration = '" +
                                                        string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + "'" +
                                                        ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + "'" +
                                                        ", BrewingData = '" + P_time + "', AdjustSuccess = 0" +
                                                        " WHERE BottleNum = " + P_int_bottle + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                                        }
                                        else
                                        {

                                            //更新数据库
                                            P_str_sql = "UPDATE bottle_details SET RealConcentration = '" +
                                                        string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + "'" +
                                                        ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + "'" +
                                                        ", BrewingData = '" + P_time + "' " +
                                                        " WHERE BottleNum = " + P_int_bottle + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                                        }
                                    }
                                }

                                P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + P_int_bottle + ";";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                int oribottle = Convert.ToInt32(P_dt_data.Rows[0][P_dt_data.Columns["OriginalBottleNum"]]);


                                if (oribottle != 0)
                                {
                                    P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + oribottle + ";";
                                    P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                                    double P_dbl_Ocurrent = Convert.ToDouble(P_dt_data.Rows[0]["CurrentWeight"]);

                                    P_dbl_Ocurrent -= (Convert.ToDouble(P_int_Oweight) / 100.00);
                                    P_dbl_Ocurrent = (P_dbl_Ocurrent < 0 ? 0 : P_dbl_Ocurrent);
                                    P_str_sql = "UPDATE bottle_details SET CurrentWeight =  '" +
                                          string.Format("{0:F6}", P_dbl_Ocurrent) + "'" +
                                          " WHERE BottleNum = " + oribottle + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                                }

                                string strInPut = "";
                                strInPut += "开料瓶号：" + P_int_bottle.ToString() + ",";
                                strInPut += "实际浓度：" + string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + ",";
                                strInPut += "当前液量：" + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + ",";

                                P_str_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + strInPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);


                            }
                            else if (P_int_bottlefinish == 1)
                            {
                                //瓶号输入完成
                                if (P_int_bottle == 0)
                                {
                                    break;
                                }

                                //根据瓶号搜索对应资料
                                string P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + P_int_bottle + ";";
                                DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                                if (P_dt_data.Rows.Count == 0)
                                {
                                    //复位输入完成标记位
                                    byte[] b_send1 = new byte[6];
                                    b_send1[0] = 0x01;
                                    b_send1[1] = 0x06;
                                    ;
                                    b_send1[2] = Convert.ToByte((10045 + i * 100) / 256);
                                    b_send1[3] = Convert.ToByte((10045 + i * 100) % 256);
                                    b_send1[4] = 0x00;
                                    b_send1[5] = 0x02;

                                    FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                    if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                    {
                                        //return;
                                    }
                                    //FADM_Form.CustomMessageBox.Show("未找到" + P_int_bottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
                                    new FADM_Object.MyAlarm("未找到" + P_int_bottle + "号母液瓶资料", "母液泡制");
                                    //清除输入完成标志位

                                    break;
                                }

                                string strInPut = "";
                                strInPut += "开料瓶号：" + P_int_bottle.ToString() + ",";
                                strInPut += "调液流程代码：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["BrewingCode"]]) + ",";
                                strInPut += "设定浓度：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["SettingConcentration"]]) + ",";
                                strInPut += "助剂代码：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["AssistantCode"]]) + ",";
                                strInPut += "允许最大调液量：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["AllowMaxWeight"]]) + ",";
                                strInPut += "开稀原瓶号：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["OriginalBottleNum"]]) + ",";


                                //调液流程代码
                                string P_str_BrewingCode = Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["BrewingCode"]]);

                                int P_int_setcon = Convert.ToInt32(Convert.ToDouble(P_dt_data.Rows[0][P_dt_data.Columns["SettingConcentration"]]) * 1000000.00);

                                string P_str_AssistantCode = Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["AssistantCode"]]);

                                /*
                                 * $M100 最大调液量
                                 */

                                //允许最大调液量
                                int P_int_maxweight = Convert.ToInt32(P_dt_data.Rows[0][P_dt_data.Columns["AllowMaxWeight"]]);

                                /*
                                 * $M101 - $M102 原浓度
                                 */

                                //开稀原瓶号
                                int P_int_oribottle = Convert.ToInt32(P_dt_data.Rows[0][P_dt_data.Columns["OriginalBottleNum"]]);


                                //根据原瓶号找到原浓度
                                int P_int_oricon = 0;
                                if (P_int_oribottle != 0)
                                {
                                    P_str_sql = "SELECT RealConcentration FROM bottle_details WHERE BottleNum = " + P_int_oribottle + ";";
                                    P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                    if (P_dt_data.Rows.Count == 0)
                                    {
                                        //FADM_Form.CustomMessageBox.Show("未找到" + P_int_oribottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
                                        //复位输入完成标记位
                                        byte[] b_send1 = new byte[6];
                                        b_send1[0] = 0x01;
                                        b_send1[1] = 0x06;
                                        ;
                                        b_send1[2] = Convert.ToByte((10045 + i * 100) / 256);
                                        b_send1[3] = Convert.ToByte((10045 + i * 100) % 256);
                                        b_send1[4] = 0x00;
                                        b_send1[5] = 0x02;

                                        FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                        if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                        {
                                            //return;
                                        }
                                        new FADM_Object.MyAlarm("未找到" + P_int_oribottle + "号母液瓶资料", "母液泡制");
                                        break;
                                    }
                                    P_int_oricon = Convert.ToInt32(Convert.ToDouble(P_dt_data.Rows[0][P_dt_data.Columns["RealConcentration"]]) * 1000000.00);

                                    strInPut += "原瓶号浓度：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["RealConcentration"]]) + ",";

                                }

                                /*
                                 * $M103 - $M104 设定浓度
                                 */

                                //设定浓度


                                /*
                                 * $M105 染助剂单位
                                 */

                                //根据染助剂代码找到单位
                                P_str_sql = "SELECT UnitOfAccount, AssistantName FROM" +
                                            " assistant_details WHERE AssistantCode = '" + P_str_AssistantCode + "';";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                if (P_dt_data.Rows.Count == 0)
                                {
                                    //FADM_Form.CustomMessageBox.Show("未找到" + P_str_AssistantCode + "染助剂资料", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    byte[] b_send1 = new byte[6];
                                    b_send1[0] = 0x01;
                                    b_send1[1] = 0x06;
                                    ;
                                    b_send1[2] = Convert.ToByte((10045 + i * 100) / 256);
                                    b_send1[3] = Convert.ToByte((10045 + i * 100) % 256);
                                    b_send1[4] = 0x00;
                                    b_send1[5] = 0x02;

                                    FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                    if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                    {
                                        //return;
                                    }
                                    new FADM_Object.MyAlarm("未找到" + P_str_AssistantCode + "染助剂资料", "母液泡制");
                                    break;
                                }
                                int P_int_UnitOfAccount = 0;
                                if (Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["UnitOfAccount"]]) == "%")
                                {
                                    P_int_UnitOfAccount = 0;
                                }
                                else if (Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["UnitOfAccount"]]) == "g/l")
                                {
                                    P_int_UnitOfAccount = 1;
                                }
                                else if (Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["UnitOfAccount"]]) == "G/L")
                                {
                                    P_int_UnitOfAccount = 2;
                                }
                                else
                                {
                                    P_int_UnitOfAccount = 3;
                                }

                                string P_str_AssistantName = Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["AssistantName"]]);

                                strInPut += "染助剂名称：" + P_str_AssistantName + ",";

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
                                P_str_sql = "SELECT * FROM brewing_process WHERE BrewingCode = '" + P_str_BrewingCode + "' ORDER BY StepNum;";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                if (P_dt_data.Rows.Count == 0)
                                {
                                    //FADM_Form.CustomMessageBox.Show("未找到" + P_str_BrewingCode + "调液流程", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    byte[] b_send1 = new byte[6];
                                    b_send1[0] = 0x01;
                                    b_send1[1] = 0x06;
                                    ;
                                    b_send1[2] = Convert.ToByte((10045 + i * 100) / 256);
                                    b_send1[3] = Convert.ToByte((10045 + i * 100) % 256);
                                    b_send1[4] = 0x00;
                                    b_send1[5] = 0x02;

                                    FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                    if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                    {
                                        //return;
                                    }
                                    new FADM_Object.MyAlarm("未找到" + P_str_BrewingCode + "调液流程", "母液泡制");
                                    break;
                                }
                                if (P_dt_data.Rows.Count > 5)
                                {
                                    //FADM_Form.CustomMessageBox.Show(P_str_BrewingCode + "调液流程步骤超过5步", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    byte[] b_send1 = new byte[6];
                                    b_send1[0] = 0x01;
                                    b_send1[1] = 0x06;
                                    ;
                                    b_send1[2] = Convert.ToByte((10045 + i * 100) / 256);
                                    b_send1[3] = Convert.ToByte((10045 + i * 100) % 256);
                                    b_send1[4] = 0x00;
                                    b_send1[5] = 0x02;

                                    FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                    if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                    {
                                        //return;
                                    }
                                    new FADM_Object.MyAlarm(P_str_BrewingCode + "调液流程步骤超过5步", "母液泡制");
                                    break;
                                }

                                int[] P_int_no_1 = { 0, 0, 0, 0, 0 };
                                int[] P_int_data_1 = { 0, 0, 0, 0, 0 };
                                for (int j = 0; j < P_dt_data.Rows.Count; j++)
                                {
                                    string P_str_TechnologyName = Convert.ToString(P_dt_data.Rows[j][P_dt_data.Columns["TechnologyName"]]);
                                    int P_int_data = Convert.ToInt32(P_dt_data.Rows[j][P_dt_data.Columns["ProportionOrTime"]]);
                                    switch (P_str_TechnologyName)
                                    {

                                        case "加大冷水":
                                            //1

                                            P_int_no_1[j] = 1;
                                            P_int_data_1[j] = P_int_data;

                                            break;

                                        case "加小冷水":
                                            //2
                                            P_int_no_1[j] = 2;
                                            P_int_data_1[j] = P_int_data;

                                            break;

                                        case "加热水":
                                            //3
                                            P_int_no_1[j] = 3;
                                            P_int_data_1[j] = P_int_data;

                                            break;

                                        case "手动加染助剂":
                                            //4
                                            P_int_no_1[j] = 4;
                                            P_int_data_1[j] = P_int_data;

                                            break;
                                        case "搅拌":
                                            //5
                                            P_int_no_1[j] = 5;
                                            P_int_data_1[j] = P_int_data;

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

                                string[] P_str_name = { "000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D" };
                                byte[] b_AssistantName = { 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D };
                                int k = 0;
                                for (int j = 0; j < P_str_AssistantName.Length && j < P_str_name.Length; j++)
                                {
                                    Encoding fromEcoding = Encoding.GetEncoding("UTF-8");//返回utf-8的编码
                                    Encoding toEcoding = Encoding.GetEncoding("gb2312");
                                    byte[] fromBytes = fromEcoding.GetBytes(P_str_AssistantName[j].ToString());
                                    byte[] tobytes = Encoding.Convert(fromEcoding, toEcoding, fromBytes);
                                    if (tobytes.Length > 1)
                                    {
                                        P_str_name[k] = tobytes[1].ToString("X") + tobytes[0].ToString("X");
                                        b_AssistantName[2 * k] = tobytes[1];
                                        b_AssistantName[2 * k + 1] = tobytes[0];
                                    }
                                    else if (tobytes.Length == 1)
                                    {
                                        if (k - 1 >= 0)
                                        {
                                            string s = (P_str_name[k - 1]).Substring(0, 2);
                                            if (s == "00")
                                            {
                                                P_str_name[k - 1] = tobytes[0].ToString("X") + P_str_name[k - 1].Substring(2);
                                                //b_AssistantName[2 * (k - 1) + 1] = b_AssistantName[2 * (k - 1)];
                                                b_AssistantName[2 * (k - 1)] = tobytes[0];
                                                k--;
                                            }
                                            else
                                            {
                                                P_str_name[k] = "00" + tobytes[0].ToString("X");
                                                b_AssistantName[2 * k] = 0x00;
                                                b_AssistantName[2 * k + 1] = tobytes[0];
                                            }
                                        }
                                        else
                                        {
                                            P_str_name[k] = "00" + tobytes[0].ToString("X");
                                            b_AssistantName[2 * k] = 0x00;
                                            b_AssistantName[2 * k + 1] = tobytes[0];
                                        }
                                    }
                                    k++;
                                }


                                P_str_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','开始','" + strInPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                                // 清空输入完成标志位
                                b_send = new byte[6];
                                b_send[0] = 0x01;
                                b_send[1] = 0x06;
                                ;
                                b_send[2] = Convert.ToByte((10045 + i * 100) / 256);
                                b_send[3] = Convert.ToByte((10045 + i * 100) % 256);
                                b_send[4] = 0x00;
                                b_send[5] = 0x00;

                                FADM_Object.Communal.MyBrew.WriteAndRead(b_send);
                                if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                {
                                    //return;
                                }


                                //byte[] b_send3 = new byte[63];
                                //b_send3[0] = 0x01;
                                //b_send3[1] = 0x10;
                                //b_send3[2] = Convert.ToByte((9999 + P_int_no * 100) / 256);
                                //b_send3[3] = Convert.ToByte((9999 + P_int_no * 100) % 256);
                                //b_send3[4] = 0x00;
                                //b_send3[5] = 0x1;
                                //b_send3[6] = 0x38;
                                //b_send3[7] = Convert.ToByte(P_int_maxweight / 256);
                                //b_send3[8] = Convert.ToByte(P_int_maxweight % 256);
                                //b_send3[9] = Convert.ToByte(P_int_oricon % (256 * 256) / 256);
                                //b_send3[10] = Convert.ToByte(P_int_oricon % (256 * 256) % 256);
                                //b_send3[11] = Convert.ToByte(P_int_oricon / (256 * 256) / 256);
                                //b_send3[12] = Convert.ToByte(P_int_oricon / (256 * 256) % 256);
                                //b_send3[13] = Convert.ToByte(P_int_setcon % (256 * 256) / 256);
                                //b_send3[14] = Convert.ToByte(P_int_setcon % (256 * 256) % 256);
                                //b_send3[15] = Convert.ToByte(P_int_setcon / (256 * 256) / 256);
                                //b_send3[16] = Convert.ToByte(P_int_setcon / (256 * 256) % 256);
                                //b_send3[17] = Convert.ToByte(P_int_UnitOfAccount / 256);
                                //b_send3[18] = Convert.ToByte(P_int_UnitOfAccount % 256);
                                //b_send3[19] = Convert.ToByte(P_dt_data.Rows.Count / 256);
                                //b_send3[20] = Convert.ToByte(P_dt_data.Rows.Count % 256);
                                //b_send3[21] = Convert.ToByte(1 / 256);
                                //b_send3[22] = Convert.ToByte(1 % 256);
                                //b_send3[23] = Convert.ToByte(P_int_no_1[0] / 256);
                                //b_send3[24] = Convert.ToByte(P_int_no_1[0] % 256);
                                //b_send3[25] = Convert.ToByte(P_int_data_1[0] / 256);
                                //b_send3[26] = Convert.ToByte(P_int_data_1[0] % 256);
                                //b_send3[27] = Convert.ToByte(P_int_no_1[1] / 256);
                                //b_send3[28] = Convert.ToByte(P_int_no_1[1] % 256);
                                //b_send3[29] = Convert.ToByte(P_int_data_1[1] / 256);
                                //b_send3[30] = Convert.ToByte(P_int_data_1[1] % 256);
                                //b_send3[31] = Convert.ToByte(P_int_no_1[2] / 256);
                                //b_send3[32] = Convert.ToByte(P_int_no_1[2] % 256);
                                //b_send3[33] = Convert.ToByte(P_int_data_1[2] / 256);
                                //b_send3[34] = Convert.ToByte(P_int_data_1[2] % 256);
                                //b_send3[35] = Convert.ToByte(P_int_no_1[3] / 256);
                                //b_send3[36] = Convert.ToByte(P_int_no_1[3] % 256);
                                //b_send3[37] = Convert.ToByte(P_int_data_1[3] / 256);
                                //b_send3[38] = Convert.ToByte(P_int_data_1[3] % 256);
                                //b_send3[39] = Convert.ToByte(P_int_no_1[4] / 256);
                                //b_send3[40] = Convert.ToByte(P_int_no_1[4] % 256);
                                //b_send3[41] = Convert.ToByte(P_int_data_1[4] / 256);
                                //b_send3[42] = Convert.ToByte(P_int_data_1[4] % 256);
                                //b_send3[43] = Convert.ToByte(P_int_oribottle / 256);
                                //b_send3[44] = Convert.ToByte(P_int_oribottle % 256);
                                //b_send3[45] = 0x00;
                                //b_send3[46] = 0x00;
                                //b_send3[47] = b_AssistantName[0];
                                //b_send3[48] = b_AssistantName[1];
                                //b_send3[49] = b_AssistantName[2];
                                //b_send3[50] = b_AssistantName[3];
                                //b_send3[51] = b_AssistantName[4];
                                //b_send3[52] = b_AssistantName[5];
                                //b_send3[53] = b_AssistantName[6];
                                //b_send3[54] = b_AssistantName[7];
                                //b_send3[55] = b_AssistantName[8];
                                //b_send3[56] = b_AssistantName[9];
                                //b_send3[57] = b_AssistantName[10];
                                //b_send3[58] = b_AssistantName[11];
                                //b_send3[59] = b_AssistantName[12];
                                //b_send3[60] = b_AssistantName[13];
                                //b_send3[61] = b_AssistantName[14];
                                //b_send3[62] = b_AssistantName[15];



                                //FADM_Object.Communal.MyBrew.WriteAndRead(b_send3);
                                //if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                //{
                                //    //return;
                                //}

                                byte[] b_send3 = new byte[13];
                                b_send3[0] = 0x01;
                                b_send3[1] = 0x10;
                                b_send3[2] = Convert.ToByte((9999 + P_int_no * 100) / 256);
                                b_send3[3] = Convert.ToByte((9999 + P_int_no * 100) % 256);
                                b_send3[4] = 0x00;
                                b_send3[5] = 0x03;
                                b_send3[6] = 0x06;
                                b_send3[7] = Convert.ToByte(P_int_maxweight / 256);
                                b_send3[8] = Convert.ToByte(P_int_maxweight % 256);
                                b_send3[9] = Convert.ToByte(P_int_oricon % (256 * 256) / 256);
                                b_send3[10] = Convert.ToByte(P_int_oricon % (256 * 256) % 256);
                                b_send3[11] = Convert.ToByte(P_int_oricon / (256 * 256) / 256);
                                b_send3[12] = Convert.ToByte(P_int_oricon / (256 * 256) % 256);
                                FADM_Object.Communal.MyBrew.WriteAndRead(b_send3);

                                byte[] b_send4 = new byte[57];
                                b_send4[0] = 0x01;
                                b_send4[1] = 0x10;
                                b_send4[2] = Convert.ToByte((9999 + P_int_no * 100 + 3) / 256);
                                b_send4[3] = Convert.ToByte((9999 + P_int_no * 100 + 3) % 256);
                                b_send4[4] = 0x00;
                                b_send4[5] = 0x19;
                                b_send4[6] = 0x32;

                                b_send4[7] = Convert.ToByte(P_int_setcon % (256 * 256) / 256);
                                b_send4[8] = Convert.ToByte(P_int_setcon % (256 * 256) % 256);
                                b_send4[9] = Convert.ToByte(P_int_setcon / (256 * 256) / 256);
                                b_send4[10] = Convert.ToByte(P_int_setcon / (256 * 256) % 256);
                                b_send4[11] = Convert.ToByte(P_int_UnitOfAccount / 256);
                                b_send4[12] = Convert.ToByte(P_int_UnitOfAccount % 256);
                                b_send4[13] = Convert.ToByte(P_dt_data.Rows.Count / 256);
                                b_send4[14] = Convert.ToByte(P_dt_data.Rows.Count % 256);
                                b_send4[15] = Convert.ToByte(1 / 256);
                                b_send4[16] = Convert.ToByte(1 % 256);
                                b_send4[17] = Convert.ToByte(P_int_no_1[0] / 256);
                                b_send4[18] = Convert.ToByte(P_int_no_1[0] % 256);
                                b_send4[19] = Convert.ToByte(P_int_data_1[0] / 256);
                                b_send4[20] = Convert.ToByte(P_int_data_1[0] % 256);
                                b_send4[21] = Convert.ToByte(P_int_no_1[1] / 256);
                                b_send4[22] = Convert.ToByte(P_int_no_1[1] % 256);
                                b_send4[23] = Convert.ToByte(P_int_data_1[1] / 256);
                                b_send4[24] = Convert.ToByte(P_int_data_1[1] % 256);
                                b_send4[25] = Convert.ToByte(P_int_no_1[2] / 256);
                                b_send4[26] = Convert.ToByte(P_int_no_1[2] % 256);
                                b_send4[27] = Convert.ToByte(P_int_data_1[2] / 256);
                                b_send4[28] = Convert.ToByte(P_int_data_1[2] % 256);
                                b_send4[29] = Convert.ToByte(P_int_no_1[3] / 256);
                                b_send4[30] = Convert.ToByte(P_int_no_1[3] % 256);
                                b_send4[31] = Convert.ToByte(P_int_data_1[3] / 256);
                                b_send4[32] = Convert.ToByte(P_int_data_1[3] % 256);
                                b_send4[33] = Convert.ToByte(P_int_no_1[4] / 256);
                                b_send4[34] = Convert.ToByte(P_int_no_1[4] % 256);
                                b_send4[35] = Convert.ToByte(P_int_data_1[4] / 256);
                                b_send4[36] = Convert.ToByte(P_int_data_1[4] % 256);
                                b_send4[37] = Convert.ToByte(P_int_oribottle / 256);
                                b_send4[38] = Convert.ToByte(P_int_oribottle % 256);
                                b_send4[39] = 0x00;
                                b_send4[40] = 0x00;
                                b_send4[41] = b_AssistantName[0];
                                b_send4[42] = b_AssistantName[1];
                                b_send4[43] = b_AssistantName[2];
                                b_send4[44] = b_AssistantName[3];
                                b_send4[45] = b_AssistantName[4];
                                b_send4[46] = b_AssistantName[5];
                                b_send4[47] = b_AssistantName[6];
                                b_send4[48] = b_AssistantName[7];
                                b_send4[49] = b_AssistantName[8];
                                b_send4[50] = b_AssistantName[9];
                                b_send4[51] = b_AssistantName[10];
                                b_send4[52] = b_AssistantName[11];
                                b_send4[53] = b_AssistantName[12];
                                b_send4[54] = b_AssistantName[13];
                                b_send4[55] = b_AssistantName[14];
                                b_send4[56] = b_AssistantName[15];
                                FADM_Object.Communal.MyBrew.WriteAndRead(b_send4);
                            }

                        }
                    }
                    //台达老款开料机，不支持中文
                    else
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            byte[] b_send = new byte[6];
                            b_send[0] = 0x01;
                            b_send[1] = 0x03;
                            ;
                            b_send[2] = Convert.ToByte((2030 + i * 100) / 256);
                            b_send[3] = Convert.ToByte((2030 + i * 100) % 256);
                            b_send[4] = 0x00;
                            b_send[5] = 0x0B;

                            FADM_Object.Communal.MyBrew.WriteAndRead(b_send);
                            if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                            {
                                throw new Exception("开料机通讯异常");
                            }

                            if (FADM_Object.Communal.MyBrew.datapool.Count < 27)
                                continue;


                            //工位3,4位
                            int P_int_no = (FADM_Object.Communal.MyBrew.datapool[3] << 8 | FADM_Object.Communal.MyBrew.datapool[4]);

                            //瓶号5,6
                            int P_int_bottle = (FADM_Object.Communal.MyBrew.datapool[5] << 8 | FADM_Object.Communal.MyBrew.datapool[6]);

                            //真实浓度7,8,9,10
                            int P_int_realcon = (FADM_Object.Communal.MyBrew.datapool[9] << 24 | FADM_Object.Communal.MyBrew.datapool[10] << 16 | FADM_Object.Communal.MyBrew.datapool[7] << 8 | FADM_Object.Communal.MyBrew.datapool[8]);
                            //真实重量11,12,13,14

                            int P_int_weight = (FADM_Object.Communal.MyBrew.datapool[13] << 24 | FADM_Object.Communal.MyBrew.datapool[14] << 16 | FADM_Object.Communal.MyBrew.datapool[11] << 8 | FADM_Object.Communal.MyBrew.datapool[12]);

                            P_int_weight = P_int_weight < 0 ? 0 : P_int_weight;
                            //瓶号输入完成状态15,16
                            int P_int_bottlefinish = (FADM_Object.Communal.MyBrew.datapool[15] << 8 | FADM_Object.Communal.MyBrew.datapool[16]);

                            //泡制完成状态17,18
                            int P_int_brewfinish = (FADM_Object.Communal.MyBrew.datapool[17] << 8 | FADM_Object.Communal.MyBrew.datapool[18]);


                            //19,20,21,22
                            int P_int_Oweight = (FADM_Object.Communal.MyBrew.datapool[21] << 24 | FADM_Object.Communal.MyBrew.datapool[22] << 16 | FADM_Object.Communal.MyBrew.datapool[19] << 8 | FADM_Object.Communal.MyBrew.datapool[20]);

                            //是否备料23,24
                            int P_int_prebrew = (FADM_Object.Communal.MyBrew.datapool[23] << 8 | FADM_Object.Communal.MyBrew.datapool[24]);

                            if (P_int_brewfinish == 1)
                            {
                                if (P_int_bottle == 0)
                                {
                                    break;
                                }
                                if (P_int_weight == 0)
                                {
                                    DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("返回当前液量为0，查看开料机面板值，如果确实是0请点是，重新获取值请点否", "母液泡制", MessageBoxButtons.YesNo, true);
                                    if (dialogResult == DialogResult.No)
                                    {
                                        break;
                                    }
                                }
                                //复位开料机完成标志位
                                byte[] b_send1 = new byte[6];
                                b_send1[0] = 0x01;
                                b_send1[1] = 0x06;
                                ;
                                b_send1[2] = Convert.ToByte((2037 + i * 100) / 256);
                                b_send1[3] = Convert.ToByte((2037 + i * 100) % 256);
                                b_send1[4] = 0x00;
                                b_send1[5] = 0x00;

                                FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                {
                                    //return;
                                }

                                //泡制完成

                                int P_int_oribottle = P_int_bottle;
                                System.DateTime P_time = System.DateTime.Now;
                                bool P_bl = false;
                                //again:
                                //    string P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + P_int_oribottle + ";";
                                //    DataTable P_dt_bottle = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                                //    if (P_dt_bottle.Rows.Count > 0)
                                //    {

                                //        P_int_oribottle = Convert.ToInt16(P_dt_bottle.Rows[0]["OriginalBottleNum"]);
                                //        if (P_int_oribottle > 0)
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

                                //if (P_int_bottle == Class_SemiAuto.MyMove_XY.Move_XY_BottleNum)
                                //{
                                //    Class_Module.MyModule.Module_Update = true;
                                //}

                                string P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + P_int_bottle + ";";
                                DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                                if (P_dt_data.Rows.Count == 0)
                                {
                                    FADM_Form.CustomMessageBox.Show("未找到" + P_int_bottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
                                    break;
                                }
                                //设定浓度
                                int P_int_setcon = Convert.ToInt32(Convert.ToDouble(P_dt_data.Rows[0][P_dt_data.Columns["SettingConcentration"]]) * 1000000.00);

                                double P_dbl_realcon = Convert.ToDouble(P_int_realcon / 1000000.00);
                                double P_dbl_setcon = Convert.ToDouble(P_int_setcon / 1000000.00);

                                double P_dbl_err = Convert.ToDouble(string.Format("{0:F2}", (((P_dbl_realcon - P_dbl_setcon) / P_dbl_setcon) * 100.00)));
                                P_dbl_err = P_dbl_err < 0 ? -P_dbl_err : P_dbl_err;

                                if (P_dbl_err > 3.00)
                                {
                                    FADM_Form.CustomMessageBox.Show(P_int_bottle + "号瓶设定浓度：" +
                                           Convert.ToDouble(P_dt_data.Rows[0][P_dt_data.Columns["SettingConcentration"]]) +
                                           ",实际浓度：" + string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + ",误差过大", "母液泡制", MessageBoxButtons.OK, false);

                                }
                                else
                                {
                                    //如果是备料，先记录备料表，不更新到母液瓶资料
                                    if (P_int_prebrew == 1)
                                    {
                                        //P_str_sql = "UPDATE pre_brew SET RealConcentration = '" +
                                        //                string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + "'" +
                                        //                ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + "'" +
                                        //                ", BrewingData = '" + P_time + "', AdjustSuccess = 0" +
                                        //                " WHERE BottleNum = " + P_int_bottle + ";";

                                        //先判断备料表有无这个母液瓶备料记录，如果有先删除，保存最新记录
                                        P_str_sql = "Delete FROM pre_brew WHERE BottleNum = " + P_int_bottle + ";";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                                        //保存最新记录
                                        P_str_sql = "Insert Into pre_brew(BottleNum,RealConcentration,CurrentWeight,BrewingData) values(" + P_int_bottle + "," + string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + ","
                                            + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + ",'" + P_time + "');";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                                    }
                                    else
                                    {
                                        //需要针检
                                        if (FADM_Object.Communal._b_isNeedCheck)
                                        {
                                            //更新数据库
                                            P_str_sql = "UPDATE bottle_details SET RealConcentration = '" +
                                                        string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + "'" +
                                                        ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + "'" +
                                                        ", BrewingData = '" + P_time + "', AdjustSuccess = 0" +
                                                        " WHERE BottleNum = " + P_int_bottle + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                                        }
                                        else
                                        {
                                            //更新数据库
                                            P_str_sql = "UPDATE bottle_details SET RealConcentration = '" +
                                                        string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + "'" +
                                                        ", CurrentWeight = '" + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + "'" +
                                                        ", BrewingData = '" + P_time + "' " +
                                                        " WHERE BottleNum = " + P_int_bottle + ";";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                                        }
                                    }
                                }

                                P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + P_int_bottle + ";";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                int oribottle = Convert.ToInt32(P_dt_data.Rows[0][P_dt_data.Columns["OriginalBottleNum"]]);


                                if (oribottle != 0)
                                {
                                    P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + oribottle + ";";
                                    P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                                    double P_dbl_Ocurrent = Convert.ToDouble(P_dt_data.Rows[0]["CurrentWeight"]);

                                    P_dbl_Ocurrent -= (Convert.ToDouble(P_int_Oweight) / 100.00);
                                    P_dbl_Ocurrent = (P_dbl_Ocurrent < 0 ? 0 : P_dbl_Ocurrent);
                                    P_str_sql = "UPDATE bottle_details SET CurrentWeight =  '" +
                                          string.Format("{0:F6}", P_dbl_Ocurrent) + "'" +
                                          " WHERE BottleNum = " + oribottle + ";";
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                                }

                                string strInPut = "";
                                strInPut += "开料瓶号：" + P_int_bottle.ToString() + ",";
                                strInPut += "实际浓度：" + string.Format("{0:F6}", Convert.ToDouble(P_int_realcon) / 1000000.00) + ",";
                                strInPut += "当前液量：" + string.Format("{0:F6}", Convert.ToDouble(P_int_weight) / 1000.00) + ",";

                                P_str_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','结束','" + strInPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                            }
                            else if (P_int_bottlefinish == 1)
                            {
                                //瓶号输入完成
                                if (P_int_bottle == 0)
                                {
                                    break;
                                }

                                //根据瓶号搜索对应资料
                                string P_str_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + P_int_bottle + ";";
                                DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                                if (P_dt_data.Rows.Count == 0)
                                {
                                    //FADM_Form.CustomMessageBox.Show("未找到" + P_int_bottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);

                                    //复位开料机完成标志位
                                    byte[] b_send1 = new byte[6];
                                    b_send1[0] = 0x01;
                                    b_send1[1] = 0x06;
                                    ;
                                    b_send1[2] = Convert.ToByte((2036 + i * 100) / 256);
                                    b_send1[3] = Convert.ToByte((2036 + i * 100) % 256);
                                    b_send1[4] = 0x00;
                                    b_send1[5] = 0x02;

                                    FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                    if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                    {
                                        //return;
                                    }

                                    new FADM_Object.MyAlarm("未找到" + P_int_bottle + "号母液瓶资料", "母液泡制");
                                    break;
                                }

                                string strInPut = "";
                                strInPut += "开料瓶号：" + P_int_bottle.ToString() + ",";
                                strInPut += "调液流程代码：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["BrewingCode"]]) + ",";
                                strInPut += "设定浓度：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["SettingConcentration"]]) + ",";
                                strInPut += "助剂代码：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["AssistantCode"]]) + ",";
                                strInPut += "允许最大调液量：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["AllowMaxWeight"]]) + ",";
                                strInPut += "开稀原瓶号：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["OriginalBottleNum"]]) + ",";

                                //调液流程代码
                                string P_str_BrewingCode = Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["BrewingCode"]]);

                                int P_int_setcon = Convert.ToInt32(Convert.ToDouble(P_dt_data.Rows[0][P_dt_data.Columns["SettingConcentration"]]) * 1000000.00);

                                string P_str_AssistantCode = Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["AssistantCode"]]);

                                /*
                                 * $M100 最大调液量
                                 */

                                //允许最大调液量
                                int P_int_maxweight = Convert.ToInt32(P_dt_data.Rows[0][P_dt_data.Columns["AllowMaxWeight"]]);

                                /*
                                 * $M101 - $M102 原浓度
                                 */

                                //开稀原瓶号
                                int P_int_oribottle = Convert.ToInt32(P_dt_data.Rows[0][P_dt_data.Columns["OriginalBottleNum"]]);


                                //根据原瓶号找到原浓度
                                int P_int_oricon = 0;
                                if (P_int_oribottle != 0)
                                {
                                    P_str_sql = "SELECT RealConcentration FROM bottle_details WHERE BottleNum = " + P_int_oribottle + ";";
                                    P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                    if (P_dt_data.Rows.Count == 0)
                                    {
                                        //FADM_Form.CustomMessageBox.Show("未找到" + P_int_oribottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
                                        //复位输入完成标记位
                                        byte[] b_send1 = new byte[6];
                                        b_send1[0] = 0x01;
                                        b_send1[1] = 0x06;
                                        ;
                                        b_send1[2] = Convert.ToByte((2036 + i * 100) / 256);
                                        b_send1[3] = Convert.ToByte((2036 + i * 100) % 256);
                                        b_send1[4] = 0x00;
                                        b_send1[5] = 0x02;

                                        FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                        if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                        {
                                            //return;
                                        }
                                        new FADM_Object.MyAlarm("未找到" + P_int_oribottle + "号母液瓶资料", "母液泡制");
                                        break;
                                    }
                                    P_int_oricon = Convert.ToInt32(Convert.ToDouble(P_dt_data.Rows[0][P_dt_data.Columns["RealConcentration"]]) * 1000000.00);

                                    strInPut += "原瓶号浓度：" + Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["RealConcentration"]]) + ",";

                                }

                                /*
                                 * $M103 - $M104 设定浓度
                                 */

                                //设定浓度


                                /*
                                 * $M105 染助剂单位
                                 */

                                //根据染助剂代码找到单位
                                P_str_sql = "SELECT UnitOfAccount, AssistantName FROM" +
                                            " assistant_details WHERE AssistantCode = '" + P_str_AssistantCode + "';";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                if (P_dt_data.Rows.Count == 0)
                                {
                                    //FADM_Form.CustomMessageBox.Show("未找到" + P_str_AssistantCode + "染助剂资料", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    byte[] b_send1 = new byte[6];
                                    b_send1[0] = 0x01;
                                    b_send1[1] = 0x06;
                                    ;
                                    b_send1[2] = Convert.ToByte((2036 + i * 100) / 256);
                                    b_send1[3] = Convert.ToByte((2036 + i * 100) % 256);
                                    b_send1[4] = 0x00;
                                    b_send1[5] = 0x02;

                                    FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                    if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                    {
                                        //return;
                                    }
                                    new FADM_Object.MyAlarm("未找到" + P_str_AssistantCode + "染助剂资料", "母液泡制");
                                    break;
                                }
                                int P_int_UnitOfAccount = 0;
                                if (Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["UnitOfAccount"]]) == "%")
                                {
                                    P_int_UnitOfAccount = 0;
                                }
                                else if (Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["UnitOfAccount"]]) == "g/l")
                                {
                                    P_int_UnitOfAccount = 1;
                                }
                                else if (Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["UnitOfAccount"]]) == "G/L")
                                {
                                    P_int_UnitOfAccount = 2;
                                }
                                else
                                {
                                    P_int_UnitOfAccount = 3;
                                }

                                string P_str_AssistantName = Convert.ToString(P_dt_data.Rows[0][P_dt_data.Columns["AssistantName"]]);

                                strInPut += "染助剂名称：" + P_str_AssistantName + ",";


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
                                P_str_sql = "SELECT * FROM brewing_process WHERE BrewingCode = '" + P_str_BrewingCode + "' ORDER BY StepNum;";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                if (P_dt_data.Rows.Count == 0)
                                {
                                    //FADM_Form.CustomMessageBox.Show("未找到" + P_str_BrewingCode + "调液流程", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    byte[] b_send1 = new byte[6];
                                    b_send1[0] = 0x01;
                                    b_send1[1] = 0x06;
                                    ;
                                    b_send1[2] = Convert.ToByte((2036 + i * 100) / 256);
                                    b_send1[3] = Convert.ToByte((2036 + i * 100) % 256);
                                    b_send1[4] = 0x00;
                                    b_send1[5] = 0x02;

                                    FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                    if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                    {
                                        //return;
                                    }
                                    new FADM_Object.MyAlarm("未找到" + P_str_BrewingCode + "调液流程", "母液泡制");
                                    break;
                                }
                                if (P_dt_data.Rows.Count > 5)
                                {
                                    //FADM_Form.CustomMessageBox.Show(P_str_BrewingCode + "调液流程步骤超过5步", "母液泡制", MessageBoxButtons.OK, false);
                                    //复位输入完成标记位
                                    byte[] b_send1 = new byte[6];
                                    b_send1[0] = 0x01;
                                    b_send1[1] = 0x06;
                                    ;
                                    b_send1[2] = Convert.ToByte((2036 + i * 100) / 256);
                                    b_send1[3] = Convert.ToByte((2036 + i * 100) % 256);
                                    b_send1[4] = 0x00;
                                    b_send1[5] = 0x02;

                                    FADM_Object.Communal.MyBrew.WriteAndRead(b_send1);
                                    if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                    {
                                        //return;
                                    }
                                    new FADM_Object.MyAlarm(P_str_BrewingCode + "调液流程步骤超过5步", "母液泡制");
                                    break;
                                }

                                int[] P_int_no_1 = { 0, 0, 0, 0, 0 };
                                int[] P_int_data_1 = { 0, 0, 0, 0, 0 };
                                for (int j = 0; j < P_dt_data.Rows.Count; j++)
                                {
                                    string P_str_TechnologyName = Convert.ToString(P_dt_data.Rows[j][P_dt_data.Columns["TechnologyName"]]);
                                    int P_int_data = Convert.ToInt32(P_dt_data.Rows[j][P_dt_data.Columns["ProportionOrTime"]]);
                                    switch (P_str_TechnologyName)
                                    {

                                        case "加大冷水":
                                            //1

                                            P_int_no_1[j] = 1;
                                            P_int_data_1[j] = P_int_data;

                                            break;

                                        case "加小冷水":
                                            //2
                                            P_int_no_1[j] = 2;
                                            P_int_data_1[j] = P_int_data;

                                            break;

                                        case "加热水":
                                            //3
                                            P_int_no_1[j] = 3;
                                            P_int_data_1[j] = P_int_data;

                                            break;

                                        case "手动加染助剂":
                                            //4
                                            P_int_no_1[j] = 4;
                                            P_int_data_1[j] = P_int_data;

                                            break;
                                        case "搅拌":
                                            //5
                                            P_int_no_1[j] = 5;
                                            P_int_data_1[j] = P_int_data;

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

                                string[] P_str_name = { "000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D" };
                                byte[] b_AssistantName = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                //int k = 0;
                                //for (int j = 0; j < P_str_AssistantName.Length && j < P_str_name.Length; j++)
                                //{
                                //    Encoding fromEcoding = Encoding.GetEncoding("UTF-8");//返回utf-8的编码
                                //    Encoding toEcoding = Encoding.GetEncoding("gb2312");
                                //    byte[] fromBytes = fromEcoding.GetBytes(P_str_AssistantName[j].ToString());
                                //    byte[] tobytes = Encoding.Convert(fromEcoding, toEcoding, fromBytes);
                                //    if (tobytes.Length > 1)
                                //    {
                                //        P_str_name[k] = tobytes[1].ToString("X") + tobytes[0].ToString("X");
                                //        b_AssistantName[2 * k] = tobytes[1];
                                //        b_AssistantName[2 * k + 1] = tobytes[0];
                                //    }
                                //    else if (tobytes.Length == 1)
                                //    {
                                //        if (k - 1 >= 0)
                                //        {
                                //            string s = (P_str_name[k - 1]).Substring(0, 2);
                                //            if (b_AssistantName[2 * (k -1)] == 0x00)
                                //            {
                                //                P_str_name[k - 1] = tobytes[0].ToString("X") + P_str_name[k - 1].Substring(2);
                                //                b_AssistantName[2 * (k - 1) + 1] = b_AssistantName[2 * (k - 1)];
                                //                b_AssistantName[2 * (k - 1)] = tobytes[0];
                                //                k--;
                                //            }
                                //            else
                                //            {
                                //                P_str_name[k] = "00" + tobytes[0].ToString("X");
                                //                b_AssistantName[2 * k] = 0x00;
                                //                b_AssistantName[2 * k + 1] = tobytes[0];
                                //            }
                                //        }
                                //        else
                                //        {
                                //            P_str_name[k] = "00" + tobytes[0].ToString("X");
                                //            b_AssistantName[2 * k] = 0x00;
                                //            b_AssistantName[2 * k + 1] = tobytes[0];
                                //        }
                                //    }
                                //    k++;
                                //}

                                int time = 0;
                                int time1 = 0;
                                bool flag = false;
                                foreach (char c in P_str_AssistantName)
                                {
                                    if (time > P_str_name.Length - 1)
                                    {
                                        break;
                                    }

                                    string s2 = Convert.ToInt32(c).ToString("X");
                                    if (s2.Length > 2)
                                    {
                                        s2 = "0D";

                                        int code = FileRWini.getklZh(c);
                                        if (code == 0)//没找到我要翻译的中文
                                        {
                                            if (flag)
                                            {
                                                b_AssistantName[2 * (time1 - 1) + 1] = 0x0D;
                                                flag = false;
                                                time1--;
                                            }
                                            else
                                            {
                                                b_AssistantName[2 * time1] = 0x0D;
                                                b_AssistantName[2 * time1 + 1] = 0x00;
                                                flag = true;
                                            }
                                        }
                                        else
                                        {
                                            b_AssistantName[2 * time1] = Convert.ToByte(code % 256);
                                            b_AssistantName[2 * time1 + 1] = Convert.ToByte(code / 256);
                                            flag = false;
                                        }
                                    }
                                    else
                                    {
                                        if (flag)
                                        {
                                            b_AssistantName[2 * (time1 - 1) + 1] = Convert.ToByte(Convert.ToInt32(c));
                                            flag = false;
                                            time1--;
                                        }
                                        else
                                        {
                                            b_AssistantName[2 * time1] = Convert.ToByte(Convert.ToInt32(c));
                                            b_AssistantName[2 * time1 + 1] = 0x00;
                                            flag = true;
                                        }
                                    }

                                    time1++;
                                    time++;


                                }



                                P_str_sql = "Insert into brew_run_table(MyDateTime,State,Info) values ('" + DateTime.Now.ToString() + "','开始','" + strInPut + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);


                                byte[] b_send3 = new byte[63];
                                b_send3[0] = 0x01;
                                b_send3[1] = 0x10;
                                b_send3[2] = Convert.ToByte((2000 + P_int_no * 100) / 256);
                                b_send3[3] = Convert.ToByte((2000 + P_int_no * 100) % 256);
                                b_send3[4] = 0x00;
                                b_send3[5] = 0x1C;
                                b_send3[6] = 0x38;
                                b_send3[7] = Convert.ToByte(P_int_maxweight / 256);
                                b_send3[8] = Convert.ToByte(P_int_maxweight % 256);
                                b_send3[9] = Convert.ToByte(P_int_oricon % (256 * 256) / 256);
                                b_send3[10] = Convert.ToByte(P_int_oricon % (256 * 256) % 256);
                                b_send3[11] = Convert.ToByte(P_int_oricon / (256 * 256) / 256);
                                b_send3[12] = Convert.ToByte(P_int_oricon / (256 * 256) % 256);
                                b_send3[13] = Convert.ToByte(P_int_setcon % (256 * 256) / 256);
                                b_send3[14] = Convert.ToByte(P_int_setcon % (256 * 256) % 256);
                                b_send3[15] = Convert.ToByte(P_int_setcon / (256 * 256) / 256);
                                b_send3[16] = Convert.ToByte(P_int_setcon / (256 * 256) % 256);
                                b_send3[17] = Convert.ToByte(P_int_UnitOfAccount / 256);
                                b_send3[18] = Convert.ToByte(P_int_UnitOfAccount % 256);
                                b_send3[19] = Convert.ToByte(P_dt_data.Rows.Count / 256);
                                b_send3[20] = Convert.ToByte(P_dt_data.Rows.Count % 256);
                                b_send3[21] = Convert.ToByte(1 / 256);
                                b_send3[22] = Convert.ToByte(1 % 256);
                                b_send3[23] = Convert.ToByte(P_int_no_1[0] / 256);
                                b_send3[24] = Convert.ToByte(P_int_no_1[0] % 256);
                                b_send3[25] = Convert.ToByte(P_int_data_1[0] / 256);
                                b_send3[26] = Convert.ToByte(P_int_data_1[0] % 256);
                                b_send3[27] = Convert.ToByte(P_int_no_1[1] / 256);
                                b_send3[28] = Convert.ToByte(P_int_no_1[1] % 256);
                                b_send3[29] = Convert.ToByte(P_int_data_1[1] / 256);
                                b_send3[30] = Convert.ToByte(P_int_data_1[1] % 256);
                                b_send3[31] = Convert.ToByte(P_int_no_1[2] / 256);
                                b_send3[32] = Convert.ToByte(P_int_no_1[2] % 256);
                                b_send3[33] = Convert.ToByte(P_int_data_1[2] / 256);
                                b_send3[34] = Convert.ToByte(P_int_data_1[2] % 256);
                                b_send3[35] = Convert.ToByte(P_int_no_1[3] / 256);
                                b_send3[36] = Convert.ToByte(P_int_no_1[3] % 256);
                                b_send3[37] = Convert.ToByte(P_int_data_1[3] / 256);
                                b_send3[38] = Convert.ToByte(P_int_data_1[3] % 256);
                                b_send3[39] = Convert.ToByte(P_int_no_1[4] / 256);
                                b_send3[40] = Convert.ToByte(P_int_no_1[4] % 256);
                                b_send3[41] = Convert.ToByte(P_int_data_1[4] / 256);
                                b_send3[42] = Convert.ToByte(P_int_data_1[4] % 256);
                                b_send3[43] = b_AssistantName[0];
                                b_send3[44] = b_AssistantName[1];
                                b_send3[45] = b_AssistantName[2];
                                b_send3[46] = b_AssistantName[3];
                                b_send3[47] = b_AssistantName[4];
                                b_send3[48] = b_AssistantName[5];
                                b_send3[49] = b_AssistantName[6];
                                b_send3[50] = b_AssistantName[7];
                                b_send3[51] = b_AssistantName[8];
                                b_send3[52] = b_AssistantName[9];
                                b_send3[53] = b_AssistantName[10];
                                b_send3[54] = b_AssistantName[11];
                                b_send3[55] = b_AssistantName[12];
                                b_send3[56] = b_AssistantName[13];
                                b_send3[57] = b_AssistantName[14];
                                b_send3[58] = b_AssistantName[15];
                                b_send3[59] = 0x00;
                                b_send3[60] = 0x00;
                                b_send3[61] = Convert.ToByte(P_int_oribottle / 256);
                                b_send3[62] = Convert.ToByte(P_int_oribottle % 256);


                                FADM_Object.Communal.MyBrew.WriteAndRead(b_send3);
                                if (Lib_SerialPort.HMI.BrewHMI.nState != 0)
                                {
                                    //return;
                                }

                            }

                        }
                    }

                    Thread.Sleep(1);

                }
            }
            catch (Exception ex)
            {
                FADM_Object.Communal._b_brewErr = true;
                if (1 == Lib_Card.Configure.Parameter.Machine_Opening_Type)
                {
                    FADM_Object.Communal._s_brewVersion = "";
                }

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    new SmartDyeing.FADM_Object.MyAlarm(ex.Message, "开料机通讯", false, 1);
                else
                    new SmartDyeing.FADM_Object.MyAlarm(ex.Message, "Cutting machine communication", false, 1);


            }
        }
    }
}
