using SmartDyeing.FADM_Object;
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
    public partial class Abort : Form
    {
        //测试下
        //再来一次
        public Abort()
        {
            InitializeComponent();
        }

        private void Abort_Load(object sender, EventArgs e)
        {
            string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
            string s_name = Lib_File.Ini.GetIni("info", "Name", "广州科联精密机器有限公司", s_path);
            label6.Text = s_name;
            string s_tel = Lib_File.Ini.GetIni("info", "Tel", "18620114477", s_path);
            label8.Text = s_tel;

            label2.Text = Communal._s_plcVersion;
        }


        public string getVersion() {
            return this.label4.Text;
        }
    }
}
