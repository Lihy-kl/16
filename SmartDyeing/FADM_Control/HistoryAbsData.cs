using CHNSpec.Device.Bluetooth;
using CHNSpec.Device.Models;
using CHNSpec.Device.Models.Enums;
using BLECode;
using SmartDyeing.FADM_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslControls;
using HslControls.Charts;
using System.Windows.Forms.DataVisualization.Charting;
using Lib_DataBank.MySQL;
using SmartDyeing.FADM_Auto;
using System.Resources;
using System.Reflection.Emit;
using Lib_File;
using SmartDyeing.FADM_Object;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmartDyeing.FADM_Control
{
    public partial class HistoryAbsData : UserControl
    {
        DateTime[] times;



        Main _main;
        public HistoryAbsData(Main m)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            _main = m;
            //if (main.BtnUserSwitching.Text == "管理用户")
            //{
            //    btn_Record_Delete.Visible = true;
            //}
            ShowHeader();
            InitChart();

        }

        private void btn_Record_Select_Click(object sender, EventArgs e)
        {
            DropRecordHeadShow();
        }

        private void btn_Record_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_DropRecord.CurrentRow != null)
                {

                    //如果选中行
                    if (dgv_DropRecord.SelectedRows.Count > 0)
                    {

                        string s_finishtime = dgv_DropRecord.CurrentRow.Cells[3].Value.ToString();
                        string s_assistantCode = dgv_DropRecord.CurrentRow.Cells[0].Value.ToString();

                        string s_sql = "SELECT * FROM history_abs" +
                                   " Where FinishTime = '" + s_finishtime + "';";
                        DataTable dt_history_abs = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        if (dt_history_abs.Rows.Count > 0)
                        {
                            DateTime dateTime = Convert.ToDateTime(dt_history_abs.Rows[0]["FinishTime"].ToString());
                            string s_time = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                            s_time = s_time.Replace(" ","");
                            s_time = s_time.Replace("-", "");
                            s_time = s_time.Replace(":", "");

                            s_sql = "SELECT *  FROM assistant_details WHERE AssistantCode = '" + s_assistantCode + "';";
                            DataTable dt_assistant_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                            //MessageBox.Show(s_time);
                            string s_assName = "";
                            if(dt_assistant_details.Rows.Count>0)
                            {
                                s_assName = dt_assistant_details.Rows[0]["AssistantName"].ToString();
                            }
                            string s_data = dt_history_abs.Rows[0]["Abs"] is DBNull ? "" : dt_history_abs.Rows[0]["Abs"].ToString();
                            if (s_data != "")
                                s_data = s_data.Substring(0, s_data.Length - 2);
                            string[] sa_arr = s_data.Split('/');

                            if (s_data != "")
                            {
                                using (StreamWriter writer = new StreamWriter(FADM_Object.Communal._s_absPath + s_assName+"-"+ dt_history_abs.Rows[0]["BottleNum"].ToString()+"-"+s_time + ".JTDAT"))
                                {
                                    string s_h = "                \"Time\":\" " + dateTime.ToString("yyyy-MM-dd    HH:mm:ss").Substring(2, 17) + "\",\n";
                                    string s = "[";
                                    for (int i = 0; i < sa_arr.Count(); i++)
                                    {

                                        s += "[" + (Convert.ToInt32(dt_history_abs.Rows[0]["StartWave"]) + i * Convert.ToInt32(dt_history_abs.Rows[0]["IntWave"])) + "," + sa_arr[i] + "],";
                                    }
                                    s = s.Substring(0, s.Length - 1);
                                    s += "]";

                                    string s_w = "{\n" + s_h + "                " + "\"Data\":" + s + "\n}";
                                    //输出抬头明细
                                    writer.WriteLine(s_w);

                                    FADM_Form.CustomMessageBox.Show("导出成功", "温馨提示", MessageBoxButtons.OK, true);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show("导出失败", "温馨提示", MessageBoxButtons.OK, true);
            }

            //if (FADM_Object.Communal._s_operator != "管理用户" && FADM_Object.Communal._s_operator != "工程师")
            //{
            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //        FADM_Form.CustomMessageBox.Show("非管理员用户不能删除！", "温馨提示", MessageBoxButtons.OK, false);
            //    else
            //        FADM_Form.CustomMessageBox.Show("Non administrator users cannot delete！", "Tips", MessageBoxButtons.OK, false);
            //    btn_Record_Delete.Visible = false;
            //    return;
            //}
            //try
            //{
            //    if (dgv_DropRecord.CurrentRow != null)
            //    {
            //        if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //        {
            //            DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("确定删除吗?", "删除历史记录", MessageBoxButtons.YesNo, true);

            //            if (dialogResult == DialogResult.Yes)
            //            {
            //                //如果选中行
            //                if (dgv_DropRecord.SelectedRows.Count > 0)
            //                {
            //                    string s_formulaCode = dgv_DropRecord.CurrentRow.Cells[0].Value.ToString();
            //                    string s_versionNum = dgv_DropRecord.CurrentRow.Cells[1].Value.ToString();
            //                    string s_finishTime = dgv_DropRecord.CurrentRow.Cells[2].Value.ToString();
            //                    string s_cupNum = dgv_DropRecord.CurrentRow.Cells[3].Value.ToString();
            //                    string s_batch = dgv_DropRecord.CurrentRow.Cells[4].Value.ToString();

            //                    //string P_str_sql = "SELECT BatchName FROM history_head WHERE" +
            //                    //                   " FormulaCode = '" + FormulaCode + "' AND" +
            //                    //                   " VersionNum = '" + VersionNum + "' AND" +
            //                    //                   //" FinishTime = '" + FinishTime + "' AND" +
            //                    //                   " CupNum = " + CupNum + ";";

            //                    //DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

            //                    //string P_str_batch = Convert.ToString(_dt_data.Rows[0][_dt_data.Columns[0]]);

            //                    string s_sql = "DELETE FROM history_head WHERE" +
            //                                " FormulaCode = '" + s_formulaCode + "' AND" +
            //                                " VersionNum = '" + s_versionNum + "' AND" +
            //                                //" FinishTime = '" + FinishTime + "' AND" +
            //                                " BatchName = '" + s_batch + "' AND" +
            //                                " CupNum = " + s_cupNum + ";";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            //                    s_sql = "DELETE FROM history_details WHERE" +
            //                                " FormulaCode = '" + s_formulaCode + "' AND" +
            //                                " VersionNum = '" + s_versionNum + "' AND" +
            //                                " BatchName = '" + s_batch + "' AND" +
            //                                " CupNum = " + s_cupNum + ";";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            //                    s_sql = "DELETE FROM history_dye WHERE" +
            //                                " FormulaCode = '" + s_formulaCode + "' AND" +
            //                                " VersionNum = '" + s_versionNum + "' AND" +
            //                                " BatchName = '" + s_batch + "' AND" +
            //                                " CupNum = " + s_cupNum + ";";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            //                }
            //                //按照时间删除
            //                else
            //                {
            //                    //先把时间段内数据查询出来
            //                    string s_str = "Select BatchName from history_head Where ";
            //                    if (dt_Record_Start.Text != null && dt_Record_Start.Text != "")
            //                    {
            //                        s_str += (" FinishTime >= '" + dt_Record_Start.Text + "' AND");
            //                    }
            //                    else
            //                    {
            //                        return;
            //                    }

            //                    if (dt_Record_End.Text != null && dt_Record_End.Text != "")
            //                    {
            //                        s_str += (" FinishTime <= '" + dt_Record_End.Text + "' ");
            //                    }
            //                    else
            //                    {
            //                        return;
            //                    }

            //                    //
            //                    string s_sql = "DELETE FROM history_dye WHERE BatchName  in(" + s_str + ") ;";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            //                    s_sql = "DELETE FROM history_details WHERE BatchName  in(" + s_str + ") ;";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            //                    s_sql = "DELETE FROM history_head WHERE BatchName  in(" + s_str + ") ;";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            //                }

            //                DropRecordHeadShow();

            //            }
            //        }
            //        else
            //        {
            //            DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Are you sure to delete it?", "Delete History", MessageBoxButtons.YesNo, true);

            //            if (dialogResult == DialogResult.Yes)
            //            {
            //                //如果选中行
            //                if (dgv_DropRecord.SelectedRows.Count > 0)
            //                {
            //                    string s_formulaCode = dgv_DropRecord.CurrentRow.Cells[0].Value.ToString();
            //                    string s_versionNum = dgv_DropRecord.CurrentRow.Cells[1].Value.ToString();
            //                    string s_finishTime = dgv_DropRecord.CurrentRow.Cells[2].Value.ToString();
            //                    string s_cupNum = dgv_DropRecord.CurrentRow.Cells[3].Value.ToString();
            //                    string s_batch = dgv_DropRecord.CurrentRow.Cells[4].Value.ToString();

            //                    //string P_str_sql = "SELECT BatchName FROM history_head WHERE" +
            //                    //                   " FormulaCode = '" + FormulaCode + "' AND" +
            //                    //                   " VersionNum = '" + VersionNum + "' AND" +
            //                    //                   //" FinishTime = '" + FinishTime + "' AND" +
            //                    //                   " CupNum = " + CupNum + ";";

            //                    //DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

            //                    //string P_str_batch = Convert.ToString(_dt_data.Rows[0][_dt_data.Columns[0]]);

            //                    string s_sql = "DELETE FROM history_head WHERE" +
            //                                " FormulaCode = '" + s_formulaCode + "' AND" +
            //                                " VersionNum = '" + s_versionNum + "' AND" +
            //                                //" FinishTime = '" + FinishTime + "' AND" +
            //                                " BatchName = '" + s_batch + "' AND" +
            //                                " CupNum = " + s_cupNum + ";";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            //                    s_sql = "DELETE FROM history_details WHERE" +
            //                                " FormulaCode = '" + s_formulaCode + "' AND" +
            //                                " VersionNum = '" + s_versionNum + "' AND" +
            //                                " BatchName = '" + s_batch + "' AND" +
            //                                " CupNum = " + s_cupNum + ";";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            //                    s_sql = "DELETE FROM history_dye WHERE" +
            //                                " FormulaCode = '" + s_formulaCode + "' AND" +
            //                                " VersionNum = '" + s_versionNum + "' AND" +
            //                                " BatchName = '" + s_batch + "' AND" +
            //                                " CupNum = " + s_cupNum + ";";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            //                }
            //                //按照时间删除
            //                else
            //                {
            //                    //先把时间段内数据查询出来
            //                    string s_str = "Select BatchName from history_head Where ";
            //                    if (dt_Record_Start.Text != null && dt_Record_Start.Text != "")
            //                    {
            //                        s_str += (" FinishTime >= '" + dt_Record_Start.Text + "' AND");
            //                    }
            //                    else
            //                    {
            //                        return;
            //                    }

            //                    if (dt_Record_End.Text != null && dt_Record_End.Text != "")
            //                    {
            //                        s_str += (" FinishTime <= '" + dt_Record_End.Text + "' ");
            //                    }
            //                    else
            //                    {
            //                        return;
            //                    }

            //                    //
            //                    string s_sql = "DELETE FROM history_dye WHERE BatchName  in(" + s_str + ") ;";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            //                    s_sql = "DELETE FROM history_details WHERE BatchName  in(" + s_str + ") ;";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

            //                    s_sql = "DELETE FROM history_head WHERE BatchName  in(" + s_str + ") ;";
            //                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            //                }

            //                DropRecordHeadShow();

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    FADM_Form.CustomMessageBox.Show(ex.Message, "btn_Record_Delete_Click", MessageBoxButtons.OK, true);
            //}
        }

        private void ShowHeader()
        {
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                string[] sa_lineName = { "波长", "标样", "试样" };
                for (int i = 0; i < sa_lineName.Count(); i++)
                {
                    dgv_Details.Columns.Add("", "");
                    dgv_Details.Columns[i].HeaderCell.Value = sa_lineName[i];
                    //关闭点击标题自动排序功能
                    dgv_Details.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //dgv_Details.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                //设置标题字体
                dgv_Details.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);
                //设置内容字体
                dgv_Details.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
            }
            else
            {
                string[] sa_lineName = { "Wave length", "Standard sample", "Test sample" };
                for (int i = 0; i < sa_lineName.Count(); i++)
                {
                    dgv_Details.Columns.Add("", "");
                    dgv_Details.Columns[i].HeaderCell.Value = sa_lineName[i];
                    //关闭点击标题自动排序功能
                    dgv_Details.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //dgv_Details.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                //设置标题字体
                dgv_Details.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);
                //设置内容字体
                dgv_Details.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
            }
            ////设置标题宽度
            dgv_Details.Columns[0].Width = 100;
            dgv_Details.Columns[1].Width = 130;
            dgv_Details.Columns[2].Width = 130;
            //dgv_Details.Columns[3].Width = 120;
            //dgv_Details.Columns[4].Width = 200;
            //dgv_Details.Columns[5].Width = 60;
            //dgv_Details.Columns[6].Width = 60;
            //dgv_Details.Columns[7].Width = 60;
            //dgv_Details.Columns[8].Width = 100;
            //dgv_Details.Columns[9].Width = 100;
            //dgv_Details.Columns[10].Width = 100;
            //dgv_Details.Columns[11].Width = 100;
            //dgv_Details.Columns[12].Width = 100;
            //dgv_Details.Columns[13].Width = 70;
            //dgv_Details.Columns[14].Width = 70;
            //dgv_Details.Columns[15].Width = 100;
            //dgv_Details.Columns[16].Width = 60;
            //dgv_Details.Columns[17].Width = 100;
            //dgv_Details.Columns[18].Width = 100;
            //设置标题居中显示
            dgv_Details.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            //设置内容居中显示
            dgv_Details.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            //设置行高
            dgv_Details.RowTemplate.Height = 30;


            dgv_DropRecord.Columns.Add("", "");
            dgv_DropRecord.Columns.Add("", "");
            dgv_DropRecord.Columns.Add("", "");
            dgv_DropRecord.Columns.Add("", "");

            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                //设置标题文字
                dgv_DropRecord.Columns[0].HeaderCell.Value = "助剂代码";
                dgv_DropRecord.Columns[1].HeaderCell.Value = "工位";
                dgv_DropRecord.Columns[2].HeaderCell.Value = "瓶号";
                dgv_DropRecord.Columns[3].HeaderCell.Value = "时间/时期";
                //设置标题字体
                dgv_DropRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);
                //设置内容字体
                dgv_DropRecord.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);
            }
            else
            {
                dgv_DropRecord.Columns[0].HeaderCell.Value = "AssistantCode";
                dgv_DropRecord.Columns[1].HeaderCell.Value = "CupNumber";
                dgv_DropRecord.Columns[2].HeaderCell.Value = "BottleNumber";
                dgv_DropRecord.Columns[3].HeaderCell.Value = "Date/Time";
                //设置标题字体
                dgv_DropRecord.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 7.5F);
                //设置内容字体
                dgv_DropRecord.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
            }


            //设置标题宽度
            dgv_DropRecord.Columns[0].Width = 150;
            dgv_DropRecord.Columns[1].Width = 80;
            dgv_DropRecord.Columns[2].Width = 80;
            dgv_DropRecord.Columns[3].Width = 220;
            //if (dgv_FormulaData.Rows.Count > 5)
            //{
            //    dgv_DropRecord.Columns[4].Width = 515;
            //}
            //else
            //{
            //dgv_DropRecord.Columns[4].Width = 135;
            //}


            //关闭自动排序功能
            for (int i = 0; i < dgv_DropRecord.Columns.Count; i++)
            {
                dgv_DropRecord.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //设置标题居中显示
            dgv_DropRecord.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            //设置内容居中显示
            dgv_DropRecord.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            //设置行高
            dgv_DropRecord.RowTemplate.Height = 30;

            dgv_DropRecord.ClearSelection();

        }

        /// <summary>
        /// 显示滴液记录资料
        /// </summary>
        /// <returns>0:正常;-1异常</returns>
        private void DropRecordHeadShow()
        {
            try
            {
                dgv_DropRecord.Rows.Clear();

                string s_sql = null;
                DataTable dt_data = new DataTable();


                //获取配方浏览资料表头
                if (rdo_Record_Now.Checked)
                {
                    s_sql = "SELECT AssistantCode,CupNum, BottleNum, FinishTime,Stand FROM history_abs WHERE" +
                                " FinishTime > CONVERT(varchar,GETDATE(),23) And Type !=2 And Type !=4 ORDER BY FinishTime DESC;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else if (rdo_Record_All.Checked)
                {
                    s_sql = "SELECT AssistantCode,CupNum, BottleNum, FinishTime,Stand FROM history_abs " +
                                " WHERE FinishTime != ''  And Type !=2 And Type !=4 ORDER BY FinishTime DESC;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else
                {
                    string s_str = null;
                    if (txt_Record_CupNum.Text != null && txt_Record_CupNum.Text != "")
                    {
                        s_str = (" AssistantCode = '" + txt_Record_CupNum.Text + "' AND");
                    }
                    if (dt_Record_Start.Text != null && dt_Record_Start.Text != "")
                    {
                        s_str += (" FinishTime >= '" + dt_Record_Start.Text + "' AND");
                    }
                    else
                    {
                        return;
                    }

                    if (dt_Record_End.Text != null && dt_Record_End.Text != "")
                    {
                        s_str += (" FinishTime <= '" + dt_Record_End.Text + "' ");
                    }
                    else
                    {
                        return;
                    }

                    s_sql = "SELECT AssistantCode,CupNum, BottleNum, FinishTime,Stand FROM history_abs  Where " + s_str + " And Type !=2 And Type !=4 " +
                               " ORDER BY FinishTime DESC;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }

                //捆绑
                //dgv_DropRecord.DataSource = dt_data;

                //捆绑
                for (int i = 0; i < dt_data.Rows.Count; i++)
                {
                    dgv_DropRecord.Rows.Add(dt_data.Rows[i][0].ToString(), dt_data.Rows[i][1].ToString(), dt_data.Rows[i][2].ToString(), dt_data.Rows[i][3].ToString());
                    if (!(dt_data.Rows[i]["Stand"] is DBNull))
                    {
                        if (dt_data.Rows[i]["Stand"].ToString().Contains("1"))
                        {
                            dgv_DropRecord.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }

                dgv_DropRecord.ClearSelection();
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "DropRecordHeadShow", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_DropRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_DropRecord_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_DropRecord.ClearSelection();
        }

        private void rdo_Record_All_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Record_All.Checked)
            {
                txt_Record_CupNum.Enabled = false;
                dt_Record_Start.Enabled = false;
                dt_Record_End.Enabled = false;
                //btn_Record_Delete.Visible = false;
            }
        }

        private void rdo_Record_condition_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Record_condition.Checked)
            {
                txt_Record_CupNum.Enabled = true;
                dt_Record_Start.Enabled = true;
                dt_Record_End.Enabled = true;

                if (FADM_Object.Communal._s_operator == "管理用户" || FADM_Object.Communal._s_operator == "工程师")
                {
                    btn_Record_Delete.Visible = true;
                }
            }
        }

        private void rdo_Record_Now_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Record_Now.Checked)
            {
                txt_Record_CupNum.Enabled = false;
                dt_Record_Start.Enabled = false;
                dt_Record_End.Enabled = false;
                //btn_Record_Delete.Visible = false;
            }
        }

        private void DetailsShow()
        {
            try
            {
                dgv_Details.Rows.Clear();
                txt_dL.Text = "";
                txt_dA.Text = "";
                txt_dB.Text = "";
                txt_dE.Text = "";
                txt_PL.Text = "";
                txt_PA.Text = "";
                txt_PB.Text = "";
                txt_SL.Text = "";
                txt_SA.Text = "";
                txt_SB.Text = "";
                txt_RealConcentration.Text = "";
                txt_BottleNum.Text = "";
                txt_BrewingData.Text = "";
                label20.Text = "";
                label15.Text = "";
                label16.Text = "";
                textBox4.Text = "";

                if (chart.Series.Count > 0)
                {
                    chart.Series.Clear();
                    chart.MouseMove -= new MouseEventHandler(chart1_MouseMove);
                    chart.MouseWheel -= new MouseEventHandler(chart1_MouseMove);
                }
                toolTip1.RemoveAll();

                //读取选中行对应的配方资料
                string s_finishtime = dgv_DropRecord.CurrentRow.Cells[3].Value.ToString();
                string s_sql = "SELECT * FROM history_abs" +
                                   " Where FinishTime = '" + s_finishtime + "';";
                DataTable dt_history_abs = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                string s_stand = "";
                string s_test = "";
                //查询标样数据
                if (dt_history_abs.Rows.Count > 0)
                {
                    string s_result = dt_history_abs.Rows[0]["Result"] is DBNull ? "" : dt_history_abs.Rows[0]["Result"].ToString();
                    if (s_result.Length > 4)
                        textBox1.Text = s_result.Substring(0, 4);
                    else
                        textBox1.Text = s_result;
                    textBox3.Text = dt_history_abs.Rows[0]["RealSampleDosage"].ToString();
                    textBox2.Text = dt_history_abs.Rows[0]["RealAdditivesDosage"].ToString();

                    string s_data = dt_history_abs.Rows[0]["Abs"] is DBNull ? "" : dt_history_abs.Rows[0]["Abs"].ToString();
                    if (s_data != "")
                        s_data = s_data.Substring(0, s_data.Length - 2);
                    string[] sa_arr = s_data.Split('/');
                    //判断是否是否标样
                    if (dt_history_abs.Rows[0]["Type"].ToString() == "3")
                    {
                        for (int i = 0; i < sa_arr.Count(); i++)
                        {
                            dgv_Details.Rows.Add(Convert.ToInt32(dt_history_abs.Rows[0]["StartWave"]) + i * Convert.ToInt32(dt_history_abs.Rows[0]["IntWave"]), sa_arr[i], "-");

                        }

                        s_stand = dt_history_abs.Rows[0]["Abs"] is DBNull ? "" : dt_history_abs.Rows[0]["Abs"].ToString();
                    }
                    else
                    {
                        s_test = dt_history_abs.Rows[0]["Abs"] is DBNull ? "" : dt_history_abs.Rows[0]["Abs"].ToString();
                        //先查询标准吸光度
                        s_sql = "SELECT *  FROM bottle_details WHERE BottleNum = '" + dt_history_abs.Rows[0]["BottleNum"].ToString() + "';";
                        DataTable dt_bottle = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                        if (dt_bottle.Rows.Count > 0)
                        {
                            if (!(dt_history_abs.Rows[0]["L"] is DBNull))
                                txt_PL.Text = Convert.ToDouble(dt_history_abs.Rows[0]["L"].ToString()).ToString("f2");
                            if (!(dt_history_abs.Rows[0]["A"] is DBNull))
                                txt_PA.Text = Convert.ToDouble(dt_history_abs.Rows[0]["A"].ToString()).ToString("f2");
                            if (!(dt_history_abs.Rows[0]["B"] is DBNull))
                                txt_PB.Text = Convert.ToDouble(dt_history_abs.Rows[0]["B"].ToString()).ToString("f2");
                            txt_BottleNum.Text = dt_history_abs.Rows[0]["BottleNum"].ToString();
                            txt_BrewingData.Text = dt_history_abs.Rows[0]["BrewingData"].ToString();
                            txt_RealConcentration.Text = dt_history_abs.Rows[0]["RealConcentration"].ToString();



                            s_sql = "SELECT *  FROM assistant_details WHERE AssistantCode = '" + dt_bottle.Rows[0]["AssistantCode"].ToString() + "';";
                            DataTable dt_assistant_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                            if (dt_assistant_details.Rows.Count > 0)
                            {
                                label20.Text = dt_assistant_details.Rows[0]["UnitOfAccount"].ToString();
                                textBox4.Text = dt_assistant_details.Rows[0]["AssistantName"].ToString();
                            }

                            string s_data1 = dt_assistant_details.Rows[0]["Abs"] is DBNull ? "" : dt_assistant_details.Rows[0]["Abs"].ToString();
                            if (s_data1 != "")
                            {
                                string s_L = dt_history_abs.Rows[0]["L"] is DBNull ? "" : dt_history_abs.Rows[0]["L"].ToString();
                                if (!(dt_history_abs.Rows[0]["L"] is DBNull) && !(dt_assistant_details.Rows[0]["L"] is DBNull))
                                {
                                    double db_dL = Convert.ToDouble(dt_history_abs.Rows[0]["L"].ToString()) - Convert.ToDouble(dt_assistant_details.Rows[0]["L"].ToString());
                                    txt_dL.Text = db_dL.ToString("f2");
                                    if(Math.Abs(db_dL) >0.3)
                                    {
                                        label15.Text = "不合格";
                                        label15.BackColor = Color.Red;

                                    }
                                    else
                                    {
                                        label15.Text = "合格";
                                        label15.BackColor = Color.Lime;
                                    }
                                    
                                }

                                string s_A = dt_history_abs.Rows[0]["A"] is DBNull ? "" : dt_history_abs.Rows[0]["A"].ToString();
                                if (!(dt_history_abs.Rows[0]["A"] is DBNull) && !(dt_assistant_details.Rows[0]["A"] is DBNull))
                                    txt_dA.Text = (Convert.ToDouble(dt_history_abs.Rows[0]["A"].ToString()) - Convert.ToDouble(dt_assistant_details.Rows[0]["A"].ToString())).ToString("f2");
                                string s_B = dt_history_abs.Rows[0]["B"] is DBNull ? "" : dt_history_abs.Rows[0]["B"].ToString();
                                if (!(dt_history_abs.Rows[0]["B"] is DBNull) && !(dt_assistant_details.Rows[0]["B"] is DBNull))
                                    txt_dB.Text = (Convert.ToDouble(dt_history_abs.Rows[0]["B"].ToString()) - Convert.ToDouble(dt_assistant_details.Rows[0]["B"].ToString())).ToString("f2");
                                if (!(dt_history_abs.Rows[0]["L"] is DBNull) && !(dt_assistant_details.Rows[0]["L"] is DBNull)
                                    && !(dt_history_abs.Rows[0]["A"] is DBNull) && !(dt_assistant_details.Rows[0]["A"] is DBNull)
                                    && !(dt_history_abs.Rows[0]["B"] is DBNull) && !(dt_assistant_details.Rows[0]["B"] is DBNull))
                                {
                                    double d_cmc = MyAbsorbance.CalculateCMC(Convert.ToDouble(dt_assistant_details.Rows[0]["L"].ToString()), Convert.ToDouble(dt_assistant_details.Rows[0]["A"].ToString()), Convert.ToDouble(dt_assistant_details.Rows[0]["B"].ToString()), Convert.ToDouble(dt_history_abs.Rows[0]["L"].ToString()), Convert.ToDouble(dt_history_abs.Rows[0]["A"].ToString()), Convert.ToDouble(dt_history_abs.Rows[0]["B"].ToString()), 2, 1);
                                    txt_dE.Text = d_cmc.ToString("f2");
                                    if (Math.Abs(d_cmc) > 0.3)
                                    {
                                        label16.Text = "不合格";
                                        label16.BackColor = Color.Red;

                                    }
                                    else
                                    {
                                        label16.Text = "合格";
                                        label16.BackColor = Color.Lime;
                                    }
                                }

                                s_data1 = s_data1.Substring(0, s_data1.Length - 2);

                               if(!(dt_assistant_details.Rows[0]["L"] is DBNull))
                                txt_SL.Text = Convert.ToDouble(dt_assistant_details.Rows[0]["L"].ToString()).ToString("f2");
                                if (!(dt_assistant_details.Rows[0]["A"] is DBNull))
                                    txt_SA.Text = Convert.ToDouble(dt_assistant_details.Rows[0]["A"].ToString()).ToString("f2");
                                if (!(dt_assistant_details.Rows[0]["B"] is DBNull))
                                    txt_SB.Text = Convert.ToDouble(dt_assistant_details.Rows[0]["B"].ToString()).ToString("f2");
                            }
                            string[] sa_arr1 = s_data1.Split('/');

                            //当标样数据不存在时，只显示试样
                            if (sa_arr1.Count() == 0 || s_data1 == "")
                            {
                                for (int i = 0; i < sa_arr.Count(); i++)
                                {
                                    dgv_Details.Rows.Add(Convert.ToInt32(dt_history_abs.Rows[0]["StartWave"]) + i * Convert.ToInt32(dt_history_abs.Rows[0]["IntWave"]), "-", sa_arr[i]);

                                }
                            }
                            else
                            {
                                //当数据相等时，一起显示
                                if (sa_arr1.Count() == sa_arr.Count())
                                {
                                    for (int i = 0; i < sa_arr1.Count(); i++)
                                    {
                                        dgv_Details.Rows.Add(Convert.ToInt32(dt_history_abs.Rows[0]["StartWave"]) + i * Convert.ToInt32(dt_history_abs.Rows[0]["IntWave"]), sa_arr1[i], sa_arr[i]);

                                    }
                                }
                                //当数据不一致时，直接返回，曲线也不显示
                                else
                                {
                                    FADM_Form.CustomMessageBox.Show("标样采集点与试样采集点不一致", "温馨提示", MessageBoxButtons.OK, true);
                                    return;
                                }
                            }

                            s_stand = dt_assistant_details.Rows[0]["Abs"] is DBNull ? "" : dt_assistant_details.Rows[0]["Abs"].ToString();
                            
                            int i_index = 0;
                            for (int i = 0; i < sa_arr.Count(); i++)
                            {
                                if (Convert.ToInt32(dt_history_abs.Rows[0]["StartWave"]) + i * Convert.ToInt32(dt_history_abs.Rows[0]["IntWave"]) >= 400)
                                {
                                    i_index = i;
                                    break;
                                }
                            }
                            double[] doublesS = new double[sa_arr.Count()- i_index];
                            double[] doublesT = new double[sa_arr.Count()- i_index];
                            for (int i = 0; i < sa_arr.Count() - i_index; i++)
                            {
                                doublesS[i] = Convert.ToDouble(sa_arr[i + i_index]);
                            }
                            for (int i = 0; i < sa_arr1.Count() - i_index; i++)
                            {
                                doublesT[i] = Convert.ToDouble(sa_arr1[i + i_index]);
                            }
                            textBox5.Text = MyAbsorbance.CalculateColorStrength(doublesS, doublesT).ToString("F2")+"%";
                            textBox6.Text = MyAbsorbance.KS(doublesS, doublesT).ToString("F2") + "%";
                        }
                    }

                }

                //显示表头

                if (dt_history_abs.Rows.Count > 0)
                {
                    //读取曲线数据并显示
                    //if (s_stand != "")
                    //{
                    //    Show(s_stand);
                    if (s_test != "")
                    {
                        InitChart();
                        Show1(s_test);
                        if (s_stand != "")
                            Show(s_stand);
                    }
                    //}



                    dgv_Details.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "DetailsShow", MessageBoxButtons.OK, true);
            }
        }

        private void dgv_DropRecord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // DetailsShow();
        }

        //private void Show(string s, DateTime dateTime)
        //{
        //    s = s.Substring(0, s.Length - 1);
        //    string[] arr = s.Split('@');

        //    float[] temp = new float[arr.Count()];
        //    DateTime[] times = new DateTime[arr.Count()];

        //    for (int i = 0; i < arr.Count(); i++)
        //    {
        //        temp[i] = Convert.ToSingle(arr[i]);
        //        times[i] = dateTime.AddSeconds((i - arr.Count()) * 30);
        //    }

        //    // 显示出数据信息来
        //    Invoke(new Action(() =>
        //    {
        //        // 设置曲线属性，名称，数据，颜色，是否平滑，格式化显示文本
        //        hslCurveHistory1.SetLeftCurve("温度", temp, Color.Blue, HslControls.CurveStyle.Curve, "{0:F1} ℃");
        //        hslCurveHistory1.SetDateTimes(times);
        //        hslCurveHistory1.RenderCurveUI(true);
        //    }));
        //}

        private void Show(string s_data)
        {
            s_data = s_data.Substring(0, s_data.Length - 2);
            string[] sa_arr = s_data.Split('/');

            //times = new DateTime[sa_arr.Count()];
            //for (int i = 0; i < sa_arr.Count(); i++)
            //{
            //    times[i] = dateTime.AddSeconds((i - sa_arr.Count()) * 30);
            //}

            AddSeries("标样", Color.Red);

            Series series = chart.Series[1];
            //series.Points.AddXY(0, 0);

            for (int i = 0; i < sa_arr.Count(); i++)
            {
                series.Points.AddXY(Convert.ToDouble(Lib_Card.Configure.Parameter.Other_StartWave + i * Lib_Card.Configure.Parameter.Other_IntWave), Convert.ToDouble(sa_arr[i]));
            }
            chart.MouseMove += new MouseEventHandler(chart1_MouseMove);

            chart.MouseWheel += new System.Windows.Forms.MouseEventHandler(chart1_Mouselheel);


        }

        private void Show1(string s_data)
        {
            s_data = s_data.Substring(0, s_data.Length - 2);
            string[] sa_arr = s_data.Split('/');

            //times = new DateTime[sa_arr.Count()];
            //for (int i = 0; i < sa_arr.Count(); i++)
            //{
            //    times[i] = dateTime.AddSeconds((i - sa_arr.Count()) * 30);
            //}

            AddSeries("试样", Color.Blue);

            Series series = chart.Series[0];
            //series.Points.AddXY(0, 0);

            for (int i = 0; i < sa_arr.Count(); i++)
            {
                series.Points.AddXY(Convert.ToDouble(Lib_Card.Configure.Parameter.Other_StartWave + i * Lib_Card.Configure.Parameter.Other_IntWave), Convert.ToDouble(sa_arr[i]));
            }
            chart.MouseMove += new MouseEventHandler(chart1_MouseMove);

            chart.MouseWheel += new System.Windows.Forms.MouseEventHandler(chart1_Mouselheel);


        }

        private void chart1_Mouselheel(object sender, MouseEventArgs e)
        {
            //var chart1 = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;
            //var xAxis = chart1.ChartAreas[0].AxisX;
            //var yAxis = chart1.ChartAreas[0].AxisY;

            //try
            //{
            //    if (e.Delta < 0) // Scrolled down.
            //    {
            //        xAxis.ScaleView.ZoomReset();
            //        yAxis.ScaleView.ZoomReset();
            //    }
            //    else if (e.Delta > 0) // Scrolled up.
            //    {
            //        var xMin = xAxis.ScaleView.ViewMinimum;
            //        var xMax = xAxis.ScaleView.ViewMaximum;
            //        var yMin = yAxis.ScaleView.ViewMinimum;
            //        var yMax = yAxis.ScaleView.ViewMaximum;

            //        var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
            //        var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
            //        var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
            //        var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

            //        xAxis.ScaleView.Zoom(posXStart, posXFinish);
            //        yAxis.ScaleView.Zoom(posYStart, posYFinish);
            //    }
            //}
            //catch { }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (chart.Series.Count > 0)
                {
                    chart.ChartAreas[0].CursorX.SetCursorPixelPosition(new PointF(e.X, e.Y), true);
                    chart.ChartAreas[0].CursorY.SetCursorPixelPosition(new PointF(e.X, e.Y), true);
                    if (Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) < (Lib_Card.Configure.Parameter.Other_StartWave + chart.Series[0].Points.Count * Lib_Card.Configure.Parameter.Other_IntWave) && Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) > 0)
                    {
                        int i_index = 0;
                        for (int i = 0; i < chart.Series[0].Points.Count; i++)
                        {
                            if (Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) < Lib_Card.Configure.Parameter.Other_StartWave + Lib_Card.Configure.Parameter.Other_IntWave * i)
                            {
                                i_index = i - 1; break;
                            }
                        }

                        if (Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) > Lib_Card.Configure.Parameter.Other_StartWave + Lib_Card.Configure.Parameter.Other_IntWave * (chart.Series[0].Points.Count - 1))
                        {
                            i_index = chart.Series[0].Points.Count - 1;
                        }
                        if (chart.Series.Count == 1)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                toolTip1.SetToolTip(chart, string.Format("波长:{0},试样吸光度值:{1}", chart.Series[0].Points[i_index].XValue,
                                chart.Series[0].Points[i_index].YValues[0]));
                            else
                                toolTip1.SetToolTip(chart, string.Format("Wave:{0},Test Abs:{1}", chart.Series[0].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].XValue,
                                chart.Series[0].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].YValues[0]));
                        }
                        else
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                toolTip1.SetToolTip(chart, string.Format("波长:{0},试样吸光度值:{1},标样吸光度值:{2}", chart.Series[0].Points[i_index].XValue,
                                chart.Series[0].Points[i_index].YValues[0],
                                chart.Series[1].Points[i_index].YValues[0]));
                            else
                                toolTip1.SetToolTip(chart, string.Format("Wave:{0},Test Abs:{1},standard Abs:{2}", chart.Series[0].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].XValue,
                                chart.Series[0].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].YValues[0],
                                chart.Series[1].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].YValues[0]));
                        }
                    }
                }
            }
            catch { }

        }

        private void AddSeries(string seriersName, Color serierscolor)
        {
            Series series = new Series(seriersName);
            //图表类型  设置为样条图曲线
            series.ChartType = SeriesChartType.Line;
            //series.IsXValueIndexed = true;
            series.XValueType = ChartValueType.Double;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerColor = Color.Black;
            //设置点的大小
            series.MarkerSize = 3;
            //设置曲线的颜色
            series.Color = serierscolor;
            //设置曲线宽度
            series.BorderWidth = 2;
            series.CustomProperties = "PointWidth=2";
            series.IsValueShownAsLabel = false;//是否显示点的值

            chart.Series.Add(series);
        }


        private void CreateChart()
        {
            chart = new Chart();
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(chart);
            chart.Dock = DockStyle.Fill;
            chart.Visible = true;

            ChartArea chartArea = new ChartArea();
            //chartArea.Name = "FirstArea";
            chartArea.AxisX.Interval = 40;
            //chartArea.AxisX.IntervalOffset = 40;
            chartArea.AxisX.Minimum = 300;

            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.SelectionColor = Color.SkyBlue;
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.AutoScroll = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.SelectionColor = Color.SkyBlue;

            //chartArea.CursorX.IntervalType = DateTimeIntervalType._b_auto;
            chartArea.AxisX.ScaleView.Zoomable = false;//是否可以放大X轴
            chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//启用X轴滚动条按钮

            chartArea.BackColor = Color.White;                      //背景色
            //chartArea.BackSecondaryColor = Color.White;                 //渐变背景色
            //chartArea.BackGradientStyle = GradientStyle.TopBottom;      //渐变方式
            //chartArea.BackHatchStyle = ChartHatchStyle.None;            //背景阴影
            chartArea.BorderDashStyle = ChartDashStyle.NotSet;          //边框线样式
            //chartArea.BorderWidth = 1;                                  //边框宽度
            chartArea.BorderColor = Color.Black;
            //chartArea.AxisX.ArrowStyle = AxisArrowStyle.Lines;//坐标轴是否有箭头
            //chartArea.AxisY.ArrowStyle = AxisArrowStyle.Lines;//坐标轴是否有箭头




            //chartArea.AxisX.
            chartArea.AxisX.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.Enabled = true;

            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                // Axis
                chartArea.AxisY.Title = @"吸光度";
                chartArea.AxisY.LineWidth = 2;
                chartArea.AxisY.LineColor = Color.Black;
                chartArea.AxisY.Enabled = AxisEnabled.True;


                chartArea.AxisX.Title = @"波长";
                chartArea.AxisX.IsLabelAutoFit = true;
                chartArea.AxisX.LabelAutoFitMinFontSize = 5;
                chartArea.AxisX.LabelStyle.Angle = -15;
            }
            else
            {
                // Axis
                chartArea.AxisY.Title = @"Abs";
                chartArea.AxisY.LineWidth = 2;
                chartArea.AxisY.LineColor = Color.Black;
                chartArea.AxisY.Enabled = AxisEnabled.True;


                chartArea.AxisX.Title = @"Wave length";
                chartArea.AxisX.IsLabelAutoFit = true;
                chartArea.AxisX.LabelAutoFitMinFontSize = 5;
                chartArea.AxisX.LabelStyle.Angle = -15;
            }

            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;        //show the last label


            chartArea.AxisX.LineWidth = 2;
            chartArea.AxisX.LineColor = Color.Black;
            chartArea.AxisX.Enabled = AxisEnabled.True;


            chartArea.Position.Height = 85;
            chartArea.Position.Width = 95;
            chartArea.Position.X = 0;
            chartArea.Position.Y = 13;

            chart.ChartAreas.Add(chartArea);
            chart.BackGradientStyle = GradientStyle.TopBottom;
            //图表的边框颜色、
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            //图表的边框线条样式
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            //图表边框线条的宽度
            chart.BorderlineWidth = 2;
            //图表边框的皮肤
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            //chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;

            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            chart.ChartAreas[0].AxisY.IsStartedFromZero = true;

            Legend Legend = new Legend("L1");
            Legend.DockedToChartArea = chart.Name;
            Legend.Font = new Font("宋体", 14.25F);
            chart.Legends.Add(Legend);
        }

        private void InitChart()
        {
            CreateChart();
        }

        private void dgv_DropRecord_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgv_DropRecord.CurrentRow != null)
                DetailsShow();
        }

        private void Btn_SetStand_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_DropRecord.CurrentRow != null)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("确定把当前记录设为标样吗?", "设定标样", MessageBoxButtons.YesNo, true);

                        if (dialogResult == DialogResult.Yes)
                        {
                            //如果选中行
                            if (dgv_DropRecord.SelectedRows.Count > 0)
                            {
                                string s_Ass = dgv_DropRecord.CurrentRow.Cells[0].Value.ToString();
                                string s_cupNum = dgv_DropRecord.CurrentRow.Cells[1].Value.ToString();
                                string s_bottleNum = dgv_DropRecord.CurrentRow.Cells[2].Value.ToString();
                                string s_finishTime = dgv_DropRecord.CurrentRow.Cells[3].Value.ToString();

                                //保存到助剂表
                                string s_sql = "SELECT *  FROM bottle_details WHERE BottleNum = '" + s_bottleNum + "';";
                                DataTable dt_bottle = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                if (dt_bottle.Rows.Count > 0)
                                {
                                    s_sql = "SELECT *  FROM history_abs WHERE FinishTime = '" + s_finishTime + "' And CupNum = " + s_cupNum + " And BottleNum =" + s_bottleNum + ";";
                                    DataTable dt_history_abs = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                    if (dt_history_abs.Rows.Count > 0)
                                    {

                                        FADM_Object.Communal._fadmSqlserver.ReviseData("Update assistant_details Set Abs = '" + dt_history_abs.Rows[0]["Abs"].ToString() + "',L=" + dt_history_abs.Rows[0]["L"].ToString() + ",A=" + dt_history_abs.Rows[0]["A"].ToString() + ",B=" + dt_history_abs.Rows[0]["B"].ToString() + " where AssistantCode = '" + dt_bottle.Rows[0]["AssistantCode"].ToString() + "';");

                                        //删除原来的标样
                                        FADM_Object.Communal._fadmSqlserver.ReviseData("Update history_abs Set Stand = 0  where AssistantCode = '" + s_Ass + "';");

                                        //标记为标样
                                        FADM_Object.Communal._fadmSqlserver.ReviseData("Update history_abs Set Stand = 1  WHERE FinishTime = '" + s_finishTime + "' And CupNum = " + s_cupNum + " And BottleNum =" + s_bottleNum + ";");

                                        DropRecordHeadShow();
                                    }
                                }
                            }
                            //按照时间删除
                            else
                            {
                                FADM_Form.CustomMessageBox.Show("请选择操作行", "DetailsShow", MessageBoxButtons.OK, true);
                            }


                        }
                    }
                    else
                    {
                        DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Are you sure to set the current record as the standard?", "Set standard sample", MessageBoxButtons.YesNo, true);

                        if (dialogResult == DialogResult.Yes)
                        {
                            //如果选中行
                            if (dgv_DropRecord.SelectedRows.Count > 0)
                            {
                                string s_cupNum = dgv_DropRecord.CurrentRow.Cells[1].Value.ToString();
                                string s_bottleNum = dgv_DropRecord.CurrentRow.Cells[2].Value.ToString();
                                string s_finishTime = dgv_DropRecord.CurrentRow.Cells[3].Value.ToString();

                                //保存到助剂表
                                string s_sql = "SELECT *  FROM bottle_details WHERE BottleNum = '" + s_bottleNum + "';";
                                DataTable dt_bottle = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                if (dt_bottle.Rows.Count > 0)
                                {
                                    s_sql = "SELECT *  FROM history_abs WHERE FinishTime = '" + s_finishTime + "' And CupNum = " + s_cupNum + " And BottleNum =" + s_bottleNum + ";";
                                    DataTable dt_history_abs = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                    if (dt_history_abs.Rows.Count > 0)
                                    {
                                        FADM_Object.Communal._fadmSqlserver.ReviseData("Update assistant_details Set Abs = '" + dt_history_abs.Rows[0]["Abs"].ToString() + "',L=" + dt_history_abs.Rows[0]["L"].ToString() + ",A=" + dt_history_abs.Rows[0]["A"].ToString() + ",B=" + dt_history_abs.Rows[0]["B"].ToString() + " where AssistantCode = '" + dt_bottle.Rows[0]["AssistantCode"].ToString() + "';");
                                    }
                                }


                            }
                            //按照时间删除
                            else
                            {
                                FADM_Form.CustomMessageBox.Show("Please select an action line", "DetailsShow", MessageBoxButtons.OK, true);
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "Btn_SetStand_Click", MessageBoxButtons.OK, true);
            }
        }
    }
}
