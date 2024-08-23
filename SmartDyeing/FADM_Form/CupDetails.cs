using HslControls;
using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartDyeing.FADM_Form
{
    public partial class CupDetails : Form
    {
        DateTime[] _times;
        private int _i_cupNo;
        public CupDetails(int CupNo)
        {
            InitializeComponent();
            //this.Text = CupNo + "号杯详细资料";
            txt_CupNum.Text = CupNo.ToString();
            this._i_cupNo = CupNo;
            Curve2D cuv2D = new Curve2D();
            List<float> list_values = new List<float>();
            //string s = Lib_File.Txt.ReadTXT(CupNo);
            //if (s != "" && s != null)
            //{
            //    s = s.Substring(0, s.Length - 1);
            //    string[] arr = s.Split('@');

            //    float[] temp = new float[arr.Count()];
            //    DateTime[] times = new DateTime[arr.Count()];

            //    for (int i = 0; i < arr.Count(); i++)
            //    {
            //        temp[i] = Convert.ToSingle(arr[i]);
            //        times[i] = DateTime.Now.AddSeconds((i - arr.Count()) * 30);
            //    }

            //    // 显示出数据信息来

            //    // 设置曲线属性，名称，数据，颜色，是否平滑，格式化显示文本
            //    hslCurveHistory1.SetLeftCurve("温度", temp, Color.Blue, HslControls.CurveStyle.Curve, "{0:F1} ℃");
            //    hslCurveHistory1.SetDateTimes(times);
            //    hslCurveHistory1.RenderCurveUI(true);

            //}

            //string sMarkStep = Lib_File.Txt.ReadMarkTXT(CupNo);
            //if (sMarkStep != "" && sMarkStep != null)
            //{
            //    bool b = true;
            //    sMarkStep = sMarkStep.Substring(0, sMarkStep.Length - 1);
            //    string[] arr = sMarkStep.Split('@');
            //    for (int i = 0; i < arr.Count(); i++)
            //    {
            //        string sName = arr[i].Substring(0, arr[i].IndexOf(","));
            //        string sNum = arr[i].Substring(arr[i].IndexOf(",") + 1, arr[i].Count() - arr[i].IndexOf(",") - 1);

            //        hslCurveHistory1.AddMarkText(new HslControls.HslMarkText()
            //        {
            //            Index = Convert.ToInt32(sNum) - 1,
            //            CurveKey = "温度",
            //            MarkText = sName,
            //            PositionStyle = b ? MarkTextPositionStyle.Up : MarkTextPositionStyle.Down,
            //            CircleBrush = Brushes.Red,
            //            TextBrush = Brushes.Red
            //        }); ; ;
            //        b = !b;
            //    }
            //}

            string s_cupno = Lib_File.Txt.ReadTXT(CupNo);
            if (s_cupno != "" && s_cupno != null)
            {
                s_cupno = s_cupno.Substring(0, s_cupno.Length - 1);
                string[] sa_arr = s_cupno.Split('@');

                InitChart();

                _times = new DateTime[sa_arr.Count()];

                for (int i = 0; i < sa_arr.Count(); i++)
                {
                    _times[i] = DateTime.Now.AddSeconds((i - sa_arr.Count()) * 30);
                }


                AddSeries("温度", Color.Red);


                Series series = chart.Series[0];

                for (int i = 0; i < sa_arr.Count(); i++)
                {
                    series.Points.AddXY(Convert.ToDouble(i + 1), Convert.ToDouble(sa_arr[i]));
                }


                chart.MouseMove += new MouseEventHandler(chart1_MouseMove);
                chart.MouseWheel += new System.Windows.Forms.MouseEventHandler(chart1_Mouselheel);
            }

            string s_markStep = Lib_File.Txt.ReadMarkTXT(CupNo);
            if (s_markStep != "" && s_markStep != null)
            {
                s_markStep = s_markStep.Substring(0, s_markStep.Length - 1);
                string[] sa_arr = s_markStep.Split('@');
                for (int i = 0; i < sa_arr.Count(); i++)
                {
                    string s_name = sa_arr[i].Substring(0, sa_arr[i].IndexOf(","));
                    string s_num = sa_arr[i].Substring(sa_arr[i].IndexOf(",") + 1, sa_arr[i].Count() - sa_arr[i].IndexOf(",") - 1);

                    if (Convert.ToInt32(s_num) <= chart.Series[0].Points.Count)
                    {
                        chart.Series[0].Points[Convert.ToInt32(s_num) - 1].MarkerColor = Color.Blue;
                        chart.Series[0].Points[Convert.ToInt32(s_num) - 1].MarkerSize = 10;
                        chart.Series[0].Points[Convert.ToInt32(s_num) - 1].MarkerStyle = MarkerStyle.Triangle;
                        chart.Series[0].Points[Convert.ToInt32(s_num) - 1].Label = s_name;
                        chart.Series[0].Points[Convert.ToInt32(s_num) - 1].Font = new Font("Consolas", 12f);
                        chart.Series[0].Points[Convert.ToInt32(s_num) - 1].LabelForeColor = Color.Blue;
                    }
                }
            }
        }

        private void CupDetails_Load(object sender, EventArgs e)
        {
            DataTable dt_data = new DataTable();
            DataRow dr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                //建立Column例，可以指明例的类型,这里用的是默认的string
                dt_data.Columns.Add(new DataColumn("类型"));
                dt_data.Columns.Add(new DataColumn("瓶号"));
                dt_data.Columns.Add(new DataColumn("配方用量"));
                dt_data.Columns.Add(new DataColumn("需加重量"));
                dt_data.Columns.Add(new DataColumn("实加重量"));

                //设置标题字体
                dgv_CupDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 14.25F);

                //设置内容字体
                dgv_CupDetails.RowsDefaultCellStyle.Font = new Font("宋体", 14.25F);

            }
            else
            {
                dt_data.Columns.Add(new DataColumn("Type"));
                dt_data.Columns.Add(new DataColumn("BottleNum"));
                dt_data.Columns.Add(new DataColumn("DosageOfFormula"));
                dt_data.Columns.Add(new DataColumn("TargetVolume"));
                dt_data.Columns.Add(new DataColumn("ActualVolume"));

                //设置标题字体
                dgv_CupDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 10.5F);

                //设置内容字体
                dgv_CupDetails.RowsDefaultCellStyle.Font = new Font("宋体", 10.5F);
            }
            //获取要显示的行数

            //设置标题居中显示
            dgv_CupDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

          

            //设置内容居中显示
            dgv_CupDetails.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

           

            //设置行高
            dgv_CupDetails.RowTemplate.Height = 30;

            ////获取当前批次号
            //string P_str_sql = "SELECT BatchName FROM enabled_set" +
            //                  " WHERE MyID = 1 ";

            //DataTable _dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);

            //string P_str_batch = Convert.ToString(_dt_data.Rows[0][_dt_data.Columns[0]]);

            //P_str_sql = "SELECT * FROM machine_parameters" +
            //            " WHERE MyID = 1 ";
            //_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
            int i_maxcup = Lib_Card.Configure.Parameter.Machine_Cup_Total;
            int i_maxbottle = Lib_Card.Configure.Parameter.Machine_Bottle_Total;

            //获取当前批次当前杯号信息
            string s_sql = "SELECT * FROM drop_details WHERE" +
                        " CupNum = '" + this._i_cupNo +
                        "' AND BottleNum > 0 AND ( BottleNum <= " + i_maxbottle + "" +
                        " ) ORDER BY BottleNum;";

            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            int i_line = dt_data1.Rows.Count;
            for (int i = 1; i <= i_line; i++)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    dr = dt_data.NewRow();
                    dr[0] = "滴液";
                    dr[1] = dt_data1.Rows[i - 1]["BottleNum"];
                    dr[2] = dt_data1.Rows[i - 1]["FormulaDosage"];
                    dr[3] = dt_data1.Rows[i - 1]["ObjectDropWeight"];
                    dr[4] = dt_data1.Rows[i - 1]["RealDropWeight"];
                    dt_data.Rows.Add(dr);
                }
                else
                {
                    dr = dt_data.NewRow();
                    dr[0] = "Drip";
                    dr[1] = dt_data1.Rows[i - 1]["BottleNum"];
                    dr[2] = dt_data1.Rows[i - 1]["FormulaDosage"];
                    dr[3] = dt_data1.Rows[i - 1]["ObjectDropWeight"];
                    dr[4] = dt_data1.Rows[i - 1]["RealDropWeight"];
                    dt_data.Rows.Add(dr);
                }
            }

            //获取当前批次当前杯号信息
            s_sql = "SELECT * FROM dye_details WHERE" +
                        " CupNum = '" + this._i_cupNo +
                        "' AND BottleNum > 0 AND ( BottleNum <= " + i_maxbottle + "" +
                        " ) ORDER BY StepNum;";

            dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            int i_line1 = dt_data1.Rows.Count;
            for (int i = 1; i <= i_line1; i++)
            {
                double d = 0.0;
                if (dt_data1.Rows[i - 1]["Compensation"] is   DBNull)
                {
                }
                else
                {
                    d = Convert.ToDouble(dt_data1.Rows[i - 1]["Compensation"].ToString());
                }
                dr = dt_data.NewRow();
                dr[0] = dt_data1.Rows[i - 1]["Code"];
                dr[1] = dt_data1.Rows[i - 1]["BottleNum"];
                dr[2] = dt_data1.Rows[i - 1]["FormulaDosage"];
                dr[3] = (Convert.ToDouble(dt_data1.Rows[i - 1]["ObjectDropWeight"].ToString()) + d).ToString("F2");
                dr[4] = dt_data1.Rows[i - 1]["RealDropWeight"];
                dt_data.Rows.Add(dr);
            }

            //捆绑
            dgv_CupDetails.DataSource = new DataView(dt_data);
            //预留滚动条空间
            int i_headspace = 1;
            if (i_line > 11)
            {
                i_headspace = 5;
            }
            for (int i = 0; i < 5; i++)
            {
                dgv_CupDetails.Columns[i].Width = dgv_CupDetails.Width / 5 - i_headspace;
            }
            dgv_CupDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_CupDetails.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            s_sql = "SELECT * FROM drop_head WHERE" +
                        " CupNum = '" + this._i_cupNo + "' ;";

            dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            if (dt_data1.Rows.Count > 0)
            {

                txt_FormulaCode.Text = Convert.ToString(dt_data1.Rows[0][dt_data1.Columns["FormulaCode"]]);

                txt_VersionNum.Text = Convert.ToString(dt_data1.Rows[0][dt_data1.Columns["VersionNum"]]);

                txt_objectWater.Text = Convert.ToString(dt_data1.Rows[0][dt_data1.Columns["ObjectAddWaterWeight"]]);

                txt_realWater.Text = Convert.ToString(dt_data1.Rows[0][dt_data1.Columns["RealAddWaterWeight"]]);


                s_sql = "SELECT SUM(CAST(ISNULL(ObjectDropWeight,0.00) as numeric(18,2))) FROM drop_details WHERE" +
                            " CupNum = '" + this._i_cupNo +
                            "' AND BottleNum > " + i_maxbottle + ";";

                dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            }

            //string s = Lib_File.Txt.ReadTXT(this.iCupNo);
            //if (s != "" && s != null)
            //{
            //    s = s.Substring(0, s.Length - 1);
            //    string[] arr = s.Split('@');

            //    float[] temp = new float[arr.Count()];
            //    DateTime[] times = new DateTime[arr.Count()];

            //    for (int i = 0; i < arr.Count(); i++)
            //    {
            //        temp[i] = Convert.ToSingle(arr[i]);
            //        times[i] = DateTime.Now.AddSeconds((i - arr.Count()) * 30);
            //    }

            //    // 显示出数据信息来

            //    // 显示出数据信息来
            //    Invoke(new Action(() =>
            //    {
            //        // 设置曲线属性，名称，数据，颜色，是否平滑，格式化显示文本
            //        hslCurveHistory1.SetLeftCurve("温度", temp, Color.Blue, HslControls.CurveStyle.Curve, "{0:F1} ℃");
            //        hslCurveHistory1.SetDateTimes(times);
            //        hslCurveHistory1.RenderCurveUI(true);
            //    }));

            //}

            //string sMarkStep = Lib_File.Txt.ReadMarkTXT(this.iCupNo);
            //if (sMarkStep != "" && sMarkStep != null)
            //{
            //    bool b = true;
            //    sMarkStep = sMarkStep.Substring(0, sMarkStep.Length - 1);
            //    string[] arr = sMarkStep.Split('@');
            //    for (int i = 0; i < arr.Count(); i++)
            //    {
            //        string sName = arr[i].Substring(0, arr[i].IndexOf(","));
            //        string sNum = arr[i].Substring(arr[i].IndexOf(",") + 1, arr[i].Count() - arr[i].IndexOf(",") - 1);

            //        hslCurveHistory1.AddMarkText(new HslControls.HslMarkText()
            //        {
            //            Index = Convert.ToInt32(sNum) - 1,
            //            CurveKey = "温度",
            //            MarkText = sName,
            //            PositionStyle = b ? MarkTextPositionStyle.Up : MarkTextPositionStyle.Down,
            //            CircleBrush = Brushes.Red,
            //            TextBrush = Brushes.Red
            //        }); ; ;
            //        b = !b;
            //    }
            //}

        }

        private void Show(string s)
        {
            s = s.Substring(0, s.Length - 1);
            string[] arr = s.Split('@');

            _times = new DateTime[arr.Count()];
            for (int i = 0; i < arr.Count(); i++)
            {
                _times[i] = DateTime.Now.AddSeconds((i - arr.Count()) * 30);
            }

            AddSeries("温度", Color.Red);

            Series series = chart.Series[0];
            //series.Points.AddXY(0, 0);

            for (int i = 0; i < arr.Count(); i++)
            {
                series.Points.AddXY(Convert.ToDouble(i + 1), Convert.ToDouble(arr[i]));
            }
            chart.MouseMove += new MouseEventHandler(chart1_MouseMove);

            chart.MouseWheel += new System.Windows.Forms.MouseEventHandler(chart1_Mouselheel);


        }

        private void chart1_Mouselheel(object sender, MouseEventArgs e)
        {
            var chart1 = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;
            var xAxis = chart1.ChartAreas[0].AxisX;
            var yAxis = chart1.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (chart.Series.Count > 0)
                {
                    chart.ChartAreas[0].CursorX.SetCursorPixelPosition(new PointF(e.X, e.Y), true);
                    chart.ChartAreas[0].CursorY.SetCursorPixelPosition(new PointF(e.X, e.Y), true);
                    if (Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) < chart.Series[0].Points.Count && Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) > 0)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            toolTip1.SetToolTip(chart, string.Format("序号:{0},时间:{1}, 温度:{2}", chart.Series[0].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].XValue, _times[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].ToString(),
                            chart.Series[0].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].YValues[0]));
                        else
                            toolTip1.SetToolTip(chart, string.Format("Index:{0},Time:{1}, Temperature:{2}", chart.Series[0].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].XValue, _times[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].ToString(),
                            chart.Series[0].Points[Convert.ToInt32(chart.ChartAreas[0].CursorX.Position.ToString()) - 1].YValues[0]));
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
            this.panel1.Controls.Add(chart);
            chart.Dock = DockStyle.Fill;
            chart.Visible = true;

            ChartArea chartArea = new ChartArea();
            //chartArea.Name = "FirstArea";
            chartArea.AxisX.Interval = 40;
            chartArea.AxisX.IntervalOffset = 40;

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
                chartArea.AxisY.Title = @"温度(℃)";
                chartArea.AxisY.LineWidth = 2;
                chartArea.AxisY.LineColor = Color.Black;
                chartArea.AxisY.Enabled = AxisEnabled.True;


                chartArea.AxisX.Title = @"时间(X30s)";
                chartArea.AxisX.IsLabelAutoFit = true;
                chartArea.AxisX.LabelAutoFitMinFontSize = 5;
                chartArea.AxisX.LabelStyle.Angle = -15;
            }
            else
            {
                // Axis
                chartArea.AxisY.Title = @"Temperature(℃)";
                chartArea.AxisY.LineWidth = 2;
                chartArea.AxisY.LineColor = Color.Black;
                chartArea.AxisY.Enabled = AxisEnabled.True;


                chartArea.AxisX.Title = @"Time(X30s)";
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
        }

        private void InitChart()
        {
            CreateChart();
        }
    }
}
