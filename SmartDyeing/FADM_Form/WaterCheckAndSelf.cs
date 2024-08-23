using SmartDyeing.FADM_Auto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public partial class WaterCheckAndSelf : Form
    {
        public WaterCheckAndSelf()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            while (true)
            {
                if (0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus())
                {
                    FADM_Object.Communal.WriteDripWait(false);
                    break;
                }
                Thread.Sleep(1);
            }
        }

        private void RecheckWater()
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtWaterAddWeight.Text))
                {
                    double d_blObjectWeight = Convert.ToDouble(string.Format("{0:F3}", TxtWaterAddWeight.Text));
                    new FADM_Auto.Water().Recheck(d_blObjectWeight);
                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        throw new Exception("请输入验证水量");
                    else
                        throw new Exception("Please enter the verification water quantity");
                }

            }
            catch (Exception ex)
            {
                FADM_Object.Communal.WriteMachineStatus(8);
                FADM_Object.Communal.WriteDripWait(false);
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    new FADM_Object.MyAlarm(ex.Message, "水验证", false, 0);
                else
                {
                    string s_mes = ex.Message;
                    if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                    {
                        //如果存在就替换英文
                        s_mes = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                    }
                    new FADM_Object.MyAlarm(s_mes, "Water validation", false, 0);
                }
            }
        }

        private void CheckWater()
        {
            new FADM_Auto.Water().Check();
        }

        private void BtnWaterCheckStart_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击水校正启动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread())
            {

                FADM_Object.Communal.WriteDripWait(true);
                FADM_Object.Communal.WriteMachineStatus(5);


                Thread thread = new Thread(CheckWater);
                thread.Start();

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
        }

        private void BtnRecheckStart_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击水验证启动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread())
            {
                FADM_Object.Communal.WriteDripWait(true);
                FADM_Object.Communal.WriteMachineStatus(12);

                Thread thread = new Thread(RecheckWater);
                thread.Start();

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
        }
    }
}
