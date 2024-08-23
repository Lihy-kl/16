using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SmartDyeing.FADM_Control
{
    public partial class FormulaGroup : UserControl
    {
        public FormulaGroup()
        {
            InitializeComponent();
            AssistantHeadShow(""); //染助剂右边
            getFormulaGroupHeadShow("");//左边的组合
            getFormulaGroupHeadDetailShow("");
            dgv_FormulaGroup.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_FormulaGroup.Rows[0].Cells[0].Value = "1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(FG_dataGridView1.SelectedRows.Count==0)
                {
                    return;
                }
                string s_fg = FG_dataGridView1.CurrentRow.Cells[1].Value.ToString();
                Console.WriteLine(s_fg);
                string s_sql_1 = "DELETE FROM formula_group WHERE" +
                                                  " group_Name = '" + s_fg + "';";
                FADM_Object.Communal._fadmSqlserver.GetData(s_sql_1);
                getFormulaGroupHeadShow("");//左边的组合
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "删除失败", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Delete failed", MessageBoxButtons.OK, false);
            }
        }

        private int getFormulaGroupHeadShow(string groupName)
        {
            try
            {
                string s_sql = "SELECT Id, group_Name FROM" +
                                   " formula_group WHERE node='0' ORDER BY Id  ;";
                DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                //捆绑
                FG_dataGridView1.DataSource = new DataView(dt_assistant);

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题文字
                    FG_dataGridView1.Columns[0].HeaderCell.Value = "序号";
                    FG_dataGridView1.Columns[1].HeaderCell.Value = "组合配方名称";
                    //设置标题字体
                    FG_dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                    //设置内容字体
                    FG_dataGridView1.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
                }
                else
                {
                    //设置标题文字
                    FG_dataGridView1.Columns[0].HeaderCell.Value = "ID";
                    FG_dataGridView1.Columns[1].HeaderCell.Value = "CombinationOfRecipesName";
                    //设置标题字体
                    FG_dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);

                    //设置内容字体
                    FG_dataGridView1.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);

                }

                //设置标题宽度
                FG_dataGridView1.Columns[0].Width = 100;
                FG_dataGridView1.Columns[1].Width = 350;


                //关闭染助剂名称自动排序功能
                FG_dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                FG_dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

               

                //设置内容居中显示
                FG_dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                

                //设置行高
                FG_dataGridView1.RowTemplate.Height = 30;

                //设置当前选中行
                for (int i = 0; i < FG_dataGridView1.Rows.Count; i++)
                {
                    string s_fg = FG_dataGridView1.Rows[i].Cells[0].Value.ToString();
                    if (s_fg == groupName)
                    {
                        FG_dataGridView1.CurrentCell = FG_dataGridView1.Rows[i].Cells[0];
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

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgv_FormulaGroup.EndEdit();

                string s_groupName = this.txt_GroupName.Text;//组合名称
                string s_sql_1 = "DELETE FROM formula_group WHERE" +
                                              " group_Name = '" + s_groupName + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_1);
                string s_sql_0 = "INSERT INTO formula_group (group_Name, node, createTime) VALUES( '" + s_groupName + "', '0', GetDate());";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);


                foreach (DataGridViewRow dgvr in dgv_FormulaGroup.Rows)
                {
                    
                    if (dgvr.Cells[1].Value == null)
                    {
                        break;
                    }
                    string s_cell0 = dgvr.Cells[0].Value.ToString();
                    string s_cell1 = dgvr.Cells[1].Value.ToString();
                    string s_cell2 = dgvr.Cells[2].Value.ToString();
                    string s_cell3 = dgvr.Cells[3].Value.ToString();
                    s_sql_0 = "INSERT INTO formula_group (group_Name, node,AssistantCode,AssistantName,UnitOfAccount,createTime) VALUES( '" + s_groupName + "', '1 ','" + s_cell1 + "','" + s_cell2 + "','" + s_cell3 + "', GetDate());";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql_0);
                }
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("保存成功", "组合配方", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Successfully saved", "Combination formula", MessageBoxButtons.OK, false);


                getFormulaGroupHeadShow("");
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("保存失败!" + ex.Message, "组合配方", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Save failed!" + ex.Message, "Combination formula", MessageBoxButtons.OK, false);


            }
        }

        private void btn_FormulaCodeAdd_Click(object sender, EventArgs e)
        {
            this.txt_GroupName.Focus();
        }

        private void txt_GroupName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txt_GroupName.Text != "")
                {
                    dgv_FormulaGroup.Enabled = true;
                    dgv_FormulaGroup.CurrentCell = dgv_FormulaGroup[1, 0];
                    dgv_FormulaGroup.Focus();
                    return;

                }
            }
        }

       

        private void getFormulaGroupHeadDetailShow(string s_groupName)
        {
            try
            {
                if (s_groupName != null && s_groupName.Length > 0)
                {
                    this.txt_GroupName.Text = s_groupName;
                    string s_sql = "SELECT * FROM formula_group" +
                                     " WHERE   node = '1' AND group_Name = '" + s_groupName + "';";
                    DataTable dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                    dgv_FormulaGroup.Rows.Clear();
                    for (int i = 0; i < dt_assistant.Rows.Count; i++)
                    {
                        dgv_FormulaGroup.Rows.Add((i + 1).ToString(),
                                                 dt_assistant.Rows[i]["AssistantCode"].ToString(),
                                                 dt_assistant.Rows[i]["AssistantName"].ToString(),
                                                 dt_assistant.Rows[i]["UnitOfAccount"].ToString()
                                                 );
                    }
                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show( ex.Message, "加载组合详情失败", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show(ex.Message, "Failed to load combination details", MessageBoxButtons.OK, false);
            }
        }

        /// <summary>
        /// 显示染助剂表头
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private int AssistantHeadShow(string s_assistantCode)
        {
            try
            {
                //获取染助剂代码表头
                string P_str_sql = "SELECT AssistantCode, AssistantName FROM" +
                                   " assistant_details  ;";
                DataTable P_dt_assistant = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                //捆绑
                dgv_Assistant.DataSource = new DataView(P_dt_assistant);

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //设置标题文字
                    dgv_Assistant.Columns[0].HeaderCell.Value = "染助剂代码";
                    dgv_Assistant.Columns[1].HeaderCell.Value = "染助剂名称";
                    //设置标题字体
                    dgv_Assistant.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);
                    //设置内容字体
                    dgv_Assistant.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
                }
                else
                {
                    //设置标题文字
                    dgv_Assistant.Columns[0].HeaderCell.Value = "DyeingAuxiliariesCode";
                    dgv_Assistant.Columns[1].HeaderCell.Value = "DyeingAuxiliariesName";
                    //设置标题字体
                    dgv_Assistant.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);
                    //设置内容字体
                    dgv_Assistant.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
                }

                //设置标题宽度
                dgv_Assistant.Columns[0].Width = 160;
                if (dgv_Assistant.Rows.Count > 30)
                {
                    dgv_Assistant.Columns[1].Width = 220;
                }
                else
                {
                    dgv_Assistant.Columns[1].Width = 250;
                }


                //关闭染助剂名称自动排序功能
                dgv_Assistant.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dgv_Assistant.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

             

                //设置内容居中显示
                dgv_Assistant.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                

                //设置行高
                dgv_Assistant.RowTemplate.Height = 30;

                //设置当前选中行
                for (int i = 0; i < dgv_Assistant.Rows.Count; i++)
                {
                    string s = dgv_Assistant.Rows[i].Cells[0].Value.ToString();
                    if (s == s_assistantCode)
                    {
                        dgv_Assistant.CurrentCell = dgv_Assistant.Rows[i].Cells[0];
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

      

     
      

        private void dgv_FormulaGroup_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgv_FormulaGroup.Rows.Count; i++)
                {
                    dgv_FormulaGroup[0, i].Value = i + 1;
                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_FormulaGroup_RowsAdded", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_FormulaGroup_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgv_FormulaGroup.Rows.Count; i++)
                {
                    dgv_FormulaGroup[0, i].Value = i + 1;
                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "dgv_FormulaGroup_RowsRemoved", MessageBoxButtons.OK, true);
            }
        }

        private void FG_dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (FG_dataGridView1.CurrentRow != null && FG_dataGridView1.CurrentRow.Cells[1].Value != null)
                {
                    string s = FG_dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    getFormulaGroupHeadDetailShow(s);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void dgv_FormulaGroup_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_FormulaGroup.Rows.Count > 1)
            {
                int i_col = dgv_FormulaGroup.CurrentCell.ColumnIndex;

                int i_row = dgv_FormulaGroup.CurrentCell.RowIndex;

                if (i_col == 1)
                {

                    i_col = 0;

                    if (i_row == dgv_FormulaGroup.NewRowIndex)
                    {
                        //判断上一行是否为空行
                        string s_1 = null;
                        string s_2 = null;
                        try
                        {
                            if (dgv_FormulaGroup.CurrentCell.Value != null)
                                s_1 = dgv_FormulaGroup.CurrentCell.Value.ToString();
                        }
                        catch
                        {

                        }
                        if (s_1 == null )
                        {
                                    btn_Save.Focus();
                            
                        }

                    }

                }
            }
        }
    }
}
