using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Speech.Synthesis;
using static System.Net.Mime.MediaTypeNames;
using SmartDyeing.FADM_Auto;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using com.google.zxing;
using System.Data;

namespace SmartDyeing.FADM_Object
{
    class MyAlarm
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        #region 全局变量

        /// <summary>
        /// 选择:0:未选择;1:Yes;2:No
        /// </summary>
        public int _i_alarm_Choose = 0;

        /// <summary>
        /// 是否重复 1：重复 0：不重复
        /// </summary>
        public int _i_alarm_Repeat = 0;



        #endregion

        #region 构造函数
        public MyAlarm(string s_text, int i_speech, params bool[] ba_isBroadcastW)
        {
            if (!string.IsNullOrEmpty(s_text))
            {
            label1:
                try
                {
                    string s_insert = Lib_Card.CardObject.InsertCF(s_text, Lib_Card.Configure.Parameter.Other_Language == 0 ? "温馨提示":"Tips");

                    if (s_insert == "重复"|| s_insert == "重复1")
                    {
                        return;
                    }
                    if (s_text != "右光幕遮挡" && s_text != "左光幕遮挡" && s_text != "前光幕遮挡" && s_text != "急停已按下" && s_text != "左门已打开" && s_text != "右门已打开" && FADM_Object.Communal._b_isNetWork)
                    {
                        //插入微信播报表 推送微信 isBroadcastW为真 保存表   isBroadcastW为假 不保存表   
                        inBroadcastW(FADM_Object.Communal._s_machineCode, s_text, s_insert, ba_isBroadcastW);
                    }

                    Thread P_thd_1 = new Thread(() =>
                    {
                        Thread P_thd_2 = new Thread(() =>
                        {
                            if (s_text == "右光幕遮挡" || s_text == "左光幕遮挡" || s_text == "前光幕遮挡" || s_text == "急停已按下" || s_text == "左门已打开" || s_text == "右门已打开"
                            || s_text == "Right light curtain occlusion" || s_text == "The right door is open"
                            || s_text == "Left light curtain occlusion" || s_text == "The left door is open"
                            || s_text == "Emergency stop pressed" || s_text == "Front light curtain occlusion")
                            {
                                //Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer buzzer = new Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer_Basic();
                                //buzzer.Buzzer_On();


                                while (true)
                                {
                                    if (s_text == "右光幕遮挡" || s_text == "Right light curtain occlusion" || s_text == "右门已打开"|| s_text == "The right door is open")
                                    {
                                        //获取光幕1状态
                                        if (!Lib_Card.CardObject.bRight)
                                        {
                                            Lib_Card.CardObject.DeleteD(s_insert);
                                            break;
                                        }
                                        if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                        {
                                            Lib_Card.CardObject.DeleteD(s_insert);
                                            break;
                                        }

                                    }
                                    else if (s_text == "左光幕遮挡" || s_text == "Left light curtain occlusion" || s_text == "左门已打开" || s_text == "The left door is open")
                                    {
                                        //获取光幕2状态
                                        if (!Lib_Card.CardObject.bLeft)
                                        {
                                            Lib_Card.CardObject.DeleteD(s_insert);

                                            break;
                                        }
                                        if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                        {
                                            Lib_Card.CardObject.DeleteD(s_insert);
                                            break;
                                        }
                                    }
                                    else if (s_text == "前光幕遮挡" || s_text == "Front light curtain occlusion")
                                    {
                                        //获取光幕2状态
                                        if (!Lib_Card.CardObject.bFront)
                                        {
                                            Lib_Card.CardObject.DeleteD(s_insert);

                                            break;
                                        }
                                        if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                        {
                                            Lib_Card.CardObject.DeleteD(s_insert);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //获取急停按钮状态
                                        if (s_text == "急停已按下" || s_text == "Emergency stop pressed")
                                        {
                                            if (!Lib_Card.CardObject.bStopScr)
                                            {
                                                Lib_Card.CardObject.DeleteD(s_insert);
                                                break;
                                            }
                                        }
                                        if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                        {
                                            Lib_Card.CardObject.DeleteD(s_insert);
                                            break;
                                        }
                                    }
                                    Thread.Sleep(1);
                                }
                                //buzzer.Buzzer_Off();
                            }
                            else
                            {
                                //触发定时关闭对话框功能
                                Thread.Sleep(1000);
                                Lib_Card.CardObject.DeleteD(s_insert);
                                kill();

                            }
                        });

                        P_thd_2.Start();



                    });


                    // 启动线程
                    P_thd_1.Start();
                }
                catch
                {
                    goto label1;
                }


            }
        }


        public MyAlarm(string s_text, string s_caption)
        {
            if (!string.IsNullOrEmpty(s_text))
            {
            label1:
                try
                {
                    Thread thread = new Thread(() =>
                    {

                        
                            string s_insert = Lib_Card.CardObject.InsertCF(s_text, s_caption);
                            if (s_insert == "重复")
                            {
                                return;
                            }

                            //Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer buzzer = new Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer_Basic();
                            //buzzer.Buzzer_On();


                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                    break;

                            }

                            Lib_Card.CardObject.DeleteD(s_insert);


                            //buzzer.Buzzer_Off();
                        




                    });
                    thread.Start();
                }
                catch
                {
                    goto label1;
                }
            }
        }

