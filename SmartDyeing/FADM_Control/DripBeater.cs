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
    public partial class DripBeater : UserControl
    {
        private Cup _cup = null;
        private int _i_row;
        private int _i_cupMin;
        private int _i_cupMax;
        public DripBeater(int i_row, int i_cupmin, int i_cupmax)
        {
            InitializeComponent();
            _i_row = i_row;
            _i_cupMin = i_cupmin;
            _i_cupMax = i_cupmax;
            //foreach (Control c in this.Controls)
            //{
            //    if (c is Cup)
            //    {
            //        c.MouseDown += Cup_MouseDown;
            //    }
            //
            //}

            //计算列数(能整除)
            int i_cupline = 0;
            int i_p = 0;
            if ((i_cupmax - i_cupmin + 1) % i_row == 0)
            {
                i_cupline = (i_cupmax - i_cupmin + 1) / i_row;
            }
            else
            {
                i_cupline = (i_cupmax - i_cupmin + 1) / i_row + 1;
            }
            //
            for (int i = i_cupmin; i <= i_cupmax; i++)
            {
                i_p++;

                //初始化母液瓶
                Cup cup = new Cup();

                //计算瓶子X轴间隔
                int i_bottleInterval_X = (this.groupBox1.Width - cup.Width * i_row) / i_row;

                //计算瓶子Y轴间隔
                int i_bottleInterval_Y = (this.groupBox1.Height - 10 - (cup.Height) * i_cupline) / i_cupline;

                //定义母液瓶名称
                cup.Name = i.ToString();

                //显示母液瓶瓶号
                cup.NO = i.ToString();

                //计算母液瓶坐标
                cup.Location = new Point(i_bottleInterval_X / 2 + ((i_p - 1) % i_row * (i_bottleInterval_X + cup.Width)), this.groupBox1.Bottom + 10 - (i_cupline - (i_p - 1) / i_row) * (i_bottleInterval_Y + cup.Height));


                //显示母液瓶
                this.groupBox1.Controls.Add(cup);

            }
        }

        void Cup_MouseDown(object sender, MouseEventArgs e)
        {
            _cup = (Cup)sender;
        }
    }
}
