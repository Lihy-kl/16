using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHNSpec.Device.Bluetooth;
using CHNSpec.Device.Models;
using CHNSpec.Device.Models.Enums;
using BLECode;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using static System.Windows.Forms.AxHost;

namespace Interact
{
    public partial class Interact : Form
    {
        /// <summary>
        /// 蓝牙设备列表
        /// </summary>
        List<DeviceInfo> bluetoothList = new List<DeviceInfo>();

        public BluetoothHelper helper = new BluetoothHelper();

        List<string> bluetoothNane = new List<string>();

        //判断是否点击按钮测量
        bool bBtn = false;

        int iCount = 0;
        int iFail = 0;

        public Interact()
        {
            InitializeComponent();

            string sPath = Environment.CurrentDirectory + "\\Config\\Config.ini";
            string sName = Ini.GetIni("BlueTooth", "Name", "", sPath);
            textBox1.Text = sName;
        }

        private void Btn_Con_Click(object sender, EventArgs e)
        {
            if(!helper.bleCode.IsConnected)
            {
                label3.Text = "";
                Thread P_thd2 = new Thread(ThreadBlueToothCon);
                P_thd2.IsBackground = true;
                P_thd2.Start();
            }
        }

        private void Btn_Test_Click(object sender, EventArgs e)
        {
            string sPathResult = Environment.CurrentDirectory + "\\App_Data\\result.ini";


            string s = Ini.GetIni("Result", "Data",  sPathResult);
             MessageBox.Show(s);
            if (helper.bleCode.IsConnected)
            {
                bBtn = true;
                if (helper.Send_MeasureCmd(EnumMeasure_Mode.SCI, 0))
                {
                    //测量下发成功
                     MessageBox.Show("测量下发成功");
                }
                else
                {
                     MessageBox.Show("测量下发失败");
                }
            }
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //判断是否存在开始测量文件
            string sPath = Environment.CurrentDirectory + "\\App_Data\\start.ini";
            string sPathResult = Environment.CurrentDirectory + "\\App_Data\\result.ini";
            if (File.Exists(sPath))
            {

                timer2.Enabled = true;
                iCount++;

                //先删除文件，再测量
                timer1.Enabled = false;
                if (helper.bleCode.IsConnected)
                {
                    if (helper.Send_MeasureCmd(EnumMeasure_Mode.SCI, 0))
                    {
                        //发送测量信息
                    }
                }

            }            

            
        }

        /// <summary>
        /// 新增蓝牙设备
        /// </summary>
        private void AddBluetoothDevice(DeviceInfo data)
        {
            if (!IsHandleCreated) return;


            DeviceInfo deviceInfo = new DeviceInfo()
            {
                Address = data.Address,
                DeviceId = data.DeviceId,
                IsPaired = data.IsPaired,
                Name = data.Name,
                State = ConnectionStatus.Disconnected,
                Type = data.Type,
            };
            if (!bluetoothList.Contains(deviceInfo))
            {
                bluetoothList.Add(deviceInfo);
                this.Invoke(new Action(() =>
                {
                    if (!IsHandleCreated) return;

                    //listBox1.Items.Add(deviceInfo.Name);
                    bluetoothNane.Add(deviceInfo.Name);
                }));
            }
        }

        /// <summary>
        /// 停止查找蓝牙设备
        /// </summary>
        private void WatcherStopped()
        {
            if (!IsHandleCreated) return;

            this.Invoke(new Action(() =>
            {

            }));
        }

        /// <summary>
        /// 查找完设备完成
        /// </summary>
        private void EnumerationCompleted()
        {
            if (!IsHandleCreated) return;
            this.Invoke(new Action(() =>
            {

            }));
        }

        private void ThreadBlueToothCon()
        {
            bool bFind = false;
            string sPath = Environment.CurrentDirectory + "\\Config\\Config.ini";
            string sName = Ini.GetIni("BlueTooth", "Name", "", sPath);
            if (sName == "")
            {
                Ini.WriteIni("BlueTooth", "Name", "", sPath);
            }

            bluetoothNane.Clear();
            bluetoothList.Clear();

            helper.bleCode.StartBleDeviceWatcher("CM");

            Thread.Sleep(5000);
            DeviceInfo deviceInfo = null; ;
            for (int i = 0; i < bluetoothList.Count; i++)
            {
                deviceInfo = bluetoothList[i];
                if (deviceInfo.Name == sName)
                {
                    bFind = true;
                    break;
                }
            }

            if (deviceInfo == null)
            {
                return;
            }
            if (!bFind)
            {
                return;
            }
            bool result = helper.OpenBluetooth(deviceInfo.DeviceId, deviceInfo.Name);
            if (!result)
            {
                 MessageBox.Show("连接分光仪失败");
                return;
            }
        }

