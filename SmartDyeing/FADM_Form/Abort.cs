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
    public partial class Abort : Form
    {
        string s_ver = "20250102";
        //测试下
        //再来一次
        public Abort()
        {
            InitializeComponent();
        }

        private void Abort_Load(object sender, EventArgs e)
        {
            string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
            string s_name = Lib_File.Ini.GetIni("info", "Name", "广州科联精密机器有限公司", s_path);
            label6.Text = s_name;
            string s_tel = Lib_File.Ini.GetIni("info", "Tel", "18620114477", s_path);
            label8.Text = s_tel;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                dataGridView1.Columns.Add("名称", "Name");
                dataGridView1.Columns.Add("内容", "Info");
                dataGridView1.Columns[0].Width = 129;
                dataGridView1.Columns[1].Width = 200;
                //关闭自动排序功能
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                //设置内容居中显示
                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Rows.Add("软件版本", s_ver);
                dataGridView1.Rows.Add("PLC版本", Communal._s_plcVersion);
                dataGridView1.Rows.Add("开料机版本", Communal._s_brewVersion);
                dataGridView1.ClearSelection();
            }
            else
            {
                dataGridView1.Columns.Add("名称", "Name");
                dataGridView1.Columns.Add("内容", "Info");
                dataGridView1.Columns[0].Width = 327;
                dataGridView1.Columns[1].Width = 330;
                //关闭自动排序功能
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                //设置内容居中显示
                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Rows.Add("Software version", s_ver);
                dataGridView1.Rows.Add("PLC version", Communal._s_plcVersion);
                dataGridView1.Rows.Add("Brew version", Communal._s_brewVersion);
                dataGridView1.ClearSelection();
            }

            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                dataGridView2.Columns.Add("Area", "区域");
                dataGridView2.Columns.Add("Touch", "触摸屏版本");
                dataGridView2.Columns.Add("V1", "1号板版本");
                dataGridView2.Columns.Add("V2", "2号板版本");
                dataGridView2.Columns[0].Width = 68;
                dataGridView2.Columns[1].Width = 87;
                dataGridView2.Columns[2].Width = 87;
                dataGridView2.Columns[3].Width = 87;

                //关闭自动排序功能
                dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                //设置内容居中显示
                dataGridView2.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area2_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area3_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area4_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area5_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area6_Type == 3)
                {
                    if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 3)
                    {
                        dataGridView2.Rows.Add("1", FADM_Object.Communal._s_TouchVer1, FADM_Object.Communal._s_CardOneVer1, FADM_Object.Communal._s_CardTwoVer1);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area2_Type == 3)
                    {
                        dataGridView2.Rows.Add("2", FADM_Object.Communal._s_TouchVer2, FADM_Object.Communal._s_CardOneVer2, FADM_Object.Communal._s_CardTwoVer2);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area3_Type == 3)
                    {
                        dataGridView2.Rows.Add("3", FADM_Object.Communal._s_TouchVer3, FADM_Object.Communal._s_CardOneVer3, FADM_Object.Communal._s_CardTwoVer3);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area4_Type == 3)
                    {
                        dataGridView2.Rows.Add("4", FADM_Object.Communal._s_TouchVer4, FADM_Object.Communal._s_CardOneVer4, FADM_Object.Communal._s_CardTwoVer4);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area5_Type == 3)
                    {
                        dataGridView2.Rows.Add("5", FADM_Object.Communal._s_TouchVer5, FADM_Object.Communal._s_CardOneVer5, FADM_Object.Communal._s_CardTwoVer5);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area6_Type == 3)
                    {
                        dataGridView2.Rows.Add("6", FADM_Object.Communal._s_TouchVer6, FADM_Object.Communal._s_CardOneVer6, FADM_Object.Communal._s_CardTwoVer6);
                    }
                    dataGridView2.ClearSelection();
                }
                else
                {
                    dataGridView2.Visible = false;
                }
            }
            else
            {
                dataGridView2.Columns.Add("Area", "Area");
                dataGridView2.Columns.Add("Touch", "Touch");
                dataGridView2.Columns.Add("V1", "V1");
                dataGridView2.Columns.Add("V2", "V2");
                dataGridView2.Columns[0].Width = 147;
                dataGridView2.Columns[1].Width = 170;
                dataGridView2.Columns[2].Width = 170;
                dataGridView2.Columns[3].Width = 170;

                //关闭自动排序功能
                dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

                //设置标题居中显示
                dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                //设置内容居中显示
                dataGridView2.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area2_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area3_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area4_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area5_Type == 3
                    || Lib_Card.Configure.Parameter.Machine_Area6_Type == 3)
                {
                    if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 3)
                    {
                        dataGridView2.Rows.Add("1", FADM_Object.Communal._s_TouchVer1, FADM_Object.Communal._s_CardOneVer1, FADM_Object.Communal._s_CardTwoVer1);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area2_Type == 3)
                    {
                        dataGridView2.Rows.Add("2", FADM_Object.Communal._s_TouchVer2, FADM_Object.Communal._s_CardOneVer2, FADM_Object.Communal._s_CardTwoVer2);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area3_Type == 3)
                    {
                        dataGridView2.Rows.Add("3", FADM_Object.Communal._s_TouchVer3, FADM_Object.Communal._s_CardOneVer3, FADM_Object.Communal._s_CardTwoVer3);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area4_Type == 3)
                    {
                        dataGridView2.Rows.Add("4", FADM_Object.Communal._s_TouchVer4, FADM_Object.Communal._s_CardOneVer4, FADM_Object.Communal._s_CardTwoVer4);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area5_Type == 3)
                    {
                        dataGridView2.Rows.Add("5", FADM_Object.Communal._s_TouchVer5, FADM_Object.Communal._s_CardOneVer5, FADM_Object.Communal._s_CardTwoVer5);
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area6_Type == 3)
                    {
                        dataGridView2.Rows.Add("6", FADM_Object.Communal._s_TouchVer6, FADM_Object.Communal._s_CardOneVer6, FADM_Object.Communal._s_CardTwoVer6);
                    }
                    dataGridView2.ClearSelection();
                }
                else
                {
                    dataGridView2.Visible = false;
                }
            }
        }


        public string getVersion() {
            return s_ver;
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
