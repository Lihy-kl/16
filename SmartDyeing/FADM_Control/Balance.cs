using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class Balance : UserControl
    {

        //电子秤区域
        Rectangle m_workingRect;
        private string title = "0.00";
        [Description("天平读数"), Category("自定义")]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                ResetWorkingRect();
                Refresh();
            }
        }


        private readonly Color bottleColor = Color.Black;

        private Color liquidColor = Color.DeepSkyBlue;
        [Description("液体颜色"), Category("自定义")]
        public Color LiquidColor
        {
            get { return liquidColor; }
            set
            {
                liquidColor = value;
                Refresh();
            }
        }

        [Description("文字字体"), Category("自定义")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                ResetWorkingRect();
                Refresh();
            }
        }


        [Description("文字颜色"), Category("自定义")]
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                Refresh();
            }
        }

        private decimal maxValue = 2000;
        [Description("最大值"), Category("自定义")]
        public decimal MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                Refresh();
            }
        }

        public string m_NO = "天平";
        [Description("天平编号"), Category("自定义")]
        public string NO
        {
            get { return m_NO; }
            set
            {
                m_NO = value;
                Refresh();
            }
        }

        public Balance()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.SizeChanged += UCBalance_SizeChanged;
            this.Size = new Size(160, 160);
            this.BackColor = Color.Transparent;

        }


        void UCBalance_SizeChanged(object sender, EventArgs e)
        {
            ResetWorkingRect();
        }


        private void ResetWorkingRect()
        {
            var g = this.CreateGraphics();
            g.MeasureString(title, Font);
            m_workingRect = new Rectangle(0, 10, this.Width, this.Height - 15);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            Pen myPen = new Pen(bottleColor, 1);

            //画废液桶口
            GraphicsPath pathBM = new GraphicsPath();
            Point[] psBM = new Point[]
                {
                    new Point(m_workingRect.Left + m_workingRect.Width / 4 +9,  m_workingRect.Top - 10 + 1),
                    new Point(m_workingRect.Right - m_workingRect.Width / 4-10, m_workingRect.Top - 10 + 1),
                    new Point(m_workingRect.Right - m_workingRect.Width / 4-10, m_workingRect.Top ),
                    new Point(m_workingRect.Left + m_workingRect.Width / 4+9,  m_workingRect.Top ),

                };
            pathBM.AddLines(psBM);
            pathBM.CloseAllFigures();
            g.DrawPolygon(myPen, psBM);

            //画废液桶
            GraphicsPath pathPS = new GraphicsPath();
            Point[] psPS = new Point[]
                {
                    new Point(m_workingRect.Left + m_workingRect.Width / 4+10, m_workingRect.Top),
                    new Point(m_workingRect.Right - 1- m_workingRect.Width / 4-10, m_workingRect.Top),
                    new Point(m_workingRect.Right - 1 - m_workingRect.Width/4-10, m_workingRect.Top + 80),
                    new Point(m_workingRect.Left + m_workingRect.Width / 4+10, m_workingRect.Top + 80),

                };
            pathPS.AddLines(psPS);
            pathPS.CloseAllFigures();
            g.DrawPolygon(myPen, psPS);



            //画液体
            decimal decYTHeight = (((decimal)Convert.ToDouble(title.Length==0?"0.00":title)) / maxValue) * 80;
            GraphicsPath pathYT = new GraphicsPath();


            PointF[] psYT = new PointF[]
                    {
                        new Point(m_workingRect.Left + m_workingRect.Width / 4+11, (int)(m_workingRect.Top +80 -  decYTHeight)),
                        new Point(m_workingRect.Right - 1- m_workingRect.Width / 4-10, (int)(m_workingRect.Top +80 -  decYTHeight)),
                        new Point(m_workingRect.Right - 1 - m_workingRect.Width/4-10, m_workingRect.Top + 80),
                        new Point(m_workingRect.Left + m_workingRect.Width / 4+11, m_workingRect.Top + 80),
                    };
            pathYT.AddLines(psYT);
            pathYT.CloseAllFigures();
            new Rectangle(m_workingRect.Left, m_workingRect.Bottom - (int)decYTHeight - 5, m_workingRect.Width, 10);


            g.FillPath(new SolidBrush(liquidColor), pathYT);
            g.FillPath(new SolidBrush(Color.FromArgb(50, liquidColor)), pathYT);


            //画托盘
            GraphicsPath pathTP = new GraphicsPath();
            Point[] psTP = new Point[]
                {
                    new Point(m_workingRect.Left ,  m_workingRect.Top + 80 + 1),
                    new Point(m_workingRect.Right - 1, m_workingRect.Top + 80 + 1),
                    new Point(m_workingRect.Right -1, m_workingRect.Top+90 ),
                    new Point(m_workingRect.Left ,  m_workingRect.Top+90 ),

                };
            pathTP.AddLines(psTP);
            pathTP.CloseAllFigures();
            g.FillPath(new SolidBrush(Color.SlateBlue), pathTP);

            //画支柱1
            GraphicsPath pathZZ1 = new GraphicsPath();
            Point[] psZZ1 = new Point[]
                {
                    new Point(m_workingRect.Left + m_workingRect.Width / 3,  m_workingRect.Top + 90 ),
                    new Point(m_workingRect.Left + m_workingRect.Width / 3+5,m_workingRect.Top + 90 ),
                    new Point(m_workingRect.Left + m_workingRect.Width / 3+5,m_workingRect.Top + 100  ),
                    new Point(m_workingRect.Left + m_workingRect.Width / 3 ,  m_workingRect.Top + 100  ),

                };
            pathZZ1.AddLines(psZZ1);
            pathZZ1.CloseAllFigures();
            g.FillPath(new SolidBrush(Color.Black), pathZZ1);

            //画支柱2
            GraphicsPath pathZZ2 = new GraphicsPath();
            Point[] psZZ2 = new Point[]
                {
                    new Point(m_workingRect.Right - 1 - m_workingRect.Width/3 ,  m_workingRect.Top + 90 ),
                    new Point(m_workingRect.Right - 1 - m_workingRect.Width/3-5,m_workingRect.Top + 90 ),
                    new Point(m_workingRect.Right - 1 - m_workingRect.Width/3-5,m_workingRect.Top + 100  ),
                    new Point(m_workingRect.Right - 1 - m_workingRect.Width/3,  m_workingRect.Top + 100  ),

                };
            pathZZ2.AddLines(psZZ2);
            pathZZ2.CloseAllFigures();
            g.FillPath(new SolidBrush(Color.Black), pathZZ2);

            //画秤
            GraphicsPath pathC = new GraphicsPath();
            Point[] psC = new Point[]
                {
                    new Point(m_workingRect.Left + m_workingRect.Width / 4, m_workingRect.Top+100),
                    new Point(m_workingRect.Right - 1- m_workingRect.Width / 4, m_workingRect.Top+100),
                    new Point(m_workingRect.Right - 1- m_workingRect.Width / 20 , m_workingRect.Bottom-1),
                    new Point(m_workingRect.Left+m_workingRect.Width / 20 , m_workingRect .Bottom-1),

                };
            pathC.AddLines(psC);
            pathC.CloseAllFigures();
            g.DrawPolygon(myPen, psC);


            //画显示屏
            GraphicsPath pathXSP = new GraphicsPath();
            Point[] psXSP = new Point[]
                {
                    new Point(m_workingRect.Left + m_workingRect.Width / 4 ,  m_workingRect.Bottom-1-30),
                    new Point(m_workingRect.Right - m_workingRect.Width / 4,  m_workingRect.Bottom-1-30),
                    new Point(m_workingRect.Right - m_workingRect.Width / 4, m_workingRect.Bottom-1-5 ),
                    new Point(m_workingRect.Left + m_workingRect.Width / 4,  m_workingRect.Bottom-1-5 ),

                };
            pathXSP.AddLines(psXSP);
            pathXSP.CloseAllFigures();
            g.DrawPolygon(myPen, psXSP);

            //写编号
            if (!string.IsNullOrEmpty(m_NO))
            {
                var nosize = g.MeasureString(m_NO, Font);
                g.DrawString(m_NO, Font, new SolidBrush(ForeColor), new PointF((this.Width - nosize.Width) / 2, m_workingRect.Bottom - nosize.Height - 40));
            }
            //写读数
            var size = g.MeasureString(title, Font);
            var nosize1 = g.MeasureString(m_NO, Font);
            g.DrawString(title, new Font("宋体", (float)10.5, FontStyle.Bold), new SolidBrush(ForeColor), new PointF((this.Width - size.Width) / 2, m_workingRect.Bottom - 1 - 8 - nosize1.Height));

        }
    }
}
