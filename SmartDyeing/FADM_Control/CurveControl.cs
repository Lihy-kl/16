using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartDyeing.FADM_Control
{

    public partial class CurveControl : UserControl
    {
        private Chart _chart;
        private DateTime[] _maxTimes = new DateTime[0];
        private int _language = 0;
        private DateTime _startTime = DateTime.Now;

        public struct chartData
        {
            /// <summary>
            /// 温度数组
            /// </summary>
            public string temperature;

            /// <summary>
            /// 时间点-工艺对应表
            /// </summary>
            public string craft;
        }


        public struct ProcessStep
        {
            public string StepName; // 工艺名称
            public double? TargetTemperature; // 目标温度
            public double? HeatingRate; // 升温速率
            public double? Duration; // 持续时间
        }

        public CurveControl(int language, Chart chart)
        {
            _chart = chart;
            _language = language;
            InitializeComponent();
            InitializeChart();
        }

        public CurveControl(int language, Chart chart, DateTime dateTime)
        {
            _chart = chart;
            _language = language;
            _startTime = dateTime;
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            ChartArea chartArea = new ChartArea
            {
                AxisX = { Interval = 40, IntervalOffset = 40, ScaleView = { Zoomable = true }, ScrollBar = { ButtonStyle = ScrollBarButtonStyles.All }, LineWidth = 2, LineColor = Color.Black, Enabled = AxisEnabled.True },
                AxisY = { LineWidth = 2, LineColor = Color.Black, Enabled = AxisEnabled.True },
                CursorX = { IsUserEnabled = true, IsUserSelectionEnabled = true, SelectionColor = Color.SkyBlue },
                CursorY = { IsUserEnabled = true, IsUserSelectionEnabled = true, SelectionColor = Color.SkyBlue },
                BackColor = Color.White,
                BorderDashStyle = ChartDashStyle.NotSet,
                BorderColor = Color.Black,
                Position = { Height = 85, Width = 95, X = 0, Y = 13 }
            };

            _chart.ChartAreas.Add(chartArea);
            _chart.BackGradientStyle = GradientStyle.TopBottom;
            _chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            _chart.BorderlineDashStyle = ChartDashStyle.Solid;
            _chart.BorderlineWidth = 2;
            _chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // 添加图例
            Legend legend = new Legend
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Far,
                LegendStyle = LegendStyle.Row,
                BorderColor = Color.Black,
                BorderWidth = 1,
                BorderDashStyle = ChartDashStyle.Solid
            };
            _chart.Legends.Add(legend);

            _chart.MouseMove += Chart_MouseMove;
            _chart.MouseWheel += Chart_MouseWheel;
        }

        //public void Show(chartData[] dataSets)
        //{
        //    double totalTimeInSeconds = 0; // 总计用时（秒）

        //    foreach (var dataSet in dataSets)
        //    {
        //        string[] arr = dataSet.temperature.TrimEnd('@').Split('@');

        //        DateTime[] _times = new DateTime[arr.Length];

        //        for (int i = 0; i < arr.Length; i++)
        //        {
        //            _times[i] = _startTime.AddSeconds((i - arr.Length) * 30);
        //        }

        //        Series series = _chart.Series[dataSets.ToList().IndexOf(dataSet)];
        //        for (int i = 0; i < arr.Length; i++)
        //        {
        //            series.Points.AddXY(i + 1, Convert.ToDouble(arr[i]));
        //        }

        //        string s_markStep = dataSet.craft;
        //        if (!string.IsNullOrEmpty(s_markStep))
        //        {
        //            string[] sa_arr = s_markStep.TrimEnd('@').Split('@');
        //            foreach (var item in sa_arr)
        //            {
        //                string[] parts = item.Split(',');
        //                string s_name = parts[0];
        //                int s_num = Convert.ToInt32(parts[1]);

        //                if (s_num <= series.Points.Count)
        //                {
        //                    var point = series.Points[s_num - 1];
        //                    point.MarkerColor = Color.Blue;
        //                    point.MarkerSize = 10;
        //                    point.MarkerStyle = MarkerStyle.Triangle;
        //                    point.Label = s_name;
        //                    point.Font = new Font("Consolas", 12f);
        //                    point.LabelForeColor = Color.Blue;
        //                }
        //            }
        //        }

        //        // 计算总计用时
        //        totalTimeInSeconds += arr.Length * 30; // 每个点代表30秒
        //        if (_maxTimes.Length < _times.Length)
        //        {
        //            _maxTimes = _times;
        //        }
        //        // 将总计用时转换为时分秒
        //        TimeSpan totalTime = TimeSpan.FromSeconds(totalTimeInSeconds);
        //        string totalTimeFormatted = $"{totalTime.Hours:D2}小时 {totalTime.Minutes:D2}分钟 {totalTime.Seconds:D2}秒";

        //        // 在图例中显示总计用时
        //        Legend legend = _chart.Legends[0];
        //        legend.Title = $"总计用时: {totalTimeFormatted}";
        //    }


        //}


        public void Show(chartData[] dataSets)
        {
            foreach (var dataSet in dataSets)
            {
                double totalTimeInSeconds = 0; // 每条曲线的总计用时（秒）
                string[] arr = dataSet.temperature.TrimEnd('@').Split('@');

                DateTime[] _times = new DateTime[arr.Length];

                for (int i = 0; i < arr.Length; i++)
                {
                    _times[i] = _startTime.AddSeconds((i - arr.Length) * 30);
                }

                Series series = _chart.Series[dataSets.ToList().IndexOf(dataSet)];
                for (int i = 0; i < arr.Length; i++)
                {
                    series.Points.AddXY(i + 1, Convert.ToDouble(arr[i]));
                }

                string s_markStep = dataSet.craft;
                if (!string.IsNullOrEmpty(s_markStep))
                {
                    string[] sa_arr = s_markStep.TrimEnd('@').Split('@');
                    foreach (var item in sa_arr)
                    {
                        string[] parts = item.Split(',');
                        string s_name = parts[0];
                        int s_num = Convert.ToInt32(parts[1]);

                        if (s_num <= series.Points.Count)
                        {
                            var point = series.Points[s_num - 1];
                            point.MarkerColor = Color.Blue;
                            point.MarkerSize = 10;
                            point.MarkerStyle = MarkerStyle.Triangle;
                            point.Label = s_name;
                            point.Font = new Font("Consolas", 12f);
                            point.LabelForeColor = Color.Blue;
                        }
                    }
                }

                // 计算每条曲线的总计用时
                totalTimeInSeconds = arr.Length * 30; // 每个点代表30秒

                // 将总计用时转换为时分秒
                TimeSpan totalTime = TimeSpan.FromSeconds(totalTimeInSeconds);
                string totalTimeFormatted = $"{totalTime.Hours:D2}小时 {totalTime.Minutes:D2}分钟 {totalTime.Seconds:D2}秒";

                // 在图例中显示每条曲线的总计用时
                LegendItem legendItem = new LegendItem
                {
                    Name = series.Name,
                    Color = series.Color,
                    BorderColor = series.BorderColor,
                    BorderWidth = series.BorderWidth,
                    MarkerStyle = series.MarkerStyle,
                    MarkerSize = series.MarkerSize,
                    MarkerColor = series.MarkerColor,
                    MarkerBorderColor = series.MarkerBorderColor,
                    MarkerBorderWidth = series.MarkerBorderWidth,
                    ShadowColor = series.ShadowColor,
                    ShadowOffset = series.ShadowOffset,
                    Tag = series.Tag,
                    ToolTip = series.ToolTip
                };

                // 使用 LegendItem.Cells 设置文本
                legendItem.Cells.Add(LegendCellType.Text, $"{series.Name} - 总计: {totalTimeFormatted}", ContentAlignment.MiddleLeft);

                _chart.Legends[0].CustomItems.Add(legendItem);

                if (_maxTimes.Length < _times.Length)
                {
                    _maxTimes = _times;
                }
            }
        }




        public void AddSeries(string seriesName, Color seriesColor)
        {
            Series series = new Series(seriesName)
            {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.Double,
                MarkerStyle = MarkerStyle.Circle,
                MarkerColor = Color.Black,
                MarkerSize = 3,
                Color = seriesColor,
                BorderWidth = 2,
                CustomProperties = "PointWidth=2",
                IsValueShownAsLabel = false
            };
            _chart.Series.Add(series);
            series.Legend = _chart.Legends[0].Name; // 将系列添加到图例中
        }





        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (_chart.Series.Count > 0)
                {
                    _chart.ChartAreas[0].CursorX.SetCursorPixelPosition(new PointF(e.X, e.Y), true);
                    _chart.ChartAreas[0].CursorY.SetCursorPixelPosition(new PointF(e.X, e.Y), true);
                    int cursorPosition = Convert.ToInt32(_chart.ChartAreas[0].CursorX.Position.ToString()) - 1;

                    if (cursorPosition >= 0)
                    {
                        string tooltipText;
                        if (_chart.Series.Count == 1)
                        {
                            if (cursorPosition < _chart.Series[0].Points.Count)
                            {
                                double temperature = Math.Round(_chart.Series[0].Points[cursorPosition].YValues[0], 2);
                                tooltipText = _language == 0
                                    ? string.Format("序号:{0},时间:{1}, 温度:{2}",
                                        _chart.Series[0].Points[cursorPosition].XValue,
                                        _maxTimes[cursorPosition].ToString(),
                                        temperature)
                                    : string.Format("Index:{0},Time:{1}, Temperature:{2}",
                                        _chart.Series[0].Points[cursorPosition].XValue,
                                        _maxTimes[cursorPosition].ToString(),
                                        temperature);
                            }
                            else
                            {
                                tooltipText = "";
                            }
                        }
                        else
                        {
                            if (cursorPosition < _chart.Series[0].Points.Count ||
                                cursorPosition < _chart.Series[1].Points.Count)
                            {
                                string temp1 = cursorPosition < _chart.Series[0].Points.Count
                                    ? Math.Round(_chart.Series[0].Points[cursorPosition].YValues[0], 2).ToString()
                                    : "null";
                                string temp2 = cursorPosition < _chart.Series[1].Points.Count
                                    ? Math.Round(_chart.Series[1].Points[cursorPosition].YValues[0], 2).ToString()
                                    : "null";

                                string index = cursorPosition < _chart.Series[0].Points.Count
                                    ? _chart.Series[0].Points[cursorPosition].XValue.ToString()
                                    : _chart.Series[1].Points[cursorPosition].XValue.ToString();

                                tooltipText = _language == 0
                                    ? string.Format("序号:{0},时间:{1}, 温度:{2}/{3}",
                                        index,
                                        _maxTimes[cursorPosition].ToString(),
                                        temp1,
                                        temp2)
                                    : string.Format("Index:{0},Time:{1}, Temperature:{2}/{3}",
                                        index,
                                        _maxTimes[cursorPosition].ToString(),
                                        temp1,
                                        temp2);
                            }
                            else
                            {
                                tooltipText = "";
                            }
                        }
                        toolTip1.SetToolTip(_chart, tooltipText);
                    }
                }
            }
            catch { }
        }

        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart1 = (Chart)sender;
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






        public chartData GenerateChartData(ProcessStep[] processSteps)
        {
            StringBuilder temperatureBuilder = new StringBuilder();
            StringBuilder craftBuilder = new StringBuilder();
            double currentTemperature = 25.0; // 常温起步
            int timePoint = 0; // 从第0个点开始
            double ambientTemperature = 25.0; // 常温
            double coolingConstant = 0.1; // 冷却常数

            foreach (var step in processSteps)
            {
                double fixedDuration = 0;
                double duration = 0;
                // 在每个步骤开始时记录工艺步骤和当前时间点
                if (timePoint == 0)
                    craftBuilder.Append($"{step.StepName},{1}@");
                else
                    craftBuilder.Append($"{step.StepName},{timePoint}@");

                switch (step.StepName)
                {
                    case "放布":
                    case "出布":
                    case "取小样":
                    case "测PH":
                        // 固定时间设定为3分钟
                        fixedDuration = step.Duration ?? 3;
                        for (int i = 0; i < fixedDuration * 2; i++) // 每分钟记录两个温度值
                        {
                            currentTemperature -= coolingConstant * (currentTemperature - ambientTemperature) / 2; // 根据牛顿冷却定律降温
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }
                        break;
                    case "加A":
                    case "加B":
                    case "加C":
                    case "加D":
                    case "加E":
                    case "加F":
                    case "加G":
                    case "加H":
                    case "加I":
                    case "加J":
                    case "加K":
                    case "加L":
                    case "加M":
                    case "加N":
                        // 固定时间设定为0.5分钟
                        fixedDuration = step.Duration ?? 0.5;
                        for (int i = 0; i < fixedDuration * 2; i++) // 每分钟记录两个温度值
                        {
                            currentTemperature -= coolingConstant * (currentTemperature - ambientTemperature) / 2; // 根据牛顿冷却定律降温
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }
                        break;

                    case "洗杯":
                        // 固定时间设定为10分钟
                        fixedDuration = step.Duration ?? 10;
                        for (int i = 0; i < fixedDuration * 2; i++) // 每分钟记录两个温度值
                        {
                            currentTemperature -= coolingConstant * (currentTemperature - ambientTemperature) / 2; // 根据牛顿冷却定律降温
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }
                        break;

                    case "排液":
                        // 使用结构体中的时间参数，默认0.25分钟
                        duration = step.Duration ?? 0.25;
                        for (int i = 0; i < duration * 2; i++) // 每分钟记录两个温度值
                        {
                            currentTemperature -= coolingConstant * (currentTemperature - ambientTemperature) / 2; // 根据牛顿冷却定律降温
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }
                        break;
                    case "加水":
                        // 使用结构体中的时间参数，默认1分钟
                        duration = step.Duration ?? 1;
                        for (int i = 0; i < duration * 2; i++) // 每分钟记录两个温度值
                        {
                            currentTemperature -= coolingConstant * (currentTemperature - ambientTemperature) / 2; // 根据牛顿冷却定律降温
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }
                        break;
                    case "冷行":
                        // 使用结构体中的时间参数，默认5分钟
                        duration = step.Duration ?? 5;
                        for (int i = 0; i < duration * 2; i++) // 每分钟记录两个温度值
                        {
                            currentTemperature -= coolingConstant * (currentTemperature - ambientTemperature) / 2; // 根据牛顿冷却定律降温
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }
                        break;
                    case "搅拌":
                        // 使用结构体中的时间参数，默认5分钟
                        duration = step.Duration ?? 5;
                        for (int i = 0; i < duration * 2; i++) // 每分钟记录两个温度值
                        {
                            currentTemperature -= coolingConstant * (currentTemperature - ambientTemperature) / 2; // 根据牛顿冷却定律降温
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }
                        break;
                    case "温控":
                        // 使用结构体中的目标温度，保温时间，升温速率
                        double targetTemperature = step.TargetTemperature ?? 100;
                        double holdTime = step.Duration ?? 120;
                        double heatingRate = step.HeatingRate ?? 1;

                        // 升温或降温阶段
                        double temperatureDifference = targetTemperature - currentTemperature;
                        double heatingTime = Math.Abs(temperatureDifference) / heatingRate; // 转换为分钟
                        int heatingPoints = (int)(heatingTime * 2); // 每分钟记录两个温度值
                        for (int i = 0; i < heatingPoints; i++)
                        {
                            currentTemperature += (temperatureDifference > 0 ? heatingRate : -heatingRate) / 2; // 每分钟升温或降温
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }

                        // 保温阶段
                        int holdPoints = (int)(holdTime * 2); // 每分钟记录两个温度值
                        for (int i = 0; i < holdPoints; i++)
                        {
                            temperatureBuilder.Append($"{currentTemperature}@");
                            timePoint++;
                        }
                        break;

                    default:
                        throw new ArgumentException($"未知的工艺步骤: {step.StepName}");
                }
            }

            return new chartData
            {
                temperature = temperatureBuilder.ToString().TrimEnd('@'),
                craft = craftBuilder.ToString().TrimEnd('@')
            };
        }


    }
}
