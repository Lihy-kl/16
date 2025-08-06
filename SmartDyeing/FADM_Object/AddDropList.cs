using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace SmartDyeing.FADM_Object
{
    internal class AddDropList
    {
        public AddDropList(string s_formulaCode, string s_versionNum, string s_cupNum, int i_type)
        {
            try
            {
                string s_maxCupNum = s_cupNum;
                //查找formula_head
                string s_sql = "SELECT *  FROM formula_head WHERE" +
                                                      " FormulaCode = '" + s_formulaCode + "' and VersionNum ='" + s_versionNum + "' ; ";

                DataTable dt_formula_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                if (dt_formula_head.Rows.Count == 0)
                {
                    throw new Exception();
                }

                s_sql = "SELECT *  FROM formula_details WHERE" +
                                                      " FormulaCode = '" + s_formulaCode + "' and VersionNum ='" + s_versionNum + "' order by IndexNum; ";

                DataTable dt_formula_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //if (dt_formula_details.Rows.Count == 0)
                //{
                //    throw new Exception();
                //}

                //染色后处理浴比值
                string[] sa_hBRList;

                List<string> lis_hBRList = new List<string>();

                //加入表头
                List<string> lis_head = new List<string>();
                lis_head.Add(s_maxCupNum);
                lis_head.Add(dt_formula_head.Rows[0]["FormulaCode"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["VersionNum"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["State"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["FormulaName"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["ClothType"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["Customer"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["AddWaterChoose"].ToString() == "False" || dt_formula_head.Rows[0]["AddWaterChoose"].ToString() == "0" ? "0" : "1");
                lis_head.Add(dt_formula_head.Rows[0]["CompoundBoardChoose"].ToString() == "False" || dt_formula_head.Rows[0]["CompoundBoardChoose"].ToString() == "0" ? "0" : "1");
                lis_head.Add(dt_formula_head.Rows[0]["ClothWeight"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["BathRatio"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["TotalWeight"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["Operator"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["CupCode"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["CreateTime"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["ObjectAddWaterWeight"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["TestTubeObjectAddWaterWeight"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["DyeingCode"].ToString());

                lis_head.Add(dt_formula_head.Rows[0]["Non_AnhydrationWR"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["AnhydrationWR"].ToString());


                lis_head.Add(dt_formula_head.Rows[0]["HandleBathRatio"].ToString()==""?"0": dt_formula_head.Rows[0]["HandleBathRatio"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["Handle_Rev1"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["Handle_Rev2"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["Handle_Rev3"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["Handle_Rev4"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["Handle_Rev5"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["Stage"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["HandleBRList"].ToString());
                //if (Communal._b_isUseCloth)
                if (dt_formula_head.Rows[0]["ClothNum"] is DBNull)
                    lis_head.Add("0");
                else
                    lis_head.Add(dt_formula_head.Rows[0]["ClothNum"].ToString());
                lis_head.Add(dt_formula_head.Rows[0]["IsAutoIn"].ToString());
                string s_li = dt_formula_head.Rows[0]["HandleBRList"] is DBNull ? "" : dt_formula_head.Rows[0]["HandleBRList"].ToString();
                if (s_li != "")
                {
                    sa_hBRList = s_li.Split('|');
                    lis_hBRList = sa_hBRList.ToList();

                }
                else
                { sa_hBRList = new string[1]; }


                string s_cup = lis_head[0];

                s_sql = "DELETE FROM drop_head WHERE CupNum = " + s_maxCupNum + ";";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                s_sql = "DELETE FROM drop_details WHERE CupNum = " + s_maxCupNum + ";";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                s_sql = "DELETE FROM dye_details WHERE CupNum = " + s_maxCupNum + ";";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                s_sql = "DELETE FROM dyeing_details WHERE CupNum = " + s_maxCupNum + ";";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                {
                    //if (Communal._b_isUseCloth)
                        // 添加进批次表头
                        s_sql = "INSERT INTO drop_head (" +
                                " CupNum, FormulaCode, VersionNum, State ,FormulaName, ClothType," +
                                " Customer, AddWaterChoose, CompoundBoardChoose, ClothWeight, BathRatio, TotalWeight," +
                                " Operator, CupCode, CreateTime, ObjectAddWaterWeight, TestTubeObjectAddWaterWeight,DyeingCode,Non_AnhydrationWR,AnhydrationWR,HandleBathRatio,Handle_Rev1,Handle_Rev2,Handle_Rev3,Handle_Rev4,Handle_Rev5,Stage,HandleBRList,ClothNum,IsAutoIn) VALUES(" +
                                " '" + lis_head[0] + "', '" + lis_head[1] + "', '" + lis_head[2] + "'," +
                                " '" + lis_head[3] + "', '" + lis_head[4] + "', '" + lis_head[5] + "'," +
                                " '" + lis_head[6] + "', '" + lis_head[7] + "', '" + lis_head[8] + "'," +
                                " '" + lis_head[9] + "', '" + lis_head[10] + "', '" + lis_head[11] + "'," +
                               " '" + lis_head[12] + "', '" + lis_head[13] + "', '" + lis_head[14] + "'," +
                                " '" + lis_head[15] + "','" + lis_head[16] + "','" + lis_head[17] + "', '" + lis_head[18] + "', '" + lis_head[19]
                                         + "', '" + lis_head[20] + "', '" + lis_head[21] + "', '" + lis_head[22] + "', '" + lis_head[23] + "', '" + lis_head[24] + "', '" + lis_head[25] + "', '" + lis_head[26] + "', '" + lis_head[27] + "', '" + lis_head[28] + "', '" + lis_head[29] + "');";
                    //else
                    //    // 添加进批次表头
                    //    s_sql = "INSERT INTO drop_head (" +
                    //            " CupNum, FormulaCode, VersionNum, State ,FormulaName, ClothType," +
                    //            " Customer, AddWaterChoose, CompoundBoardChoose, ClothWeight, BathRatio, TotalWeight," +
                    //            " Operator, CupCode, CreateTime, ObjectAddWaterWeight, TestTubeObjectAddWaterWeight,DyeingCode,Non_AnhydrationWR,AnhydrationWR,HandleBathRatio,Handle_Rev1,Handle_Rev2,Handle_Rev3,Handle_Rev4,Handle_Rev5,Stage,HandleBRList) VALUES(" +
                    //            " '" + lis_head[0] + "', '" + lis_head[1] + "', '" + lis_head[2] + "'," +
                    //            " '" + lis_head[3] + "', '" + lis_head[4] + "', '" + lis_head[5] + "'," +
                    //            " '" + lis_head[6] + "', '" + lis_head[7] + "', '" + lis_head[8] + "'," +
                    //            " '" + lis_head[9] + "', '" + lis_head[10] + "', '" + lis_head[11] + "'," +
                    //           " '" + lis_head[12] + "', '" + lis_head[13] + "', '" + lis_head[14] + "'," +
                    //            " '" + lis_head[15] + "','" + lis_head[16] + "','" + lis_head[17] + "', '" + lis_head[18] + "', '" + lis_head[19]
                    //                     + "', '" + lis_head[20] + "', '" + lis_head[21] + "', '" + lis_head[22] + "', '" + lis_head[23] + "', '" + lis_head[24] + "', '" + lis_head[25] + "', '" + lis_head[26] + "', '" + lis_head[27]  + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                }


                //添加进批次详细表
                foreach (DataRow dr in dt_formula_details.Rows)
                {
                    List<string> lis_detail = new List<string>();
                    lis_detail.Add(s_maxCupNum);
                    foreach (DataColumn dc in dt_formula_details.Columns)
                    {
                        if (dc.ColumnName == "BottleSelection")
                        {
                            lis_detail.Add((dr[dc]).ToString() == "False" || (dr[dc]).ToString() == "0" ? "0" : "1");
                            continue;
                        }
                        lis_detail.Add((dr[dc]).ToString());
                    }
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
                if (i_type == 3)
                {
                    string s_select_sql = "SELECT * FROM dyeing_details where FormulaCode = '" + s_formulaCode + "' and VersionNum = '" + s_versionNum + "' order by StepNum asc ;";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_select_sql);
                    int pcc = 0;
                    int i_nHeight = 80;
                    SortedDictionary<int, List<List<string>>> map = new SortedDictionary<int, List<List<string>>>();
                    Dictionary<string, int> ccList = new Dictionary<string, int>();
                    foreach (DataRow dr in dt_data.Rows)
                    {
                        List<string> strList = new List<string>();

                        for (int i = 0; i < 37; i++)
                        { //这个为一行
                            if (!ccList.ContainsKey(dr["Code"].ToString() + "-" + dr["No"].ToString()))
                            { //不包含工艺名字
                                ccList.Add(dr["Code"].ToString() + "-" + dr["No"].ToString(), pcc);//Code
                                pcc++;
                            }
                            object unknownTypeValue = dr[i];
                            string valueAsString = Convert.ChangeType(unknownTypeValue, typeof(string)) as string;
                            strList.Add(valueAsString);
                        }
                        int v = ccList[strList[25] + "-" + strList[36]];
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
                    int i_nNum = 0;
                    int SuperStepNum = 1;
                    foreach (KeyValuePair<int, List<List<string>>> kvp in map)
                    {

                        List<List<string>> chilList = kvp.Value;
                        string DyeType = chilList[0][32];
                        if (DyeType.Equals("1"))//把每一步复制到他的dye_details表里
                        { //染色 
                            foreach (List<string> dr in chilList)
                            { //一行就是一个子步骤 
                                List<string> lis_Dye_Detail = new List<string>();

                                lis_Dye_Detail.Add("0");
                                lis_Dye_Detail.Add(s_maxCupNum);
                                lis_Dye_Detail.Add(dt_formula_head.Rows[0]["FormulaCode"].ToString());//FormulaCode
                                lis_Dye_Detail.Add(dt_formula_head.Rows[0]["VersionNum"].ToString());//VersionNum
                                lis_Dye_Detail.Add(dr[25].ToString());//Code
                                //lis_Dye_Detail.Add(dr[16].ToString());//StepNum
                                lis_Dye_Detail.Add(SuperStepNum.ToString());//2024-11-19改下
                                SuperStepNum++;
                                lis_Dye_Detail.Add(dr[17].ToString());//TechnologyName
                                lis_Dye_Detail.Add("0");//Finish
                                lis_Dye_Detail.Add(dr[22].ToString()); //RotorSpeed

                                if (dr[17].ToString() == "温控")
                                {
                                    lis_Dye_Detail.Add(dr[18].ToString());//Temp
                                    lis_Dye_Detail.Add(dr[19].ToString());//TempSpeed
                                    lis_Dye_Detail.Add(dr[20].ToString());//Time
                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum,FormulaCode,VersionNum, Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Temp, TempSpeed, Time,DyeType) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "', '" + lis_Dye_Detail[6] + "', '" + lis_Dye_Detail[7] + "'," +
                                    " '" + lis_Dye_Detail[8] + "'," +
                                    " '" + lis_Dye_Detail[9] + "'," +
                                    " '" + lis_Dye_Detail[10] + "'," +
                                    " '" + lis_Dye_Detail[11] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr[17].ToString() == "冷行" || dr[17].ToString() == "洗杯" || dr[17].ToString() == "排液" || dr[17].ToString() == "搅拌")
                                {
                                    lis_Dye_Detail.Add(dr[20].ToString());//Time
                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Time,DyeType) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "', '" + lis_Dye_Detail[6] + "', '" + lis_Dye_Detail[7] + "', '" + lis_Dye_Detail[8] + "'," +
                                    " '" + lis_Dye_Detail[9] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr[17].ToString().Substring(0, 1) == "加" && dr[17].ToString() != "加水" && dr[17].ToString() != "加药")
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[25].ToString() + "' and  FormulaCode = '" + dt_formula_head.Rows[0]["FormulaCode"].ToString() + "' and VersionNum = '" + dt_formula_head.Rows[0]["VersionNum"].ToString() + "' and TechnologyName = '" + dr[17].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    //lis_Dye_Detail.Add(dt_data2.Rows[0]["FormulaCode"].ToString());
                                    //lis_Dye_Detail.Add(dt_data2.Rows[0]["VersionNum"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["AssistantCode"].ToString());
                                    lis_Dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr[20].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr[20].ToString()) / 100));
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["UnitOfAccount"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["BottleNum"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["SettingConcentration"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["RealConcentration"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["AssistantName"].ToString());
                                    lis_Dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr[20].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr[20].ToString()) / 100));
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["RealDropWeight"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["BottleSelection"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["MinWeight"].ToString());
                                    lis_Dye_Detail.Add("0.0");
                                    lis_Dye_Detail.Add(dr[20].ToString());//Time
                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,Time,RotorSpeed,AssistantCode," +
                                    " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                    " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                    " BottleSelection,MinWeight,ObjectWaterWeight,DyeType) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "', '" + lis_Dye_Detail[6] + "', '" + lis_Dye_Detail[7] + "','" + lis_Dye_Detail[21] + "' ," +
                                    " '" + lis_Dye_Detail[8] + "', '" + lis_Dye_Detail[9] + "', '" + lis_Dye_Detail[10] + "'," +
                                    " '" + lis_Dye_Detail[11] + "', '" + lis_Dye_Detail[12] + "', '" + lis_Dye_Detail[13] + "', '" + lis_Dye_Detail[14] + "', '"
                                    + lis_Dye_Detail[15] + "', '" + lis_Dye_Detail[16] + "', '" + lis_Dye_Detail[17] + "', '" + lis_Dye_Detail[18] + "', '" + lis_Dye_Detail[19] + "'," +
                                    " '" + lis_Dye_Detail[20] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr[17].ToString() == "加水")
                                {
                                    lis_Dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", 0.0) : string.Format("{0:F3}", 0.0));

                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,ObjectWaterWeight,DyeType" +
                                    " ) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "'," +
                                    " '" + lis_Dye_Detail[6] + "'," +
                                    " '" + lis_Dye_Detail[7] + "'," +
                                    " '" + lis_Dye_Detail[8] + "'," +
                                    " '" + lis_Dye_Detail[9] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else
                                {

                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,DyeType" +
                                    " ) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "'," +
                                    " '" + lis_Dye_Detail[6] + "'," +
                                    " '" + lis_Dye_Detail[7] + "'," +
                                    " '" + lis_Dye_Detail[8] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                            }

                        }
                        else
                        { //后处理 
                            //要在循环一遍
                            List<List<string>> bb = map[kvp.Key];
                            //先把加水量计算出来
                            double d_dropWeight = 0.0;
                            double d_dropWater = 0.0;
                            bool b_insert = false;
                            //判断现在第几次排液
                            int i_count = 0;
                            List<double> lis_dropWeight = new List<double>();
                            foreach (List<string> dr1 in bb)
                            {
                                if (dr1[17].ToString().Substring(0, 1) == "加" && dr1[17].ToString() != "加水" && dr1[17].ToString() != "加药")
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr1[25].ToString() + "' and  FormulaCode = '" + dt_formula_head.Rows[0]["FormulaCode"].ToString() + "' and VersionNum = '" + dt_formula_head.Rows[0]["VersionNum"].ToString() + "' and TechnologyName = '" + dr1[17].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    d_dropWeight += (Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[20].ToString()) / 100.0);
                                }
                                else if (dr1[17].ToString() == "排液")
                                {
                                    lis_dropWeight.Add(d_dropWeight);

                                    d_dropWeight = 0.0;
                                }
                                if (bb.Last() == dr1 && !dr1[17].ToString().Equals("排液"))
                                {
                                    lis_dropWeight.Add(0.0);
                                }
                            }

                            foreach (List<string> dr in chilList)
                            {
                                List<string> lis_dye_Detail = new List<string>();
                                lis_dye_Detail.Add("0");
                                lis_dye_Detail.Add(s_maxCupNum);
                                lis_dye_Detail.Add(dt_formula_head.Rows[0]["FormulaCode"].ToString());//FormulaCode
                                lis_dye_Detail.Add(dt_formula_head.Rows[0]["VersionNum"].ToString());//VersionNum
                                lis_dye_Detail.Add(dr[25].ToString());//Code
                                //lis_dye_Detail.Add(dr[16].ToString());//StepNum
                                lis_dye_Detail.Add(SuperStepNum.ToString());//2024-11-19改下
                                SuperStepNum++;
                                lis_dye_Detail.Add(dr[17].ToString());//TechnologyName
                                lis_dye_Detail.Add("0");//Finish
                                lis_dye_Detail.Add(dr[22].ToString());//RotorSpeed

                                if (dr[17].ToString() == "温控")
                                {
                                    lis_dye_Detail.Add(dr[18].ToString());//Temp
                                    lis_dye_Detail.Add(dr[19].ToString());//TempSpeed
                                    lis_dye_Detail.Add(dr[20].ToString());//Time
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
                                else if (dr[17].ToString() == "冷行" || dr[17].ToString() == "洗杯" || dr[17].ToString() == "排液" || dr[17].ToString() == "搅拌")
                                {
                                    if (dr[17].ToString() == "排液")
                                    {
                                        b_insert = false;
                                        i_count++;
                                    }
                                    lis_dye_Detail.Add(dr[20].ToString());//Time
                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "', '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "',2);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr[17].ToString().Substring(0, 1) == "加" && dr[17].ToString() != "加水" && dr[17].ToString() != "加药")
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[25].ToString() + "' and  FormulaCode = '" + dt_formula_head.Rows[0]["FormulaCode"].ToString() + "' and VersionNum = '" + dt_formula_head.Rows[0]["VersionNum"].ToString() + "' and AssistantCode= '" + dr[4].ToString() + "' and TechnologyName = '" + dr[17].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    //lis_Dye_Detail.Add(dt_data2.Rows[0]["FormulaCode"].ToString());
                                    //lis_Dye_Detail.Add(dt_data2.Rows[0]["VersionNum"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantCode"].ToString());
                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr[20].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr[20].ToString()) / 100));
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["UnitOfAccount"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["BottleNum"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["SettingConcentration"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["RealConcentration"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantName"].ToString());
                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr[20].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr[20].ToString()) / 100));
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["RealDropWeight"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["BottleSelection"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["MinWeight"].ToString());
                                    d_dropWater = Convert.ToDouble(dt_formula_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(lis_hBRList[i_nNum]) - Convert.ToDouble(dt_formula_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(dt_formula_head.Rows[0]["Non_AnhydrationWR"].ToString()) - (lis_dropWeight.Count == 0 ? 0 : lis_dropWeight[i_count]);
                                    lis_dye_Detail.Add(!b_insert ? (Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_dropWater) : string.Format("{0:F3}", d_dropWater)) : "0.0");
                                    b_insert = true;
                                    lis_dye_Detail.Add(dr[20].ToString());//Time
                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,Time,RotorSpeed,AssistantCode," +
                                    " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                    " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                    " BottleSelection,MinWeight,ObjectWaterWeight,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "','" + lis_dye_Detail[21] + "' ," +
                                    " '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "', '" + lis_dye_Detail[10] + "'," +
                                    " '" + lis_dye_Detail[11] + "', '" + lis_dye_Detail[12] + "', '" + lis_dye_Detail[13] + "', '" + lis_dye_Detail[14] + "', '"
                                    + lis_dye_Detail[15] + "', '" + lis_dye_Detail[16] + "', '" + lis_dye_Detail[17] + "', '" + lis_dye_Detail[18] + "', '" + lis_dye_Detail[19] + "', '" + lis_dye_Detail[20] + "',2);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr[17].ToString() == "加水")
                                {
                                    double d_dropWater1 = Convert.ToDouble(dt_formula_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(lis_hBRList[i_nNum]) * Convert.ToDouble(dr[20].ToString()) / 100 - Convert.ToDouble(dt_formula_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(dt_formula_head.Rows[0]["Non_AnhydrationWR"].ToString());

                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", (d_dropWater1 <= 0 ? 1 : d_dropWater1)) : string.Format("{0:F3}", (d_dropWater1 <= 0 ? 1 : d_dropWater1)));

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
                    //把每一步复制到他的dye_details表里
                    //2024-11-07 只设置一下杯号就行了和加药的量
                    /* string updateSql = "UPDATE dyeing_details set CupNum =  '" + s_maxCupNum + "' where FormulaCode = '" + s_formulaCode + "' and VersionNum ='" + s_versionNum + "';";
                     FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);*/


                    /*//查找染色后处理工艺步骤
                    s_sql = "select * from dyeing_code where DyeingCode ='" + dt_formula_head.Rows[0]["DyeingCode"].ToString() + "' order by IndexNum;";

                    DataTable dt_dyeing_code = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    int i_num = 0;
                    int i_nNum = 0;
                    foreach (DataRow dr in dt_dyeing_code.Rows)
                    {
                        string s_sql1 = "SELECT * FROM dyeing_process where Code = '" + dr[3].ToString() + "' order by StepNum;";
                        DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);

                        if (dr[1].ToString() == "1"&& i_nNum==0)
                        {
                            foreach (DataRow dr1 in dt_data1.Rows)
                            {
                                i_num++;
                                List<string> lis_Dye_Detail = new List<string>();
                                lis_Dye_Detail.Add("0");
                                lis_Dye_Detail.Add(s_maxCupNum);
                                lis_Dye_Detail.Add(dt_formula_head.Rows[0]["FormulaCode"].ToString());//FormulaCode
                                lis_Dye_Detail.Add(dt_formula_head.Rows[0]["VersionNum"].ToString());//VersionNum
                                lis_Dye_Detail.Add(dr[3].ToString());//Code
                                lis_Dye_Detail.Add(i_num.ToString());//StepNum
                                lis_Dye_Detail.Add(dr1[1].ToString());//TechnologyName
                                lis_Dye_Detail.Add("0");//Finish
                                //RotorSpeed
                                if (dr1[7] is DBNull)
                                {
                                    lis_Dye_Detail.Add("0");
                                }
                                else
                                {
                                    lis_Dye_Detail.Add(dr1[7].ToString());
                                }
                                if (dr1[1].ToString() == "温控")
                                {
                                    lis_Dye_Detail.Add(dr1[4].ToString());//Temp
                                    lis_Dye_Detail.Add(dr1[5].ToString());//TempSpeed
                                    lis_Dye_Detail.Add(dr1[2].ToString());//Time

                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum,FormulaCode,VersionNum, Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Temp, TempSpeed, Time,DyeType) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "', '" + lis_Dye_Detail[6] + "', '" + lis_Dye_Detail[7] + "'," +
                                    " '" + lis_Dye_Detail[8] + "'," +
                                    " '" + lis_Dye_Detail[9] + "'," +
                                    " '" + lis_Dye_Detail[10] + "'," +
                                    " '" + lis_Dye_Detail[11] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr1[1].ToString() == "冷行" || dr1[1].ToString() == "洗杯" || dr1[1].ToString() == "排液" || dr1[1].ToString() == "搅拌")
                                {
                                    lis_Dye_Detail.Add(dr1[2].ToString());//Time

                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Time,DyeType) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "', '" + lis_Dye_Detail[6] + "', '" + lis_Dye_Detail[7] + "', '" + lis_Dye_Detail[8] + "'," +
                                    " '" + lis_Dye_Detail[9] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + dt_formula_head.Rows[0]["FormulaCode"].ToString() + "' and VersionNum = '" + dt_formula_head.Rows[0]["VersionNum"].ToString() + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    //lis_Dye_Detail.Add(dt_data2.Rows[0]["FormulaCode"].ToString());
                                    //lis_Dye_Detail.Add(dt_data2.Rows[0]["VersionNum"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["AssistantCode"].ToString());
                                    lis_Dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100): string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["FormulaDosage"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["UnitOfAccount"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["BottleNum"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["SettingConcentration"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["RealConcentration"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["AssistantName"].ToString());
                                    lis_Dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100) : string.Format("{0:F3}", Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1[2].ToString()) / 100));
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["RealDropWeight"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["BottleSelection"].ToString());
                                    lis_Dye_Detail.Add(dt_data2.Rows[0]["MinWeight"].ToString());

                                    lis_Dye_Detail.Add("0.0");

                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,AssistantCode," +
                                    " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                                    " RealConcentration, AssistantName, ObjectDropWeight, RealDropWeight," +
                                    " BottleSelection,MinWeight,ObjectWaterWeight,DyeType) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "', '" + lis_Dye_Detail[6] + "', '" + lis_Dye_Detail[7] + "'," +
                                    " '" + lis_Dye_Detail[8] + "', '" + lis_Dye_Detail[9] + "', '" + lis_Dye_Detail[10] + "'," +
                                    " '" + lis_Dye_Detail[11] + "', '" + lis_Dye_Detail[12] + "', '" + lis_Dye_Detail[13] + "', '" + lis_Dye_Detail[14] + "', '"
                                    + lis_Dye_Detail[15] + "', '" + lis_Dye_Detail[16] + "', '" + lis_Dye_Detail[17] + "', '" + lis_Dye_Detail[18] + "', '" + lis_Dye_Detail[19] + "'," +
                                    " '" + lis_Dye_Detail[20] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr1[1].ToString() == "加水")
                                {
                                    lis_Dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", 0.0): string.Format("{0:F3}", 0.0));

                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,ObjectWaterWeight,DyeType" +
                                    " ) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "'," +
                                    " '" + lis_Dye_Detail[6] + "'," +
                                    " '" + lis_Dye_Detail[7] + "'," +
                                    " '" + lis_Dye_Detail[8] + "'," +
                                    " '" + lis_Dye_Detail[9] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else
                                {

                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed,DyeType" +
                                    " ) VALUES( '" + lis_Dye_Detail[0] + "', '" + lis_Dye_Detail[1] + "'," +
                                    " '" + lis_Dye_Detail[2] + "', '" + lis_Dye_Detail[3] + "', '" + lis_Dye_Detail[4] + "'," +
                                    " '" + lis_Dye_Detail[5] + "'," +
                                    " '" + lis_Dye_Detail[6] + "'," +
                                    " '" + lis_Dye_Detail[7] + "'," +
                                    " '" + lis_Dye_Detail[8] + "',1);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //先把加水量计算出来
                            double d_dropWeight = 0.0;
                            double d_dropWater = 0.0;
                            bool b_insert = false;
                            //判断现在第几次排液
                            int i_count = 0;
                            List<double> lis_dropWeight = new List<double>();
                            foreach (DataRow dr1 in dt_data1.Rows)
                            {
                                if (dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + dt_formula_head.Rows[0]["FormulaCode"].ToString() + "' and VersionNum = '" + dt_formula_head.Rows[0]["VersionNum"].ToString() + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    d_dropWeight += (Convert.ToDouble(dt_data2.Rows[0]["ObjectDropWeight"].ToString()) * Convert.ToDouble(dr1["ProportionOrTime"].ToString()) / 100.0);
                                }
                                else if (dr1[1].ToString() == "排液")
                                {
                                    lis_dropWeight.Add(d_dropWeight);

                                    d_dropWeight = 0.0;
                                }
                            }

                            foreach (DataRow dr1 in dt_data1.Rows)
                            {
                                i_num++;
                                List<string> lis_dye_Detail = new List<string>();
                                lis_dye_Detail.Add("0");                                                          //  1
                                lis_dye_Detail.Add(s_maxCupNum);
                                lis_dye_Detail.Add(dt_formula_head.Rows[0]["FormulaCode"].ToString());//FormulaCode 
                                lis_dye_Detail.Add(dt_formula_head.Rows[0]["VersionNum"].ToString());//VersionNum
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
                                if (dr1[1].ToString() == "温控")
                                {
                                    lis_dye_Detail.Add(dr1[4].ToString());//Temp
                                    lis_dye_Detail.Add(dr1[5].ToString());//TempSpeed
                                    lis_dye_Detail.Add(dr1[2].ToString());//Time

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
                                else if (dr1[1].ToString() == "冷行" || dr1[1].ToString() == "洗杯" || dr1[1].ToString() == "排液" || dr1[1].ToString() == "搅拌")
                                {
                                    if (dr1[1].ToString() == "排液")
                                    {
                                        b_insert = false;
                                        i_count++;
                                    }
                                    lis_dye_Detail.Add(dr1[2].ToString());//Time

                                    {
                                        s_sql = "INSERT INTO dye_details (" +
                                    " BatchName, CupNum, FormulaCode,VersionNum,Code, StepNum, TechnologyName,Finish,RotorSpeed," +
                                    " Time,DyeType) VALUES( '" + lis_dye_Detail[0] + "', '" + lis_dye_Detail[1] + "'," +
                                    " '" + lis_dye_Detail[2] + "', '" + lis_dye_Detail[3] + "', '" + lis_dye_Detail[4] + "'," +
                                    " '" + lis_dye_Detail[5] + "', '" + lis_dye_Detail[6] + "', '" + lis_dye_Detail[7] + "', '" + lis_dye_Detail[8] + "', '" + lis_dye_Detail[9] + "',2);";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    }
                                }
                                else if (dr1[1].ToString().Substring(0, 1) == "加" && dr1[1].ToString() != "加水" && dr1[1].ToString() != "加药")
                                {
                                    string s_sql2 = "SELECT * FROM formula_handle_details where Code = '" + dr[3].ToString() + "' and  FormulaCode = '" + dt_formula_head.Rows[0]["FormulaCode"].ToString() + "' and VersionNum = '" + dt_formula_head.Rows[0]["VersionNum"].ToString() + "' and TechnologyName = '" + dr1[1].ToString() + "';";
                                    DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                    //lis_Dye_Detail.Add(dt_data2.Rows[0]["FormulaCode"].ToString());
                                    //lis_Dye_Detail.Add(dt_data2.Rows[0]["VersionNum"].ToString());
                                    lis_dye_Detail.Add(dt_data2.Rows[0]["AssistantCode"].ToString());    // 从1开始  10
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
                                    d_dropWater = Convert.ToDouble(dt_formula_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(lis_hBRList[i_nNum]) - Convert.ToDouble(dt_formula_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(dt_formula_head.Rows[0]["Non_AnhydrationWR"].ToString()) - lis_dropWeight.Count==0?0:lis_dropWeight[i_count];
                                    lis_dye_Detail.Add(!b_insert ? (Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", d_dropWater): string.Format("{0:F3}", d_dropWater)) : "0.0");
                                    b_insert = true;

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
                                else if (dr1[1].ToString() == "加水")
                                {
                                    double d_dropWater1 = Convert.ToDouble(dt_formula_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(lis_hBRList[i_nNum]) * Convert.ToDouble(dr1[2].ToString()) / 100 - Convert.ToDouble(dt_formula_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(dt_formula_head.Rows[0]["Non_AnhydrationWR"].ToString());

                                    lis_dye_Detail.Add(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", (d_dropWater1<=0?1: d_dropWater1)) : string.Format("{0:F3}", (d_dropWater1 <= 0 ? 1 : d_dropWater1)));

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
                    }*/
                }

                //修改杯号正在使用
                s_sql = "Update cup_details set IsUsing = 1 where CupNum = '" + s_maxCupNum + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            }
            catch
            {
                //删除批次浏览表头资料
                string s_sql_1 = "DELETE FROM drop_head WHERE CupNum = '" + s_cupNum + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                //删除批次浏览表详细资料
                s_sql_1 = "DELETE FROM drop_details WHERE CupNum = '" + s_cupNum + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                //删除批次浏览表详细资料
                s_sql_1 = "DELETE FROM dye_details WHERE CupNum = '" + s_cupNum + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                //把杯号置位null
                s_sql_1 = "UPDATE dyeing_details set CupNum = '' WHERE CupNum = '" + s_cupNum + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);

                //更新杯号使用情况
                s_sql_1 = "Update cup_details set IsUsing = 0 where CupNum = '" + s_cupNum + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);
            }
        }
    }
}
