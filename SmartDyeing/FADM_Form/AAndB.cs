using Lib_DataBank.MySQL;
using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public partial class AAndB : Form
    {
        public AAndB()
        {
            InitializeComponent();
        }

        bool b_start = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == ""|| textBox2.Text == "")
            {
                return;
            }
            d1 = Convert.ToDouble(textBox1.Text);
            d2 = Convert.ToDouble(textBox2.Text);
            d3 = Convert.ToDouble(textBox3.Text);
            cont = 0;
            if (!b_start)
            {
                b_start = true;
                //启动线程
                Thread thread = new Thread(Read); //
                thread.IsBackground = true;
                thread.Start();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            b_start = false;
        }

        double d1 = 1;
        double d2 = 397.7;
        double d3 = 0.1;
        int cont = 0;

        private void Read()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if(!b_start)
                {
                    break;
                }
            labTop:
                cont++;
                if(cont >10)
                {
                    cont = 1;
                    d1 += d3;
                }
                //移动到天平，然后伸出接液盘
                int i_mRes = MyModbusFun.TargetMove(2, 0, 0);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                int[] ia_array3 = { 11};
                FADM_Object.Communal._tcpModBus.Write(811, ia_array3);
                double d_start= FADM_Object.Communal.SteBalance();
                int d_1 = 0;
                int i_extract_Pulse = Convert.ToInt32(d1 * 1000);
                d_1 = i_extract_Pulse / 65536;
                if (i_extract_Pulse < 0) //负数脉冲
                {
                    if (d_1 == 0)
                    {
                        d_1 = -1;
                    }
                    else
                    {
                        if (Math.Abs(i_extract_Pulse) > 65536)
                        {
                            d_1 = d_1 + -1;
                        }
                    }
                }
                else
                {  //正数脉冲
                    d_1 = i_extract_Pulse / 65536;
                }
                i_extract_Pulse = i_extract_Pulse % 65536;

                int d_2 = 0;
                int i_extract_Pulse2 = Convert.ToInt32(d2 * 10);
                d_2 = i_extract_Pulse2 / 65536;
                if (i_extract_Pulse2 < 0) //负数脉冲
                {
                    if (d_2 == 0)
                    {
                        d_2 = -1;
                    }
                    else
                    {
                        if (Math.Abs(i_extract_Pulse2) > 65536)
                        {
                            d_2 = d_2 + -1;
                        }
                    }
                }
                else
                {  //正数脉冲
                    d_2 = i_extract_Pulse2 / 65536;
                }
                i_extract_Pulse2 = i_extract_Pulse2 % 65536;

                label1:
                int[] ia_array = { 1, i_extract_Pulse, d_1, i_extract_Pulse2, d_2 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(10, ia_array);
                if (i_state != -1)
                {
                    label2:
                    //判断错误返回值
                    int[] ia_array2 = { 1 };
                    int i_state2 = FADM_Object.Communal._tcpModBus.Read(15, 1, ref ia_array2);

                    if (i_state2 != -1)
                    {
                        if (ia_array2[0]==1)
                        {
                            //记录数据，继续执行下一次动作
                            double d_end = FADM_Object.Communal.SteBalance();
                            string s_sql = "insert into  AAndB(MB,XS,WC,SJ) Values ('" + d1.ToString("f2") + "','" + d2.ToString("f1") + "','" + (d_end-d_start).ToString("f3") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            //goto labTop;
                        }
                        else
                        {
                            //重新读取完成
                            Thread.Sleep(1000);
                            goto label2;
                        }
                    }

                }
                else
                {
                    goto label1;
                }
            }
        }

    }
}
