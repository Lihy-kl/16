using Lib_DataBank.MySQL;
using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        //染料助剂对应顺序
        Dictionary<string, int> dic_a = new Dictionary<string, int>();
        private Dictionary<(int, int), (int, int)> mergedCells = new Dictionary<(int, int), (int, int)>();
        //染料行数
        int _i_count_d = 10;

        //助剂行数
        int _i_count_a = 5;
        //总页数
        int _i_count_Total = 1;
        //当前页数
        int _i_count_cur = 1;
        public Form1()
        {
            InitializeComponent();
            uiDataGridView1.CellPainting += DataGridView_CellPainting;

            uiDataGridView1.Columns.Add("1", "姓名");
            uiDataGridView1.Columns.Add("2", "姓名");
            uiDataGridView1.Columns.Add("3", "姓名");
            uiDataGridView1.Columns.Add("4", "姓名");
            uiDataGridView1.Columns.Add("5", "姓名");
            uiDataGridView1.Columns.Add("6", "姓名");
            uiDataGridView1.Columns.Add("7", "姓名");

            uiDataGridView1.Columns.Add("CupNum1", "杯号");
            uiDataGridView1.Columns.Add("CupNum2", "杯号");
            uiDataGridView1.Columns.Add("CupNum3", "杯号");
            uiDataGridView1.Columns.Add("CupNum4", "杯号");
            uiDataGridView1.Columns.Add("CupNum5", "杯号");
            uiDataGridView1.Columns.Add("CupNum6", "杯号");
            uiDataGridView1.Columns.Add("CupNum7", "杯号");
            uiDataGridView1.Columns.Add("CupNum8", "杯号");
            uiDataGridView1.Columns.Add("CupNum9", "杯号");
            uiDataGridView1.Columns.Add("CupNum10", "杯号");

            uiDataGridView1.Columns.Add("CupNum11", "杯号");
            uiDataGridView1.Columns.Add("CupNum12", "杯号");
            uiDataGridView1.Columns.Add("CupNum13", "杯号");
            uiDataGridView1.Columns.Add("CupNum14", "杯号");
            uiDataGridView1.Columns.Add("CupNum15", "杯号");
            uiDataGridView1.Columns.Add("CupNum16", "杯号");
            uiDataGridView1.Columns.Add("CupNum17", "杯号");
            uiDataGridView1.Columns.Add("CupNum18", "杯号");
            uiDataGridView1.Columns.Add("CupNum19", "杯号");
            uiDataGridView1.Columns.Add("CupNum20", "杯号");

            uiDataGridView1.Columns.Add("CupNum21", "杯号");
            uiDataGridView1.Columns.Add("CupNum22", "杯号");
            uiDataGridView1.Columns.Add("CupNum23", "杯号");
            uiDataGridView1.Columns.Add("CupNum24", "杯号");

            uiDataGridView1.Columns[0].Width = 55;
            uiDataGridView1.Columns[1].Width = 80;
            uiDataGridView1.Columns[2].Width = 40;
            uiDataGridView1.Columns[3].Width = 40;
            uiDataGridView1.Columns[4].Width = 40;
            uiDataGridView1.Columns[5].Width = 40;
            uiDataGridView1.Columns[6].Width = 150;

            uiDataGridView1.Columns[7].Width = 60;
            uiDataGridView1.Columns[8].Width = 60;
            uiDataGridView1.Columns[9].Width = 60;
            uiDataGridView1.Columns[10].Width = 60;
            uiDataGridView1.Columns[11].Width = 60;
            uiDataGridView1.Columns[12].Width = 60;
            uiDataGridView1.Columns[13].Width = 60;
            uiDataGridView1.Columns[14].Width = 60;
            uiDataGridView1.Columns[15].Width = 60;
            uiDataGridView1.Columns[16].Width = 60;

            uiDataGridView1.Columns[17].Width = 60;
            uiDataGridView1.Columns[18].Width = 60;
            uiDataGridView1.Columns[19].Width = 60;
            uiDataGridView1.Columns[20].Width = 60;
            uiDataGridView1.Columns[21].Width = 60;
            uiDataGridView1.Columns[22].Width = 60;
            uiDataGridView1.Columns[23].Width = 60;
            uiDataGridView1.Columns[24].Width = 60;
            uiDataGridView1.Columns[25].Width = 60;
            uiDataGridView1.Columns[26].Width = 60;

            uiDataGridView1.Columns[27].Width = 60;
            uiDataGridView1.Columns[28].Width = 60;
            uiDataGridView1.Columns[29].Width = 60;
            uiDataGridView1.Columns[30].Width = 60;

            //foreach (DataRow dr in dt.Rows)
            //{
            //    uiDataGridView1.Columns.Add("CupNum" + dr["CupNum"], "杯号");
            //}

            LoadData();
            //
            MergeCells(uiDataGridView1, 0, 0, 1, 1, Color.CornflowerBlue);
            MergeCells(uiDataGridView1, 0, 0, 6, 6, Color.CornflowerBlue);
            //数量
            MergeCells(uiDataGridView1, 0, 2, 0, 0, Color.CornflowerBlue);
            //编号
            MergeCells(uiDataGridView1, 0, 0, 2, 5, Color.CornflowerBlue);
            //单位浴比
            MergeCells(uiDataGridView1, 1, 1, 1, 6, Color.CornflowerBlue);
            //布重
            MergeCells(uiDataGridView1, 2, 2, 1, 6, Color.CornflowerBlue);
            //染料
            MergeCells(uiDataGridView1, 3, 3 + _i_count_d - 1, 0, 0, Color.CornflowerBlue);
            //助剂
            MergeCells(uiDataGridView1, 3 + _i_count_d, 3 + _i_count_d + _i_count_a - 1, 0, 0, Color.CornflowerBlue);
            //其他
            MergeCells(uiDataGridView1, 3 + _i_count_d + _i_count_a, 3 + _i_count_d + _i_count_a + 2 - 1, 0, 0, Color.CornflowerBlue);
            //加水量
            MergeCells(uiDataGridView1, 3 + _i_count_d + _i_count_a, 3 + _i_count_d + _i_count_a, 1, 6, Color.CornflowerBlue);
            //成本计算
            MergeCells(uiDataGridView1, 3 + _i_count_d + _i_count_a + 1, 3 + _i_count_d + _i_count_a + 1, 1, 6, Color.CornflowerBlue);

            for (int j = 3; j <= 3 + _i_count_d + _i_count_a-1; j++)
            {
                uiDataGridView1[2, j].Style.BackColor = Color.LightBlue;
            }
            for (int j = 3; j <= 3 + _i_count_d + _i_count_a - 1; j++)
            {
                uiDataGridView1[3, j].Style.BackColor = Color.BlueViolet;
            }
            for (int j = 3; j <= 3 + _i_count_d + _i_count_a - 1; j++)
            {
                uiDataGridView1[4, j].Style.BackColor = Color.CornflowerBlue;
            }
            for (int j = 3; j <= 3 + _i_count_d + _i_count_a - 1; j++)
            {
                uiDataGridView1[5, j].Style.BackColor = Color.Blue;
            }

            //uiDataGridView1.CellPainting -= DataGridView_CellPainting;
            //uiDataGridView1.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            label3.Text = _i_count_cur + "/"+ _i_count_Total;
        }

        void LoadData()
        {
            List<string> lis_a = new List<string>();
            
            //查询现在有多少杯在批次表，就要加多少列
            string s_sql = "SELECT AssistantCode  FROM drop_details  where UnitOfAccount = '%' group by AssistantCode;";

            DataTable dt = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            _i_count_d= dt.Rows.Count;

            uiDataGridView1.Rows.Add("数量", "代码", "编号", "编号", "编号", "编号", "名称");
            uiDataGridView1.Rows.Add("数量", "单位浴比", "单位浴比", "单位浴比", "单位浴比", "单位浴比", "单位浴比");
            uiDataGridView1.Rows.Add("数量", "布重", "布重", "布重", "布重", "布重", "布重");
            foreach(DataRow dr in dt.Rows)
            {
                lis_a.Add(dr["AssistantCode"].ToString());
                dic_a.Add(dr["AssistantCode"].ToString(), dic_a.Count);
                DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData("Select * from bottle_details where AssistantCode = '"+ dr["AssistantCode"].ToString()+ "' order by SettingConcentration;");
                string s_AssistantCode = dr["AssistantCode"].ToString();
                string s_1 = "";
                string s_2 = "";
                string s_3 = "";
                string s_4 = "";
                string s_AssistantName = "";
                DataTable dt_Name = FADM_Object.Communal._fadmSqlserver.GetData("SELECT AssistantName from assistant_details where  AssistantCode = '"+ dr["AssistantCode"].ToString() +"';");
                if(dt_Name.Rows.Count >0)
                    s_AssistantName= dt_Name.Rows[0]["AssistantName"].ToString();

                if (dt_bottle_details.Rows.Count >0)
                    s_1 = dt_bottle_details.Rows[0]["BottleNum"].ToString();
                if (dt_bottle_details.Rows.Count > 1)
                    s_2 = dt_bottle_details.Rows[1]["BottleNum"].ToString();
                if (dt_bottle_details.Rows.Count > 2)
                    s_3 = dt_bottle_details.Rows[2]["BottleNum"].ToString();
                if (dt_bottle_details.Rows.Count > 3)
                    s_4 = dt_bottle_details.Rows[3]["BottleNum"].ToString();

                uiDataGridView1.Rows.Add("染料", s_AssistantCode, s_1, s_2, s_3, s_4, s_AssistantName);
            }

            s_sql = "SELECT AssistantCode  FROM drop_details  where UnitOfAccount = 'g/l' group by AssistantCode;";

            DataTable dt_a = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            _i_count_a = dt_a.Rows.Count;
            foreach (DataRow dr in dt_a.Rows)
            {
                lis_a.Add(dr["AssistantCode"].ToString());
                dic_a.Add(dr["AssistantCode"].ToString(), dic_a.Count);
                DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData("Select * from bottle_details where AssistantCode = '" + dr["AssistantCode"].ToString() + "' order by SettingConcentration;");
                string s_AssistantCode = dr["AssistantCode"].ToString();
                string s_1 = "";
                string s_2 = "";
                string s_3 = "";
                string s_4 = "";
                string s_AssistantName = "";
                DataTable dt_Name = FADM_Object.Communal._fadmSqlserver.GetData("SELECT AssistantName from assistant_details where  AssistantCode = '" + dr["AssistantCode"].ToString() + "';");
                if (dt_Name.Rows.Count > 0)
                    s_AssistantName = dt_Name.Rows[0]["AssistantName"].ToString();

                if (dt_bottle_details.Rows.Count > 0)
                    s_1 = dt_bottle_details.Rows[0]["BottleNum"].ToString();
                if (dt_bottle_details.Rows.Count > 1)
                    s_2 = dt_bottle_details.Rows[1]["BottleNum"].ToString();
                if (dt_bottle_details.Rows.Count > 2)
                    s_3 = dt_bottle_details.Rows[2]["BottleNum"].ToString();
                if (dt_bottle_details.Rows.Count > 3)
                    s_4 = dt_bottle_details.Rows[3]["BottleNum"].ToString();

                uiDataGridView1.Rows.Add("助剂", s_AssistantCode, s_1, s_2, s_3, s_4, s_AssistantName);
            }
            uiDataGridView1.Rows.Add("其他", "加水量", "加水量", "加水量", "加水量", "加水量", "加水量");
            uiDataGridView1.Rows.Add("其他", "成本计算", "成本计算", "成本计算", "成本计算", "成本计算", "成本计算");

            //把杯号信息填充进去
            //查询现在有多少杯在批次表，就要加多少列
            s_sql = "SELECT *  FROM drop_head order by CupNum;";

            DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_drop_head.Rows.Count % 24 > 0)
            {
                _i_count_Total = dt_drop_head.Rows.Count/24+1;
            }
            else
            {
                _i_count_Total = dt_drop_head.Rows.Count / 24;
            }
            int i_index = 0;
            foreach (DataRow dr in dt_drop_head.Rows)
            {
                i_index++;
                if (i_index > 24 * _i_count_cur)
                {
                    break;
                }
                //当数据满足条件时，才填充数据
                if (i_index > 24 * (_i_count_cur - 1))
                {
                    uiDataGridView1.Rows[0].Cells[6 + i_index].Value = dr["CupNum"].ToString();
                    uiDataGridView1[6 + i_index, 0].Style.BackColor = Color.CornflowerBlue;
                    uiDataGridView1.Rows[1].Cells[6 + i_index].Value = dr["BathRatio"].ToString();
                    uiDataGridView1.Rows[2].Cells[6 + i_index].Value = dr["ClothWeight"].ToString();
                    DataTable dt_drop_details = FADM_Object.Communal._fadmSqlserver.GetData("select * from drop_details where CupNum = " + dr["CupNum"].ToString());
                    foreach (DataRow dr1 in dt_drop_details.Rows)
                    {
                        uiDataGridView1.Rows[3 + dic_a[dr1["AssistantCode"].ToString()]].Cells[6 + i_index].Value = dr1["ObjectDropWeight"].ToString();
                        //判断是否一样瓶号
                        if (dr1["BottleNum"].ToString() == uiDataGridView1[2, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                        {
                            uiDataGridView1[6 + i_index, 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.LightBlue;
                        }
                        else if (dr1["BottleNum"].ToString() == uiDataGridView1[3, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                        {
                            uiDataGridView1[6 + i_index, 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.BlueViolet;
                        }
                        else if (dr1["BottleNum"].ToString() == uiDataGridView1[4, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                        {
                            uiDataGridView1[6 + i_index, 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.CornflowerBlue;
                        }
                        else if (dr1["BottleNum"].ToString() == uiDataGridView1[5, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                        {
                            uiDataGridView1[6 + i_index, 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.Blue;
                        }
                    }
                    //加水量
                    uiDataGridView1.Rows[3 + _i_count_d + _i_count_a].Cells[6 + i_index].Value = dr["ObjectAddWaterWeight"].ToString();
                }
            }
            
        }

        public void MergeCells(DataGridView dataGridView, int startRow, int endRow, int startcolumn, int endcolumn, Color color)
        {
            if (startcolumn < 0 || dataGridView.Columns.Count <= endcolumn || startcolumn > endcolumn || startRow < 0 || endRow >= dataGridView.Rows.Count || startRow > endRow)
            {
                return;
            }

            DataGridViewCell cell = dataGridView[startcolumn, startRow];
            string cellValue = cell.Value != null ? cell.Value.ToString() : "";

            for (int i = startRow; i <= endRow; i++)
            {
                for (int j = startcolumn; j <= endcolumn; j++)
                    dataGridView[j, i].Value = null;
            }
            //在中间的行写值
            int valRowIndex = 0;
            int valColIndex = 0;
            int rows = endRow - startRow + 1;
            int halfRow = rows / 2;
            valRowIndex = startRow + halfRow;
            int halfCol = (endcolumn - startcolumn + 1) / 2;
            valColIndex = startcolumn + halfCol;

            DataGridViewTextBoxCell textBoxCell = new DataGridViewTextBoxCell();
            if (cellValue.Length <= 2)
            {
                textBoxCell.Value = cellValue;
            }
            else
            {
                textBoxCell.Value = cellValue.Substring(0, 2);
            }
            dataGridView[valColIndex, valRowIndex] = textBoxCell;
            if (cellValue.Length > 2)
            {
                DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                textBoxCell1.Value = cellValue.Substring(2, cellValue.Length-2);
                dataGridView[valColIndex+1, valRowIndex] = textBoxCell1;
            }


                for (int i = startRow; i <= endRow; i++)
            {
                for (int j = startcolumn; j <= endcolumn; j++)
                {
                    dataGridView[j, i].Style.BackColor = color;
                    dataGridView[j, i].ReadOnly = true; // 设置合并后的单元格只读
                    dataGridView[j, i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // 设置文本居中
                    dataGridView[j, i].Style.Padding = new Padding(0); // 设置内边距为0，达到不显示边框的效果
                    dataGridView[j, i].Style.SelectionBackColor = dataGridView[j, i].Style.BackColor; // 设置选中背景色与背景色一致
                }
            }
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DataGridView dgv = sender as DataGridView;
            //数量
            if (e.ColumnIndex == 0 && e.RowIndex <= 2)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        
                        //只画右边框
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }
            //染料
            else if (e.ColumnIndex == 0 && e.RowIndex > 2 && e.RowIndex <= 3 + _i_count_d-1)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        if (e.RowIndex == 3)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
                        //只画右边框
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }
            //助剂
            else if (e.ColumnIndex == 0 && e.RowIndex > 3 + _i_count_d-1 && e.RowIndex <= 3 + _i_count_d + _i_count_a - 1 && _i_count_a>0)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        if (e.RowIndex == 3 + _i_count_d)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
                        //只画右边框
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }
            //编号
            else if (e.RowIndex == 0 && e.ColumnIndex >= 2 && e.ColumnIndex <= 5)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                        //只画右边框
                        if (e.ColumnIndex == 5)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }
            //单位浴比
            else if (e.RowIndex == 1 && e.ColumnIndex >= 1 && e.ColumnIndex <= 6)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        if (e.ColumnIndex != 1 && e.ColumnIndex != 6)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
                        //只画右边框
                        if (e.ColumnIndex == 6)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }
            //布重
            else if (e.RowIndex == 2 && e.ColumnIndex >= 1 && e.ColumnIndex <= 6)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                        //只画右边框
                        if (e.ColumnIndex == 6)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }

            //其他
            else if (e.ColumnIndex == 0 && e.RowIndex >= 3 + _i_count_d + _i_count_a && e.RowIndex <= 3 + _i_count_d + _i_count_a + 1)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        if (e.RowIndex == 3 + _i_count_d + _i_count_a)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
                        if (e.RowIndex == 3 + _i_count_d + _i_count_a + 1)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                        //只画右边框
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }
            //加水量
            else if (e.RowIndex == 3 + _i_count_d + _i_count_a && e.ColumnIndex >= 1 && e.ColumnIndex <= 6)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                        //只画右边框
                        if (e.ColumnIndex == 6)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }
            //计算成本
            else if (e.RowIndex == 3 + _i_count_d + _i_count_a + 1 && e.ColumnIndex >= 1 && e.ColumnIndex <= 6)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                        //只画右边框
                        if (e.ColumnIndex == 6)
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        if (e.Value != null)
                        {
                            var s = new StringFormat();
                            s.Alignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                        e.Handled = true;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_i_count_cur == _i_count_Total)
            {
                MessageBox.Show("已经是最后一页");
            }
            else
            {
                _i_count_cur++;
                //先清空数据
                for (int i = 0; i <= 3 + _i_count_d + _i_count_a + 1; i++)
                {
                    for (int j = 7; j < uiDataGridView1.Columns.Count; j++)
                    {
                        uiDataGridView1[j, i].Value = "";
                        uiDataGridView1[j, i].Style.BackColor = uiDataGridView1[1, 3].Style.BackColor;
                    }
                }
                //压入新数据
                //把杯号信息填充进去
                //查询现在有多少杯在批次表，就要加多少列
                string s_sql = "SELECT *  FROM drop_head order by CupNum;";

                DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_drop_head.Rows.Count % 24 > 0)
                {
                    _i_count_Total = dt_drop_head.Rows.Count / 24 + 1;
                }
                else
                {
                    _i_count_Total = dt_drop_head.Rows.Count / 24;
                }
                int i_index = 0;
                foreach (DataRow dr in dt_drop_head.Rows)
                {
                    i_index++;
                    if (i_index > 24 * _i_count_cur)
                    {
                        break;
                    }
                    //当数据满足条件时，才填充数据
                    if (i_index > 24 * (_i_count_cur - 1))
                    {
                        uiDataGridView1.Rows[0].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["CupNum"].ToString();
                        uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 0].Style.BackColor = Color.CornflowerBlue;
                        uiDataGridView1.Rows[1].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["BathRatio"].ToString();
                        uiDataGridView1.Rows[2].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["ClothWeight"].ToString();
                        DataTable dt_drop_details = FADM_Object.Communal._fadmSqlserver.GetData("select * from drop_details where CupNum = " + dr["CupNum"].ToString());
                        foreach (DataRow dr1 in dt_drop_details.Rows)
                        {
                            uiDataGridView1.Rows[3 + dic_a[dr1["AssistantCode"].ToString()]].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr1["ObjectDropWeight"].ToString();
                            //判断是否一样瓶号
                            if (dr1["BottleNum"].ToString() == uiDataGridView1[2, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.LightBlue;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[3, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.BlueViolet;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[4, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.CornflowerBlue;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[5, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.Blue;
                            }
                        }
                        //加水量
                        uiDataGridView1.Rows[3 + _i_count_d + _i_count_a].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["ObjectAddWaterWeight"].ToString();
                    }
                }
                label3.Text = _i_count_cur.ToString() + "/" + _i_count_Total.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_i_count_cur == 1)
            {
                MessageBox.Show("已经是第一页");
            }
            else
            {
                _i_count_cur--;
                //先清空数据
                for (int i = 0; i <= 3 + _i_count_d + _i_count_a + 1; i++)
                {
                    for (int j = 7; j < uiDataGridView1.Columns.Count; j++)
                    {
                        uiDataGridView1[j, i].Value = "";
                        uiDataGridView1[j, i].Style.BackColor = uiDataGridView1[1, 3].Style.BackColor;
                    }
                }
                //压入新数据
                //把杯号信息填充进去
                //查询现在有多少杯在批次表，就要加多少列
                string s_sql = "SELECT *  FROM drop_head order by CupNum;";

                DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_drop_head.Rows.Count % 24 > 0)
                {
                    _i_count_Total = dt_drop_head.Rows.Count / 24 + 1;
                }
                else
                {
                    _i_count_Total = dt_drop_head.Rows.Count / 24;
                }
                int i_index = 0;
                foreach (DataRow dr in dt_drop_head.Rows)
                {
                    i_index++;
                    if (i_index > 24 * _i_count_cur)
                    {
                        break;
                    }
                    //当数据满足条件时，才填充数据
                    if (i_index > 24 * (_i_count_cur - 1))
                    {
                        uiDataGridView1.Rows[0].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["CupNum"].ToString();
                        uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 0].Style.BackColor = Color.CornflowerBlue;
                        uiDataGridView1.Rows[1].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["BathRatio"].ToString();
                        uiDataGridView1.Rows[2].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["ClothWeight"].ToString();
                        DataTable dt_drop_details = FADM_Object.Communal._fadmSqlserver.GetData("select * from drop_details where CupNum = " + dr["CupNum"].ToString());
                        foreach (DataRow dr1 in dt_drop_details.Rows)
                        {
                            uiDataGridView1.Rows[3 + dic_a[dr1["AssistantCode"].ToString()]].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr1["ObjectDropWeight"].ToString();
                            //判断是否一样瓶号
                            if (dr1["BottleNum"].ToString() == uiDataGridView1[2, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.LightBlue;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[3, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.BlueViolet;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[4, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.CornflowerBlue;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[5, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.Blue;
                            }
                        }
                        //加水量
                        uiDataGridView1.Rows[3 + _i_count_d + _i_count_a].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["ObjectAddWaterWeight"].ToString();
                    }
                }
                label3.Text = _i_count_cur.ToString() + "/" + _i_count_Total.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_i_count_cur == 1)
            {
                MessageBox.Show("已经是第一页");
            }
            else
            {
                _i_count_cur=1;
                //先清空数据
                for (int i = 0; i <= 3 + _i_count_d + _i_count_a + 1; i++)
                {
                    for (int j = 7; j < uiDataGridView1.Columns.Count; j++)
                    {
                        uiDataGridView1[j, i].Value = "";
                        uiDataGridView1[j, i].Style.BackColor = uiDataGridView1[1, 3].Style.BackColor;
                    }
                }
                //压入新数据
                //把杯号信息填充进去
                //查询现在有多少杯在批次表，就要加多少列
                string s_sql = "SELECT *  FROM drop_head order by CupNum;";

                DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_drop_head.Rows.Count % 24 > 0)
                {
                    _i_count_Total = dt_drop_head.Rows.Count / 24 + 1;
                }
                else
                {
                    _i_count_Total = dt_drop_head.Rows.Count / 24;
                }
                int i_index = 0;
                foreach (DataRow dr in dt_drop_head.Rows)
                {
                    i_index++;
                    if (i_index > 24 * _i_count_cur)
                    {
                        break;
                    }
                    //当数据满足条件时，才填充数据
                    if (i_index > 24 * (_i_count_cur - 1))
                    {
                        uiDataGridView1.Rows[0].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["CupNum"].ToString();
                        uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 0].Style.BackColor = Color.CornflowerBlue;
                        uiDataGridView1.Rows[1].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["BathRatio"].ToString();
                        uiDataGridView1.Rows[2].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["ClothWeight"].ToString();
                        DataTable dt_drop_details = FADM_Object.Communal._fadmSqlserver.GetData("select * from drop_details where CupNum = " + dr["CupNum"].ToString());
                        foreach (DataRow dr1 in dt_drop_details.Rows)
                        {
                            uiDataGridView1.Rows[3 + dic_a[dr1["AssistantCode"].ToString()]].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr1["ObjectDropWeight"].ToString();
                            //判断是否一样瓶号
                            if (dr1["BottleNum"].ToString() == uiDataGridView1[2, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.LightBlue;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[3, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.BlueViolet;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[4, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.CornflowerBlue;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[5, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.Blue;
                            }
                        }
                        //加水量
                        uiDataGridView1.Rows[3 + _i_count_d + _i_count_a].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["ObjectAddWaterWeight"].ToString();
                    }
                }
                label3.Text = _i_count_cur.ToString() + "/" + _i_count_Total.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_i_count_cur == _i_count_Total)
            {
                MessageBox.Show("已经是最后一页");
            }
            else
            {
                _i_count_cur=_i_count_Total;
                //先清空数据
                for (int i = 0; i <= 3 + _i_count_d + _i_count_a + 1; i++)
                {
                    for (int j = 7; j < uiDataGridView1.Columns.Count; j++)
                    {
                        uiDataGridView1[j, i].Value = "";
                        uiDataGridView1[j, i].Style.BackColor = uiDataGridView1[1, 3].Style.BackColor;
                    }
                }
                //压入新数据
                //把杯号信息填充进去
                //查询现在有多少杯在批次表，就要加多少列
                string s_sql = "SELECT *  FROM drop_head order by CupNum;";

                DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_drop_head.Rows.Count % 24 > 0)
                {
                    _i_count_Total = dt_drop_head.Rows.Count / 24 + 1;
                }
                else
                {
                    _i_count_Total = dt_drop_head.Rows.Count / 24;
                }
                int i_index = 0;
                foreach (DataRow dr in dt_drop_head.Rows)
                {
                    i_index++;
                    if (i_index > 24 * _i_count_cur)
                    {
                        break;
                    }
                    //当数据满足条件时，才填充数据
                    if (i_index > 24 * (_i_count_cur - 1))
                    {
                        uiDataGridView1.Rows[0].Cells[6 + i_index- 24 * (_i_count_cur - 1)].Value = dr["CupNum"].ToString();
                        uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 0].Style.BackColor = Color.CornflowerBlue;
                        uiDataGridView1.Rows[1].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["BathRatio"].ToString();
                        uiDataGridView1.Rows[2].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["ClothWeight"].ToString();
                        DataTable dt_drop_details = FADM_Object.Communal._fadmSqlserver.GetData("select * from drop_details where CupNum = " + dr["CupNum"].ToString());
                        foreach (DataRow dr1 in dt_drop_details.Rows)
                        {
                            uiDataGridView1.Rows[3 + dic_a[dr1["AssistantCode"].ToString()]].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr1["ObjectDropWeight"].ToString();
                            //判断是否一样瓶号
                            if (dr1["BottleNum"].ToString() == uiDataGridView1[2, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.LightBlue;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[3, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.BlueViolet;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[4, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.CornflowerBlue;
                            }
                            else if (dr1["BottleNum"].ToString() == uiDataGridView1[5, 3 + dic_a[dr1["AssistantCode"].ToString()]].Value.ToString())
                            {
                                uiDataGridView1[6 + i_index - 24 * (_i_count_cur - 1), 3 + dic_a[dr1["AssistantCode"].ToString()]].Style.BackColor = Color.Blue;
                            }
                        }
                        //加水量
                        uiDataGridView1.Rows[3 + _i_count_d + _i_count_a].Cells[6 + i_index - 24 * (_i_count_cur - 1)].Value = dr["ObjectAddWaterWeight"].ToString();
                    }
                }
                label3.Text = _i_count_cur.ToString() + "/" + _i_count_Total.ToString();
            }
        }
    }
}
