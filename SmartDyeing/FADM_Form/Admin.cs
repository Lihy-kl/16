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
    public partial class Admin : Form
    {
        Main _main;
        public Admin(Main m)
        {
            InitializeComponent();

            _main = m;
        }

        private void BtnLogOn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtName.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("用户名或密码不能为空！", "登录异常", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("The username or password cannot be empty！", "Login exception", MessageBoxButtons.OK, false);

            }
            else
            {
                if (TxtName.Text == "admin" && TxtPassword.Text.ToLower() == "999")
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        _main.BtnUserSwitching.Text = "管理用户";
                    }
                    else
                    {
                        _main.BtnUserSwitching.Text = "Administrator";
                    }
                    FADM_Object.Communal._s_operator = "管理用户";
                    _main.BtnUserSwitching.Enabled = true;
                    _main.toolStripSplitButton1.Enabled = true;
                    //main.toolStripSplitButton6.Enabled = true;

                    this.Dispose();
                }
                else if (TxtName.Text == "eng" && TxtPassword.Text.ToLower() == "eng")
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        _main.BtnUserSwitching.Text = "工程师";
                    }
                    else
                    {
                        _main.BtnUserSwitching.Text = "Engineer";
                    }

                    _main.BtnUserSwitching.Enabled = true;
                    _main.toolStripSplitButton1.Enabled = true;
                    _main.toolStripSplitButton2.Enabled = true;
                    _main.p.Enabled = true;
                    FADM_Object.Communal._s_operator = "工程师";
                    this.Dispose();
                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("用户名或密码错误！", "登录异常", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Incorrect username or password！", "Login exception", MessageBoxButtons.OK, false);
                    TxtPassword.Text = null;
                }

            }
        }

        private void Admin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    BtnLogOn_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._main.BtnUserSwitching.Enabled = true;
        }
    }
}
