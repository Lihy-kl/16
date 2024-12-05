using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public partial class InsertAbs : Form
    {
        public InsertAbs()
        {
            InitializeComponent();
        }

        private void BtnBottleCheckStart_Click(object sender, EventArgs e)
        {
            try
            {
                //FADM_Object.Communal._fadmSqlserver.ReviseData("TRUNCATE TABLE abs_wait_list");

                char[] ca_chars = { ',', '，' };
                string[] sa_strings = TxtCheckBottleNo.Text.Split(ca_chars);

                char[] ca_chars1 = { '-', '-' };
                List<int> lis_ints = new List<int>();
                foreach (string s in sa_strings)
                {
                    string[] sa_strings1 = s.Split(ca_chars1);
                    if (1 == sa_strings1.Length)
                        lis_ints.Add(Convert.ToInt16(s));
                    else
                        for (int i = Convert.ToInt16(sa_strings1[0]); i <= Convert.ToInt16(sa_strings1[1]); i++)
                            lis_ints.Add(i);

                }

                lis_ints.Sort();
                lis_ints = lis_ints.ToList();

                foreach (int i in lis_ints)
                {
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                        "SELECT * FROM bottle_details WHERE BottleNum = " + i + ";");
                    if (0 == dt_data.Rows.Count || i > Lib_Card.Configure.Parameter.Machine_Bottle_Total)
                        continue;
                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "INSERT INTO abs_wait_list(BottleNum, InsertDate,Type) VALUES('" + i + "','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+"',0);");
                    Thread.Sleep(10);
                }

                FADM_Form.CustomMessageBox.Show("启动成功", "InsertAbs",
                    MessageBoxButtons.OK, false);



            }
            catch (Exception ex)
            {
                //if (Lib_Card.Configure.Parameter.Other_Language == 0)
                //    FADM_Form.CustomMessageBox.Show(ex.Message, "BottleCheck",
                //    MessageBoxButtons.OK, false);
                //else
                //{
                //    string s_mes = ex.Message;
                //    if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                //    {
                //        //如果存在就替换英文
                //        s_mes = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                //    }
                //    FADM_Form.CustomMessageBox.Show(s_mes, "BottleCheck",
                //    MessageBoxButtons.OK, false);
                //}
            }
        }

        private void BtnBottleCheckPause_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.ReviseData("TRUNCATE TABLE abs_wait_list");
        }
    }
}
