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
    public partial class ReName : Form
    {
        //nType:1为大工艺，2为中工艺
        int _i_nType = 0;
        string _s_code = "";
        public ReName(int type, string code)
        {
            InitializeComponent();
            this._i_nType = type;
            _s_code = code;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            if (txt_Name.Text == "")
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("请输入新名字", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Please enter a new name", "Tips", MessageBoxButtons.OK, false);

                return;
            }
            //查询新名字是否已存在
            if (_i_nType == 1)
            {
                string s_sql = "select COUNT(*) from dyeing_code where DyeingCode  = '" + txt_Name.Text + "'; ";
                DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_data.Rows[0][0].ToString() != "0")
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("新名字已存在，请重新输入",
                                        "温馨提示", MessageBoxButtons.OK, false))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("The new name already exists, please re-enter it",
                                        "Tips", MessageBoxButtons.OK, false))
                        {
                            return;
                        }
                    }
                }

                //P_str_sql = "select * from dyeing_code where DyeingCode  = '" + sCode + "' order by Type,Step; ";
                //P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                //foreach (DataRow dr in P_dt.Rows)
                //{
                //    P_str_sql = "_b_insert into dyeing_code (DyeingCode,Type,Step,Code) values ('" + txt_Name.Text + "','" + dr["Type"] + "','" + dr["Step"] + "','" + dr["Code"] + "');";
                //    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                //}
            }
            else if (_i_nType == 2)
            {
                string s_sql = "select COUNT(*) from dyeing_process where Code  = '" + txt_Name.Text + "'; ";
                DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_data.Rows[0][0].ToString() != "0")
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("新名字已存在，请重新输入",
                                        "温馨提示", MessageBoxButtons.OK, false))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("The new name already exists, please re-enter it",
                                        "Tips", MessageBoxButtons.OK, false))
                        {
                            return;
                        }
                    }
                }

                s_sql = "select * from dyeing_process where Code  = '" + _s_code + "' order by StepNum; ";
                dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                foreach (DataRow dr in dt_data.Rows)
                {
                    if (dr["TechnologyName"].ToString() == "温控"|| dr["TechnologyName"].ToString() == "Temperature control")
                    {
                        s_sql = "insert into dyeing_process (Code,Type,TechnologyName,ProportionOrTime,Temp,Rate,StepNum,Rev,Remark) values ('" + txt_Name.Text + "','" + dr["Type"] + "','" + dr["TechnologyName"] + "','" + dr["ProportionOrTime"] + "','" + dr["Temp"] + "','" + dr["Rate"] + "','" + dr["StepNum"] + "','" + dr["Rev"] + "','" + dr["Remark"] + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }
                    else if (dr["TechnologyName"].ToString() == "放布"  || dr["TechnologyName"].ToString() == "出布"||dr["TechnologyName"].ToString() == "Entering the fabric" || dr["TechnologyName"].ToString() == "Outgoing fabric")
                    {
                        s_sql = "insert into dyeing_process (Code,Type,TechnologyName,StepNum,Remark) values ('" + txt_Name.Text + "','" + dr["Type"] + "','" + dr["TechnologyName"] + "','" + dr["StepNum"] + "','" + dr["Remark"] + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }
                    else if (dr["TechnologyName"].ToString() == "排液" || dr["TechnologyName"].ToString() == "Drainage")
                    {
                        s_sql = "insert into dyeing_process (Code,Type,TechnologyName,StepNum,Rev,Remark) values ('" + txt_Name.Text + "','" + dr["Type"] + "','" + dr["TechnologyName"] + "','" + dr["StepNum"] + "','" + dr["Rev"] + "','" + dr["Remark"] + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }
                    else
                    {
                        s_sql = "insert into dyeing_process (Code,Type,TechnologyName,ProportionOrTime,StepNum,Rev,Remark) values ('" + txt_Name.Text + "','" + dr["Type"] + "','" + dr["TechnologyName"] + "','" + dr["ProportionOrTime"] + "','" + dr["StepNum"] + "','" + dr["Rev"] + "','" + dr["Remark"] + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }

                }
            }
            FADM_Object.Communal._s_reName = txt_Name.Text;
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void txt_Name_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        button1_Click(null, null);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
