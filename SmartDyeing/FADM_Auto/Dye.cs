using SmartDyeing.FADM_Control;
using SmartDyeing.FADM_Form;
using Lib_File;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SmartDyeing.FADM_Auto.Dye;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using SmartDyeing.FADM_Object;
using System.Drawing;
using static System.Windows.Forms.AxHost;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.InteropServices;
using static SmartDyeing.FADM_Object.Communal;
using System.Management;

namespace SmartDyeing.FADM_Auto
{
    internal class Dye
    {



        public struct s_Cup
        {
            public bool _b_start;
            public string _s_temp;
            public string _s_statues;
            public string _s_history;
            public bool _b_finish;
            public bool _b_tagging;
            public string _s_technologyName;
            public bool _b_stressRelief;
            //加水/加药信号
            public bool _b_addSignal;
            //杯盖状态1=关盖 2=开盖
            public int _i_cupCover;
            //杯盖请求1=开盖，2=关盖
            public int _i_requesCupCover;
            //锁止上状态0=无信号 1=锁止上信号
            public int _i_lockUp;

            //加水加药状态 0 代表可接收信号写入数据库 1代表写入数据库完成 2 代表执行对应操作完成(接收到2后通过刷新才能变为0)
            public int _i_addStatus;
            //开关盖状态 0 代表可接收信号写入数据库 1代表写入数据库完成 2 代表执行对应操作完成(接收到2后通过刷新才能变为0)
            public int _i_cover;
            //泄压状态 0 代表可接收信号写入数据库 1代表写入数据库完成 2 代表执行对应操作完成(接收到2后通过刷新才能变为0)
            public int _i_stress;

            /// <summary>
            /// 放布时间
            /// </summary>
            public string _s_inTime;

            /// <summary>
            /// 出布时间
            /// </summary>
            public string _s_outTime;

        }



        public static s_Cup[] _cup_Temps = new s_Cup[Lib_Card.Configure.Parameter.Machine_Cup_Total];

        //是否停止
        public static int[] _ia_stop = new int[Lib_Card.Configure.Parameter.Machine_Cup_Total];

        //是否已发送停止
        public static int[] _ia_stopSend = new int[Lib_Card.Configure.Parameter.Machine_Cup_Total];


        private int[] _ai_iArray = { 0, 0, 0, 0, 0, 0 };

        public static Dictionary<int, bool> _dic_keyValue = new Dictionary<int, bool>();

        public int _i_state = 0;

        public bool _b_state = false;

