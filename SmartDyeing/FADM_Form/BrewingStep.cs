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
    public partial class BrewingStep : Form
    {
        //定义调液代码
        private string _s_brewingCode = null;

        //定义是添加还是修改:true:添加;false:修改
        private bool _b_insertOrUpdate = false;

        //定义调液流程控件
        BrewingProcess _page_BrewingProcess = null;


        public BrewingStep(string s_stepNum, string s_technologyName, string s_proportionOrTime, string s_ratio, string s_brewingCode, bool b_insertOrUpdate, BrewingProcess _Page_BrewingProcess)
        {
            InitializeComponent();
            txt_StepNum.Text = s_stepNum;
            cbo_TechnologyName.Text = s_technologyName;
            txt_ProportionOrTime.Text = s_proportionOrTime;
            txt_Ratio.Text = s_ratio;
            _s_brewingCode = s_brewingCode;
            _b_insertOrUpdate = b_insertOrUpdate;
            _page_BrewingProcess = _Page_BrewingProcess;
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.KeyPress += TextBox_KeyPress;
                }
            }

            if (_b_insertOrUpdate)
            {
                //检查步号
                string s_sql = "SELECT StepNum FROM brewing_process WHERE BrewingCode = '" + _s_brewingCode + "' ORDER BY StepNum";
                DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                int i_step = 1;
                foreach (DataRow dr in dt_data.Rows)
                {
                    int i_stepnow = Convert.ToInt16(dr[0]);
                    if (i_step != i_stepnow)
                    {
                        break;
                    }
                    i_step++;

                }

                txt_StepNum.Text = i_step.ToString();
            }
            if(s_technologyName == "加温水" || s_technologyName == "Add warm water")
            {
                txt_Ratio.Enabled = true;
            }
            else
            {
                txt_Ratio.Enabled = false;
            }

        }

        //输入检查
        void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberTextbox_KeyPress(e);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                if (cbo_TechnologyName.Text != "手动加染助剂" && cbo_TechnologyName.Text != "搅拌" && cbo_TechnologyName.Text != "加补充剂")
                {
                    string s_sql = "SELECT  SUM(ProportionOrTime) FROM brewing_process WHERE BrewingCode = '" + _s_brewingCode +
                                       "' AND TechnologyName != '手动加染助剂' AND TechnologyName != '搅拌' AND TechnologyName != '加补充剂' AND StepNum != '" + txt_StepNum.Text + "';";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    if (Convert.ToString(dt_data.Rows[0][dt_data.Columns[0]]) != "" && txt_ProportionOrTime.Text != null && txt_ProportionOrTime.Text != "")
                    {
                        if (Convert.ToInt16(txt_ProportionOrTime.Text) > 100 - Convert.ToInt16(dt_data.Rows[0][dt_data.Columns[0]]))
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("加水比例总量超出100，请重新输入！", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("The total amount of water added exceeds 100, please re-enter！", "Tips", MessageBoxButtons.OK, false);
                            txt_ProportionOrTime.Text = null;
                            return;
                        }

                    }
                }
            }
            else
            {
                if (cbo_TechnologyName.Text != "Add dyeing auxiliaries manually" && cbo_TechnologyName.Text != "Stir" && cbo_TechnologyName.Text != "Add supplements")
                {
                    string s_sql = "SELECT  SUM(ProportionOrTime) FROM brewing_process WHERE BrewingCode = '" + _s_brewingCode +
                                       "' AND TechnologyName != 'Add dyeing auxiliaries manually' AND TechnologyName != 'Stir' AND TechnologyName != 'Add supplements'  AND StepNum != '" + txt_StepNum.Text + "';";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    if (Convert.ToString(dt_data.Rows[0][dt_data.Columns[0]]) != "" && txt_ProportionOrTime.Text != null && txt_ProportionOrTime.Text != "")
                    {
                        if (Convert.ToInt16(txt_ProportionOrTime.Text) > 100 - Convert.ToInt16(dt_data.Rows[0][dt_data.Columns[0]]))
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("加水比例总量超出100，请重新输入！", "温馨提示", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("The total amount of water added exceeds 100, please re-enter！", "Tips", MessageBoxButtons.OK, false);
                            txt_ProportionOrTime.Text = null;
                            return;
                        }

                    }
                }
            }

            if (txt_StepNum.Text != "" && txt_StepNum.Text != null && cbo_TechnologyName.Text != "" &&
                cbo_TechnologyName.Text != null && txt_ProportionOrTime.Text != "" && txt_ProportionOrTime.Text != null)
            {
                if (cbo_TechnologyName.Text == "加温水" || cbo_TechnologyName.Text == "Add warm water")
                {
                    if(txt_StepNum.Text != "" && txt_StepNum.Text != null)
                    {
                        if (_b_insertOrUpdate)
                        {
                            string s_sql = "INSERT INTO brewing_process (StepNum, TechnologyName," +
                                               " ProportionOrTime, BrewingCode,Ratio) VALUES('" + txt_StepNum.Text + "'," +
                                               "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "'," +
                                               "'" + _s_brewingCode + "'," +
                                               "'" + txt_Ratio.Text + "');";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        else
                        {
                            string s_sql = "UPDATE brewing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "'," +
                                               " ProportionOrTime = '" + txt_ProportionOrTime.Text + "'," +
                                               " Ratio = '" + txt_Ratio.Text + "' WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                               " AND BrewingCode = '" + _s_brewingCode + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                    }
                    else
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please complete the documentation before saving it！", "Abnormal operation", MessageBoxButtons.OK, false);
                        return;
                    }
                }
                else
                {
                    if (_b_insertOrUpdate)
                    {
                        string s_sql = "INSERT INTO brewing_process (StepNum, TechnologyName," +
                                           " ProportionOrTime, BrewingCode) VALUES('" + txt_StepNum.Text + "'," +
                                           "'" + cbo_TechnologyName.Text + "','" + txt_ProportionOrTime.Text + "'," +
                                           "'" + _s_brewingCode + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }
                    else
                    {
                        string s_sql = "UPDATE brewing_process SET TechnologyName = '" + cbo_TechnologyName.Text + "'," +
                                           " ProportionOrTime = '" + txt_ProportionOrTime.Text + "' WHERE StepNum = '" + txt_StepNum.Text + "'" +
                                           " AND BrewingCode = '" + _s_brewingCode + "';";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }
                }

                _page_BrewingProcess.BrewingProcessShow(txt_StepNum.Text);

                this.Close();
            }
            else
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("请先编写完资料再保存！", "操作异常", MessageBoxButtons.OK,false);
                else
                    FADM_Form.CustomMessageBox.Show("Please complete the documentation before saving it！", "Abnormal operation", MessageBoxButtons.OK, false);
            }
        }

        private void cbo_TechnologyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbo_TechnologyName.Text == "加温水" || cbo_TechnologyName.Text == "Add warm water")
            {
                txt_Ratio.Enabled = true;
            }
            else
            {
                txt_Ratio.Enabled= false;
            }
        }
    }
}
