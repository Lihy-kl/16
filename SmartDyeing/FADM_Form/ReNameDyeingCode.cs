using SmartDyeing.FADM_Control;
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
    public partial class ReNameDyeingCode : Form
    {
        int _i_nType = 0;
        string _s_code = "";
        List<myDyeSelect> myDyeSelectList = null;
        public ReNameDyeingCode(int type, string code, List<myDyeSelect> myDyeSelectList)
        {
            InitializeComponent();
            this._i_nType = type;
            _s_code = code;
            this.myDyeSelectList = myDyeSelectList;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try {
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
                    else
                    {
                        //新增新染固色代码

                        for (int i = 0; i < myDyeSelectList.Count - 1; i++)
                        {
                            myDyeSelect mySelect = myDyeSelectList[i];
                            if (mySelect.dy_type_comboBox1.Text == null || mySelect.dy_type_comboBox1.Text.Length == 0 || mySelect.dy_nodelist_comboBox2.Text == null || mySelect.dy_nodelist_comboBox2.Text.Length == 0)
                            {
                                continue;
                            }
                            //判断当前是染色还是后处理
                            s_sql = "SELECT Code  FROM dyeing_process WHERE Type = 1 and Code ='" + mySelect.dy_nodelist_comboBox2.Text.Trim() + "'  group by Code;";
                            DataTable dt_dyeingpro = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                            string s_type = "";
                            string s_step = "";
                            int i_nDye = 0;
                            int i_nHandle = 0;
                            if (dt_dyeingpro.Rows.Count != 0)
                            {
                                i_nDye++;
                                s_type = "1";
                                s_step = i_nDye.ToString();
                            }
                            else
                            {
                                i_nHandle++;
                                s_type = "2";
                                s_step = i_nHandle.ToString();
                            }

                            s_sql = "INSERT INTO dyeing_code (DyeingCode, Type," +
                                                       " Step, Code,IndexNum,IsUse) VALUES('" + txt_Name.Text.Trim() + "'," + s_type + "," + s_step + ",'" + mySelect.dy_nodelist_comboBox2.Text.Trim() + "'," + (i + 1).ToString() + ",1);";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                    }
                }
                this.Close();
                this.DialogResult = DialogResult.OK;

            } catch (Exception ex) {
                FADM_Form.CustomMessageBox.Show("保存异常，请重新输入",
                                            "温馨提示", MessageBoxButtons.OK, false);

            }

           
        }

        private void ReNameDyeingCode_KeyDown(object sender, KeyEventArgs e)
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
