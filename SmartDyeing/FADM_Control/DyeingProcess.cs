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
    public partial class DyeingProcess : UserControl
    {
        public DyeingProcess()
        {
            InitializeComponent();
            UpdateListAndDyeCode();
            DyeingCodeShow("");
            DyeCodeShow("");
            dgv_Dye_Code.ClearSelection();
            dgv_Child_DyeData.ClearSelection();
        }

        List<string> listCode = new List<string>();
        //判断是新增染色还是后处理
        bool b_NewDye = false;

        //处理工艺字典
        Dictionary<string, string> directoryDyeCode = new Dictionary<string, string>();

        private void UpdateListAndDyeCode()
        {
            listCode.Clear();

            string P_str_sql = "SELECT Code  FROM dyeing_process group by Code;";
            DataTable P_dt_Dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            for (int i = 0; i < P_dt_Dyeingcode.Rows.Count; i++)
            {
                listCode.Add(P_dt_Dyeingcode.Rows[i][0].ToString());
            }

            for (int i = 0; i < dgv_DyeDetails.Rows.Count; i++)
            {
                DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_DyeDetails[0, i];
                dd.DataSource = null;
                dd.DataSource = listCode;
            }

            directoryDyeCode.Clear();
            P_str_sql = "SELECT *  FROM dyeing_Remark;";
            P_dt_Dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            for (int i = 0; i < P_dt_Dyeingcode.Rows.Count; i++)
            {
                directoryDyeCode.Add(P_dt_Dyeingcode.Rows[i]["Code"].ToString(), P_dt_Dyeingcode.Rows[i]["Remark"].ToString());
            }

        }

        /// <summary>
        /// 显示染固色流程代码
        /// </summary>
        /// <param name="_DyeingCode">选中的染固色流程代码</param>
        /// <returns>0:正常;-1:异常</returns>
        private int DyeingCodeShow(string _DyeingCode)
        {
            try
            {
                //获取染固色流程代码
                string P_str_sql = "SELECT DyeingCode  FROM dyeing_code group by DyeingCode;";
                DataTable P_dt_Dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                //捆绑
                dgv_DyeingCode.DataSource = new DataView(P_dt_Dyeingcode);

                //设置标题文字
                dgv_DyeingCode.Columns[0].HeaderCell.Value = "染固色流程代码";

                //设置标题宽度
                dgv_DyeingCode.Columns[0].Width = dgv_DyeingCode.Width - 3;

                //关闭点击标题自动排序功能
                dgv_DyeingCode.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dgv_DyeingCode.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置标题字体
                dgv_DyeingCode.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容居中显示
                dgv_DyeingCode.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置内容字体
                dgv_DyeingCode.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置行高
                dgv_DyeingCode.RowTemplate.Height = 30;

                //设置当前选中行
                for (int i = 0; i < dgv_DyeingCode.Rows.Count; i++)
                {
                    string s = dgv_DyeingCode.Rows[i].Cells[0].Value.ToString();
                    if (s == _DyeingCode)
                    {
                        dgv_DyeingCode.CurrentCell = dgv_DyeingCode.Rows[i].Cells[0];
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
        /// 显示染固色代码对应详情
        /// </summary>
        /// <param name="_DyeingCode">选中的染固色流程代码</param>
        /// <returns>0:正常;-1:异常</returns>
        private int DyeDetailsShow(string _DyeingCode)
        {
            try
            {
                dgv_DyeDetails.Rows.Clear();

                //获取染固色流程代码
                string P_str_sql = "SELECT Code  FROM dyeing_code where DyeingCode = '" + _DyeingCode + "' order by Step,Type;";
                DataTable P_dt_Dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                //////捆绑
                //dgv_DyeDetails.DataSource = new DataView(P_dt_Dyeingcode);

                dgv_DyeDetails.Columns[0].HeaderCell.Value = "流程代码";
                //设置标题宽度
                dgv_DyeDetails.Columns[0].Width = dgv_DyeDetails.Width - 3;
                //关闭点击标题自动排序功能
                dgv_DyeDetails.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;


                //设置标题居中显示
                dgv_DyeDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置标题字体
                dgv_DyeDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容居中显示
                dgv_DyeDetails.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置内容字体
                dgv_DyeDetails.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置行高
                dgv_DyeDetails.RowTemplate.Height = 30;

                for (int i = 0; i < P_dt_Dyeingcode.Rows.Count; i++)
                {
                    dgv_DyeDetails.Rows.Add();
                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_DyeDetails[0, i];
                    dd.DataSource = listCode;
                    dd.Value = P_dt_Dyeingcode.Rows[i][0].ToString();
                }
                //新增行下拉选项
                {
                    DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_DyeDetails[0, dgv_DyeDetails.Rows.Count - 1];
                    dd.DataSource = listCode;
                }


                ////设置当前选中行
                //for (int i = 0; i < dgv_DyeDetails.Rows.Count; i++)
                //{
                //    string s = dgv_DyeDetails.Rows[i].Cells[0].Value.ToString();
                //    if (s == _DyeingCode)
                //    {
                //        dgv_DyeDetails.CurrentCell = dgv_DyeDetails.Rows[i].Cells[0];
                //        break;
                //    }
                //}

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 显示染色步骤
        /// </summary>
        /// <param name="_StepNum">当前选中的步号</param>
        /// <returns>0:正常;-1:异常</returns>
        public int ChildDyeDataShow(string _StepNum)
        {
            try
            {
                //获取当前调液代码的调液流程
                string P_str_sql = "SELECT Type  FROM dyeing_process WHERE" +
                                   " Code = '" + txt_Dye_Code.Text + "' Order By StepNum ; ";
                DataTable P_dt_Dyeingprocess = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                if (P_dt_Dyeingprocess.Rows.Count > 0)
                {
                    if (P_dt_Dyeingprocess.Rows[0][0].ToString() == "1")
                    {
                        b_NewDye = true;
                    }
                    else
                    {
                        b_NewDye = false;
                    }
                }

                //获取当前调液代码的调液流程
                P_str_sql = "SELECT StepNum,TechnologyName,Temp,Rate,ProportionOrTime,Rev  FROM dyeing_process WHERE" +
                                   " Code = '" + txt_Dye_Code.Text + "' Order By StepNum ; ";
                P_dt_Dyeingprocess = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                //捆绑
                dgv_Child_DyeData.DataSource = new DataView(P_dt_Dyeingprocess);

                //设置标题栏名称
                string[] lineName = { "步号", "操作类型", "温度", "速率", "百分比(%)/时间(s)", "转速" };
                for (int i = 0; i < 6; i++)
                {
                    dgv_Child_DyeData.Columns[i].HeaderCell.Value = lineName[i];
                    //设置标题宽度
                    dgv_Child_DyeData.Columns[i].Width = (dgv_Child_DyeData.Width - 2) / 6;
                    //关闭点击标题自动排序功能
                    dgv_Child_DyeData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                //设置标题居中显示
                dgv_Child_DyeData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置标题字体
                dgv_Child_DyeData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容居中显示
                dgv_Child_DyeData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //设置内容字体
                dgv_Child_DyeData.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置行高
                dgv_Child_DyeData.RowTemplate.Height = 30;

                ////设置当前选中行
                //for (int i = 0; i < dgv_Child_DyeData.Rows.Count; i++)
                //{
                //    string s = dgv_Child_DyeData.Rows[i].Cells[0].Value.ToString();
                //    if (s == _StepNum)
                //    {
                //        dgv_Child_DyeData.CurrentCell = dgv_Child_DyeData.Rows[i].Cells[0];
                //        break;
                //    }
                //}

                dgv_Child_DyeData.ClearSelection();

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 显示染色流程代码
        /// </summary>
        /// <param name="_DyeingCode">选中的详细代码</param>
        /// <returns>0:正常;-1:异常</returns>
        private int DyeCodeShow(string _DyeCode)
        {
            string P_str_sql = "SELECT Code  FROM dyeing_process group by Code;";
            DataTable P_dt_Dyeingcode = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

            //捆绑
            dgv_Dye_Code.DataSource = new DataView(P_dt_Dyeingcode);

            //设置标题文字
            dgv_Dye_Code.Columns[0].HeaderCell.Value = "工艺代码";

            //设置标题宽度
            dgv_Dye_Code.Columns[0].Width = dgv_Dye_Code.Width - 3;

            //关闭点击标题自动排序功能
            dgv_Dye_Code.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            //设置标题居中显示
            dgv_Dye_Code.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置标题字体
            dgv_Dye_Code.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置内容居中显示
            dgv_Dye_Code.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置内容字体
            dgv_Dye_Code.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置行高
            dgv_DyeingCode.RowTemplate.Height = 30;

            try
            {
                //设置当前选中行
                for (int i = 0; i < dgv_Dye_Code.Rows.Count; i++)
                {
                    string s = dgv_Dye_Code.Rows[i].Cells[0].Value.ToString();
                    if (s == _DyeCode)
                    {
                        dgv_Dye_Code.CurrentCell = dgv_Dye_Code.Rows[i].Cells[0];
                        break;
                    }
                }
                if (_DyeCode == "")
                {
                    txt_Remark.Text = "";
                }
                else
                {
                    if (directoryDyeCode.ContainsKey(txt_Dye_Code.Text))
                        txt_Remark.Text = directoryDyeCode[txt_Dye_Code.Text];
                    else
                    {
                        txt_Remark.Text = "";
                    }
                }

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        private void btn_DyeingCodeAdd_Click(object sender, EventArgs e)
        {
            txt_DyeingCode.Text = null;
            txt_DyeingCode.Enabled = true;
            txt_DyeingCode.Focus();
            //btn_DyeCodeAdd.Enabled = false;
            //btn_DyeingCodeDelete.Enabled = false;
            btn_DyeingProcessAdd.Enabled = false;
            btn_DyeingProcessDelete.Enabled = false;
            btn_DyeingProcessUpdate.Enabled = false;
        }

        private void btn_DyeingCodeDelete_Click(object sender, EventArgs e)
        {
            if (dgv_DyeingCode.SelectedRows.Count == 0)
            {
                FADM_Form.CustomMessageBox.Show("请先选择操作行", "操作异常", MessageBoxButtons.OK, false);
                return;
            }
            string P_str_sql = "select COUNT(*) from drop_head where DyeingCode  = '" + txt_DyeingCode.Text + "' ;";
            DataTable P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            if (P_dt.Rows[0][0].ToString() != "0")
            {
                if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("此工艺正在运行，禁止删除",
                                    "温馨提示", MessageBoxButtons.OK, false))
                {
                    return;
                }
            }

            P_str_sql = "select * from wait_list where Type != 2;";
            P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            foreach(DataRow dr in P_dt.Rows)
            {
                string code = dr["FormulaCode"].ToString();
                int Version = Convert.ToInt16(dr["VersionNum"]);
                P_str_sql = "select * from formula_head where FormulaCode = '"+code+ "' and VersionNum = "+ Version + ";";
                DataTable dataTable = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                string DyeingCode = dataTable.Rows[0]["DyeingCode"].ToString();
                if(DyeingCode == txt_DyeingCode.Text)
                {
                    if (DialogResult.OK == FADM_Form.CustomMessageBox.Show("此工艺正在运行，禁止删除",
                                   "温馨提示", MessageBoxButtons.OK, false))
                    {
                        return;
                    }
                }

            }

            P_str_sql = "DELETE FROM dyeing_code" +
                               " WHERE DyeingCode = '" + txt_DyeingCode.Text + "';";
            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

            P_str_sql = "update formula_head set DyeingCode = null,Stage='滴液'  where DyeingCode = '" + txt_DyeingCode.Text + "';";

            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
            FADM_Object.Communal._b_isUpdateNotDripList = true;
            try
            {
                DyeingCodeShow(dgv_DyeingCode.Rows[dgv_DyeingCode.CurrentCell.RowIndex - 1].Cells[0].Value.ToString());
            }
            catch
            {
                DyeingCodeShow("");
            }
        }

        private void btn_DyeingProcessAdd_Click(object sender, EventArgs e)
        {
            //if (dgv_Child_DyeData.SelectedRows.Count == 0)
            //{
            //    FADM_Form.CustomMessageBox.Show("请先选择操作行", "操作异常", MessageBoxButtons.OK, false);
            //    return;
            //}
            string P_str_sql = "select COUNT(*) from formula_head where DyeingCode in( select DyeingCode from dyeing_code where Code = '" + txt_Dye_Code.Text + "') and State = '已滴定配方'";
            DataTable P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            if (P_dt.Rows.Count > 0)
            {
                if (P_dt.Rows[0][0].ToString() != "0")
                {
                    //if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("此工艺已有完成滴液记录，不能修改",
                    //                    "温馨提示", MessageBoxButtons.OK, false))
                    //{
                    //    return;
                    //}

                    ReName rename = new ReName(2, txt_Dye_Code.Text);
                    rename.ShowDialog();
                    if (rename.DialogResult != DialogResult.OK)
                    {
                        return;
                    }

                    //写入数据库
                    string P_str_sql1 = "INSERT INTO dyeing_Remark" +
                                           " (Code,Remark) VALUES( '" + FADM_Object.Communal._s_reName + "' ,'" + txt_Remark.Text + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql1);

                    UpdateListAndDyeCode();
                    DyeCodeShow(FADM_Object.Communal._s_reName);
                }
            }

            string stepNum = (dgv_Child_DyeData.Rows.Count + 1).ToString();
            DyeingStep form_DyeingStep = new DyeingStep(b_NewDye ? 1 : 2, stepNum, "", "", "", "", txt_Dye_Code.Text,"",txt_Remark.Text, true, this,null,null);
            form_DyeingStep.Show();
        }

        private void btn_DyeingProcessUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Child_DyeData.SelectedRows.Count == 0)
                {
                    FADM_Form.CustomMessageBox.Show("请先选择操作行", "操作异常", MessageBoxButtons.OK, false);
                    return;
                }

                string P_str_sql = "select COUNT(*) from formula_head where DyeingCode in( select DyeingCode from dyeing_code where Code = '" + txt_Dye_Code.Text + "') and State = '已滴定配方'";
                DataTable P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                if (P_dt.Rows[0][0].ToString() != "0")
                {
                    //if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("此工艺已有完成滴液记录，不能修改",
                    //                    "温馨提示", MessageBoxButtons.OK, false))
                    //{
                    //    return;
                    //}

                    ReName rename = new ReName(2, txt_Dye_Code.Text);
                    rename.ShowDialog();
                    if (rename.DialogResult != DialogResult.OK)
                    {
                        return;
                    }
                    //写入数据库
                    string P_str_sql1 = "INSERT INTO dyeing_Remark" +
                                           " (Code,Remark) VALUES( '" + FADM_Object.Communal._s_reName + "' ,'" + txt_Remark.Text + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql1);

                    UpdateListAndDyeCode();

                    int index = dgv_Child_DyeData.CurrentRow.Index;

                    DyeCodeShow(FADM_Object.Communal._s_reName);
                    dgv_Child_DyeData.CurrentCell = dgv_Child_DyeData[0, index];
                }

                string stepNum = dgv_Child_DyeData.CurrentRow.Cells[0].Value.ToString();
                string technologyName = dgv_Child_DyeData.CurrentRow.Cells[1].Value.ToString().Trim();
                string proportionOrTime = "";
                string temp = "";
                string rate = "";
                string rev = "";
                if (dgv_Child_DyeData.CurrentRow.Cells[2].Value != null)
                {
                    temp = dgv_Child_DyeData.CurrentRow.Cells[2].Value.ToString();
                }
                if (dgv_Child_DyeData.CurrentRow.Cells[3].Value != null)
                {
                    rate = dgv_Child_DyeData.CurrentRow.Cells[3].Value.ToString();
                }
                if (dgv_Child_DyeData.CurrentRow.Cells[4].Value != null)
                {
                    proportionOrTime = dgv_Child_DyeData.CurrentRow.Cells[4].Value.ToString();
                }
                if (dgv_Child_DyeData.CurrentRow.Cells[5].Value != null)
                {
                    rev = dgv_Child_DyeData.CurrentRow.Cells[5].Value.ToString();
                }
                DyeingStep form_DyeingStep = new DyeingStep(b_NewDye ? 1 : 2, stepNum, technologyName, proportionOrTime, temp, rate, txt_Dye_Code.Text,rev,txt_Remark.Text, false, this,null,null);
                form_DyeingStep.Show();
            }
            catch
            {
                 FADM_Form.CustomMessageBox.Show("未发现可修改的行,请先添加！", "操作异常", MessageBoxButtons.OK, false);
            }
        }

        private void btn_DyeingProcessDelete_Click(object sender, EventArgs e)
        {
            if(dgv_Child_DyeData.SelectedRows.Count == 0)
            {
                FADM_Form.CustomMessageBox.Show("请先选择操作行", "操作异常", MessageBoxButtons.OK, false);
                return;
            }
            string P_str_sql = "select COUNT(*) from formula_head where DyeingCode in( select DyeingCode from dyeing_code where Code = '" + txt_Dye_Code.Text + "') and State = '已滴定配方'";
            DataTable P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            if (P_dt.Rows[0][0].ToString() != "0")
            {
                //if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("此工艺已有完成滴液记录，不能修改",
                //                    "温馨提示", MessageBoxButtons.OK, false))
                //{
                //    return;
                //}

                ReName rename = new ReName(2, txt_Dye_Code.Text);
                rename.ShowDialog();
                if (rename.DialogResult != DialogResult.OK)
                {
                    return;
                }
                //写入数据库
                string P_str_sql1 = "INSERT INTO dyeing_Remark" +
                                       " (Code,Remark) VALUES( '" + FADM_Object.Communal._s_reName + "' ,'" + txt_Remark.Text + "');";
                FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql1);

                UpdateListAndDyeCode();

                int index = dgv_Child_DyeData.CurrentRow.Index;

                DyeCodeShow(FADM_Object.Communal._s_reName);
                dgv_Child_DyeData.CurrentCell = dgv_Child_DyeData[0, index];
            }

            P_str_sql = "DELETE FROM dyeing_process WHERE" +
                              " StepNum = '" + dgv_Child_DyeData.CurrentRow.Cells[0].Value.ToString() + "'" +
                              " AND Code = '" + txt_Dye_Code.Text + "' ;";
            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
            try
            {
                ChildDyeDataShow(dgv_Child_DyeData.Rows[dgv_Child_DyeData.CurrentCell.RowIndex - 1].Cells[0].Value.ToString());
            }
            catch
            {
                ChildDyeDataShow("");
            }
        }

        private void btn_DyeCodeAdd_Click(object sender, EventArgs e)
        {
            b_NewDye = true;
            txt_Dye_Code.Text = null;
            txt_Dye_Code.Enabled = true;
            txt_Dye_Code.Focus();

            btn_DyeingProcessAdd.Enabled = false;
            btn_DyeingProcessDelete.Enabled = false;
            btn_DyeingProcessUpdate.Enabled = false;
            btn_DyeCodeDelete.Enabled = false;
        }

        private void btn_AddHandle_Click(object sender, EventArgs e)
        {
            b_NewDye = false;
            txt_Dye_Code.Text = null;
            txt_Dye_Code.Enabled = true;
            txt_Dye_Code.Focus();

            btn_DyeingProcessAdd.Enabled = false;
            btn_DyeingProcessDelete.Enabled = false;
            btn_DyeingProcessUpdate.Enabled = false;
            btn_DyeCodeDelete.Enabled = false;
        }

        private void dgv_DyeingCode_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_DyeingCode.CurrentCell != null)
                {
                    txt_DyeingCode.Text = dgv_DyeingCode.CurrentCell.Value.ToString();
                    DyeDetailsShow(txt_DyeingCode.Text);
                    dgv_DyeDetails.ClearSelection();
                }
            }
            catch
            {
                txt_DyeingCode.Text = null;
            }
        }

        private void btn_DyeCodeDelete_Click(object sender, EventArgs e)
        {
            if (dgv_Dye_Code.SelectedRows.Count == 0)
            {
                FADM_Form.CustomMessageBox.Show("请先选择操作行", "操作异常", MessageBoxButtons.OK, false);
                return;
            }
            string P_str_sql = "select COUNT(*) from formula_head where DyeingCode in( select DyeingCode from dyeing_code where Code = '" + txt_Dye_Code.Text + "') and State = '已滴定配方'";
            DataTable P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            if (P_dt.Rows.Count > 0)
            {
                if (P_dt.Rows[0][0].ToString() != "0")
                {
                    if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("此工艺已有完成滴液记录，不能修改",
                                        "温馨提示", MessageBoxButtons.OK, false))
                    {
                        return;
                    }
                }
            }

            P_str_sql = "DELETE FROM dyeing_process" +
                               " WHERE Code = '" + txt_Dye_Code.Text + "';";
            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

            //重新排序
            P_str_sql = "Select DyeingCode FROM dyeing_code" +
                               " WHERE Code = '" + txt_Dye_Code.Text + "' group by DyeingCode;";
            P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

            foreach (DataRow dr in P_dt.Rows)
            {
                //获取要删除是第几个步骤，删除后，后边Step就要减1
                P_str_sql = "Select Step,Type FROM dyeing_code" +
                               " WHERE Code = '" + txt_Dye_Code.Text + "' and DyeingCode = '" + dr[0].ToString() + "';";
                DataTable P_dt1 = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                //如果删除的是染色工艺，会把整个大工艺删除
                if (P_dt1.Rows[0][1].ToString() == "1")
                {
                    P_str_sql = "DELETE FROM dyeing_code" +
                                   " WHERE  DyeingCode = '" + dr[0].ToString() + "';";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                }
                else
                {

                    //先删除
                    P_str_sql = "DELETE FROM dyeing_code" +
                                   " WHERE Code = '" + txt_Dye_Code.Text + "' and DyeingCode = '" + dr[0].ToString() + "';";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                    //修改比删除Step大的序号
                    P_str_sql = "Update dyeing_code Set Step = Step - 1" +
                                   " WHERE  DyeingCode = '" + dr[0].ToString() + "' and Step >" + P_dt1.Rows[0][0].ToString() + " ;";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                }
            }

            //
            FADM_Object.Communal._fadmSqlserver.GetData("DELETE FROM dyeing_Remark where Code = '" + txt_Dye_Code.Text + "';");

            DyeingCodeShow("");
            UpdateListAndDyeCode();
            try
            {
                DyeCodeShow(dgv_Dye_Code.Rows[dgv_Dye_Code.CurrentCell.RowIndex - 1].Cells[0].Value.ToString());
            }
            catch
            {
                DyeCodeShow("");
            }
        }

        private void dgv_DyeDetails_CurrentCellChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dgv_DyeDetails.CurrentCell != null && dgv_DyeDetails.CurrentCell.Value != null)
            //    {
            //        txt_Dye_Code.Text = dgv_DyeDetails.CurrentCell.Value.ToString();
            //        DyeCodeShow(txt_Dye_Code.Text);
            //    }
            //}
            //catch
            //{
            //    txt_Dye_Code.Text = null;
            //    //dgv_DyeDetails.Rows.Clear();
            //}
        }

        private void dgv_Dye_Code_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Dye_Code.CurrentCell != null)
                {
                    txt_Dye_Code.Text = dgv_Dye_Code.CurrentCell.Value.ToString();
                    if (txt_Dye_Code.Text == "")
                    {
                        txt_Remark.Text = "";
                    }
                    else
                    {
                        if (directoryDyeCode.ContainsKey(txt_Dye_Code.Text))
                            txt_Remark.Text = directoryDyeCode[txt_Dye_Code.Text];
                        else
                        {
                            txt_Remark.Text = "";
                        }
                    }

                    ChildDyeDataShow("");
                    btn_DyeingProcessAdd.Enabled = true;
                    btn_DyeingProcessUpdate.Enabled = true;
                    btn_DyeingProcessDelete.Enabled = true;
                    btn_DyeCodeDelete.Enabled = true;
                    dgv_Child_DyeData.ClearSelection();
                }
            }
            catch
            {
                txt_Dye_Code.Text = null;
                txt_Remark.Text = "";
                ChildDyeDataShow("");
                btn_DyeingProcessAdd.Enabled = false;
                btn_DyeingProcessUpdate.Enabled = false;
                btn_DyeingProcessDelete.Enabled = false;
                btn_DyeCodeDelete.Enabled = false;
                dgv_Child_DyeData.ClearSelection();
            }
        }

        private void dgv_DyeDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_DyeDetails.CurrentCell != null && dgv_DyeDetails.CurrentCell.Value != null)
                {
                    txt_Dye_Code.Text = dgv_DyeDetails.CurrentCell.Value.ToString();
                    DyeCodeShow(txt_Dye_Code.Text);

                    
                }
            }
            catch
            {
                txt_Dye_Code.Text = null;
                //dgv_DyeDetails.Rows.Clear();
            }
        }

        private void dgv_DyeingCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_Dye_Code_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_DyeingCode_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txt_DyeingCode.Enabled && txt_DyeingCode.Text != null && txt_DyeingCode.Text != "")
                    {
                        //检索是否存在这个调液流程代码
                        for (int i = 0; i < dgv_DyeingCode.Rows.Count; i++)
                        {
                            if (txt_DyeingCode.Text == dgv_DyeingCode[0, i].Value.ToString())
                            {
                                if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("代码重复,请重新输入",
                                    "温馨提示", MessageBoxButtons.OK, false))
                                {
                                    txt_DyeingCode.Text = null;
                                    txt_DyeingCode.Focus();
                                    return;
                                }
                            }
                        }
                        //将新的调液流程代码写入数据库
                        string P_str_sql = "INSERT INTO dyeing_code" +
                                           " (DyeingCode) VALUES( '" + txt_DyeingCode.Text + "' );";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                        //更新调液流程代码表
                        DyeingCodeShow(txt_DyeingCode.Text);

                        //设置添加按钮焦点
                        btn_DyeingProcessAdd.Focus();
                    }
                    break;
                default:
                    break;
            }
        }

        private void txt_Dye_Code_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txt_Dye_Code.Enabled && txt_Dye_Code.Text != null && txt_Dye_Code.Text != "")
                    {
                        //检索是否存在这个代码
                        for (int i = 0; i < dgv_Dye_Code.Rows.Count; i++)
                        {
                            if (txt_Dye_Code.Text == dgv_Dye_Code[0, i].Value.ToString())
                            {
                                if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("代码重复,请重新输入",
                                    "温馨提示", MessageBoxButtons.OK, false))
                                {
                                    txt_Dye_Code.Text = null;
                                    txt_Dye_Code.Focus();
                                    return;
                                }
                            }
                        }

                        //
                        //if (b_NewDye)
                        //{
                        //    string P_str_sql = "INSERT INTO dyeing_process" +
                        //                       " (Code,Type) VALUES( '" + txt_Dye_Code.Text + "' ,1);";
                        //    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                        //}
                        //else
                        //{
                        //    string P_str_sql = "INSERT INTO dyeing_process" +
                        //                   " (Code,Type) VALUES( '" + txt_Dye_Code.Text + "' ,2);";
                        //    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                        //}

                        //////更新调液流程代码表
                        //UpdateListAndDyeCode();
                        //DyeCodeShow(txt_Dye_Code.Text);


                        ////打开按钮
                        //btn_DyeingProcessAdd.Enabled = true;
                        //btn_DyeingProcessDelete.Enabled = true;
                        //btn_DyeingProcessUpdate.Enabled = true;
                        //txt_DyeingCode.Enabled = false;
                        txt_Dye_Code.Enabled = false;

                        //设置添加按钮焦点
                        txt_Remark.Enabled = true;
                        txt_Remark.Focus();
                    }
                    break;
                default:
                    break;
            }
        }

        private void dgv_DyeDetails_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //新增行下拉选项
            {
                DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_DyeDetails[0, dgv_DyeDetails.Rows.Count - 1];
                dd.DataSource = listCode;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_DyeingCode.Text == "")
                {
                    if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("染固色代码为空，不能保存",
                                        "温馨提示", MessageBoxButtons.OK, false))
                    {
                        return;
                    }
                }
                string P_str_sql = "select COUNT(*) from formula_head where DyeingCode  = '" + txt_DyeingCode.Text + "' and State = '已滴定配方'";
                DataTable P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                //if (P_dt.Rows[0][0].ToString() != "0")
                //{
                //    if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("此工艺已有完成滴液记录，不能修改",
                //                        "温馨提示", MessageBoxButtons.OK, false))
                //    {
                //        return;
                //    }
                //}

                if (dgv_DyeDetails.Rows.Count == 1)
                {
                    if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("不存在处理工艺，请添加后再保存",
                                        "温馨提示", MessageBoxButtons.OK, false))
                    {
                        return;
                    }
                }
                //判断是否空行
                for (int i = 0; i < dgv_DyeDetails.Rows.Count - 1; i++)
                {
                    string s = dgv_DyeDetails.Rows[i].Cells[0].Value.ToString();
                    if (s == "")
                    {
                        if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("存在空行，请删除后再保存",
                                            "温馨提示", MessageBoxButtons.OK, false))
                        {
                            return;
                        }
                    }
                }
                //查询所有染色工艺代码
                P_str_sql = "SELECT Code  FROM dyeing_process WHERE Type = 1 group by Code;";
                DataTable P_dt_Dyeingprocess = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                int n_Count = 0;
                //先判断染色工艺是否有多个，现在只允许一个
                for (int i = 0; i < dgv_DyeDetails.Rows.Count - 1; i++)
                {
                    string s = dgv_DyeDetails.Rows[i].Cells[0].Value.ToString();
                    for (int k = 0; k < P_dt_Dyeingprocess.Rows.Count; k++)
                    {
                        if (s == P_dt_Dyeingprocess.Rows[k][0].ToString())
                        {
                            n_Count++;
                            break;
                        }
                    }
                }
                //判断第一个工艺是否染色工艺
                if (dgv_DyeDetails.Rows.Count > 1)
                {
                    string s = dgv_DyeDetails.Rows[0].Cells[0].Value.ToString();
                    P_str_sql = "SELECT Code  FROM dyeing_process WHERE Type = 1 and Code = '" + s + "';";
                    P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                    if (P_dt.Rows.Count == 0)
                    {
                        if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("第一个工艺不是染色工艺，输入错误，请重新输入",
                                        "温馨提示", MessageBoxButtons.OK, false))
                        {
                            return;
                        }
                    }
                }
                if (n_Count > 1)
                {
                    if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("染色工艺多于1个，请删除后再保存",
                                        "温馨提示", MessageBoxButtons.OK, false))
                    {
                        return;
                    }
                }
                //判断第一个工艺是否染色工艺
                if (dgv_DyeDetails.Rows.Count > 7)
                {
                    if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("最多只能输入5个后处理工艺，请重新输入",
                                    "温馨提示", MessageBoxButtons.OK, false))
                    {
                        return;
                    }
                }
                //判断工艺加药量之和是否100%
                for (int i = 0; i < dgv_DyeDetails.Rows.Count - 1; i++)
                {
                    string s = dgv_DyeDetails.Rows[i].Cells[0].Value.ToString();
                    P_str_sql = "SELECT SUM(ProportionOrTime)  FROM dyeing_process WHERE" +
                                   " Code = '" + s + "' and TechnologyName in('加A','加B','加C','加D','加E','加F','加G','加H','加I','加J','加K','加L','加M','加N') Group By TechnologyName ; ";
                    P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                    foreach (DataRow dr in P_dt.Rows)
                    {
                        if (Convert.ToInt32(dr[0].ToString()) != 100)
                        {
                            if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("子工艺添加量之和不等于100，请重新输入后再保存",
                                         "温馨提示", MessageBoxButtons.OK, false))
                            {
                                return;
                            }
                        }
                    }
                }
                //判断工艺加药量之和是否100%
                for (int i = 0; i < dgv_DyeDetails.Rows.Count - 1; i++)
                {
                    string s = dgv_DyeDetails.Rows[i].Cells[0].Value.ToString();
                    P_str_sql = "SELECT *  FROM dyeing_process WHERE" +
                                   " Code = '" + s + "' and StepNum is not null; ";
                    P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

                    if (P_dt.Rows.Count == 0)
                    {
                        if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("流程工艺不存在子工艺工艺，请重新输入后再保存",
                                     "温馨提示", MessageBoxButtons.OK, false))
                        {
                            return;
                        }
                    }
                }

                P_str_sql = "select COUNT(*) from formula_head where DyeingCode  = '" + txt_DyeingCode.Text + "' and State = '已滴定配方'";
                P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                if (P_dt.Rows[0][0].ToString() != "0")
                {
                    //if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("此工艺已有完成滴液记录，不能修改",
                    //                    "温馨提示", MessageBoxButtons.OK, false))
                    //{
                    //    return;
                    //}

                    ReName rename = new ReName(1, txt_DyeingCode.Text);
                    rename.ShowDialog();
                    if (rename.DialogResult != DialogResult.OK)
                    {
                        return;
                    }




                    txt_DyeingCode.Text = FADM_Object.Communal._s_reName;
                }

                //删除原有固染色流程记录
                P_str_sql = "DELETE FROM dyeing_code" +
                                   " WHERE DyeingCode = '" + txt_DyeingCode.Text + "';";
                FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);

                //一个染色工艺
                if (n_Count == 1)
                {
                    P_str_sql = "INSERT INTO dyeing_code (DyeingCode, Type," +
                                                   " Step, Code) VALUES('" + txt_DyeingCode.Text + "',1,1,'" + dgv_DyeDetails.Rows[0].Cells[0].Value.ToString() + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                    for (int i = 1; i < dgv_DyeDetails.Rows.Count - 1; i++)
                    {
                        P_str_sql = "INSERT INTO dyeing_code (DyeingCode, Type," +
                                                   " Step, Code) VALUES('" + txt_DyeingCode.Text + "',2," + i.ToString() + ",'" + dgv_DyeDetails.Rows[i].Cells[0].Value.ToString() + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                    }
                }
                //全部是后处理工艺
                else
                {
                    for (int i = 1; i < dgv_DyeDetails.Rows.Count; i++)
                    {
                        P_str_sql = "INSERT INTO dyeing_code (DyeingCode, Type," +
                                                   " Step, Code) VALUES('" + txt_DyeingCode.Text + "',2," + i.ToString() + ",'" + dgv_DyeDetails.Rows[i].Cells[0].Value.ToString() + "');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                    }
                }
                DyeingCodeShow(FADM_Object.Communal._s_reName);
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    new SmartDyeing.FADM_Object.MyAlarm("保存完成", 0);
                else
                    new SmartDyeing.FADM_Object.MyAlarm("Save completed", 0);
            }
            catch { }
        }

        private void dgv_DyeDetails_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    {
                        try
                        {
                            dgv_DyeDetails.Rows.Remove(dgv_DyeDetails.CurrentRow);
                        }
                        catch
                        {
                        }
                    }
                    break;
                case Keys.Insert:
                    try
                    {
                        dgv_DyeDetails.Rows.Insert(dgv_DyeDetails.CurrentRow.Index);
                        //新增行下拉选项
                        {
                            DataGridViewComboBoxCell dd = (DataGridViewComboBoxCell)dgv_DyeDetails[0, dgv_DyeDetails.CurrentRow.Index - 1];
                            dd.DataSource = listCode;
                        }
                    }
                    catch
                    {

                    }
                    break;
                default:

                    break;
            }
        }

        private void txt_Remark_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txt_Remark.Enabled && txt_Remark.Text != null && txt_Remark.Text != "")
                    {
                        //检索是否存在这个代码
                        for (int i = 0; i < dgv_Dye_Code.Rows.Count; i++)
                        {
                            if (txt_Dye_Code.Text == dgv_Dye_Code[0, i].Value.ToString())
                            {
                                if (DialogResult.OK ==  FADM_Form.CustomMessageBox.Show("代码重复,请重新输入",
                                    "温馨提示", MessageBoxButtons.OK, false))
                                {
                                    txt_Dye_Code.Text = null;
                                    txt_Dye_Code.Focus();
                                    return;
                                }
                            }
                        }


                        FADM_Object.Communal._fadmSqlserver.ReviseData("Delete from dyeing_Remark where Code = '" +
                                                txt_Dye_Code.Text + "' ;");
                        //写入数据库
                        string P_str_sql1 = "INSERT INTO dyeing_Remark" +
                                               " (Code,Remark) VALUES( '" + txt_Dye_Code.Text + "' ,'"+txt_Remark.Text+"');";
                        FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql1);

                        //
                        if (b_NewDye)
                        {
                            string P_str_sql = "INSERT INTO dyeing_process" +
                                               " (Code,Type) VALUES( '" + txt_Dye_Code.Text + "' ,1);";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                        }
                        else
                        {
                            string P_str_sql = "INSERT INTO dyeing_process" +
                                           " (Code,Type) VALUES( '" + txt_Dye_Code.Text + "' ,2);";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(P_str_sql);
                        }

                        ////更新调液流程代码表
                        UpdateListAndDyeCode();
                        DyeCodeShow(txt_Dye_Code.Text);


                        //打开按钮
                        btn_DyeingProcessAdd.Enabled = true;
                        btn_DyeingProcessDelete.Enabled = true;
                        btn_DyeingProcessUpdate.Enabled = true;
                        txt_DyeingCode.Enabled = false;
                        txt_Dye_Code.Enabled = false;
                        txt_Remark.Enabled = false;

                        //设置添加按钮焦点
                        btn_DyeingProcessAdd.Focus();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
