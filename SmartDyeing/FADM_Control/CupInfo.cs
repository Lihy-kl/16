using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class CupInfo : UserControl
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        //记录当前杯号
        string _s_cupNum = "";
        public CupInfo()
        {
            InitializeComponent();
        }

        public void Update(CupData cupData)
        {
            try
            {
                lab_CupNum.Text = cupData._s_cupNum + "：";
                _s_cupNum = cupData._s_cupNum;
                txt_FoumulaCode.Text = cupData._s_formulaCode;
                txt_DyeCode.Text = cupData._s_dyeingCode;
                if (cupData._s_startTime != "")
                {
                    //计算运行时间
                    DateTime dateTime1 = Convert.ToDateTime(cupData._s_startTime);
                    DateTime dateTime2 = DateTime.Now;
                    TimeSpan ts = dateTime2 - dateTime1;
                    string s_temp;
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        s_temp = Convert.ToInt32(ts.TotalSeconds) / 60 / 60 + "时" +
                        Convert.ToInt32(ts.TotalSeconds) % (60 * 60) / 60 + "分" +
                        Convert.ToInt32(ts.TotalSeconds) % (60 * 60) % 60 + "秒";
                    }
                    else
                    {
                        s_temp = Convert.ToInt32(ts.TotalSeconds) / 60 / 60 + "H" +
                        Convert.ToInt32(ts.TotalSeconds) % (60 * 60) / 60 + "M" +
                        Convert.ToInt32(ts.TotalSeconds) % (60 * 60) % 60 + "S";
                    }
                    txt_TotalTime.Text = s_temp;
                }

                if (cupData._s_stepStartTime != "")
                {
                    //计算运行时间
                    DateTime dateTime1 = Convert.ToDateTime(cupData._s_stepStartTime);
                    DateTime dateTime2 = DateTime.Now;
                    TimeSpan ts = dateTime2 - dateTime1;
                    string s_temp;
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        s_temp = Convert.ToInt32(ts.TotalSeconds) / 60 / 60 + "时" +
                        Convert.ToInt32(ts.TotalSeconds) % (60 * 60) / 60 + "分" +
                        Convert.ToInt32(ts.TotalSeconds) % (60 * 60) % 60 + "秒";
                    }
                    else
                    {
                        s_temp = Convert.ToInt32(ts.TotalSeconds) / 60 / 60 + "H" +
                        Convert.ToInt32(ts.TotalSeconds) % (60 * 60) / 60 + "M" +
                        Convert.ToInt32(ts.TotalSeconds) % (60 * 60) % 60 + "S";
                    }
                    txt_CurrentStepTime.Text = s_temp;
                }

                txt_RealTemp.Text = cupData._s_realTemp;
                txt_SetTemp.Text = cupData._s_setTemp;
                txt_Weight.Text = cupData._s_totalWeight;
                txt_StepTotal.Text = cupData._s_totalStep;
                txt_StepNum.Text = cupData._s_stepNum;
                txt_Statues.Text = cupData._s_state;
                txt_TechnologyName.Text = cupData._s_technologyName;
                txt_SetTime.Text = cupData._s_setTime;


                if (cupData._s_state == "下线")
                {
                    lab_OffLine.Visible = true;

                    //把显示值全部清空
                    txt_FoumulaCode.Text = "";
                    txt_DyeCode.Text = "";
                    txt_TotalTime.Text = "";
                    txt_CurrentStepTime.Text = "";
                    txt_RealTemp.Text = "";
                    txt_Weight.Text = "";
                    txt_StepTotal.Text = "";
                    txt_StepNum.Text = "";
                    txt_TechnologyName.Text = "";
                    txt_SetTemp.Text = "";
                    txt_SetTime.Text = "";
                }
                else if (cupData._s_state == "待机")
                {
                    lab_OffLine.Visible = false;

                    ////把显示值全部清空
                    txt_FoumulaCode.Text = "";
                    txt_DyeCode.Text = "";
                    txt_TotalTime.Text = "";
                    txt_CurrentStepTime.Text = "";
                    //txt_RealTemp.Text = "";
                    txt_Weight.Text = "";
                    txt_StepTotal.Text = "";
                    txt_StepNum.Text = "";
                    txt_TechnologyName.Text = "";
                    txt_SetTemp.Text = "";
                    txt_SetTime.Text = "";
                }
                else
                {
                    lab_OffLine.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Lib_Log.Log.writeLogException("配液杯状态Update：" + ex.ToString());
            }
        }

        private void btn_Details_Click(object sender, EventArgs e)
        {
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "配液杯详细资料");
            else
                ptr = FindWindow(null, "CupDetails");

            if (ptr == IntPtr.Zero)
            {
                if (!string.IsNullOrEmpty(txt_Statues.Text))
                {
                    if (txt_Statues.Text != "下线" && txt_Statues.Text != "待机")
                        new FADM_Form.CupDetails(Convert.ToInt16(_s_cupNum)).Show();
                }
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            DataTable dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(
              "SELECT * FROM drop_head WHERE CupNum = " + _s_cupNum + ";");
            if (dt_drop_head.Rows.Count > 0)
            {
                dt_drop_head = FADM_Object.Communal._fadmSqlserver.GetData(
                    "SELECT * FROM cup_details WHERE CupNum = " + _s_cupNum + ";");
                string s_statues = Convert.ToString(dt_drop_head.Rows[0]["Statues"]);
                if ("下线" != s_statues)
                {
                    if (("滴液" == s_statues && FADM_Object.Communal.ReadMachineStatus() != 7) || "滴液" != s_statues)
                    {
                        DialogResult dialogResult =  FADM_Form.CustomMessageBox.Show(_s_cupNum + "号配液杯确定停止吗?(确认停止请点是，取消停止请点否)", "温馨提示", MessageBoxButtons.YesNo, true);
                        if (dialogResult == DialogResult.Yes)
                            FADM_Object.Communal._lis_dripStopCup.Add(Convert.ToInt16(_s_cupNum));
                    }

                }
            }
        }

        private void tsm_AllInLine_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(_s_cupNum) <= 10)
            {
                string s_sql = "update cup_details set Enable=1,Statues='待机' where CupNum in (1,2,3,4,5,6,7,8,9,10) and Statues ='下线'; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                FADM_Object.Communal._ia_dyeStatus[0] = 1;
            }
            else if (Convert.ToInt32(_s_cupNum) <= 20)
            {
                string s_sql = "update cup_details set Enable=1,Statues='待机' where CupNum in (11,12,13,14,15,16,17,18,19,20) and Statues ='下线'; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                FADM_Object.Communal._ia_dyeStatus[1] = 1;
            }
            else
            {
                string s_sql = "update cup_details set Enable=1,Statues='待机' where CupNum in (21,22,23,24,25,26,27,28,29,30) and Statues ='下线'; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                FADM_Object.Communal._ia_dyeStatus[2] = 1;
            }
        }

        private void btn_OffLine_Click(object sender, EventArgs e)
        {

        }

        private void tsm_AllOffLine_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(_s_cupNum) <= 10)
            {
                string s_sql = "update cup_details set Enable=0,Statues='下线' where CupNum in (1,2,3,4,5,6,7,8,9,10) and Statues ='待机'; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                    "SELECT * FROM cup_details WHERE CupNum in (1,2,3,4,5,6,7,8,9,10);");
                foreach (DataRow row in dt_temp.Rows)
                {
                    int i_enable = Convert.ToInt16(row["Enable"]);
                    if (1 == i_enable)
                    {
                        return;
                    }
                }
                FADM_Object.Communal._ia_dyeStatus[0] = 0;

            }
            else if (Convert.ToInt32(_s_cupNum) <= 20)
            {
                string s_sql = "update cup_details set Enable=0,Statues='下线' where CupNum in (11,12,13,14,15,16,17,18,19,20) and Statues ='待机'; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                  "SELECT * FROM cup_details WHERE CupNum in (11,12,13,14,15,16,17,18,19,20);");
                foreach (DataRow row in dt_temp.Rows)
                {
                    int i_enable = Convert.ToInt16(row["Enable"]);
                    if (1 == i_enable)
                    {
                        return;
                    }
                }
                FADM_Object.Communal._ia_dyeStatus[1] = 0;
            }
            else
            {
                string s_sql = "update cup_details set Enable=0,Statues='下线' where CupNum in (21,22,23,24,25,26,27,28,29,30) and Statues ='待机'; ";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(
                  "SELECT * FROM cup_details WHERE CupNum in (21,22,23,24,25,26,27,28,29,30);");
                foreach (DataRow row in dt_temp.Rows)
                {
                    int i_enable = Convert.ToInt16(row["Enable"]);
                    if (1 == i_enable)
                    {
                        return;
                    }
                }
                FADM_Object.Communal._ia_dyeStatus[2] = 0;
            }
        }
    }

    public class CupData
    {
        //杯号
        public string _s_cupNum;
        //状态
        public string _s_state;
        //配方号
        public string _s_formulaCode;
        //固然色编号
        public string _s_dyeingCode;
        //持续时间
        public string _s_startTime;
        //实际温度
        public string _s_realTemp;
        //设定温度
        public string _s_setTemp;
        //当前液量
        public string _s_totalWeight;
        //当前步号
        public string _s_stepNum;
        //总步号
        public string _s_totalStep;
        //当前步名称
        public string _s_technologyName;
        //当前步运行时间
        public string _s_stepStartTime;

        //设定时间
        public string _s_setTime;
    }
}
