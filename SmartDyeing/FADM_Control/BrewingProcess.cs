using SmartDyeing.FADM_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class BrewingProcess : UserControl
    {
        public BrewingProcess()
        {
            InitializeComponent();

            BrewingCodeShow("");
        }

        /// <summary>
        /// 显示调液流程代码
        /// </summary>
        /// <param name="s_brewingCode">选中的调液流程代码</param>
        /// <returns>0:正常;-1:异常</returns>
        private int BrewingCodeShow(string s_brewingCode)
        {
            try
            {
                //获取调液流程代码
                string s_sql = "SELECT *  FROM brewing_code;";
                DataTable dt_brewcode = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //捆绑
                dgv_BrewCode.DataSource = new DataView(dt_brewcode);

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题文字
                    dgv_BrewCode.Columns[0].HeaderCell.Value = "调液流程代码";

                }
                else
                {
                    dgv_BrewCode.Columns[0].HeaderCell.Value = "LiquidFlowCode";

                }

                //设置标题宽度
                dgv_BrewCode.Columns[0].Width = dgv_BrewCode.Width - 3;

                //关闭点击标题自动排序功能
                dgv_BrewCode.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dgv_BrewCode.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置标题字体
                dgv_BrewCode.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容居中显示
                dgv_BrewCode.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置内容字体
                dgv_BrewCode.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置行高
                dgv_BrewCode.RowTemplate.Height = 30;

                //设置当前选中行
                for (int i = 0; i < dgv_BrewCode.Rows.Count; i++)
                {
                    string s_temp = dgv_BrewCode.Rows[i].Cells[0].Value.ToString();
                    if (s_temp == s_brewingCode)
                    {
                        dgv_BrewCode.CurrentCell = dgv_BrewCode.Rows[i].Cells[0];
                        break;
                    }
                }

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 显示调液流程
        /// </summary>
        /// <param name="s_stepNum">当前选中的步号</param>
        /// <returns>0:正常;-1:异常</returns>
        public int BrewingProcessShow(string s_stepNum)
        {
            try
            {
                //获取当前调液代码的调液流程
                string s_sql = "SELECT StepNum,TechnologyName,ProportionOrTime,Ratio  FROM brewing_process WHERE" +
                                   " BrewingCode = '" + txt_BrewCode.Text + "' Order BY StepNum ; ";
                DataTable dt_brewprocess = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //捆绑
                dgv_BrewProcess.DataSource = new DataView(dt_brewprocess);

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题栏名称
                    string[] sa_lineName = { "步号", "操作类型", "百分比(%)/时间(s)", "热水占比(%)" };
                    for (int i = 0; i < 4; i++)
                    {
                        dgv_BrewProcess.Columns[i].HeaderCell.Value = sa_lineName[i];
                        //设置标题宽度
                        dgv_BrewProcess.Columns[i].Width = (dgv_BrewProcess.Width - 2) / 4;
                        //关闭点击标题自动排序功能
                        dgv_BrewProcess.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                else
                {
                    //设置标题栏名称
                    string[] sa_lineName = { "StepNumber", "OperationType", "Percentage(%)/time(s) ", "Hot Water Ratio(%)/ " };
                    for (int i = 0; i < 4; i++)
                    {
                        dgv_BrewProcess.Columns[i].HeaderCell.Value = sa_lineName[i];
                        //设置标题宽度
                        dgv_BrewProcess.Columns[i].Width = (dgv_BrewProcess.Width - 2) / 4;
                        //关闭点击标题自动排序功能
                        dgv_BrewProcess.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                }
                //设置标题居中显示
                dgv_BrewProcess.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置标题字体
                dgv_BrewProcess.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容居中显示
                dgv_BrewProcess.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置内容字体
                dgv_BrewProcess.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置行高
                dgv_BrewProcess.RowTemplate.Height = 30;

                //设置当前选中行
                for (int i = 0; i < dgv_BrewProcess.Rows.Count; i++)
                {
                    string s_temp = dgv_BrewProcess.Rows[i].Cells[0].Value.ToString();
                    if (s_temp == s_stepNum)
                    {
                        dgv_BrewProcess.CurrentCell = dgv_BrewProcess.Rows[i].Cells[0];
                        break;
                    }
                }

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private void btn_BrewingCodeAdd_Click(object sender, EventArgs e)
        {
            txt_BrewCode.Text = null;
            txt_BrewCode.Enabled = true;
            txt_BrewCode.Focus();
            btn_BrewingCodeDelete.Enabled = false;
            btn_BrewingProcessAdd.Enabled = false;
            btn_BrewingProcessDelete.Enabled = false;
            btn_BrewingProcessUpdate.Enabled = false;
        }

        private void btn_BrewingCodeDelete_Click(object sender, EventArgs e)
        {
            string s_sql = "DELETE FROM brewing_code" +
                               " WHERE BrewingCode = '" + txt_BrewCode.Text + "';";
            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            s_sql = "DELETE FROM brewing_process" +
                        " WHERE BrewingCode = '" + txt_BrewCode.Text + "';";
            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            try
            {
                BrewingCodeShow(dgv_BrewCode.Rows[dgv_BrewCode.CurrentCell.RowIndex - 1].Cells[0].Value.ToString());
            }
            catch
            {
                BrewingCodeShow("");
            }
        }

        private void btn_BrewingProcessAdd_Click(object sender, EventArgs e)
        {
            string s_stepNum = (dgv_BrewProcess.Rows.Count + 1).ToString();
            BrewingStep form_BrewingStep = new BrewingStep(s_stepNum, "", "", "", txt_BrewCode.Text, true, this);
            form_BrewingStep.Show();
        }

        private void btn_BrewingProcessUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string s_stepNum = dgv_BrewProcess.CurrentRow.Cells[0].Value.ToString();
                string s_technologyName = dgv_BrewProcess.CurrentRow.Cells[1].Value.ToString().Trim();
                string s_proportionOrTime = dgv_BrewProcess.CurrentRow.Cells[2].Value.ToString();
                string s_ratio = dgv_BrewProcess.CurrentRow.Cells[3].Value.ToString();
                BrewingStep form_BrewingStep = new BrewingStep(s_stepNum, s_technologyName, s_proportionOrTime, s_ratio, txt_BrewCode.Text, false, this);
                form_BrewingStep.Show();
            }
            catch
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("未发现可修改的行,请先添加！", "操作异常", MessageBoxButtons.OK,false);
                else
                    FADM_Form.CustomMessageBox.Show("No modifiable rows found, please add first！", "Abnormal operation", MessageBoxButtons.OK, false);
            }
        }

        private void btn_BrewingProcessDelete_Click(object sender, EventArgs e)
        {
            if (dgv_BrewProcess.SelectedRows.Count == 0)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("请先选择操作行", "操作异常", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Please select the operation line first", "Abnormal operation", MessageBoxButtons.OK, false);
                return;
            }

            string s_sql = "DELETE FROM brewing_process WHERE" +
                               " StepNum = '" + dgv_BrewProcess.CurrentRow.Cells[0].Value.ToString() + "'" +
                               " AND BrewingCode = '" + txt_BrewCode.Text + "' ;";
            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            try
            {
                BrewingProcessShow(dgv_BrewProcess.Rows[dgv_BrewProcess.CurrentCell.RowIndex - 1].Cells[0].Value.ToString());
            }
            catch
            {
                BrewingProcessShow("");
            }
        }

        private void dgv_BrewCode_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_BrewCode.CurrentCell != null)
                {
                    txt_BrewCode.Text = dgv_BrewCode.CurrentCell.Value.ToString();
                    BrewingProcessShow("");
                    btn_BrewingCodeDelete.Enabled = true;
                    btn_BrewingProcessAdd.Enabled = true;
                    btn_BrewingProcessDelete.Enabled = true;
                    btn_BrewingProcessUpdate.Enabled = true;
                }
            }
            catch
            {
                txt_BrewCode.Text = null;
                BrewingProcessShow("");
                btn_BrewingCodeDelete.Enabled = false;
                btn_BrewingProcessAdd.Enabled = false;
                btn_BrewingProcessDelete.Enabled = false;
                btn_BrewingProcessUpdate.Enabled = false;
            }
        }

        private void txt_BrewCode_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txt_BrewCode.Enabled && txt_BrewCode.Text != null && txt_BrewCode.Text != "")
                    {
                        //检索是否存在这个调液流程代码
                        for (int i = 0; i < dgv_BrewCode.Rows.Count; i++)
                        {
                            if (txt_BrewCode.Text == dgv_BrewCode[0, i].Value.ToString())
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                {
                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("调液流程代码重复,请重新输入",
                                    "温馨提示", MessageBoxButtons.OK, false))
                                    {
                                        txt_BrewCode.Text = null;
                                        txt_BrewCode.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("The mixing process code is duplicated, please re-enter it",
                                    "Tips", MessageBoxButtons.OK, false))
                                    {
                                        txt_BrewCode.Text = null;
                                        txt_BrewCode.Focus();
                                        return;
                                    }
                                }
                            }
                        }
                        //将新的调液流程代码写入数据库
                        string s_sql = "INSERT INTO brewing_code" +
                                           " (BrewingCode) VALUES( '" + txt_BrewCode.Text + "' );";
                        FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        //更新调液流程代码表
                        BrewingCodeShow(txt_BrewCode.Text);



                        //显示新增的调液流程
                        BrewingProcessShow("");

                        //打开按钮
                        btn_BrewingProcessAdd.Enabled = true;
                        btn_BrewingProcessDelete.Enabled = true;
                        btn_BrewingProcessUpdate.Enabled = true;
                        txt_BrewCode.Enabled = false;

                        //设置添加按钮焦点
                        btn_BrewingProcessAdd.Focus();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
