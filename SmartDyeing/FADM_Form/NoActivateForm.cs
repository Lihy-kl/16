using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public class NoActivateForm : Form
    {
        
        protected override bool ShowWithoutActivation => true;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WM_MOUSEACTIVATE = 0x21;
        public ListBox lb_End_Stations;
        private const int MA_NOACTIVATE = 3;

        public NoActivateForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Selectable, false);

            lb_End_Stations.DrawMode = DrawMode.OwnerDrawVariable;
            lb_End_Stations.MeasureItem += new MeasureItemEventHandler(lb_End_Stations_MeasureItem);
            lb_End_Stations.DrawItem += Lb_End_Stations_DrawItem;
        }

        private void Lb_End_Stations_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            if (e.Index >= 0)
            {
                string text = lb_End_Stations.Items[e.Index].ToString();
                using (Brush brush = new SolidBrush(e.ForeColor))
                {
                    // 获取文本的大小
                    SizeF textSize = e.Graphics.MeasureString(text, e.Font);
                    // 计算文本的起始位置，使其在项中居中
                    float x = e.Bounds.Left + (e.Bounds.Width - textSize.Width) / 2;
                    float y = e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2;
                    e.Graphics.DrawString(text, e.Font, brush, x,y);
                }
            }
        }

        private void lb_End_Stations_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 21;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE;
                return cp;
            }
        }

        [DebuggerStepThrough]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg.Equals(WM_MOUSEACTIVATE))
            {
                m.Result = new IntPtr(MA_NOACTIVATE);
                return;
            }
            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.lb_End_Stations = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb_End_Stations
            // 
            this.lb_End_Stations.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_End_Stations.FormattingEnabled = true;
            this.lb_End_Stations.ItemHeight = 23;
            this.lb_End_Stations.Location = new System.Drawing.Point(2, 3);
            this.lb_End_Stations.Margin = new System.Windows.Forms.Padding(4);
            this.lb_End_Stations.Name = "lb_End_Stations";
            this.lb_End_Stations.Size = new System.Drawing.Size(274, 164);
            this.lb_End_Stations.TabIndex = 27;
            // 
            // NoActivateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(289, 180);
            this.Controls.Add(this.lb_End_Stations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "NoActivateForm";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }
       
    }
}
