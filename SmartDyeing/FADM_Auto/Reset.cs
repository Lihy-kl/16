using Lib_File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Auto
{
    internal class Reset
    {

        public static void MoveData(string s_batchName)
        {
            if (Convert.ToString(s_batchName) != "0")
            {
                if (FADM_Object.Communal._b_isDripAll)
                {
                    //添加历史表
                    DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                     "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'drop_head';");
                    string s_columnHead = null;
                    foreach (DataRow row in dt_temp.Rows)
                    {
                        string s_curName = Convert.ToString(row[0]);
                        if ("TestTubeFinish" != s_curName && "TestTubeWaterLower" != s_curName && "AddWaterFinish" != s_curName &&
                            "CupFinish" != s_curName && "TestTubeWaterLower" != s_curName)
                            s_columnHead += s_curName + ", ";
                    }
                    s_columnHead = s_columnHead.Remove(s_columnHead.Length - 2);

                    dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                       "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'drop_details';");
                    string s_columnDetails = null;
                    foreach (DataRow row in dt_temp.Rows)
                    {
                        string s_curName = Convert.ToString(row[0]);
                        if ("MinWeight" != s_curName && "Finish" != s_curName && "IsShow" != s_curName)
                            s_columnDetails += Convert.ToString(row[0]) + ", ";
                    }
                    s_columnDetails = s_columnDetails.Remove(s_columnDetails.Length - 2);

                    dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                       "SELECT * FROM drop_head WHERE BatchName = '" + s_batchName + "'   ORDER BY CupNum;");

                    foreach (DataRow dataRow in dt_temp.Rows)
                    {
                        //先删除已有记录
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                           "DELETE FROM history_head WHERE BatchName = '" + s_batchName + "' AND CupNum = " +dataRow["CupNum"].ToString() + ";");
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "DELETE FROM history_details WHERE BatchName = '" + s_batchName + "' AND CupNum = " + dataRow["CupNum"].ToString() + ";");

                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "INSERT INTO history_head (" + s_columnHead + ") (SELECT " + s_columnHead + " FROM drop_head " +
                            "WHERE BatchName = '" + s_batchName + "' AND CupNum = " + dataRow["CupNum"].ToString() + ";");

                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                           "INSERT INTO history_details (" + s_columnDetails + ") (SELECT " + s_columnDetails + " FROM drop_details " +
                           "WHERE BatchName = '" + s_batchName + "' AND CupNum = " + dataRow["CupNum"].ToString() + ";");

                        if (SmartDyeing.FADM_Object.Communal._lis_dripCupNum.Contains(Convert.ToInt32(dataRow["CupNum"].ToString())))
                        {
                            //滴液记录重新添加到批次
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "Update drop_head set BatchName = '0',Step=0,RealAddWaterWeight=0.0,AddWaterFinish=0,CupFinish=0 WHERE CupNum = " + dataRow["CupNum"].ToString() + "  AND BatchName = '" + s_batchName + "' ;");
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                "Update  drop_details set BatchName = '0',Finish=0,RealDropWeight=0.00,MinWeight=0 WHERE CupNum = " + dataRow["CupNum"].ToString() + "  AND BatchName = '" + s_batchName + "';");

                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                           "UPDATE cup_details SET FormulaCode = null, " +
                           "DyeingCode = null, IsUsing = 1, Statues = '待机', " +
                           "StartTime = null, SetTemp = null, StepNum = null, TotalWeight = null, " +
                           "TotalStep = null, TechnologyName = null, StepStartTime = null, SetTime = null,RecordIndex = 0 WHERE CupNum = " +
                           dataRow["CupNum"].ToString() + "  ;");
                        }
                        else if (SmartDyeing.FADM_Object.Communal._lis_dyeCupNum.Contains(Convert.ToInt32(dataRow["CupNum"].ToString())))
                        {
                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(
                                "SELECT * FROM cup_details WHERE CupNum = " + dataRow["CupNum"].ToString() + ";");
                            string sStatues = dt_cup_details.Rows[0]["Statues"].ToString();
                            if (sStatues != "下线")
                            {
                                if (sStatues != "待机")
                                {
                                    //FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    //    "DELETE FROM drop_head WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "' ;");
                                    //FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    //    "DELETE FROM drop_details WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "';");
                                    //FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    //   "DELETE FROM dye_details WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "';"); ;
                                    //Txt.DeleteTXT(i_cupNum);
                                    //Txt.DeleteMarkTXT(i_cupNum);
                                    FADM_Object.Communal._lis_dripStopCup.Add(Convert.ToInt32(dataRow["CupNum"].ToString()));
                                }
                                else
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "DELETE FROM drop_head WHERE CupNum = " + dataRow["CupNum"].ToString() + "  AND BatchName = '" + s_batchName + "' ;");
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "DELETE FROM drop_details WHERE CupNum = " + dataRow["CupNum"].ToString() + "  AND BatchName = '" + s_batchName + "';");
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "DELETE FROM dye_details WHERE CupNum = " + dataRow["CupNum"].ToString() + "  AND BatchName = '" + s_batchName + "';");
                                    Txt.DeleteTXT(Convert.ToInt32(dataRow["CupNum"].ToString()));
                                    Txt.DeleteMarkTXT(Convert.ToInt32(dataRow["CupNum"].ToString()));
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET FormulaCode = null, " +
                                   "DyeingCode = null, IsUsing = 0, Statues = '待机', " +
                                   "StartTime = null, SetTemp = null, StepNum = null, TotalWeight = null, " +
                                   "TotalStep = null, TechnologyName = null, StepStartTime = null, SetTime = null,RecordIndex = 0 WHERE CupNum = " +
                                   dataRow["CupNum"].ToString() + "  ;");

                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < 7; i++)
                    {
                        int i_cupMin = 0, i_cupMax = 0, i_type = 0;
                        switch (i)
                        {
                            case 1:
                                i_cupMin = Lib_Card.Configure.Parameter.Machine_Area1_CupMin;
                                i_cupMax = Lib_Card.Configure.Parameter.Machine_Area1_CupMax;
                                i_type = Lib_Card.Configure.Parameter.Machine_Area1_Type;
                                break;
                            case 2:
                                i_cupMin = Lib_Card.Configure.Parameter.Machine_Area2_CupMin;
                                i_cupMax = Lib_Card.Configure.Parameter.Machine_Area2_CupMax;
                                i_type = Lib_Card.Configure.Parameter.Machine_Area2_Type;
                                break;
                            case 3:
                                i_cupMin = Lib_Card.Configure.Parameter.Machine_Area3_CupMin;
                                i_cupMax = Lib_Card.Configure.Parameter.Machine_Area3_CupMax;
                                i_type = Lib_Card.Configure.Parameter.Machine_Area3_Type;
                                break;
                            case 4:
                                i_cupMin = Lib_Card.Configure.Parameter.Machine_Area4_CupMin;
                                i_cupMax = Lib_Card.Configure.Parameter.Machine_Area4_CupMax;
                                i_type = Lib_Card.Configure.Parameter.Machine_Area4_Type;
                                break;
                            case 5:
                                i_cupMin = Lib_Card.Configure.Parameter.Machine_Area5_CupMin;
                                i_cupMax = Lib_Card.Configure.Parameter.Machine_Area5_CupMax;
                                i_type = Lib_Card.Configure.Parameter.Machine_Area5_Type;
                                break;
                            case 6:
                                i_cupMin = Lib_Card.Configure.Parameter.Machine_Area6_CupMin;
                                i_cupMax = Lib_Card.Configure.Parameter.Machine_Area6_CupMax;
                                i_type = Lib_Card.Configure.Parameter.Machine_Area6_Type;
                                break;
                            default:
                                break;

                        }



                        //添加历史表
                        DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                         "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'drop_head';");
                        string s_columnHead = null;
                        foreach (DataRow row in dt_temp.Rows)
                        {
                            string s_curName = Convert.ToString(row[0]);
                            if ("TestTubeFinish" != s_curName && "TestTubeWaterLower" != s_curName && "AddWaterFinish" != s_curName &&
                                "CupFinish" != s_curName && "TestTubeWaterLower" != s_curName)
                                s_columnHead += s_curName + ", ";
                        }
                        s_columnHead = s_columnHead.Remove(s_columnHead.Length - 2);

                        dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                           "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'drop_details';");
                        string s_columnDetails = null;
                        foreach (DataRow row in dt_temp.Rows)
                        {
                            string s_curName = Convert.ToString(row[0]);
                            if ("MinWeight" != s_curName && "Finish" != s_curName && "IsShow" != s_curName)
                                s_columnDetails += Convert.ToString(row[0]) + ", ";
                        }
                        s_columnDetails = s_columnDetails.Remove(s_columnDetails.Length - 2);

                        //先删除已有记录
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "DELETE FROM history_head WHERE BatchName = '" + s_batchName + "' AND CupNum >= " + i_cupMin + " AND CupNum <= " + i_cupMax + ";");
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "DELETE FROM history_details WHERE BatchName = '" + s_batchName + "' AND CupNum >= " + i_cupMin + " AND CupNum <= " + i_cupMax + ";");

                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "INSERT INTO history_head (" + s_columnHead + ") (SELECT " + s_columnHead + " FROM drop_head " +
                            "WHERE BatchName = '" + s_batchName + "' AND CupNum >= " + i_cupMin + " AND CupNum <= " + i_cupMax + ");");

                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                           "INSERT INTO history_details (" + s_columnDetails + ") (SELECT " + s_columnDetails + " FROM drop_details " +
                           "WHERE BatchName = '" + s_batchName + "' AND CupNum >= " + i_cupMin + " AND CupNum <= " + i_cupMax + ");");




                        dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                            "SELECT * FROM drop_head WHERE BatchName = '" + s_batchName + "'  AND CupNum >= " + i_cupMin + " AND CupNum <= " + i_cupMax + "   ORDER BY CupNum;");
                        if (dt_temp.Rows.Count == 0)
                            continue;

                        foreach (DataRow dataRow in dt_temp.Rows)
                        {
                            int i_cupNum = Convert.ToInt16(dataRow["CupNum"]);
                            if (2 == i_type)
                            {
                                //原来删除记录
                                // FADM_Object.Communal._fadmSqlserver.ReviseData(
                                //        "DELETE FROM drop_head WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "' ;");
                                // FADM_Object.Communal._fadmSqlserver.ReviseData(
                                //     "DELETE FROM drop_details WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "';");

                                // FADM_Object.Communal._fadmSqlserver.ReviseData(
                                //"UPDATE cup_details SET FormulaCode = null, " +
                                //"DyeingCode = null, IsUsing = 0, Statues = '待机', " +
                                //"StartTime = null, SetTemp = null, StepNum = null, TotalWeight = null, " +
                                //"TotalStep = null, TechnologyName = null, StepStartTime = null, SetTime = null,RecordIndex = 0 WHERE CupNum = " +
                                //i_cupNum + "  ;");

                                //滴液记录重新添加到批次
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "Update drop_head set BatchName = '0',Step=0,RealAddWaterWeight=0.0,AddWaterFinish=0,CupFinish=0 WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "' ;");
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "Update  drop_details set BatchName = '0',Finish=0,RealDropWeight=0.00,MinWeight=0 WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "';");

                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET FormulaCode = null, " +
                               "DyeingCode = null, IsUsing = 1, Statues = '待机', " +
                               "StartTime = null, SetTemp = null, StepNum = null, TotalWeight = null, " +
                               "TotalStep = null, TechnologyName = null, StepStartTime = null, SetTime = null,RecordIndex = 0 WHERE CupNum = " +
                               i_cupNum + "  ;");

                            }
                            else if (3 == i_type)
                            {
                                DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(
                                    "SELECT * FROM cup_details WHERE CupNum = " + i_cupNum + ";");
                                string s_statues = dt_cup_details.Rows[0]["Statues"].ToString();
                                if (s_statues != "下线")
                                {
                                    if (s_statues != "待机")
                                    {
                                        //FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        //    "DELETE FROM drop_head WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "' ;");
                                        //FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        //    "DELETE FROM drop_details WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "';");
                                        //FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        //   "DELETE FROM dye_details WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "';"); ;
                                        //Txt.DeleteTXT(i_cupNum);
                                        //Txt.DeleteMarkTXT(i_cupNum);
                                        FADM_Object.Communal._lis_dripStopCup.Add(i_cupNum);
                                    }
                                    else
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "DELETE FROM drop_head WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "' ;");
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "DELETE FROM drop_details WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "';");
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "DELETE FROM dye_details WHERE CupNum = " + i_cupNum + "  AND BatchName = '" + s_batchName + "';");
                                        Txt.DeleteTXT(i_cupNum);
                                        Txt.DeleteMarkTXT(i_cupNum);
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET FormulaCode = null, " +
                                       "DyeingCode = null, IsUsing = 0, Statues = '待机', " +
                                       "StartTime = null, SetTemp = null, StepNum = null, TotalWeight = null, " +
                                       "TotalStep = null, TechnologyName = null, StepStartTime = null, SetTime = null,RecordIndex = 0 WHERE CupNum = " +
                                       i_cupNum + "  ;");

                                    }
                                }
                            }

                        }

                    }
                }
            }
        }

    }
}
