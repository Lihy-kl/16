using SmartDyeing.FADM_Object;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        bool b_start = false;
        bool b = false;
        int ip = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                    return;
                ip = Convert.ToInt32(textBox1.Text);
                //移动到母液瓶
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找" + 1 + "号母液瓶");
                FADM_Object.Communal._i_optBottleNum = 1;
                int i_mRes = MyModbusFun.TargetMove(0, 1, 1);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达" + 1 + "号母液瓶");

                ////抽液
                //FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抽液启动(" + Convert.ToInt32(textBox1.Text) + ")");

                //MyModbusFun.Extract(Convert.ToInt32(textBox1.Text), false, 0);
                //接液盘回
                int[] _ia_array = new int[1];
                _ia_array[0] = 12;
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);

                Thread.Sleep(3000);

                //气缸下
                _ia_array[0] = 6;
                i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);

                Thread.Sleep(5000);

                //抓手关
                _ia_array[0] = 8;
                i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);

                Thread.Sleep(3000);

                MyModbusFun.TargetMoveRelative(3, Convert.ToInt32(textBox1.Text), Lib_Card.Configure.Parameter.Move_S_HSpeed, Convert.ToInt32( Lib_Card.Configure.Parameter.Move_S_UTime), Lib_Card.Configure.Parameter.Move_S_HSpeed);



                if (!b_start)
                {
                    b_start = true;
                    //启动线程
                    Thread thread = new Thread(Read); //
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            catch { }
        }

        private void Read()
        {
            while (true)
            {
                label1:
                Thread.Sleep(1000);
                if (!b_start)
                {
                    MyModbusFun.MyMachineReset();
                    break;
                }
                if(!b)
                {
                    //MyModbusFun.Shove(0, 0);
                    MyModbusFun.TargetMoveRelative(3, -Convert.ToInt32(textBox1.Text), Lib_Card.Configure.Parameter.Move_S_HSpeed, Convert.ToInt32(Lib_Card.Configure.Parameter.Move_S_UTime), Lib_Card.Configure.Parameter.Move_S_HSpeed);
                    b = !b;
                }
                else
                {
                    //MyModbusFun.Shove(ip, 0);
                    MyModbusFun.TargetMoveRelative(3, Convert.ToInt32(textBox1.Text), Lib_Card.Configure.Parameter.Move_S_HSpeed, Convert.ToInt32(Lib_Card.Configure.Parameter.Move_S_UTime), Lib_Card.Configure.Parameter.Move_S_HSpeed);
                    b = !b;
                }
                goto label1;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            b_start = false;
        }
    }
}
