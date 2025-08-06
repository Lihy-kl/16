using Configure.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configure
{
    public partial class Configure : Form
    {
        public Configure()
        {
            InitializeComponent();

            //获取参数
            try
            {
                Parameter parameter = new Parameter();
                string sPath1 = Environment.CurrentDirectory + "\\Config\\parameter.ini";
                foreach (PropertyInfo info in parameter.GetType().GetProperties())
                {
                    try
                    {
                        char[] separator = { '_' };
                        string head = info.Name.Split(separator)[0];
                        Console.WriteLine(info.Name);

                        Control control = this.Controls.Find(info.Name, true).FirstOrDefault();
                        if (control != null && control is TextBox)
                        {
                            ((TextBox)control).Text = Lib_File.Ini.GetIni(head, info.Name, sPath1);
                        }


                        if (info.Name.Equals("CylinderVersion"))
                        {

                        }
                        if (info.PropertyType == typeof(int))
                        {

                            int value = Convert.ToInt32(Lib_File.Ini.GetIni(head, info.Name, sPath1));
                            parameter.GetType().GetProperty(info.Name).SetValue(parameter, value);


                        }
                        else if (info.PropertyType == typeof(double))
                        {
                            double value = Convert.ToDouble(Lib_File.Ini.GetIni(head, info.Name, sPath1));
                            parameter.GetType().GetProperty(info.Name).SetValue(parameter, value);
                        }
                    }
                    catch
                    { }
                }
            }
            catch (Exception ex)
            {
                //FADM_Form.CustomMessageBox.Show(ex.Message, "parameter", MessageBoxButtons.OK, false);
                //System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();
            }

            //获取IO_Mapping
            try
            {
                IOMapping IO = new IOMapping();
                string s_path1 = Environment.CurrentDirectory + "\\Config\\IO_Mapping.ini";

                foreach (PropertyInfo info in IO.GetType().GetProperties())
                {
                    char[] ca_separator = { '_' };
                    string s_head = info.Name.Split(ca_separator)[0];
                    try
                    {
                        int i_value = Convert.ToInt32(Lib_File.Ini.GetIni(s_head, info.Name, s_path1));
                        Control control = this.Controls.Find(info.Name, true).FirstOrDefault();
                        if (control != null && control is TextBox)
                        {
                            ((TextBox)control).Text = i_value.ToString();
                        }
                        IO.GetType().GetProperty(info.Name).SetValue(IO, i_value);
                    }
                    catch (Exception ex)
                    {
                        //if (ex.Message != "Input string was not in a correct format." && ex.Message != "输入字符串的格式不正确。")
                        //    throw;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            radioButton1.Checked = true;

            string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
            string s_out = Lib_File.Ini.GetIni("Setting", "IsOutDrip", "0", s_path);
            Setting_IsOutDrip.Text = s_out;


            string s_low = Lib_File.Ini.GetIni("Setting", "IsLowDrip", "0", s_path);
            Setting_IsLowDrip.Text = s_low;

            string s_full = Lib_File.Ini.GetIni("Setting", "IsFullDrip", "1", s_path);
            Setting_IsFullDrip.Text = s_full;

            string s_outAllow = Lib_File.Ini.GetIni("Setting", "IsOutDripAllow", "1", s_path);
            Setting_IsOutDripAllow.Text = s_outAllow;

            string s_lowAllow = Lib_File.Ini.GetIni("Setting", "IsLowDripAllow", "1", s_path);
            Setting_IsLowDripAllow.Text = s_lowAllow;


            string s_registerOld = Lib_File.Ini.GetIni("Setting", "registerOld", "0", s_path);
            Setting_registerOld.Text = s_registerOld;

            string s_isNetWork = Lib_File.Ini.GetIni("Setting", "IsNetWork", "0", s_path);
            Setting_IsNetWork.Text = s_isNetWork;

            string IsUnitChange = Lib_File.Ini.GetIni("Setting", "IsUnitChange", "0", s_path);
            Setting_IsUnitChange.Text = IsUnitChange;

            string s_dripAll = Lib_File.Ini.GetIni("Setting", "IsDripAll", "0", s_path);
            Setting_IsDripAll.Text = s_dripAll;

            string s_assitantFirst = Lib_File.Ini.GetIni("Setting", "IsAssitantFirst", "0", s_path);
            Setting_IsAssitantFirst.Text = s_assitantFirst;

            string s_needCheck = Lib_File.Ini.GetIni("Setting", "IsNeedCheck", "1", s_path);
            Setting_IsNeedCheck.Text = s_needCheck;

            string s_autoAbs = Lib_File.Ini.GetIni("Setting", "IsAutoAbs", "1", s_path);
            Setting_IsAutoAbs.Text = s_autoAbs;

            string s_useWater = Lib_File.Ini.GetIni("Setting", "IsUseWaterTestBase", "0", s_path);
            Setting_IsUseWaterTestBase.Text = s_useWater;

            string s_finishSend = Lib_File.Ini.GetIni("Setting", "IsFinishSend", "1", s_path);
            Setting_IsFinishSend.Text = s_finishSend;

            string s_dripReserveFirst = Lib_File.Ini.GetIni("Setting", "IsDripReserveFirst", "1", s_path);
            Setting_IsDripReserveFirst.Text = s_dripReserveFirst;

            string s_debug = Lib_File.Ini.GetIni("Setting", "IsDebug", "0", s_path);
            Setting_IsDebug.Text = s_debug;

            string s_saveRealConcentration = Lib_File.Ini.GetIni("Setting", "IsSaveRealConcentration", "0", s_path);
            Setting_IsSaveRealConcentration.Text = s_saveRealConcentration;

            string s_useClamp = Lib_File.Ini.GetIni("Setting", "IsUseClamp", "0", s_path);
            Setting_IsUseClamp.Text = s_useClamp;

            string s_useClampOut = Lib_File.Ini.GetIni("Setting", "IsUseClampOut", "0", s_path);
            Setting_IsUseClampOut.Text = s_useClampOut;

            string IsDyMin = Lib_File.Ini.GetIni("Setting", "IsDyMin", "0", s_path);
            Setting_IsDyMin.Text = IsDyMin;

            string _b_DyeCupNum = Lib_File.Ini.GetIni("Setting", "IsDyeCupNum", "0", s_path);
            Setting_IsDyeCupNum.Text = _b_DyeCupNum;


            string s_desc = Lib_File.Ini.GetIni("Setting", "DESC", "0", s_path);
            Setting_DESC.Text = s_desc;

            string s_isShowSample = Lib_File.Ini.GetIni("Setting", "IsShowSample", "0", s_path);
            Setting_IsShowSample.Text = s_isShowSample;

            string s_Head_Ip = Lib_File.Ini.GetIni("CplImport", "Head_Ip", "1", s_path);
            CplImport_Head_Ip.Text = s_Head_Ip;

            string s_Head_Ip_Len = Lib_File.Ini.GetIni("CplImport", "Head_Ip_Len", "4", s_path);
            CplImport_Head_Ip_Len.Text = s_Head_Ip_Len;

            string s_Head_FormulaCode = Lib_File.Ini.GetIni("CplImport", "Head_FormulaCode", "5", s_path);
            CplImport_Head_FormulaCode.Text = s_Head_FormulaCode;

            string s_Head_FormulaCode_Len = Lib_File.Ini.GetIni("CplImport", "Head_FormulaCode_Len", "12", s_path);
            CplImport_Head_FormulaCode_Len.Text = s_Head_FormulaCode_Len;

            string s_Head_VersionNum = Lib_File.Ini.GetIni("CplImport", "Head_VersionNum", "17", s_path);
            CplImport_Head_VersionNum.Text = s_Head_VersionNum;

            string s_Head_VersionNum_Len = Lib_File.Ini.GetIni("CplImport", "Head_VersionNum_Len", "2", s_path);
            CplImport_Head_VersionNum_Len.Text = s_Head_VersionNum_Len;

            string s_Head_Count = Lib_File.Ini.GetIni("CplImport", "Head_Count", "19", s_path);
            CplImport_Head_Count.Text = s_Head_Count;

            string s_Head_Count_Len = Lib_File.Ini.GetIni("CplImport", "Head_Count_Len", "2", s_path);
            CplImport_Head_Count_Len.Text = s_Head_Count_Len;

            string s_Head_Unit = Lib_File.Ini.GetIni("CplImport", "Head_Unit", "21", s_path);
            CplImport_Head_Unit.Text = s_Head_Unit;

            string s_Head_Unit_Len = Lib_File.Ini.GetIni("CplImport", "Head_Unit_Len", "1", s_path);
            CplImport_Head_Unit_Len.Text = s_Head_Unit_Len;

            string s_Head_Index = Lib_File.Ini.GetIni("CplImport", "Head_Index", "22", s_path);
            CplImport_Head_Index.Text = s_Head_Index;

            string s_Head_Index_Len = Lib_File.Ini.GetIni("CplImport", "Head_Index_Len", "3", s_path);
            CplImport_Head_Index_Len.Text = s_Head_Index_Len;

            string s_Head_FormulaName = Lib_File.Ini.GetIni("CplImport", "Head_FormulaName", "25", s_path);
            CplImport_Head_FormulaName.Text = s_Head_FormulaName;

            string s_Head_FormulaName_Len = Lib_File.Ini.GetIni("CplImport", "Head_FormulaName_Len", "24", s_path);
            CplImport_Head_FormulaName_Len.Text = s_Head_FormulaName_Len;

            string s_Head_ClothWeight = Lib_File.Ini.GetIni("CplImport", "Head_ClothWeight", "49", s_path);
            CplImport_Head_ClothWeight.Text = s_Head_ClothWeight;

            string s_Head_ClothWeight_Len = Lib_File.Ini.GetIni("CplImport", "Head_ClothWeight_Len", "8", s_path);
            CplImport_Head_ClothWeight_Len.Text = s_Head_ClothWeight_Len;

            string s_Head_TotalWeight = Lib_File.Ini.GetIni("CplImport", "Head_TotalWeight", "57", s_path);
            CplImport_Head_TotalWeight.Text = s_Head_TotalWeight;

            string s_Head_TotalWeight_Len = Lib_File.Ini.GetIni("CplImport", "Head_TotalWeight_Len", "8", s_path);
            CplImport_Head_TotalWeight_Len.Text = s_Head_TotalWeight_Len;

            string s_Head_AddWater = Lib_File.Ini.GetIni("CplImport", "Head_AddWater", "65", s_path);
            CplImport_Head_AddWater.Text = s_Head_AddWater;

            string s_Head_AddWater_Len = Lib_File.Ini.GetIni("CplImport", "Head_AddWater_Len", "1", s_path);
            CplImport_Head_AddWater_Len.Text = s_Head_AddWater_Len;

            string s_Head_ConAdd = Lib_File.Ini.GetIni("CplImport", "Head_ConAdd", "66", s_path);
            CplImport_Head_ConAdd.Text = s_Head_ConAdd;

            string s_Head_ConAdd_Len = Lib_File.Ini.GetIni("CplImport", "Head_ConAdd_Len", "1", s_path);
            CplImport_Head_ConAdd_Len.Text = s_Head_ConAdd_Len;

            string s_Head_MNum = Lib_File.Ini.GetIni("CplImport", "Head_MNum", "67", s_path);
            CplImport_Head_MNum.Text = s_Head_MNum;

            string s_Head_MNum_Len = Lib_File.Ini.GetIni("CplImport", "Head_MNum_Len", "2", s_path);
            CplImport_Head_MNum_Len.Text = s_Head_MNum_Len;

            string s_Head_Date = Lib_File.Ini.GetIni("CplImport", "Head_Date", "69", s_path);
            CplImport_Head_Date.Text = s_Head_Date;

            string s_Head_Date_Len = Lib_File.Ini.GetIni("CplImport", "Head_Date_Len", "8", s_path);
            CplImport_Head_Date_Len.Text = s_Head_Date_Len;


            string s_Head_Code = Lib_File.Ini.GetIni("CplImport", "Head_Code", "77", s_path);
            CplImport_Head_Code.Text = s_Head_Code;

            string s_Head_Code_Len = Lib_File.Ini.GetIni("CplImport", "Head_Code_Len", "8", s_path);
            CplImport_Head_Code_Len.Text = s_Head_Code_Len;

            string s_Head_Type = Lib_File.Ini.GetIni("CplImport", "Head_Type", "85", s_path);
            CplImport_Head_Type.Text = s_Head_Type;

            string s_Head_Type_Len = Lib_File.Ini.GetIni("CplImport", "Head_Type_Len", "1", s_path);
            CplImport_Head_Type_Len.Text = s_Head_Type_Len;

            string s_Head_Drip = Lib_File.Ini.GetIni("CplImport", "Head_Drip", "86", s_path);
            CplImport_Head_Drip.Text = s_Head_Drip;

            string s_Head_Drip_Len = Lib_File.Ini.GetIni("CplImport", "Head_Drip_Len", "1", s_path);
            CplImport_Head_Drip_Len.Text = s_Head_Drip_Len;

            string s_Head_SIN = Lib_File.Ini.GetIni("CplImport", "Head_SIN", "87", s_path);
            CplImport_Head_SIN.Text = s_Head_SIN;

            string s_Head_SIN_Len = Lib_File.Ini.GetIni("CplImport", "Head_SIN_Len", "2", s_path);
            CplImport_Head_SIN_Len.Text = s_Head_SIN_Len;

            string s_Detail_Ip = Lib_File.Ini.GetIni("CplImport", "Detail_Ip", "1", s_path);
            CplImport_Detail_Ip.Text = s_Detail_Ip;

            string s_Detail_Ip_Len = Lib_File.Ini.GetIni("CplImport", "Detail_Ip_Len", "4", s_path);
            CplImport_Detail_Ip_Len.Text = s_Detail_Ip_Len;

            string s_Detail_FormulaCode = Lib_File.Ini.GetIni("CplImport", "Detail_FormulaCode", "5", s_path);
            CplImport_Detail_FormulaCode.Text = s_Detail_FormulaCode;

            string s_Detail_FormulaCode_Len = Lib_File.Ini.GetIni("CplImport", "Detail_FormulaCode_Len", "12", s_path);
            CplImport_Detail_FormulaCode_Len.Text = s_Detail_FormulaCode_Len;

            string s_Detail_VersionNum = Lib_File.Ini.GetIni("CplImport", "Detail_VersionNum", "17", s_path);
            CplImport_Detail_VersionNum.Text = s_Detail_VersionNum;

            string s_Detail_VersionNum_Len = Lib_File.Ini.GetIni("CplImport", "Detail_VersionNum_Len", "2", s_path);
            CplImport_Detail_VersionNum_Len.Text = s_Detail_VersionNum_Len;

            string s_Detail_Index = Lib_File.Ini.GetIni("CplImport", "Detail_Index", "19", s_path);
            CplImport_Detail_Index.Text = s_Detail_Index;

            string s_Detail_Index_Len = Lib_File.Ini.GetIni("CplImport", "Detail_Index_Len", "2", s_path);
            CplImport_Detail_Index_Len.Text = s_Detail_Index_Len;

            string s_Detail_Unit = Lib_File.Ini.GetIni("CplImport", "Detail_Unit", "21", s_path);
            CplImport_Detail_Unit.Text = s_Detail_Unit;

            string s_Detail_Unit_Len = Lib_File.Ini.GetIni("CplImport", "Detail_Unit_Len", "1", s_path);
            CplImport_Detail_Unit_Len.Text = s_Detail_Unit_Len;

            string s_Detail_Num = Lib_File.Ini.GetIni("CplImport", "Detail_Num", "22", s_path);
            CplImport_Detail_Num.Text = s_Detail_Num;

            string s_Detail_Num_Len = Lib_File.Ini.GetIni("CplImport", "Detail_Num_Len", "3", s_path);
            CplImport_Detail_Num_Len.Text = s_Detail_Num_Len;

            string s_Detail_AssistantCode = Lib_File.Ini.GetIni("CplImport", "Detail_AssistantCode", "25", s_path);
            CplImport_Detail_AssistantCode.Text = s_Detail_AssistantCode;

            string s_Detail_AssistantCode_Len = Lib_File.Ini.GetIni("CplImport", "Detail_AssistantCode_Len", "8", s_path);
            CplImport_Detail_AssistantCode_Len.Text = s_Detail_AssistantCode_Len;

            string s_Detail_RealConcentration = Lib_File.Ini.GetIni("CplImport", "Detail_RealConcentration", "33", s_path);
            CplImport_Detail_RealConcentration.Text = s_Detail_RealConcentration;

            string s_Detail_RealConcentration_Len = Lib_File.Ini.GetIni("CplImport", "Detail_RealConcentration_Len", "9", s_path);
            CplImport_Detail_RealConcentration_Len.Text = s_Detail_RealConcentration_Len;

            string s_Detail_SIN = Lib_File.Ini.GetIni("CplImport", "Detail_SIN", "42", s_path);
            CplImport_Detail_SIN.Text = s_Detail_SIN;

            string s_Detail_SIN_Len = Lib_File.Ini.GetIni("CplImport", "Detail_SIN_Len", "2", s_path);
            CplImport_Detail_SIN_Len.Text = s_Detail_SIN_Len;

            string s_isAddWaterFirst = Lib_File.Ini.GetIni("Setting", "IsAddWaterFirst", "1", s_path);
            Setting_IsAddWaterFirst.Text = s_isAddWaterFirst;

            string s_isSort = Lib_File.Ini.GetIni("Setting", "IsSort", "1", s_path);
            Setting_IsSort.Text = s_isSort;

            string s_isDripNeedCupNum = Lib_File.Ini.GetIni("Setting", "IsDripNeedCupNum", "0", s_path);
            Setting_IsDripNeedCupNum.Text = s_isDripNeedCupNum;

            string s_isUseCloth = Lib_File.Ini.GetIni("Setting", "IsUseCloth", "0", s_path);
            Setting_IsUseCloth.Text = s_isUseCloth;

            string s_isNeedConfirm = Lib_File.Ini.GetIni("Setting", "IsNeedConfirm", "0", s_path);
            Setting_IsNeedConfirm.Text = s_isNeedConfirm;

            string IsBathRatioTxtDyBath = Lib_File.Ini.GetIni("Setting", "IsBathRatioTxtDyBath", "0", s_path);
            Setting_IsBathRatioTxtDyBath.Text = IsBathRatioTxtDyBath;

            string s_isUseBrewOnly = Lib_File.Ini.GetIni("Setting", "IsUseBrewOnly", "0", s_path);
            Setting_IsUseBrewOnly.Text = s_isUseBrewOnly;

            string s_isStableRead = Lib_File.Ini.GetIni("Setting", "IsStableRead", "0", s_path);
            Setting_IsStableRead.Text = s_isStableRead;

            string s_isHighWash = Lib_File.Ini.GetIni("Setting", "IsHighWash", "0", s_path);
            Setting_IsHighWash.Text = s_isHighWash;

            string s_isUsePowerAB = Lib_File.Ini.GetIni("Setting", "PowerAB", "0", s_path);
            Setting_PowerAB.Text = s_isUsePowerAB;

            string s_isShowBottleStatus = Lib_File.Ini.GetIni("Setting", "IsShowBottleStatus", "0", s_path);
            Setting_IsShowBottleStatus.Text = s_isShowBottleStatus;

            string s_isDripPriority = Lib_File.Ini.GetIni("Setting", "IsDripPriority", "1", s_path);
            Setting_IsDripPriority.Text = s_isDripPriority;

            //string s_isAloneDripReserve = Lib_File.Ini.GetIni("Setting", "IsAloneDripReserve", "0", s_path);
            //if (s_isAloneDripReserve == "1")
            //{
            //    FADM_Object.Communal._b_isAloneDripReserve = true;
            //}

            string s_isSelf = Lib_File.Ini.GetIni("Setting", "SelfData", "1,0.5,2,5,11,2", s_path);
            Setting_SelfData.Text = s_isSelf;



            string s_isDripAddBatch = Lib_File.Ini.GetIni("Setting", "IsDripAddBatch", "0", s_path);
            Setting_IsDripAddBatch.Text = s_isDripAddBatch;

            string s_isUseNewCorrectingWater = Lib_File.Ini.GetIni("Setting", "IsUseNewCorrectingWater", "0", s_path);
            Setting_IsUseNewCorrectingWater.Text = s_isUseNewCorrectingWater;

            string s_isHasWashSyringe = Lib_File.Ini.GetIni("Setting", "IsHasWashSyringe", "0", s_path);
            Setting_IsHasWashSyringe.Text = s_isHasWashSyringe;

            string s_isWaterRecheck = Lib_File.Ini.GetIni("Setting", "IsWaterRecheck", "1", s_path);
            Setting_IsWaterRecheck.Text = s_isWaterRecheck;

            string s_isAssShowWater = Lib_File.Ini.GetIni("Setting", "IsAssShowWater", "0", s_path);
            Setting_IsAssShowWater.Text = s_isAssShowWater;

            string s_balanceInDrip = Lib_File.Ini.GetIni("Setting", "IsBalanceInDrip", "1", s_path);
            Setting_IsBalanceInDrip.Text = s_balanceInDrip;

            string s_isCupAreaOnly = Lib_File.Ini.GetIni("Setting", "IsCupAreaOnly", "0", s_path);
            Setting_IsCupAreaOnly.Text = s_isCupAreaOnly;

            string s_newSet = Lib_File.Ini.GetIni("Setting", "IsNewSet", "1", s_path);
            Setting_IsNewSet.Text = s_newSet;

            string isUseAuto = Lib_File.Ini.GetIni("Setting", "IsUseAuto", "1", s_path);
            Setting_IsUseAuto.Text = isUseAuto;

            string s_isUseABAssistant = Lib_File.Ini.GetIni("Setting", "IsUseABAssistant", "0", s_path);
            Setting_IsUseABAssistant.Text = s_isUseABAssistant;

            string s_NeedToDoTime = Lib_File.Ini.GetIni("Setting", "NeedToDoTime", "60", s_path);
            Setting_NeedToDoTime.Text = s_NeedToDoTime;
            string s_NeedToDo = Lib_File.Ini.GetIni("Setting", "NeedToDo", "0", s_path);
            Setting_NeedToDo.Text = s_NeedToDo;

            string s_UpdateTime = Lib_File.Ini.GetIni("Setting", "UpdateTime", "60", s_path);
            Setting_UpdateTime.Text = s_UpdateTime;
            string s_Update = Lib_File.Ini.GetIni("Setting", "Update", "0", s_path);
            Setting_Update.Text = s_Update;

            string s_SpeechCount = Lib_File.Ini.GetIni("Setting", "SpeechCount", "0", s_path);
            Setting_SpeechCount.Text = s_SpeechCount;
            string s_isAloneDripReserve = Lib_File.Ini.GetIni("Setting", "IsAloneDripReserve", "0", s_path);
            Setting_IsAloneDripReserve.Text = s_isAloneDripReserve;
            string s_server = Lib_File.Ini.GetIni("PLC", "IP", s_path);
            PLC_IP.Text = s_server;
            string s_port = Lib_File.Ini.GetIni("PLC", "Port", s_path);
            PLC_Port.Text = s_port;
            string s_server_1 = Lib_File.Ini.GetIni("HMIBrew", "IP", s_path);
            HMIBrew_IP.Text = s_server_1;
            string s_port_1 = Lib_File.Ini.GetIni("HMIBrew", "Port", s_path);
            HMIBrew_Port.Text = s_port_1;
            string s_server1 = Lib_File.Ini.GetIni("HMI1", "IP", s_path);
            HMI1_IP.Text = s_server1;
            string s_server1_s = Lib_File.Ini.GetIni("HMI1", "IP_s", s_path);
            string s_port1 = Lib_File.Ini.GetIni("HMI1", "Port", s_path);
            HMI1_Port.Text = s_port1;
            string s_server2 = Lib_File.Ini.GetIni("HMI2", "IP", s_path);
            HMI2_IP.Text = s_server2;
            string s_server2_s = Lib_File.Ini.GetIni("HMI2", "IP_s", s_path);
            string s_port2 = Lib_File.Ini.GetIni("HMI2", "Port", s_path);
            HMI2_Port.Text = s_port2;
            string s_server3 = Lib_File.Ini.GetIni("HMI3", "IP", s_path);
            HMI3_IP.Text = s_server3;
            string s_server3_s = Lib_File.Ini.GetIni("HMI3", "IP_s", s_path);
            string s_port3 = Lib_File.Ini.GetIni("HMI3", "Port", s_path);
            HMI3_Port.Text = s_port3;
            string s_server4 = Lib_File.Ini.GetIni("HMI4", "IP", s_path);
            HMI4_IP.Text = s_server4;
            string s_server4_s = Lib_File.Ini.GetIni("HMI4", "IP_s", s_path);
            string s_port4 = Lib_File.Ini.GetIni("HMI4", "Port", s_path);
            HMI4_Port.Text = s_port4;
            string s_server5 = Lib_File.Ini.GetIni("HMI5", "IP", s_path);
            HMI5_IP.Text = s_server5;
            string s_server5_s = Lib_File.Ini.GetIni("HMI5", "IP_s", s_path);
            string s_port5 = Lib_File.Ini.GetIni("HMI5", "Port", s_path);
            HMI5_Port.Text = s_port5;
            string s_server6 = Lib_File.Ini.GetIni("HMI6", "IP", s_path);
            HMI6_IP.Text = s_server6;
            string s_server6_s = Lib_File.Ini.GetIni("HMI6", "IP_s", s_path);
            string s_port6 = Lib_File.Ini.GetIni("HMI6", "Port", s_path);
            HMI6_Port.Text = s_port6;
            string hMIBaClo_IP = Lib_File.Ini.GetIni("HMIBaClo", "IP", s_path);
            HMIBaClo_IP.Text = hMIBaClo_IP;
            string HMIBaClo_s_port6 = Lib_File.Ini.GetIni("HMIBaClo", "Port", s_path);
            HMIBaClo_Port.Text = HMIBaClo_s_port6;
            string power_IP = Lib_File.Ini.GetIni("Power", "IP", s_path);
            Power_IP.Text = power_IP;
            string power_port = Lib_File.Ini.GetIni("Power", "Port", s_path);
            Power_Port.Text = power_port;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            //获取IO_Mapping
            try
            {
                IOMapping IO = new IOMapping();
                string s_path = Environment.CurrentDirectory + "\\Config\\IO_Mapping.ini";

                foreach (PropertyInfo info in IO.GetType().GetProperties())
                {
                    char[] ca_separator = { '_' };
                    string s_head = info.Name.Split(ca_separator)[0];
                    try
                    {
                        int i_value = Convert.ToInt32(Lib_File.Ini.GetIni(s_head, info.Name, s_path));
                        Control control = this.Controls.Find(info.Name, true).FirstOrDefault();
                        if (control != null && control is TextBox)
                        {
                            ((TextBox)control).Text = i_value.ToString();
                        }
                        IO.GetType().GetProperty(info.Name).SetValue(IO, i_value);
                    }
                    catch (Exception ex)
                    {
                        //if (ex.Message != "Input string was not in a correct format." && ex.Message != "输入字符串的格式不正确。")
                        //    throw;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            //获取ADT8940A1_IOs
            try
            {
                ADT8940A1_IO adt8940a1 = new ADT8940A1_IO();
                string sPath = Environment.CurrentDirectory + "\\Config\\ADT8940A1_IO.ini";

                foreach (PropertyInfo info in adt8940a1.GetType().GetProperties())
                {
                    char[] separator = { '_' };
                    string head = info.Name.Split(separator)[0];
                    try
                    {
                        int value = Convert.ToInt32(Lib_File.Ini.GetIni(head, info.Name, sPath));

                        Control control = this.Controls.Find(info.Name, true).FirstOrDefault();
                        if (control != null && control is TextBox)
                        {
                            ((TextBox)control).Text = value.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        //if (ex.Message != "Input string was not in a correct format." && ex.Message != "输入字符串的格式不正确。")
                        //    throw;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("确定保存吗?", "保存数据", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Parameter parameter = new Parameter();
                string sPath1 = Environment.CurrentDirectory + "\\Config\\parameter.ini";
                foreach (Control c1 in tabControl1.TabPages[0].Controls)
                {
                    if (c1 is TextBox)
                    {
                        char[] separator = { '_' };
                        string head = c1.Name.Split(separator)[0];
                        Lib_File.Ini.WriteIni(head, c1.Name, c1.Text, sPath1);
                    }
                }
                MessageBox.Show("保存完成");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("确定保存吗?", "保存数据", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Parameter parameter = new Parameter();
                string sPath1 = Environment.CurrentDirectory + "\\Config\\Config.ini";
                foreach (Control c1 in tabControl1.TabPages[4].Controls)
                {
                    if (c1 is TextBox)
                    {
                        char[] separator = { '_' };
                        string head = c1.Name.Split(separator)[0];
                        string key = c1.Name.Split(separator,2)[1];
                        Lib_File.Ini.WriteIni(head, key, c1.Text, sPath1);
                    }
                }
                MessageBox.Show("保存完成");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("确定保存吗?", "保存数据", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                if (radioButton1.Checked)
                {
                    Parameter parameter = new Parameter();
                    string sPath1 = Environment.CurrentDirectory + "\\Config\\IO_Mapping.ini";
                    foreach (Control c1 in tabControl1.TabPages[3].Controls)
                    {
                        if (c1 is TextBox)
                        {
                            char[] separator = { '_' };
                            string head = c1.Name.Split(separator)[0];
                            Lib_File.Ini.WriteIni(head, c1.Name, c1.Text, sPath1);
                        }
                    }
                }
                else
                {
                    Parameter parameter = new Parameter();
                    string sPath1 = Environment.CurrentDirectory + "\\Config\\ADT8940A1_IO.ini";
                    foreach (Control c1 in tabControl1.TabPages[3].Controls)
                    {
                        if (c1 is TextBox)
                        {
                            char[] separator = { '_' };
                            string head = c1.Name.Split(separator)[0];
                            Lib_File.Ini.WriteIni(head, c1.Name, c1.Text, sPath1);
                        }
                    }
                }
                MessageBox.Show("保存完成");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("确定保存吗?", "保存数据", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Parameter parameter = new Parameter();
                string sPath1 = Environment.CurrentDirectory + "\\Config\\parameter.ini";
                foreach (Control c1 in tabControl1.TabPages[2].Controls)
                {
                    if (c1 is TextBox)
                    {
                        char[] separator = { '_' };
                        string head = c1.Name.Split(separator)[0];
                        Lib_File.Ini.WriteIni(head, c1.Name, c1.Text, sPath1);
                    }
                }
                MessageBox.Show("保存完成");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("确定保存吗?", "保存数据", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Parameter parameter = new Parameter();
                string sPath1 = Environment.CurrentDirectory + "\\Config\\parameter.ini";
                foreach (Control c1 in tabControl1.TabPages[1].Controls)
                {
                    if (c1 is TextBox)
                    {
                        char[] separator = { '_' };
                        string head = c1.Name.Split(separator)[0];
                        Lib_File.Ini.WriteIni(head, c1.Name, c1.Text, sPath1);
                    }
                }
                MessageBox.Show("保存完成");

            }
        }
    }
}
