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
    public partial class Check : Form
    {
        public Check()
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

        private void BottleCheck()
        {
            try
            {
                FADM_Object.Communal._fadmSqlserver.ReviseData("TRUNCATE TABLE bottle_check");

                char[] ca_chars = { ',', '，' };
                string[] sa_strings = TxtCheckBottleNo.Text.Split(ca_chars);

                char[] ca_chars1 = { '-', '-' };
                List<int> lis_ints = new List<int>();
                foreach (string s in sa_strings)
                {
                    string[] sa_strings1 = s.Split(ca_chars1);
                    if (1 == sa_strings1.Length)
                        lis_ints.Add(Convert.ToInt16(s));
                    else
                        for (int i = Convert.ToInt16(sa_strings1[0]); i <= Convert.ToInt16(sa_strings1[1]); i++)
                            lis_ints.Add(i);

                }

                lis_ints.Sort();
                lis_ints = lis_ints.Distinct().ToList();

                foreach (int i in lis_ints)
                {
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                        "SELECT * FROM bottle_details WHERE BottleNum = " + i + ";");
                    if (0 == dt_data.Rows.Count || i > Lib_Card.Configure.Parameter.Machine_Bottle_Total)
                        continue;
                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "INSERT INTO bottle_check(BottleNum, Finish, Successed) VALUES('" + i + "',0,0);");


                }

                Thread thread = new Thread(() =>
                {
                    new FADM_Auto.BottleCheck().Check();
                });
                thread.Start();



            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "BottleCheck",
                    MessageBoxButtons.OK, false);
                else
                {
                    string s_mes = ex.Message;
                    if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                    {
                        //如果存在就替换英文
                        s_mes = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                    }
                    FADM_Form.CustomMessageBox.Show(s_mes, "BottleCheck",
                    MessageBoxButtons.OK, false);
                }

                FADM_Object.Communal.WriteDripWait(false);
                FADM_Object.Communal.WriteMachineStatus(8);
            }
        }

        private void BtnBottleCheckStart_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检启动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread())
            {
                FADM_Object.Communal.WriteDripWait(true);
                FADM_Object.Communal.WriteMachineStatus(6);

                Thread thread = new Thread(BottleCheck);
                thread.Start();

                Thread threadReset = new Thread(Reset);
                threadReset.Start();

            }

        }

        private void BtnBottleCheckPause_Click(object sender, EventArgs e)
        {
            if (6 == FADM_Object.Communal.ReadMachineStatus())
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    if ("暂 停" == BtnBottleCheckPause.Text)
                    {
                        FADM_Object.Communal._b_pause = true;
                        BtnBottleCheckPause.Text = "恢 复";
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检暂停");
                    }
                    else
                    {
                        FADM_Object.Communal._b_pause = false;
                        BtnBottleCheckPause.Text = "暂 停";
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检恢复");
                    }
                }
                else
                {
                    if ("Pause" == BtnBottleCheckPause.Text)
                    {
                        FADM_Object.Communal._b_pause = true;
                        BtnBottleCheckPause.Text = "Restore";
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检暂停");
                    }
                    else
                    {
                        FADM_Object.Communal._b_pause = false;
                        BtnBottleCheckPause.Text = "Pause";
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检恢复");
                    }
                }
            }
        }

        private void BtnBottleCheckStop_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检停止");
            if (6 == FADM_Object.Communal.ReadMachineStatus())
            {
                FADM_Object.Communal._b_stop = true;

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
        }
    }
}