        public MyAlarm(string s_text, string s_caption, int i_cupNum, int i_req, int i_type)
        {
            if (!string.IsNullOrEmpty(s_text))
            {
            label1:
                try
                {
                    Thread thread = new Thread(() =>
                    {


                        string s_insert = Lib_Card.CardObject.InsertCF(s_text, s_caption);
                        if (s_insert == "重复")
                        {
                            //_i_alarm_Choose = 1;
                            return;
                        }
                        else if (s_insert == "重复1")
                        {
                            if (i_type == 4 )
                            {
                                //修改为加药
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE dye_details SET  Cooperate = 1 WHERE  Cooperate = " + 9 + " AND CupNum = " + i_cupNum + " ;");

                                _i_alarm_Repeat = 1;
                                return;

                            }
                            else if (i_type == 5)
                            {
                                //修改为流程加水
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE dye_details SET  Cooperate = 3 WHERE  Cooperate = " + 9 + " AND CupNum = " + i_cupNum + " ;");

                                _i_alarm_Repeat = 1;
                                return;
                            }
                            else if (i_type == 6)
                            {
                                //修改为洗杯加水
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET  Cooperate = 4 WHERE  CupNum = " + i_cupNum + " ;");

                                _i_alarm_Repeat = 1;
                                return;
                            }
                            else if (i_type == 7)
                            {
                                //滴液加水
                                _i_alarm_Repeat = 1;
                                return;
                            }
                            else if (i_type == 8)
                            {
                                //修改为放布
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET  Cooperate = 8 WHERE  CupNum = " + i_cupNum + " ;");

                                _i_alarm_Repeat = 1;
                                return;
                            }
                            else if (i_type == 9)
                            {
                                //修改为出布
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET  Cooperate = 9 WHERE  CupNum = " + i_cupNum + " ;");

                                _i_alarm_Repeat = 1;
                                return;
                            }
                            else if (i_type == 10)
                            {
                                //预滴液数值太小
                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE drop_details SET  MinWeight = 0 WHERE  MinWeight = " + 4 + " AND BottleNum = " + i_cupNum + " ;");

                                _i_alarm_Repeat = 1;
                                return;
                            }
                            else
                            {
                                FADM_Auto.Dye._cup_Temps[i_cupNum - 1]._i_cover = 0;

                                FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET  Cooperate = 0 WHERE  CupNum = " + i_cupNum + " ;");

                                _i_alarm_Repeat = 1;
                                return;
                            }
                        }

                        //Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer buzzer = new Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer_Basic();
                        //buzzer.Buzzer_On();


                        while (true)
                        {
                            Thread.Sleep(1);
                            if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                break;

                        }

                        _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;

                        //正常重复执行，重置等待下一次轮询
                        if (i_req == 1)
                        {

                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET  Cooperate = 0 WHERE  CupNum = " + i_cupNum + " ;");

                        }
                        //选择继续执行或者已完成开关盖
                        else
                        {
                            //如果选择是，就继续
                            if (_i_alarm_Choose == 1)
                            {
                                if (i_type == 4)
                                {
                                    //修改为加药
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE dye_details SET  Cooperate = 1 WHERE  Cooperate = " + 9 + " AND CupNum = " + i_cupNum + " ;");

                                }
                                else if (i_type == 5)
                                {
                                    //修改为流程加水
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE dye_details SET  Cooperate = 3 WHERE  Cooperate = " + 9 + " AND CupNum = " + i_cupNum + " ;");
                                }
                                else if (i_type == 6)
                                {
                                    //修改为洗杯加水
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET  Cooperate = 4 WHERE  CupNum = " + i_cupNum + " ;");
                                }
                                else if (i_type == 7)
                                {
                                    //滴液加水
                                }
                                else if (i_type == 8 )
                                {
                                    //修改为放布
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET  Cooperate = 8 WHERE  CupNum = " + i_cupNum + " ;");
                                }
                                else if (i_type == 9 )
                                {
                                    //修改为出布
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET  Cooperate = 9 WHERE  CupNum = " + i_cupNum + " ;");
                                }
                                else if (i_type == 10)
                                {
                                    //预滴液数值太小
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE drop_details SET  MinWeight = 0 WHERE  MinWeight = " + 4 + " AND BottleNum = " + i_cupNum + " ;");

                                    _i_alarm_Repeat = 1;
                                }
                                else if (i_type == 12)
                                {
                                    //关盖失败，已把盖子放到杯中，要人工检查是否关盖正常，点击是就直接完成关盖，继续运行

                                    //复位加药启动信号
                                    int[] ia_zero = new int[1];
                                    //
                                    int i_cupNo = i_cupNum;
                                    ia_zero[0] = 0;

                                    FADM_Auto.Dye.DyeOpenOrCloseCover(i_cupNo, 1);

                                    FADM_Auto.Dye._cup_Temps[i_cupNo - 1]._i_cover = 2;
                                    Thread.Sleep(2000);
                                    Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 1,Cooperate=0 where CupNum = " + i_cupNo);
                                    //Communal._fadmSqlserver.ReviseData("Update  cup_details set Cooperate=0 where Cooperate = 5 and CupNum = " + i_cupNo);

                                    FADM_Auto.Dye._cup_Temps[i_cupNo - 1]._i_cupCover = 1;
                                }
                                else
                                {
                                    FADM_Auto.Dye._cup_Temps[i_cupNum - 1]._i_cover = 0;

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET  Cooperate = 0 WHERE  CupNum = " + i_cupNum + " ;");
                                }
                            }
                            //选择否就直接置杯盖状态
                            else
                            {
                                //关盖完成
                                if (i_type == 1)
                                {
                                    //复位加药启动信号
                                    int[] ia_zero = new int[1];
                                    //
                                    int i_cupNo = i_cupNum;
                                    ia_zero[0] = 0;

                                    FADM_Auto.Dye.DyeOpenOrCloseCover(i_cupNo,1);

                                    FADM_Auto.Dye._cup_Temps[i_cupNo - 1]._i_cover = 2;
                                    Thread.Sleep(2000);
                                    Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 1,Cooperate=0 where CupNum = " + i_cupNo);
                                    //Communal._fadmSqlserver.ReviseData("Update  cup_details set Cooperate=0 where Cooperate = 5 and CupNum = " + i_cupNo);

                                    FADM_Auto.Dye._cup_Temps[i_cupNo - 1]._i_cupCover = 1;
                                }
                                //开盖完成
                                else if (i_type == 2 || i_type == 5 || i_type == 4 || i_type == 6 || i_type == 8 || i_type == 9)
                                {
                                    //复位加药启动信号
                                    int[] ia_zero = new int[1];
                                    //
                                    int iCupNo = i_cupNum;
                                    ia_zero[0] = 0;
                                   

                                    FADM_Auto.Dye.DyeOpenOrCloseCover(iCupNo, 2);

                                    FADM_Auto.Dye._cup_Temps[iCupNo - 1]._i_cover = 2;
                                    Thread.Sleep(2000);
                                    Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 2,Cooperate=0 where CupNum = " + iCupNo);

                                    FADM_Auto.Dye._cup_Temps[iCupNo - 1]._i_cupCover = 2;

                                    //加药
                                    if (i_type == 4)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE dye_details SET  Cooperate = 1 WHERE  Cooperate = " + 9 + " AND CupNum = " + i_cupNum + " ;");
                                    }
                                    //流程加水
                                    else if (i_type == 5)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE dye_details SET  Cooperate = 3 WHERE  Cooperate = " + 9 + " AND CupNum = " + i_cupNum + " ;");
                                    }
                                    //洗杯加水
                                    else if (i_type == 6)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE cup_details SET  Cooperate = 4 WHERE  CupNum = " + i_cupNum + " ;");
                                    }
                                    else if (i_type == 8)
                                    {
                                        //修改为放布
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE cup_details SET  Cooperate = 8 WHERE  CupNum = " + i_cupNum + " ;");
                                    }
                                    else if (i_type == 9)
                                    {
                                        //修改为出布
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                           "UPDATE cup_details SET  Cooperate = 9 WHERE  CupNum = " + i_cupNum + " ;");
                                    }
                                    else if (i_type == 10)
                                    {
                                        //预滴液数值太小
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                       "UPDATE drop_details SET  MinWeight = 0 WHERE  MinWeight = " + 4 + " AND BottleNum = " + i_cupNum + " ;");

                                        _i_alarm_Repeat = 1;
                                    }

                                }
                                //无锁止信号操作
                                else if (i_type == 3)
                                {
                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET  Cooperate = 0 WHERE  CupNum = " + i_cupNum + " ;");
                                }
                                else if (i_type == 13 || i_type == 14)
                                {
                                    FADM_Auto.Dye._cup_Temps[i_cupNum - 1]._i_cover = 0;

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                                   "UPDATE cup_details SET  Cooperate = 0 WHERE  CupNum = " + i_cupNum + " ;");
                                }
                                else if(i_type==12)
                                {
                                    //关盖失败，已把盖子放到杯中，要人工检查是否关盖正常，点击否也直接完成关盖，继续运行

                                    //复位加药启动信号
                                    int[] ia_zero = new int[1];
                                    //
                                    int i_cupNo = i_cupNum;
                                    ia_zero[0] = 0;

                                    FADM_Auto.Dye.DyeOpenOrCloseCover(i_cupNo, 1);

                                    FADM_Auto.Dye._cup_Temps[i_cupNo - 1]._i_cover = 2;
                                    Thread.Sleep(2000);
                                    Communal._fadmSqlserver.ReviseData("Update  cup_details set CoverStatus = 1,Cooperate=0 where CupNum = " + i_cupNo);
                                    //Communal._fadmSqlserver.ReviseData("Update  cup_details set Cooperate=0 where Cooperate = 5 and CupNum = " + i_cupNo);

                                    FADM_Auto.Dye._cup_Temps[i_cupNo - 1]._i_cupCover = 1;
                                }
                                //7滴液过程加水在外边处理

                            }

                        }

                        //if (messageBoxButtons == MessageBoxButtons.YesNo)
                        {
                            //写进数据库
                            string s_sql = "INSERT INTO alarm_table" +
                                         "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                         " VALUES( '" +
                                         String.Format("{0:d}", DateTime.Now) + "','" +
                                         String.Format("{0:T}", DateTime.Now) + "','SwitchCover','" +
                                         s_text + "(" + (_i_alarm_Choose == 1 ? "Yes" : "No") + ")');";

                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        }

                        Lib_Card.CardObject.DeleteD(s_insert);


                        //buzzer.Buzzer_Off();





                    });
                    thread.Start();
                }
                catch
                {
                    goto label1;
                }
            }
        }

        private void kill()
        {
            IntPtr ptr = FindWindow(null, "温馨提示");
            if (ptr != IntPtr.Zero)
            {
                //找到则关闭MessageBox窗口  

                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);


            }
        }


        public MyAlarm(string s_text, string s_caption, bool b_choose, int i_speech)
        {
            if (!string.IsNullOrEmpty(s_text))
            {
            label1:
                try
                {
                    //Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer buzzer = new Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer_Basic();
                    // buzzer.Buzzer_On();

                    string s_insert = Lib_Card.CardObject.InsertCF(s_text, s_caption);
                    string s_errcode = "";
                    JObject obj = null;
                    if (s_insert == "重复")
                    {
                        return;
                    }
                    else if (s_insert == "重复1")
                    {
                        if (b_choose)
                        {
                            //休眠1秒，等待上一个消除(原因是因为有可能已做选择，但由于没播报导致待办没消除)
                            Thread.Sleep(1000);
                            goto label1;
                        }
                    }
                    else
                    {
                        if (FADM_Object.Communal._b_isNetWork)
                        {
                            //插入微信播报表
                            string Result = inBroadcastW(FADM_Object.Communal._s_machineCode, s_text, s_insert, true);
                            if (Result != null && Result.Length > 0)
                            {
                                obj = JObject.Parse(Result);
                                if (obj["istrue"].Value<string>().Equals("true") && obj["errcode"].Value<string>().Equals("0"))
                                {
                                    s_errcode = "0";
                                }
                            }
                        }
                    }

                    while (true)   //new对象  等待当前是否结果
                    {
                        Thread.Sleep(1);
                        if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                            break;
                        if (s_errcode.Equals("0"))
                        {
                            //发起请求查询 
                            getBroadcastRe(FADM_Object.Communal._s_machineCode, s_insert, s_insert);
                            Thread.Sleep(1500);
                        }
                    }

                    _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;

                    //写进数据库
                    string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 s_caption + "','" +
                                 s_text + "(" + (_i_alarm_Choose == 1 ? "Yes" : "No") + ")');";

                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                    Lib_Card.CardObject.DeleteD(s_insert);


                    // buzzer.Buzzer_Off();
                }
                catch
                {
                    goto label1;
                }
            }
        }

         public static string inBroadcastW(string machineCode,string Text,string time, params bool[] isFlag) {
            Console.WriteLine("插入播报表==============");
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("machineCode", machineCode);
            parameters.Add("Text", Text);
            parameters.Add("time", time);
            HttpWebResponse response = HttpUtil.CreatePostHttpResponse("https://www.gz-kelian.com/outer/product/inBroadcastW", parameters, 15000, null, null);
            Stream st = response.GetResponseStream();
            StreamReader reader = new StreamReader(st);
            string msg = reader.ReadToEnd();
            Console.WriteLine("返回内容是" + msg);
            return msg;
            /*return await Task.Run(() => {
                // htt ps://www.gz-kelian.com/outer/product/inBroadcastW  h ttp://192.168.144.105:8080/outer/product/inBroadcastW
                */
            //});
        }

        async public static void getBroadcastRe(string machineCode, string time, string s)
        {
            try
            {
                Console.WriteLine("获取播报表结果状态==============");
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("machineCode", machineCode);
                parameters.Add("time", time);
                await Task.Run(() => {
                    // htt ps://www.gz-kelian.com/outer/product/inBroadcastW  h ttp://192.168.144.105:8080/outer/product/inBroadcastW
                    HttpWebResponse response = HttpUtil.CreatePostHttpResponse("https://www.gz-kelian.com/outer/product/getBroadcastRe", parameters, 15000, null, null);
                    Stream st = response.GetResponseStream();
                    StreamReader reader = new StreamReader(st);
                    string msg = reader.ReadToEnd();
                    Console.WriteLine("获取播报表结果结果" + msg);
                    JObject obj = JObject.Parse(msg);
                    if (obj["istrue"].Value<string>().Equals("true"))
                    {
                        Lib_Card.CardObject.prompt prompt = new Lib_Card.CardObject.prompt();
                        prompt = Lib_Card.CardObject.keyValuePairs[s];
                        prompt.Choose = Convert.ToInt32(obj["state"].Value<string>());
                        Lib_Card.CardObject.keyValuePairs[s] = prompt;
                    }
                });
            }
            catch
            {

            }
        }

        #endregion

        public MyAlarm(string s_text, int i_bottleNo, int i_oldSign, MessageBoxButtons messageBoxButtons)
        {
        label1:
            try
            {
                if (!string.IsNullOrEmpty(s_text))
                {
                    Thread thread = new Thread(() =>
                    {
                        string s_insert = Lib_Card.CardObject.InsertCF(s_text, "AddMedicine");
                        if (s_insert == "重复")
                        {
                            //FADM_Object.Communal._fadmSqlserver.ReviseData(
                            //   "UPDATE dye_details SET  Cooperate = 1 WHERE  Cooperate = " + i_oldSign + " AND BottleNum = " + i_bottleNo + " ;");
                            return;
                        }
                        else if (s_insert == "重复1")
                        {
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE dye_details SET  Cooperate = 1 WHERE  Cooperate = " + i_oldSign + " AND BottleNum = " + i_bottleNo + " ;");
                            return;
                        }

                        //Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer buzzer = new Lib_Card.ADT8940A1.OutPut.Buzzer.Buzzer_Basic();
                        //buzzer.Buzzer_On();


                        while (true)
                        {
                            Thread.Sleep(1);
                            if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                break;

                        }

                        _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                        if (messageBoxButtons == MessageBoxButtons.YesNo)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose == 2)
                            {
                                //选择否
                                Dye._dic_keyValue[i_bottleNo] = true;

                            }
                            else
                            {
                                if (i_oldSign == 5 || i_oldSign == 6)
                                {
                                    //如果点击是,在液量低和过期选项下，更新母液瓶数据，删除备料记录
                                    string s_sqlpre = "SELECT * FROM pre_brew WHERE  BottleNum = " + i_bottleNo + ";";
                                    DataTable dt_pre_brew = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlpre);
                                    if (dt_pre_brew.Rows.Count > 0)
                                    {

                                        string s_sql = "UPDATE bottle_details SET RealConcentration = '" + dt_pre_brew.Rows[0]["RealConcentration"].ToString() + "',CurrentWeight = '" + dt_pre_brew.Rows[0]["CurrentWeight"].ToString() + "',BrewingData='"
                                            + dt_pre_brew.Rows[0]["BrewingData"].ToString() + "'" +
                                        " WHERE BottleNum = " + i_bottleNo + ";";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        FADM_Object.Communal._fadmSqlserver.ReviseData("Delete from pre_brew where BottleNum = " + i_bottleNo);
                                    }
                                }
                            }
                        }

                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                               "UPDATE dye_details SET  Cooperate = 1 WHERE  Cooperate = " + i_oldSign + " AND BottleNum = " + i_bottleNo + " ;");
                        if (messageBoxButtons == MessageBoxButtons.YesNo)
                        {
                            //写进数据库
                            string s_sql = "INSERT INTO alarm_table" +
                                         "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                         " VALUES( '" +
                                         String.Format("{0:d}", DateTime.Now) + "','" +
                                         String.Format("{0:T}", DateTime.Now) + "','AddMedicine','" +
                                         s_text + "(" + (_i_alarm_Choose == 1 ? "Yes" : "No") + ")');";

                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                        }
                        else
                        {
                            string s_sql = "INSERT INTO alarm_table" +
                                        "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                        " VALUES( '" +
                                        String.Format("{0:d}", DateTime.Now) + "','" +
                                        String.Format("{0:T}", DateTime.Now) + "','AddMedicine','" +
                                        s_text + "(OK)');";

                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                        Lib_Card.CardObject.DeleteD(s_insert);


                        //buzzer.Buzzer_Off();




                    });
                    thread.Start();
                }
            }
            catch
            {
                goto label1;
            }


        }

    }
}
