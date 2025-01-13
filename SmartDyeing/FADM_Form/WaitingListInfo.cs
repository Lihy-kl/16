using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public partial class WaitingListInfo : Form
    {
        public WaitingListInfo()
        {
            InitializeComponent();
            FADM_Object.Communal._b_isOpenWaitList = true;
            WaitListShow();
            WaitListShow1();
        }

        /// <summary>
        /// 排队列表显示
        /// </summary>
        private void WaitListShow()
        {
            try
            {
                //获取染固色流程代码
                string s_sql = "SELECT ROW_NUMBER() over (order by IndexNum) as num,FormulaCode,VersionNum,CupNum,IndexNum  FROM wait_list  where Type = 3 order by IndexNum;";
                DataTable dt_dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //捆绑
                dgv_WaitList.DataSource = new DataView(dt_dyeingcode);

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题文字
                    dgv_WaitList.Columns[0].HeaderCell.Value = "序号";
                    dgv_WaitList.Columns[1].HeaderCell.Value = "配方代码";
                    dgv_WaitList.Columns[2].HeaderCell.Value = "版本";
                    dgv_WaitList.Columns[3].HeaderCell.Value = "杯号";
                    dgv_WaitList.Columns[4].HeaderCell.Value = "原序号";
                }
                else
                {
                    //设置标题文字
                    dgv_WaitList.Columns[0].HeaderCell.Value = "Index";
                    dgv_WaitList.Columns[1].HeaderCell.Value = "RecipeCode";
                    dgv_WaitList.Columns[2].HeaderCell.Value = "Version";
                    dgv_WaitList.Columns[3].HeaderCell.Value = "CupNumber";
                    dgv_WaitList.Columns[4].HeaderCell.Value = "Old Index";
                }

                //设置标题宽度
                dgv_WaitList.Columns[0].Width = 60;
                dgv_WaitList.Columns[1].Width = 140;
                dgv_WaitList.Columns[2].Width = 60;
                dgv_WaitList.Columns[3].Width = 60;
                dgv_WaitList.Columns[4].Width = 80;

                //关闭点击标题自动排序功能
                dgv_WaitList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_WaitList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_WaitList.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_WaitList.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_WaitList.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dgv_WaitList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置标题字体
                dgv_WaitList.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容居中显示
                dgv_WaitList.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置内容字体
                dgv_WaitList.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置行高
                dgv_WaitList.RowTemplate.Height = 30;
            }
            catch
            {
                return;
            }
        }

        private void WaitListShow1()
        {
            try
            {
                //获取染固色流程代码
                string s_sql = "SELECT ROW_NUMBER() over (order by IndexNum) as num,FormulaCode,VersionNum,CupNum,IndexNum  FROM wait_list  where Type = 2 order by IndexNum;";
                DataTable dt_dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //捆绑
                dgv_WaitList1.DataSource = new DataView(dt_dyeingcode);

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题文字
                    dgv_WaitList1.Columns[0].HeaderCell.Value = "序号";
                    dgv_WaitList1.Columns[1].HeaderCell.Value = "配方代码";
                    dgv_WaitList1.Columns[2].HeaderCell.Value = "版本";
                    dgv_WaitList1.Columns[3].HeaderCell.Value = "杯号";
                    dgv_WaitList1.Columns[4].HeaderCell.Value = "原序号";
                }
                else
                {
                    //设置标题文字
                    dgv_WaitList1.Columns[0].HeaderCell.Value = "Index";
                    dgv_WaitList1.Columns[1].HeaderCell.Value = "RecipeCode";
                    dgv_WaitList1.Columns[2].HeaderCell.Value = "Version";
                    dgv_WaitList1.Columns[3].HeaderCell.Value = "CupNumber";
                    dgv_WaitList1.Columns[4].HeaderCell.Value = "Old Index";
                }

                //设置标题宽度
                dgv_WaitList1.Columns[0].Width = 60;
                dgv_WaitList1.Columns[1].Width = 140;
                dgv_WaitList1.Columns[2].Width = 60;
                dgv_WaitList1.Columns[3].Width = 60;
                dgv_WaitList1.Columns[4].Width = 80;

                //关闭点击标题自动排序功能
                dgv_WaitList1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_WaitList1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_WaitList1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_WaitList1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv_WaitList1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dgv_WaitList1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置标题字体
                dgv_WaitList1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容居中显示
                dgv_WaitList1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置内容字体
                dgv_WaitList1.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置行高
                dgv_WaitList1.RowTemplate.Height = 30;
            }
            catch
            {
                return;
            }
        }

        private void Btn_Up_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_WaitList.CurrentCell != null)
                {
                    if (dgv_WaitList.CurrentCell.RowIndex == 0)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已是第一条记录","温馨提示",MessageBoxButtons.OK,false);
                        else
                            FADM_Form.CustomMessageBox.Show("This is already the first record", "Tips", MessageBoxButtons.OK, false);
                    }
                    else
                    {
                        //记录当前选中数据
                        string s_formulaCode = "";
                        string s_versionNum = "";
                        string s_cupNum = "";
                        string s_indexNum = "";

                        s_formulaCode = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[1].Value.ToString();
                        s_versionNum = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[2].Value.ToString();
                        s_cupNum = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[3].Value.ToString();
                        s_indexNum = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[4].Value.ToString();

                        //交换2行数据
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[1].Value = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex - 1].Cells[1].Value;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[2].Value = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex - 1].Cells[2].Value;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[3].Value = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex - 1].Cells[3].Value;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[4].Value = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex - 1].Cells[4].Value;

                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex - 1].Cells[1].Value = s_formulaCode;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex - 1].Cells[2].Value = s_versionNum;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex - 1].Cells[3].Value = s_cupNum;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex - 1].Cells[4].Value = s_indexNum;

                        dgv_WaitList.CurrentCell = dgv_WaitList[0, dgv_WaitList.CurrentCell.RowIndex - 1];
                    }
                }
            }
            catch
            {
            }
        }

        private void Btn_Down_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_WaitList.CurrentCell != null)
                {
                    if (dgv_WaitList.CurrentCell.RowIndex == dgv_WaitList.Rows.Count - 1)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已是最后一条记录", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("This is already the last record", "Tips", MessageBoxButtons.OK, false);
                    }
                    else
                    {
                        //记录当前选中数据
                        string s_formulaCode = "";
                        string s_versionNum = "";
                        string s_cupNum = "";
                        string s_indexNum = "";

                        s_formulaCode = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[1].Value.ToString();
                        s_versionNum = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[2].Value.ToString();
                        s_cupNum = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[3].Value.ToString();
                        s_indexNum = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[4].Value.ToString();

                        //交换2行数据
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[1].Value = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex + 1].Cells[1].Value;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[2].Value = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex + 1].Cells[2].Value;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[3].Value = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex + 1].Cells[3].Value;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[4].Value = dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex + 1].Cells[4].Value;

                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex + 1].Cells[1].Value = s_formulaCode;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex + 1].Cells[2].Value = s_versionNum;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex + 1].Cells[3].Value = s_cupNum;
                        dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex + 1].Cells[4].Value = s_indexNum;

                        dgv_WaitList.CurrentCell = dgv_WaitList[0, dgv_WaitList.CurrentCell.RowIndex + 1];
                    }
                }
            }
            catch
            {
            }
        }

        private void Btn_Del_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_WaitList.SelectedRows.Count > 0)
                {
                    FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 3 and IndexNum = " + dgv_WaitList.Rows[dgv_WaitList.CurrentCell.RowIndex].Cells[4].Value.ToString());
                    dgv_WaitList.Rows.Remove(dgv_WaitList.CurrentRow);
                    // dgv_WaitList.ClearSelection();

                    int i_num = 1;
                    foreach (DataGridViewRow dgvr in dgv_WaitList.Rows)
                    {
                        dgv_WaitList[0, dgvr.Index].Value = i_num.ToString();
                        i_num++;
                    }
                    FADM_Control.Formula._b_updateWait = true;
                }
            }
            catch
            {
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //先删除，再保存
                FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 3 ");
                foreach (DataGridViewRow dgvr in dgv_WaitList.Rows)
                {
                    string s_sql_0 = "INSERT INTO wait_list ( FormulaCode, VersionNum, IndexNum, CupNum,Type) values('" + dgv_WaitList[1, dgvr.Index].Value.ToString() + "','" + dgv_WaitList[2, dgvr.Index].Value.ToString() + "','" + dgv_WaitList[0, dgvr.Index].Value.ToString() + "','" + dgv_WaitList[3, dgvr.Index].Value.ToString() + "',3);";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);
                }
                //重新显示
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("保存成功", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Successfully saved", "Tips", MessageBoxButtons.OK, false);
                WaitListShow();
            }
            catch { }
        }

        private void dgv_WaitList_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void WaitingListInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            FADM_Object.Communal._b_isOpenWaitList = false;
        }

        private void Btn_Up1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_WaitList1.CurrentCell != null)
                {
                    if (dgv_WaitList1.CurrentCell.RowIndex == 0)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已是第一条记录", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("This is already the first record", "Tips", MessageBoxButtons.OK, false);
                    }
                    else
                    {
                        //记录当前选中数据
                        string s_formulaCode = "";
                        string s_versionNum = "";
                        string s_cupNum = "";
                        string s_indexNum = "";

                        s_formulaCode = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[1].Value.ToString();
                        s_versionNum = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[2].Value.ToString();
                        s_cupNum = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[3].Value.ToString();
                        s_indexNum = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[4].Value.ToString();

                        //交换2行数据
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[1].Value = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex - 1].Cells[1].Value;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[2].Value = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex - 1].Cells[2].Value;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[3].Value = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex - 1].Cells[3].Value;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[4].Value = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex - 1].Cells[4].Value;

                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex - 1].Cells[1].Value = s_formulaCode;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex - 1].Cells[2].Value = s_versionNum;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex - 1].Cells[3].Value = s_cupNum;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex - 1].Cells[4].Value = s_indexNum;

                        dgv_WaitList1.CurrentCell = dgv_WaitList1[0, dgv_WaitList1.CurrentCell.RowIndex - 1];
                    }
                }
            }
            catch
            {
            }
        }

        private void Btn_Down1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_WaitList1.CurrentCell != null)
                {
                    if (dgv_WaitList1.CurrentCell.RowIndex == dgv_WaitList1.Rows.Count - 1)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已是最后一条记录", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("This is already the last record", "Tips", MessageBoxButtons.OK, false);
                    }
                    else
                    {
                        //记录当前选中数据
                        string s_formulaCode = "";
                        string s_versionNum = "";
                        string s_cupNum = "";
                        string s_indexNum = "";

                        s_formulaCode = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[1].Value.ToString();
                        s_versionNum = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[2].Value.ToString();
                        s_cupNum = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[3].Value.ToString();
                        s_indexNum = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[4].Value.ToString();

                        //交换2行数据
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[1].Value = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex + 1].Cells[1].Value;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[2].Value = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex + 1].Cells[2].Value;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[3].Value = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex + 1].Cells[3].Value;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[4].Value = dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex + 1].Cells[4].Value;

                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex + 1].Cells[1].Value = s_formulaCode;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex + 1].Cells[2].Value = s_versionNum;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex + 1].Cells[3].Value = s_cupNum;
                        dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex + 1].Cells[4].Value = s_indexNum;

                        dgv_WaitList1.CurrentCell = dgv_WaitList1[0, dgv_WaitList1.CurrentCell.RowIndex + 1];
                    }
                }
            }
            catch
            {
            }
        }

        private void Btn_Del1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_WaitList1.SelectedRows.Count > 0)
                {
                    FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 2 and IndexNum = " + dgv_WaitList1.Rows[dgv_WaitList1.CurrentCell.RowIndex].Cells[4].Value.ToString());
                    dgv_WaitList1.Rows.Remove(dgv_WaitList1.CurrentRow);
                    // dgv_WaitList.ClearSelection();

                    int i_num = 1;
                    foreach (DataGridViewRow dgvr in dgv_WaitList1.Rows)
                    {
                        dgv_WaitList1[0, dgvr.Index].Value = i_num.ToString();
                        i_num++;
                    }
                    FADM_Control.Formula._b_updateWait = true;
                }
            }
            catch
            {
            }
        }

        private void Btn_Save1_Click(object sender, EventArgs e)
        {
            try
            {
                //先删除，再保存
                FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 2 ");
                foreach (DataGridViewRow dgvr in dgv_WaitList1.Rows)
                {
                    string s_sql_0 = "INSERT INTO wait_list ( FormulaCode, VersionNum, IndexNum, CupNum,Type) values('" + dgv_WaitList1[1, dgvr.Index].Value.ToString() + "','" + dgv_WaitList1[2, dgvr.Index].Value.ToString() + "','" + dgv_WaitList1[0, dgvr.Index].Value.ToString() + "','" + dgv_WaitList1[3, dgvr.Index].Value.ToString() + "',2);";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);
                }
                //重新显示
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("保存成功", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Successfully saved", "Tips", MessageBoxButtons.OK, false);
                WaitListShow1();
            }
            catch { }
        }
    }
}
