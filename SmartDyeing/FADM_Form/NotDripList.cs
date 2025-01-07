using Lib_File;
using SmartDyeing.FADM_Object;
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
    public partial class NotDripList : Form
    {
        public NotDripList()
        {
            InitializeComponent();

            dgv_BatchData.Columns.Add("", "");
            dgv_BatchData.Columns.Add("", "");
            dgv_BatchData.Columns.Add("", "");
            dgv_BatchData.Columns.Add("", "");
            dgv_BatchData.Columns.Add("", "");
            //设置标题文字
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                dgv_BatchData.Columns[0].HeaderCell.Value = "配方代码";
                dgv_BatchData.Columns[1].HeaderCell.Value = "版本";
                dgv_BatchData.Columns[2].HeaderCell.Value = "存档时间";
                dgv_BatchData.Columns[3].HeaderCell.Value = "杯位";
                dgv_BatchData.Columns[4].HeaderCell.Value = "类型";
            }
            else
            {
                dgv_BatchData.Columns[0].HeaderCell.Value = "RecipeCode";
                dgv_BatchData.Columns[1].HeaderCell.Value = "Version";
                dgv_BatchData.Columns[2].HeaderCell.Value = "Archive time";
                dgv_BatchData.Columns[3].HeaderCell.Value = "CupNumber";
                dgv_BatchData.Columns[4].HeaderCell.Value = "Type";
            }


            //设置标题宽度
            dgv_BatchData.Columns[0].Width = 120;
            dgv_BatchData.Columns[1].Width = 70;
            dgv_BatchData.Columns[2].Width = 220;
            dgv_BatchData.Columns[3].Width = 70;
            dgv_BatchData.Columns[4].Width = 100;


            //关闭自动排序功能
            for (int i = 0; i < dgv_BatchData.Columns.Count; i++)
            {
                dgv_BatchData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            //设置标题居中显示
            dgv_BatchData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置标题字体
            dgv_BatchData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置内容居中显示
            dgv_BatchData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //设置内容字体
            dgv_BatchData.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

            //设置行高
            dgv_BatchData.RowTemplate.Height = 30;

            FormulaHeadShow();

            FADM_Object.Communal._b_isUpdateNotDripList = false;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_BatchData.SelectedRows.Count; i++)
            {
                //修改配方(先删除后添加)
                string s_sql = "DELETE FROM formula_head WHERE" +
                                   " FormulaCode = '" + dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString() + "'" +
                                   " AND VersionNum = '" + dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString() + "' ;";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                s_sql = "DELETE FROM formula_details WHERE" +
                            " FormulaCode = '" + dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString() + "' AND" +
                            " VersionNum = '" + dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString() + "' ;";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                s_sql = "DELETE FROM formula_handle_details WHERE" +
                                    " FormulaCode = '" + dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString() + "' AND" +
                                    " VersionNum = '" + dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString() + "' ;";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            }

            FormulaHeadShow();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = dgv_BatchData.SelectedRows.Count-1; i >= 0; i--)
                {
                    //先判断是否后处理
                    if (dgv_BatchData.SelectedRows[i].Cells[4].Value.ToString() == "后处理"|| dgv_BatchData.SelectedRows[i].Cells[4].Value.ToString() == "Handle")
                    {
                        //判断是否固定杯位
                        if (dgv_BatchData.SelectedRows[i].Cells[3].Value.ToString() == "0")
                        {
                            string s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0 and IsFixed = 0 and enable = 1 and Type = 3 order by CupNum ;";
                            DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                            if(dt_temp.Rows.Count >0)
                            {

                                AddDropList a = new AddDropList(dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString(), dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString(), dt_temp.Rows[0][0].ToString(), 3);
                            }
                            //加入等待列表
                            else
                            {
                                s_sqltemp = "SELECT MAX(IndexNum) as maxnum  FROM wait_list  where Type = 3;";
                                dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                                int i_nIndex = 0;
                                if (dt_temp.Rows[0]["maxnum"] is DBNull)
                                {
                                    i_nIndex = 1;
                                }
                                else
                                {
                                    i_nIndex = Convert.ToInt16(dt_temp.Rows[0]["maxnum"]) + 1;
                                }

                                s_sqltemp = "Insert into wait_list(FormulaCode,VersionNum,IndexNum,CupNum,Type)values('" + dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString() + "','" + dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString() + "'," + i_nIndex.ToString() + "," + dgv_BatchData.SelectedRows[i].Cells[3].Value.ToString() + ",3);";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sqltemp);
                            }
                        }
                        else
                        {
                            string s_sqltemp = "SELECT  CupNum FROM cup_details WHERE  CupNum = '" + dgv_BatchData.SelectedRows[i].Cells[3].Value.ToString() + "' and IsUsing = 1 and enable = 1   and Type = 3;";
                            DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                            //没有空闲杯
                            if (dt_temp.Rows.Count > 0)
                            {
                                //加入等待列表
                                s_sqltemp = "SELECT MAX(IndexNum) as maxnum  FROM wait_list  where Type = 3;";
                                dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                                int nIndex = 0;
                                if (dt_temp.Rows[0]["maxnum"] is DBNull)
                                {
                                    nIndex = 1;
                                }
                                else
                                {
                                    nIndex = Convert.ToInt16(dt_temp.Rows[0]["maxnum"]) + 1;
                                }

                                s_sqltemp = "Insert into wait_list(FormulaCode,VersionNum,IndexNum,CupNum,Type)values('" + dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString() + "','" + dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString() + "'," + nIndex.ToString() + "," + dgv_BatchData.SelectedRows[i].Cells[3].Value.ToString() + ",3);";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sqltemp);
                            }
                            else
                            {
                                AddDropList a = new AddDropList(dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString(), dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString(), dgv_BatchData.SelectedRows[i].Cells[3].Value.ToString(), 3);
                            }
                        }
                    }
                    else
                    {
                        string s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0  and enable = 1 and Type = 2 order by CupNum ;";
                        DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                        if(dt_temp.Rows.Count >0)
                        {
                            AddDropList a = new AddDropList(dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString(), dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString(), dt_temp.Rows[0][0].ToString(), 2);
                        }
                        else
                        {
                            //加入等待列表
                            s_sqltemp = "SELECT MAX(IndexNum) as maxnum  FROM wait_list  where Type = 2;";
                            dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);
                            int nIndex = 0;
                            if (dt_temp.Rows[0]["maxnum"] is DBNull)
                            {
                                nIndex = 1;
                            }
                            else
                            {
                                nIndex = Convert.ToInt16(dt_temp.Rows[0]["maxnum"]) + 1;
                            }

                            s_sqltemp = "Insert into wait_list(FormulaCode,VersionNum,IndexNum,CupNum,Type)values('" + dgv_BatchData.SelectedRows[i].Cells[0].Value.ToString() + "','" + dgv_BatchData.SelectedRows[i].Cells[1].Value.ToString() + "'," + nIndex.ToString() + "," + dgv_BatchData.SelectedRows[i].Cells[3].Value.ToString() + ",2);";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sqltemp);
                        }
                    }
                }
            }
            catch { }
            FADM_Control.Formula._b_updateWait = true;
            FADM_Control.Formula_Cloth._b_updateWait = true;
            FormulaHeadShow();
        }

        private void FormulaHeadShow()
        {
            try
            {
                dgv_BatchData.Rows.Clear();

                string s_sql = null;
                DataTable dt_data = new DataTable();


                //获取配方浏览资料表头
                if (rdo_Record_Now.Checked)
                {
                    s_sql = "SELECT FormulaCode, VersionNum, CreateTime, CupNum,Stage from  formula_head WHERE" +
                                " CreateTime > CONVERT(varchar,GETDATE(),23) and State = '尚未滴液' ;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }
                else 
                {
                    s_sql = "SELECT FormulaCode, VersionNum, CreateTime, CupNum,Stage from  formula_head WHERE" +
                                "  State = '尚未滴液' ;";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                }

                for (int i = 0; i < dt_data.Rows.Count; i++)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        dgv_BatchData.Rows.Add(dt_data.Rows[i][0].ToString(), dt_data.Rows[i][1].ToString(), dt_data.Rows[i][2].ToString(), dt_data.Rows[i][3].ToString(), dt_data.Rows[i][4].ToString());
                    else
                        dgv_BatchData.Rows.Add(dt_data.Rows[i][0].ToString(), dt_data.Rows[i][1].ToString(), dt_data.Rows[i][2].ToString(), dt_data.Rows[i][3].ToString(), dt_data.Rows[i][4].ToString()=="滴液"?"Drip": "Handle");
                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "FormulaHeadShow", MessageBoxButtons.OK, true);
            }
        }

        private void btn_Refalsh_Click(object sender, EventArgs e)
        {
            FormulaHeadShow();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(FADM_Object.Communal._b_isUpdateNotDripList)
            {
                FADM_Object.Communal._b_isUpdateNotDripList = false;
                FormulaHeadShow();
            }
        }
    }
}
