using SmartDyeing.FADM_Form;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing
{
    //internal static class Program
    //{
    //    /// <summary>
    //    /// 应用程序的主入口点。
    //    /// </summary>
    //    [STAThread]
    //    static void Main()
    //    {
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);
    //        Application.Run(new Login());
    //    }
    //}

    internal static class Program
    {
        /// <summary> 
        /// 该函数设置由不同线程产生的窗口的显示状态。 
        /// </summary> 
        /// <param name="hWnd">窗口句柄</param> 
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary> 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary> 
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //获取参数
            try
            {
                Lib_Card.Configure.Parameter parameter = new Lib_Card.Configure.Parameter();
                string sPath = Environment.CurrentDirectory + "\\Config\\parameter.ini";
                foreach (PropertyInfo info in parameter.GetType().GetProperties())
                {
                    char[] separator = { '_' };
                    string head = info.Name.Split(separator)[0];
                    Console.WriteLine(info.Name);
                    if (info.Name.Equals("CylinderVersion")) {
                        
                    }
                    if (info.PropertyType == typeof(int))
                    {
                        int value = Convert.ToInt32(Lib_File.Ini.GetIni(head, info.Name, sPath));
                        parameter.GetType().GetProperty(info.Name).SetValue(parameter, value);

                    }
                    else if (info.PropertyType == typeof(double))
                    {
                        double value = Convert.ToDouble(Lib_File.Ini.GetIni(head, info.Name, sPath));
                        parameter.GetType().GetProperty(info.Name).SetValue(parameter, value);
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "parameter", MessageBoxButtons.OK, false);
                System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();
            }


            Process instance = RunningInstance();
            if (instance == null)
            {
                string sLanguage = Lib_Card.Configure.Parameter.Other_Language  == 0 ? "zh":"en";
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(sLanguage);

                FADM_Form.Login login = new Login();

                //界面转换
                login.ShowDialog();

                if (login.DialogResult == DialogResult.OK)
                {
                    login.Dispose();
                    FADM_Form.Main main = new FADM_Form.Main();
                    Application.Run(main);
                }

                else
                {
                    System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();
                    return;
                }
            }
            else
            {
                HandleRunningInstance(instance);
            }
        }

        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") != current.MainModule.FileName)
                    {
                        continue;
                    }
#pragma warning restore CS8602 // 解引用可能出现空引用。
                    return process;
                }
            }
#pragma warning disable CS8603 // 可能返回 null 引用。
            return null;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }

        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 3); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端 
        }
    }
}
