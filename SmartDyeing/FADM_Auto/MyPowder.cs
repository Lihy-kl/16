using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartDyeing.FADM_Auto
{
    class MyPowder
    {
        

        /// <summary>
        /// 称粉
        /// </summary>
        /// <param name="batchnum"></param>
        public static void powder(string batchnum)
        {
            try
            {
                if (Communal._b_PowerAB)
                {
                    string P_str_sql = "SELECT * FROM drop_details " +
                                   " WHERE BatchName = '" + batchnum + "' AND" +
                                   " Finish = 0 AND " +
                                   " CupNum <= '" + (Lib_Card.Configure.Parameter.Machine_Cup_Total) + "' AND" +
                                   " (BottleNum = 200 OR BottleNum = 201) ORDER BY CupNum;";
                    DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                    if (P_dt_data.Rows.Count == 0)
                    {
                        return;
                    }

                //if (Class_Object.MyObject.Module_Powder == null)
                //{
                //    P_str_sql = "UPDATE drop_system.drop_details SET RealDropWeight = '" + 0.00 + "'," +
                //               " Finish = 1 WHERE BatchName = '" + batchnum + "' AND ( BottleNum = 201 OR BottleNum = 200);";
                //    MyObject.Object_Server.getsqlcom(P_str_sql);
                //    return;
                //}





                again:
                    //获取称粉机状态

                    int[] v = new int[1];
                    int nRet = FADM_Object.Communal.Powder.Read(10, 1, ref v);
                    if (nRet == -1)
                    {
                        goto again;
                    }




                    int P_int_state = v[0];

                    /* 状态：
                     * 0:等待数据
                     * 1:接收数据中
                     * 2:接收完成
                     * 3:正在运行
                     * 4:滴料完成
                     * 5:上传完成
                     * 6:机台停止
                     * 7:数据停止接收
                     * 
                     */

                    switch (P_int_state)
                    {
                        case 0:
                            {
                                //等待数据
                                P_str_sql = "SELECT * FROM drop_details " +
                                       " WHERE BatchName = '" + batchnum + "' AND" +
                                       " Finish = 0 AND " +
                                       " CupNum <= '" + (Lib_Card.Configure.Parameter.Machine_Cup_Total) + "' AND" +
                                       " (BottleNum = 200 OR BottleNum = 201) ORDER BY CupNum;";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                if (P_dt_data.Rows.Count == 0)
                                {
                                    return;
                                }
                                //转变为数据接收中

                                v = new int[1];
                                v[0] = 1;
                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(10, v);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                Thread.Sleep(1000);

                                int P_int_Plate = 1;
                                while (true)
                                {
                                    //根据轮数读取需加量
                                    P_str_sql = "SELECT * FROM drop_details " +
                                                " WHERE BatchName = '" + batchnum + "' AND" +
                                                " Finish = 0 AND CupNum >'" + 12 * (P_int_Plate - 1) + "' AND" +
                                                " CupNum <= '" + 12 * P_int_Plate + "' AND" +
                                                " (BottleNum = 200 OR BottleNum = 201) ORDER BY CupNum;";
                                    P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                    if (P_dt_data.Rows.Count == 0)
                                    {
                                        P_int_Plate++;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    Thread.Sleep(1);
                                }
                                int[] Data = {
                                0x0101,0,0,2, 0x0102, 0, 0, 2, 0x0103, 0, 0, 2,
                            0x0201,0,0,2, 0x0202, 0, 0, 2, 0x0203, 0, 0, 2,
                            0x0301,0,0,2, 0x0302, 0, 0, 2, 0x0303, 0, 0, 2,
                            0x0401,0,0,2, 0x0402, 0, 0, 2, 0x0403, 0, 0, 2,
                            0x0501,0,0,2, 0x0502, 0, 0, 2, 0x0503, 0, 0, 2,
                            0x0601,0,0,2, 0x0602, 0, 0, 2, 0x0603, 0, 0, 2,
                            0x0701,0,0,2, 0x0702, 0, 0, 2, 0x0703, 0, 0, 2,
                            0x0801,0,0,2, 0x0802, 0, 0, 2, 0x0803, 0, 0, 2,
                            0x0901,0,0,2, 0x0902, 0, 0, 2, 0x0903, 0, 0, 2,
                            0x0A01,0,0,2, 0x0A02, 0, 0, 2, 0x0A03, 0, 0, 2,
                            0x0B01,0,0,2, 0x0B02, 0, 0, 2, 0x0B03, 0, 0, 2,
                            0x0C01,0,0,2, 0x0C02, 0, 0, 2, 0x0C03, 0, 0, 2};
                                string[] P_str_data = {"0101","0000","0000","0002","0102","0000","0000","0002","0103","0000","0000","0002",
                                               "0201","0000","0000","0002","0202","0000","0000","0002","0203","0000","0000","0002",
                                               "0301","0000","0000","0002","0302","0000","0000","0002","0303","0000","0000","0002",
                                               "0401","0000","0000","0002","0402","0000","0000","0002","0403","0000","0000","0002",
                                               "0501","0000","0000","0002","0502","0000","0000","0002","0503","0000","0000","0002",
                                               "0601","0000","0000","0002","0602","0000","0000","0002","0603","0000","0000","0002",
                                               "0701","0000","0000","0002","0702","0000","0000","0002","0703","0000","0000","0002",
                                               "0801","0000","0000","0002","0802","0000","0000","0002","0803","0000","0000","0002",
                                               "0901","0000","0000","0002","0902","0000","0000","0002","0903","0000","0000","0002",
                                               "0A01","0000","0000","0002","0A02","0000","0000","0002","0A03","0000","0000","0002",
                                               "0B01","0000","0000","0002","0B02","0000","0000","0002","0B03","0000","0000","0002",
                                               "0C01","0000","0000","0002","0C02","0000","0000","0002","0C03","0000","0000","0002"};



                                foreach (DataRow dr in P_dt_data.Rows)
                                {
                                    int P_int_cup = Convert.ToInt16(dr["CupNum"]);
                                    int P_int_bottle = Convert.ToInt16(dr["BottleNum"]);

                                    int P_int_ObjectWeight = Convert.ToInt32(Convert.ToDouble(dr["ObjectDropWeight"]) * 100.00);
                                    string P_str_ObjectWeight_1 = P_int_ObjectWeight.ToString("X").PadLeft(8, '0').Substring(4, 4);
                                    string P_str_ObjectWeight_2 = P_int_ObjectWeight.ToString("X").PadLeft(8, '0').Substring(0, 4);
                                    if (P_int_bottle == 200)
                                    {
                                        //A粉数据
                                        P_str_data[P_int_cup % 12 == 0 ? 133 : (P_int_cup % 12 - 1) * 12 + 1] = P_str_ObjectWeight_1;
                                        P_str_data[P_int_cup % 12 == 0 ? 134 : (P_int_cup % 12 - 1) * 12 + 2] = P_str_ObjectWeight_2;

                                        Data[P_int_cup % 12 == 0 ? 133 : (P_int_cup % 12 - 1) * 12 + 1] = P_int_ObjectWeight % (256 * 256);
                                        Data[P_int_cup % 12 == 0 ? 134 : (P_int_cup % 12 - 1) * 12 + 2] = P_int_ObjectWeight / (256 * 256);
                                    }
                                    else
                                    {
                                        //B粉数据
                                        P_str_data[P_int_cup % 12 == 0 ? 141 : (P_int_cup % 12 - 1) * 12 + 9] = P_str_ObjectWeight_1;
                                        P_str_data[P_int_cup % 12 == 0 ? 142 : (P_int_cup % 12 - 1) * 12 + 10] = P_str_ObjectWeight_2;

                                        Data[P_int_cup % 12 == 0 ? 141 : (P_int_cup % 12 - 1) * 12 + 9] = P_int_ObjectWeight % (256 * 256);
                                        Data[P_int_cup % 12 == 0 ? 142 : (P_int_cup % 12 - 1) * 12 + 10] = P_int_ObjectWeight / (256 * 256);
                                    }

                                }

                                string P_str_data_1 = null,
                                       P_str_data_2 = null;
                                int[] Data1 = new int[72];
                                int[] Data2 = new int[72];
                                for (int i = 0; i < P_str_data.Length; i++)
                                {
                                    if (i < P_str_data.Length / 2)
                                    {
                                        P_str_data_1 += ((P_str_data[i].PadLeft(4, '0')).Insert(2, " ") + " ");

                                    }
                                    else
                                    {
                                        P_str_data_2 += ((P_str_data[i].PadLeft(4, '0')).Insert(2, " ") + " ");
                                    }
                                }

                                for (int i = 0; i < Data.Length; i++)
                                {
                                    if (i < Data.Length / 2)
                                    {
                                        Data1[i] = Data[i];

                                    }
                                    else
                                    {
                                        Data2[i - 72] = Data[i];
                                    }
                                }


                                //P_str_message = "01 10 " + (20.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 48 90 " + P_str_data_1;
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(20, Data1);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                //P_str_message = "01 10 " + (92.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 48 90 " + P_str_data_2;
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(92, Data2);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                //更新当前批次号
                                int P_int_batchnum_1 = Convert.ToInt16(batchnum.Substring(0, 4));
                                int P_int_batchnum_2 = Convert.ToInt16(batchnum.Substring(4, 4));
                                int P_int_batchnum_3 = Convert.ToInt16(batchnum.Substring(8, 4));

                                int[] Data3 = new int[4];
                                Data3[0] = Convert.ToInt32(P_int_batchnum_1.ToString());
                                Data3[1] = Convert.ToInt32(P_int_batchnum_2.ToString());
                                Data3[2] = Convert.ToInt32(P_int_batchnum_3.ToString());
                                Data3[3] = Convert.ToInt32(P_int_Plate.ToString());


                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(5000, Data3);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }


                                //更改称粉机接收完成状态
                                v[0] = 2;
                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(10, v);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                Thread.Sleep(1000);

                                goto again;
                            }
                        case 2:
                            {
                                //读取当前批次号
                                //P_str_message = "01 03 " + (5000.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 04";
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                int[] v1 = new int[4];
                                int nRet1 = FADM_Object.Communal.Powder.Read(5000, 4, ref v1);
                                if (nRet1 == -1)
                                {
                                    goto again;
                                }



                                //if (Class_Module.MyModule.Module_Com1_Error)
                                //{
                                //    return;
                                //}

                                //if (Class_Object.MyObject.Module_Powder.P_str_rec == null || Class_Object.MyObject.Module_Powder.P_str_rec.Length < 26)
                                //{
                                //    goto again;
                                //}

                                string P_str_batchnum_1 = v1[0].ToString().PadLeft(4, '0');
                                string P_str_batchnum_2 = v1[1].ToString().PadLeft(4, '0');
                                string P_str_batchnum_3 = v1[2].ToString().PadLeft(4, '0');

                                string P_str_batchnum = P_str_batchnum_1 + P_str_batchnum_2 + P_str_batchnum_3;

                                if (P_str_batchnum != batchnum)
                                {
                                    //更改称粉机数据停止接受状态
                                    v = new int[1];
                                    v[0] = 7;
                                    while (true)
                                    {
                                        nRet = FADM_Object.Communal.Powder.Write(10, v);
                                        if (nRet == 0)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(1);
                                    }
                                }

                                Thread.Sleep(1000);
                                goto again;


                            }
                        case 4:
                            {
                                //滴料完成
                                Thread.Sleep(1000);

                                //读取当前批次号
                                v = new int[1];
                                v[0] = 4;
                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(10, v);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                //if (Class_Module.MyModule.Module_Com1_Error)
                                //{
                                //    return;
                                //}

                                //if (Class_Object.MyObject.Module_Powder.P_str_rec == null || Class_Object.MyObject.Module_Powder.P_str_rec.Length < 26)
                                //{
                                //    goto again;
                                //}

                                int[] v1 = new int[4];
                                int nRet1 = FADM_Object.Communal.Powder.Read(5000, 4, ref v1);
                                if (nRet1 == -1)
                                {
                                    goto again;
                                }

                                string P_str_batchnum_1 = v1[0].ToString().PadLeft(4, '0');
                                string P_str_batchnum_2 = v1[1].ToString().PadLeft(4, '0');
                                string P_str_batchnum_3 = v1[2].ToString().PadLeft(4, '0');
                                int P_int_PlateRec = Convert.ToInt32(v1[3].ToString());

                                string P_str_batchnum = P_str_batchnum_1 + P_str_batchnum_2 + P_str_batchnum_3;

                                if (P_str_batchnum != batchnum)
                                {
                                    //当前称粉机批次和滴液机批次不是同一批次
                                    new FADM_Object.MyAlarm("称粉机请切换为等待数据状态！", 1);
                                    goto again;

                                }

                                int[] Data4 = new int[72];
                                int[] Data5 = new int[72];

                                //P_str_message = "01 03 " + (20.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 48";
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                //string P_str_data_1 = Class_Object.MyObject.Module_Powder.P_str_rec;

                                int nRet4 = FADM_Object.Communal.Powder.Read(20, 72, ref Data4);
                                if (nRet4 == -1)
                                {
                                    goto again;
                                }


                                //P_str_message = "01 03 " + (92.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 48";
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                //string P_str_data_2 = Class_Object.MyObject.Module_Powder.P_str_rec;

                                int nRet5 = FADM_Object.Communal.Powder.Read(92, 72, ref Data5);
                                if (nRet5 == -1)
                                {
                                    goto again;
                                }


                                //P_str_message = "01 06 " + (10.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 05 ";
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                v = new int[1];
                                v[0] = 5;
                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(10, v);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }


                                int[] DataTotal = new int[144];
                                for (int i = 0; i < DataTotal.Length; i++)
                                {
                                    if (i < DataTotal.Length / 2)
                                    {
                                        //前6杯数据
                                        DataTotal[i] = Data4[i];

                                    }
                                    else
                                    {
                                        //后6杯数据
                                        DataTotal[i] = Data5[i - 72];
                                    }
                                }




                                P_str_sql = "SELECT * FROM drop_details " +
                                            " WHERE BatchName = '" + batchnum + "' AND" +
                                            " Finish = 0 AND CupNum >'" + 12 * (P_int_PlateRec - 1) + "' AND" +
                                            " CupNum <= '" + 12 * P_int_PlateRec + "' AND" +
                                            " (BottleNum = 200 OR BottleNum = 201) ORDER BY CupNum;";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);


                                foreach (DataRow dr in P_dt_data.Rows)
                                {
                                    int P_int_cup = Convert.ToInt16(dr["CupNum"]);
                                    int P_int_bottle = Convert.ToInt16(dr["BottleNum"]);

                                    if (P_int_bottle == 200)
                                    {
                                        //A粉数据
                                        int P_str_weight_real_1 = DataTotal[((P_int_cup % 12 == 0 ? 12 : P_int_cup % 12) - 1) * 12 + 1];
                                        int P_str_weight_real_2 = DataTotal[((P_int_cup % 12 == 0 ? 12 : P_int_cup % 12) - 1) * 12 + 2];

                                        string P_str_weight_real = string.Format("{0:F2}", (Convert.ToDouble(P_str_weight_real_2 * 256 * 256 + P_str_weight_real_1) / 100.00));

                                        P_str_sql = "UPDATE drop_details SET RealDropWeight = '" + P_str_weight_real + "'," +
                                                    " Finish = 1 WHERE BatchName = '" + batchnum + "' AND CupNum = '" + P_int_cup + "' AND BottleNum = 200;";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                                        P_str_sql = "UPDATE history_details SET RealDropWeight = '" + P_str_weight_real + "'" +
                                                    " WHERE BatchName = '" + batchnum + "' AND CupNum = '" + P_int_cup + "' AND BottleNum = 200;";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);


                                    }
                                    else
                                    {
                                        //B粉数据                              
                                        int P_str_weight_real_1 = DataTotal[((P_int_cup % 12 == 0 ? 12 : P_int_cup % 12) - 1) * 12 + 9];
                                        int P_str_weight_real_2 = DataTotal[((P_int_cup % 12 == 0 ? 12 : P_int_cup % 12) - 1) * 12 + 10];

                                        string P_str_weight_real = string.Format("{0:F2}", (Convert.ToDouble(P_str_weight_real_2 * 256 * 256 + P_str_weight_real_1) / 100.00));

                                        P_str_sql = "UPDATE drop_details SET RealDropWeight = '" + P_str_weight_real + "'," +
                                                    " Finish = 1 WHERE BatchName = '" + batchnum + "' AND CupNum = '" + P_int_cup + "' AND BottleNum = 201;";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                                        P_str_sql = "UPDATE history_details SET RealDropWeight = '" + P_str_weight_real + "'" +
                                                    " WHERE BatchName = '" + batchnum + "' AND CupNum = '" + P_int_cup + "' AND BottleNum = 201;";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);


                                    }



                                }

                                //重新判断是否完成滴液



                                goto again;
                            }
                        default:
                            goto again;

                    }
                }
                //两个通道就称同一个粉，联缘使用模式
                else
                {
                    string P_str_sql = "SELECT * FROM drop_details " +
                                   " WHERE BatchName = '" + batchnum + "' AND" +
                                   " Finish = 0 AND " +
                                   " CupNum <= '" + (Lib_Card.Configure.Parameter.Machine_Cup_Total) + "' AND" +
                                   " BottleNum = 200  ORDER BY CupNum;";
                    DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                    if (P_dt_data.Rows.Count == 0)
                    {
                        return;
                    }

                //if (Class_Object.MyObject.Module_Powder == null)
                //{
                //    P_str_sql = "UPDATE drop_system.drop_details SET RealDropWeight = '" + 0.00 + "'," +
                //               " Finish = 1 WHERE BatchName = '" + batchnum + "' AND ( BottleNum = 201 OR BottleNum = 200);";
                //    MyObject.Object_Server.getsqlcom(P_str_sql);
                //    return;
                //}





                again:
                    //获取称粉机状态

                    int[] v = new int[1];
                    int nRet = FADM_Object.Communal.Powder.Read(10, 1, ref v);
                    if (nRet == -1)
                    {
                        goto again;
                    }


                    if (FADM_Object.Communal.ReadMachineStatus() == 0)
                    {
                        //更改称粉机数据停止接受状态
                        v = new int[1];
                        v[0] = 7;
                        while (true)
                        {
                            nRet = FADM_Object.Communal.Powder.Write(10, v);
                            if (nRet == 0)
                            {
                                break;
                            }
                            Thread.Sleep(1);
                        }
                        return;
                    }


                    int P_int_state = v[0];
                    if (P_int_state == 2560)
                    {
                        FADM_Object.Communal.Powder.ReConnect();
                        goto again;
                    }
                    

                    /* 状态：
                     * 0:等待数据
                     * 1:接收数据中
                     * 2:接收完成
                     * 3:正在运行
                     * 4:滴料完成
                     * 5:上传完成
                     * 6:机台停止
                     * 7:数据停止接收
                     * 
                     */


                    switch (P_int_state)
                    {
                        case 0:
                            {
                                //等待数据
                                P_str_sql = "SELECT * FROM drop_details " +
                                       " WHERE BatchName = '" + batchnum + "' AND" +
                                       " Finish = 0 AND " +
                                       " CupNum <= '" + (Lib_Card.Configure.Parameter.Machine_Cup_Total) + "' AND" +
                                       " BottleNum = 200  ORDER BY CupNum;";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                if (P_dt_data.Rows.Count == 0)
                                {
                                    return;
                                }
                                //转变为数据接收中

                                v = new int[1];
                                v[0] = 1;
                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(10, v);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                Thread.Sleep(1000);

                                int P_int_Plate = 1;
                                while (true)
                                {
                                    //根据轮数读取需加量
                                    P_str_sql = "SELECT * FROM drop_details " +
                                                " WHERE BatchName = '" + batchnum + "' AND" +
                                                " Finish = 0 AND CupNum >'" + 18 * (P_int_Plate - 1) + "' AND" +
                                                " CupNum <= '" + 18 * P_int_Plate + "' AND" +
                                                " BottleNum = 200 ORDER BY CupNum;";
                                    P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                                    if (P_dt_data.Rows.Count == 0)
                                    {
                                        P_int_Plate++;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    Thread.Sleep(1);
                                }
                                int[] Data = {
                                0x0101,0,0,2,0x0201,0,0,2,0x0301,0,0,2, 0x0401,0,0,2,0x0501,0,0,2,0x0601,0,0,2,
                            0x0701,0,0,2,0x0801,0,0,2, 0x0901,0,0,2,0x0A01,0,0,2,0x0B01,0,0,2,0x0C01,0,0,2,
                            0x0D01,0,0,2,0x0E01,0,0,2,0x0F01,0,0,2,0x1001,0,0,2,0x1101,0,0,2,0x1201,0,0,2};
                                string[] P_str_data = {"0101","0000","0000","0002",
                                               "0201","0000","0000","0002",
                                               "0301","0000","0000","0002",
                                               "0401","0000","0000","0002",
                                               "0501","0000","0000","0002",
                                               "0601","0000","0000","0002",
                                               "0701","0000","0000","0002",
                                               "0801","0000","0000","0002",
                                               "0901","0000","0000","0002",
                                               "0A01","0000","0000","0002",
                                               "0B01","0000","0000","0002",
                                               "0C01","0000","0000","0002",
                                               "0D01","0000","0000","0002",
                                               "0E01","0000","0000","0002",
                                               "0F01","0000","0000","0002",
                                               "1001","0000","0000","0002",
                                               "1101","0000","0000","0002",
                                               "1201","0000","0000","0002"};



                                foreach (DataRow dr in P_dt_data.Rows)
                                {
                                    int P_int_cup = Convert.ToInt16(dr["CupNum"]);
                                    int P_int_bottle = Convert.ToInt16(dr["BottleNum"]);

                                    int P_int_ObjectWeight = Convert.ToInt32(Convert.ToDouble(dr["ObjectDropWeight"]) * 100.00);
                                    string P_str_ObjectWeight_1 = P_int_ObjectWeight.ToString("X").PadLeft(8, '0').Substring(4, 4);
                                    string P_str_ObjectWeight_2 = P_int_ObjectWeight.ToString("X").PadLeft(8, '0').Substring(0, 4);
                                    if (P_int_bottle == 200)
                                    {
                                        //A粉数据
                                        P_str_data[P_int_cup % 18 == 0 ? 69 : (P_int_cup % 18 - 1) * 4 + 1] = P_str_ObjectWeight_1;
                                        P_str_data[P_int_cup % 18 == 0 ? 70 : (P_int_cup % 18 - 1) * 4 + 2] = P_str_ObjectWeight_2;

                                        Data[P_int_cup % 18 == 0 ? 69 : (P_int_cup % 18 - 1) * 4 + 1] = P_int_ObjectWeight % (256 * 256);
                                        Data[P_int_cup % 18 == 0 ? 70 : (P_int_cup % 18 - 1) * 4 + 2] = P_int_ObjectWeight / (256 * 256);
                                    }
                                    

                                }

                                string P_str_data_1 = null;
                                int[] Data1 = new int[72];
                                for (int i = 0; i < P_str_data.Length; i++)
                                {
                                    P_str_data_1 += ((P_str_data[i].PadLeft(4, '0')).Insert(2, " ") + " ");
                                    //if (i < P_str_data.Length / 2)
                                    //{
                                    //    P_str_data_1 += ((P_str_data[i].PadLeft(4, '0')).Insert(2, " ") + " ");

                                    //}
                                    //else
                                    //{
                                    //    P_str_data_2 += ((P_str_data[i].PadLeft(4, '0')).Insert(2, " ") + " ");
                                    //}
                                }

                                for (int i = 0; i < Data.Length; i++)
                                {
                                    Data1[i] = Data[i];

                                    //if (i < Data.Length / 2)
                                    //{
                                    //    Data1[i] = Data[i];

                                    //}
                                    //else
                                    //{
                                    //    Data2[i - 72] = Data[i];
                                    //}
                                }


                                //P_str_message = "01 10 " + (20.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 48 90 " + P_str_data_1;
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(20, Data1);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                //P_str_message = "01 10 " + (92.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 48 90 " + P_str_data_2;
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                //while (true)
                                //{
                                //    nRet = FADM_Object.Communal.Powder.Write(92, Data2);
                                //    if (nRet == 0)
                                //    {
                                //        break;
                                //    }
                                //    Thread.Sleep(1);
                                //}

                                //更新当前批次号
                                int P_int_batchnum_1 = Convert.ToInt16(batchnum.Substring(0, 4));
                                int P_int_batchnum_2 = Convert.ToInt16(batchnum.Substring(4, 4));
                                int P_int_batchnum_3 = Convert.ToInt16(batchnum.Substring(8, 4));

                                int[] Data3 = new int[4];
                                Data3[0] = Convert.ToInt32(P_int_batchnum_1.ToString());
                                Data3[1] = Convert.ToInt32(P_int_batchnum_2.ToString());
                                Data3[2] = Convert.ToInt32(P_int_batchnum_3.ToString());
                                Data3[3] = Convert.ToInt32(P_int_Plate.ToString());


                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(5000, Data3);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }


                                //更改称粉机接收完成状态
                                v[0] = 2;
                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(10, v);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                Thread.Sleep(1000);

                                goto again;
                            }
                        case 2:
                            {
                                //读取当前批次号
                                //P_str_message = "01 03 " + (5000.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 04";
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                int[] v1 = new int[4];
                                int nRet1 = FADM_Object.Communal.Powder.Read(5000, 4, ref v1);
                                if (nRet1 == -1)
                                {
                                    goto again;
                                }



                                //if (Class_Module.MyModule.Module_Com1_Error)
                                //{
                                //    return;
                                //}

                                //if (Class_Object.MyObject.Module_Powder.P_str_rec == null || Class_Object.MyObject.Module_Powder.P_str_rec.Length < 26)
                                //{
                                //    goto again;
                                //}

                                string P_str_batchnum_1 = v1[0].ToString().PadLeft(4, '0');
                                string P_str_batchnum_2 = v1[1].ToString().PadLeft(4, '0');
                                string P_str_batchnum_3 = v1[2].ToString().PadLeft(4, '0');

                                string P_str_batchnum = P_str_batchnum_1 + P_str_batchnum_2 + P_str_batchnum_3;

                                if (P_str_batchnum != batchnum)
                                {
                                    //更改称粉机数据停止接受状态
                                    v = new int[1];
                                    v[0] = 7;
                                    while (true)
                                    {
                                        nRet = FADM_Object.Communal.Powder.Write(10, v);
                                        if (nRet == 0)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(1);
                                    }
                                }

                                Thread.Sleep(1000);
                                goto again;


                            }
                        case 4:
                            {
                                //滴料完成
                                Thread.Sleep(1000);

                                //读取当前批次号
                                v = new int[1];
                                v[0] = 4;
                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(10, v);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }

                                //if (Class_Module.MyModule.Module_Com1_Error)
                                //{
                                //    return;
                                //}

                                //if (Class_Object.MyObject.Module_Powder.P_str_rec == null || Class_Object.MyObject.Module_Powder.P_str_rec.Length < 26)
                                //{
                                //    goto again;
                                //}

                                int[] v1 = new int[4];
                                int nRet1 = FADM_Object.Communal.Powder.Read(5000, 4, ref v1);
                                if (nRet1 == -1)
                                {
                                    goto again;
                                }

                                string P_str_batchnum_1 = v1[0].ToString().PadLeft(4, '0');
                                string P_str_batchnum_2 = v1[1].ToString().PadLeft(4, '0');
                                string P_str_batchnum_3 = v1[2].ToString().PadLeft(4, '0');
                                int P_int_PlateRec = Convert.ToInt32(v1[3].ToString());

                                string P_str_batchnum = P_str_batchnum_1 + P_str_batchnum_2 + P_str_batchnum_3;

                                if (P_str_batchnum != batchnum)
                                {
                                    //当前称粉机批次和滴液机批次不是同一批次
                                    new FADM_Object.MyAlarm("称粉机请切换为等待数据状态！", 1);
                                    goto again;

                                }

                                int[] Data4 = new int[72];
                                int[] Data5 = new int[72];

                                //P_str_message = "01 03 " + (20.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 48";
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                //string P_str_data_1 = Class_Object.MyObject.Module_Powder.P_str_rec;

                                int nRet4 = FADM_Object.Communal.Powder.Read(20, 72, ref Data4);
                                if (nRet4 == -1)
                                {
                                    goto again;
                                }


                                //P_str_message = "01 03 " + (92.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 48";
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                //string P_str_data_2 = Class_Object.MyObject.Module_Powder.P_str_rec;

                                //int nRet5 = FADM_Object.Communal.Powder.Read(92, 72, ref Data5);
                                //if (nRet5 == -1)
                                //{
                                //    goto again;
                                //}


                                //P_str_message = "01 06 " + (10.ToString("X").PadLeft(4, '0')).Insert(2, " ") + " 00 05 ";
                                //Class_Object.MyObject.Module_Powder.Send(P_str_message, "COM1");

                                v = new int[1];
                                v[0] = 5;
                                while (true)
                                {
                                    nRet = FADM_Object.Communal.Powder.Write(10, v);
                                    if (nRet == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }


                                int[] DataTotal = new int[72];
                                for (int i = 0; i < DataTotal.Length; i++)
                                {
                                    DataTotal[i] = Data4[i];
                                }




                                P_str_sql = "SELECT * FROM drop_details " +
                                            " WHERE BatchName = '" + batchnum + "' AND" +
                                            " Finish = 0 AND CupNum >'" + 18 * (P_int_PlateRec - 1) + "' AND" +
                                            " CupNum <= '" + 18 * P_int_PlateRec + "' AND" +
                                            " BottleNum = 200 ORDER BY CupNum;";
                                P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);


                                foreach (DataRow dr in P_dt_data.Rows)
                                {
                                    int P_int_cup = Convert.ToInt16(dr["CupNum"]);
                                    int P_int_bottle = Convert.ToInt16(dr["BottleNum"]);

                                    if (P_int_bottle == 200)
                                    {
                                        //A粉数据
                                        int P_str_weight_real_1 = DataTotal[P_int_cup % 18 == 0 ? 69 : (P_int_cup % 18 - 1) * 4 + 1];
                                        int P_str_weight_real_2 = DataTotal[P_int_cup % 18 == 0 ? 70 : (P_int_cup % 18 - 1) * 4 + 2];

                                        string P_str_weight_real = string.Format("{0:F2}", (Convert.ToDouble(P_str_weight_real_2 * 256 * 256 + P_str_weight_real_1) / 100.00));

                                        P_str_sql = "UPDATE drop_details SET RealDropWeight = '" + P_str_weight_real + "'," +
                                                    " Finish = 1 WHERE BatchName = '" + batchnum + "' AND CupNum = '" + P_int_cup + "' AND BottleNum = 200;";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                                        //P_str_sql = "UPDATE history_details SET RealDropWeight = '" + P_str_weight_real + "'" +
                                        //            " WHERE BatchName = '" + batchnum + "' AND CupNum = '" + P_int_cup + "' AND BottleNum = 200;";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);


                                    }


                                }
                                
                                //重新判断是否完成滴液



                                goto again;
                            }
                        default:
                            goto again;

                    }
                }
            }
            catch (Exception ex)
            {

                //new Class_Alarm.MyAlarm(ex.Message, "称粉机通讯", false);
            }

        }
    }
}
