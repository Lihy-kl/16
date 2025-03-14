using Newtonsoft.Json.Linq;
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

namespace SmartDyeing.FADM_Control
{
    public partial class ParameterData : UserControl
    {
        //页面
        int _i_page = 0;

        int _i_count = 0;

        public ParameterData()
        {
            InitializeComponent();
        }
        //上一页
        private void button5_Click(object sender, EventArgs e)
        {
            //判断是否第一页
            if(Convert.ToInt32(lab_main1.Text) == SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[0])
            {
                return;
            }
            FADM_Object.Communal._b_hasrefreshDye = false;
            _i_page--;
            _i_count = 0;
            //LoadInfo();
        }
        //下一页
        private void button6_Click(object sender, EventArgs e)
        {
            //判断是否最后一页
            if (Convert.ToInt32(lab_assistant4.Text) == SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum.Count-1])
            {
                return;
            }
            FADM_Object.Communal._b_hasrefreshDye = false;
            _i_page++;
            _i_count = 0;
            //LoadInfo();
        }

        void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberTextbox_KeyPress(e);
        }

        //输入检查
        void TextBox_KeyPress1(object sender, KeyPressEventArgs e)
        {
            e.Handled = SmartDyeing.FADM_Object.MyTextBoxCheck.NumberDotTextbox_KeyPress(sender, e);
        }

        private void ParameterData_Load(object sender, EventArgs e)
        {
            FADM_Object.Communal._b_refreshDye = true;

            FADM_Object.Communal._b_hasrefreshDye = false;
            int i=0;

            foreach (Control c in this.panel1.Controls)
            {
                if (c is TextBox)
                {
                    if(c.Name== "txt_mainTem1"|| c.Name == "txt_mainTemCorrection1" || c.Name == "txt_mainControlCycle1" || c.Name == "txt_mainLowTem1" || c.Name == "txt_mainHighTem1"
                        || c.Name == "txt_assistantTem1" || c.Name == "txt_assistantTemCorrection1" || c.Name == "txt_assistantControlCycle1" || c.Name == "txt_assistantLowTem1" || c.Name == "txt_assistantHighTem1")
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }
            foreach (Control c in this.panel2.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "txt_rev1" || c.Name == "txt_zeroVelocity1" || c.Name == "txt_openTem1" || c.Name == "txt_limitTem1" || c.Name == "txt_warmUp1"
                        || c.Name == "txt_warmDown1" || c.Name == "txt_fastTem1" || c.Name == "txt_fastRate1" || c.Name == "txt_washTem1" )
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }
            foreach (Control c in this.panel3.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "txt_mainTem2" || c.Name == "txt_mainTemCorrection2" || c.Name == "txt_mainControlCycle2" || c.Name == "txt_mainLowTem2" || c.Name == "txt_mainHighTem2"
                        || c.Name == "txt_assistantTem2" || c.Name == "txt_assistantTemCorrection2" || c.Name == "txt_assistantControlCycle2" || c.Name == "txt_assistantLowTem2" || c.Name == "txt_assistantHighTem2")
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }
            foreach (Control c in this.panel4.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "txt_rev2" || c.Name == "txt_zeroVelocity2" || c.Name == "txt_openTem2" || c.Name == "txt_limitTem2" || c.Name == "txt_warmUp2"
                        || c.Name == "txt_warmDown2" || c.Name == "txt_fastTem2" || c.Name == "txt_fastRate2" || c.Name == "txt_washTem2")
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }

            foreach (Control c in this.panel5.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "txt_mainTem3" || c.Name == "txt_mainTemCorrection3" || c.Name == "txt_mainControlCycle3" || c.Name == "txt_mainLowTem3" || c.Name == "txt_mainHighTem3"
                        || c.Name == "txt_assistantTem3" || c.Name == "txt_assistantTemCorrection3" || c.Name == "txt_assistantControlCycle3" || c.Name == "txt_assistantLowTem3" || c.Name == "txt_assistantHighTem3")
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }
            foreach (Control c in this.panel6.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "txt_rev3" || c.Name == "txt_zeroVelocity3" || c.Name == "txt_openTem3" || c.Name == "txt_limitTem3" || c.Name == "txt_warmUp3"
                        || c.Name == "txt_warmDown3" || c.Name == "txt_fastTem3" || c.Name == "txt_fastRate3" || c.Name == "txt_washTem3")
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }

            foreach (Control c in this.panel7.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "txt_mainTem4" || c.Name == "txt_mainTemCorrection4" || c.Name == "txt_mainControlCycle4" || c.Name == "txt_mainLowTem4" || c.Name == "txt_mainHighTem4"
                        || c.Name == "txt_assistantTem4" || c.Name == "txt_assistantTemCorrection4" || c.Name == "txt_assistantControlCycle4" || c.Name == "txt_assistantLowTem4" || c.Name == "txt_assistantHighTem4")
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }
            foreach (Control c in this.panel8.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "txt_rev4" || c.Name == "txt_zeroVelocity4" || c.Name == "txt_openTem4" || c.Name == "txt_limitTem4" || c.Name == "txt_warmUp4"
                        || c.Name == "txt_warmDown4" || c.Name == "txt_fastTem4" || c.Name == "txt_fastRate4" || c.Name == "txt_washTem4")
                    {
                        c.KeyPress += TextBox_KeyPress1;
                    }
                    else
                    {
                        c.KeyPress += TextBox_KeyPress;
                    }
                }
            }

            LoadInfo();
        }
        //加载显示参数
        void LoadInfo()
        {
            lab_main1.Text = SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page*8+0].ToString();
            lab_assistant1.Text = SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 1].ToString();
            lab_main2.Text = SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2].ToString();
            lab_assistant2.Text = SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 3].ToString();
            lab_main3.Text = SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4].ToString();
            lab_assistant3.Text = SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 5].ToString();
            lab_main4.Text = SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6].ToString();
            lab_assistant4.Text = SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 7].ToString();

            if (FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]] !=null&&FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._b_refresh )
            {
                //刷新一次数据后就不再更新，只更新温度和当前AD值
                if (!FADM_Object.Communal._b_hasrefreshDye)
                {
                    txt_mainTemCorrection1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainTemCorrection;

                    txt_mainControlCycle1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainControlCycle;

                    txt_mainP1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainP;

                    txt_mainI1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainI;

                    txt_mainD1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainD;

                    

                    txt_mainLowTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainLowTem;

                    txt_mainLowTemAD1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainLowTemAD;

                    txt_mainHighTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainHighTem;

                    txt_mainHighTemAD1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainHighTemAD;

                    

                    txt_assistantTemCorrection1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantTemCorrection;

                    txt_assistantControlCycle1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantControlCycle;

                    txt_assistantP1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantP;
                    txt_assistantI1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantI;
                    txt_assistantD1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantD;

                    

                    txt_assistantLowTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantLowTem;

                    txt_assistantLowTemAD1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantLowTemAD;

                    txt_assistantHighTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantHighTem;

                    txt_assistantHighTemAD1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantHighTemAD;

                    txt_rev1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_rev;

                    txt_zeroVelocity1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_zeroVelocity;

                    txt_decelerationTime1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_decelerationTime;

                    txt_forwardTime1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_forwardTime;

                    txt_pauseTime1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_pauseTime;

                    txt_reversalTime1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_reversalTime;

                    txt_openTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_openTem;

                    txt_limitTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_limitTem;

                    txt_warmUp1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_warmUp;

                    txt_warmDown1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_warmDown;

                    txt_openCoverDrainage1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_openCoverDrainage;

                    txt_closeCoverDrainage1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_closeCoverDrainage;

                    txt_currenAlarmValue1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_currenAlarmValue;

                    txt_alarmTime1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_alarmTime;

                    txt_fastTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_fastTem;

                    txt_fastRate1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_fastRate;

                    txt_washTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_washTem;
                }

                txt_mainTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainTem;

                txt_mainCurrenAD1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_mainCurrenAD;

                txt_assistantTem1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantTem;
                txt_assistantCurrenAD1.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 0]]]._s_assistantCurrenAD;
            }

            if (FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]] != null && FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._b_refresh)
            {
                //刷新一次数据后就不再更新，只更新温度和当前AD值
                if (!FADM_Object.Communal._b_hasrefreshDye)
                {
                    txt_mainTemCorrection2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainTemCorrection;

                    txt_mainControlCycle2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainControlCycle;

                    txt_mainP2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainP;

                    txt_mainI2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainI;

                    txt_mainD2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainD;

                    

                    txt_mainLowTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainLowTem;

                    txt_mainLowTemAD2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainLowTemAD;

                    txt_mainHighTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainHighTem;

                    txt_mainHighTemAD2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainHighTemAD;

                    

                    txt_assistantTemCorrection2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantTemCorrection;

                    txt_assistantControlCycle2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantControlCycle;

                    txt_assistantP2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantP;
                    txt_assistantI2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantI;
                    txt_assistantD2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantD;

                    

                    txt_assistantLowTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantLowTem;

                    txt_assistantLowTemAD2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantLowTemAD;

                    txt_assistantHighTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantHighTem;

                    txt_assistantHighTemAD2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantHighTemAD;

                    txt_rev2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_rev;

                    txt_zeroVelocity2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_zeroVelocity;

                    txt_decelerationTime2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_decelerationTime;

                    txt_forwardTime2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_forwardTime;

                    txt_pauseTime2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_pauseTime;

                    txt_reversalTime2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_reversalTime;

                    txt_openTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_openTem;

                    txt_limitTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_limitTem;

                    txt_warmUp2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_warmUp;

                    txt_warmDown2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_warmDown;

                    txt_openCoverDrainage2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_openCoverDrainage;

                    txt_closeCoverDrainage2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_closeCoverDrainage;

                    txt_currenAlarmValue2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_currenAlarmValue;

                    txt_alarmTime2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_alarmTime;

                    txt_fastTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_fastTem;

                    txt_fastRate2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_fastRate;

                    txt_washTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_washTem;
                }
                txt_mainTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainTem;

                txt_mainCurrenAD2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_mainCurrenAD;

                txt_assistantTem2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantTem;

                txt_assistantCurrenAD2.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 2]]]._s_assistantCurrenAD;
            }

            if (FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]] != null&&FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._b_refresh)
            {
                //刷新一次数据后就不再更新，只更新温度和当前AD值
                if (!FADM_Object.Communal._b_hasrefreshDye)
                {
                    txt_mainTemCorrection3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainTemCorrection;

                    txt_mainControlCycle3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainControlCycle;

                    txt_mainP3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainP;

                    txt_mainI3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainI;

                    txt_mainD3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainD;

                    

                    txt_mainLowTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainLowTem;

                    txt_mainLowTemAD3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainLowTemAD;

                    txt_mainHighTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainHighTem;

                    txt_mainHighTemAD3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainHighTemAD;

                    

                    txt_assistantTemCorrection3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantTemCorrection;

                    txt_assistantControlCycle3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantControlCycle;

                    txt_assistantP3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantP;
                    txt_assistantI3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantI;
                    txt_assistantD3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantD;

                    txt_assistantLowTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantLowTem;

                    txt_assistantLowTemAD3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantLowTemAD;

                    txt_assistantHighTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantHighTem;

                    txt_assistantHighTemAD3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantHighTemAD;

                    txt_rev3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_rev;

                    txt_zeroVelocity3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_zeroVelocity;

                    txt_decelerationTime3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_decelerationTime;

                    txt_forwardTime3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_forwardTime;

                    txt_pauseTime3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_pauseTime;

                    txt_reversalTime3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_reversalTime;

                    txt_openTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_openTem;

                    txt_limitTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_limitTem;

                    txt_warmUp3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_warmUp;

                    txt_warmDown3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_warmDown;

                    txt_openCoverDrainage3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_openCoverDrainage;

                    txt_closeCoverDrainage3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_closeCoverDrainage;

                    txt_currenAlarmValue3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_currenAlarmValue;

                    txt_alarmTime3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_alarmTime;

                    txt_fastTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_fastTem;

                    txt_fastRate3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_fastRate;

                    txt_washTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_washTem;
                }
                txt_mainTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainTem;

                txt_mainCurrenAD3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_mainCurrenAD;

                txt_assistantTem3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantTem;

                txt_assistantCurrenAD3.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 4]]]._s_assistantCurrenAD;
            }

            if (FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]] != null && FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._b_refresh)
            {
                //刷新一次数据后就不再更新，只更新温度和当前AD值
                if (!FADM_Object.Communal._b_hasrefreshDye)
                {
                    txt_mainTemCorrection4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainTemCorrection;

                    txt_mainControlCycle4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainControlCycle;

                    txt_mainP4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainP;

                    txt_mainI4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainI;

                    txt_mainD4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainD;

                    

                    txt_mainLowTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainLowTem;

                    txt_mainLowTemAD4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainLowTemAD;

                    txt_mainHighTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainHighTem;

                    txt_mainHighTemAD4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainHighTemAD;

                    

                    txt_assistantTemCorrection4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantTemCorrection;

                    txt_assistantControlCycle4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantControlCycle;

                    txt_assistantP4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantP;
                    txt_assistantI4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantI;
                    txt_assistantD4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantD;

                    

                    txt_assistantLowTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantLowTem;

                    txt_assistantLowTemAD4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantLowTemAD;

                    txt_assistantHighTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantHighTem;

                    txt_assistantHighTemAD4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantHighTemAD;

                    txt_rev4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_rev;

                    txt_zeroVelocity4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_zeroVelocity;

                    txt_decelerationTime4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_decelerationTime;

                    txt_forwardTime4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_forwardTime;

                    txt_pauseTime4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_pauseTime;

                    txt_reversalTime4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_reversalTime;

                    txt_openTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_openTem;

                    txt_limitTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_limitTem;

                    txt_warmUp4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_warmUp;

                    txt_warmDown4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_warmDown;

                    txt_openCoverDrainage4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_openCoverDrainage;

                    txt_closeCoverDrainage4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_closeCoverDrainage;

                    txt_currenAlarmValue4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_currenAlarmValue;

                    txt_alarmTime4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_alarmTime;

                    txt_fastTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_fastTem;

                    txt_fastRate4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_fastRate;

                    txt_washTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_washTem;
                    _i_count++;
                    if (_i_count > 1)
                        FADM_Object.Communal._b_hasrefreshDye = true;
                }
                txt_mainTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainTem;

                txt_mainCurrenAD4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_mainCurrenAD;

                txt_assistantTem4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantTem;

                txt_assistantCurrenAD4.Text = FADM_Auto.Dye._pd[SmartDyeing.FADM_Object.Communal._dic_SixteenCupNum[SmartDyeing.FADM_Object.Communal._lis_SixteenCupNum[_i_page * 8 + 6]]]._s_assistantCurrenAD;
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadInfo();
        }

        private void ParameterData_Leave(object sender, EventArgs e)
        {
            
            FADM_Object.Communal._b_refreshDye = false;

            Thread.Sleep(1000);

            for (int i = 0; i < FADM_Auto.Dye._pd.Count(); i++)
            {
                FADM_Auto.Dye._pd[i]._b_refresh = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HMITCPModBus hMITCPModBus = new HMITCPModBus();
            if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI1;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI1_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI2;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI2_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI3;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI3_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI4;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI4_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI5;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI5_s;
                }
            }

            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI6;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI6_s;
                }
            }
            else
                return;
            
            if(txt_mainTemCorrection1.Text== "" || txt_mainControlCycle1.Text == "" ||  txt_mainP1.Text == "" || txt_mainI1.Text == "" || txt_mainD1.Text == "" || txt_mainLowTem1.Text == "" || txt_mainLowTemAD1.Text == "" || txt_mainHighTem1.Text == "" || txt_mainHighTemAD1.Text == "" || txt_assistantTemCorrection1.Text == "" || txt_assistantControlCycle1.Text == "" || txt_assistantP1.Text == "" || txt_assistantI1.Text == "" || txt_assistantD1.Text == "" || txt_assistantLowTem1.Text == "" || txt_assistantLowTemAD1.Text == "" || txt_assistantHighTem1.Text == "" || txt_assistantHighTemAD1.Text == "" || txt_rev1.Text == "" || txt_zeroVelocity1.Text == "" || txt_decelerationTime1.Text == "" || txt_forwardTime1.Text == "" || txt_pauseTime1.Text == "" || txt_reversalTime1.Text == "" || txt_openTem1.Text == "" || txt_limitTem1.Text == "" || txt_warmUp1.Text == "" || txt_warmDown1.Text == "" || txt_openCoverDrainage1.Text == "" || txt_closeCoverDrainage1.Text == "" || txt_currenAlarmValue1.Text == "" || txt_alarmTime1.Text == "" || txt_fastTem1.Text == "" || txt_fastRate1.Text == "" || txt_washTem1.Text == "")
            {
                FADM_Form.CustomMessageBox.Show("参数不能为空", "温馨提示", MessageBoxButtons.OK, false);
                return;
            }
            int[] ia_value = new int[6];
            //2-7(6个)
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_rev1.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_zeroVelocity1.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_decelerationTime1.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_forwardTime1.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_pauseTime1.Text) * 1);
            ia_value[5] = Convert.ToInt32(Convert.ToDouble(txt_reversalTime1.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x202, ia_value);

            //13-14(2个)
            ia_value = new int[2];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_openTem1.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_limitTem1.Text) * 10);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x20D, ia_value);

            //19-21(3个)
            ia_value = new int[3];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_warmUp1.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_warmDown1.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_openCoverDrainage1.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x213, ia_value);

            //27-32(6个)
            ia_value = new int[6];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_currenAlarmValue1.Text) * 1);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_alarmTime1.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_fastTem1.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_fastRate1.Text) * 10);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_washTem1.Text) * 10);
            ia_value[5] = Convert.ToInt32(Convert.ToDouble(txt_closeCoverDrainage1.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x21B, ia_value);

            //8-12(5个)
            ia_value = new int[5];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_mainTemCorrection1.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_mainControlCycle1.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_mainP1.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_mainI1.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_mainD1.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x208, ia_value);

            //47-50(4个)
            ia_value = new int[4];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_mainLowTem1.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_mainLowTemAD1.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_mainHighTem1.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_mainHighTemAD1.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x22F, ia_value);

            //副杯
            //8-12(5个)
            ia_value = new int[5];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_assistantTemCorrection1.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_assistantControlCycle1.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_assistantP1.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_assistantI1.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_assistantD1.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x308, ia_value);

            //47-50(4个)
            ia_value = new int[4];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_assistantLowTem1.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_assistantLowTemAD1.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_assistantHighTem1.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_assistantHighTemAD1.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x32F, ia_value);

            for (int i = 0; i < FADM_Auto.Dye._pd.Count(); i++)
            {
                FADM_Auto.Dye._pd[i]._b_refresh = false;
            }

            FADM_Object.Communal._b_hasrefreshDye = false;
            _i_count = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HMITCPModBus hMITCPModBus = new HMITCPModBus();
            if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI1;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI1_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI2;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI2_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI3;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI3_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI4;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI4_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI5;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI5_s;
                }
            }

            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI6;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI6_s;
                }
            }
            else
                return;

            if (txt_mainTemCorrection2.Text == "" || txt_mainControlCycle2.Text == "" || txt_mainP2.Text == "" || txt_mainI2.Text == "" || txt_mainD2.Text == "" || txt_mainLowTem2.Text == "" || txt_mainLowTemAD2.Text == "" || txt_mainHighTem2.Text == "" || txt_mainHighTemAD2.Text == "" || txt_assistantTemCorrection2.Text == "" || txt_assistantControlCycle2.Text == "" || txt_assistantP2.Text == "" || txt_assistantI2.Text == "" || txt_assistantD2.Text == "" || txt_assistantLowTem2.Text == "" || txt_assistantLowTemAD2.Text == "" || txt_assistantHighTem2.Text == "" || txt_assistantHighTemAD2.Text == "" || txt_rev2.Text == "" || txt_zeroVelocity2.Text == "" || txt_decelerationTime2.Text == "" || txt_forwardTime2.Text == "" || txt_pauseTime2.Text == "" || txt_reversalTime2.Text == "" || txt_openTem2.Text == "" || txt_limitTem2.Text == "" || txt_warmUp2.Text == "" || txt_warmDown2.Text == "" || txt_openCoverDrainage2.Text == "" || txt_closeCoverDrainage2.Text == "" || txt_currenAlarmValue2.Text == "" || txt_alarmTime2.Text == "" || txt_fastTem2.Text == "" || txt_fastRate2.Text == "" || txt_washTem2.Text == "")
            {
                FADM_Form.CustomMessageBox.Show("参数不能为空", "温馨提示", MessageBoxButtons.OK, false);
                return;
            }
            int[] ia_value = new int[6];
            //2-7(6个)
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_rev2.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_zeroVelocity2.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_decelerationTime2.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_forwardTime2.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_pauseTime2.Text) * 1);
            ia_value[5] = Convert.ToInt32(Convert.ToDouble(txt_reversalTime2.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x242, ia_value);

            //13-14(2个)
            ia_value = new int[2];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_openTem2.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_limitTem2.Text) * 10);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x24D, ia_value);

            //19-21(3个)
            ia_value = new int[3];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_warmUp2.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_warmDown2.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_openCoverDrainage2.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x253, ia_value);

            //27-32(6个)
            ia_value = new int[6];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_currenAlarmValue2.Text) * 1);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_alarmTime2.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_fastTem2.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_fastRate2.Text) * 10);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_washTem2.Text) * 10);
            ia_value[5] = Convert.ToInt32(Convert.ToDouble(txt_closeCoverDrainage2.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x25B, ia_value);

            //8-12(5个)
            ia_value = new int[5];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_mainTemCorrection2.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_mainControlCycle2.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_mainP2.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_mainI2.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_mainD2.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x248, ia_value);

            //47-50(4个)
            ia_value = new int[4];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_mainLowTem2.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_mainLowTemAD2.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_mainHighTem2.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_mainHighTemAD2.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x26F, ia_value);

            //副杯
            //8-12(5个)
            ia_value = new int[5];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_assistantTemCorrection2.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_assistantControlCycle2.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_assistantP2.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_assistantI2.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_assistantD2.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x348, ia_value);

            //47-50(4个)
            ia_value = new int[4];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_assistantLowTem2.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_assistantLowTemAD2.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_assistantHighTem2.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_assistantHighTemAD2.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x36F, ia_value);

            for (int i = 0; i < FADM_Auto.Dye._pd.Count(); i++)
            {
                FADM_Auto.Dye._pd[i]._b_refresh = false;
            }

            FADM_Object.Communal._b_hasrefreshDye = false;
            _i_count = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HMITCPModBus hMITCPModBus = new HMITCPModBus();
            if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI1;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI1_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI2;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI2_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI3;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI3_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI4;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI4_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI5;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI5_s;
                }
            }

            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI6;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI6_s;
                }
            }
            else
                return;

            if (txt_mainTemCorrection3.Text == "" || txt_mainControlCycle3.Text == "" || txt_mainP3.Text == "" || txt_mainI3.Text == "" || txt_mainD3.Text == "" || txt_mainLowTem3.Text == "" || txt_mainLowTemAD3.Text == "" || txt_mainHighTem3.Text == "" || txt_mainHighTemAD3.Text == "" || txt_assistantTemCorrection3.Text == "" || txt_assistantControlCycle3.Text == "" || txt_assistantP3.Text == "" || txt_assistantI3.Text == "" || txt_assistantD3.Text == "" || txt_assistantLowTem3.Text == "" || txt_assistantLowTemAD3.Text == "" || txt_assistantHighTem3.Text == "" || txt_assistantHighTemAD3.Text == "" || txt_rev3.Text == "" || txt_zeroVelocity3.Text == "" || txt_decelerationTime3.Text == "" || txt_forwardTime3.Text == "" || txt_pauseTime3.Text == "" || txt_reversalTime3.Text == "" || txt_openTem3.Text == "" || txt_limitTem3.Text == "" || txt_warmUp3.Text == "" || txt_warmDown3.Text == "" || txt_openCoverDrainage3.Text == "" || txt_closeCoverDrainage3.Text == "" || txt_currenAlarmValue3.Text == "" || txt_alarmTime3.Text == "" || txt_fastTem3.Text == "" || txt_fastRate3.Text == "" || txt_washTem3.Text == "")
            {
                FADM_Form.CustomMessageBox.Show("参数不能为空", "温馨提示", MessageBoxButtons.OK, false);
                return;
            }
            int[] ia_value = new int[6];
            //2-7(6个)
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_rev3.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_zeroVelocity3.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_decelerationTime3.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_forwardTime3.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_pauseTime3.Text) * 1);
            ia_value[5] = Convert.ToInt32(Convert.ToDouble(txt_reversalTime3.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x282, ia_value);

            //13-14(2个)
            ia_value = new int[2];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_openTem3.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_limitTem3.Text) * 10);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x28D, ia_value);

            //19-21(3个)
            ia_value = new int[3];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_warmUp3.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_warmDown3.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_openCoverDrainage3.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x293, ia_value);

            //27-32(6个)
            ia_value = new int[6];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_currenAlarmValue3.Text) * 1);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_alarmTime3.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_fastTem3.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_fastRate3.Text) * 10);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_washTem3.Text) * 10);
            ia_value[5] = Convert.ToInt32(Convert.ToDouble(txt_closeCoverDrainage3.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x29B, ia_value);

            //8-12(5个)
            ia_value = new int[5];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_mainTemCorrection3.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_mainControlCycle3.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_mainP3.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_mainI3.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_mainD3.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x288, ia_value);

            //47-50(4个)
            ia_value = new int[4];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_mainLowTem3.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_mainLowTemAD3.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_mainHighTem3.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_mainHighTemAD3.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x2AF, ia_value);

            //副杯
            //8-12(5个)
            ia_value = new int[5];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_assistantTemCorrection3.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_assistantControlCycle3.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_assistantP3.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_assistantI3.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_assistantD3.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x388, ia_value);

            //47-50(4个)
            ia_value = new int[4];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_assistantLowTem3.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_assistantLowTemAD3.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_assistantHighTem3.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_assistantHighTemAD3.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x3AF, ia_value);

            for (int i = 0; i < FADM_Auto.Dye._pd.Count(); i++)
            {
                FADM_Auto.Dye._pd[i]._b_refresh = false;
            }

            FADM_Object.Communal._b_hasrefreshDye = false;
            _i_count = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HMITCPModBus hMITCPModBus = new HMITCPModBus();
            if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area1_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI1;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI1_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area2_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI2;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI2_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area3_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI3;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI3_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area4_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI4;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI4_s;
                }
            }
            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area5_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI5;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI5_s;
                }
            }

            else if (Convert.ToInt32(lab_main1.Text) >= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()) && Convert.ToInt32(lab_main1.Text) <= Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMax.ToString()))
            {
                if (Convert.ToInt32(lab_main1.Text) == Convert.ToInt32(Lib_Card.Configure.Parameter.Machine_Area6_CupMin.ToString()))
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI6;
                }
                else
                {
                    hMITCPModBus = FADM_Object.Communal._tcpDyeHMI6_s;
                }
            }
            else
                return;

            if (txt_mainTemCorrection4.Text == "" || txt_mainControlCycle4.Text == "" || txt_mainP4.Text == "" || txt_mainI4.Text == "" || txt_mainD4.Text == "" || txt_mainLowTem4.Text == "" || txt_mainLowTemAD4.Text == "" || txt_mainHighTem4.Text == "" || txt_mainHighTemAD4.Text == "" || txt_assistantTemCorrection4.Text == "" || txt_assistantControlCycle4.Text == "" || txt_assistantP4.Text == "" || txt_assistantI4.Text == "" || txt_assistantD4.Text == "" || txt_assistantLowTem4.Text == "" || txt_assistantLowTemAD4.Text == "" || txt_assistantHighTem4.Text == "" || txt_assistantHighTemAD4.Text == "" || txt_rev4.Text == "" || txt_zeroVelocity4.Text == "" || txt_decelerationTime4.Text == "" || txt_forwardTime4.Text == "" || txt_pauseTime4.Text == "" || txt_reversalTime4.Text == "" || txt_openTem4.Text == "" || txt_limitTem4.Text == "" || txt_warmUp4.Text == "" || txt_warmDown4.Text == "" || txt_openCoverDrainage4.Text == "" || txt_closeCoverDrainage4.Text == "" || txt_currenAlarmValue4.Text == "" || txt_alarmTime4.Text == "" || txt_fastTem4.Text == "" || txt_fastRate4.Text == "" || txt_washTem4.Text == "")
            {
                FADM_Form.CustomMessageBox.Show("参数不能为空", "温馨提示", MessageBoxButtons.OK, false);
                return;
            }
            int[] ia_value = new int[6];
            //2-7(6个)
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_rev4.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_zeroVelocity4.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_decelerationTime4.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_forwardTime4.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_pauseTime4.Text) * 1);
            ia_value[5] = Convert.ToInt32(Convert.ToDouble(txt_reversalTime4.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x2C2, ia_value);

            //13-14(2个)
            ia_value = new int[2];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_openTem4.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_limitTem4.Text) * 10);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x2CD, ia_value);

            //19-21(3个)
            ia_value = new int[3];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_warmUp4.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_warmDown4.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_openCoverDrainage4.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x2D3, ia_value);

            //27-32(6个)
            ia_value = new int[6];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_currenAlarmValue4.Text) * 1);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_alarmTime4.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_fastTem4.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_fastRate4.Text) * 10);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_washTem4.Text) * 10);
            ia_value[5] = Convert.ToInt32(Convert.ToDouble(txt_closeCoverDrainage4.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x2DB, ia_value);

            //8-12(5个)
            ia_value = new int[5];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_mainTemCorrection4.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_mainControlCycle4.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_mainP4.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_mainI4.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_mainD4.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x2C8, ia_value);

            //47-50(4个)
            ia_value = new int[4];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_mainLowTem4.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_mainLowTemAD4.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_mainHighTem4.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_mainHighTemAD4.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x2EF, ia_value);

            //副杯
            //8-12(5个)
            ia_value = new int[5];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_assistantTemCorrection4.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_assistantControlCycle4.Text) * 10);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_assistantP4.Text) * 1);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_assistantI4.Text) * 1);
            ia_value[4] = Convert.ToInt32(Convert.ToDouble(txt_assistantD4.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x3C8, ia_value);

            //47-50(4个)
            ia_value = new int[4];
            ia_value[0] = Convert.ToInt32(Convert.ToDouble(txt_assistantLowTem4.Text) * 10);
            ia_value[1] = Convert.ToInt32(Convert.ToDouble(txt_assistantLowTemAD4.Text) * 1);
            ia_value[2] = Convert.ToInt32(Convert.ToDouble(txt_assistantHighTem4.Text) * 10);
            ia_value[3] = Convert.ToInt32(Convert.ToDouble(txt_assistantHighTemAD4.Text) * 1);
            if (!hMITCPModBus._b_Connect)
            {
                hMITCPModBus.ReConnect();
            }
            hMITCPModBus.Write(0x3EF, ia_value);

            for (int i = 0; i < FADM_Auto.Dye._pd.Count(); i++)
            {
                FADM_Auto.Dye._pd[i]._b_refresh = false;
            }

            FADM_Object.Communal._b_hasrefreshDye = false;
            _i_count = 0;
        }
    }
}
