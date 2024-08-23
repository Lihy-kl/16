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
    public partial class Operator : Form
    {
        //private Button P_btn;
        public Operator(/*Button _btn*/)
        {
            InitializeComponent();
            //this.P_btn = _btn;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
            again:
                List<string> lis_op = new List<string>();
                lis_op.Add("");
                foreach (DataGridViewRow dgvr in dgv_Operator.Rows)
                {
                    if (dgvr.Index < dgv_Operator.RowCount - 1)
                    {
                        for (int i = 0; i < dgv_Operator.Columns.Count - 1; i++)
                        {
                            if (dgvr.Cells[i].Value == null || Convert.ToString(dgvr.Cells[i].Value) == "")
                            {
                                try
                                {
                                    dgv_Operator.Rows.Remove(dgvr);
                                    break;
                                }
                                catch
                                {

                                }
                            }
                        }
                        if (lis_op.Contains(dgvr.Cells[0].Value.ToString()))
                        {
                            dgv_Operator.Rows.Remove(dgvr);
                            goto again;
                        }
                        else
                        {
                            lis_op.Add(dgvr.Cells[0].Value.ToString());
                        }
                    }
                }

                string s_sql = "TRUNCATE TABLE operator_table;";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                foreach (DataGridViewRow dgvr in dgv_Operator.Rows)
                {
                    if (dgvr.Index < dgv_Operator.Rows.Count - 1)
                    {
                        s_sql = "INSERT INTO operator_table (" +
                                    " operatorname) VALUES( '" + dgvr.Cells[0].Value.ToString() + "');";

                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                    }
                }
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("保存完成!", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Save completed!", "Tips", MessageBoxButtons.OK, false);

                this.Close();
            }
            catch
            {

            }
        }

        private void Operator_Load(object sender, EventArgs e)
        {
            string s_sql = "SELECT * FROM operator_table ;";
            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            dgv_Operator.DataSource = new DataView(dt_data);

            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                dgv_Operator.Columns[0].HeaderCell.Value = "操作员姓名";
            }
            else
            {
                dgv_Operator.Columns[0].HeaderCell.Value = "OperatorName";
            }

            //设置标题宽度
            dgv_Operator.Columns[0].Width = 320;

            //关闭自动排序功能
            dgv_Operator.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            //设置标题居中显示
            dgv_Operator.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置标题字体
            dgv_Operator.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置内容居中显示
            dgv_Operator.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置内容字体
            dgv_Operator.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置行高
            dgv_Operator.RowTemplate.Height = 30;
        }

        private void Operator_FormClosing(object sender, FormClosingEventArgs e)
        {
            //P_btn.Visible = true;
        }
    }
}
