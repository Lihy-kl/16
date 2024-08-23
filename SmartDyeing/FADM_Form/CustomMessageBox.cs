using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SmartDyeing.FADM_Form
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox(string message, string Caption, MessageBoxButtons messageBoxButtons)
        {
            InitializeComponent();

            this.Text = Caption;
            this.label1.Text = message;
            switch (messageBoxButtons)
            {
                case MessageBoxButtons.OK:
                    this.Btn_No.Visible = false;
                    this.Btn_Yes.Visible = false;

                    break;
                case MessageBoxButtons.YesNo:
                    this.Btn_OK.Visible = false;
                    break;

                default:
                    return;
            }

        }

        public static DialogResult Show(string message, string Caption, MessageBoxButtons buttons,bool Insert)
        {
            CustomMessageBox cmb = new CustomMessageBox(message, Caption, buttons);
            cmb.ShowDialog();
            using ( cmb )
            {
                if(Insert)
                {
                    string s_str = "INSERT INTO alarm_table" +
                                    "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                    " VALUES( '" +
                                    String.Format("{0:d}", DateTime.Now) + "','" +
                                    String.Format("{0:T}", DateTime.Now) + "','" +
                                    Caption + "','" +
                                    message + "(" + cmb.DialogResult.ToString() + ")');";

                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                }
                
                return cmb.DialogResult;
            }
            
            
        }

        private void Btn_Yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Btn_No_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
