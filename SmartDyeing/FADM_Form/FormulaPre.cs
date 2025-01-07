using SmartDyeing.FADM_Control;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using static SmartDyeing.FADM_Control.CurveControl;

namespace SmartDyeing.FADM_Form
{
    public partial class FormulaPre : Form
    {
        private SmartDyeing.FADM_Control.Formula PFormula;

        private SmartDyeing.FADM_Control.Formula_Cloth PFormula_Cloth;


        private CurveControl _userControl1;

        public FormulaPre()
        {
            InitializeComponent();
        }
        public FormulaPre(SmartDyeing.FADM_Control.Formula Formula)
        {
            InitializeComponent();
            this.PFormula = Formula;
            int height = this.Height;
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height-35;
            this.Height = 360  + (PFormula.dgv_FormulaData.Rows.Count * 30) + 300;
            
            this.flowLayoutPanel1.Height = this.flowLayoutPanel1.Height + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - height - 35;
        }

        public FormulaPre(SmartDyeing.FADM_Control.Formula_Cloth Formula)
        {
            InitializeComponent();
            this.PFormula_Cloth = Formula;
            int height = this.Height;
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height-35;
            this.Height = 360 + (PFormula_Cloth.dgv_FormulaData.Rows.Count * 30) + 300;

            this.flowLayoutPanel1.Height = this.flowLayoutPanel1.Height + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - height - 35;
        }

