using SmartDyeing.FADM_Control;
using System;
using System.Data;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmartDyeing.FADM_Form
{
    public partial class DyeingStep : Form
    {
        //定义调液代码
        private string _s_code = null;
        private string _s_remark = null;

        //定义是添加还是修改:true:添加;false:修改
        private bool _b_insertOrUpdate = false;

        //定义调液流程控件
        DyeingProcess _page_DyeingProcess = null;
        DyeingConfiguration _page_dyeingConfiguration = null;
        PostTreatmentConfiguration _page_postTreatmentConfiguration = null;
        private int _i_type;

        public DyeingStep(int i_type, string s_stepNum, string s_technologyName, string s_proportionOrTime, string s_temp, string s_rate, string s_code, string s_rev, string s_remark, bool b_insertOrUpdate, DyeingProcess _Page_DyeingProcess, DyeingConfiguration _page_dyeingConfiguration, PostTreatmentConfiguration _page_postTreatmentConfiguration)
        {
            InitializeComponent();
            txt_StepNum.Text = s_stepNum;
            cbo_TechnologyName.Text = s_technologyName;
            txt_ProportionOrTime.Text = s_proportionOrTime;
            txt_Temp.Text = s_temp;
            txt_Rate.Text = s_rate;
            _s_code = s_code;
            _s_remark = s_remark;
            _b_insertOrUpdate = b_insertOrUpdate;
            _page_DyeingProcess = _Page_DyeingProcess; 
            this._page_dyeingConfiguration = _page_dyeingConfiguration;
            this._page_postTreatmentConfiguration = _page_postTreatmentConfiguration;
            _i_type = i_type;
            txt_Rev.Text = s_rev;

            if (txt_Rev.Enabled)
            {
                if (txt_Rev.Text == "" || txt_Rev.Text == null)
                {
                    txt_Rev.Text = "0";
                }
            }

            

            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "txt_Rate")
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }

            if (_b_insertOrUpdate)
            {
                //检查步号
                string s_sql = "SELECT StepNum FROM dyeing_process WHERE Code = '" + _s_code + "'  ORDER BY StepNum";
                DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                int i_step = 1;
                foreach (DataRow dr in dt_data.Rows)
                {
                    if (dr[0] != System.DBNull.Value)
                    {
                        int i_stepnow = Convert.ToInt16(dr[0]);
                        if (i_step != i_stepnow)
                        {
                            break;
                        }
                        i_step++;
                    }
                    else
                    { break; }

                }

                txt_StepNum.Text = i_step.ToString();
            }

            cbo_TechnologyName.Focus();
            if (i_type == 1)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    cbo_TechnologyName.Items.Remove("加水");
                }
                else
                {
                    cbo_TechnologyName.Items.Remove("Add Water");
                }
            }

            if (!FADM_Object.Communal._b_isShowSample)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    cbo_TechnologyName.Items.Remove("取小样");
                    cbo_TechnologyName.Items.Remove("测PH");
                }
                else
                {
                    cbo_TechnologyName.Items.Remove("Sample");
                    cbo_TechnologyName.Items.Remove("Test PH");
                }
            }

        }

        //输入检查
        void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberTextbox_KeyPress(e);
        }

        //输入检查
        void TextBox_KeyPress1(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                if (cbo_TechnologyName.Text == "冷行" || cbo_TechnologyName.Text == "搅拌")
                {
                    if (txt_ProportionOrTime.Text == "0")
                    {
                        FADM_Form.CustomMessageBox.Show("时间不能为0，请重新编辑！", "操作异常", MessageBoxButtons.OK, false);
                        return;
                    }
                }
                if (cbo_TechnologyName.Text == "温控")
                {
                    if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                        cbo_TechnologyName.Text != null && txt_ProportionOrTime.Text != "" && txt_ProportionOrTime.Text != null &&
                        txt_Temp.Text != "" && txt_Temp.Text != null && txt_Rate.Text != null && txt_Rate.Text != "" && txt_Rev.Text != null && txt_Rev.Text != "")
                    {
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = null;
                            if (_s_remark != "")
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                                   " ProportionOrTime, Code,Temp,Rate,Type,Rev,Remark) VALUES('" + txt_StepNum.Text + "'," +
                                                   "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "','" + _s_code + "','" + txt_Temp.Text + "','" + txt_Rate.Text + "'," + _i_type + "," + txt_Rev.Text + ",'" + _s_remark + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }
                            else
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                                   " ProportionOrTime, Code,Temp,Rate,Type,Rev) VALUES('" + txt_StepNum.Text + "'," +
                                                   "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "','" + _s_code + "','" + txt_Temp.Text + "','" + txt_Rate.Text + "'," + _i_type + "," + txt_Rev.Text + ");";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }

                            //删除新增为空的数据
                            s_sql = "delete from dyeing_process where  StepNum is null and Code = '" + _s_code + "'; ";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE dyeing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "'," +
                                               " ProportionOrTime = '" + txt_ProportionOrTime.Text + "', Temp = '" + txt_Temp.Text + "', Rate='" + txt_Rate.Text + "', Rev='" + txt_Rev.Text + "' WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND Code = '" + _s_code + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                        if (_i_type == 1)
                        {
                            _page_dyeingConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        else
                        {
                            _page_postTreatmentConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        //page_DyeingProcess.ChildDyeDataShow(txt_StepNum.Text);


                        this.Close();
                    }
                    else
                    {
                        FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                    }
                }

                else if (cbo_TechnologyName.Text == "放布" || cbo_TechnologyName.Text == "出布" || cbo_TechnologyName.Text == "取小样" || cbo_TechnologyName.Text == "测PH")
                {
                    if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                        cbo_TechnologyName.Text != null)
                    {
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = null;
                            if (_s_remark != "")
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " Code,Type,Remark) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + ",'" + _s_remark + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }
                            else
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " Code,Type) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + ");";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }

                            //删除新增为空的数据
                            s_sql = "delete from dyeing_process where  StepNum is null and Code = '" + _s_code + "'; ";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE dyeing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "' ,ProportionOrTime = null,Temp=null,Rate=null,Rev=null " +
                                               " WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND Code = '" + _s_code + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                        if (_i_type == 1)
                        {
                            _page_dyeingConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        else
                        {
                            _page_postTreatmentConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        //page_DyeingProcess.ChildDyeDataShow(txt_StepNum.Text);


                        this.Close();
                    }
                    else
                    {
                        FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                    }
                }
                else if (cbo_TechnologyName.Text == "排液")
                {
                    if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                        cbo_TechnologyName.Text != null && txt_Rev.Text != null && txt_Rev.Text != "")
                    {
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = null;
                            if (_s_remark != "")
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " Code,Type,Rev,Remark) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + "," + txt_Rev.Text + ",'" + _s_remark + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }
                            else
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " Code,Type,Rev) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + "," + txt_Rev.Text + ");";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }

                            //删除新增为空的数据
                            s_sql = "delete from dyeing_process where  StepNum is null and Code = '" + _s_code + "'; ";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE dyeing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "', Rev='" + txt_Rev.Text + "' ,ProportionOrTime = null,Temp=null,Rate=null " +
                                               " WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND Code = '" + _s_code + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        if (_i_type == 1)
                        {
                            _page_dyeingConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        else
                        {
                            _page_postTreatmentConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        //page_DyeingProcess.ChildDyeDataShow(txt_StepNum.Text);


                        this.Close();
                    }
                    else
                    {
                        FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                    }
                }
                else
                {
                    if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                        cbo_TechnologyName.Text != null && txt_ProportionOrTime.Text != "" && txt_ProportionOrTime.Text != null && txt_Rev.Text != null && txt_Rev.Text != "")
                    {
                        if(Convert.ToInt32(txt_ProportionOrTime.Text) <=0)
                        {
                            FADM_Form.CustomMessageBox.Show("时间/比例不能为0，请重新编辑！", "操作异常", MessageBoxButtons.OK, false);
                            return;
                        }
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = null;
                            if (_s_remark != "")
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                              " ProportionOrTime, Code,Type,Rev,Remark) VALUES('" + txt_StepNum.Text + "'," +
                                              "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "'," +
                                              "'" + _s_code + "'," + _i_type + "," + txt_Rev.Text + ",'" + _s_remark + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }
                            else
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " ProportionOrTime, Code,Type,Rev) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + "," + txt_Rev.Text + ");";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }

                            //删除新增为空的数据
                            s_sql = "delete from dyeing_process where  StepNum is null and Code = '" + _s_code + "'; ";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE dyeing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "'," +
                                               " ProportionOrTime = '" + txt_ProportionOrTime.Text + "', Rev='" + txt_Rev.Text + "' ,Temp=null,Rate=null  WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND Code = '" + _s_code + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                        if (_i_type == 1)
                        {
                            _page_dyeingConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        else
                        {
                            _page_postTreatmentConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        //page_DyeingProcess.ChildDyeDataShow(txt_StepNum.Text);


                        this.Close();
                    }
                    else
                    {
                        FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                    }
                }
            }
            else
            {
                if (cbo_TechnologyName.Text == "Cool line" || cbo_TechnologyName.Text == "Stir")
                {
                    if (txt_ProportionOrTime.Text == "0")
                    {
                        FADM_Form.CustomMessageBox.Show("Time cannot be 0, please edit again！", "Abnormal operation", MessageBoxButtons.OK, false);
                        return;
                    }
                }
                if (cbo_TechnologyName.Text == "Temperature control")
                {
                    if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                        cbo_TechnologyName.Text != null && txt_ProportionOrTime.Text != "" && txt_ProportionOrTime.Text != null &&
                        txt_Temp.Text != "" && txt_Temp.Text != null && txt_Rate.Text != null && txt_Rate.Text != "" && txt_Rev.Text != null && txt_Rev.Text != "")
                    {
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = null;
                            if (_s_remark != "")
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                                   " ProportionOrTime, Code,Temp,Rate,Type,Rev,Remark) VALUES('" + txt_StepNum.Text + "'," +
                                                   "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "','" + _s_code + "','" + txt_Temp.Text + "','" + txt_Rate.Text + "'," + _i_type + "," + txt_Rev.Text + ",'" + _s_remark + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }
                            else
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                                   " ProportionOrTime, Code,Temp,Rate,Type,Rev) VALUES('" + txt_StepNum.Text + "'," +
                                                   "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "','" + _s_code + "','" + txt_Temp.Text + "','" + txt_Rate.Text + "'," + _i_type + "," + txt_Rev.Text + ");";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }

                            //删除新增为空的数据
                            s_sql = "delete from dyeing_process where  StepNum is null and Code = '" + _s_code + "'; ";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE dyeing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "'," +
                                               " ProportionOrTime = '" + txt_ProportionOrTime.Text + "', Temp = '" + txt_Temp.Text + "', Rate='" + txt_Rate.Text + "', Rev='" + txt_Rev.Text + "' WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND Code = '" + _s_code + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                        if (_i_type == 1)
                        {
                            _page_dyeingConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        else
                        {
                            _page_postTreatmentConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        //page_DyeingProcess.ChildDyeDataShow(txt_StepNum.Text);


                        this.Close();
                    }
                    else
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please complete the documentation before saving it！", "Abnormal operation", MessageBoxButtons.OK, false);
                    }
                }

                else if (cbo_TechnologyName.Text == "Entering the fabric" || cbo_TechnologyName.Text == "Outgoing fabric" || cbo_TechnologyName.Text == "Sample" || cbo_TechnologyName.Text == "Test PH")
                {
                    if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                        cbo_TechnologyName.Text != null)
                    {
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = null;
                            if (_s_remark != "")
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " Code,Type,Remark) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + ",'" + _s_remark + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }
                            else
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " Code,Type) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + ");";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }

                            //删除新增为空的数据
                            s_sql = "delete from dyeing_process where  StepNum is null and Code = '" + _s_code + "'; ";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE dyeing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "' ,ProportionOrTime = null,Temp=null,Rate=null,Rev=null " +
                                               " WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND Code = '" + _s_code + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                        if (_i_type == 1)
                        {
                            _page_dyeingConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        else
                        {
                            _page_postTreatmentConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        //page_DyeingProcess.ChildDyeDataShow(txt_StepNum.Text);


                        this.Close();
                    }
                    else
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please complete the documentation before saving it！", "Abnormal operation", MessageBoxButtons.OK, false);
                    }
                }
                else if (cbo_TechnologyName.Text == "Drainage")
                {
                    if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                        cbo_TechnologyName.Text != null && txt_Rev.Text != null && txt_Rev.Text != "")
                    {
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = null;
                            if (_s_remark != "")
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " Code,Type,Rev,Remark) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + "," + txt_Rev.Text + ",'" + _s_remark + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }
                            else
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " Code,Type,Rev) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + "," + txt_Rev.Text + ");";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }

                            //删除新增为空的数据
                            s_sql = "delete from dyeing_process where  StepNum is null and Code = '" + _s_code + "'; ";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE dyeing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "', Rev='" + txt_Rev.Text + "' ,ProportionOrTime = null,Temp=null,Rate=null " +
                                               " WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND Code = '" + _s_code + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        if (_i_type == 1)
                        {
                            _page_dyeingConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        else
                        {
                            _page_postTreatmentConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        //page_DyeingProcess.ChildDyeDataShow(txt_StepNum.Text);


                        this.Close();
                    }
                    else
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please complete the documentation before saving it！", "Abnormal operation", MessageBoxButtons.OK, false);
                    }
                }
                else
                {
                    if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                        cbo_TechnologyName.Text != null && txt_ProportionOrTime.Text != "" && txt_ProportionOrTime.Text != null && txt_Rev.Text != null && txt_Rev.Text != "")
                    {
                        if (Convert.ToInt32(txt_ProportionOrTime.Text) <= 0)
                        {
                            FADM_Form.CustomMessageBox.Show("Time or ratio cannot be Zero, please edit again", "Abnormal operation", MessageBoxButtons.OK, false);
                            return;
                        }
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = null;
                            if (_s_remark != "")
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                              " ProportionOrTime, Code,Type,Rev,Remark) VALUES('" + txt_StepNum.Text + "'," +
                                              "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "'," +
                                              "'" + _s_code + "'," + _i_type + "," + txt_Rev.Text + ",'" + _s_remark + "');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }
                            else
                            {
                                s_sql = "INSERT INTO dyeing_process (StepNum, TechnologyName," +
                                               " ProportionOrTime, Code,Type,Rev) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "'," +
                                               "'" + _s_code + "'," + _i_type + "," + txt_Rev.Text + ");";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                            }

                            //删除新增为空的数据
                            s_sql = "delete from dyeing_process where  StepNum is null and Code = '" + _s_code + "'; ";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE dyeing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "'," +
                                               " ProportionOrTime = '" + txt_ProportionOrTime.Text + "', Rev='" + txt_Rev.Text + "' ,Temp=null,Rate=null  WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND Code = '" + _s_code + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }

                        if (_i_type == 1)
                        {
                            _page_dyeingConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        else
                        {
                            _page_postTreatmentConfiguration.ChildDyeDataShow(txt_StepNum.Text);
                        }
                        //page_DyeingProcess.ChildDyeDataShow(txt_StepNum.Text);


                        this.Close();
                    }
                    else
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please complete the documentation before saving it！", "Abnormal operation", MessageBoxButtons.OK, false);
                    }
                }
            }

        }

        private void cbo_TechnologyName_SelectedIndexChanged(object sender, EventArgs e)
        {

            //放布
            //冷行
            //温控
            //加A
            //加B
            //加C
            //加D
            //加E
            //加水
            //搅拌
            //排液
            //出布
            //洗杯
            if (cbo_TechnologyName.Text == "放布"  || cbo_TechnologyName.Text == "出布" || cbo_TechnologyName.Text == "取小样" || cbo_TechnologyName.Text == "测PH"
                || cbo_TechnologyName.Text == "Entering the fabric" || cbo_TechnologyName.Text == "Outgoing fabric"
                || cbo_TechnologyName.Text == "Sample" || cbo_TechnologyName.Text == "Test PH")
            {
                txt_ProportionOrTime.Enabled = false;
                txt_Rate.Enabled = false;
                txt_Temp.Enabled = false;
                txt_Rev.Enabled = false;
                txt_Rev.Text = "";
            }
            else if ( cbo_TechnologyName.Text == "排液" || cbo_TechnologyName.Text == "Drainage")
            {
                txt_ProportionOrTime.Enabled = false;
                txt_Rate.Enabled = false;
                txt_Temp.Enabled = false;
                txt_Rev.Enabled = true;
            }
            else if (cbo_TechnologyName.Text == "温控" || cbo_TechnologyName.Text == "Temperature control")
            {
                txt_ProportionOrTime.Enabled = true;
                txt_Rate.Enabled = true;
                txt_Temp.Enabled = true;
                txt_Rev.Enabled = true;
            }
            else
            {
                txt_ProportionOrTime.Enabled = true;
                txt_Rate.Enabled = false;
                txt_Temp.Enabled = false;
                txt_Rev.Enabled = true;
            }

            if(txt_Rev.Enabled)
            {
                if(txt_Rev.Text == ""|| txt_Rev.Text == null)
                {
                    txt_Rev.Text = "0";
                }
            }
        }

        private void cbo_TechnologyName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (cbo_TechnologyName.Text != "")
                    {
                        if (txt_Temp.Enabled)
                        {
                            txt_Temp.Focus();
                        }
                        else if (txt_ProportionOrTime.Enabled)
                        {
                            txt_ProportionOrTime.Focus();
                        }
                        else if (txt_Rev.Enabled)
                        {
                            txt_Rev.Focus();
                        }
                        else
                        {
                            btn_Save.Focus();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void txt_Temp_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txt_Temp.Text != "")
                    {
                        if (txt_Rate.Enabled)
                        {
                            txt_Rate.Focus();
                        }
                        else
                        {
                            txt_ProportionOrTime.Focus();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void txt_Rate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txt_Rate.Text != "")
                    {
                        txt_ProportionOrTime.Focus();
                    }
                    break;
                default:
                    break;
            }
        }

        private void txt_ProportionOrTime_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txt_ProportionOrTime.Text != "")
                    {
                        if (txt_Rev.Enabled)
                        {
                            txt_Rev.Focus();
                        }
                        else
                        {
                            btn_Save.Focus();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void DyeingStep_Load(object sender, EventArgs e)
        {
            cbo_TechnologyName.Focus();
        }

        private void txt_Rev_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_Rev_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txt_Rev.Text != "")
                    {
                        btn_Save.Focus();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