        private void Interact_Load(object sender, EventArgs e)
        {
            #region 蓝牙回调

            if (helper.bleCode != null)
            {
                //查找到的蓝牙设备
                helper.bleCode.Added = AddBluetoothDevice;

                //停止查找蓝牙设备
                helper.bleCode.WatcherStopped = WatcherStopped;

                //查找完设备完成
                helper.bleCode.EnumerationCompleted = EnumerationCompleted;
            }
            #endregion


            //订阅仪器连接状态
            DeviceCallback.ConnectionChangeCallback = (state) =>
            {
                this.Invoke(new Action(() =>
                {
                    if (state)
                    {
                        label3.Text = state ? "已连接" : "未连接";
                        timer1.Enabled = true;
                    }
                    //if (cmb_multilingual.SelectedIndex == 0)
                    //{
                    //lab_state.Text = state ? "已连接" : "未连接";
                    //}
                    //else
                    //{
                    //    lab_state.Text = state ? "Connected" : "Not connected";
                    //}
                }));

            };


            //订阅测量状态
            DeviceCallback.MeasureCallback = (state, result) =>
            {
                this.Invoke(new Action(() =>
                {
                    string msg = string.Empty;
                    if (state)
                    {
                        //if (cmb_multilingual.SelectedIndex == 0)
                        {
                            string spectrum = string.Empty;
                            foreach (var item in result.spectrums)
                            {
                                spectrum += "测量模式：" + item.measure_mode.ToString() + Environment.NewLine;
                                float[] f = new float[item.spectral_data.Count() - 12];
                                for (int i = 4; i < item.spectral_data.Count() - 8; i++)
                                {
                                    f[i - 4] = item.spectral_data[i];
                                    msg += item.spectral_data[i].ToString() + ",";
                                }
                                spectrum += "光谱信息：" + string.Join(",", item.spectral_data) + Environment.NewLine;
                            }
                        }

                    }
                    else
                    {
                        {
                            msg = "测量失败" + Environment.NewLine + Environment.NewLine;
                        }
                    }
                    if (bBtn)
                    {
                        bBtn = false;
                        if (!msg.Contains("测量失败"))
                        {

                             MessageBox.Show("测量成功");

                        }
                        else
                        {
                             MessageBox.Show("测量失败");
                        }
                    }
                    //文件交互
                    else
                    {
                        //删除开始文件，导出结果数据，开启定时器
                        if (!msg.Contains("测量失败"))
                        {
                            //
                            string sPathResult = Environment.CurrentDirectory + "\\App_Data\\result.ini";
                            string sPath = Environment.CurrentDirectory + "\\App_Data\\start.ini";
                            if (File.Exists(sPathResult))
                            {
                                File.Delete(sPathResult);
                            }

                            Ini.WriteIni("Result", "Data", msg, sPathResult);

                            File.Delete(sPath);

                            iFail = 0;
                        }
                        else
                        {
                            iFail++;
                        }
                        timer1.Enabled = true;
                    }

                }));
            };

            //订阅校准状态
            DeviceCallback.CalibrateCallback = (state) =>
            {
                {
                     MessageBox.Show(state ? "校准成功" : "校准失败");
                }
            };

            Thread P_thd2 = new Thread(ThreadBlueToothCon);
            P_thd2.IsBackground = true;
            P_thd2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            iFail++;
            if(iFail == 3)
            {
                string sPathResult = Environment.CurrentDirectory + "\\App_Data\\result.ini";
                string sPath = Environment.CurrentDirectory + "\\App_Data\\start.ini";
                if (File.Exists(sPathResult))
                {
                    File.Delete(sPathResult);
                }

                Ini.WriteIni("Result", "Data", "测量失败", sPathResult);

                iFail = 0;

                File.Delete(sPath);
            }
            timer1.Enabled = true;
            timer2.Enabled = false;
        }
    }
}