        public void ClothDyeing()
        {
            _i_state = 1;
            while (true)
            {
                if (_i_state == 1)
                {
                    _b_state = true;
                }

                try
                {
                    Thread.Sleep(1);
                    int[] ia_aType = {Lib_Card.Configure.Parameter.Machine_Area1_Type,
                    Lib_Card.Configure.Parameter.Machine_Area2_Type,
                    Lib_Card.Configure.Parameter.Machine_Area3_Type,
                    Lib_Card.Configure.Parameter.Machine_Area4_Type,
                    Lib_Card.Configure.Parameter.Machine_Area5_Type,
                    Lib_Card.Configure.Parameter.Machine_Area6_Type};

                    for (int i = 0; i < ia_aType.Length; i++)
                    {
                        //判断是否是打板机
                        if (ia_aType[i] != 3)
                            continue;

                        //判断当前机是否下线
                        int i_status = FADM_Object.Communal._ia_dyeStatus[i];
                        if (0 == i_status)
                        {
                            //当前机台下限不做通讯
                            continue;
                        }

                        //判断当前打板机类型
                        int i_dyeType = 0;

                        //获取数据
                        List<FADM_Object.Data> lis_datas = new List<FADM_Object.Data>();
                        if (i == 0)
                        {
                            if (!FADM_Object.Communal._tcpDyeHMI1._b_Connect)
                            {
                                FADM_Object.Communal._tcpDyeHMI1.ReConnect();
                            }
                            FADM_Object.Communal._tcpDyeHMI1.DyeRead(ref lis_datas, Lib_Card.Configure.Parameter.Machine_Area1_DyeType);
                            i_dyeType = Lib_Card.Configure.Parameter.Machine_Area1_DyeType;
                        }
                        else if (i == 1)
                        {
                            if (!FADM_Object.Communal._tcpDyeHMI2._b_Connect)
                            {
                                FADM_Object.Communal._tcpDyeHMI2.ReConnect();
                            }
                            FADM_Object.Communal._tcpDyeHMI2.DyeRead(ref lis_datas, Lib_Card.Configure.Parameter.Machine_Area2_DyeType);
                            i_dyeType = Lib_Card.Configure.Parameter.Machine_Area2_DyeType;
                        }
                        else if (i == 2)
                        {
                            if (!FADM_Object.Communal._tcpDyeHMI3._b_Connect)
                            {
                                FADM_Object.Communal._tcpDyeHMI3.ReConnect();
                            }
                            FADM_Object.Communal._tcpDyeHMI3.DyeRead(ref lis_datas, Lib_Card.Configure.Parameter.Machine_Area3_DyeType);
                            i_dyeType = Lib_Card.Configure.Parameter.Machine_Area3_DyeType;
                        }
                        else if (i == 3)
                        {
                            if (!FADM_Object.Communal._tcpDyeHMI4._b_Connect)
                            {
                                FADM_Object.Communal._tcpDyeHMI4.ReConnect();
                            }
                            FADM_Object.Communal._tcpDyeHMI4.DyeRead(ref lis_datas, Lib_Card.Configure.Parameter.Machine_Area4_DyeType);
                            i_dyeType = Lib_Card.Configure.Parameter.Machine_Area4_DyeType;
                        }
                        else if (i == 4)
                        {
                            if (!FADM_Object.Communal._tcpDyeHMI5._b_Connect)
                            {
                                FADM_Object.Communal._tcpDyeHMI5.ReConnect();
                            }
                            FADM_Object.Communal._tcpDyeHMI5.DyeRead(ref lis_datas, Lib_Card.Configure.Parameter.Machine_Area5_DyeType);
                            i_dyeType = Lib_Card.Configure.Parameter.Machine_Area5_DyeType;
                        }
                        else if (i == 5)
                        {
                            if (!FADM_Object.Communal._tcpDyeHMI6._b_Connect)
                            {
                                FADM_Object.Communal._tcpDyeHMI6.ReConnect();
                            }
                            FADM_Object.Communal._tcpDyeHMI6.DyeRead(ref lis_datas, Lib_Card.Configure.Parameter.Machine_Area6_DyeType);
                            i_dyeType = Lib_Card.Configure.Parameter.Machine_Area6_DyeType;
                        }
                        if (i_dyeType == 1)
                        {

                            if (lis_datas.Count != 6)
                            {
                                if (++_ai_iArray[i] > 5)
                                {
                                    //重置杯盖状态
                                    if (i == 0)
                                    {
                                        FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus1 = false;
                                        FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus2 = false;
                                        FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus3 = false;
                                        FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus4 = false;
                                        FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus5 = false;
                                        FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus6 = false;
                                    }
                                    else if (i == 1)
                                    {
                                        FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus1 = false;
                                        FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus2 = false;
                                        FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus3 = false;
                                        FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus4 = false;
                                        FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus5 = false;
                                        FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus6 = false;
                                    }
                                    else if (i == 2)
                                    {
                                        FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus1 = false;
                                        FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus2 = false;
                                        FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus3 = false;
                                        FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus4 = false;
                                        FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus5 = false;
                                        FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus6 = false;
                                    }
                                    else if (i == 3)
                                    {
                                        FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus1 = false;
                                        FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus2 = false;
                                        FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus3 = false;
                                        FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus4 = false;
                                        FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus5 = false;
                                        FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus6 = false;
                                    }
                                    else if (i == 4)
                                    {
                                        FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus1 = false;
                                        FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus2 = false;
                                        FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus3 = false;
                                        FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus4 = false;
                                        FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus5 = false;
                                        FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus6 = false;
                                    }
                                    else if (i == 5)
                                    {
                                        FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus1 = false;
                                        FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus2 = false;
                                        FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus3 = false;
                                        FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus4 = false;
                                        FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus5 = false;
                                        FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus6 = false;
                                    }
                                    // FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo((i_erea + 1) + "号打板机通讯异常");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Object.Communal._sa_dyeConFTime[i] = Lib_Card.CardObject.InsertD((i + 1) + "号打板机通讯异常", "Dye");
                                    else
                                        FADM_Object.Communal._sa_dyeConFTime[i] = Lib_Card.CardObject.InsertD("Communication abnormality of " + (i + 1) + " board making machine", "Dye");
                                }
                                continue;
                            }
                            else
                            {
                                _ai_iArray[i] = 0;
                                Lib_Card.CardObject.DeleteD(FADM_Object.Communal._sa_dyeConFTime[i]);
                            }

                            

                            for (int j = 0; j < lis_datas.Count; j++)
                            {
                                int i_cupmin = 0;
                                if (i == 0)
                                {
                                    i_cupmin = Lib_Card.Configure.Parameter.Machine_Area1_CupMin;
                                }
                                else if (i == 1)
                                {
                                    i_cupmin = Lib_Card.Configure.Parameter.Machine_Area2_CupMin;
                                }
                                else if (i == 2)
                                {
                                    i_cupmin = Lib_Card.Configure.Parameter.Machine_Area3_CupMin;
                                }
                                else if (i == 3)
                                {
                                    i_cupmin = Lib_Card.Configure.Parameter.Machine_Area4_CupMin;
                                }
                                else if (i == 4)
                                {
                                    i_cupmin = Lib_Card.Configure.Parameter.Machine_Area5_CupMin;
                                }
                                else if (i == 5)
                                {
                                    i_cupmin = Lib_Card.Configure.Parameter.Machine_Area6_CupMin;
                                }

                                int i_cupNo = i_cupmin-1 + j + 1;
                                _cup_Temps[i_cupNo - 1]._s_temp = string.Format("{0:F1}", Convert.ToDouble(lis_datas[j]._s_realTem) / 10.0);
                                _cup_Temps[i_cupNo - 1]._s_statues = lis_datas[j]._s_currentState;
                                _cup_Temps[i_cupNo - 1]._s_history = lis_datas[j]._s_history;
                                _cup_Temps[i_cupNo - 1]._i_requesCupCover = Convert.ToInt32(lis_datas[j]._s_openInplace);
                                _cup_Temps[i_cupNo - 1]._i_lockUp = Convert.ToInt32(lis_datas[j]._s_lockUp);

                                if ("0" == lis_datas[j]._s_addWater)
                                {
                                    _cup_Temps[i_cupNo - 1]._i_addStatus = 0;
                                }
                                if ("0" == lis_datas[j]._s_openInplace)
                                {
                                    _cup_Temps[i_cupNo - 1]._i_cover = 0;
                                }
                                if (_cup_Temps[i_cupNo - 1]._i_cover == 0)
                                {
                                    //加入开盖，关盖请求
                                    if (lis_datas[j]._s_openInplace == "1" && lis_datas[j]._s_lockUp == "1")
                                    {
                                        if (lis_datas[j]._s_currentState == "0" || lis_datas[j]._s_currentState == "6")
                                        {
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE cup_details SET Cooperate = 2,DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " and Cooperate=0;");
                                        }
                                        else
                                        {
                                            //查询对应步号，找到对应是后处理还是染色工艺
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData("Select * from dye_details where StepNum = '" + lis_datas[j]._s_currentStepNum + "' and CupNum = " + i_cupNo);
                                            if (dt_dye_details.Rows.Count > 0)
                                            {
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE cup_details SET Cooperate = 2,ReceptionTime='" + DateTime.Now + "',DyeType = '" + dt_dye_details.Rows[0]["DyeType"].ToString() + "' WHERE CupNum = " + i_cupNo + " and Cooperate=0;");
                                            }
                                            else
                                            {
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET Cooperate = 2,DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " and Cooperate=0;");
                                            }
                                        }
                                        _cup_Temps[i_cupNo - 1]._i_cover = 1;
                                    }
                                    else if (lis_datas[j]._s_openInplace == "2" && lis_datas[j]._s_lockUp == "1")
                                    {
                                        if (lis_datas[j]._s_currentState == "0" || lis_datas[j]._s_currentState == "6")
                                        {
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE cup_details SET Cooperate = 5,DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " and Cooperate=0;");
                                        }
                                        else
                                        {
                                            //查询对应步号，找到对应是后处理还是染色工艺
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData("Select * from dye_details where StepNum = '" + lis_datas[j]._s_currentStepNum + "' and CupNum = " + i_cupNo);
                                            if (dt_dye_details.Rows.Count > 0)
                                            {
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE cup_details SET Cooperate = 5,ReceptionTime='" + DateTime.Now + "',DyeType = '" + dt_dye_details.Rows[0]["DyeType"].ToString() + "' WHERE CupNum = " + i_cupNo + " and Cooperate=0;");
                                            }
                                            else
                                            {
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET Cooperate = 5,DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " and Cooperate=0;");
                                            }
                                        }
                                        _cup_Temps[i_cupNo - 1]._i_cover = 1;
                                    }
                                }

                                //同步数据库杯盖状态到打板机
                                if (i == 0)
                                {
                                    if ((i_cupNo- i_cupmin+1) % 6 == 1)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus1)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);

                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI1.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 2)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus2)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI1.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus2 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 3)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus3)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI1.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus3 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 4)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus4)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dataTablecup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dataTablecup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dataTablecup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dataTablecup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI1.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus4 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 5)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus5)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI1.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus5 = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus6)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI1.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI1._b_isSendCoverStatus6 = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (i == 1)
                                {
                                    if ((i_cupNo - i_cupmin + 1) % 6 == 1)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus1)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI2.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 2)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus2)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI2.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus2 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 3)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus3)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI2.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus3 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 4)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus4)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI2.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus4 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 5)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus5)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI2.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus5 = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus6)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI2.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI2._b_isSendCoverStatus6 = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (i == 2)
                                {
                                    if ((i_cupNo - i_cupmin + 1) % 6 == 1)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus1)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI3.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 2)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus2)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI3.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus2 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 3)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus3)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI3.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus3 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 4)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus4)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI3.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus4 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 5)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus5)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI3.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus5 = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus6)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI3.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI3._b_isSendCoverStatus6 = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (i == 3)
                                {
                                    if ((i_cupNo - i_cupmin + 1) % 6 == 1)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus1)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_upstatus = new int[1];
                                                ia_upstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI4.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_upstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 2)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus2)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI4.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus2 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 3)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus3)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI4.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus3 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 4)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus4)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI4.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus4 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 5)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus5)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI4.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus5 = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus6)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_upstatus = new int[1];
                                                ia_upstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI4.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_upstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI4._b_isSendCoverStatus6 = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (i == 4)
                                {
                                    if ((i_cupNo - i_cupmin + 1) % 6 == 1)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus1)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_upstatus = new int[1];
                                                ia_upstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI5.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_upstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 2)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus2)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI5.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus2 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 3)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus3)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI5.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus3 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 4)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus4)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI5.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus4 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 5)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus5)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI5.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus5 = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus6)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI5.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI5._b_isSendCoverStatus6 = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (i == 5)
                                {
                                    if ((i_cupNo - i_cupmin + 1) % 6 == 1)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus1)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI6.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus1 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 2)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus2)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_upstatus = new int[1];
                                                ia_upstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI6.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_upstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus2 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 3)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus3)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI6.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus3 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 4)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus4)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_upstatus = new int[1];
                                                ia_upstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI6.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_upstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus4 = true;
                                                }
                                            }
                                        }
                                    }
                                    else if ((i_cupNo - i_cupmin + 1) % 6 == 5)
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus5)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI6.Write(117 + 64 * ((i_cupNo - i_cupmin + 1 - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus5 = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus6)
                                        {
                                            string s_sqlCupdetail = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlCupdetail);
                                            if (dt_cup_details.Rows.Count > 0)
                                            {
                                                _cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                int[] ia_cupstatus = new int[1];
                                                ia_cupstatus[0] = Convert.ToInt32(dt_cup_details.Rows[0]["CoverStatus"].ToString());
                                                //修改盖子状态
                                                if (FADM_Object.Communal._tcpDyeHMI6.Write(117 + 64 * ((i_cupNo - 1) % 6), ia_cupstatus) == 0)
                                                {
                                                    FADM_Object.Communal._tcpDyeHMI6._b_isSendCoverStatus6 = true;
                                                }
                                            }
                                        }
                                    }
                                }



                                string[] sa_statues = { "待机", "运行中", "暂停", "保温运行", "排水", "滴液", "停止中" };
                                string[] sa_technology = { "", "冷行", "温控", "加药", "放布", "出布", "排液", "洗杯", "加水", "搅拌", "待机保温", "快速冷却" };

                                if ("0" != lis_datas[j]._s_currentState)
                                {
                                    if ("3" != lis_datas[j]._s_currentState)
                                    {

                                        //运行非温控
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE cup_details SET StepStartTime = '" + DateTime.Now + "' " +
                                           "WHERE CupNum = " + i_cupNo + " AND Statues != '下线'  AND TechnologyName != '" +
                                           sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "';");

                                    }
                                    else
                                    {
                                        //保温运行
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET " +
                                            "StepStartTime = '" + DateTime.Now + "' " +
                                            "WHERE CupNum = " + i_cupNo + " AND TechnologyName != '保温运行' AND Statues != '下线';");
                                    }

                                }


                                //当前杯染固色完成
                                if ("1" == lis_datas[j]._s_isTotalFinish || "2" == lis_datas[j]._s_isTotalFinish || "3" == lis_datas[j]._s_isTotalFinish)
                                {
                                    //重置
                                    _ia_stopSend[i_cupNo - 1] = 0;

                                    int[] ia_zero = new int[16];
                                    for (int k = 0; k < 16; k++)
                                    {
                                        ia_zero[k] = 0;
                                    }
                                    ia_zero[1] = 0x0D0D;
                                    ia_zero[2] = 0x0D0D;
                                    ia_zero[3] = 0x0D0D;
                                    ia_zero[4] = 0x0D0D;
                                    DyeHMIWriteSigle(i, j, 100, 64, ia_zero);


                                    

                                    //清空全部完成标记位
                                    ia_zero = new int[1];
                                    ia_zero[0] = 0;
                                    DyeHMIWriteSigle(i, j, 501, 64, ia_zero);

                                    if ("1" == lis_datas[j]._s_isTotalFinish)
                                    {
                                        _cup_Temps[i_cupNo - 1]._b_start = false;
                                        _cup_Temps[i_cupNo - 1]._b_finish = true;
                                        Thread thread = new Thread(Finish);
                                        thread.Start(i_cupNo);



                                    }
                                    else if ("2" == lis_datas[j]._s_isTotalFinish)
                                    {
                                        //失败洗杯结束
                                        FADM_Object.Communal._lis_dripFailCupFinish.Add(i_cupNo);
                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯失败洗杯完成");
                                    }
                                    else
                                    {
                                        //前洗杯结束
                                        lock (this)
                                        {
                                            FADM_Object.Communal._lis_washCupFinish.Add(i_cupNo);
                                        }
                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯前洗杯完成");
                                    }


                                    continue;
                                }

                                if ("0" == lis_datas[j]._s_currentState)
                                {
                                    //待机

                                }

                                else if ("1" == lis_datas[j]._s_currentState)
                                {
                                    //运行中
                                    if ("3" != lis_datas[j]._s_dripFail)
                                        _cup_Temps[i_cupNo - 1]._b_start = true;
                                }
                                else if ("2" == lis_datas[j]._s_currentState)
                                {
                                    //暂停
                                    _cup_Temps[i_cupNo - 1]._b_start = false;
                                }
                                else if ("3" == lis_datas[j]._s_currentState)
                                {
                                    //保温运行
                                    _cup_Temps[i_cupNo - 1]._b_start = true;
                                }
                                else if ("4" == lis_datas[j]._s_currentState)
                                {
                                    //排水
                                    _cup_Temps[i_cupNo - 1]._b_start = true;
                                }
                                else if ("5" == lis_datas[j]._s_currentState)
                                {
                                    //滴液
                                    _cup_Temps[i_cupNo - 1]._b_start = false;
                                }
                                else if ("6" == lis_datas[j]._s_currentState)
                                {
                                    //停止中
                                    _cup_Temps[i_cupNo - 1]._b_start = false;
                                }




                                if ("0" == lis_datas[j]._s_currentState)
                                {
                                    if (_cup_Temps[i_cupNo - 1]._b_finish == false && _cup_Temps[i_cupNo - 1]._b_start == false)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                            "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                            "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "', " +
                                            "Statues = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                            "WHERE CupNum = " + i_cupNo + " AND Statues != '下线' AND Statues != '检查待机状态' " +
                                            "AND Statues != '检查历史状态' AND Statues != '等待准备状态' AND Statues != '失败洗杯' " +
                                            "AND Statues != '前洗杯' ;");

                                        _cup_Temps[i_cupNo - 1]._b_start = false;
                                    }
                                }
                                else if ("1" == lis_datas[j]._s_currentState)
                                {
                                    if ("2" == lis_datas[j]._s_currentCraft)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                        "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                        "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "' " +
                                        "WHERE CupNum = " + i_cupNo + " AND Statues != '下线' ;");
                                    }
                                    else
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                       "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                       "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "' " +
                                       "WHERE CupNum = " + i_cupNo + " AND Statues != '下线';");
                                    }
                                }
                                else if ("3" == lis_datas[j]._s_currentState)
                                {

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                       "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                       "TechnologyName = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                       "WHERE CupNum = " + i_cupNo + " AND Statues != '下线';");
                                }


                                else if ("5" == lis_datas[j]._s_currentState)
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                        "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                        "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "', " +
                                        "Statues = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                        "WHERE CupNum = " + i_cupNo + " AND Statues = '等待准备状态' AND Statues != '下线';");
                                }
                                else
                                {
                                    if ("2" == lis_datas[j]._s_currentCraft)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                           "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                           "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "', " +
                                           "Statues = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                           "WHERE CupNum = " + i_cupNo + " AND Statues != '下线'  AND TechnologyName != '保温运行';");
                                    }
                                    else
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                            "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                            "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "', " +
                                            "Statues = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                            "WHERE CupNum = " + i_cupNo + " AND Statues != '下线' ;");
                                    }
                                }

                                if ("4" == lis_datas[j]._s_currentCraft)
                                {
                                    if (_cup_Temps[i_cupNo - 1]._s_inTime == null)
                                    {
                                        // FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo(i_cupNo + "号杯放布");
                                        

                                        if (Communal._b_isUseClamp)
                                        {
                                            _cup_Temps[i_cupNo - 1]._s_inTime = "1";

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET Cooperate = 8,DyeType=1,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " and Cooperate=0;");
                                        }
                                        else
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                _cup_Temps[i_cupNo - 1]._s_inTime = Lib_Card.CardObject.InsertD(i_cupNo + "号杯放布", "Dye");
                                            else
                                                _cup_Temps[i_cupNo - 1]._s_inTime = Lib_Card.CardObject.InsertD(i_cupNo + " cup cloth placement", "Dye");
                                        }
                                    }

                                }
                                else if ("5" == lis_datas[j]._s_currentCraft)
                                {
                                    if (_cup_Temps[i_cupNo - 1]._s_outTime == null)
                                    {

                                        //判断已经开盖，才加入出布播报
                                        if (_cup_Temps[i_cupNo - 1]._i_cupCover == 2)
                                        {
                                            //FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo(i_cupNo + "号杯出布");
                                            
                                            if (Communal._b_isUseClamp)
                                            {
                                                _cup_Temps[i_cupNo - 1]._s_outTime = "2";

                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET Cooperate = 9,DyeType=1,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " and Cooperate=0;");
                                            }
                                            else
                                            {
                                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                    _cup_Temps[i_cupNo - 1]._s_outTime = Lib_Card.CardObject.InsertD(i_cupNo + "号杯出布", "Dye");
                                                else
                                                    _cup_Temps[i_cupNo - 1]._s_outTime = Lib_Card.CardObject.InsertD(i_cupNo + " cup discharge", "Dye");
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo(i_cupNo + "号杯放布");
                                    //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo(i_cupNo + "号杯出布");
                                    if (_cup_Temps[i_cupNo - 1]._s_inTime != null)
                                        Lib_Card.CardObject.DeleteD(_cup_Temps[i_cupNo - 1]._s_inTime);
                                    _cup_Temps[i_cupNo - 1]._s_inTime = null;
                                    if (_cup_Temps[i_cupNo - 1]._s_outTime != null)
                                        Lib_Card.CardObject.DeleteD(_cup_Temps[i_cupNo - 1]._s_outTime);
                                    _cup_Temps[i_cupNo - 1]._s_outTime = null;
                                }


                                //停止染色的杯号
                                if (FADM_Object.Communal._lis_dripStopCup.Contains(i_cupNo))
                                {
                                    if (sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] != "加药" && sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] != "加水")
                                    {

                                        //停止信号
                                        int[] ia_zero = new int[1];
                                        ia_zero[0] = 2;
                                        DyeHMIWriteSigle(i, j, 100, 64, ia_zero);

                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯染固色停止启动");
                                        //滴液完成数组移除当前杯号
                                        FADM_Object.Communal._lis_dripStopCup.Remove(i_cupNo);
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                  "UPDATE cup_details SET TotalWeight = 0, StepStartTime = '" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + ";");


                                        continue;
                                    }
                                }

                                //当前杯刚滴液成功
                                if (FADM_Object.Communal._lis_dripSuccessCup.Contains(i_cupNo) && "5" == lis_datas[j]._s_currentState)
                                {

                                    //重置数据
                                    int[] ia_zero = new int[16];
                                    for (int k1 = 0; k1 < 16; k1++)
                                    {
                                        ia_zero[k1] = 0;
                                    }
                                    ia_zero[1] = 0x0D0D;
                                    ia_zero[2] = 0x0D0D;
                                    ia_zero[3] = 0x0D0D;
                                    ia_zero[4] = 0x0D0D;
                                    DyeHMIWriteSigle(i, j, 100, 64, ia_zero);





                                    //染固色代码,总步号，滴液状态
                                    ia_zero = new int[6];
                                    byte[] byta_send = new byte[19];
                                    //染固色代码
                                    string s_sql = "SELECT * FROM drop_head WHERE CupNum = " + i_cupNo + ";";
                                    DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                    

                                    string s_assistantName = dt_drop_head.Rows[0]["DyeingCode"].ToString();
                                    string[] sa_name = { "000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D" };
                                    byte[] byta_assistantName = { 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D };
                                    int i_k = 0;
                                    for (int j1 = 0; j1 < s_assistantName.Length && j1 < sa_name.Length; j1++)
                                    {
                                        Encoding fromEcoding = Encoding.GetEncoding("UTF-8");//返回utf-8的编码
                                        Encoding toEcoding = Encoding.GetEncoding("gb2312");
                                        byte[] byta_fromBytes = fromEcoding.GetBytes(s_assistantName[j1].ToString());
                                        byte[] byta_tobytes = Encoding.Convert(fromEcoding, toEcoding, byta_fromBytes);
                                        if (byta_tobytes.Length > 1)
                                        {
                                            sa_name[i_k] = byta_tobytes[1].ToString("X") + byta_tobytes[0].ToString("X");
                                            byta_assistantName[2 * i_k] = byta_tobytes[1];
                                            byta_assistantName[2 * i_k + 1] = byta_tobytes[0];
                                        }
                                        else if (byta_tobytes.Length == 1)
                                        {
                                            if (i_k - 1 >= 0)
                                            {
                                                string s = (sa_name[i_k - 1]).Substring(0, 2);
                                                if (s == "00")
                                                {
                                                    sa_name[i_k - 1] = byta_tobytes[0].ToString("X") + sa_name[i_k - 1].Substring(2);
                                                    //byta_assistantName[2 * (i_k - 1) + 1] = byta_assistantName[2 * (i_k - 1)];
                                                    byta_assistantName[2 * (i_k - 1)] = byta_tobytes[0];
                                                    i_k--;
                                                }
                                                else
                                                {
                                                    sa_name[i_k] = "00" + byta_tobytes[0].ToString("X");
                                                    byta_assistantName[2 * i_k] = 0x00;
                                                    byta_assistantName[2 * i_k + 1] = byta_tobytes[0];
                                                }
                                            }
                                            else
                                            {
                                                sa_name[i_k] = "00" + byta_tobytes[0].ToString("X");
                                                byta_assistantName[2 * i_k] = 0x00;
                                                byta_assistantName[2 * i_k + 1] = byta_tobytes[0];
                                            }
                                        }
                                        i_k++;
                                    }

                                    ia_zero[0] = byta_assistantName[0] << 8 | byta_assistantName[1];
                                    ia_zero[1] = byta_assistantName[2] << 8 | byta_assistantName[3];
                                    ia_zero[2] = byta_assistantName[4] << 8 | byta_assistantName[5];
                                    ia_zero[3] = byta_assistantName[6] << 8 | byta_assistantName[7];


                                    //总步数
                                    s_sql = "SELECT * FROM dye_details WHERE CupNum = " + i_cupNo + " ORDER BY StepNum DESC;";
                                    dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                    ia_zero[4] = Convert.ToInt32(dt_drop_head.Rows[0]["StepNum"].ToString());
                                    ia_zero[5] = 1;
                                    
                                    DyeHMIWriteSigle(i, j, 101, 64, ia_zero);



                                    //启动
                                    ia_zero = new int[1];
                                    ia_zero[0] = 1;
                                    
                                    DyeHMIWriteSigle(i, j, 100, 64, ia_zero);

                                    //修改总步号
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET TotalStep = " + dt_drop_head.Rows[0]["StepNum"] + " WHERE CupNum = " + i_cupNo + ";");



                                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯染固色启动");
                                    //滴液完成数组移除当前杯号
                                    FADM_Object.Communal._lis_dripSuccessCup.Remove(i_cupNo);
                                    //重置
                                    _ia_stopSend[i_cupNo - 1] = 0;
                                    continue;
                                }

                                //当前杯刚滴液失败
                                if (FADM_Object.Communal._lis_dripFailCup.Contains(i_cupNo) && "5" == lis_datas[j]._s_currentState)
                                {
                                    //失败洗杯
                                    //染固色代码,总步号，滴液状态
                                    int[] ia_zero = new int[6];
                                    byte[] byta_send = new byte[19];
                                    //染固色代码
                                    byte[] byta_bytes = Encoding.GetEncoding("GBK").GetBytes("失败洗杯");
                                    for (int k = 7; k < 15; k++)
                                    {
                                        if (k - 7 > byta_bytes.Length - 1)
                                            byta_send[k] = 0x0D;
                                        else
                                        {
                                            if (0 == k % 2)
                                            {
                                                byta_send[k] = byta_bytes[k - 8];
                                            }
                                            else
                                            {
                                                if (k % 7 + 1 < byta_bytes.Length)
                                                    byta_send[k] = byta_bytes[k % 7 + 1];
                                                else
                                                    byta_send[k] = 0x0D;
                                            }
                                        }
                                    }
                                    ia_zero[0] = byta_send[7] << 8 | byta_send[8];
                                    ia_zero[1] = byta_send[9] << 8 | byta_send[10];
                                    ia_zero[2] = byta_send[11] << 8 | byta_send[12];
                                    ia_zero[3] = byta_send[13] << 8 | byta_send[14];

                                    //总步数
                                    ia_zero[4] = 1;
                                    ia_zero[5] = 2;
                                    
                                    DyeHMIWriteSigle(i, j, 101, 64, ia_zero);



                                    //启动
                                    ia_zero = new int[1];
                                    ia_zero[0] = 1;
                                    

                                    DyeHMIWriteSigle(i, j, 100, 64, ia_zero);

                                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯失败洗杯启动");
                                    //滴液完成数组移除当前杯号
                                    FADM_Object.Communal._lis_dripFailCup.Remove(i_cupNo);

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET Statues = '失败洗杯', TotalStep = 1 WHERE CupNum = " + i_cupNo + ";");
                                    continue;
                                }

                                //当前杯前洗杯
                                if (FADM_Object.Communal._lis_washCup.Contains(i_cupNo) && "0" == lis_datas[j]._s_currentState)
                                {

                                    

                                    //失败洗杯
                                    //染固色代码,总步号，滴液状态
                                    int[] ia_zero = new int[6];
                                    byte[] byta_send = new byte[19];
                                    //染固色代码
                                    byte[] byta_bytes = Encoding.GetEncoding("GBK").GetBytes("前洗杯");
                                    for (int k = 7; k < 15; k++)
                                    {
                                        if (k - 7 > byta_bytes.Length - 1)
                                            byta_send[k] = 0x0D;
                                        else
                                        {
                                            if (0 == k % 2)
                                            {
                                                byta_send[k] = byta_bytes[k - 8];
                                            }
                                            else
                                            {
                                                if (k % 7 + 1 < byta_bytes.Length)
                                                    byta_send[k] = byta_bytes[k % 7 + 1];
                                                else
                                                    byta_send[k] = 0x0D;
                                            }
                                        }
                                    }
                                    ia_zero[0] = byta_send[7] << 8 | byta_send[8];
                                    ia_zero[1] = byta_send[9] << 8 | byta_send[10];
                                    ia_zero[2] = byta_send[11] << 8 | byta_send[12];
                                    ia_zero[3] = byta_send[13] << 8 | byta_send[14];

                                    //总步数
                                    ia_zero[4] = 1;
                                    ia_zero[5] = 3;
                                    
                                    DyeHMIWriteSigle(i, j, 101, 64, ia_zero);



                                    //启动
                                    ia_zero = new int[1];
                                    ia_zero[0] = 1;
                                    
                                    DyeHMIWriteSigle(i, j, 100, 64, ia_zero);


                                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯前洗杯启动");
                                    //滴液完成数组移除当前杯号
                                    FADM_Object.Communal._lis_washCup.Remove(i_cupNo);

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET Statues = '前洗杯', TotalStep = 1 WHERE CupNum = " + i_cupNo + ";");
                                    continue;
                                }


                                //if ("2" == lis_datas[i_index]._s_openInplace)
                                {
                                    if (_cup_Temps[i_cupNo - 1]._i_addStatus == 0)
                                    //{

                                    //if (FADM_Object.Communal.ReadDyeThread() == null && _i_state == 2)
                                    {
                                        //洗杯加水
                                        if ("1" == lis_datas[j]._s_addWater)
                                        {
                                            

                                            Lib_Card.CardObject.DeleteD(_cup_Temps[i_cupNo - 1]._s_outTime);
                                            _cup_Temps[i_cupNo - 1]._s_outTime = null;
                                            _cup_Temps[i_cupNo - 1]._s_inTime = null;
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                               "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + ";");


                                            if (lis_datas[j]._s_currentState == "0" || lis_datas[j]._s_currentState == "6")
                                            {
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + "  ;");
                                            }
                                            else
                                            {
                                                //查询对应步号，找到对应是后处理还是染色工艺
                                                DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData("Select * from dye_details where StepNum = '" + lis_datas[j]._s_currentStepNum + "' and CupNum = " + i_cupNo);
                                                if (dt_dye_details.Rows.Count > 0)
                                                {
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET ReceptionTime='" + DateTime.Now + "',DyeType = '" + dt_dye_details.Rows[0]["DyeType"].ToString() + "' WHERE CupNum = " + i_cupNo + ";");
                                                }
                                                else
                                                {
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " ;");
                                                }
                                            }
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                "UPDATE cup_details SET Cooperate = 4 WHERE CupNum = " + i_cupNo + " And Cooperate !=6;");
                                            Txt.WriteTXTC(i_cupNo, "收到洗杯加水");

                                            //检查是否已把数据库更新
                                            DataTable dt_cup = _fadmSqlserver.GetData("Select * from cup_details WHERE CupNum = " + i_cupNo + " ;");
                                            if (dt_cup.Rows.Count > 0)
                                            {
                                                if (dt_cup.Rows[0]["Cooperate"].ToString() == "4")
                                                    _cup_Temps[i_cupNo - 1]._i_addStatus = 1;
                                            }

                                            continue;
                                        }
                                        else if ("2" == lis_datas[j]._s_addWater)
                                        {
                                            

                                            //加药
                                            string s_sql = "SELECT * FROM dye_details WHERE CupNum = " + i_cupNo + " AND " +
                                                  "StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum)) + ";";
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                            if (dt_dye_details.Rows.Count == 0)
                                            {
                                                continue;
                                            }
                                            int i_finish = Convert.ToInt16(dt_dye_details.Rows[0]["Finish"]);
                                            if (i_finish != 0)
                                                continue;



                                            if (dt_dye_details.Rows[0]["BottleNum"] is DBNull)
                                                continue;
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                               "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + ";");

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " AND StepNum != " + lis_datas[j]._s_currentStepNum + ";");
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE dye_details SET Cooperate = 1,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " AND StepNum = " + lis_datas[j]._s_currentStepNum + " AND Cooperate not in (5,6,7,8,9) ;");

                                            //检查是否已把数据库更新
                                            DataTable dt_cup = _fadmSqlserver.GetData("Select * from dye_details WHERE CupNum = " + i_cupNo + " AND StepNum = " + lis_datas[j]._s_currentStepNum + " ;");
                                            if (dt_cup.Rows.Count > 0)
                                            {
                                                if (dt_cup.Rows[0]["Cooperate"].ToString() == "1")
                                                    _cup_Temps[i_cupNo - 1]._i_addStatus = 1;
                                            }



                                            continue;

                                        }
                                        else if ("3" == lis_datas[j]._s_addWater)
                                        {
                                            //流程加水



                                            

                                            string s_sql = "SELECT * FROM dye_details WHERE CupNum = " + i_cupNo + " AND " +
                                               "StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum)) + ";";
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                            if (dt_dye_details.Rows.Count == 0)
                                            {
                                                continue;
                                            }
                                            int i_finish = Convert.ToInt16(dt_dye_details.Rows[0]["Finish"]);
                                            if (i_finish != 0)
                                                continue;

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                             "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + ";");

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " AND StepNum != " + lis_datas[j]._s_currentStepNum + " ;");
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE dye_details SET Cooperate = 3,ReceptionTime='" + DateTime.Now + "'  WHERE CupNum = " + i_cupNo + " AND StepNum = " + lis_datas[j]._s_currentStepNum + " And Cooperate !=9;");
                                            Txt.WriteTXTC(i_cupNo, "收到流程加水");

                                            //检查是否已把数据库更新
                                            DataTable dt_cup = _fadmSqlserver.GetData("Select * from dye_details WHERE CupNum = " + i_cupNo + " AND StepNum = " + lis_datas[j]._s_currentStepNum + " ;");
                                            if (dt_cup.Rows.Count > 0)
                                            {
                                                if (dt_cup.Rows[0]["Cooperate"].ToString() == "3")
                                                    _cup_Temps[i_cupNo - 1]._i_addStatus = 1;
                                            }

                                            continue;


                                        }
                                    }
                                    //}
                                    //else
                                    //{

                                    //    continue;
                                    //}
                                }

                                //等待数据
                                if ("1" == lis_datas[j]._s_waitData && "6" != lis_datas[j]._s_currentState)
                                {



                                    string s_sql;
                                    //if ("2" == lis_datas[i_index]._s_currentCraft)
                                    //{
                                    //    //当前步是温控
                                    //    s_sql = "UPDATE dye_details SET OvertempNum = " + Convert.ToInt16(lis_datas[i_index]._s_overTemTimes) + ", " +
                                    //        "OvertempTime = " + Convert.ToInt16(lis_datas[i_index]._s_overTime) + " WHERE CupNum = " + i_cupNo + " AND " +
                                    //        "StepNum = " + Convert.ToInt16(lis_datas[i_index]._s_currentStepNum) + ";";
                                    //    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    //}


                                    //滴液成功
                                    if ("1" == lis_datas[j]._s_dripFail)
                                    {
                                        if (_cup_Temps[i_cupNo - 1]._b_tagging == false)
                                        {
                                            if (sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] == "加药" || sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] == "加水")
                                            {
                                                //判断是否接收到停止信号
                                                if (FADM_Object.Communal._lis_dripStopCup.Contains(i_cupNo) || _ia_stopSend[i_cupNo - 1] == 1)
                                                {
                                                    //没有发送就发一次停止
                                                    if (_ia_stopSend[i_cupNo - 1] == 0)
                                                    {
                                                        _ia_stopSend[i_cupNo - 1] = 1;

                                                       
                                                        //发送停止
                                                        int[] ai_zero1 = new int[1];
                                                        ai_zero1[0] = 2;
                                                        DyeHMIWriteSigle(i, j, 100, 64, ai_zero1);

                                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯染固色停止启动");
                                                        //滴液完成数组移除当前杯号
                                                        FADM_Object.Communal._lis_dripStopCup.Remove(i_cupNo);
                                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                                  "UPDATE cup_details SET TotalWeight = 0, StepStartTime = '" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + ";");


                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                            }

                                            
                                            //复位等待数据
                                            int[] ai_zero = new int[1];
                                            ai_zero[0] = 0;
                                            DyeHMIWriteSigle(i, j, 500, 64, ai_zero);

                                            //写入下一步工艺
                                            s_sql = "SELECT * FROM dye_details WHERE CupNum = " + i_cupNo + " AND " +
                                                   "StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum) + 1) + ";";
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                            if (0 == dt_dye_details.Rows.Count)
                                                continue;

                                            s_sql = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                            string s_technologyName = dt_dye_details.Rows[0]["TechnologyName"].ToString();

                                            FADM_Object.Communal._fadmSqlserver.InsertRun(
                                                "Dail", i_cupNo + "号配液杯执行(" + Convert.ToInt32(dt_dye_details.Rows[0]["StepNum"]) + ":" + s_technologyName + ")");
                                            if (Convert.ToInt16(lis_datas[j]._s_currentStepNum) != 0)
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                    "UPDATE dye_details SET FinishTime = '" + DateTime.Now + "', Finish = 1 WHERE CupNum = " + i_cupNo +
                                                    " AND StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum)) + ";");

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                "UPDATE dye_details SET StartTime = '" + DateTime.Now + "' WHERE CupNum = " + i_cupNo +
                                                " AND StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum) + 1) + ";");



                                            if (Convert.ToInt16(dt_dye_details.Rows[0]["Temp"]) == 0)
                                            {
                                                if (Convert.ToInt16(dt_dye_details.Rows[0]["Time"]) == 0)
                                                {
                                                    s_sql = "UPDATE cup_details SET Statues = '" + dt_dye_details.Rows[0]["Code"] +
                                                        "',SetTemp = null, SetTime = null WHERE CupNum = " + i_cupNo + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                }
                                                else
                                                {
                                                    s_sql = "UPDATE cup_details SET Statues = '" + dt_dye_details.Rows[0]["Code"] +
                                                        "',SetTemp = null, SetTime = '" + Convert.ToInt32(dt_dye_details.Rows[0]["Time"]) + "' WHERE CupNum = " + i_cupNo + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToInt16(dt_dye_details.Rows[0]["Time"]) == 0)
                                                {
                                                    s_sql = "UPDATE cup_details SET Statues = '" + dt_dye_details.Rows[0]["Code"] + "',SetTemp = '" +
                                                        Convert.ToInt32(dt_dye_details.Rows[0]["Temp"]) + "', SetTime = null WHERE CupNum = " + i_cupNo + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                }
                                                else
                                                {
                                                    s_sql = "UPDATE cup_details SET Statues = '" + dt_dye_details.Rows[0]["Code"] +
                                                        "',SetTemp = '" + Convert.ToInt32(dt_dye_details.Rows[0]["Temp"]) + "', SetTime = '" +
                                                        Convert.ToInt32(dt_dye_details.Rows[0]["Time"]) + "' WHERE CupNum = " + i_cupNo + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                                }
                                            }


                                            _cup_Temps[i_cupNo - 1]._s_technologyName = s_technologyName;
                                            _cup_Temps[i_cupNo - 1]._b_tagging = true;

                                            


                                            //下发下一步工艺
                                            //计算排液时间用于下发
                                            int i_time = 0;
                                            ai_zero = new int[10];
                                            //当前步号
                                            ai_zero[0] = Convert.ToInt32(dt_dye_details.Rows[0]["StepNum"]);
                                            //当前名称
                                            if ("冷行" == s_technologyName || "Cool line" == s_technologyName)
                                                ai_zero[1] = 0x01;
                                            else if ("温控" == s_technologyName || "Temperature control" == s_technologyName)
                                                ai_zero[1] = 0x02;

                                            else if ("放布" == s_technologyName || "Entering the fabric" == s_technologyName)
                                            {
                                                ai_zero[1] = 0x04;
                                                //更新当前液量，当前液量加上布重X脱水水比
                                                s_sql = "select * from drop_head  WHERE CupNum = " + i_cupNo + ";";
                                                DataTable dt = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                                if (dt.Rows.Count > 0)
                                                {
                                                    double d_clothWeight = Convert.ToDouble(dt.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(dt.Rows[0]["AnhydrationWR"].ToString());
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET TotalWeight = TotalWeight + " + d_clothWeight + " WHERE CupNum = " + i_cupNo + ";");
                                                }
                                            }

                                            else if ("出布" == s_technologyName || "Outgoing fabric" == s_technologyName)
                                                ai_zero[1] = 0x05;

                                            else if ("排液" == s_technologyName || "Drainage" == s_technologyName)
                                            {
                                                ai_zero[1] = 0x06;

                                                //更新当前液量，布重X非脱水水比
                                                s_sql = "select * from drop_head  WHERE CupNum = " + i_cupNo + ";";
                                                DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                                DataTable dt_cup_details2 = FADM_Object.Communal._fadmSqlserver.GetData("select TotalWeight from cup_details WHERE CupNum = " + i_cupNo + "; ");
                                                if (dt_cup_details2.Rows[0][0] != System.DBNull.Value)
                                                {
                                                    i_time = Convert.ToInt32(Convert.ToDouble(dt_cup_details2.Rows[0][0].ToString()) * 1.3);
                                                    if (i_time < 100)
                                                    {
                                                        i_time = 100;
                                                    }
                                                    i_time /= 10;
                                                }

                                                if (dt_drop_head.Rows.Count > 0)
                                                {
                                                    double d_clothWeight = Convert.ToDouble(dt_drop_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(dt_drop_head.Rows[0]["Non_AnhydrationWR"].ToString());
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET TotalWeight =  " + d_clothWeight + " WHERE CupNum = " + i_cupNo + ";");
                                                }
                                            }
                                            else if ("洗杯" == s_technologyName || "Wash the cup" == s_technologyName)
                                            {
                                                ai_zero[1] = 0x07;
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                   "UPDATE cup_details SET TotalWeight = 0 WHERE CupNum = " + i_cupNo + ";");
                                            }
                                            else if ("加水" == s_technologyName || "Add Water" == s_technologyName)
                                                ai_zero[1] = 0x08;
                                            else if ("搅拌" == s_technologyName || "Stir" == s_technologyName)
                                                ai_zero[1] = 0x09;
                                            else
                                                ai_zero[1] = 0x03;


                                            //目标温度
                                            ai_zero[2] = Convert.ToInt32(Convert.ToDouble(dt_dye_details.Rows[0]["Temp"]) * 10);
                                            //温度速率
                                            ai_zero[3] = Convert.ToInt32(Convert.ToDouble(dt_dye_details.Rows[0]["TempSpeed"]) * 10);


                                            //保温时间/分
                                            ai_zero[4] = Convert.ToInt32(dt_dye_details.Rows[0]["Time"]);

                                            //排水时间
                                            ai_zero[5] = Convert.ToInt32(Convert.ToDouble(dt_dye_details.Rows[0]["RotorSpeed"]) * 10);
                                            if ("排液" == s_technologyName || "Drainage" == s_technologyName)
                                            {
                                                ai_zero[6] = i_time;
                                            }
                                            else
                                            {
                                                ai_zero[6] = 0;
                                            }

                                            //接受完成
                                            ai_zero[7] = 1;

                                            //
                                            ai_zero[8] = 0;

                                            //传当前液量
                                            if (dt_cup_details.Rows[0]["TotalWeight"] != System.DBNull.Value)
                                            {
                                                if (Convert.ToDouble(dt_cup_details.Rows[0]["TotalWeight"]) > 0)
                                                {
                                                    ai_zero[9] = Convert.ToInt32(Convert.ToDouble(dt_cup_details.Rows[0]["TotalWeight"]) * 100);
                                                }
                                                else
                                                {
                                                    ai_zero[9] = 0;
                                                }
                                            }
                                            else
                                            {
                                                ai_zero[9] = 0;
                                            }
                                            

                                            DyeHMIWriteSigle(i, j, 107, 64, ai_zero);
                                        }

                                    }
                                    else if ("3" == lis_datas[j]._s_dripFail || "2" == lis_datas[j]._s_dripFail)
                                    {

                                        //复位等待数据
                                        int[] ia_zero = new int[1];
                                        ia_zero[0] = 0;
                                        

                                        DyeHMIWriteSigle(i, j, 500, 64, ia_zero);

                                        //
                                        ia_zero = new int[8];
                                        //当前步号
                                        ia_zero[0] = 1;
                                        //当前步名称
                                        ia_zero[1] = 7;
                                        //目标温度
                                        ia_zero[2] = 0;
                                        //温度速率
                                        ia_zero[3] = 0;
                                        //保温时间/分
                                        ia_zero[4] = 1;
                                        //转子速率
                                        ia_zero[5] = 0;
                                        //排水时间
                                        ia_zero[6] = 0;
                                        //接收完成
                                        ia_zero[7] = 1;
                                        

                                        DyeHMIWriteSigle(i, j, 107, 64, ia_zero);

                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯执行(1:洗杯)工艺");


                                    }



                                    continue;

                                }

                            }
                        }
                        else
                        {
                            if (lis_datas.Count != 10)
                            {
                                if (++_ai_iArray[i] > 5)
                                {
                                    
                                    // FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo((i_erea + 1) + "号打板机通讯异常");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Object.Communal._sa_dyeConFTime[i] = Lib_Card.CardObject.InsertD((i + 1) + "号打板机通讯异常", "Dye");
                                    else
                                        FADM_Object.Communal._sa_dyeConFTime[i] = Lib_Card.CardObject.InsertD("Communication abnormality of " + (i + 1) + " board making machine", "Dye");
                                }
                                continue;
                            }
                            else
                            {
                                _ai_iArray[i] = 0;
                                Lib_Card.CardObject.DeleteD(FADM_Object.Communal._sa_dyeConFTime[i]);
                            }
                            int i_cupmin=0;
                            if (i == 0)
                            {
                                i_cupmin = Lib_Card.Configure.Parameter.Machine_Area1_CupMin;
                            }
                            else if (i == 1)
                            {
                                i_cupmin = Lib_Card.Configure.Parameter.Machine_Area2_CupMin;
                            }
                            else if (i == 2)
                            {
                                i_cupmin = Lib_Card.Configure.Parameter.Machine_Area3_CupMin;
                            }
                            else if (i == 3)
                            {
                                i_cupmin = Lib_Card.Configure.Parameter.Machine_Area4_CupMin;
                            }
                            else if (i == 4)
                            {
                                i_cupmin = Lib_Card.Configure.Parameter.Machine_Area5_CupMin;
                            }
                            else if (i == 5)
                            {
                                i_cupmin = Lib_Card.Configure.Parameter.Machine_Area6_CupMin;
                            }
                            int i_ret=-1;

                            if (i == 0)
                            {
                                if (!FADM_Object.Communal._tcpDyeHMI1._b_Connect)
                                {
                                    FADM_Object.Communal._tcpDyeHMI1.ReConnect();
                                }
                                i_ret = FADM_Object.Communal._tcpDyeHMI1.DyeReadAlarm();
                            }
                            else if (i == 1)
                            {
                                if (!FADM_Object.Communal._tcpDyeHMI2._b_Connect)
                                {
                                    FADM_Object.Communal._tcpDyeHMI2.ReConnect();
                                }
                                i_ret = FADM_Object.Communal._tcpDyeHMI2.DyeReadAlarm();
                            }
                            else if (i == 2)
                            {
                                if (!FADM_Object.Communal._tcpDyeHMI3._b_Connect)
                                {
                                    FADM_Object.Communal._tcpDyeHMI3.ReConnect();
                                }
                                i_ret = FADM_Object.Communal._tcpDyeHMI3.DyeReadAlarm();
                                //int[] ia_array = new int[1];
                                //ia_array[0] = 0;
                                //i_ret = FADM_Object.Communal._tcpDyeHMI3.Write(460, ia_array);
                            }
                            else if (i == 3)
                            {
                                if (!FADM_Object.Communal._tcpDyeHMI4._b_Connect)
                                {
                                    FADM_Object.Communal._tcpDyeHMI4.ReConnect();
                                }
                                i_ret = FADM_Object.Communal._tcpDyeHMI4.DyeReadAlarm();
                            }
                            else if (i == 4)
                            {
                                if (!FADM_Object.Communal._tcpDyeHMI5._b_Connect)
                                {
                                    FADM_Object.Communal._tcpDyeHMI5.ReConnect();
                                }
                                i_ret = FADM_Object.Communal._tcpDyeHMI5.DyeReadAlarm();
                            }
                            else if (i == 5)
                            {
                                if (!FADM_Object.Communal._tcpDyeHMI6._b_Connect)
                                {
                                    FADM_Object.Communal._tcpDyeHMI6.ReConnect();
                                }
                                i_ret = FADM_Object.Communal._tcpDyeHMI6.DyeReadAlarm();
                            }
                            //当报警代号不相同，先删除旧报警播报，再加入新报警播报
                            if (i_ret != FADM_Object.Communal._ia_alarmNum[i])
                            {
                                if (FADM_Object.Communal._ia_alarmNum[i] == 0)
                                {
                                    FADM_Object.Communal._ia_alarmNum[i] = i_ret;
                                    string s_alarm = "";
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    {
                                        if (i_ret / 10 == 0 || i_ret == 10)
                                        {
                                            if (i_ret != 10)
                                            {
                                                s_alarm = i_cupmin - 1 + i_ret % 10 + "号杯电机失步";
                                            }
                                            else
                                            {
                                                s_alarm = i_cupmin - 1 + 10 + "号杯电机失步";
                                            }
                                        }
                                        else if (i_ret / 10 == 1 || i_ret == 20)
                                        {
                                            if (i_ret != 20)
                                            {
                                                s_alarm = i_cupmin - 1 + i_ret % 10 + "号杯关盖异常";
                                            }
                                            else
                                            {
                                                s_alarm = i_cupmin - 1 + 10 + "号杯关盖异常";
                                            }
                                        }
                                        else if (i_ret / 10 == 2 || i_ret == 30)
                                        {
                                            if (i_ret != 30)
                                            {
                                                s_alarm = i_cupmin - 1 + i_ret % 10 + "号杯开盖异常";
                                            }
                                            else
                                            {
                                                s_alarm = i_cupmin - 1 + 10 + "号杯开盖异常";
                                            }
                                        }
                                        else
                                        {
                                            s_alarm = i + 1 + "号打板机气压不足";
                                        }
                                    }
                                    else
                                    {
                                        if (i_ret / 10 == 0 || i_ret == 10)
                                        {
                                            if (i_ret != 10)
                                            {
                                                s_alarm = i_cupmin - 1 + i_ret % 10 + " Number cup motor out of step";
                                            }
                                            else
                                            {
                                                s_alarm = i_cupmin - 1 + 10 + " Number cup motor out of step";
                                            }
                                        }
                                        else if (i_ret / 10 == 1 || i_ret == 20)
                                        {
                                            if (i_ret != 20)
                                            {
                                                s_alarm = i_cupmin - 1 + i_ret % 10 + " Abnormal closure of cup lid";
                                            }
                                            else
                                            {
                                                s_alarm = i_cupmin - 1 + 10 + " Abnormal closure of cup lid";
                                            }
                                        }
                                        else if (i_ret / 10 == 2 || i_ret == 30)
                                        {
                                            if (i_ret != 30)
                                            {
                                                s_alarm = i_cupmin - 1 + i_ret % 10 + " Abnormal opening of cup lid";
                                            }
                                            else
                                            {
                                                s_alarm = i_cupmin - 1 + 10 + " Abnormal opening of cup lid";
                                            }
                                        }
                                        else
                                        {
                                            s_alarm = i + 1 + " The air pressure of the plate making machine is insufficient";
                                        }
                                    }

                                    FADM_Object.Communal._sa_dyeAlarm[i] = Lib_Card.CardObject.InsertD(s_alarm, "Dye");
                                }
                                else
                                {
                                    FADM_Object.Communal._ia_alarmNum[i] = i_ret;
                                    Lib_Card.CardObject.DeleteD(FADM_Object.Communal._sa_dyeAlarm[i]);
                                    if (i_ret != 0)
                                    {
                                        string s_alarm = "";
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        {
                                            if (i_ret / 10 == 0 || i_ret == 10)
                                            {
                                                if (i_ret != 10)
                                                {
                                                    s_alarm = i_cupmin - 1 + i_ret % 10 + "号杯电机失步";
                                                }
                                                else
                                                {
                                                    s_alarm = i_cupmin - 1 + 10 + "号杯电机失步";
                                                }
                                            }
                                            else if (i_ret / 10 == 1 || i_ret == 20)
                                            {
                                                if (i_ret != 20)
                                                {
                                                    s_alarm = i_cupmin - 1 + i_ret % 10 + "号杯关盖异常";
                                                }
                                                else
                                                {
                                                    s_alarm = i_cupmin - 1 + 10 + "号杯关盖异常";
                                                }
                                            }
                                            else if (i_ret / 10 == 2 || i_ret == 30)
                                            {
                                                if (i_ret != 30)
                                                {
                                                    s_alarm = i_cupmin - 1 + i_ret % 10 + "号杯开盖异常";
                                                }
                                                else
                                                {
                                                    s_alarm = i_cupmin - 1 + 10 + "号杯开盖异常";
                                                }
                                            }
                                            else
                                            {
                                                s_alarm = i + 1 + "号打板机气压不足";
                                            }
                                        }
                                        else
                                        {
                                            if (i_ret / 10 == 0 || i_ret == 10)
                                            {
                                                if (i_ret != 10)
                                                {
                                                    s_alarm = i_cupmin - 1 + i_ret % 10 + " Number cup motor out of step";
                                                }
                                                else
                                                {
                                                    s_alarm = i_cupmin - 1 + 10 + " Number cup motor out of step";
                                                }
                                            }
                                            else if (i_ret / 10 == 1 || i_ret == 20)
                                            {
                                                if (i_ret != 20)
                                                {
                                                    s_alarm = i_cupmin - 1 + i_ret % 10 + " Abnormal closure of cup lid";
                                                }
                                                else
                                                {
                                                    s_alarm = i_cupmin - 1 + 10 + " Abnormal closure of cup lid";
                                                }
                                            }
                                            else if (i_ret / 10 == 2 || i_ret == 30)
                                            {
                                                if (i_ret != 30)
                                                {
                                                    s_alarm = i_cupmin - 1 + i_ret % 10 + " Abnormal opening of cup lid";
                                                }
                                                else
                                                {
                                                    s_alarm = i_cupmin - 1 + 10 + " Abnormal opening of cup lid";
                                                }
                                            }
                                            else
                                            {
                                                s_alarm = i + 1 + " The air pressure of the plate making machine is insufficient";
                                            }
                                        }
                                        FADM_Object.Communal._sa_dyeAlarm[i] = Lib_Card.CardObject.InsertD(s_alarm, "Dye");
                                    }
                                }
                            }

                            for (int j = 0; j < lis_datas.Count; j++)
                            {
                                int i_cupNo = i_cupmin - 1 + j + 1;
                                _cup_Temps[i_cupNo - 1]._s_temp = string.Format("{0:F1}", Convert.ToDouble(lis_datas[j]._s_realTem) / 10.0);
                                _cup_Temps[i_cupNo - 1]._s_statues = lis_datas[j]._s_currentState;
                                _cup_Temps[i_cupNo - 1]._s_history = lis_datas[j]._s_history;
                                //_cup_Temps[i_cupNo - 1]._i_cupCover = Convert.ToInt16(lis_datas[i_index]._s_openInplace);

                                if ("0" == lis_datas[j]._s_addWater)
                                {
                                    _cup_Temps[i_cupNo - 1]._i_addStatus = 0;
                                }
                                if ("3" != lis_datas[j]._s_openInplace)
                                {
                                    _cup_Temps[i_cupNo - 1]._i_stress = 0;
                                }


                                string[] sa_statues = { "待机", "运行中", "暂停", "保温运行", "排水", "滴液", "停止中" };
                                string[] sa_technology = { "", "冷行", "温控", "加药", "放布", "出布", "排液", "洗杯", "加水", "搅拌", "待机保温", "快速冷却" };

                                if ("0" != lis_datas[j]._s_currentState)
                                {
                                    if ("3" != lis_datas[j]._s_currentState)
                                    {

                                        //运行非温控
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE cup_details SET StepStartTime = '" + DateTime.Now + "' " +
                                           "WHERE CupNum = " + i_cupNo + " AND Statues != '下线'  AND TechnologyName != '" +
                                           sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "';");

                                    }
                                    else
                                    {
                                        //保温运行
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET " +
                                            "StepStartTime = '" + DateTime.Now + "' " +
                                            "WHERE CupNum = " + i_cupNo + " AND TechnologyName != '保温运行' AND Statues != '下线';");
                                    }

                                }


                                //当前杯染固色完成
                                if ("1" == lis_datas[j]._s_isTotalFinish || "2" == lis_datas[j]._s_isTotalFinish || "3" == lis_datas[j]._s_isTotalFinish)
                                {
                                    //重置
                                    _ia_stopSend[i_cupNo - 1] = 0;


                                    Lib_Card.CardObject.DeleteD(_cup_Temps[i_cupNo - 1]._s_inTime);
                                    _cup_Temps[i_cupNo - 1]._s_inTime = null;
                                    Lib_Card.CardObject.DeleteD(_cup_Temps[i_cupNo - 1]._s_outTime);
                                    _cup_Temps[i_cupNo - 1]._s_outTime = null;
                                    

                                    //重置
                                    int[] ia_zero = new int[16];
                                    for (int k = 0; k < 16; k++)
                                    {
                                        ia_zero[k] = 0;
                                    }
                                    ia_zero[1] = 0x0D0D;
                                    ia_zero[2] = 0x0D0D;
                                    ia_zero[3] = 0x0D0D;
                                    ia_zero[4] = 0x0D0D;
                                    

                                    DyeHMIWriteSigle(i, j, 100, 16, ia_zero);



                                    //清空全部完成标记位
                                    ia_zero = new int[1];
                                    ia_zero[0] = 0;
                                   

                                    DyeHMIWriteSigle(i, j, 301, 16, ia_zero);

                                    if ("1" == lis_datas[j]._s_isTotalFinish)
                                    {
                                        _cup_Temps[i_cupNo - 1]._b_start = false;
                                        _cup_Temps[i_cupNo - 1]._b_finish = true;
                                        Thread thread = new Thread(Finish);
                                        thread.Start(i_cupNo);



                                    }
                                    else if ("2" == lis_datas[j]._s_isTotalFinish)
                                    {
                                        //失败洗杯结束
                                        FADM_Object.Communal._lis_dripFailCupFinish.Add(i_cupNo);
                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯失败洗杯完成");
                                    }
                                    else
                                    {
                                        //前洗杯结束
                                        lock (this)
                                        {
                                            FADM_Object.Communal._lis_washCupFinish.Add(i_cupNo);
                                        }
                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯前洗杯完成");
                                    }


                                    continue;
                                }

                                if ("0" == lis_datas[j]._s_currentState)
                                {
                                    //待机

                                }

                                else if ("1" == lis_datas[j]._s_currentState)
                                {
                                    //运行中
                                    if ("3" != lis_datas[j]._s_dripFail)
                                        _cup_Temps[i_cupNo - 1]._b_start = true;
                                }
                                else if ("2" == lis_datas[j]._s_currentState)
                                {
                                    //暂停
                                    _cup_Temps[i_cupNo - 1]._b_start = false;
                                }
                                else if ("3" == lis_datas[j]._s_currentState)
                                {
                                    //保温运行
                                    _cup_Temps[i_cupNo - 1]._b_start = true;
                                }
                                else if ("4" == lis_datas[j]._s_currentState)
                                {
                                    //排水
                                    _cup_Temps[i_cupNo - 1]._b_start = true;
                                }
                                else if ("5" == lis_datas[j]._s_currentState)
                                {
                                    //滴液
                                    _cup_Temps[i_cupNo - 1]._b_start = false;
                                }
                                else if ("6" == lis_datas[j]._s_currentState)
                                {
                                    //停止中
                                    _cup_Temps[i_cupNo - 1]._b_start = false;
                                }




                                if ("0" == lis_datas[j]._s_currentState)
                                {
                                    if (_cup_Temps[i_cupNo - 1]._b_finish == false && _cup_Temps[i_cupNo - 1]._b_start == false)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                            "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                            "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "', " +
                                            "Statues = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                            "WHERE CupNum = " + i_cupNo + " AND Statues != '下线' AND Statues != '检查待机状态' " +
                                            "AND Statues != '检查历史状态' AND Statues != '等待准备状态' AND Statues != '失败洗杯' " +
                                            "AND Statues != '前洗杯' ;");

                                        _cup_Temps[i_cupNo - 1]._b_start = false;
                                    }
                                }
                                else if ("1" == lis_datas[j]._s_currentState)
                                {
                                    if ("2" == lis_datas[j]._s_currentCraft)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                        "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                        "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "' " +
                                        "WHERE CupNum = " + i_cupNo + " AND Statues != '下线' ;");
                                    }
                                    else
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                       "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                       "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "' " +
                                       "WHERE CupNum = " + i_cupNo + " AND Statues != '下线';");
                                    }
                                }
                                else if ("3" == lis_datas[j]._s_currentState)
                                {

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                       "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                       "TechnologyName = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                       "WHERE CupNum = " + i_cupNo + " AND Statues != '下线';");
                                }


                                else if ("5" == lis_datas[j]._s_currentState)
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                        "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                        "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "', " +
                                        "Statues = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                        "WHERE CupNum = " + i_cupNo + " AND Statues = '等待准备状态' AND Statues != '下线';");
                                }
                                else
                                {
                                    if ("2" == lis_datas[j]._s_currentCraft)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                           "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                           "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "', " +
                                           "Statues = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                           "WHERE CupNum = " + i_cupNo + " AND Statues != '下线'  AND TechnologyName != '保温运行';");
                                    }
                                    else
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET RealTemp = '" + _cup_Temps[i_cupNo - 1]._s_temp + "', " +
                                            "StepNum = " + lis_datas[j]._s_currentStepNum + ", " +
                                            "TechnologyName = '" + sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] + "', " +
                                            "Statues = '" + sa_statues[Convert.ToInt16(lis_datas[j]._s_currentState)] + "' " +
                                            "WHERE CupNum = " + i_cupNo + " AND Statues != '下线' ;");
                                    }
                                }

                                if ("4" == lis_datas[j]._s_currentCraft)
                                {
                                    if (_cup_Temps[i_cupNo - 1]._s_inTime == null)
                                    {
                                        // FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo(i_cupNo + "号杯放布");
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            _cup_Temps[i_cupNo - 1]._s_inTime = Lib_Card.CardObject.InsertD(i_cupNo + "号杯放布", "Dye");
                                        else
                                            _cup_Temps[i_cupNo - 1]._s_inTime = Lib_Card.CardObject.InsertD(i_cupNo + " Number cup with cloth", "Dye");
                                    }
                                }
                                else if ("5" == lis_datas[j]._s_currentCraft)
                                {
                                    if (_cup_Temps[i_cupNo - 1]._s_outTime == null)
                                    {
                                        //FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo(i_cupNo + "号杯出布");
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            _cup_Temps[i_cupNo - 1]._s_outTime = Lib_Card.CardObject.InsertD(i_cupNo + "号杯出布", "Dye");
                                        else
                                            _cup_Temps[i_cupNo - 1]._s_outTime = Lib_Card.CardObject.InsertD(i_cupNo + " Number cup out fabric", "Dye");

                                    }
                                }
                                else
                                {
                                    //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo(i_cupNo + "号杯放布");
                                    //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo(i_cupNo + "号杯出布");
                                    Lib_Card.CardObject.DeleteD(_cup_Temps[i_cupNo - 1]._s_inTime);
                                    _cup_Temps[i_cupNo - 1]._s_inTime = null;
                                    Lib_Card.CardObject.DeleteD(_cup_Temps[i_cupNo - 1]._s_outTime);
                                    _cup_Temps[i_cupNo - 1]._s_outTime = null;
                                }


                                //停止染色的杯号
                                if (FADM_Object.Communal._lis_dripStopCup.Contains(i_cupNo))
                                {
                                    if (sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] != "加药" && sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] != "加水")
                                    {
                                        

                                        //停止信号
                                        int[] ia_zero = new int[1];
                                        ia_zero[0] = 2;
                                        

                                        DyeHMIWriteSigle(i, j, 100, 16, ia_zero);

                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯染固色停止启动");
                                        //滴液完成数组移除当前杯号
                                        FADM_Object.Communal._lis_dripStopCup.Remove(i_cupNo);
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                  "UPDATE cup_details SET TotalWeight = 0, StepStartTime = '" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + ";");


                                        continue;
                                    }
                                }

                                //当前杯刚滴液成功
                                if (FADM_Object.Communal._lis_dripSuccessCup.Contains(i_cupNo) && "5" == lis_datas[j]._s_currentState)
                                {

                                    

                                    //重置数据
                                    int[] ia_zero = new int[16];
                                    for (int k1 = 0; k1 < 16; k1++)
                                    {
                                        ia_zero[k1] = 0;
                                    }
                                    ia_zero[1] = 0x0D0D;
                                    ia_zero[2] = 0x0D0D;
                                    ia_zero[3] = 0x0D0D;
                                    ia_zero[4] = 0x0D0D;
                                   

                                    DyeHMIWriteSigle(i, j, 100, 16, ia_zero);




                                    //染固色代码,总步号，滴液状态
                                    ia_zero = new int[6];
                                    byte[] byta_send = new byte[19];
                                    //染固色代码
                                    string s_sql = "SELECT * FROM drop_head WHERE CupNum = " + i_cupNo + ";";
                                    DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                    string s_assistantName = dt_drop_head.Rows[0]["DyeingCode"].ToString();
                                    string[] sa_name = { "000D", "000D", "000D", "000D", "000D", "000D", "000D", "000D" };
                                    byte[] byta_assistantName = { 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x0D };
                                    int i_k = 0;
                                    for (int j1 = 0; j1 < s_assistantName.Length && j1 < sa_name.Length; j1++)
                                    {
                                        Encoding fromEcoding = Encoding.GetEncoding("UTF-8");//返回utf-8的编码
                                        Encoding toEcoding = Encoding.GetEncoding("gb2312");
                                        byte[] byta_fromBytes = fromEcoding.GetBytes(s_assistantName[j1].ToString());
                                        byte[] byta_tobytes = Encoding.Convert(fromEcoding, toEcoding, byta_fromBytes);
                                        if (byta_tobytes.Length > 1)
                                        {
                                            sa_name[i_k] = byta_tobytes[1].ToString("X") + byta_tobytes[0].ToString("X");
                                            byta_assistantName[2 * i_k] = byta_tobytes[1];
                                            byta_assistantName[2 * i_k + 1] = byta_tobytes[0];
                                        }
                                        else if (byta_tobytes.Length == 1)
                                        {
                                            if (i_k - 1 >= 0)
                                            {
                                                string s_temp = (sa_name[i_k - 1]).Substring(0, 2);
                                                if (s_temp == "00")
                                                {
                                                    sa_name[i_k - 1] = byta_tobytes[0].ToString("X") + sa_name[i_k - 1].Substring(2);
                                                    //byta_assistantName[2 * (i_k - 1) + 1] = byta_assistantName[2 * (i_k - 1)];
                                                    byta_assistantName[2 * (i_k - 1)] = byta_tobytes[0];
                                                    i_k--;
                                                }
                                                else
                                                {
                                                    sa_name[i_k] = "00" + byta_tobytes[0].ToString("X");
                                                    byta_assistantName[2 * i_k] = 0x00;
                                                    byta_assistantName[2 * i_k + 1] = byta_tobytes[0];
                                                }
                                            }
                                            else
                                            {
                                                sa_name[i_k] = "00" + byta_tobytes[0].ToString("X");
                                                byta_assistantName[2 * i_k] = 0x00;
                                                byta_assistantName[2 * i_k + 1] = byta_tobytes[0];
                                            }
                                        }
                                        i_k++;
                                    }

                                    ia_zero[0] = byta_assistantName[0] << 8 | byta_assistantName[1];
                                    ia_zero[1] = byta_assistantName[2] << 8 | byta_assistantName[3];
                                    ia_zero[2] = byta_assistantName[4] << 8 | byta_assistantName[5];
                                    ia_zero[3] = byta_assistantName[6] << 8 | byta_assistantName[7];


                                    //总步数
                                    s_sql = "SELECT * FROM dye_details WHERE CupNum = " + i_cupNo + " ORDER BY StepNum DESC;";
                                    dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                    ia_zero[4] = Convert.ToInt32(dt_drop_head.Rows[0]["StepNum"].ToString());
                                    ia_zero[5] = 1;
                                   
                                    DyeHMIWriteSigle(i, j, 101, 16, ia_zero);



                                    //启动
                                    ia_zero = new int[1];
                                    ia_zero[0] = 1;
                                    

                                    DyeHMIWriteSigle(i, j, 100, 16, ia_zero);

                                    //修改总步号
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET TotalStep = " + dt_drop_head.Rows[0]["StepNum"] + " WHERE CupNum = " + i_cupNo + ";");



                                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯染固色启动");
                                    //滴液完成数组移除当前杯号
                                    FADM_Object.Communal._lis_dripSuccessCup.Remove(i_cupNo);
                                    //重置
                                    _ia_stopSend[i_cupNo - 1] = 0;
                                    continue;
                                }

                                //当前杯刚滴液失败
                                if (FADM_Object.Communal._lis_dripFailCup.Contains(i_cupNo) && "5" == lis_datas[j]._s_currentState)
                                {

                                    //失败洗杯
                                    //染固色代码,总步号，滴液状态
                                    int[] ia_zero = new int[6];
                                    byte[] byta_send = new byte[19];
                                    //染固色代码
                                    byte[] byta_bytes = Encoding.GetEncoding("GBK").GetBytes("失败洗杯");
                                    for (int k = 7; k < 15; k++)
                                    {
                                        if (k - 7 > byta_bytes.Length - 1)
                                            byta_send[k] = 0x0D;
                                        else
                                        {
                                            if (0 == k % 2)
                                            {
                                                byta_send[k] = byta_bytes[k - 8];
                                            }
                                            else
                                            {
                                                if (k % 7 + 1 < byta_bytes.Length)
                                                    byta_send[k] = byta_bytes[k % 7 + 1];
                                                else
                                                    byta_send[k] = 0x0D;
                                            }
                                        }
                                    }
                                    ia_zero[0] = byta_send[7] << 8 | byta_send[8];
                                    ia_zero[1] = byta_send[9] << 8 | byta_send[10];
                                    ia_zero[2] = byta_send[11] << 8 | byta_send[12];
                                    ia_zero[3] = byta_send[13] << 8 | byta_send[14];

                                    //总步数
                                    ia_zero[4] = 1;
                                    ia_zero[5] = 2;
                                   

                                    DyeHMIWriteSigle(i, j, 101, 16, ia_zero);



                                    //启动
                                    ia_zero = new int[1];
                                    ia_zero[0] = 1;
                                   

                                    DyeHMIWriteSigle(i, j, 100, 16, ia_zero);

                                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯失败洗杯启动");
                                    //滴液完成数组移除当前杯号
                                    FADM_Object.Communal._lis_dripFailCup.Remove(i_cupNo);

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET Statues = '失败洗杯', TotalStep = 1 WHERE CupNum = " + i_cupNo + ";");
                                    continue;
                                }

                                //当前杯前洗杯
                                if (FADM_Object.Communal._lis_washCup.Contains(i_cupNo) && "0" == lis_datas[j]._s_currentState)
                                {

                                    //失败洗杯
                                    //染固色代码,总步号，滴液状态
                                    int[] ia_zero = new int[6];
                                    byte[] byta_send = new byte[19];
                                    //染固色代码
                                    byte[] byta_bytes = Encoding.GetEncoding("GBK").GetBytes("前洗杯");
                                    for (int k = 7; k < 15; k++)
                                    {
                                        if (k - 7 > byta_bytes.Length - 1)
                                            byta_send[k] = 0x0D;
                                        else
                                        {
                                            if (0 == k % 2)
                                            {
                                                byta_send[k] = byta_bytes[k - 8];
                                            }
                                            else
                                            {
                                                if (k % 7 + 1 < byta_bytes.Length)
                                                    byta_send[k] = byta_bytes[k % 7 + 1];
                                                else
                                                    byta_send[k] = 0x0D;
                                            }
                                        }
                                    }
                                    ia_zero[0] = byta_send[7] << 8 | byta_send[8];
                                    ia_zero[1] = byta_send[9] << 8 | byta_send[10];
                                    ia_zero[2] = byta_send[11] << 8 | byta_send[12];
                                    ia_zero[3] = byta_send[13] << 8 | byta_send[14];

                                    //总步数
                                    ia_zero[4] = 1;
                                    ia_zero[5] = 3;
                                    

                                    DyeHMIWriteSigle(i, j, 101, 16, ia_zero);



                                    //启动
                                    ia_zero = new int[1];
                                    ia_zero[0] = 1;
                                    

                                    DyeHMIWriteSigle(i, j, 100, 16, ia_zero);

                                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯前洗杯启动");
                                    //滴液完成数组移除当前杯号
                                    FADM_Object.Communal._lis_washCup.Remove(i_cupNo);

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET Statues = '前洗杯', TotalStep = 1 WHERE CupNum = " + i_cupNo + ";");
                                    continue;
                                }


                                if ("2" == lis_datas[j]._s_openInplace)
                                {
                                    if (_cup_Temps[i_cupNo - 1]._i_addStatus == 0)
                                    //{

                                    //if (FADM_Object.Communal.ReadDyeThread() == null && _i_state == 2)
                                    {
                                        //洗杯加水
                                        if ("1" == lis_datas[j]._s_addWater)
                                        {
                                            
                                            Lib_Card.CardObject.DeleteD(_cup_Temps[i_cupNo - 1]._s_outTime);
                                            _cup_Temps[i_cupNo - 1]._s_outTime = null;
                                            _cup_Temps[i_cupNo - 1]._s_inTime = null;
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                               "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + ";");

                                            if (lis_datas[j]._s_currentState == "0" || lis_datas[j]._s_currentState == "6")
                                            {
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + "  ;");
                                            }
                                            else
                                            {
                                                //查询对应步号，找到对应是后处理还是染色工艺
                                                DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData("Select * from dye_details where StepNum = '" + lis_datas[j]._s_currentStepNum + "' and CupNum = " + i_cupNo);
                                                if (dt_dye_details.Rows.Count > 0)
                                                {
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET ReceptionTime='" + DateTime.Now + "',DyeType = '" + dt_dye_details.Rows[0]["DyeType"].ToString() + "' WHERE CupNum = " + i_cupNo + ";");
                                                }
                                                else
                                                {
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                            "UPDATE cup_details SET DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " ;");
                                                }
                                            }
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                "UPDATE cup_details SET Cooperate = 4 WHERE CupNum = " + i_cupNo + " And Cooperate !=6;");
                                            Txt.WriteTXTC(i_cupNo, "收到洗杯加水");

                                            //检查是否已把数据库更新
                                            DataTable dt_cup = _fadmSqlserver.GetData("Select * from cup_details WHERE CupNum = " + i_cupNo + " ;");
                                            if (dt_cup.Rows.Count > 0)
                                            {
                                                if (dt_cup.Rows[0]["Cooperate"].ToString() == "4")
                                                    _cup_Temps[i_cupNo - 1]._i_addStatus = 1;
                                            }

                                            continue;
                                        }
                                        else if ("2" == lis_datas[j]._s_addWater)
                                        {
                                            
                                            //加药
                                            string s_sql = "SELECT * FROM dye_details WHERE CupNum = " + i_cupNo + " AND " +
                                                  "StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum)) + ";";
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                            if (dt_dye_details.Rows.Count == 0)
                                            {
                                                continue;
                                            }
                                            int i_finish = Convert.ToInt16(dt_dye_details.Rows[0]["Finish"]);
                                            if (i_finish != 0)
                                                continue;



                                            if (dt_dye_details.Rows[0]["BottleNum"] is DBNull)
                                                continue;
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                               "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + ";");

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " AND StepNum != " + lis_datas[j]._s_currentStepNum + ";");
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE dye_details SET Cooperate = 1,ReceptionTime='" + DateTime.Now + "'  WHERE CupNum = " + i_cupNo + " AND StepNum = " + lis_datas[j]._s_currentStepNum + " AND Cooperate not in (5,6,7,8,9) ;");

                                            //检查是否已把数据库更新
                                            DataTable dt_cup = _fadmSqlserver.GetData("Select * from dye_details WHERE CupNum = " + i_cupNo + " AND StepNum = " + lis_datas[j]._s_currentStepNum + " ;");
                                            if (dt_cup.Rows.Count > 0)
                                            {
                                                if (dt_cup.Rows[0]["Cooperate"].ToString() == "1")
                                                    _cup_Temps[i_cupNo - 1]._i_addStatus = 1;
                                            }

                                            continue;

                                        }
                                        else if ("3" == lis_datas[j]._s_addWater)
                                        {
                                            //流程加水



                                            

                                            string s_sql = "SELECT * FROM dye_details WHERE CupNum = " + i_cupNo + " AND " +
                                               "StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum)) + ";";
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                            if (dt_dye_details.Rows.Count == 0)
                                            {
                                                continue;
                                            }
                                            int i_finish = Convert.ToInt16(dt_dye_details.Rows[0]["Finish"]);
                                            if (i_finish != 0)
                                                continue;

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                             "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + ";");

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " AND StepNum != " + lis_datas[j]._s_currentStepNum + " ;");
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE dye_details SET Cooperate = 3,ReceptionTime='" + DateTime.Now + "'  WHERE CupNum = " + i_cupNo + " AND StepNum = " + lis_datas[j]._s_currentStepNum + " And Cooperate !=9;");
                                            Txt.WriteTXTC(i_cupNo, "收到流程加水");

                                            //检查是否已把数据库更新
                                            DataTable dt_cup = _fadmSqlserver.GetData("Select * from dye_details WHERE CupNum = " + i_cupNo + " AND StepNum = " + lis_datas[j]._s_currentStepNum + " ;");
                                            if (dt_cup.Rows.Count > 0)
                                            {
                                                if (dt_cup.Rows[0]["Cooperate"].ToString() == "3")
                                                    _cup_Temps[i_cupNo - 1]._i_addStatus = 1;
                                            }

                                            continue;


                                        }
                                    }
                                    //}
                                    //else
                                    //{

                                    //    continue;
                                    //}
                                }

                                //等待数据
                                if ("1" == lis_datas[j]._s_waitData && "6" != lis_datas[j]._s_currentState)
                                {



                                    string sSql;
                                    //if ("2" == lis_datas[i_index]._s_currentCraft)
                                    //{
                                    //    //当前步是温控
                                    //    s_sql = "UPDATE dye_details SET OvertempNum = " + Convert.ToInt16(lis_datas[i_index]._s_overTemTimes) + ", " +
                                    //        "OvertempTime = " + Convert.ToInt16(lis_datas[i_index]._s_overTime) + " WHERE CupNum = " + i_cupNo + " AND " +
                                    //        "StepNum = " + Convert.ToInt16(lis_datas[i_index]._s_currentStepNum) + ";";
                                    //    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                    //}


                                    //滴液成功
                                    if ("1" == lis_datas[j]._s_dripFail)
                                    {
                                        if (_cup_Temps[i_cupNo - 1]._b_tagging == false)
                                        {
                                            if (sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] == "加药" || sa_technology[Convert.ToInt16(lis_datas[j]._s_currentCraft)] == "加水")
                                            {
                                                //判断是否接收到停止信号
                                                if (FADM_Object.Communal._lis_dripStopCup.Contains(i_cupNo) || _ia_stopSend[i_cupNo - 1] == 1)
                                                {
                                                    //没有发送就发一次停止
                                                    if (_ia_stopSend[i_cupNo - 1] == 0)
                                                    {
                                                        _ia_stopSend[i_cupNo - 1] = 1;

                                                        

                                                        //发送停止
                                                        int[] ia_zero1 = new int[1];
                                                        ia_zero1[0] = 2;
                                                       

                                                        DyeHMIWriteSigle(i, j, 100, 16, ia_zero1);

                                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯染固色停止启动");
                                                        //滴液完成数组移除当前杯号
                                                        FADM_Object.Communal._lis_dripStopCup.Remove(i_cupNo);
                                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                                  "UPDATE cup_details SET TotalWeight = 0, StepStartTime = '" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + ";");


                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                            }

                                           

                                            //复位等待数据
                                            int[] ia_Zero = new int[1];
                                            ia_Zero[0] = 0;
                                            

                                            DyeHMIWriteSigle(i, j, 300, 16, ia_Zero);

                                            //写入下一步工艺
                                            sSql = "SELECT * FROM dye_details WHERE CupNum = " + i_cupNo + " AND " +
                                                   "StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum) + 1) + ";";
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(sSql);
                                            if (0 == dt_dye_details.Rows.Count)
                                                continue;

                                            string s_temp = "SELECT * FROM cup_details WHERE CupNum = " + i_cupNo + ";";
                                            DataTable dt_cup_details = FADM_Object.Communal._fadmSqlserver.GetData(s_temp);

                                            string s_technologyName = dt_dye_details.Rows[0]["TechnologyName"].ToString();

                                            FADM_Object.Communal._fadmSqlserver.InsertRun(
                                                "Dail", i_cupNo + "号配液杯执行(" + Convert.ToInt32(dt_dye_details.Rows[0]["StepNum"]) + ":" + s_technologyName + ")");
                                            if (Convert.ToInt16(lis_datas[j]._s_currentStepNum) != 0)
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                    "UPDATE dye_details SET FinishTime = '" + DateTime.Now + "', Finish = 1 WHERE CupNum = " + i_cupNo +
                                                    " AND StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum)) + ";");

                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                "UPDATE dye_details SET StartTime = '" + DateTime.Now + "' WHERE CupNum = " + i_cupNo +
                                                " AND StepNum = " + (Convert.ToInt16(lis_datas[j]._s_currentStepNum) + 1) + ";");



                                            if (Convert.ToInt16(dt_dye_details.Rows[0]["Temp"]) == 0)
                                            {
                                                if (Convert.ToInt16(dt_dye_details.Rows[0]["Time"]) == 0)
                                                {
                                                    string s_sQL = "UPDATE cup_details SET Statues = '" + dt_dye_details.Rows[0]["Code"] +
                                                        "',SetTemp = null, SetTime = null WHERE CupNum = " + i_cupNo + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sQL);
                                                }
                                                else
                                                {
                                                    string s_sQL = "UPDATE cup_details SET Statues = '" + dt_dye_details.Rows[0]["Code"] +
                                                        "',SetTemp = null, SetTime = '" + Convert.ToInt32(dt_dye_details.Rows[0]["Time"]) + "' WHERE CupNum = " + i_cupNo + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sQL);
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToInt16(dt_dye_details.Rows[0]["Time"]) == 0)
                                                {
                                                    string s_sQL = "UPDATE cup_details SET Statues = '" + dt_dye_details.Rows[0]["Code"] + "',SetTemp = '" +
                                                        Convert.ToInt32(dt_dye_details.Rows[0]["Temp"]) + "', SetTime = null WHERE CupNum = " + i_cupNo + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sQL);
                                                }
                                                else
                                                {
                                                    string s_sQL = "UPDATE cup_details SET Statues = '" + dt_dye_details.Rows[0]["Code"] +
                                                        "',SetTemp = '" + Convert.ToInt32(dt_dye_details.Rows[0]["Temp"]) + "', SetTime = '" +
                                                        Convert.ToInt32(dt_dye_details.Rows[0]["Time"]) + "' WHERE CupNum = " + i_cupNo + ";";
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sQL);
                                                }
                                            }


                                            _cup_Temps[i_cupNo - 1]._s_technologyName = s_technologyName;
                                            _cup_Temps[i_cupNo - 1]._b_tagging = true;

                                           

                                            //下发下一步工艺
                                            //计算排液时间用于下发
                                            int i_Time = 0;
                                            ia_Zero = new int[9];
                                            //当前步号
                                            ia_Zero[0] = Convert.ToInt32(dt_dye_details.Rows[0]["StepNum"]);
                                            //当前名称
                                            if ("冷行" == s_technologyName || "Cool line" == s_technologyName)
                                                ia_Zero[1] = 0x01;
                                            else if ("温控" == s_technologyName || "Temperature control" == s_technologyName)
                                                ia_Zero[1] = 0x02;

                                            else if ("放布" == s_technologyName || "Entering the fabric" == s_technologyName)
                                            {
                                                ia_Zero[1] = 0x04;
                                                //更新当前液量，当前液量加上布重X脱水水比
                                                string s_sQL = "select * from drop_head  WHERE CupNum = " + i_cupNo + ";";
                                                DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sQL);

                                                if (dt_drop_head.Rows.Count > 0)
                                                {
                                                    double d_clothWeight = Convert.ToDouble(dt_drop_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(dt_drop_head.Rows[0]["AnhydrationWR"].ToString());
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET TotalWeight = TotalWeight + " + d_clothWeight + " WHERE CupNum = " + i_cupNo + ";");
                                                }
                                            }

                                            else if ("出布" == s_technologyName || "Outgoing fabric" == s_technologyName)
                                                ia_Zero[1] = 0x05;

                                            else if ("排液" == s_technologyName || "Drainage" == s_technologyName)
                                            {
                                                ia_Zero[1] = 0x06;

                                                //更新当前液量，布重X非脱水水比
                                                string s_sQL = "select * from drop_head  WHERE CupNum = " + i_cupNo + ";";
                                                DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sQL);

                                                DataTable dt_cup_details2 = FADM_Object.Communal._fadmSqlserver.GetData("select TotalWeight from cup_details WHERE CupNum = " + i_cupNo + "; ");
                                                if (dt_cup_details2.Rows[0][0] != System.DBNull.Value)
                                                {
                                                    i_Time = Convert.ToInt32(Convert.ToDouble(dt_cup_details2.Rows[0][0].ToString()) * 1.3);
                                                    if (i_Time < 100)
                                                    {
                                                        i_Time = 100;
                                                    }
                                                    i_Time /= 10;
                                                }

                                                if (dt_drop_head.Rows.Count > 0)
                                                {
                                                    double d_clothWeight = Convert.ToDouble(dt_drop_head.Rows[0]["ClothWeight"].ToString()) * Convert.ToDouble(dt_drop_head.Rows[0]["Non_AnhydrationWR"].ToString());
                                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET TotalWeight =  " + d_clothWeight + " WHERE CupNum = " + i_cupNo + ";");
                                                }
                                            }
                                            else if ("洗杯" == s_technologyName || "Wash the cup" == s_technologyName)
                                            {
                                                ia_Zero[1] = 0x07;
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                   "UPDATE cup_details SET TotalWeight = 0 WHERE CupNum = " + i_cupNo + ";");
                                            }
                                            else if ("加水" == s_technologyName || "Add Water" == s_technologyName)
                                                ia_Zero[1] = 0x08;
                                            else if ("搅拌" == s_technologyName || "Stir" == s_technologyName)
                                                ia_Zero[1] = 0x09;
                                            else
                                                ia_Zero[1] = 0x03;


                                            //目标温度
                                            ia_Zero[2] = Convert.ToInt32(Convert.ToDouble(dt_dye_details.Rows[0]["Temp"]) * 10);
                                            //温度速率
                                            ia_Zero[3] = Convert.ToInt32(Convert.ToDouble(dt_dye_details.Rows[0]["TempSpeed"]) * 10);

                                            if ("排液" == s_technologyName || "Drainage" == s_technologyName)
                                            {
                                                //排水时间
                                                ia_Zero[4] = i_Time;
                                            }
                                            else
                                            {
                                                //保温时间/分
                                                ia_Zero[4] = Convert.ToInt32(dt_dye_details.Rows[0]["Time"]);
                                            }

                                            //转子速率
                                            ia_Zero[5] = Convert.ToInt32(Convert.ToDouble(dt_dye_details.Rows[0]["RotorSpeed"]) * 10);

                                            //接受完成
                                            ia_Zero[6] = 1;

                                            //
                                            ia_Zero[7] = 0;

                                            //传当前液量
                                            if (dt_cup_details.Rows[0]["TotalWeight"] != System.DBNull.Value)
                                            {
                                                if (Convert.ToDouble(dt_cup_details.Rows[0]["TotalWeight"]) > 0)
                                                {
                                                    ia_Zero[8] = Convert.ToInt32(Convert.ToDouble(dt_cup_details.Rows[0]["TotalWeight"]) * 100);
                                                }
                                                else
                                                {
                                                    ia_Zero[8] = 0;
                                                }
                                            }
                                            else
                                            {
                                                ia_Zero[8] = 0;
                                            }
                                            

                                            DyeHMIWriteSigle(i, j, 107, 16, ia_Zero);
                                        }

                                    }
                                    else if ("3" == lis_datas[j]._s_dripFail || "2" == lis_datas[j]._s_dripFail)
                                    {

                                        

                                        //复位等待数据
                                        int[] ia_zero = new int[1];
                                        ia_zero[0] = 0;
                                        

                                        DyeHMIWriteSigle(i, j, 300, 16, ia_zero);





                                        ia_zero = new int[7];
                                        //当前步号
                                        ia_zero[0] = 1;
                                        //当前步名称
                                        ia_zero[1] = 7;
                                        //目标温度
                                        ia_zero[2] = 0;
                                        //温度速率
                                        ia_zero[3] = 0;
                                        //保温时间/分
                                        ia_zero[4] = 1;
                                        //转子速率
                                        ia_zero[5] = 0;
                                        //接收完成
                                        ia_zero[6] = 1;
                                       
                                        DyeHMIWriteSigle(i, j, 107, 16, ia_zero);

                                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯执行(1:洗杯)工艺");


                                    }



                                    continue;

                                }

                                if ("3" == lis_datas[j]._s_openInplace || "4" == lis_datas[j]._s_openInplace)
                                {
                                    //开关盖未到位

                                    if (_cup_Temps[i_cupNo - 1]._i_stress == 0)
                                    //if (FADM_Object.Communal.ReadDyeThread() == null && _i_state == 2)
                                    {

                                        

                                        _cup_Temps[i_cupNo - 1]._b_stressRelief = true;



                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                          "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");
                                        if (lis_datas[j]._s_currentState == "0" || lis_datas[j]._s_currentState == "6")
                                        {
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE cup_details SET DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + "  ;");
                                        }
                                        else
                                        {
                                            //查询对应步号，找到对应是后处理还是染色工艺
                                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData("Select * from dye_details where StepNum = '" + lis_datas[j]._s_currentStepNum + "' and CupNum = " + i_cupNo);
                                            if (dt_dye_details.Rows.Count > 0)
                                            {
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE cup_details SET ReceptionTime='" + DateTime.Now + "',DyeType = '" + dt_dye_details.Rows[0]["DyeType"].ToString() + "' WHERE CupNum = " + i_cupNo + ";");
                                            }
                                            else
                                            {
                                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET DyeType=2,ReceptionTime='" + DateTime.Now + "' WHERE CupNum = " + i_cupNo + " ;");
                                            }
                                        }
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                          "UPDATE cup_details SET Cooperate = 7 WHERE CupNum = " + i_cupNo + ";");

                                        _cup_Temps[i_cupNo - 1]._i_stress = 1;

                                    }



                                    continue;

                                }


                            }
                        }
                    }

                    DataTable dt_dye_details_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                            "SELECT * FROM dye_details WHERE Cooperate in (1,2,3,4);");
                    if (dt_dye_details_temp.Rows.Count > 0 && FADM_Object.Communal.ReadDyeThread() == null)
                    {
                        _b_state = false;
                        _i_state = 0;
                        FADM_Object.Communal.WriteDyeThread(new Thread(Cooperate));
                        FADM_Object.Communal.ReadDyeThread().Start();
                    }

                    dt_dye_details_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                        "SELECT * FROM cup_details WHERE Cooperate != 0 and Cooperate != 6;");
                    if (dt_dye_details_temp.Rows.Count > 0 && FADM_Object.Communal.ReadDyeThread() == null)
                    {
                        _b_state = false;
                        _i_state = 0;
                        FADM_Object.Communal.WriteDyeThread(new Thread(Cooperate));
                        FADM_Object.Communal.ReadDyeThread().Start();
                    }

                    if (_b_state)
                    {
                        _i_state = 2;
                    }



                }
                catch (Exception ex)
                {
                    string s_temp = "INSERT INTO alarm_table" +
                             "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                             " VALUES( '" +
                             String.Format("{0:d}", DateTime.Now) + "','" +
                             String.Format("{0:T}", DateTime.Now) + "','" +
                             "Dye" + "','" +
                             ex.ToString() + "(Test)');";

                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_temp);

                    //new FADM_Object.MyAlarm(ex.ToString(), "Dye", false, 0);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        new FADM_Object.MyAlarm(ex.Message, "Dye", false, 0);
                    else
                    {
                        string s_message = ex.Message;
                        if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                        {
                            //如果存在就替换英文
                            s_message = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                        }
                        new FADM_Object.MyAlarm(s_message, "Dye", false, 0);
                    }
                }
            }

        }


        private void Cooperate()
        {
            try
            {



                //等待机械手状态
                while (true)
                {
                    //判断当前机台状态
                    //滴液过程，可抢占线程
                    if (7 == FADM_Object.Communal.ReadMachineStatus())
                    {
                        if (FADM_Object.Communal.ReadDripWait())
                            break;
                    }
                    //待机状态下直接进入
                    else if (0 == FADM_Object.Communal.ReadMachineStatus())
                        break;
                    //其他状态下直接等待完成后再进行


                    Thread.Sleep(1);
                }

                bool b_return = false;
                MyModbusFun.Reset();
            //FADM_Auto.Reset.IOReset();
            //加药


            labTop:
                Lib_Log.Log.writeLogException("检查是否有其他动作启动");
                //查找所有染色阶段关盖记录
                DataTable dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
               "SELECT * FROM cup_details WHERE Cooperate = 5 and DyeType = 1 ORDER BY ReceptionTime ;");
                int i_firstCup = -1;
                if (dt_cupordye_details.Rows.Count > 0)
                {
                    i_firstCup = Convert.ToInt32(dt_cupordye_details.Rows[0]["CupNum"].ToString());
                }

                foreach (DataRow row in dt_cupordye_details.Rows)
                {
                    int i_cupNum = Convert.ToInt32(row["CupNum"].ToString());
                    //查找最早加药后的关盖请求
                    if (_cup_Temps[i_cupNum - 1]._s_statues != "0" && _cup_Temps[i_cupNum - 1]._s_statues != "6")
                    {
                        //
                        DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(
               "SELECT * FROM dye_details WHERE CupNum = " + i_cupNum + " and StepNum = " + (Convert.ToInt32(row["StepNum"].ToString()) - 1));
                        if (dt_dye_details.Rows.Count > 0)
                        {
                            //判断是否加药关盖
                            if (dt_dye_details.Rows[0]["TechnologyName"].ToString() == "加A" || dt_dye_details.Rows[0]["TechnologyName"].ToString() == "加B" || dt_dye_details.Rows[0]["TechnologyName"].ToString() == "加C" || dt_dye_details.Rows[0]["TechnologyName"].ToString() == "加D" || dt_dye_details.Rows[0]["TechnologyName"].ToString() == "加E")
                            {
                                int i_cupNo = Convert.ToInt16(row["CupNum"]);
                                if (_cup_Temps[i_cupNo - 1]._i_requesCupCover == 2)
                                {
                                    //再一次确定锁止再开始
                                    if (_cup_Temps[i_cupNo - 1]._i_lockUp == 1)
                                    {
                                        //判断是否拿住夹子
                                        if (Communal._b_isGetDryClamp)
                                        {
                                            //3.放夹子
                                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                            //int i_xStart = 0, i_yStart = 0;
                                            //计算干布夹子位置
                                            int i_xStart = 0, i_yStart = 0;
                                            MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                                            int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                            if (-2 == i_mRes)
                                                throw new Exception("收到退出消息");
                                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                                        }
                                        if (Communal._b_isGetWetClamp)
                                        {
                                            //3.放夹子
                                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                            //int i_xStart = 0, i_yStart = 0;
                                            //计算湿布布夹子位置
                                            int i_xStart = 0, i_yStart = 0;
                                            MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                                            int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                            if (-2 == i_mRes)
                                                throw new Exception("收到退出消息");
                                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                                        }

                                        SwitchCover(i_cupNo, 2);
                                    }
                                    else
                                    {
                                        FADM_Object.MyAlarm myAlarm;
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现锁止信号，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 3);
                                        else
                                            myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find lock sign, do you want to continue? " +
                                                "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 3);
                                    }
                                    //Thread.Sleep(2000);
                                }
                                else
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                        "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");
                                }

                                //_cup_Temps[i_cupNo - 1]._b_addSignal = false;
                                b_return = true;

                                goto labTop;
                            }
                        }
                    }

                }

                //查找到染色非加药的关盖
                if (i_firstCup != -1)
                {
                    if (_cup_Temps[i_firstCup - 1]._i_requesCupCover == 2)
                    {
                        //再一次确定锁止再开始
                        if (_cup_Temps[i_firstCup - 1]._i_lockUp == 1)
                        {
                            //判断是否拿住夹子
                            if (Communal._b_isGetDryClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算干布夹子位置
                                int i_xStart = 0, i_yStart = 0;
                                MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                                int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            if (Communal._b_isGetWetClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算湿布布夹子位置
                                int i_xStart = 0, i_yStart = 0;
                                MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                                int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            SwitchCover(i_firstCup, 2);
                        }
                        else
                        {
                            FADM_Object.MyAlarm myAlarm;
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_firstCup + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_firstCup + "号配液杯未发现锁止信号，是否继续执行?(继续执行请点是)", "SwitchCover", i_firstCup, 2, 3);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_firstCup + " Cup did not find lock sign, do you want to continue? " +
                                    "( Continue to perform please click Yes)", "SwitchCover", i_firstCup, 2, 3);
                        }
                        //Thread.Sleep(2000);
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_firstCup + " ;");
                    }

                    //_cup_Temps[i_firstCup - 1]._b_addSignal = false;
                    b_return = true;

                    goto labTop;
                }

                //加药

                List<int> list = new List<int>();
                DataTable dataTable = FADM_Object.Communal._fadmSqlserver.GetData(
                   "SELECT * FROM dye_details WHERE Cooperate = 1 ORDER BY CupNum ;");
                foreach (DataRow row in dataTable.Rows)
                {
                    int iBottleNo = Convert.ToInt16(row["BottleNum"]);
                    if (!list.Contains(iBottleNo))
                        list.Add(iBottleNo);
                }

                foreach (int i in list)
                {
                    dataTable = FADM_Object.Communal._fadmSqlserver.GetData(
                        "SELECT * FROM dye_details WHERE Cooperate = 1 AND BottleNum = " + i + " ORDER BY CupNum ;");

                    DyeAddMedicine(dataTable);


                }

                // //查找染色加药
                // dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
                //"SELECT top 1 * FROM dye_details WHERE Cooperate = 1 and DyeType = 1 ORDER BY ReceptionTime ;");

                // if (dt_cupordye_details.Rows.Count > 0)
                // {
                //     //判断是否拿住夹子
                //     if(Communal._b_isGetDryClamp)
                //     {
                //         //3.放夹子
                //         FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                //         //int i_xStart = 0, i_yStart = 0;
                //         //计算干布夹子位置
                //         int i_xStart = 0, i_yStart = 0;
                //         MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                //         int iMRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                //         if (-2 == iMRes)
                //             throw new Exception("收到退出消息");
                //         FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                //     }
                //     if (Communal._b_isGetWetClamp)
                //     {
                //         //3.放夹子
                //         FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                //         //int i_xStart = 0, i_yStart = 0;
                //         //计算湿布布夹子位置
                //         int i_xStart = 0, i_yStart = 0;
                //         MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                //         int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                //         if (-2 == i_mRes)
                //             throw new Exception("收到退出消息");
                //         FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                //     }

                //     DyeAddMedicine(dt_cupordye_details);

                //     //加药完成就马上关盖
                //     int iCupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                //     if (SmartDyeing.FADM_Object.Communal._dic_dyeType[iCupNo] == 1)
                //     {
                //         //判断是否加药完成
                //         DataTable dt_addme = FADM_Object.Communal._fadmSqlserver.GetData(
                //"SELECT * FROM dye_details WHERE CupNum = " + iCupNo + " and StepNum = " + Convert.ToInt32(dt_cupordye_details.Rows[0]["StepNum"].ToString()) + " and Finish = 1");
                //         if (dt_addme.Rows.Count > 0)
                //         {
                //             DataTable dt_dye = FADM_Object.Communal._fadmSqlserver.GetData(
                //"SELECT * FROM dye_details WHERE CupNum = " + iCupNo + " and StepNum = " + (Convert.ToInt32(dt_cupordye_details.Rows[0]["StepNum"].ToString()) + 1));
                //             if (dt_dye.Rows.Count > 0)
                //             {
                //                 //如果下个步骤是加药或加水就不关盖
                //                 if (dt_dye.Rows[0]["TechnologyName"].ToString() == "加A" || dt_dye.Rows[0]["TechnologyName"].ToString() == "加B" || dt_dye.Rows[0]["TechnologyName"].ToString() == "加C" || dt_dye.Rows[0]["TechnologyName"].ToString() == "加D" || dt_dye.Rows[0]["TechnologyName"].ToString() == "加E" || dt_dye.Rows[0]["TechnologyName"].ToString() == "加水")
                //                 {
                //                 }
                //                 else
                //                 {
                //                     //再一次确定锁止再开始
                //                     if (_cup_Temps[iCupNo - 1]._i_lockUp == 1)
                //                     {
                //                         //判断是否拿住夹子
                //                         if (Communal._b_isGetDryClamp)
                //                         {
                //                             //3.放夹子
                //                             FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                //                             //int i_xStart = 0, i_yStart = 0;
                //                             //计算干布夹子位置
                //                             int i_xStart = 0, i_yStart = 0;
                //                             MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                //                             int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                //                             if (-2 == i_mRes)
                //                                 throw new Exception("收到退出消息");
                //                             FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                //                         }
                //                         if (Communal._b_isGetWetClamp)
                //                         {
                //                             //3.放夹子
                //                             FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                //                             //int i_xStart = 0, i_yStart = 0;
                //                             //计算湿布布夹子位置
                //                             int i_xStart = 0, i_yStart = 0;
                //                             MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                //                             int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                //                             if (-2 == i_mRes)
                //                                 throw new Exception("收到退出消息");
                //                             FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                //                         }
                //                         SwitchCover(iCupNo, 2);
                //                     }
                //                 }
                //             }
                //         }
                //     }
                //     goto labTop;
                // }




                //查找染色开盖
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
               "SELECT top 1 * FROM cup_details WHERE Cooperate = 2 and DyeType = 1 ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int iCupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                    if (_cup_Temps[iCupNo - 1]._i_requesCupCover == 1)
                    {
                        //再一次确定锁止再开始
                        if (_cup_Temps[iCupNo - 1]._i_lockUp == 1)
                        {
                            //判断是否拿住夹子
                            if (Communal._b_isGetDryClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算干布夹子位置
                                int i_xStart = 0, i_yStart = 0;
                                MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                                int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            if (Communal._b_isGetWetClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算湿布布夹子位置
                                int i_xStart = 0, i_yStart = 0;
                                MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                                int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            SwitchCover(iCupNo, 1);
                        }
                        else
                        {
                            FADM_Object.MyAlarm myAlarm;
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + iCupNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(iCupNo + "号配液杯未发现锁止信号，是否继续执行?(继续执行请点是)", "SwitchCover", iCupNo, 2, 3);
                            else
                                myAlarm = new FADM_Object.MyAlarm(iCupNo + " Cup did not find lock sign, do you want to continue? " +
                                    "( Continue to perform please click Yes)", "SwitchCover", iCupNo, 2, 3);
                        }
                        //Thread.Sleep(2000);
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + iCupNo + " ;");
                    }

                    //_cup_Temps[i_cupNo - 1]._b_addSignal = false;
                    b_return = true;

                    goto labTop;
                }

                //查找染色流程加水
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
             "SELECT top 1 * FROM dye_details WHERE Cooperate = 3 and DyeType = 1  ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                    int i_stepNum = Convert.ToInt16(dt_cupordye_details.Rows[0]["StepNum"]);
                    double d_blWeight = Convert.ToDouble(dt_cupordye_details.Rows[0]["ObjectWaterWeight"]);
                    DyeAddWater(i_cupNo, d_blWeight,0);



                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " AND StepNum = " + i_stepNum + " ;");

                    _cup_Temps[i_cupNo - 1]._i_addStatus = 2;

                    b_return = true;

                    goto labTop;
                }

                //查找染色洗杯加水
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
             "SELECT top 1 * FROM cup_details WHERE Cooperate = 4 and DyeType = 1  ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                    double d_blWeight = Lib_Card.Configure.Parameter.Other_AddWater;
                    DyeAddWater(i_cupNo, d_blWeight,1);

                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                    "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");

                    _cup_Temps[i_cupNo - 1]._i_addStatus = 2;

                    b_return = true;

                    goto labTop;
                }

                //查找染色泄压
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
             "SELECT top 1 * FROM cup_details WHERE Cooperate = 7 and DyeType = 1  ORDER BY ReceptionTime ;");
                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);

                    Stressrelief(i_cupNo);
                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");

                    _cup_Temps[i_cupNo - 1]._b_stressRelief = false;
                    b_return = true;

                    goto labTop;
                }


                //查找所有后处理阶段关盖记录
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
               "SELECT top 1 * FROM cup_details WHERE Cooperate = 5 and DyeType = 2 ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);

                    if (_cup_Temps[i_cupNo - 1]._i_requesCupCover == 2)
                    {
                        //再一次确定锁止再开始
                        if (_cup_Temps[i_cupNo - 1]._i_lockUp == 1)
                        {
                            //判断是否拿住夹子
                            //判断是否拿住夹子
                            if (Communal._b_isGetDryClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算干布夹子位置
                                int i_xStart = 0, i_yStart = 0;
                                MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                                int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            if (Communal._b_isGetWetClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算湿布布夹子位置
                                int i_xStart = 0, i_yStart = 0;
                                MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                                int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            SwitchCover(i_cupNo, 2);
                        }
                        else
                        {
                            FADM_Object.MyAlarm myAlarm;
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现锁止信号，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 3);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find lock sign, do you want to continue? " +
                                    "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 3);
                        }
                        //Thread.Sleep(2000);
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");
                    }

                    //_cup_Temps[i_cupNo - 1]._b_addSignal = false;
                    b_return = true;

                    goto labTop;

                }

               // //查找后处理加药
               // dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
               //"SELECT top 1 * FROM dye_details WHERE Cooperate = 1 and DyeType = 2 ORDER BY ReceptionTime ;");

               // if (dt_cupordye_details.Rows.Count > 0)
               // {
               //     //判断是否拿住夹子
               //     //判断是否拿住夹子
               //     if (Communal._b_isGetDryClamp)
               //     {
               //         //3.放夹子
               //         FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
               //         //int i_xStart = 0, i_yStart = 0;
               //         //计算干布夹子位置
               //         int i_xStart = 0, i_yStart = 0;
               //         MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
               //         int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
               //         if (-2 == i_mRes)
               //             throw new Exception("收到退出消息");
               //         FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
               //     }
               //     if (Communal._b_isGetWetClamp)
               //     {
               //         //3.放夹子
               //         FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
               //         //int i_xStart = 0, i_yStart = 0;
               //         //计算湿布布夹子位置
               //         int i_xStart = 0, i_yStart = 0;
               //         MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
               //         int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
               //         if (-2 == i_mRes)
               //             throw new Exception("收到退出消息");
               //         FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
               //     }

               //     DyeAddMedicine(dt_cupordye_details);
               //     ////加药完成就马上关盖
               //     //int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
               //     //if (SmartDyeing.FADM_Object.Communal._dic_dyeType[i_cupNo] == 1)
               //     //{
               //     //    //再一次确定锁止再开始
               //     //    if (_cup_Temps[i_cupNo - 1]._i_lockUp == 1)
               //     //    {
               //     //        SwitchCover(i_cupNo, 2);
               //     //    }
               //     //}
               //     goto labTop;
               // }

                //查找后处理开盖
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
               "SELECT top 1 * FROM cup_details WHERE Cooperate = 2 and DyeType = 2 ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                    if (_cup_Temps[i_cupNo - 1]._i_requesCupCover == 1)
                    {
                        //再一次确定锁止再开始
                        if (_cup_Temps[i_cupNo - 1]._i_lockUp == 1)
                        {
                            //判断是否拿住夹子
                            if (Communal._b_isGetDryClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算干布夹子位置
                                int i_xStart = 0, i_yStart = 0;
                                MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                                int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            if (Communal._b_isGetWetClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算湿布布夹子位置
                                int i_xStart = 0, i_yStart = 0;
                                MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                                int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            SwitchCover(i_cupNo, 1);
                        }
                        else
                        {
                            FADM_Object.MyAlarm myAlarm;
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现锁止信号，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 3);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find lock sign, do you want to continue? " +
                                    "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 3);
                        }
                        //Thread.Sleep(2000);
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");
                    }

                    //_cup_Temps[i_cupNo - 1]._b_addSignal = false;
                    b_return = true;

                    goto labTop;
                }

                //查找后处理流程加水
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
             "SELECT top 1 * FROM dye_details WHERE Cooperate = 3 and DyeType = 2  ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                    int i_stepNum = Convert.ToInt16(dt_cupordye_details.Rows[0]["StepNum"]);
                    double d_blWeight = Convert.ToDouble(dt_cupordye_details.Rows[0]["ObjectWaterWeight"]);
                    DyeAddWater(i_cupNo, d_blWeight,0);



                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "UPDATE dye_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " AND StepNum = " + i_stepNum + " ;");

                    _cup_Temps[i_cupNo - 1]._i_addStatus = 2;

                    b_return = true;

                    goto labTop;
                }

                //查找后处理洗杯加水
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
             "SELECT top 1 * FROM cup_details WHERE Cooperate = 4 and DyeType = 2  ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                    double d_blWeight = Lib_Card.Configure.Parameter.Other_AddWater;
                    DyeAddWater(i_cupNo, d_blWeight,1);

                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                    "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");

                    _cup_Temps[i_cupNo - 1]._i_addStatus = 2;

                    b_return = true;

                    goto labTop;
                }

                //查找后处理泄压
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
             "SELECT top 1 * FROM cup_details WHERE Cooperate = 7 and DyeType = 2  ORDER BY ReceptionTime ;");
                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);

                    Stressrelief(i_cupNo);
                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");

                    _cup_Temps[i_cupNo - 1]._b_stressRelief = false;
                    b_return = true;

                    goto labTop;
                }

                //查找放布
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
               "SELECT top 1 * FROM cup_details WHERE Cooperate = 8  ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                    PutOrGetCloth(i_cupNo, 1);

                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");

                    goto labTop;
                }


                //查找出布
                dt_cupordye_details = FADM_Object.Communal._fadmSqlserver.GetData(
               "SELECT top 1 * FROM cup_details WHERE Cooperate = 9  ORDER BY ReceptionTime ;");

                if (dt_cupordye_details.Rows.Count > 0)
                {
                    int i_cupNo = Convert.ToInt16(dt_cupordye_details.Rows[0]["CupNum"]);
                    PutOrGetCloth(i_cupNo, 2);

                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "UPDATE cup_details SET Cooperate = 0 WHERE CupNum = " + i_cupNo + " ;");

                    goto labTop;
                }


                if (0 == FADM_Object.Communal.ReadMachineStatus())
                {
                    Lib_Log.Log.writeLogException("检查是否有其他动作完成");

                    //判断是否拿住夹子
                    if (Communal._b_isGetDryClamp)
                    {
                        //3.放夹子
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                        //int i_xStart = 0, i_yStart = 0;
                        //计算干布夹子位置
                        int i_xStart = 0, i_yStart = 0;
                        MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                        int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                        if (-2 == i_mRes)
                            throw new Exception("收到退出消息");
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                    }
                    if (Communal._b_isGetWetClamp)
                    {
                        //3.放夹子
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                        //int i_xStart = 0, i_yStart = 0;
                        //计算湿布布夹子位置
                        int i_xStart = 0, i_yStart = 0;
                        MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                        int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                        if (-2 == i_mRes)
                            throw new Exception("收到退出消息");
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                    }

                    //回到停止位
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "寻找待机位");
                    FADM_Object.Communal._i_optBottleNum = 0;
                    FADM_Object.Communal._i_OptCupNum = 0;
                    
                    if (b_return)
                    {
                        int i_mRes = MyModbusFun.TargetMove(3, 0, 1);
                        if (-2 == i_mRes)
                            throw new Exception("收到退出消息");
                    }
                    else
                    {
                        //不回待机位，失能关闭
                        MyModbusFun.Power(2);
                    }
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "抵达待机位");

                }
                else if (7 == FADM_Object.Communal.ReadMachineStatus())
                {
                    bool b_ds = false;
                    foreach (s_Cup _Cup in _cup_Temps)
                    {
                        if (_Cup._s_statues == "5")
                        {
                            b_ds = true;
                            break;
                        }
                    }
                    if (Drip._i_dripType == 2)
                    {
                        b_ds = true;
                    }
                    if (b_ds == false)
                    {
                        //判断是否拿住夹子
                        if (Communal._b_isGetDryClamp)
                        {
                            //3.放夹子
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                            //int i_xStart = 0, i_yStart = 0;
                            //计算干布夹子位置
                            int i_xStart = 0, i_yStart = 0;
                            MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                            int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                        }
                        if (Communal._b_isGetWetClamp)
                        {
                            //3.放夹子
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                            //int i_xStart = 0, i_yStart = 0;
                            //计算湿布布夹子位置
                            int i_xStart = 0, i_yStart = 0;
                            MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                            int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                        }

                        //回到停止位
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "寻找待机位");
                        //FADM_Object.Communal._i_optBottleNum = 0;
                        //FADM_Object.Communal._i_OptCupNum = 0;
                        int iMRes = MyModbusFun.TargetMove(3, 0, 1);
                        if (-2 == iMRes)
                            throw new Exception("收到退出消息");
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "抵达待机位");
                    }
                    else
                    {
                        //判断是否拿住夹子
                        if (Communal._b_isGetDryClamp)
                        {
                            //3.放夹子
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                            //int i_xStart = 0, i_yStart = 0;
                            //计算干布夹子位置
                            int i_xStart = 0, i_yStart = 0;
                            MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                            int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                        }
                        if (Communal._b_isGetWetClamp)
                        {
                            //3.放夹子
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                            //int i_xStart = 0, i_yStart = 0;
                            //计算湿布布夹子位置
                            int i_xStart = 0, i_yStart = 0;
                            MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                            int i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                        }
                    }
                }

                //把状态置为1，等待刷新最新状态才能响应
                _i_state = 1;
                FADM_Object.Communal.WriteDyeThread(null);
                FADM_Object.Communal.WriteDripWait(false);
                return;
            }
            catch (Exception ex)
            {
                FADM_Object.Communal.WriteMachineStatus(8);

                if (ex.Message.Equals("-2"))
                {
                    //string[] strArray = { "", "" };
                    ////根据编号读取异常信息
                    //MyModbusFun.GetErrMsg(ref strArray);
                    //if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    //    FADM_Form.CustomMessageBox.Show(strArray[1], "原点", MessageBoxButtons.OK, true);
                    //else
                    //    FADM_Form.CustomMessageBox.Show(strArray[1], "origin", MessageBoxButtons.OK, true);

                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);

                    List<string> sa_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Cooperate" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = Lib_Card.CardObject.InsertD(s_err, " Cooperate");
                                if (!sa_err.Contains(s_insert))
                                    sa_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_temp].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_temp].Choose;
                                //CardObject.DeleteD(s_temp);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = sa_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[sa_err[p]].Choose != 0)
                            {
                                Lib_Card.CardObject.DeleteD(sa_err[p]);
                                sa_err.Remove(sa_err[p]);
                            }
                        }
                        if (sa_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Cooperate", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Cooperate", MessageBoxButtons.OK, true);
                }

            }

        }

        /// <summary>
        /// 开关盖
        /// i_type:1 开盖 i_type:2 关盖
        /// </summary>
        private void SwitchCover(int i_cupNo,int i_type )
        {
            try
            {

                //等待机械手状态
                while (true)
                {
                    //判断当前机台状态
                    //滴液过程，可抢占线程
                    if (7 == FADM_Object.Communal.ReadMachineStatus())
                    {
                        if (FADM_Object.Communal.ReadDripWait())
                            break;
                    }
                    //待机状态下直接进入
                    else if (0 == FADM_Object.Communal.ReadMachineStatus())
                        break;
                    //其他状态下直接等待完成后再进行


                    Thread.Sleep(1);
                }
                //开盖
                if (i_type == 1)
                {
                    label1:
                    //开盖
                    try
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯开盖启动");

                        int i_xStart = 0, i_yStart = 0;
                        int i_xEnd = 0, i_yEnd = 0;
                        MyModbusFun.CalTarget(1, i_cupNo, ref i_xStart, ref i_yStart);

                        MyModbusFun.CalTarget(4, i_cupNo, ref i_xEnd, ref i_yEnd);

                        int i_mRes = MyModbusFun.OpenOrPutCover(i_xStart, i_yStart, i_xEnd, i_yEnd, 0);
                        if (-2 == i_mRes)
                            throw new Exception("收到退出消息");
                    }
                    catch (Exception ex) 
                    {
                        
                        if ("未发现杯盖" == ex.Message)
                        {
                        //    //抓手开
                        //    int[] ia_array = new int[1];
                        //    ia_array[0] = 7;
                        //lab812:
                        //    int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                        //    if (i_state == -1)
                        //        goto lab812;
                        //    Thread.Sleep(2000);
                        //    //气缸上
                        //    ia_array = new int[1];
                        //    ia_array[0] = 5;
                        //lab811:
                        //    i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                        //    if (i_state == -1)
                        //        goto lab811;

                            FADM_Object.MyAlarm myAlarm;
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现杯盖，是否继续执行?(继续执行请点是，已完成开盖请点否)", "SwitchCover", i_cupNo, 2, 2);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a cap, do you want to continue? " +
                                    "( Continue to perform please click Yes, have completed the opening please click No)", "SwitchCover", i_cupNo, 2, 2);
                            //while (true)

                            //{
                            //    if (0 != myAlarm._i_alarm_Choose)
                            //        break;
                            //    Thread.Sleep(1);
                            //}
                            //if(myAlarm._i_alarm_Choose == 1)
                            //{
                            //    goto label1;
                            //}
                            //else
                            //{
                            //    goto label2;
                            //}
                            return;
                        }
                        else if ("发现杯盖或针筒" == ex.Message)
                        {
                            FADM_Object.MyAlarm myAlarm;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm("请先拿住针筒或杯盖，然后点确定", "SwitchCover", true, 1);
                            else
                                myAlarm = new FADM_Object.MyAlarm("Please hold the syringe or cup lid first, and then confirm", "SwitchCover", true, 1);
                            while (true)

                            {
                                if (0 != myAlarm._i_alarm_Choose)
                                    break;
                                Thread.Sleep(1);
                            }
                            //抓手开
                            int[] ia_array = new int[1];
                            ia_array[0] = 7;
                            lab811:
                            int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                            if (i_state == -1)
                                goto lab811;
                            //等5秒后继续
                            Thread.Sleep(5000);
                            goto label1;
                        }
                        else if ("配液杯取盖失败" == ex.Message)
                        {

                            FADM_Object.MyAlarm myAlarm;
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 14);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove cap from dispensing cup, do you want to continue? " +
                                    "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 14);
                           
                            return;
                        }
                        else if ("放盖失败" == ex.Message)
                        {

                            FADM_Object.MyAlarm myAlarm;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖到杯盖区失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 22);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to place lid into lid area, do you want to continue? " +
                                    "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 22);

                            //认为开盖完成
                            goto label2;
                        }
                        //else if ("放盖区取盖失败" == ex.Message)
                        //{

                        //    FADM_Object.MyAlarm myAlarm;
                        //    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        //       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                        //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖区取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 2);
                        //    else
                        //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove the cover from the cover placement area, do you want to continue? " +
                        //            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 2);

                        //    return;
                        //}
                        //else if ("抓手A夹紧异常" == ex.Message|| "抓手B夹紧异常" == ex.Message)
                        //{

                        //    int[] ia_array = new int[1];

                        //    //抓手开
                        //    ia_array = new int[1];
                        //    ia_array[0] = 7;

                        //    int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);

                        //    Thread.Sleep(2000);

                        //    //气缸上
                        //    ia_array[0] = 5;

                        //    i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                        //    Thread.Sleep(1000);
                        //    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        //       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                        //    FADM_Object.MyAlarm myAlarm;
                        //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯关闭抓手异常，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 1,0);
                        //    else
                        //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup Close grip exception, do you want to continue? " +
                        //            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 1, 0);
                        //    //while (true)

                        //    //{
                        //    //    if (0 != myAlarm._i_alarm_Choose)
                        //    //        break;
                        //    //    Thread.Sleep(1);
                        //    //}
                        //    //goto label1;
                        //    return;
                        //}
                        else
                            throw;
                    }
                    

                    
                    label2:
                    //复位加药启动信号
                    int[] ia_zero = new int[1];
                    //
                    ia_zero[0] = 0;
                   

                    FADM_Auto.Dye.DyeOpenOrCloseCover(i_cupNo, 2);
                    _cup_Temps[i_cupNo - 1]._i_cover = 2;
                    Thread.Sleep(1000);

                    Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 2,Cooperate=0 where CupNum = " + i_cupNo);

                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯开盖完成");

                    _cup_Temps[i_cupNo - 1]._i_cupCover = 2;
                    

                }
                //关盖
                else if (i_type == 2)
                {

                    label3:
                    //开盖
                    try
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯关盖启动");

                        int i_xStart = 0, i_yStart = 0;
                        int i_xEnd = 0, i_yEnd = 0;
                        MyModbusFun.CalTarget(4, i_cupNo, ref i_xStart, ref i_yStart);

                        MyModbusFun.CalTarget(1, i_cupNo, ref i_xEnd, ref i_yEnd);

                        int iMRes = MyModbusFun.OpenOrPutCover(i_xStart, i_yStart, i_xEnd, i_yEnd, 1);
                        if (-2 == iMRes)
                            throw new Exception("收到退出消息");
                    }
                    catch (Exception ex)
                    {
                        
                        if ("未发现杯盖" == ex.Message)
                        {
                        //    //抓手开
                        //    int[] ia_array = new int[1];
                        //    ia_array[0] = 7;
                        //lab812:
                        //    int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                        //    if (i_state == -1)
                        //        goto lab812;

                        //    Thread.Sleep(2000);

                        //    //气缸上
                        //    ia_array = new int[1];
                        //    ia_array[0] = 5;
                        //lab811:
                        //    i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                        //    if (i_state == -1)
                        //        goto lab811;

                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            FADM_Object.MyAlarm myAlarm;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现杯盖，是否继续执行?(继续执行请点是，已完成关盖请点否)", "SwitchCover", i_cupNo, 2,1);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a cap, do you want to continue? " +
                                    "( Continue to perform please click Yes, have completed the close please click No)", "SwitchCover", i_cupNo, 2, 1);
                            return;
                            //while (true)

                            //{
                            //    if (0 != myAlarm._i_alarm_Choose)
                            //        break;
                            //    Thread.Sleep(1);
                            //}
                            //if (myAlarm._i_alarm_Choose == 1)
                            //{
                            //    goto label3;
                            //}
                            //else
                            //{
                                
                            //    goto label4;
                            //}
                        }
                        else if ("发现杯盖或针筒" == ex.Message)
                        {
                            FADM_Object.MyAlarm myAlarm;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm("请先拿住针筒或杯盖，然后点确定", "SwitchCover", true, 1);
                            else
                                myAlarm = new FADM_Object.MyAlarm("Please hold the syringe or cup lid first, and then confirm", "SwitchCover", true, 1);
                            while (true)

                            {
                                if (0 != myAlarm._i_alarm_Choose)
                                    break;
                                Thread.Sleep(1);
                            }
                            //抓手开
                            int[] ia_array = new int[1];
                            ia_array[0] = 7;

                            int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                            //等5秒后继续
                            Thread.Sleep(5000);
                            goto label3;
                        }
                        //else if ("配液杯取盖失败" == ex.Message)
                        //{

                        //    FADM_Object.MyAlarm myAlarm;
                        //    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        //       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                        //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 1);
                        //    else
                        //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove cap from dispensing cup, do you want to continue? " +
                        //            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 1);

                        //    return;
                        //}
                        else if ("关盖失败" == ex.Message)
                        {

                            FADM_Object.MyAlarm myAlarm;
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号关盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 12);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Closing failure, do you want to continue? " +
                                    "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 12);

                            return;
                        }
                        else if ("放盖区取盖失败" == ex.Message)
                        {

                            FADM_Object.MyAlarm myAlarm;
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖区取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 13);
                            else
                                myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove the cover from the cover placement area, do you want to continue? " +
                                    "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 13);

                            return;
                        }
                        //else if ("抓手A夹紧异常" == ex.Message || "抓手B夹紧异常" == ex.Message)
                        //{

                        //    int[] ia_array = new int[1];

                        //    //抓手开
                        //    ia_array = new int[1];
                        //    ia_array[0] = 7;

                        //    int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);

                        //    Thread.Sleep(2000);

                        //    //气缸上
                        //    ia_array[0] = 5;

                        //    i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                        //    Thread.Sleep(1000);
                        //    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        //       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                        //    FADM_Object.MyAlarm myAlarm;
                        //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯关闭抓手异常，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 1, 0);
                        //    else
                        //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup Close grip exception, do you want to continue? " +
                        //            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 1, 0);
                        //    while (true)

                        //    {
                        //        if (0 != myAlarm._i_alarm_Choose)
                        //            break;
                        //        Thread.Sleep(1);
                        //    }
                        //    goto label3;
                        //}
                        else
                            throw;
                    }
                    
                    label4:
                    //复位加药启动信号
                    int[] ia_zero = new int[1];
                    //
                    ia_zero[0] = 0;
                    

                    FADM_Auto.Dye.DyeOpenOrCloseCover(i_cupNo, 1);

                    _cup_Temps[i_cupNo - 1]._i_cover = 2;
                    Thread.Sleep(2000);
                    Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 1,Cooperate=0 where CupNum = " + i_cupNo);
                    
                    _cup_Temps[i_cupNo - 1]._i_cupCover = 1;

                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯关盖完成");


                }

                

            }
            catch (Exception ex)
            {

                if ("收到退出消息" == ex.Message)
                {
                    FADM_Object.Communal._b_stop = false;
                    //new Reset().MachineReset(0);
                    MyModbusFun.MyMachineReset(); //复位
                }

                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);

                    if (ex.Message.Equals("-2"))
                    {
                        throw;

                    }
                    else
                    {
                        string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "SwitchCover" + "','" +
                                  ex.ToString() + "(Test)');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        new FADM_Object.MyAlarm(ex.ToString(), "SwitchCover", false, 0);
                    }

                    //string[] strArray = { "", "" };
                    //if (ex.Message.Equals("-2"))
                    //{
                    //    //根据编号读取异常信息
                    //    MyModbusFun.GetErrMsg(ref strArray);
                    //}

                    //FADM_Object.Communal.WriteMachineStatus(8);

                    //if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    //    FADM_Form.CustomMessageBox.Show(ex.Message == "-2" ? strArray[1] : ex.ToString(), "SwitchCover", MessageBoxButtons.OK, true);
                    //else
                    //{
                    //    string s_message = ex.Message;
                    //    if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                    //    {
                    //        //如果存在就替换英文
                    //        s_message = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                    //    }
                    //    FADM_Form.CustomMessageBox.Show(s_message, "SwitchCover", MessageBoxButtons.OK, true);
                    //}
                }
            }
        }

        /// <summary>
        /// 放布取布
        /// i_type:1 放布 i_type:2 出布
        /// </summary>
        private void PutOrGetCloth(int i_cupNo, int i_type)
        {
            try
            {
                int i_cupNO = i_cupNo;
                int i_xStart = 0, i_yStart = 0;
                int i_xEnd = 0, i_yEnd = 0;
                int i_mRes = 0;
                //等待机械手状态
                while (true)
                {
                    //判断当前机台状态
                    //滴液过程，可抢占线程
                    if (7 == FADM_Object.Communal.ReadMachineStatus())
                    {
                        if (FADM_Object.Communal.ReadDripWait())
                            break;
                    }
                    //待机状态下直接进入
                    else if (0 == FADM_Object.Communal.ReadMachineStatus())
                        break;
                    //其他状态下直接等待完成后再进行


                    Thread.Sleep(1);
                }
                
                //先判断是否开盖
                if (SmartDyeing.FADM_Object.Communal._dic_dyeType[i_cupNO] == 1)
                {
                    //如果关盖状态，就先执行开盖动作
                    if (_cup_Temps[i_cupNO - 1]._i_cupCover == 1)
                    {
                    label1:
                        //开盖
                        try
                        {
                            //判断是否拿住夹子
                            if (Communal._b_isGetDryClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算干布夹子位置
                                MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                                i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }
                            if (Communal._b_isGetWetClamp)
                            {
                                //3.放夹子
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                                //int i_xStart = 0, i_yStart = 0;
                                //计算湿布布夹子位置
                                MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                                i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                            }


                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯开盖");
                            
                            MyModbusFun.CalTarget(1, i_cupNo, ref i_xStart, ref i_yStart);

                            MyModbusFun.CalTarget(4, i_cupNo, ref i_xEnd, ref i_yEnd);

                            i_mRes = MyModbusFun.OpenOrPutCover(i_xStart, i_yStart, i_xEnd, i_yEnd, 0);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");

                        }
                        catch (Exception ex)
                        {

                            if ("未发现杯盖" == ex.Message)
                            {
                                FADM_Object.MyAlarm myAlarm;

                                if (i_type == 1)
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现杯盖，是否继续执行?(继续执行请点是，已完成开盖请点否)", "SwitchCover", i_cupNo, 2, 8);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a cap, do you want to continue? " +
                                            "( Continue to perform please click Yes, have completed the opening please click No)", "SwitchCover", i_cupNo, 2, 8);
                                }
                                else
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现杯盖，是否继续执行?(继续执行请点是，已完成开盖请点否)", "SwitchCover", i_cupNo, 2, 9);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a cap, do you want to continue? " +
                                            "( Continue to perform please click Yes, have completed the opening please click No)", "SwitchCover", i_cupNo, 2, 9);
                                }

                                return;
                            }
                            else if ("发现杯盖或针筒" == ex.Message)
                            {
                                FADM_Object.MyAlarm myAlarm;
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    myAlarm = new FADM_Object.MyAlarm("请先拿住针筒或杯盖，然后点确定", "SwitchCover", true, 1);
                                else
                                    myAlarm = new FADM_Object.MyAlarm("Please hold the syringe or cup lid first, and then confirm", "SwitchCover", true, 1);
                                while (true)

                                {
                                    if (0 != myAlarm._i_alarm_Choose)
                                        break;
                                    Thread.Sleep(1);
                                }
                                //抓手开
                                int[] ia_array = new int[1];
                                ia_array[0] = 7;

                                int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                                //等5秒后继续
                                Thread.Sleep(5000);
                                goto label1;
                            }
                            else if ("配液杯取盖失败" == ex.Message)
                            {
                                if (i_type == 1)
                                {
                                    FADM_Object.MyAlarm myAlarm;
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 8);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove cap from dispensing cup, do you want to continue? " +
                                            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 8);
                                }
                                else
                                {
                                    FADM_Object.MyAlarm myAlarm;
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 9);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove cap from dispensing cup, do you want to continue? " +
                                            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 9);
                                }

                                return;
                            }
                            else if ("放盖失败" == ex.Message)
                            {

                                FADM_Object.MyAlarm myAlarm;
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖到杯盖区失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 22);
                                else
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to place lid into lid area, do you want to continue? " +
                                        "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 22);

                                //认为开盖完成
                                goto label2;
                            }
                            //else if ("放盖区取盖失败" == ex.Message)
                            //{

                            //    FADM_Object.MyAlarm myAlarm;
                            //    FADM_Object.Communal._fadmSqlserver.ReviseData(
                            //       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖区取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 9);
                            //    else
                            //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove the cover from the cover placement area, do you want to continue? " +
                            //            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 9);

                            //    return;
                            //}
                            else
                                throw;
                        }
                    label2:
                        //复位加药启动信号
                        int[] ia_zero1 = new int[1];
                        //
                        ia_zero1[0] = 0;


                        FADM_Auto.Dye.DyeOpenOrCloseCover(i_cupNo, 2);

                        Thread.Sleep(1000);
                        Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 2 where CupNum = " + i_cupNo);

                        _cup_Temps[i_cupNo - 1]._i_cupCover = 2;

                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯开盖完成");

                    }
                }
                //放布
                if (i_type == 1)
                {
                //1.拿夹子
                label3:
                    try
                    {

                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail",  "拿夹子启动");

                        //i_mRes = MyModbusFun.TargetMove(3, 0, 1);
                        //if (-2 == i_mRes)
                        //    throw new Exception("收到退出消息");

                        if (Communal._b_isGetWetClamp)
                        {
                            //3.放夹子
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                            //int i_xStart = 0, i_yStart = 0;
                            //计算湿布布夹子位置
                            MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                            i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                        }

                        if (!Communal._b_isGetDryClamp)
                        {
                            //计算干布夹子位置
                            MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                            i_mRes = MyModbusFun.GetClamp(i_xStart, i_yStart, 1);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                        }

                        //i_mRes = MyModbusFun.TargetMove(3, 0, 1);
                        //if (-2 == i_mRes)
                        //    throw new Exception("收到退出消息");

                        //int i_state = MyModbusFun.TargetMoveRelative(3, Convert.ToInt32(8000), Convert.ToInt32(8400), Convert.ToInt32(140000));
                        //if (i_state != 0 && i_state != -2)
                        //    throw new Exception("驱动异常");

                        //MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                        //i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                        //if (-2 == i_mRes)
                        //    throw new Exception("收到退出消息");

                    }
                    catch (Exception ex)
                    {

                        if ("未发现抓手" == ex.Message)
                        {
                            FADM_Object.MyAlarm myAlarm;

                            if (i_type == 1)
                            {
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                          "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现抓手，是否继续执行?(继续执行请点是)", "GetClamp", i_cupNo, 2, 8);
                                else
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a tongs, do you want to continue? " +
                                        "( Continue to perform please click Yes)", "GetClamp", i_cupNo, 2, 8);
                            }
                            else
                            {
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                          "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现抓手，是否继续执行?(继续执行请点是)", "GetClamp", i_cupNo, 2, 9);
                                else
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a tongs, do you want to continue? " +
                                        "( Continue to perform please click Yes)", "GetClamp", i_cupNo, 2, 9);
                            }

                            return;
                        }
                        else if ("发现杯盖或针筒" == ex.Message)
                        {
                            FADM_Object.MyAlarm myAlarm;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm("请先拿住针筒或杯盖，然后点确定", "SwitchCover", true, 1);
                            else
                                myAlarm = new FADM_Object.MyAlarm("Please hold the syringe or cup lid first, and then confirm", "SwitchCover", true, 1);
                            while (true)

                            {
                                if (0 != myAlarm._i_alarm_Choose)
                                    break;
                                Thread.Sleep(1);
                            }
                            //抓手开
                            int[] ia_array = new int[1];
                            ia_array[0] = 7;

                            int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                            //等5秒后继续
                            Thread.Sleep(5000);
                            goto label3;
                        }
                        else
                            throw;
                    }
                    

                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail",  "拿夹子完成");


                    //2.放布
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放布启动");
                    //计算干布夹子位置
                    MyModbusFun.CalTarget(6, i_cupNO, ref i_xStart, ref i_yStart);
                    MyModbusFun.CalTarget(1, i_cupNO, ref i_xEnd, ref i_yEnd);
                    i_mRes = MyModbusFun.PutOrGetCloth(i_xStart, i_yStart, i_xEnd, i_yEnd,0,1);
                    if (-2 == i_mRes)
                        throw new Exception("收到退出消息");
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放布完成");

                    ////3.放夹子
                    //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                    ////int i_xStart = 0, i_yStart = 0;
                    ////计算干布夹子位置
                    //MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                    //i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                    //if (-2 == i_mRes)
                    //    throw new Exception("收到退出消息");
                    //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");

                    int[] ia_zero = new int[1];
                    //放布完成
                    ia_zero[0] = 2;


                    DyeHMIWrite(i_cupNO, 118, 118, ia_zero);
                }
                //出布
                else if (i_type == 2)
                {
                //1.拿夹子
                label4:
                    
                    try
                    {
                        if (Communal._b_isGetDryClamp)
                        {
                            //3.放夹子
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                            //int i_xStart = 0, i_yStart = 0;
                            //计算干布夹子位置
                            MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                            int iMRes1 = MyModbusFun.PutClamp(i_xStart, i_yStart);
                            if (-2 == iMRes1)
                                throw new Exception("收到退出消息");
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                        }
                        if (!Communal._b_isGetWetClamp)
                        {
                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "拿夹子启动");
                            //计算湿布夹子位置
                            MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                            i_mRes = MyModbusFun.GetClamp(i_xStart, i_yStart, 2);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                        }

                    }
                    catch (Exception ex)
                    {

                        if ("未发现抓手" == ex.Message)
                        {
                            FADM_Object.MyAlarm myAlarm;

                            if (i_type == 1)
                            {
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                          "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现抓手，是否继续执行?(继续执行请点是)", "GetClamp", i_cupNo, 2, 8);
                                else
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a tongs, do you want to continue? " +
                                        "( Continue to perform please click Yes)", "GetClamp", i_cupNo, 2, 8);
                            }
                            else
                            {
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                          "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现抓手，是否继续执行?(继续执行请点是)", "GetClamp", i_cupNo, 2, 9);
                                else
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a tongs, do you want to continue? " +
                                        "( Continue to perform please click Yes)", "GetClamp", i_cupNo, 2, 9);
                            }

                            return;
                        }
                        else if ("发现杯盖或针筒" == ex.Message)
                        {
                            FADM_Object.MyAlarm myAlarm;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                myAlarm = new FADM_Object.MyAlarm("请先拿住针筒或杯盖，然后点确定", "SwitchCover", true, 1);
                            else
                                myAlarm = new FADM_Object.MyAlarm("Please hold the syringe or cup lid first, and then confirm", "SwitchCover", true, 1);
                            while (true)

                            {
                                if (0 != myAlarm._i_alarm_Choose)
                                    break;
                                Thread.Sleep(1);
                            }
                            //抓手开
                            int[] ia_array = new int[1];
                            ia_array[0] = 7;

                            int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                            //等5秒后继续
                            Thread.Sleep(5000);
                            goto label4;
                        }
                        else
                            throw;
                    }


                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "拿夹子完成");


                    //2.出布
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "出布启动");
                    //计算干布夹子位置
                    MyModbusFun.CalTarget(1, i_cupNO, ref i_xStart, ref i_yStart);
                    MyModbusFun.CalTarget(7, i_cupNO, ref i_xEnd, ref i_yEnd);
                    i_mRes = MyModbusFun.PutOrGetCloth(i_xStart, i_yStart, i_xEnd, i_yEnd, 1,0);
                    if (-2 == i_mRes)
                        throw new Exception("收到退出消息");
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "出布完成");

                    ////3.放夹子
                    //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                    ////int i_xStart = 0, i_yStart = 0;
                    ////计算湿布夹子位置
                    //MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                    //i_mRes = MyModbusFun.PutClamp(i_xStart, i_yStart);
                    //if (-2 == i_mRes)
                    //    throw new Exception("收到退出消息");
                    //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");

                    int[] ia_zero = new int[1];
                    //出布完成
                    ia_zero[0] = 1;


                    DyeHMIWrite(i_cupNO, 118, 118, ia_zero);
                }



            }
            catch (Exception ex)
            {

                if ("收到退出消息" == ex.Message)
                {
                    FADM_Object.Communal._b_stop = false;
                    //new Reset().MachineReset(0);
                    MyModbusFun.MyMachineReset(); //复位
                }

                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);

                    if (ex.Message.Equals("-2"))
                    {
                        throw;

                    }
                    else
                    {
                        string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "PutOrGetCloth" + "','" +
                                  ex.ToString() + "(Test)');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        new FADM_Object.MyAlarm(ex.ToString(), "PutOrGetCloth", false, 0);
                    }

                    //string[] strArray = { "", "" };
                    //if (ex.Message.Equals("-2"))
                    //{
                    //    //根据编号读取异常信息
                    //    MyModbusFun.GetErrMsg(ref strArray);
                    //}

                    //FADM_Object.Communal.WriteMachineStatus(8);

                    //if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    //    FADM_Form.CustomMessageBox.Show(ex.Message == "-2" ? strArray[1] : ex.ToString(), "SwitchCover", MessageBoxButtons.OK, true);
                    //else
                    //{
                    //    string s_message = ex.Message;
                    //    if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                    //    {
                    //        //如果存在就替换英文
                    //        s_message = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                    //    }
                    //    FADM_Form.CustomMessageBox.Show(s_message, "SwitchCover", MessageBoxButtons.OK, true);
                    //}
                }
            }
        }


        /// <summary>
        /// 泄压
        /// </summary>
        private void Stressrelief(int i_cupNo)
        {
            try
            {

                ////等待机械手状态
                //while (true)
                //{
                //    //判断当前机台状态
                //    if (0 != FADM_Object.Communal.ReadMachineStatus())
                //    {
                //        if (FADM_Object.Communal.ReadDripWait())
                //            break;
                //    }
                //    else
                //        break;

                //    Thread.Sleep(1);
                //}

                //等待机械手状态
                while (true)
                {
                    //判断当前机台状态
                    //滴液过程，可抢占线程
                    if (7 == FADM_Object.Communal.ReadMachineStatus())
                    {
                        if (FADM_Object.Communal.ReadDripWait())
                            break;
                    }
                    //待机状态下直接进入
                    else if (0 == FADM_Object.Communal.ReadMachineStatus())
                        break;
                    //其他状态下直接等待完成后再进行


                    Thread.Sleep(1);
                }
                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯泄压启动");

                ////寻找泄压位

                //int i_mRes = MyModbusFun.TargetMove(5, i_cupNo, 1);
                //if (-2 == i_mRes)
                //    throw new Exception("收到退出消息");

                int i_xStart = 0, i_yStart = 0;
                MyModbusFun.CalTarget(5, i_cupNo, ref i_xStart, ref i_yStart);

                MyModbusFun.Stressrelief(i_xStart, i_yStart);

                //复位加药启动信号
                int[] ia_zero = new int[1];
                //
                ia_zero[0] = 0;
                
                ia_zero[0] = 1;
                //只有转子机有泄压，所以填写一样
                DyeHMIWrite(i_cupNo, 114, 114, ia_zero);



                //再次复位泄压启动信号
                ia_zero = new int[1];
                //
                ia_zero[0] = 0;
                

                //只有转子机有泄压，所以填写一样
                DyeHMIWrite(i_cupNo, 308, 308, ia_zero);

                _cup_Temps[i_cupNo - 1]._i_stress = 2;

                //复位占用标志位
                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯泄压完成");

            }
            catch (Exception ex)
            {

                if ("收到退出消息" == ex.Message)
                {
                    FADM_Object.Communal._b_stop = false;
                    //new Reset().MachineReset(0);
                    MyModbusFun.MyMachineReset(); //复位
                }

                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);

                    if (ex.Message.Equals("-2"))
                    {
                        throw;

                    }
                    else
                    {
                        string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Stressrelief" + "','" +
                                  ex.ToString() + "(Test)');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        new FADM_Object.MyAlarm(ex.ToString(), "Stressrelief", false, 0);
                    }
                }
            }
        }



        /// <summary>
        /// 加药
        /// </summary>
        /// <param name="oMedicine">加药参数</param>
        private void DyeAddMedicine(DataTable dt)
        {
            try
            {
                int i_mRes = -1;
                //先把需要加药配液杯全部判断一次有没再开盖状态
                foreach (DataRow row in dt.Rows)
                {
                    int i_cupNo = Convert.ToInt32(row["CupNum"]);
                    if (SmartDyeing.FADM_Object.Communal._dic_dyeType[i_cupNo] == 1)
                    {
                        //如果关盖状态，就先执行开盖动作
                        if (FADM_Auto.Dye._cup_Temps[i_cupNo - 1]._i_cupCover == 1)
                        {
                        labelP1:
                            //开盖
                            try
                            {
                                //寻找配液杯
                                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯开盖");
                                int i_xStart = 0, i_yStart = 0;
                                int i_xEnd = 0, i_yEnd = 0;
                                MyModbusFun.CalTarget(1, i_cupNo, ref i_xStart, ref i_yStart);

                                MyModbusFun.CalTarget(4, i_cupNo, ref i_xEnd, ref i_yEnd);

                                i_mRes = MyModbusFun.OpenOrPutCover(i_xStart, i_yStart, i_xEnd, i_yEnd, 0);
                                if (-2 == i_mRes)
                                    throw new Exception("收到退出消息");
                            }
                            catch (Exception ex)
                            {
                                
                                if ("未发现杯盖" == ex.Message)
                                {
                                    ////气缸上
                                    //int[] ia_array = new int[1];
                                    //ia_array[0] = 5;

                                    //int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                                    //Thread.Sleep(1000);
                                    ////抓手开
                                    //ia_array = new int[1];
                                    //ia_array[0] = 7;

                                    //i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);

                                    FADM_Object.MyAlarm myAlarm;
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE dye_details SET  Cooperate = 9 WHERE  Cooperate = 1 AND  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现杯盖，是否继续执行?(继续执行请点是，已完成开盖请点否)", "SwitchCover", i_cupNo, 2, 4);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a cap, do you want to continue? " +
                                            "( Continue to perform please click Yes, have completed the opening please click No)", "SwitchCover", i_cupNo, 2, 4);
                                    //while (true)

                                    //{
                                    //    if (0 != myAlarm._i_alarm_Choose)
                                    //        break;
                                    //    Thread.Sleep(1);
                                    //}
                                    //if (myAlarm._i_alarm_Choose == 1)
                                    //{
                                    //    goto labelP1;
                                    //}
                                    //else
                                    //{
                                    //    goto labelP2;
                                    //}
                                    return;
                                }
                                else if ("发现杯盖或针筒" == ex.Message)
                                {
                                    FADM_Object.MyAlarm myAlarm;
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm("请先拿住针筒或杯盖，然后点确定", "SwitchCover", true, 1);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm("Please hold the syringe or cup lid first, and then confirm", "SwitchCover", true, 1);
                                    while (true)

                                    {
                                        if (0 != myAlarm._i_alarm_Choose)
                                            break;
                                        Thread.Sleep(1);
                                    }
                                    //抓手开
                                    int[] ia_array = new int[1];
                                    ia_array[0] = 7;

                                    int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                                    //等5秒后继续
                                    Thread.Sleep(5000);
                                    goto labelP1;
                                }
                                else if ("配液杯取盖失败" == ex.Message)
                                {

                                    FADM_Object.MyAlarm myAlarm;
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE dye_details SET  Cooperate = 9 WHERE  Cooperate = 1 AND  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 4);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove cap from dispensing cup, do you want to continue? " +
                                            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 4);

                                    return;
                                }
                                else if ("放盖失败" == ex.Message)
                                {

                                    FADM_Object.MyAlarm myAlarm;
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖到杯盖区失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 22);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to place lid into lid area, do you want to continue? " +
                                            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 22);

                                    //认为开盖完成
                                    goto labelP2;
                                }
                                //else if ("放盖区取盖失败" == ex.Message)
                                //{

                                //    FADM_Object.MyAlarm myAlarm;
                                //    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                //       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖区取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 4);
                                //    else
                                //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove the cover from the cover placement area, do you want to continue? " +
                                //            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 4);

                                //    return;
                                //}

                                else
                                    throw;
                            }
                        labelP2:
                            //复位加药启动信号
                            int[] ia_zero1 = new int[1];
                            //
                            ia_zero1[0] = 0;
                            

                            FADM_Auto.Dye.DyeOpenOrCloseCover(i_cupNo, 2);
                            Thread.Sleep(1000);
                            Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 2 where CupNum = " + i_cupNo);

                            FADM_Auto.Dye._cup_Temps[i_cupNo - 1]._i_cupCover = 2;

                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯开盖完成");


                        }
                    }
                }

                //针检失败，不继续针检状态
                bool b_checkFail = false;

                int i_bottleNo = Convert.ToInt16(dt.Rows[0]["BottleNum"]);
                List<int> lis_ints = new List<int>();

                string s_unitOfAccount = "";

            label9:
                //判断当前母液瓶液量是否足够
                string s_sql = "SELECT bottle_details.*,assistant_details.AllowMinColoringConcentration,assistant_details.AllowMaxColoringConcentration  " +
                              "FROM bottle_details left join assistant_details on bottle_details.AssistantCode = assistant_details.AssistantCode WHERE bottle_details.BottleNum = " + i_bottleNo + ";";
                DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                int i_adjust = Convert.ToInt32(dt_temp.Rows[0]["AdjustValue"]);
                bool b_checkSuccess = (Convert.ToString(dt_temp.Rows[0]["AdjustSuccess"]) == "1");
                string s_syringeType = Convert.ToString(dt_temp.Rows[0]["SyringeType"]);

                double d_blCurrentWeight = Convert.ToDouble(dt_temp.Rows[0]["CurrentWeight"]);

                double d_blCompCoefficient = Convert.ToDouble(dt_temp.Rows[0]["AllowMinColoringConcentration"]);
                double d_blCompConstant = Convert.ToDouble(dt_temp.Rows[0]["AllowMaxColoringConcentration"]);




                if (d_blCurrentWeight <= Lib_Card.Configure.Parameter.Other_Bottle_MinWeight && FADM_Object.Communal._b_isLowDrip)
                {//查询在备料表是否存在记录，如果存在，先让客户选择是否使用备料数据来更新
                    string s_sqlpre = "SELECT * FROM pre_brew WHERE  BottleNum = " + i_bottleNo + ";";
                    DataTable dt_pre_brew = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlpre);
                    if (dt_pre_brew.Rows.Count > 0)
                    {
                        if (!_dic_keyValue.ContainsKey(i_bottleNo))
                        {
                            //存在并且点了一次是
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                             "UPDATE dye_details SET  Cooperate = 5 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶液量过低,备料表存在已开料记录，是否替换(替换请点是，继续使用旧母液请点否)", i_bottleNo, 5, MessageBoxButtons.YesNo);
                            else
                                new FADM_Object.MyAlarm( " The liquid volume of the "+i_bottleNo +" mother liquor bottle is too low, and there is a record of already opened materials in the material preparation table. Should it be replaced (please click Yes for replacement, and click No for continuing to use the old mother liquor)", i_bottleNo, 5, MessageBoxButtons.YesNo);
                            return;
                        }
                    }
                    else
                    {
                        if (_dic_keyValue.ContainsKey(i_bottleNo))
                        {
                            if (_dic_keyValue[i_bottleNo] == false)
                            {
                                //存在并且点了一次是
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                     "UPDATE dye_details SET  Cooperate = 5 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶液量过低,忽略液量低提示请点否，等待泡制完成后再滴请点是", i_bottleNo, 5, MessageBoxButtons.YesNo);
                                else
                                    new FADM_Object.MyAlarm("The liquid level in the " + i_bottleNo + " mother liquor bottle is too low. Ignoring the low liquid level prompt, please click no, and wait for the brewing to be completed before dripping. Please click yes", i_bottleNo, 5, MessageBoxButtons.YesNo);
                                return;
                            }

                        }
                        else
                        {
                            _dic_keyValue.Add(i_bottleNo, false);
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                     "UPDATE dye_details SET  Cooperate = 5 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶液量过低,忽略液量低提示请点否，等待泡制完成后再滴请点是", i_bottleNo, 5, MessageBoxButtons.YesNo);
                            else
                                new FADM_Object.MyAlarm("The liquid level in the " + i_bottleNo + " mother liquor bottle is too low. Ignoring the low liquid level prompt, please click no, and wait for the brewing to be completed before dripping. Please click yes", i_bottleNo, 5, MessageBoxButtons.YesNo);
                            return;
                        }
                    }
                }


                //判断超出生命周期
                s_sql = "SELECT * FROM assistant_details WHERE AssistantCode = '" + dt_temp.Rows[0]["AssistantCode"].ToString() + "';";
                DataTable dt_assistant_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                DateTime timeA = Convert.ToDateTime(dt_temp.Rows[0]["BrewingData"].ToString());
                DateTime timeB = DateTime.Now; //获取当前时间
                TimeSpan ts = timeB - timeA; //计算时间差
                string s_time = ts.TotalHours.ToString(); //将时间差转换为小时
                string s_time2 = ts.TotalMinutes.ToString();

                if (d_blCompCoefficient != 0 || d_blCompConstant != 0)
                {

                    //需要补偿
                    foreach (DataRow row in dt.Rows)
                    {
                        double d_blW = Convert.ToDouble(row["ObjectDropWeight"]);

                        int i_cupNum = Convert.ToInt32(row["CupNum"]);

                        double d_blCW = Convert.ToDouble(string.Format("{0:F3}", d_blW * ( (Convert.ToDouble(s_time2) * d_blCompCoefficient + d_blCompConstant) / 100)));

                        FADM_Object.Communal._fadmSqlserver.ReviseData("UPDATE dye_details SET Compensation  = " + d_blCW + " WHERE Cooperate = 1 AND BottleNum = " + i_bottleNo + " AND CupNum = " + i_cupNum + "");

                    }


                    dt = FADM_Object.Communal._fadmSqlserver.GetData(
                      "SELECT top 1 * FROM dye_details WHERE Cooperate = 1 AND BottleNum = " + i_bottleNo + " ORDER BY ReceptionTime ;"); 


                }


                if (d_blCompCoefficient == 0 && d_blCompConstant == 0)
                {

                    if (Convert.ToDouble(s_time) > Convert.ToDouble(dt_assistant_details.Rows[0]["TermOfValidity"].ToString()) && FADM_Object.Communal._b_isOutDrip)
                    {
                        //查询在备料表是否存在记录，如果存在，先让客户选择是否使用备料数据来更新
                        string s_sqlpre = "SELECT * FROM pre_brew WHERE  BottleNum = " + i_bottleNo + ";";
                        DataTable dt_pre_brew = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlpre);
                        if (dt_pre_brew.Rows.Count > 0)
                        {
                            if (!_dic_keyValue.ContainsKey(i_bottleNo))
                            {
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                     "UPDATE dye_details SET  Cooperate = 6 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶过期,备料表存在已开料记录，是否替换(替换请点是，继续使用旧母液请点否)", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                else
                                    new FADM_Object.MyAlarm( "The "+i_bottleNo +" mother liquor bottle has expired, and there is a record of opened materials in the material preparation table. Should it be replaced? (Please click Yes for replacement, and click No for continuing to use the old mother liquor)", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                return;
                            }
                        }
                        else
                        {
                            if (_dic_keyValue.ContainsKey(i_bottleNo))
                            {
                                if (_dic_keyValue[i_bottleNo] == false)
                                {
                                    //存在并且点了一次是
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                         "UPDATE dye_details SET  Cooperate = 6 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶过期,忽略液量低提示请点否，等待泡制完成后再滴请点是", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                    else
                                        new FADM_Object.MyAlarm("The  " + i_bottleNo + " mother liquor bottle has expired, ignore the low liquid volume prompt, please click no, wait for the brewing to be completed before dripping, please click yes", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                    return;
                                }

                            }
                            else
                            {
                                _dic_keyValue.Add(i_bottleNo, false);
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                         "UPDATE dye_details SET  Cooperate = 6 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶过期,忽略液量低提示请点否，等待泡制完成后再滴请点是", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                else
                                    new FADM_Object.MyAlarm("The  " + i_bottleNo + " mother liquor bottle has expired, ignore the low liquid volume prompt, please click no, wait for the brewing to be completed before dripping, please click yes", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                return;
                            }
                        }
                    }
                }
                else
                {

                    if (Convert.ToDouble(s_time) > Convert.ToDouble(dt_assistant_details.Rows[0]["TermOfValidity"].ToString()))
                    {
                        string s_sqlpre = "SELECT * FROM pre_brew WHERE  BottleNum = " + i_bottleNo + ";";
                        DataTable dt_pre_brew = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlpre);
                        if (dt_pre_brew.Rows.Count > 0)
                        {
                            if (!_dic_keyValue.ContainsKey(i_bottleNo))
                            {

                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                         "UPDATE dye_details SET  Cooperate = 6 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶过期,备料表存在已开料记录，是否替换(替换请点是，继续使用旧母液请点否)", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                else
                                    new FADM_Object.MyAlarm("The " + i_bottleNo + " mother liquor bottle has expired, and there is a record of opened materials in the material preparation table. Should it be replaced? (Please click Yes for replacement, and click No for continuing to use the old mother liquor)", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                return;
                            }
                        }
                        else
                        {
                            if (_dic_keyValue.ContainsKey(i_bottleNo))
                            {
                                if (_dic_keyValue[i_bottleNo] == false)
                                {
                                    //存在并且点了一次是
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                         "UPDATE dye_details SET  Cooperate = 6 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶过期,忽略液量低提示请点否，等待泡制完成后再滴请点是", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                    else
                                        new FADM_Object.MyAlarm("The  " + i_bottleNo + " mother liquor bottle has expired, ignore the low liquid volume prompt, please click no, wait for the brewing to be completed before dripping, please click yes", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                    return;
                                }

                            }
                            else
                            {
                                Dye._dic_keyValue.Add(i_bottleNo, false);
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                         "UPDATE dye_details SET  Cooperate = 6 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶过期,忽略液量低提示请点否，等待泡制完成后再滴请点是", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                else
                                    new FADM_Object.MyAlarm("The  " + i_bottleNo + " mother liquor bottle has expired, ignore the low liquid volume prompt, please click no, wait for the brewing to be completed before dripping, please click yes", i_bottleNo, 6, MessageBoxButtons.YesNo);
                                return;
                            }
                        }
                    }
                }





                //判断当前母液瓶是否针检
                if ((0 >= i_adjust || false == b_checkSuccess) && !b_checkFail)
                {
                    //label1:
                    //int i_ret = new BottleCheck().DripCheck(i_bottleNo, false, 0);
                    int i_ret = new BottleCheck().MyDripCheck(i_bottleNo, false, 0); //针检
                    if (-1 == i_ret)
                    {
                        //FADM_Object.MyAlarm myAlarm = new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶针检失败，是否继续?(继续针检请点是，继续滴液请点否)", "染色针检", true, 1);
                        //while (true)

                        //{
                        //    if (0 != myAlarm._i_alarm_Choose)
                        //        break;
                        //    Thread.Sleep(1);
                        //}

                        //if (1 == myAlarm._i_alarm_Choose)
                        //    goto label1;
                        //else
                        //{
                        b_checkFail = true;
                        //}
                    }
                    else if (-3 == i_ret) 
                    {
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                     "UPDATE dye_details SET  Cooperate = 8 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶未发现针筒，是否继续执行 ? (继续寻找请点是)", i_bottleNo, 8, MessageBoxButtons.YesNo);
                        else
                            new FADM_Object.MyAlarm("No syringe was found in the"+i_bottleNo + "  mother liquor bottle. Do you want to continue? (To continue searching, please click Yes)", i_bottleNo, 8, MessageBoxButtons.YesNo);
                        return;
                    }

                    goto label9;
                }



                //计算脉冲
                Dictionary<int, int> dic_pulse = new Dictionary<int, int>();
                Dictionary<int, int> dic_step = new Dictionary<int, int>();
                List<double> lis_water = new List<double>();
                List<double> lis_weight = new List<double>();
                Dictionary<int, double> dic_water = new Dictionary<int, double>();
                int i_pulseT = 0;
                foreach (DataRow row in dt.Rows)
                {
                    double d_compensation = 0.0;
                    if (row["Compensation"] is DBNull)
                    {
                        d_compensation = 0.0;
                    }
                    else
                    {
                        d_compensation = Convert.ToDouble(row["Compensation"]);
                    }
                    double d_blW = Convert.ToDouble(row["ObjectDropWeight"] )+ d_compensation;
                    lis_weight.Add(d_blW);
                    int i_needPulse = row["NeedPulse"] is DBNull ? 0 : Convert.ToInt32(row["NeedPulse"]);
                    //判断是否分开两次滴液，如果是就使用需加脉冲来计算
                    int i_pulse = i_needPulse > 0 ? i_needPulse : Convert.ToInt32(d_blW * Convert.ToDouble(i_adjust));
                    dic_pulse.Add(Convert.ToInt16(row["CupNum"]), i_pulse);
                    dic_step.Add(Convert.ToInt16(row["CupNum"]), Convert.ToInt32(row["StepNum"]));
                    dic_water.Add(Convert.ToInt16(row["CupNum"]), Convert.ToDouble(row["ObjectWaterWeight"]));
                    lis_water.Add(Convert.ToDouble(row["ObjectWaterWeight"]));
                    i_pulseT += i_pulse;

                    s_unitOfAccount = row["UnitOfAccount"].ToString();
                }

                sAddArg o = new sAddArg();
                o._i_minBottleNo = i_bottleNo;
                o._obj_batchName = "";
                o._i_adjust = i_adjust;
                o._i_pulseT = i_pulseT;
                o._s_syringeType = s_syringeType;
                o._s_unitOfAccount = s_unitOfAccount;
                o._dic_pulse = dic_pulse;
                o._dic_water = dic_water;
                Dictionary<int, double> dic_return = new Dictionary<int, double>();
                int nret = FADM_Object.Communal.AddMac(o, ref dic_return);
                //夹不到针筒
                if (nret == -1)
                {
                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                             "UPDATE dye_details SET  Cooperate = 7 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶未发现针筒，请确认无误后点确定", i_bottleNo, 7, MessageBoxButtons.OK);
                    else
                        new FADM_Object.MyAlarm("No syringe was found in the" + i_bottleNo + "  mother liquor bottle. Please confirm if there are no errors and click OK", i_bottleNo, 7, MessageBoxButtons.OK);
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "未发现针筒抽液退出");
                    return;

                    //while (true)
                    //{
                    //    if (0 != myAlarm._i_alarm_Choose)
                    //        break;
                    //    Thread.Sleep(1);
                    //}

                    //if (1 == myAlarm._i_alarm_Choose)
                    //    goto label3;
                    //else
                    //    throw new Exception("收到退出消息");
                }
                //滴液完成
                else if (nret == 0)
                {

                    foreach (KeyValuePair<int, double> kvp in dic_return)
                    {
                        double d_blRErr = 0;
                        if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
                            d_blRErr = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", kvp.Value - Lib_Card.Configure.Parameter.Correcting_S_Weight)) : Convert.ToDouble(string.Format("{0:F3}", kvp.Value - Lib_Card.Configure.Parameter.Correcting_S_Weight));
                        else
                            d_blRErr = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", kvp.Value - Lib_Card.Configure.Parameter.Correcting_B_Weight)) : Convert.ToDouble(string.Format("{0:F3}", kvp.Value - Lib_Card.Configure.Parameter.Correcting_B_Weight));
                        ;

                        //查询开料日期
                        DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(
                                    "SELECT * FROM bottle_details WHERE  BottleNum = " + i_bottleNo + ";");

                        if (0.00 != kvp.Value)
                        {
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                              "UPDATE dye_details SET RealDropWeight = ObjectDropWeight + IsNULL(Compensation,0.0) + " + d_blRErr + ", Cooperate = 0 , Finish = 1 " + " ,BrewingData = '" + dt_bottle_details.Rows[0]["BrewingData"].ToString() + "' " +
                              "WHERE StepNum = " + dic_step[kvp.Key] + " AND BottleNum = " + i_bottleNo + " AND " +
                              "CupNum = " + kvp.Key + ";");

                            //更新后先把加药量取出
                            string s_sql2 = "SELECT RealDropWeight FROM dye_details WHERE StepNum = " + dic_step[kvp.Key] + " AND BottleNum = " + i_bottleNo + " AND " +
                              "CupNum = " + kvp.Key + ";";
                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);

                            if (dt_dye_details.Rows.Count > 0)
                            {
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE cup_details SET TotalWeight = TotalWeight + " + dt_dye_details.Rows[0][0].ToString() + " WHERE CupNum = " + kvp.Key + ";");

                                //母液瓶扣减
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE bottle_details SET CurrentWeight = CurrentWeight - " + dt_dye_details.Rows[0][0].ToString() + " " +
                                    "WHERE BottleNum = '" + i_bottleNo + "';");
                            }
                        }
                        else
                        {
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                              "UPDATE dye_details SET RealDropWeight = 0.00, Cooperate = 0 , Finish = 1 " + " ,BrewingData = '" + dt_bottle_details.Rows[0]["BrewingData"].ToString() + "' " +
                              "WHERE StepNum = " + dic_step[kvp.Key] + " AND BottleNum = " + i_bottleNo + " AND " +
                              "CupNum = " + kvp.Key + ";");
                        }

                        //复位加药启动信号
                        int[] ia_zero = new int[1];
                        //
                        ia_zero[0] = 0;
                        DyeHMIWrite(kvp.Key, 509, 309, ia_zero);

                        //发送加药完成
                        ia_zero = new int[1];
                        //
                        ia_zero[0] = 2;

                        DyeHMIWrite(kvp.Key, 115, 114, ia_zero);

                    }

                    FADM_Control.Formula._b_updateWait = true;

                }
                else if (nret == -2)
                {
                    //把已经滴过的先置为完成
                    foreach (KeyValuePair<int, double> kvp in dic_return)
                    {
                        double d_blRErr = 0;
                        if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
                            d_blRErr = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", kvp.Value - Lib_Card.Configure.Parameter.Correcting_S_Weight)) : Convert.ToDouble(string.Format("{0:F3}", kvp.Value - Lib_Card.Configure.Parameter.Correcting_S_Weight));
                        else
                            d_blRErr = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", kvp.Value - Lib_Card.Configure.Parameter.Correcting_B_Weight)) : Convert.ToDouble(string.Format("{0:F3}", kvp.Value - Lib_Card.Configure.Parameter.Correcting_B_Weight));
                        ;

                        //查询开料日期
                        DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(
                                    "SELECT * FROM bottle_details WHERE  BottleNum = " + i_bottleNo + ";");

                        if (0.00 != kvp.Value)
                        {
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                              "UPDATE dye_details SET RealDropWeight = ObjectDropWeight + IsNULL(Compensation,0.0) + " + d_blRErr + ", Cooperate = 0 , Finish = 1 " + " ,BrewingData = '" + dt_bottle_details.Rows[0]["BrewingData"].ToString() + "' " +
                              "WHERE StepNum = " + dic_step[kvp.Key] + " AND BottleNum = " + i_bottleNo + " AND " +
                              "CupNum = " + kvp.Key + ";");

                            //更新后先把加药量取出
                            string s_sql2 = "SELECT RealDropWeight FROM dye_details WHERE StepNum = " + dic_step[kvp.Key] + " AND BottleNum = " + i_bottleNo + " AND " +
                              "CupNum = " + kvp.Key + ";";
                            DataTable dt_dye_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);

                            if (dt_dye_details.Rows.Count > 0)
                            {
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE cup_details SET TotalWeight = TotalWeight + " + dt_dye_details.Rows[0][0].ToString() + " WHERE CupNum = " + kvp.Key + ";");

                                //母液瓶扣减
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                    "UPDATE bottle_details SET CurrentWeight = CurrentWeight - " + dt_dye_details.Rows[0][0].ToString() + " " +
                                    "WHERE BottleNum = '" + i_bottleNo + "';");
                            }
                        }
                        else
                        {
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                              "UPDATE dye_details SET RealDropWeight = 0.00, Cooperate = 0 , Finish = 1 " + " ,BrewingData = '" + dt_bottle_details.Rows[0]["BrewingData"].ToString() + "' " +
                              "WHERE StepNum = " + dic_step[kvp.Key] + " AND BottleNum = " + i_bottleNo + " AND " +
                              "CupNum = " + kvp.Key + ";");
                        }

                        //复位加药启动信号
                        int[] ia_zero = new int[1];
                        //
                        ia_zero[0] = 0;
                        DyeHMIWrite(kvp.Key, 509, 309, ia_zero);

                        //发送加药完成
                        ia_zero = new int[1];
                        //
                        ia_zero[0] = 2;

                        DyeHMIWrite(kvp.Key, 115, 114, ia_zero);

                    }

                    FADM_Control.Formula._b_updateWait = true;

                    //更新需要加药第一杯脉冲
                    s_sql = "UPDATE dye_details SET NeedPulse = " + Communal._i_needPulse + " WHERE Cooperate = 1  And " +
                                    " Finish = 0  And CupNum = " + Communal._i_needPulseCupNumber +"AND BottleNum = " + i_bottleNo + "; ";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                         "UPDATE dye_details SET  Cooperate = 6 WHERE  Cooperate = 1 AND BottleNum = " + i_bottleNo + " ;");

                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        new FADM_Object.MyAlarm(i_bottleNo + "号母液瓶预滴液数值太小,请检查实际是否液量过低?(继续执行请点是)", i_bottleNo, 6, MessageBoxButtons.YesNo);
                    else
                        new FADM_Object.MyAlarm(" The number of pre-drops in mother liquor bottle " + i_bottleNo + "  is too small, please check whether the actual amount of liquid is too low" +
                        "( Continue to perform please click Yes)", i_bottleNo, 6, MessageBoxButtons.YesNo);
                    return;

                }




                //Lib_SerialPort.Balance.METTLER.bReSetSign = true;

            }
            catch (Exception ex)
            {

                if ("收到退出消息" == ex.Message)
                {
                    FADM_Object.Communal._b_stop = false;
                    //new Reset().MachineReset(0);
                    MyModbusFun.MyMachineReset(); //复位
                }

                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);

                    if (ex.Message.Equals("-2"))
                    {
                        throw;

                    }
                    else
                    {
                        string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "DyeAddMedicine" + "','" +
                                  ex.ToString() + "(Test)');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        new FADM_Object.MyAlarm(ex.ToString(), "DyeAddMedicine", false, 0);
                    }
                }
            }
        }

        /// <summary>
        /// 加水
        /// </summary>
        /// <param name="i_wType">加水类型 0：流程加水 1：洗杯加水</param>
        private void DyeAddWater(int i_cupNO, double d_blWater, int i_wType)
        {
            try
            {

                if (d_blWater == 0)
                {
                    return;
                }

                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNO + "号配液杯加水启动");

                int i_mRes;
                int i_cupNo = i_cupNO;
                if (SmartDyeing.FADM_Object.Communal._dic_dyeType[i_cupNO] == 1)
                {
                    //如果关盖状态，就先执行开盖动作
                    if (_cup_Temps[i_cupNO - 1]._i_cupCover == 1)
                    {
                    label1:
                        //开盖
                        try
                        {

                            FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯开盖");
                            int i_xStart = 0, i_yStart = 0;
                            int i_xEnd = 0, i_yEnd = 0;
                            MyModbusFun.CalTarget(1, i_cupNo, ref i_xStart, ref i_yStart);

                            MyModbusFun.CalTarget(4, i_cupNo, ref i_xEnd, ref i_yEnd);

                            i_mRes = MyModbusFun.OpenOrPutCover(i_xStart, i_yStart, i_xEnd, i_yEnd, 0);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");

                        }
                        catch (Exception ex)
                        {
                            
                            if ("未发现杯盖" == ex.Message)
                            {
                                ////气缸上
                                //int[] ia_array = new int[1];
                                //ia_array[0] = 5;

                                //int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                                //Thread.Sleep(1000);
                                ////抓手开
                                //ia_array = new int[1];
                                //ia_array[0] = 7;

                                //i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);

                                FADM_Object.MyAlarm myAlarm;
                                //流程加水
                                if (i_wType == 0)
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE dye_details SET  Cooperate = 9 WHERE  Cooperate = 1 AND  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现杯盖，是否继续执行?(继续执行请点是，已完成开盖请点否)", "SwitchCover", i_cupNo, 2, 5);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a cap, do you want to continue? " +
                                            "( Continue to perform please click Yes, have completed the opening please click No)", "SwitchCover", i_cupNo, 2, 5);
                                }
                                //洗杯加水
                                else
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯未发现杯盖，是否继续执行?(继续执行请点是，已完成开盖请点否)", "SwitchCover", i_cupNo, 2, 6);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup did not find a cap, do you want to continue? " +
                                            "( Continue to perform please click Yes, have completed the opening please click No)", "SwitchCover", i_cupNo, 2, 6);
                                }
                                //while (true)

                                //{
                                //    if (0 != myAlarm._i_alarm_Choose)
                                //        break;
                                //    Thread.Sleep(1);
                                //}
                                //if(myAlarm._i_alarm_Choose == 1)
                                //{
                                //    goto label1;
                                //}
                                //else
                                //{
                                //    goto label2;
                                //}
                                return;
                            }
                            else if ("发现杯盖或针筒" == ex.Message)
                            {
                                FADM_Object.MyAlarm myAlarm;
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    myAlarm = new FADM_Object.MyAlarm("请先拿住针筒或杯盖，然后点确定", "SwitchCover", true, 1);
                                else
                                    myAlarm = new FADM_Object.MyAlarm("Please hold the syringe or cup lid first, and then confirm", "SwitchCover", true, 1);
                                while (true)

                                {
                                    if (0 != myAlarm._i_alarm_Choose)
                                        break;
                                    Thread.Sleep(1);
                                }
                                //抓手开
                                int[] ia_array = new int[1];
                                ia_array[0] = 7;

                                int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                                //等5秒后继续
                                Thread.Sleep(5000);
                                goto label1;
                            }
                            else if ("配液杯取盖失败" == ex.Message)
                            {

                                FADM_Object.MyAlarm myAlarm;
                                if (i_wType == 0)
                                {
                                    
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE dye_details SET  Cooperate = 9 WHERE  Cooperate = 1 AND  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 5);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove cap from dispensing cup, do you want to continue? " +
                                            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 5);
                                }
                                //洗杯加水
                                else
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                              "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 6);
                                    else
                                        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove cap from dispensing cup, do you want to continue? " +
                                            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 6);
                                }
                                return;
                            }
                            else if ("放盖失败" == ex.Message)
                            {

                                FADM_Object.MyAlarm myAlarm;
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖到杯盖区失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 22);
                                else
                                    myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to place lid into lid area, do you want to continue? " +
                                        "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 22);

                                //认为开盖完成
                                goto label2;
                            }
                            //else if ("放盖区取盖失败" == ex.Message)
                            //{

                            //    FADM_Object.MyAlarm myAlarm;
                            //    if (i_wType == 0)
                            //    {

                            //        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            //               "UPDATE dye_details SET  Cooperate = 9 WHERE  Cooperate = 1 AND  CupNum = " + i_cupNo + " ;");
                            //        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            //            myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖区取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 5);
                            //        else
                            //            myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove the cover from the cover placement area, do you want to continue? " +
                            //                "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 5);
                            //    }
                            //    //洗杯加水
                            //    else
                            //    {
                            //        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            //                  "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            //        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            //            myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号放盖区取盖失败，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 2, 6);
                            //        else
                            //            myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Failed to remove the cover from the cover placement area, do you want to continue? " +
                            //                "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 2, 6);
                            //    }
                            //    return;
                            //}
                            //else if ("抓手A夹紧异常" == ex.Message || "抓手B夹紧异常" == ex.Message)
                            //{

                            //    int[] ia_array = new int[1];

                            //    //抓手开
                            //    ia_array = new int[1];
                            //    ia_array[0] = 7;

                            //    int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);

                            //    Thread.Sleep(2000);

                            //    //气缸上
                            //    ia_array[0] = 5;

                            //    i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);
                            //    Thread.Sleep(1000);
                            //    FADM_Object.Communal._fadmSqlserver.ReviseData(
                            //       "UPDATE cup_details SET  Cooperate = 6 WHERE  CupNum = " + i_cupNo + " ;");
                            //    FADM_Object.MyAlarm myAlarm;
                            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + "号配液杯关闭抓手异常，是否继续执行?(继续执行请点是)", "SwitchCover", i_cupNo, 1, 0);
                            //    else
                            //        myAlarm = new FADM_Object.MyAlarm(i_cupNo + " Cup Close grip exception, do you want to continue? " +
                            //            "( Continue to perform please click Yes)", "SwitchCover", i_cupNo, 1, 0);
                            //    //while (true)

                            //    //{
                            //    //    if (0 != myAlarm._i_alarm_Choose)
                            //    //        break;
                            //    //    Thread.Sleep(1);
                            //    //}
                            //    //goto label1;
                            //    return;
                            //}
                            else
                                throw;
                        }
                        //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯拿盖完成");

                        //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "寻找" + i_cupNo + "号配液杯放盖位");

                        //i_mRes = MyModbusFun.TargetMove(4, i_cupNo, 1);
                        //if (-2 == i_mRes)
                        //    throw new Exception("收到退出消息");

                        //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "到达" + i_cupNo + "号配液杯放盖位");

                        //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯放盖");


                        //i_mRes = MyModbusFun.PutCover();
                        //if (-2 == i_mRes)
                        //    throw new Exception("收到退出消息");

                        //FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯放盖完成");
                    label2:
                        //复位加药启动信号
                        int[] ia_zero1 = new int[1];
                        //
                        ia_zero1[0] = 0;
                        

                        FADM_Auto.Dye.DyeOpenOrCloseCover(i_cupNo, 2);

                        Thread.Sleep(1000);
                        Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 2 where CupNum = " + i_cupNo);

                        _cup_Temps[i_cupNo - 1]._i_cupCover = 2;

                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNo + "号配液杯开盖完成");

                    }
                }

                //寻找配液杯
                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "寻找" + i_cupNO + "号配液杯");
                FADM_Object.Communal._i_OptCupNum = i_cupNO;
                
                int i_reSuccess2 = MyModbusFun.TargetMove(1, i_cupNO, 1);
                if (-2 == i_reSuccess2)
                    throw new Exception("收到退出消息");

                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "抵达" + i_cupNO + "号配液杯");
                

                

                //加水
                //new Water().Add(d_blWater, "Dail");
                double d_addWaterTime = MyModbusFun.GetWaterTime(d_blWater);//加水时间
                i_mRes = MyModbusFun.AddWater(d_addWaterTime);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                FADM_Object.Communal._fadmSqlserver.ReviseData(
                      "UPDATE cup_details SET TotalWeight = TotalWeight + " + d_blWater + " WHERE CupNum = " + i_cupNO + ";");
                d_blWater = 0;

                

                //复位加药启动信号
                int[] ia_zero = new int[1];
                //
                ia_zero[0] = 0;
                

                DyeHMIWrite(i_cupNo, 509, 309, ia_zero);






                ia_zero = new int[1];
                //
                ia_zero[0] = 2;
                

                DyeHMIWrite(i_cupNo, 115, 114, ia_zero);

                FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", i_cupNO + "号配液杯加水完成");
            }
            catch (Exception ex)
            {

                if ("收到退出消息" == ex.Message)
                {

                    FADM_Object.Communal._b_stop = false;
                    //new Reset().MachineReset(0);
                    MyModbusFun.MyMachineReset(); //复位
                }

                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);

                    if (ex.Message.Equals("-2"))
                    {
                        throw;

                    }
                    else
                    {
                        string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "DyeAddWater" + "','" +
                                  ex.ToString() + "(Test)');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        new FADM_Object.MyAlarm(ex.ToString(), "DyeAddWater", false, 0);
                    }

                }
            }
        }

        private void Finish(object obj_cupNo)
        {


            DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData("SELECT * FROM drop_head WHERE CupNum = " + obj_cupNo + ";");
            if (dt_drop_head.Rows.Count > 0)
            {
                if (dt_drop_head.Rows[0]["BatchName"] != System.DBNull.Value)
                {
                    FADM_Object.Communal._b_finshRun = false;
                    //染色正常结束
                    //复位当前杯使用状态
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", obj_cupNo + "号配液杯染固色完成");
                    else
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "Dyeing and fixation of solution cup "+ obj_cupNo + " completed");

                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                      " UPDATE dye_details SET FinishTime = '" + DateTime.Now + "', Finish = 1  WHERE CupNum = " +
                      obj_cupNo + "  and StepNum = (select MAX(StepNum) from dye_details where CupNum = " + obj_cupNo + ");");

                    //拷贝到历史表
                    DataTable dt_Temp = FADM_Object.Communal._fadmSqlserver.GetData(
                         "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'dye_details';");
                    string s_columnDetails = null;
                    foreach (DataRow row in dt_Temp.Rows)
                    {
                        if (Convert.ToString(row[0]) != "Cooperate"&& Convert.ToString(row[0]) != "NeedPulse")
                            s_columnDetails += Convert.ToString(row[0]) + ", ";
                    }
                    s_columnDetails = s_columnDetails.Remove(s_columnDetails.Length - 2);

                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "INSERT INTO history_dye (" + s_columnDetails + ") (SELECT " + s_columnDetails + " FROM dye_details " +
                        "WHERE CupNum =" + obj_cupNo + " AND BatchName != '0') ;");
                    DataTable dt_drop_head2 = FADM_Object.Communal._fadmSqlserver.GetData(
                          "SELECT * FROM drop_head WHERE CupNum =" + obj_cupNo + ";");
                    if (dt_drop_head2.Rows.Count > 0)
                    {

                        string s_temp = Txt.ReadTXT(Convert.ToInt32(obj_cupNo));
                        if (!string.IsNullOrEmpty(s_temp))
                            FADM_Object.Communal._fadmSqlserver.SetImage(s_temp, Convert.ToInt32(obj_cupNo), Convert.ToString(dt_drop_head2.Rows[0]["BatchName"]));

                    }


                    string s_txt = Txt.ReadMarkTXT(Convert.ToInt32(obj_cupNo));

                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "UPDATE history_head SET MarkStep = '" + s_txt + "' WHERE CupNum = " + obj_cupNo + " AND BatchName = '" + dt_drop_head2.Rows[0]["BatchName"].ToString() + "';");
                }

                //清空表
                FADM_Object.Communal._fadmSqlserver.ReviseData(
                     "DELETE FROM drop_head WHERE CupNum =" + obj_cupNo + " AND BatchName != '0' ;");
                FADM_Object.Communal._fadmSqlserver.ReviseData(
                    "DELETE FROM drop_details WHERE CupNum =" + obj_cupNo + " AND BatchName != '0';");
                FADM_Object.Communal._fadmSqlserver.ReviseData(
                    "DELETE FROM dye_details WHERE CupNum =" + obj_cupNo + " AND BatchName != '0';");


                Txt.DeleteTXT(Convert.ToInt32(obj_cupNo));
                Txt.DeleteMarkTXT(Convert.ToInt32(obj_cupNo));



                FADM_Control.Formula.P_bl_update = true;
                //FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo(i_cupNo + "号配液杯染固色完成");

                //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo(i_cupNo + "号配液杯染固色完成");
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    string s_insert = Lib_Card.CardObject.InsertD(obj_cupNo + "号配液杯染固色完成", "Dye");
                    Lib_Card.CardObject.DeleteD(s_insert);
                }
                else
                {
                    string s_insert = Lib_Card.CardObject.InsertD(obj_cupNo + " The dyeing and fixing of the solution cup have been completed", "Dye");
                    Lib_Card.CardObject.DeleteD(s_insert);
                }
                FADM_Object.Communal._b_finshRun = true;

            }
            //复位当前杯使用状态
            FADM_Object.Communal._fadmSqlserver.ReviseData(
                "UPDATE cup_details SET FormulaCode = null, " +
                "DyeingCode = null, IsUsing = 0, Statues = '待机', " +
                "StartTime = null, SetTemp = null, StepNum = null, TotalWeight = null, " +
                "TotalStep = null, TechnologyName = null, StepStartTime = null, SetTime = null,RecordIndex = 0, Cooperate = 0 WHERE CupNum = " + obj_cupNo + " AND Statues != '下线';");
            _cup_Temps[Convert.ToInt32(obj_cupNo) - 1]._b_finish = false;

        }

        /// <summary>
        /// 打版机读写交互
        /// i_cupNO：杯号
        /// iNewStart：摇摆机操作开始地址
        /// iOldStart：转子机操作开始地址
        ///  ia_values：操作值
        /// </summary>


        public static void DyeHMIWrite(int iCupNO, int iNewStart,int iOldStart, int[] values)
        {

            if (iCupNO >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()) && iCupNO <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI1._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI1.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area1_CupMin;
                if (SmartDyeing.FADM_Object.Communal._dic_dyeType[iCupNO] == 1)
                    FADM_Object.Communal._tcpDyeHMI1.Write(iNewStart + 64 * ((iCupNO - i_cupnum + 1 - 1) % 6), values);
                else
                    FADM_Object.Communal._tcpDyeHMI1.Write(iOldStart + 16 * ((iCupNO - i_cupnum + 1 - 1) % 10), values);
            }
            else if (iCupNO >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()) && iCupNO <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI2._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI2.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area2_CupMin;
                if (SmartDyeing.FADM_Object.Communal._dic_dyeType[iCupNO] == 1)
                    FADM_Object.Communal._tcpDyeHMI2.Write(iNewStart + 64 * ((iCupNO - i_cupnum + 1 - 1) % 6), values);
                else
                    FADM_Object.Communal._tcpDyeHMI2.Write(iOldStart + 16 * ((iCupNO - i_cupnum + 1 - 1) % 10), values);
            }
            else if (iCupNO >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()) && iCupNO <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI3._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI3.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area3_CupMin;
                if (SmartDyeing.FADM_Object.Communal._dic_dyeType[iCupNO] == 1)
                    FADM_Object.Communal._tcpDyeHMI3.Write(iNewStart + 64 * ((iCupNO - i_cupnum + 1 - 1) % 6), values);
                else
                    FADM_Object.Communal._tcpDyeHMI3.Write(iOldStart + 16 * ((iCupNO - i_cupnum + 1 - 1) % 10), values);
            }
            else if (iCupNO >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()) && iCupNO <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI4._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI4.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area4_CupMin;
                if (SmartDyeing.FADM_Object.Communal._dic_dyeType[iCupNO] == 1)
                    FADM_Object.Communal._tcpDyeHMI4.Write(iNewStart + 64 * ((iCupNO - i_cupnum + 1 - 1) % 6), values);
                else
                    FADM_Object.Communal._tcpDyeHMI4.Write(iOldStart + 16 * ((iCupNO - i_cupnum + 1 - 1) % 10), values);
            }
            else if (iCupNO >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()) && iCupNO <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI5._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI5.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area5_CupMin;
                if (SmartDyeing.FADM_Object.Communal._dic_dyeType[iCupNO] == 1)
                    FADM_Object.Communal._tcpDyeHMI5.Write(iNewStart + 64 * ((iCupNO - i_cupnum + 1 - 1) % 6), values);
                else
                    FADM_Object.Communal._tcpDyeHMI5.Write(iOldStart + 16 * ((iCupNO - i_cupnum + 1 - 1) % 10), values);
            }
            else if (iCupNO >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()) && iCupNO <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI6._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI6.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area6_CupMin;
                if (SmartDyeing.FADM_Object.Communal._dic_dyeType[iCupNO] == 1)
                    FADM_Object.Communal._tcpDyeHMI6.Write(iNewStart + 64 * ((iCupNO - i_cupnum + 1 - 1) % 6), values);
                else
                    FADM_Object.Communal._tcpDyeHMI6.Write(iOldStart + 16 * ((iCupNO - i_cupnum + 1 - 1) % 10), values);
            }
        }

        /// <summary>
        /// 打版机读写交互
        /// i_erea：打版机区域号
        /// i_index：对应工位
        /// i_start：操作开始地址
        /// i_num:对应一杯寄存器数量，转子16，摇摆机64
        ///  ia_values：操作值
        /// </summary>


        public static void DyeHMIWriteSigle(int i_erea, int i_index, int i_start,int i_num, int[] ia_values)
        {

            if (i_erea == 0)
            {
                if (!FADM_Object.Communal._tcpDyeHMI1._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI1.ReConnect();
                }
                FADM_Object.Communal._tcpDyeHMI1.Write(i_start + i_num * i_index, ia_values);
            }
            else if (i_erea == 1)
            {
                if (!FADM_Object.Communal._tcpDyeHMI2._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI2.ReConnect();
                }
                FADM_Object.Communal._tcpDyeHMI2.Write(i_start + i_num * i_index, ia_values);
            }
            else if (i_erea == 2)
            {
                if (!FADM_Object.Communal._tcpDyeHMI3._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI3.ReConnect();
                }
                FADM_Object.Communal._tcpDyeHMI3.Write(i_start + i_num * i_index, ia_values);
            }
            else if (i_erea == 3)
            {
                if (!FADM_Object.Communal._tcpDyeHMI4._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI4.ReConnect();
                }
                FADM_Object.Communal._tcpDyeHMI4.Write(i_start + i_num * i_index, ia_values);
            }
            else if (i_erea == 4)
            {
                if (!FADM_Object.Communal._tcpDyeHMI5._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI5.ReConnect();
                }
                FADM_Object.Communal._tcpDyeHMI5.Write(i_start + i_num * i_index, ia_values);
            }
            else if (i_erea == 5)
            {
                if (!FADM_Object.Communal._tcpDyeHMI6._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI6.ReConnect();
                }
                FADM_Object.Communal._tcpDyeHMI6.Write(i_start + i_num * i_index, ia_values);
            }
        }

        /// <summary>
        /// 打版机关关盖
        /// i_cupNo：杯号
        /// i_states:杯盖状态 1 关盖 2开盖
        /// </summary>


        public static void DyeOpenOrCloseCover(int i_cupNo,int i_states)
        {
            int[] ia_zero = new int[1]; 
            if (i_cupNo >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()) && i_cupNo <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI1._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI1.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area1_CupMin;
                ia_zero[0] = 0;
                //清空请求杯盖动作请求
                FADM_Object.Communal._tcpDyeHMI1.Write(508 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);
                ia_zero[0] = i_states;
                //修改盖子状态
                FADM_Object.Communal._tcpDyeHMI1.Write(117 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);

            }
            else if (i_cupNo >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()) && i_cupNo <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI2._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI2.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area2_CupMin;
                ia_zero[0] = 0;
                //清空请求杯盖动作请求
                FADM_Object.Communal._tcpDyeHMI2.Write(508 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);
                ia_zero[0] = i_states;
                //修改盖子状态
                FADM_Object.Communal._tcpDyeHMI2.Write(117 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);

            }
            else if (i_cupNo >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()) && i_cupNo <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMax.ToString()))
            {
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area3_CupMin;
                if (!FADM_Object.Communal._tcpDyeHMI3._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI3.ReConnect();
                }
                ia_zero[0] = 0;
                //清空请求杯盖动作请求
                FADM_Object.Communal._tcpDyeHMI3.Write(508 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);
                ia_zero[0] = i_states;
                //修改盖子状态
                FADM_Object.Communal._tcpDyeHMI3.Write(117 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);

            }
            else if (i_cupNo >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()) && i_cupNo <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI4._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI4.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area4_CupMin;
                ia_zero[0] = 0;
                //清空请求杯盖动作请求
                FADM_Object.Communal._tcpDyeHMI4.Write(508 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);
                ia_zero[0] = i_states;
                //修改盖子状态
                FADM_Object.Communal._tcpDyeHMI4.Write(117 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);

            }
            else if (i_cupNo >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()) && i_cupNo <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI5._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI5.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area5_CupMin;
                ia_zero[0] = 0;
                //清空请求杯盖动作请求
                FADM_Object.Communal._tcpDyeHMI5.Write(508 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);
                ia_zero[0] = i_states;
                //修改盖子状态
                FADM_Object.Communal._tcpDyeHMI5.Write(117 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);

            }
            else if (i_cupNo >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()) && i_cupNo <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMax.ToString()))
            {
                if (!FADM_Object.Communal._tcpDyeHMI6._b_Connect)
                {
                    FADM_Object.Communal._tcpDyeHMI6.ReConnect();
                }
                int i_cupnum = Lib_Card.Configure.Parameter.Machine_Area6_CupMin;
                ia_zero[0] = 0;
                //清空请求杯盖动作请求
                FADM_Object.Communal._tcpDyeHMI6.Write(508 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);
                ia_zero[0] = i_states;
                //修改盖子状态
                FADM_Object.Communal._tcpDyeHMI6.Write(117 + 64 * ((i_cupNo - i_cupnum + 1 - 1) % 6), ia_zero);

            }
        }

        
    }
}
