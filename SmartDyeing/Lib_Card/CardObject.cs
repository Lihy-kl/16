﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lib_Card
{
    public class CardObject
    {
        //public static Base.Card OA1 = new ADT8940A1.ADT8940A1_Card();
        //public static Base.Card OD1 = new ADT8940A1.ADT8940A1_Card();

        //public static ADT8940A1.InPut.InPut OA1Input = new ADT8940A1.InPut.InPut_Basic();
        //public static ADT8940D1.InPut.InPut OD1Input = new ADT8940D1.InPut.InPut_Basic();

        //public static ADT8940A1.Axis.Axis OA1Axis = new ADT8940A1.Axis.Axis_Condition();

        public static bool bLeft = false;//左光幕是否遮挡
        public static bool bRight = false;//右光幕是否遮挡
        public static bool bFront = false;//前光幕是否遮挡
        public static bool bStopScr = false;//急停按钮是否拍下

        public static bool bPLCStatus = false;//PLC连接状态


        private static readonly Mutex mutexlock = new Mutex(); 
            

        public struct prompt
        {
            public string Type;
            public string Info;
            public int Choose;
            public bool Speech;
            public int Count;
        }
        //提示信息
        public static Dictionary<string, prompt> keyValuePairs = new Dictionary<string, prompt>();
        public static Dictionary<string, prompt> keyValuePairsCopy = new Dictionary<string, prompt>();
        public static Dictionary<string, prompt> keyValuePairsCopy1 = new Dictionary<string, prompt>();

        /// <summary>
        /// 插入语音播报字典
        /// </summary>
        /// <param name="Text">待做事件</param>
        /// <param name="Caption">类型</param>
        public static string InsertD(string Text, string Caption)
        {
        labe2:
            try
            {
                Console.WriteLine("插入" + Text);
                keyValuePairsCopy1 = new Dictionary<string, prompt>(keyValuePairs);
                foreach (string s in keyValuePairsCopy1.Keys)
                {
                    //已存在
                    if (keyValuePairsCopy1[s].Type == Caption && keyValuePairsCopy1[s].Info == Text)
                    {
                        //mutexlock.WaitOne();
                        //try 
                        //{
                        //    //keyValuePairs[s].Count;
                        //}
                        //finally
                        //{ mutexlock.ReleaseMutex(); }
                        return s;
                    }
                }
            }
            catch {
                goto labe2;
            }


    label:

            try
            {
                DateTime dateTime = DateTime.Now;
                string time = dateTime.ToLongTimeString();
                prompt prompt = new prompt();

                prompt.Type = Caption;
                prompt.Info = Text;
                prompt.Choose = 0;
                prompt.Count = 0;
                keyValuePairs.Add(time, prompt);
                return time;
            }
            catch(Exception e) 
            {
               Console.WriteLine(e.Message);
                goto label;
            }

        }

        /// <summary>
        /// 插入语音播报字典(重复)
        /// </summary>
        /// <param name="Text">待做事件</param>
        /// <param name="Caption">类型</param>
        public static string InsertCF(string Text, string Caption)
        {
        label2:
            try
            {
                Console.WriteLine("插入" + Text);
                keyValuePairsCopy = new Dictionary<string, prompt>(keyValuePairs);
                foreach (string s in keyValuePairsCopy.Keys)
                {
                    //已存在
                    if (keyValuePairsCopy[s].Type == Caption && keyValuePairsCopy[s].Info == Text)
                    {
                        //mutexlock.WaitOne();
                        //try 
                        //{
                        //    //keyValuePairs[s].Count;
                        //}
                        //finally
                        //{ mutexlock.ReleaseMutex(); }
                        if (keyValuePairsCopy[s].Choose != 0)
                        {
                            //证明已经选择过
                            return "重复1";
                        }
                        else
                        {
                            return "重复";
                        }

                    }
                }

            }
            catch
            {
                goto label2;
            }


        label:

            try
            {
                DateTime dateTime = DateTime.Now;
                string time = dateTime.ToLongTimeString();
                prompt prompt = new prompt();

                prompt.Type = Caption;
                prompt.Info = Text;
                prompt.Choose = 0;
                prompt.Count = 0;
                prompt.Speech = false;
                keyValuePairs.Add(time, prompt);
                return time;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                goto label;
            }

        }

        //

        public static void DeleteD(string s)
        {
            Console.WriteLine("删除" + s);
            Thread thread = new Thread(() =>
            {
                if (!string.IsNullOrEmpty(s))
                {
                    try
                    {
                        while (true)
                        {
                            if (keyValuePairs[s].Speech)
                                break;
                            Thread.Sleep(1);
                        }
                        string str = keyValuePairs[s].Info;

                        keyValuePairs.Remove(s);
                        if( str== "右光幕遮挡" || str == "Right light curtain occlusion" || str == "右门已打开")
                        {
                            bRight = false;
                        }
                        if (str == "左光幕遮挡" || str == "Left light curtain occlusion" || str == "左门已打开")
                        {
                            bLeft = false;
                        }
                        if (str == "前光幕遮挡" || str == "Front light curtain occlusion")
                        {
                            bFront = false;
                        }
                        if (str == "急停已按下")
                        {
                            bStopScr = false;
                        }
                    }
                    catch
                    {

                    }

                }
            });
            thread.Start();

        }
    }
}
