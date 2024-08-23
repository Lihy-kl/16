﻿using System;
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
    public partial class Cup : UserControl
    {
        //杯身区域
        Rectangle m_workingRect;

        string title = "";
        [Description("配方名称"), Category("自定义")]
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


        public Color cupColor = Color.Black;
        [Description("杯子颜色"), Category("自定义")]
        public Color BottleColor
        {
            get { return cupColor; }
            set
            {
                cupColor = value;
                Refresh();
            }
        }

        private Color liquidColor = Color.Transparent;
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


        public decimal maxValue = 1;
        [Description("最大值"), Category("自定义")]
        public decimal MaxValue
        {
            get { return maxValue; }
            set
            {
                if (value < m_value)
                    return;
                maxValue = value;
                Refresh();
            }
        }


        private decimal m_value = 0;
        [Description("值"), Category("自定义")]
        public decimal Value
        {
            get { return m_value; }
            set
            {
                if (value < 0)
                {
                    return;
                }
                if (value > maxValue)
                {
                    m_value = maxValue;
                }
                m_value = value;
                Refresh();
            }
        }


        private string m_NO = "1";
        [Description("编号"), Category("自定义")]
        public string NO
        {
            get { return m_NO; }
            set
            {
                m_NO = value;
                Refresh();
            }
        }


        public Cup()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.SizeChanged += UCBottle_SizeChanged;
            this.Size = new Size(40, 80);
            this.BackColor = Color.Transparent;

        }


        void UCBottle_SizeChanged(object sender, EventArgs e)
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

            //画空杯子
            GraphicsPath pathPS = new GraphicsPath();
            Pen myPen = new Pen(cupColor, 1);
            Point[] psPS = new Point[]
                {
                    new Point(m_workingRect.Left , m_workingRect.Top),
                    new Point(m_workingRect.Right -1, m_workingRect.Top),
                    new Point(m_workingRect.Right -1, m_workingRect.Bottom),
                    new Point(m_workingRect.Left , m_workingRect.Bottom),
                };
            pathPS.AddLines(psPS);
            pathPS.CloseAllFigures();
            g.DrawPolygon(myPen, psPS);

            //画液体
            decimal decYTHeight = (m_value / maxValue) * m_workingRect.Height;
            GraphicsPath pathYT = new GraphicsPath();
            PointF[] psYT = new PointF[]
                    {
                        new PointF(m_workingRect.Left+1,(float)(m_workingRect.Bottom-decYTHeight)),
                        new PointF(m_workingRect.Right-1,(float)(m_workingRect.Bottom-decYTHeight)),
                        new PointF(m_workingRect.Right-1,m_workingRect.Bottom),
                        new PointF(m_workingRect.Left+1,m_workingRect.Bottom),
                    };
            pathYT.AddLines(psYT);
            pathYT.CloseAllFigures();
            new Rectangle(m_workingRect.Left, m_workingRect.Bottom - (int)decYTHeight - 5, m_workingRect.Width, 10);


            g.FillPath(new SolidBrush(liquidColor), pathYT);
            g.FillPath(new SolidBrush(Color.FromArgb(50, liquidColor)), pathYT);

            //画瓶口
            GraphicsPath pathBM = new GraphicsPath();
            Point[] psBM = new Point[]
                {
                    new Point(m_workingRect.Left ,  m_workingRect.Top - 8 + 1),
                    new Point(m_workingRect.Right-1 , m_workingRect.Top - 8 + 1),
                    new Point(m_workingRect.Right-1, m_workingRect.Top ),
                    new Point(m_workingRect.Left ,  m_workingRect.Top ),

                };
            pathBM.AddLines(psBM);
            pathBM.CloseAllFigures();
            g.DrawPolygon(myPen, psBM);

            //写编号
            if (!string.IsNullOrEmpty(m_NO))
            {
                var nosize = g.MeasureString(m_NO, Font);
                g.DrawString(m_NO, Font, new SolidBrush(ForeColor), new PointF((this.Width - nosize.Width) / 2, m_workingRect.Top + 10));
            }
            //写文字
            var size = g.MeasureString(title, Font);
            string s = null;
            if (size.Width > this.Width)
            {
                string s1 = null;
                foreach (char c in title)
                {
                    s1 += c;
                    var sz = g.MeasureString(s1, Font);
                    if (sz.Width > this.Width)
                    {
                        s1 = s1.Remove(s1.Length - 1);
                        s += (s1 + "\n");
                        s1 = null;
                        s1 += c;
                    }

                }
                s += s1;
            }
            else
            {
                s = title;
            }
            size = g.MeasureString(s, Font);
            var nosize1 = g.MeasureString(m_NO, Font);
            g.DrawString(s, Font, new SolidBrush(ForeColor), new PointF((this.Width - size.Width) / 2, m_workingRect.Top + nosize1.Height + 15));

        }

    }
}
