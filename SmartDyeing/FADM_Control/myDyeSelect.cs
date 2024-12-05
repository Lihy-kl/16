using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmartDyeing.FADM_Control
{
    public partial class myDyeSelect : UserControl
    {
        public myDyeSelect()
        {
            InitializeComponent();
            dy_type_comboBox1.MouseWheel += new MouseEventHandler(comboBox1_MouseWheel);
            dy_nodelist_comboBox2.MouseWheel += new MouseEventHandler(comboBox2_MouseWheel);
            dy_type_comboBox1.KeyPress += comboBox1KeyPress;
            dy_nodelist_comboBox2.KeyPress += comboBox2KeyPress;
        }
        private void comboBox1KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void comboBox2KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
    }
}
