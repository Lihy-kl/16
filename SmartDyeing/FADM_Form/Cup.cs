using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public partial class Cup : Form
    {
        public Cup()
        {

            InitializeComponent();
            this.panel1.Controls.Add(new FADM_Control.CupListDetail());
        }
        //判断是否已打开
        public static bool _b_open = false;

        private void Cup_FormClosed(object sender, FormClosedEventArgs e)
        {
            _b_open = false;
            foreach (Control control in this.panel1.Controls)
            {
                this.panel1.Controls.Remove(control);
                control.Dispose();
            }
        }
    }
}