        private void FormulaPre_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(this.Location.X, 0);
            myParLoadDa();
            this.KeyDown+= new KeyEventHandler(textBox_KeyDown);
        }
        // 事件处理方法
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Communal._b_isUseCloth) {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PFormula_Cloth.btn_Save.Focus();
                    this.Close();
                    e.Handled = true;
                }
            }
            else {
                this.PFormula.btn_Save.Focus();
                this.Close();
                e.Handled = true;
            }
           
        }
        private void myParLoadDa() {
            if (Communal._b_isUseCloth)
            {
                try
                {
                    Dictionary<string, string> mapD = new Dictionary<string, string>();
                    foreach (Control c in PFormula_Cloth.grp_FormulaData.Controls)
                    {
                        if (c.Name.Contains("txt_"))
                        {
                            mapD.Add(c.Name, c.Text);
                        }
                        else if (c is CheckBox && c.Name.Equals("chk_AddWaterChoose"))
                        {
                            CheckBox ck = (CheckBox)c;
                            mapD.Add(c.Name, ck.Checked ? "是" : "否");
                        }
                    }
                    foreach (Control c in this.panel1.Controls)
                    {
                        if (c is Label && c.Name.Contains("txt_") && mapD.ContainsKey(c.Name))
                        {
                            c.Text = mapD[c.Name];
                        }
                        else if (c.Name.Equals("chk_AddWaterChoose"))
                        {
                            c.Text = mapD["chk_AddWaterChoose"];
                        }
                    }
                    int index = 1;
                    int count = 1;
                    //配方
                    foreach (DataGridViewRow dgvr in PFormula_Cloth.dgv_FormulaData.Rows)
                    {
                        if (dgvr.Cells[1].Value != null && dgvr.Cells[2].Value != null && dgvr.Cells[3].Value != null)
                        {
                            dgv_FormulaData.Rows.Add("染色", dgvr.Cells[1].Value.ToString().Trim(),
                               dgvr.Cells[2].Value.ToString().Trim(), dgvr.Cells[3].Value.ToString().Trim(),
                               index.ToString().Trim(), "自动", dgvr.Cells[8].Value.ToString().Trim(),
                               "克", "");
                            index = index + 1;
                            count = count + 1;
                        }
                    }


                    List<ProcessStep> list = new List<ProcessStep>();
                    //加A 加B的数据
                    for (int i = 0; i < FADM_Control.Formula_Cloth.myDyeSelectList.Count; i++)
                    {
                        string name = FADM_Control.Formula_Cloth.myDyeSelectList[i].Name;
                        if (FADM_Control.Formula_Cloth.mymap.ContainsKey(name))
                        {
                            if (FADM_Control.Formula_Cloth.mymap[name].dgv_Dye.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow dgvr in FADM_Control.Formula_Cloth.mymap[name].dgv_Dye.Rows)
                                {
                                    if (dgvr.Cells[1].Value == null || dgvr.Cells[1].Value.ToString().Length == 0)
                                    {
                                        continue;
                                    }
                                    dgv_FormulaData.Rows.Add(FADM_Control.Formula_Cloth.myDyeSelectList[i].dy_type_comboBox1.Text, dgvr.Cells[1].Value.ToString().Trim(),
                                       dgvr.Cells[2].Value.ToString().Trim(), dgvr.Cells[3].Value.ToString().Trim(),
                                       index.ToString().Trim(), "自动", dgvr.Cells[8].Value.ToString().Trim(),
                                       "克", FADM_Control.Formula_Cloth.myDyeSelectList[i].dy_nodelist_comboBox2.Text);
                                    index = index + 1;
                                    count = count + 1;


                                }

                            }
                            //
                            if (FADM_Control.Formula_Cloth.mymap[name].dgv_dyconfiglisg.Rows.Count > 0)
                            {
                                ProcessStep processSte;
                                foreach (DataGridViewRow dgvr in FADM_Control.Formula_Cloth.mymap[name].dgv_dyconfiglisg.Rows)
                                {
                                    processSte = new ProcessStep();
                                    if (dgvr.Cells[1] != null)
                                    {
                                        processSte.StepName = dgvr.Cells[1].Value.ToString();
                                    }

                                    if (dgvr.Cells[1].Value.ToString().Trim().Equals("加A") || dgvr.Cells[1].Value.ToString().Trim().Equals("加B") || dgvr.Cells[1].Value.ToString().Trim().Equals("加C") || dgvr.Cells[1].Value.ToString().Trim().Equals("加D") || dgvr.Cells[1].Value.ToString().Trim().Equals("加E") || dgvr.Cells[1].Value.ToString().Trim().Equals("加F") || dgvr.Cells[1].Value.ToString().Trim().Equals("加G") || dgvr.Cells[1].Value.ToString().Trim().Equals("加H") || dgvr.Cells[1].Value.ToString().Trim().Equals("加I") || dgvr.Cells[1].Value.ToString().Trim().Equals("加J") || dgvr.Cells[1].Value.ToString().Trim().Equals("加K") || dgvr.Cells[1].Value.ToString().Trim().Equals("加L") || dgvr.Cells[1].Value.ToString().Trim().Equals("加M") || dgvr.Cells[1].Value.ToString().Trim().Equals("加N"))
                                    {
                                        //processSte.Duration = 5;
                                        list.Add(processSte);
                                        continue;
                                    }
                                    if (dgvr.Cells[1].Value.ToString().Trim().Equals("加水"))
                                    {
                                        list.Add(processSte);
                                        continue;
                                    }

                                    if (dgvr.Cells[2] != null && dgvr.Cells[2].Value.ToString().Length > 0)
                                    {
                                        processSte.TargetTemperature = Convert.ToDouble(dgvr.Cells[2].Value.ToString());
                                    }
                                    if (dgvr.Cells[3] != null && dgvr.Cells[3].Value.ToString().Length > 0)
                                    {
                                        processSte.HeatingRate = Convert.ToDouble(dgvr.Cells[3].Value.ToString());
                                    }
                                    if (dgvr.Cells[4] != null && dgvr.Cells[4].Value.ToString().Length > 0)
                                    {
                                        processSte.Duration = Convert.ToDouble(dgvr.Cells[4].Value.ToString());
                                    }
                                    list.Add(processSte);
                                }
                            }
                        }
                    }
                    dgv_FormulaData.Height = 30 * count + 10;

                    if (list != null && list.Count > 0)
                    { //list!=null && list.Count>0
                      // 创建Chart控件
                        Chart chart = new Chart
                        {
                            Dock = DockStyle.Bottom,
                            Width = this.Width - 30
                        };
                        chart.KeyDown += textBox_KeyDown;
                        this.flowLayoutPanel1.Controls.Add(chart);

                        // 初始化CurveControl
                        _userControl1 = new CurveControl(0, chart);
                        // 添加两条曲线
                        _userControl1.AddSeries("理论", Color.Blue);
                        _userControl1.AddSeries("实际", Color.Red);

                        string cc = PFormula_Cloth.txt_CupNum.Text;
                        string s_cupno = "";
                        string s_markStep = "";
                        if (!PFormula_Cloth.txt_CupNum.Text.Equals("0"))
                        {
                            string s_sql = "SELECT CupNum, FormulaCode, VersionNum" +
                                           " FROM drop_head where CupNum = " + PFormula_Cloth.txt_CupNum.Text + " and FormulaCode=" + mapD["txt_FormulaCode"] + ";";
                            DataTable dt_formula = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                            if (dt_formula.Rows.Count > 0)
                            {
                                s_cupno = Lib_File.Txt.ReadTXT(Convert.ToInt32(PFormula_Cloth.txt_CupNum.Text));
                                s_markStep = Lib_File.Txt.ReadMarkTXT(Convert.ToInt32(PFormula_Cloth.txt_CupNum.Text));
                            }
                        }

                        /*string s_cupno = Lib_File.Txt.ReadTXT(CupNo);
                        string s_markStep = Lib_File.Txt.ReadMarkTXT(CupNo);*/


                        // 准备工艺步骤数据
                        /*ProcessStep[] processSteps = new ProcessStep[]
                        {
                            new ProcessStep { StepName = "放布" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 98, HeatingRate = 0.8, Duration = 20 },
                            new ProcessStep { StepName = "温控", TargetTemperature = 55, HeatingRate = 1.5, Duration = 0 },
                            new ProcessStep { StepName = "加A" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 80, HeatingRate = 0.7, Duration = 5 },
                            new ProcessStep { StepName = "温控", TargetTemperature = 98, HeatingRate = 0.9, Duration = 30 },
                            new ProcessStep { StepName = "温控", TargetTemperature = 60, HeatingRate = 1.5, Duration = 0 },
                            new ProcessStep { StepName = "排液" },
                            new ProcessStep { StepName = "加水" },
                            new ProcessStep { StepName = "搅拌" , Duration = 1 },
                            new ProcessStep { StepName = "排液" },
                            new ProcessStep { StepName = "加A" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 50, HeatingRate = 2.5, Duration = 0 },
                            new ProcessStep { StepName = "加B" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 60, HeatingRate = 2, Duration = 30 },
                            new ProcessStep { StepName = "温控", TargetTemperature = 55, HeatingRate = 1.5, Duration = 0 },
                            new ProcessStep { StepName = "加C" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 55, HeatingRate = 2.5, Duration = 30 },
                            new ProcessStep { StepName = "排液" },
                            new ProcessStep { StepName = "出布", Duration = 5 },
                            new ProcessStep { StepName = "洗杯", Duration = 10 }
                        };*/
                        ProcessStep[] processSteps = list.ToArray();
                        // 生成chartData
                        CurveControl.chartData chartData = _userControl1.GenerateChartData(processSteps);

                        // 准备数据集

                        CurveControl.chartData[] dataSets = null;
                        if ((s_cupno != null && s_cupno.Length != 0) && (s_markStep != null && s_markStep.Length != 0))
                        {
                            dataSets = new CurveControl.chartData[2];
                            dataSets[0] = chartData;
                            dataSets[1] = new CurveControl.chartData
                            {
                                temperature = s_cupno,
                                craft = s_markStep
                            };
                        }
                        else
                        {
                            dataSets = new CurveControl.chartData[1];
                            dataSets[0] = chartData;
                        }

                        //注释的实际图表，这个要滴液完和染固色后才有数据 先null
                        // 显示生成的图表数据
                        _userControl1.Show(dataSets);
                    }

                }
                catch (Exception ex)
                {
                }

            }
            else {
                try
                {
                    Dictionary<string, string> mapD = new Dictionary<string, string>();
                    foreach (Control c in PFormula.grp_FormulaData.Controls)
                    {
                        if (c.Name.Contains("txt_"))
                        {
                            mapD.Add(c.Name, c.Text);
                        }
                        else if (c is CheckBox && c.Name.Equals("chk_AddWaterChoose"))
                        {
                            CheckBox ck = (CheckBox)c;
                            mapD.Add(c.Name, ck.Checked ? "是" : "否");
                        }
                    }
                    foreach (Control c in this.panel1.Controls)
                    {
                        if (c is Label && c.Name.Contains("txt_") && mapD.ContainsKey(c.Name))
                        {
                            c.Text = mapD[c.Name];
                        }
                        else if (c.Name.Equals("chk_AddWaterChoose"))
                        {
                            c.Text = mapD["chk_AddWaterChoose"];
                        }
                    }
                    int index = 1;
                    int count = 1;
                    //配方
                    foreach (DataGridViewRow dgvr in PFormula.dgv_FormulaData.Rows)
                    {
                        if (dgvr.Cells[1].Value != null && dgvr.Cells[2].Value != null && dgvr.Cells[3].Value != null)
                        {
                            dgv_FormulaData.Rows.Add("染色", dgvr.Cells[1].Value.ToString().Trim(),
                               dgvr.Cells[2].Value.ToString().Trim(), dgvr.Cells[3].Value.ToString().Trim(),
                               index.ToString().Trim(), "自动", dgvr.Cells[8].Value.ToString().Trim(),
                               "克", "");
                            index = index + 1;
                            count = count + 1;
                        }
                    }


                    List<ProcessStep> list = new List<ProcessStep>();
                    //加A 加B的数据
                    for (int i = 0; i < FADM_Control.Formula.myDyeSelectList.Count; i++)
                    {
                        string name = FADM_Control.Formula.myDyeSelectList[i].Name;
                        if (FADM_Control.Formula.mymap.ContainsKey(name))
                        {
                            if (FADM_Control.Formula.mymap[name].dgv_Dye.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow dgvr in FADM_Control.Formula.mymap[name].dgv_Dye.Rows)
                                {
                                    if (dgvr.Cells[1].Value == null || dgvr.Cells[1].Value.ToString().Length == 0)
                                    {
                                        continue;
                                    }
                                    dgv_FormulaData.Rows.Add(FADM_Control.Formula.myDyeSelectList[i].dy_type_comboBox1.Text, dgvr.Cells[1].Value.ToString().Trim(),
                                       dgvr.Cells[2].Value.ToString().Trim(), dgvr.Cells[3].Value.ToString().Trim(),
                                       index.ToString().Trim(), "自动", dgvr.Cells[8].Value.ToString().Trim(),
                                       "克", FADM_Control.Formula.myDyeSelectList[i].dy_nodelist_comboBox2.Text);
                                    index = index + 1;
                                    count = count + 1;


                                }

                            }
                            //
                            if (FADM_Control.Formula.mymap[name].dgv_dyconfiglisg.Rows.Count > 0)
                            {
                                ProcessStep processSte;
                                foreach (DataGridViewRow dgvr in FADM_Control.Formula.mymap[name].dgv_dyconfiglisg.Rows)
                                {
                                    processSte = new ProcessStep();
                                    if (dgvr.Cells[1] != null)
                                    {
                                        processSte.StepName = dgvr.Cells[1].Value.ToString();
                                    }

                                    if (dgvr.Cells[1].Value.ToString().Trim().Equals("加A") || dgvr.Cells[1].Value.ToString().Trim().Equals("加B") || dgvr.Cells[1].Value.ToString().Trim().Equals("加C") || dgvr.Cells[1].Value.ToString().Trim().Equals("加D") || dgvr.Cells[1].Value.ToString().Trim().Equals("加E") || dgvr.Cells[1].Value.ToString().Trim().Equals("加F") || dgvr.Cells[1].Value.ToString().Trim().Equals("加G") || dgvr.Cells[1].Value.ToString().Trim().Equals("加H") || dgvr.Cells[1].Value.ToString().Trim().Equals("加I") || dgvr.Cells[1].Value.ToString().Trim().Equals("加J") || dgvr.Cells[1].Value.ToString().Trim().Equals("加K") || dgvr.Cells[1].Value.ToString().Trim().Equals("加L") || dgvr.Cells[1].Value.ToString().Trim().Equals("加M") || dgvr.Cells[1].Value.ToString().Trim().Equals("加N"))
                                    {
                                        //processSte.Duration = 5;
                                        list.Add(processSte);
                                        continue;
                                    }
                                    if (dgvr.Cells[1].Value.ToString().Trim().Equals("加水"))
                                    {
                                        list.Add(processSte);
                                        continue;
                                    }

                                    if (dgvr.Cells[2] != null && dgvr.Cells[2].Value.ToString().Length > 0)
                                    {
                                        processSte.TargetTemperature = Convert.ToDouble(dgvr.Cells[2].Value.ToString());
                                    }
                                    if (dgvr.Cells[3] != null && dgvr.Cells[3].Value.ToString().Length > 0)
                                    {
                                        processSte.HeatingRate = Convert.ToDouble(dgvr.Cells[3].Value.ToString());
                                    }
                                    if (dgvr.Cells[4] != null && dgvr.Cells[4].Value.ToString().Length > 0)
                                    {
                                        processSte.Duration = Convert.ToDouble(dgvr.Cells[4].Value.ToString());
                                    }
                                    list.Add(processSte);
                                }
                            }
                        }
                    }
                    dgv_FormulaData.Height = 30 * count + 10;

                    if (list != null && list.Count > 0)
                    { //list!=null && list.Count>0
                      // 创建Chart控件
                        Chart chart = new Chart
                        {
                            Dock = DockStyle.Bottom,
                            Width = this.Width - 30
                        };
                        chart.KeyDown += textBox_KeyDown;
                        this.flowLayoutPanel1.Controls.Add(chart);

                        // 初始化CurveControl
                        _userControl1 = new CurveControl(0, chart);
                        // 添加两条曲线
                        _userControl1.AddSeries("理论", Color.Blue);
                        _userControl1.AddSeries("实际", Color.Red);

                        string cc = PFormula.txt_CupNum.Text;
                        string s_cupno = "";
                        string s_markStep = "";
                        if (!PFormula.txt_CupNum.Text.Equals("0"))
                        {
                            string s_sql = "SELECT CupNum, FormulaCode, VersionNum" +
                                           " FROM drop_head where CupNum = " + PFormula.txt_CupNum.Text + " and FormulaCode=" + mapD["txt_FormulaCode"] + ";";
                            DataTable dt_formula = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                            if (dt_formula.Rows.Count > 0)
                            {
                                s_cupno = Lib_File.Txt.ReadTXT(Convert.ToInt32(PFormula.txt_CupNum.Text));
                                s_markStep = Lib_File.Txt.ReadMarkTXT(Convert.ToInt32(PFormula.txt_CupNum.Text));
                            }
                        }

                        /*string s_cupno = Lib_File.Txt.ReadTXT(CupNo);
                        string s_markStep = Lib_File.Txt.ReadMarkTXT(CupNo);*/


                        // 准备工艺步骤数据
                        /*ProcessStep[] processSteps = new ProcessStep[]
                        {
                            new ProcessStep { StepName = "放布" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 98, HeatingRate = 0.8, Duration = 20 },
                            new ProcessStep { StepName = "温控", TargetTemperature = 55, HeatingRate = 1.5, Duration = 0 },
                            new ProcessStep { StepName = "加A" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 80, HeatingRate = 0.7, Duration = 5 },
                            new ProcessStep { StepName = "温控", TargetTemperature = 98, HeatingRate = 0.9, Duration = 30 },
                            new ProcessStep { StepName = "温控", TargetTemperature = 60, HeatingRate = 1.5, Duration = 0 },
                            new ProcessStep { StepName = "排液" },
                            new ProcessStep { StepName = "加水" },
                            new ProcessStep { StepName = "搅拌" , Duration = 1 },
                            new ProcessStep { StepName = "排液" },
                            new ProcessStep { StepName = "加A" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 50, HeatingRate = 2.5, Duration = 0 },
                            new ProcessStep { StepName = "加B" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 60, HeatingRate = 2, Duration = 30 },
                            new ProcessStep { StepName = "温控", TargetTemperature = 55, HeatingRate = 1.5, Duration = 0 },
                            new ProcessStep { StepName = "加C" },
                            new ProcessStep { StepName = "温控", TargetTemperature = 55, HeatingRate = 2.5, Duration = 30 },
                            new ProcessStep { StepName = "排液" },
                            new ProcessStep { StepName = "出布", Duration = 5 },
                            new ProcessStep { StepName = "洗杯", Duration = 10 }
                        };*/
                        ProcessStep[] processSteps = list.ToArray();
                        // 生成chartData
                        CurveControl.chartData chartData = _userControl1.GenerateChartData(processSteps);

                        // 准备数据集

                        CurveControl.chartData[] dataSets = null;
                        if ((s_cupno != null && s_cupno.Length != 0) && (s_markStep != null && s_markStep.Length != 0))
                        {
                            dataSets = new CurveControl.chartData[2];
                            dataSets[0] = chartData;
                            dataSets[1] = new CurveControl.chartData
                            {
                                temperature = s_cupno,
                                craft = s_markStep
                            };
                        }
                        else
                        {
                            dataSets = new CurveControl.chartData[1];
                            dataSets[0] = chartData;
                        }

                        //注释的实际图表，这个要滴液完和染固色后才有数据 先null
                        // 显示生成的图表数据
                        _userControl1.Show(dataSets);
                    }

                }
                catch (Exception ex)
                {
                }

            }



            
           
           
        }

    }
}
