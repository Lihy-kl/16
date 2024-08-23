using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class CupDefin : UserControl
    {
        /// <summary>
        /// 定义新增标志位
        /// </summary>
        private bool _b_insert = false;

        public CupDefin()
        {
            InitializeComponent();

            //获取机台型号
            int i_totalCupNum = Lib_Card.Configure.Parameter.Machine_Cup_Total;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                groupBox2.Text = "共" + i_totalCupNum + "杯";
            }
            else
            {
                groupBox2.Text = "Total-" + i_totalCupNum ;
            }
            //获取瓶子列数
            int i_cupLine = 10;

            //显示杯子
            for (int i = 1; i <= i_totalCupNum; i++)
            {
                //初始化杯子

                Cup cup = new Cup();

                //计算杯子X轴间隔
                int i_cupInterval_X = (this.PnlCup.Width - cup.Width * i_cupLine) / i_cupLine;

                //计算杯子Y轴间隔
                int i_row = i_totalCupNum / i_cupLine + (i_totalCupNum % i_cupLine == 0 ? 0 : 1);
                int i_cupInterval_Y = (this.PnlCup.Height - cup.Height * i_row) / i_row;

                //定义杯子名称
                cup.Name = "Cup" + i;

                //显示杯子瓶号
                cup.NO = i.ToString();

                //计算杯子坐标
                cup.Location = new Point(((i - 1) % i_cupLine * (i_cupInterval_X + cup.Width)) + 10,
                     ((i - 1) / i_cupLine) * (i_cupInterval_Y + cup.Height));


                //关联杯子的点击事件
                cup.Click += Cup_Click;


                //显示杯子
                this.PnlCup.Controls.Add(cup);
               
            }

            CupHeadShow();



        }

        private void Cup_Click(object sender, EventArgs e)
        {
            Cup cup = (Cup)sender;
            string s_sql = "SELECT * FROM cup_details WHERE CupNum = "+cup.NO+";";
            DataTable dt_cuphead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if(dt_cuphead.Rows.Count >0)
            {
                int i_cupNum = Convert.ToInt16(dt_cuphead.Rows[0]["CupNum"]);
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    groupBox5.Text = i_cupNum.ToString() + "号杯信息";
                }
                else
                {
                    groupBox5.Text = i_cupNum.ToString() + "-CupInformation";
                }
                if (Convert.ToInt16(dt_cuphead.Rows[0]["IsFixed"]) == 1)
                {
                    chk_IsFixed.Checked = true;
                }
                else
                {
                    chk_IsFixed.Checked = false;
                }
                if(Convert.ToInt16(dt_cuphead.Rows[0]["Enable"]) == 1)
                {
                    chk_Enable.Checked = true;
                }
                else
                {
                    chk_Enable.Checked = false;
                }
                if(Convert.ToInt16(dt_cuphead.Rows[0]["IsUsing"]) == 1)
                {
                    chk_IsUsing.Checked = true;
                }
                else
                {
                    chk_IsUsing.Checked = false;
                }
            }
        }

        /// <summary>
        /// 显示滴液杯表
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private void CupHeadShow()
        {
            try
            {
               
                string s_sql = "SELECT * FROM cup_details  order by CupNum;";
                DataTable dt_cuphead = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                foreach (DataRow row in dt_cuphead.Rows)
                {
                    int i_cupNum = Convert.ToInt16(row["CupNum"]);
                    if (Convert.ToInt16( row["IsFixed"]) == 1)
                    {
                        txt_IsFixed.Text += i_cupNum + " ";
                    }
                    if(Convert.ToInt16(row["Enable"] )== 1)
                    {
                        txt_Enable.Text += i_cupNum + " ";
                    }
                    if (Convert.ToInt16(row["IsUsing"]) == 1)
                    {
                        txt_IsUsing.Text += i_cupNum + " ";
                    }
                    if(Convert.ToInt16(row["Type"]) == 2)
                    {
                        string name = "Cup" + i_cupNum;
                       
                        ((Cup)(this.PnlCup.Controls.Find(name, false)[0])).cupColor = Color.Red;
                    }
                }


            }
            catch
            {

            }
        }















    }
}
