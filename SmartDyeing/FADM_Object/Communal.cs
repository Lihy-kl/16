using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CHNSpec.Device.Bluetooth;
using CHNSpec.Device.Models;
using CHNSpec.Device.Models.Enums;
using BLECode;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Data;
using System.Windows.Forms;
using static SmartDyeing.FADM_Object.Communal;


namespace SmartDyeing.FADM_Object
{
    public class Communal
    {
        //蓝牙分光仪
        public static BluetoothHelper _helper = new BluetoothHelper();

        private static readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 全自动滴液机数据库对象
        /// </summary>
        public static Lib_DataBank.SQLServer _fadmSqlserver { get; set; }

        /// <summary>
        /// 梅特勒天平对象
        /// </summary>
        public static Lib_SerialPort.Balance.METTLER Mettler { get; set; }

        /// <summary>
        /// 日本星光天平对象
        /// </summary>
        public static Lib_SerialPort.Balance.SHINKO Shinko { get; set; }


        //
        /// <summary>
        /// plc modbus tcp对象
        /// </summary>
        public static TCPModBus _tcpModBus { get; set; }

        /// <summary>
        /// 开料机对象（TCP）
        /// </summary>
        public static TCPModBus _tcpModBusBrew { get; set; }

        /// <summary>
        /// 开料机对象(串口)
        /// </summary>
        public static Lib_SerialPort.HMI.BrewHMI MyBrew { get; set; } = null;

        /// <summary>
        /// 吸光度机对象
        /// </summary>
        public static TCPModBus _tcpModBusAbs { get; set; }


        public static SmartDyeing.FADM_Object.MyRegister _softReg { get; set; }

        public static string _s_machineCode { get; set; }



        public static string _s_version { get; set; }


        /// <summary>
        /// plc天平读数
        /// </summary>
        public static string _s_balanceValue { get; set; }

        /// <summary>
        /// 天平复检重量
        /// </summary>
        public static double _d_reviewBalance { get; set; }


        /// <summary>
        /// 机台状态：
        /// 0：待机
        /// 1：手动回零
        /// 2：定坐标
        /// 3：联动
        /// 4：定点移动
        /// 5：校正
        /// 6：针检
        /// 7：滴液
        /// 8：异常
        /// 9：滴液完成
        /// 10:复位
        /// 11:自检
        /// </summary>
        private static int _i_machineStatus { get; set; }

        public static int ReadMachineStatus()
        {
            _rwLock.EnterReadLock();
            try
            {
                return _i_machineStatus;
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }

        public static void WriteMachineStatus(int value)
        {
            _rwLock.EnterWriteLock();
            try
            {
                _i_machineStatus = value;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }

        public static double dBalanceValue = 0.00;

        /// <summary>
        /// 操作母液瓶号
        /// </summary>
        public static int _i_optBottleNum { get; set; }

        /// <summary>
        /// 操作滴液杯号
        /// </summary>
        public static int _i_OptCupNum { get; set; }

        /// <summary>
        /// 上下气缸类型
        /// </summary>
        public static int _i_cylinderVersion = 1;

        /// <summary>
        /// 自检数组
        /// </summary>
        //public static double[] _da_self = {   11.00, 5.00, 2.00, 0.50, 4.00 };


        //public static double[] _da_self = {1.00, 11.00, 5.00, 2.00, 0.50, 1.00 };

        public static double[] _da_self = { 1.00, 0.50, 2.00, 5.00, 11.00, 2.00 };


        //public static double[] _da_self = { 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00 };
        /// <summary>
        /// 加水方式
        /// 0:不带罐
        /// 1：带罐
        /// </summary>
        public static int _i_waterWay = 1;

        /// <summary>
        /// 手自动状态
        /// </summary>
        public static bool _b_auto = true;


        /// <summary>
        /// 天平报警
        /// </summary>
        public static bool _b_balanceAlarm = false;

        /// <summary>
        /// 废液桶最大重量
        /// </summary>
        public static double _d_balanceMaxWeight = 1000;

        /// <summary>
        /// 暂停
        /// </summary>
        public static bool _b_pause = false;

        /// <summary>
        /// 停止
        /// </summary>
        public static bool _b_stop = false;

        /// <summary>
        /// 注液类型
        /// 0：气缸上限位注液
        /// 1：气缸中限位注液
        /// </summary>
        public static int _i_infusionType = 0;

        /// <summary>
        /// 染色机状态
        /// 0：下线
        /// 1：上线
        /// </summary>
        public static int[] _ia_dyeStatus = { 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// 染色机通讯失败时间
        /// </summary>
        public static string[] _sa_dyeConFTime = { "", "", "", "", "", "" };

        /// <summary>
        /// 报警
        /// </summary>
        public static string[] _sa_dyeAlarm = { "", "", "", "", "", "" };

        /// <summary>
        /// 报警
        /// </summary>
        public static int[] _ia_alarmNum = { 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// 报警(翻转缸)
        /// </summary>
        public static int[] _ia_alarmNum1 = new int[Lib_Card.Configure.Parameter.Machine_Cup_Total];

        /// <summary>
        /// 报警(翻转缸)
        /// </summary>
        public static string[] _sa_dyeAlarm1 = new string[Lib_Card.Configure.Parameter.Machine_Cup_Total];

        /// <summary>
        /// 染色机对象
        /// </summary>
        //public static Lib_SerialPort.HMI.HMI DyeHMI { get; set; } = null;

        /// <summary>
        /// 染色机对象tcp通讯
        /// </summary>
        public static HMITCPModBus _tcpDyeHMI1 { get; set; } = null;
        public static HMITCPModBus _tcpDyeHMI1_s { get; set; } = null;

        /// <summary>
        /// 染色机对象tcp通讯
        /// </summary>
        public static HMITCPModBus _tcpDyeHMI2 { get; set; } = null;
        public static HMITCPModBus _tcpDyeHMI2_s { get; set; } = null;

        /// <summary>
        /// 染色机对象tcp通讯
        /// </summary>
        public static HMITCPModBus _tcpDyeHMI3 { get; set; } = null;
        public static HMITCPModBus _tcpDyeHMI3_s { get; set; } = null;

        /// <summary>
        /// 染色机对象tcp通讯
        /// </summary>
        public static HMITCPModBus _tcpDyeHMI4 { get; set; } = null;
        public static HMITCPModBus _tcpDyeHMI4_s { get; set; } = null;

        /// <summary>
        /// 染色机对象tcp通讯
        /// </summary>
        public static HMITCPModBus _tcpDyeHMI5 { get; set; } = null;
        public static HMITCPModBus _tcpDyeHMI5_s { get; set; } = null;

        /// <summary>
        /// 染色机对象tcp通讯
        /// </summary>
        public static HMITCPModBus _tcpDyeHMI6 { get; set; } = null;
        public static HMITCPModBus _tcpDyeHMI6_s { get; set; } = null;


        /// <summary>
        /// 称布触摸屏对象
        /// </summary>
        public static HMITCPModBus HMIBaClo { get; set; } = null;






        /// <summary>
        /// 开料机对象
        /// </summary>
        //public static Lib_SerialPort.HMI.BrewHMI MyBrew { get; set; } = null;

        /// <summary>
        /// 扫码枪对象
        /// </summary>
        //public static Lib_SerialPort.Scan.Scan MyScan { get; set; } = null;



        /// <summary>
        /// 滴液完成的杯号
        /// </summary>
        public static List<int> _lis_dripSuccessCup = new List<int>();

        /// <summary>
        /// 滴液失败的杯号
        /// </summary>
        public static List<int> _lis_dripFailCup = new List<int>();

        /// <summary>
        /// 滴液失败完成的杯号
        /// </summary>
        public static List<int> _lis_dripFailCupFinish = new List<int>();

        /// <summary>
        /// 染色停止的杯号
        /// </summary>
        public static List<int> _lis_dripStopCup = new List<int>();

        /// <summary>
        /// 需要洗杯的杯号
        /// </summary>
        public static List<int> _lis_washCup = new List<int>();

        /// <summary>
        /// 洗杯完成的杯号
        /// </summary>
        public static List<int> _lis_washCupFinish = new List<int>();

        /// <summary>
        /// 洗杯完成的杯号(使用在加水时区分)
        /// </summary>
        public static List<int> _lis_addwashCupFinish = new List<int>();

        /// <summary>
        /// 前洗杯的杯号
        /// </summary>
        public static List<int> _lis_ForwordwashCup = new List<int>();

        /// <summary>
        /// 已发启动信号
        /// </summary>
        public static List<int> _lis_SendReadyCup = new List<int>();

        /// <summary>
        /// 染色线程
        /// </summary>
        private static Thread DyeThread = null;
        public static Thread ReadDyeThread()
        {
            _rwLock.EnterReadLock();
            try
            {
                return DyeThread;
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }

        public static void WriteDyeThread(Thread value)
        {
            _rwLock.EnterWriteLock();
            try
            {
                DyeThread = value;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }



        /// <summary>
        /// 滴液线程可以等待标志位
        /// </summary>
        private static bool _b_dripWait = false;
        public static bool ReadDripWait()
        {
            _rwLock.EnterReadLock();
            try
            {
                return _b_dripWait;
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }

        public static void WriteDripWait(bool value)
        {
            _rwLock.EnterWriteLock();
            try
            {
                _b_dripWait = value;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }


        /// <summary>
        /// 开料机通讯中断标志位
        /// </summary>
        public static bool _b_brewErr = false;

        /// <summary>
        /// 吸光度机通讯中断标志位
        /// </summary>
        public static bool _b_absErr = false;


        /// <summary>
        /// 是否打开等待列表页面
        /// </summary>
        public static bool _b_isOpenWaitList = false;

        /// <summary>
        /// 工艺重名名
        /// </summary>
        public static string _s_reName = null;


        /// <summary>
        /// 是否是染固色完成正在完成
        /// </summary>
        public static bool _b_finshRun = true;


        /// <summary>
        /// 是否正在备份数据
        /// </summary>
        public static bool _b_isBackUp = false;

        /// <summary>
        /// 是否点击开始滴液或自动滴液
        /// </summary>
        public static bool _b_isDripping = false;

        /// <summary>
        /// 当前操作员
        /// </summary>
        public static string _s_operator = null;

        /// <summary>
        /// 液量低是否滴液
        /// </summary>
        public static bool _b_isLowDrip = false;

        /// <summary>
        /// 超出生命周期是否滴液
        /// </summary>
        public static bool _b_isOutDrip = false;

        /// <summary>
        /// 满量程滴液
        /// </summary>
        public static bool _b_isFullDrip = true;

        /// <summary>
        /// 是否复检天平清零
        /// </summary>
        public static bool _b_isZero = false;

        /// <summary>
        /// 显示注册信息
        /// </summary>
        public static string _s_regInfo = null;

        /// <summary>
        /// 是否倒序
        /// </summary>
        public static bool _b_isDesc = false;

        /// <summary>
        /// 滴液区杯号
        /// </summary>
        public static List<int> _lis_dripCupNum = new List<int>();


        /// <summary>
        /// 逻辑滴液区杯号 为了配合称布
        /// </summary>
        public static Dictionary<int, List<int>> my_lis_dripCupNum = new Dictionary<int, List<int>>();

        /// <summary>
        /// 后处理杯号
        /// </summary>
        public static List<int> _lis_dyeCupNum = new List<int>();

        /// <summary>
        /// 副杯杯号
        /// </summary>
        public static List<int> _lis_deputyCupNum = new List<int>();

        /// <summary>
        /// 主杯杯号
        /// </summary>
        public static List<int> _lis_firstCupNum = new List<int>();

        /// <summary>
        /// 主副杯对应
        /// </summary>
        public static Dictionary<int, int> _dic_first_second = new Dictionary<int, int>();

        /// <summary>
        /// 大小杯对应
        /// </summary>
        public static Dictionary<int, int> _dic_big_small_cup = new Dictionary<int, int>();

        /// <summary>
        /// 杯号和序号对应
        /// </summary>
        public static Dictionary<int, int> _dic_cup_index = new Dictionary<int, int>();

        /// <summary>
        /// 后处理杯号和序号对应（用于后处理杯号状态显示）
        /// </summary>
        public static Dictionary<int, int> _dic_dyecup_index = new Dictionary<int, int>();

        /// <summary>
        /// 后处理杯号类型
        /// </summary>
        public static Dictionary<int, int> _dic_dyeType = new Dictionary<int, int>();

        /// <summary>
        /// 16杯模式杯号(用于显示参数页面)
        /// </summary>
        public static Dictionary<int, int> _dic_SixteenCupNum = new Dictionary<int, int>();

        public static List<int> _lis_SixteenCupNum = new List<int>();

        public static bool _b_registerOld = false;//老校验方式

        public static bool _b_isDripAll = false;//是否分区域滴液，false:分区域;true:全部一起滴液

        public static bool _b_isAssitantFirst = false;//是否先滴助剂，false:先滴染料;true:先滴助剂

        public static bool _b_isNeedCheck = true;//开料完是否需要针检

        public static bool _b_isAutoAbs = true;//有吸光度机时，是否开料就加入检测

        public static bool _b_isUseWaterTestBase = false;//吸光度机是否使用水来测试基准点

        public static bool _b_isFinishSend = true;//是否单杯完成就下发

        public static bool _b_isUpdateNotDripList = false;//是否需要刷新尚未滴液列表

        public static bool _b_isNetWork = false;//网络推送

        public static bool _b_isDripReserveFirst = true;//是否先滴预留到天平

        public static bool _b_isDebug = false;//是否调试模式

        public static bool _b_isBalanceInDrip = true;//天平是否放置滴液区

        public static bool _b_isCupAreaOnly = false;//配液杯是否独立页面

        public static bool _b_isNewSet = true;//母液区摆设(0:以前传统滴液机母液区摆设 1:PLC版母液区摆设,原点在10号母液瓶)

        public static bool _b_isGetDryClamp = false;//是否拿着干布夹子

        public static bool _b_isGetWetClamp = false;//是否拿着湿布夹子

        public static bool _b_isGetSyringes = false;//是否拿着针筒(ABS抽液针筒)

        public static bool _b_isSaveRealConcentration = false;//保存时是否将设定浓度保存到实际浓度中

        public static bool _b_isUseClamp = false;//是否使用夹子自动放布

        public static bool _b_isUseClampOut = false;//是否使用夹子自动出布

        public static bool _b_isUseAuto = true;//是否使用自动启动

        public static bool _b_isexpired = false;//是否过期

        public static bool _b_closebrew = false;//关闭开料机通讯

        public static int _i_needPulse = 0;//当前母液瓶第一杯虚假脉冲
        public static int _i_needPulseCupNumber = 0;//当前母液瓶第一杯杯号

        public static List<int> _lis_warmBottle = new List<int>();//记录一轮循环预滴液时判断液量低

        public static bool _b_getFile = true;//是否开始拿文件
        public static bool _b_getFile_sec = true;//是否开始拿文件
        public static bool[] _b_isFullTest = { false, false };//是否吸光度全测量

        public static double _d_ppm = 30;//吸光度稀释ppm值

        public static double _d_absMax = 0.004;//吸光度对比值

        public static double _d_TestSpan = 30;//吸光度测试间隔

        public static double _d_abs_total = 50;//吸光度总液量

        public static bool _b_isMove = false;//后处理是否移动过，用于判断是否需要回待机位

        public static string  _s_absPath = "";//吸光度结果导出路径

        public static bool _b_isWaitDrip = false;//是否等待滴液

        public static double _d_abs_sub = 40;//吸光度白点差值判断范围

        public static double _d_abs_mixture = 0;//混合液计算的浓度

        public static bool _b_isUnitChange = false;//助剂可以切换%单位

        public static string URL = ""; //服务器url

        public static int _b_isDyMin = 0; //滴液位置从多少开始


        public static int _b_DyeCupNum = 0; //打板的杯数


        public static int _i_Head_Ip = 40;//表头开始识别码开始位置
        public static int _i_Head_Ip_Len = 40;//表头开始识别码长度

        public static int _i_Head_FormulaCode = 40;//表头配方代码开始位置
        public static int _i_Head_FormulaCode_Len = 40;//表头配方代码长度

        public static int _i_Head_VersionNum = 40;//表头配方版本开始位置
        public static int _i_Head_VersionNum_Len = 40;//表头配方版本长度

        public static int _i_Head_Count = 40;//表头染助劑支數开始位置
        public static int _i_Head_Count_Len = 40;//表头染助劑支數长度

        public static int _i_Head_Unit = 40;//表头配方單位开始位置
        public static int _i_Head_Unit_Len = 40;//表头配方單位长度

        public static int _i_Head_Index = 40;//表头滴定序號开始位置
        public static int _i_Head_Index_Len = 40;//表头滴定序號长度

        public static int _i_Head_FormulaName = 40;//表头配方名称开始位置
        public static int _i_Head_FormulaName_Len = 40;//表头配方名称长度

        public static int _i_Head_ClothWeight = 40;//表头布重开始位置
        public static int _i_Head_ClothWeight_Len = 40;//表头布重长度

        public static int _i_Head_TotalWeight = 40;//表头总浴量开始位置
        public static int _i_Head_TotalWeight_Len = 40;//表头总浴量长度

        public static int _i_Head_AddWater = 40;//表头是否加水开始位置
        public static int _i_Head_AddWater_Len = 40;//表头是否加水长度

        public static int _i_Head_ConAdd = 40;//表头是否续加开始位置
        public static int _i_Head_ConAdd_Len = 40;//表头是否续加长度

        public static int _i_Head_MNum = 40;//表头机台编号开始位置
        public static int _i_Head_MNum_Len = 40;//表头机台编号长度

        public static int _i_Head_Date = 40;//表头日期开始位置
        public static int _i_Head_Date_Len = 40;//表头日期长度

        public static int _i_Head_Code = 40;//表头染程开始位置
        public static int _i_Head_Code_Len = 40;//表头染程长度

        public static int _i_Head_Type = 40;//表头配方類型开始位置
        public static int _i_Head_Type_Len = 40;//表头配方類型长度

        public static int _i_Head_Drip = 40;//表头是否滴過开始位置
        public static int _i_Head_Drip_Len = 40;//表头是否滴過长度

        public static int _i_Head_SIN = 40;//表头结束识别码开始位置
        public static int _i_Head_SIN_Len = 40;//表头结束识别码长度

        public static int _i_Detail_Ip = 40;//详情开始识别码开始位置
        public static int _i_Detail_Ip_Len = 40;//详情开始识别码长度

        public static int _i_Detail_FormulaCode = 40;//详情配方代码开始位置
        public static int _i_Detail_FormulaCode_Len = 40;//详情配方代码长度

        public static int _i_Detail_VersionNum = 40;//详情配方版本开始位置
        public static int _i_Detail_VersionNum_Len = 40;//详情配方版本长度

        public static int _i_Detail_Index = 40;//详情滴定序號开始位置
        public static int _i_Detail_Index_Len = 40;//详情滴定序號长度

        public static int _i_Detail_Unit = 40;//详情配方單位开始位置
        public static int _i_Detail_Unit_Len = 40;//详情配方單位长度

        public static int _i_Detail_Num = 40;//详情库存编号开始位置
        public static int _i_Detail_Num_Len = 40;//详情库存编号长度

        public static int _i_Detail_AssistantCode = 40;//详情助剂代码开始位置
        public static int _i_Detail_AssistantCode_Len = 40;//详情助剂代码长度

        public static int _i_Detail_RealConcentration = 40;//详情助剂浓度开始位置
        public static int _i_Detail_RealConcentration_Len = 40;//详情助剂浓度长度

        public static int _i_Detail_SIN = 40;//详情结束识别码开始位置
        public static int _i_Detail_SIN_Len = 40;//详情结束识别码长度

        /// <summary>
        /// 滴液过程是否先加水后加染料
        /// </summary>
        public static bool _b_isAddWaterFirst = true;

        /// <summary>
        /// 排序
        /// </summary>
        public static bool _b_isSort = true;

        /// <summary>
        /// 滴液区需要手动输入杯号
        /// </summary>
        public static bool _b_isDripNeedCupNum = false;

        /// <summary>
        /// 是否使用称布系统
        /// </summary>
        public static bool _b_isUseCloth = false;

        /// <summary>
        /// 是否在放布确认前可添加副杯滴液
        /// </summary>
        public static bool _b_isNeedConfirm = false;

        /// <summary>
        /// 配方界面上的浴比更改后 底下的所有工艺浴比跟着变化
        /// </summary>
        public static bool _b_isBathRatioTxtDyBath = false;

        /// <summary>
        /// 是否单独使用开料机(只有开料机)
        /// </summary>
        public static bool _b_isUseBrewOnly = false;


        /// <summary>
        /// 区域1触摸屏版本
        /// </summary>
        public static string _s_TouchVer1 = "";

        /// <summary>
        /// 区域1板卡1版本
        /// </summary>
        public static string _s_CardOneVer1 = "";

        /// <summary>
        /// 区域2板卡2版本
        /// </summary>
        public static string _s_CardTwoVer1 = "";

        /// <summary>
        /// 区域2触摸屏版本
        /// </summary>
        public static string _s_TouchVer2 = "";

        /// <summary>
        /// 区域2板卡1版本
        /// </summary>
        public static string _s_CardOneVer2 = "";

        /// <summary>
        /// 区域2板卡2版本
        /// </summary>
        public static string _s_CardTwoVer2 = "";

        /// <summary>
        /// 区域3触摸屏版本
        /// </summary>
        public static string _s_TouchVer3 = "";

        /// <summary>
        /// 区域3板卡1版本
        /// </summary>
        public static string _s_CardOneVer3 = "";

        /// <summary>
        /// 区域3板卡2版本
        /// </summary>
        public static string _s_CardTwoVer3 = "";

        /// <summary>
        /// 区域4触摸屏版本
        /// </summary>
        public static string _s_TouchVer4 = "";

        /// <summary>
        /// 区域4板卡1版本
        /// </summary>
        public static string _s_CardOneVer4 = "";

        /// <summary>
        /// 区域4板卡2版本
        /// </summary>
        public static string _s_CardTwoVer4 = "";

        /// <summary>
        /// 区域5触摸屏版本
        /// </summary>
        public static string _s_TouchVer5 = "";

        /// <summary>
        /// 区域5板卡1版本
        /// </summary>
        public static string _s_CardOneVer5 = "";

        /// <summary>
        /// 区域5板卡2版本
        /// </summary>
        public static string _s_CardTwoVer5 = "";

        /// <summary>
        /// 区域6触摸屏版本
        /// </summary>
        public static string _s_TouchVer6 = "";

        /// <summary>
        /// 区域6板卡1版本
        /// </summary>
        public static string _s_CardOneVer6 = "";

        /// <summary>
        /// 区域6板卡2版本
        /// </summary>
        public static string _s_CardTwoVer6 = "";

        /// <summary>
        /// 最大母液瓶Y轴坐标
        /// </summary>
        public static int _i_Max_Y = 0;

        /// <summary>
        /// 是否显示取小样测PH工艺
        /// </summary>
        public static bool _b_isShowSample = false;

        /// <summary>
        /// 是否有AB助剂
        /// </summary>
        public static bool _b_isUseABAssistant = false;

        /// <summary>
        /// AB助剂数量
        /// </summary>
        public static int _i_ABAssistantCount = 2;

        /// <summary>
        /// 抛出异常中英文对接
        /// </summary>
        public static Dictionary<string, string> _dic_warning = new Dictionary<string, string>();

        /// <summary>
        /// 后处理加药处理
        /// </summary>
        public static Dictionary<int, List<int>> _dic_cup_bottle = new Dictionary<int, List<int>>();



        private static bool _b_readTcpState = true; //进行Plc访问的可允许证

        /// <summary>
        /// 动作错误编号
        /// </summary>
        public static Dictionary<int, string> _dic_errModbusNo = new Dictionary<int, string>();

        /// <summary>
        /// 动作错误编号
        /// </summary>
        public static Dictionary<int, string> _dic_errModbusNoNew = new Dictionary<int, string>();

        /// <summary>
        /// 是否正在写入交互信息
        /// </summary>
        public static bool _b_isWriting = false;

        /// <summary>
        /// 是否已刷新新数据
        /// </summary>
        public static bool _b_isUpdateNewData = false;

        /// <summary>
        /// 调试页面下900-928地址数据
        /// </summary>
        public static int[] _ia_d900 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0 };



        /// <summary>
        /// 蜂鸣器状态 0:初始状态 1:已打开 2：已关闭
        /// </summary>
        public static int _i_bType = 0;

        /// <summary>
        /// 蜂鸣器打开是否发送 
        /// </summary>
        public static bool _b_isBSendOpen = false;

        /// <summary>
        /// 蜂鸣器关闭是否发送 
        /// </summary>
        public static bool _b_isBSendClose = false;

        /// <summary>
        /// PLC版本号
        /// </summary>
        public static string _s_plcVersion = "";

        /// <summary>
        /// 开料机版本号
        /// </summary>
        public static string _s_brewVersion = "";


        /// <summary>
        /// 是否刷新打板机参数数据
        /// </summary>
        public static bool _b_refreshDye = false;

        /// <summary>
        /// 是否已刷新打板机参数数据
        /// </summary>
        public static bool _b_hasrefreshDye = false;


        public static bool ReadTcpStatus()
        {
            _rwLock.EnterReadLock();
            try
            {
                return _b_readTcpState;
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }
        public static void WriteTcpStatus(bool value)
        {
            _rwLock.EnterWriteLock();
            try
            {
                _b_readTcpState = value;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 吸光度数据交互结构
        /// </summary>
        public struct AbsData
        {
            /// <summary>
            /// 工位状态
            /// </summary>
            public string _s_currentState;

            /// <summary>
            /// 动作请求
            /// </summary>
            public string _s_request;

            /// <summary>
            /// 保存数据请求
            /// </summary>
            public string _s_datarequest;

            /// <summary>
            /// 母液瓶号
            /// </summary>
            public string _s_boottlenum;

            /// <summary>
            /// 数据数量
            /// </summary>
            public string _s_totaldata;

            /// <summary>
            /// 报警
            /// </summary>
            public string _s_warm;

            //申请加药状态 0 代表可接收信号写入数据库 1代表写入数据库完成 2 代表执行对应操作完成(接收到2后通过刷新才能变为0)
            public int _i_requesadd;

            //历史状态 0 正常结束 1需要前洗杯
            public string _s_history;
        }


        public struct sAddArg
        {
            public string _obj_batchName;//  _obj_batchName：所属批次
            public string _s_syringeType;// _s_syringeType：针筒类型
            public int _i_minBottleNo;// _i_minBottleNo：母液瓶号
            public int _i_pulseT;// _i_pulseT：总脉冲
            public int _i_adjust;// _i_adjust：校正脉冲
            public string _s_unitOfAccount;//  _s_unitOfAccount：单位

            public Dictionary<int, int> _dic_pulse;//  _dic_pulse：配液杯对应加药脉冲
            public Dictionary<int, double> _dic_water;//  directoryWeight：配液杯对应加水重
        }

        /// <summary>
        /// 加药封装
        /// Type：类型，0代表滴液调用 1代表后处理调用
        /// _i_minBottleNo：母液瓶号
        /// _s_syringeType：针筒类型
        /// _i_pulseT：总脉冲
        /// _i_adjust:校正脉冲
        ///  _s_unitOfAccount：单位
        ///  iLowSrart：当前状态
        ///  _obj_batchName：所属批次
        ///  _dic_pulse：配液杯对应加药脉冲
        ///  directoryWeight：配液杯对应加药重
        ///  lWater:后处理加药时先加水的重量
        /// </summary>
        /// /// <returns>0：正常；-1：夹不到针筒退出；-2：收到退出消息</returns>
        
        public static int AddMac(sAddArg o, ref Dictionary<int, double> directoryReturn, int i_type)
        {
            List<int> lis_data = new List<int>();
            string s_batchName = o._obj_batchName;
            int i_minBottleNo = o._i_minBottleNo;
            string s_syringeType = o._s_syringeType;
            int i_pulseT = o._i_pulseT;
            int i_adjust = o._i_adjust;
            string s_unitOfAccount = o._s_unitOfAccount;

            Dictionary<int, int> dic_pulse = o._dic_pulse;
            Dictionary<int, double> dic_water = o._dic_water;

            Thread thread = null;

            //如果是母液瓶999就先去拿针筒
            if (i_minBottleNo == 999)
            {
                int i_xStart = 0, i_yStart = 0;
                if (Communal._b_isGetWetClamp)
                {
                    //3.放夹子
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                    //int i_xStart = 0, i_yStart = 0;
                    //计算湿布布夹子位置
                    MyModbusFun.CalTarget(9, 0, ref i_xStart, ref i_yStart);
                    int i_mRes2 = MyModbusFun.PutClamp(i_xStart, i_yStart);
                    if (-2 == i_mRes2)
                        throw new Exception("收到退出消息");
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                }

                if (Communal._b_isGetDryClamp)
                {
                    //3.放夹子
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子启动");
                    //int i_xStart = 0, i_yStart = 0;
                    //计算干布夹子位置
                    MyModbusFun.CalTarget(8, 0, ref i_xStart, ref i_yStart);
                    int iMRes1 = MyModbusFun.PutClamp(i_xStart, i_yStart);
                    if (-2 == iMRes1)
                        throw new Exception("收到退出消息");
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "放夹子完成");
                }

                if (!Communal._b_isGetSyringes)
                {
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "拿针筒启动");
                    //计算针筒位置
                    MyModbusFun.CalTarget(11, 0, ref i_xStart, ref i_yStart);
                    int i_mRes3 = MyModbusFun.GetSyringes(i_xStart, i_yStart);
                    if (-2 == i_mRes3)
                        throw new Exception("收到退出消息");
                    FADM_Object.Communal._fadmSqlserver.InsertRun("Dail", "拿针筒完成");
                }
            }

        label9:
            //移动到母液瓶
            if (FADM_Auto.Drip._b_dripStop)
            {
                FADM_Object.Communal._b_stop = true;
            }

            //if (Lib_Card.Configure.Parameter.Machine_Type == 0 && Lib_Card.Configure.Parameter.Machine_Type_Lv == 1)
            //{
            //    //富士伺服在下面判断 天平状态 原有不动 绿维的放在上面 并且置位 是否回原点 绿维的放移动机械手前面

            //    //判断是否异常
            //    FADM_Object.Communal.BalanceState("滴液");
            //}

            int i_mRes;
            if (i_minBottleNo == 999)
            {
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找1号(吸光度杯)");
                FADM_Object.Communal._i_optBottleNum = i_minBottleNo;
                i_mRes = MyModbusFun.TargetMove(10, 1, 0);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达1号(吸光度杯)");
            }
            else
            {

                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找" + i_minBottleNo + "号母液瓶");
                FADM_Object.Communal._i_optBottleNum = i_minBottleNo;
                i_mRes = MyModbusFun.TargetMove(0, i_minBottleNo, 1);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达" + i_minBottleNo + "号母液瓶");
            }

            //抽液
            if (FADM_Auto.Drip._b_dripStop)
            {
                FADM_Object.Communal._b_stop = true;
            }
            int iExtractionPulse = 0;
            if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
            {
                iExtractionPulse = (i_pulseT + i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1));
                //FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "脉冲(" + i_pulseT + ")"+ "校正值(" + i_adjust + ")" + "Other_Z_BackPulse(" + Lib_Card.Configure.Parameter.Other_Z_BackPulse + ")" + "Other_S_MaxPulse(" + Lib_Card.Configure.Parameter.Other_S_MaxPulse + ")" + "_b_isFullDrip(" + (Communal._b_isFullDrip?"1":"0") + ")" + "_b_isDripReserveFirst(" + (FADM_Object.Communal._b_isDripReserveFirst ? "1" : "0") + ")");
                if (iExtractionPulse - Lib_Card.Configure.Parameter.Other_Z_BackPulse > Lib_Card.Configure.Parameter.Other_S_MaxPulse)
                {
                    if (FADM_Object.Communal._b_isFullDrip)
                    {
                        //超出单针筒上限
                        iExtractionPulse = Lib_Card.Configure.Parameter.Other_S_MaxPulse + Lib_Card.Configure.Parameter.Other_Z_BackPulse;
                    }
                    else
                    {
                        iExtractionPulse = i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1);
                        int i_temp = iExtractionPulse;
                        for (int i = 0; i < dic_pulse.Count; i++)
                        {
                            var v1 = dic_pulse.ElementAt(i);

                            i_temp = iExtractionPulse;

                            if (i_temp + v1.Value - Lib_Card.Configure.Parameter.Other_Z_BackPulse > Lib_Card.Configure.Parameter.Other_S_MaxPulse)
                            {
                                //如果需加量大于10g，直接先加
                                //if(v1.Value/1.0/ _i_adjust > Lib_Card.Configure.Parameter.Other_SplitValue)
                                if (i_temp == i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1))
                                {
                                    iExtractionPulse = Lib_Card.Configure.Parameter.Other_S_MaxPulse + Lib_Card.Configure.Parameter.Other_Z_BackPulse;
                                }
                                else
                                {
                                    //如果一筒抽不完就分2次，一次抽完就留下一次独立抽
                                    if (v1.Value + i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1) - Lib_Card.Configure.Parameter.Other_Z_BackPulse > Lib_Card.Configure.Parameter.Other_S_MaxPulse)
                                    {
                                        iExtractionPulse = Lib_Card.Configure.Parameter.Other_S_MaxPulse + Lib_Card.Configure.Parameter.Other_Z_BackPulse;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                iExtractionPulse += v1.Value;
                            }
                        }
                    }
                }
                if (FADM_Object.Communal._b_isDripReserveFirst)
                    iExtractionPulse += i_adjust;
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抽液启动(" + iExtractionPulse + ")");

            label10:
                try
                {
                    iExtractionPulse = iExtractionPulse - Lib_Card.Configure.Parameter.Other_Z_BackPulse;
                    if (i_type == 1)
                        i_mRes = MyModbusFun.Extract(iExtractionPulse, s_unitOfAccount.Equals("g/l") ? true : false, 0); //抽液
                    else
                    {
                        if (i_minBottleNo == 999)
                        {
                            //1号吸光度杯停止搅拌
                            int[] values = new int[1];
                            values[0] = 1;
                            if (!FADM_Object.Communal._tcpModBusAbs._b_Connect)
                            {
                                FADM_Object.Communal._tcpModBusAbs.ReConnect();
                            }

                            FADM_Object.Communal._tcpModBusAbs.Write(806, values);

                            i_mRes = MyModbusFun.AbsExtract(iExtractionPulse, true, 0); //抽液

                            values[0] = 0;
                            if (!FADM_Object.Communal._tcpModBusAbs._b_Connect)
                            {
                                FADM_Object.Communal._tcpModBusAbs.ReConnect();
                            }
                            FADM_Object.Communal._tcpModBusAbs.Write(806, values);
                        }

                        else
                            i_mRes = MyModbusFun.Extract(iExtractionPulse, true, 0); //抽液
                    }

                    if (-2 == i_mRes)
                        throw new Exception("收到退出消息");
                }
                catch (Exception ex)
                {
                    if ("未发现针筒" == ex.Message)
                    {
                        return -1;

                    }
                    else
                        throw;
                }

            }
            else
            {
                iExtractionPulse = (i_pulseT + i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1));
                if (iExtractionPulse - Lib_Card.Configure.Parameter.Other_Z_BackPulse > Lib_Card.Configure.Parameter.Other_B_MaxPulse)
                {
                    if (FADM_Object.Communal._b_isFullDrip)
                    {
                        //超出单针筒上限
                        iExtractionPulse = Lib_Card.Configure.Parameter.Other_B_MaxPulse + Lib_Card.Configure.Parameter.Other_Z_BackPulse;
                    }
                    else
                    {
                        iExtractionPulse = i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1);
                        int i_temp = iExtractionPulse;
                        for (int i = 0; i < dic_pulse.Count; i++)
                        {
                            var v1 = dic_pulse.ElementAt(i);

                            i_temp = iExtractionPulse;

                            if (i_temp + v1.Value - Lib_Card.Configure.Parameter.Other_Z_BackPulse > Lib_Card.Configure.Parameter.Other_B_MaxPulse)
                            {
                                ////如果需加量大于10g，直接先加
                                //if (v1.Value / 1.0 / _i_adjust >= Lib_Card.Configure.Parameter.Other_SplitValue)
                                if (i_temp == i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1))
                                {
                                    iExtractionPulse = Lib_Card.Configure.Parameter.Other_B_MaxPulse + Lib_Card.Configure.Parameter.Other_Z_BackPulse;
                                }
                                else
                                {
                                    if (v1.Value + i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1) - Lib_Card.Configure.Parameter.Other_Z_BackPulse > Lib_Card.Configure.Parameter.Other_B_MaxPulse)
                                    {
                                        iExtractionPulse = Lib_Card.Configure.Parameter.Other_B_MaxPulse + Lib_Card.Configure.Parameter.Other_Z_BackPulse;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                iExtractionPulse += v1.Value;
                            }
                        }
                    }
                }
                if (FADM_Object.Communal._b_isDripReserveFirst)
                    iExtractionPulse += i_adjust;
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抽液启动(" + iExtractionPulse + ")");

            label11:
                try
                {
                    iExtractionPulse = iExtractionPulse - Lib_Card.Configure.Parameter.Other_Z_BackPulse;
                    if (i_type == 1)
                        i_mRes = MyModbusFun.Extract(iExtractionPulse, s_unitOfAccount.Equals("g/l") ? true : false, 1); //抽液
                    else
                    {
                        if (i_minBottleNo == 999)
                        {
                            //1号吸光度杯停止搅拌
                            int[] values = new int[1];
                            values[0] = 1;
                            if (!FADM_Object.Communal._tcpModBusAbs._b_Connect)
                            {
                                FADM_Object.Communal._tcpModBusAbs.ReConnect();
                            }

                            FADM_Object.Communal._tcpModBusAbs.Write(806, values);

                            i_mRes = MyModbusFun.AbsExtract(iExtractionPulse, true, 1); //抽液

                            values[0] = 0;
                            if (!FADM_Object.Communal._tcpModBusAbs._b_Connect)
                            {
                                FADM_Object.Communal._tcpModBusAbs.ReConnect();
                            }

                            FADM_Object.Communal._tcpModBusAbs.Write(806, values);
                        }
                        else
                            i_mRes = MyModbusFun.Extract(iExtractionPulse, true, 1); //抽液
                    }
                    if (-2 == i_mRes)
                        throw new Exception("收到退出消息");
                }
                catch (Exception ex)
                {
                    if ("未发现针筒" == ex.Message)
                    {
                        return -1;


                    }
                    else
                        throw;
                }
            }
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抽液完成");

            if (null != thread)
                thread.Join();

            for (int i = 0; i < lis_data.Count; i++)
            {
                //对应值复称值写入
                directoryReturn.Add(lis_data[i], _d_reviewBalance);
            }

            lis_data.Clear();

            //Lib_SerialPort.Balance.METTLER.bReSetSign = true;

            if (FADM_Object.Communal._b_isDripReserveFirst)
            {



                //移动到天平位，先滴预留量
                if (FADM_Auto.Drip._b_dripStop)
                {
                    FADM_Object.Communal._b_stop = true;
                }
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找天平位");
                //判断是否异常

                //if ((Lib_Card.Configure.Parameter.Machine_Type == 0 && Lib_Card.Configure.Parameter.Machine_Type_Lv == 0) || Lib_Card.Configure.Parameter.Machine_Type == 1)
                {
                    //富士伺服在下面判断 天平状态 原有不动 绿维的放在上面 并且置位 是否回原点

                    //判断是否异常
                    FADM_Object.Communal.BalanceState("滴液");
                }

                double d_blBalanceValueStart;
                if (Lib_Card.Configure.Parameter.Machine_Type == 0)
                {
                    d_blBalanceValueStart = Lib_Card.Configure.Parameter.Machine_BalanceType == 0 ? Convert.ToDouble(string.Format("{0:F2}", FADM_Object.Communal.dBalanceValue)) : Convert.ToDouble(string.Format("{0:F3}", FADM_Object.Communal.dBalanceValue));
                }
                else
                {
                    d_blBalanceValueStart = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", FADM_Object.Communal._s_balanceValue)) : Convert.ToDouble(string.Format("{0:F3}", FADM_Object.Communal._s_balanceValue));
                }


                i_mRes = MyModbusFun.TargetMove(2, 0, 0);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");

                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达天平位");

                int i_zPulse1 = 0;
                //label98:
                if (-1 == MyModbusFun.GetZPosition(ref i_zPulse1))
                    throw new Exception("驱动异常");
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "Z轴脉冲" + i_zPulse1);
                //    if (i_zPulse1 > 0)
                //        goto label98;
                int i_infusionPulse1 = 0;
                if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
                    i_infusionPulse1 = i_zPulse1 - i_adjust;
                else
                    i_infusionPulse1 = i_zPulse1 - i_adjust;

                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "废液注液启动(" + i_infusionPulse1 + ")");
                if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
                    i_mRes = MyModbusFun.Shove(i_infusionPulse1, 0);
                else
                    i_mRes = MyModbusFun.Shove(i_infusionPulse1, 1);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "废液注液完成");

                //等待一秒就当数据稳定
                Thread.Sleep(1000);

                double d_blBalanceValueEnd;
                if (Lib_Card.Configure.Parameter.Machine_Type == 0)
                {
                    d_blBalanceValueEnd = Lib_Card.Configure.Parameter.Machine_BalanceType == 0 ? Convert.ToDouble(string.Format("{0:F2}", FADM_Object.Communal.dBalanceValue)) : Convert.ToDouble(string.Format("{0:F3}", FADM_Object.Communal.dBalanceValue));
                }
                else
                {
                    d_blBalanceValueEnd = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", FADM_Object.Communal._s_balanceValue)) : Convert.ToDouble(string.Format("{0:F3}", FADM_Object.Communal._s_balanceValue));
                }
                double d_blDif = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", d_blBalanceValueEnd - d_blBalanceValueStart)) : Convert.ToDouble(string.Format("{0:F3}", d_blBalanceValueEnd - d_blBalanceValueStart));

                //母液瓶扣减1克
                FADM_Object.Communal._fadmSqlserver.ReviseData(
                    "UPDATE bottle_details SET CurrentWeight = CurrentWeight - " + d_blDif + "  " +
                    "WHERE BottleNum = '" + i_minBottleNo + "';");
                //滴到废液桶时，数据差小于0.5，就当液量不够，提示
                if (d_blDif < 0.5)
                {
                    //记录第一个杯需加量脉冲
                    _i_needPulse = dic_pulse.First().Value;
                    _i_needPulseCupNumber = dic_pulse.First().Key;
                    MyModbusFun.MyMachineReset1(); //复位
                    return -2;
                }

            }

            //移动到配液杯

            List<int> lis_ints = new List<int>();
        label12:
            if (FADM_Auto.Drip._b_dripStop)
            {
                FADM_Object.Communal._b_stop = true;
            }
            int i_zPulse = 0;
        label99:
            if (-1 == MyModbusFun.GetZPosition(ref i_zPulse))
                throw new Exception("驱动异常");
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "Z轴脉冲" + i_zPulse);
            //if (i_zPulse > 0)
            //    goto label99;
            if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
            {

                if (i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1) == i_zPulse)
                {
                    //当前针筒已滴完，进入复检
                    goto label13;
                }
            }
            else
            {
                if (i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1) == i_zPulse)
                {
                    //当前针筒已滴完，进入复检
                    goto label13;
                }

            }

            if (dic_pulse.Count == 0)
            {
                //如果已经没有要添加的杯，就直接进去
                goto label13;
            }
            if (i_type == 1)
            {
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找" + dic_pulse.First().Key + "号配液杯");

                FADM_Object.Communal._i_OptCupNum = dic_pulse.First().Key;
                i_mRes = MyModbusFun.TargetMove(1, dic_pulse.First().Key, 0);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达" + dic_pulse.First().Key + "号配液杯");
            }
            else
            {
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找" + dic_pulse.First().Key + "号(吸光度杯)");

                FADM_Object.Communal._i_OptCupNum = dic_pulse.First().Key;
                i_mRes = MyModbusFun.TargetMove(10, dic_pulse.First().Key, 0);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达" + dic_pulse.First().Key + "号(吸光度杯)");
            }

            //判断是否加水


            if (0 < dic_water[dic_pulse.First().Key])
            {
                //new Water().Add(lWater.First(), "Dail");
                double d_addWaterTime = MyModbusFun.GetWaterTime(dic_water[dic_pulse.First().Key]);//加水时间
                if (d_addWaterTime <= 32)
                {
                    i_mRes = MyModbusFun.AddWater(d_addWaterTime);
                    if (-2 == i_mRes)
                        throw new Exception("收到退出消息");
                }
                else
                {
                    double d = 32;
                    while (true)
                    {
                        if (d_addWaterTime > 32)
                        {
                            //每次减32s
                            i_mRes = MyModbusFun.AddWater(d);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                        }
                        else
                        {
                            i_mRes = MyModbusFun.AddWater(d_addWaterTime);
                            if (-2 == i_mRes)
                                throw new Exception("收到退出消息");
                            break;
                        }
                        d_addWaterTime -= d;
                    }
                }
                //加完水后会把加水量置为0，否则一针筒加药加不完的第二针筒加药时会重复加水
                if (i_type == 1)
                {
                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                    "UPDATE cup_details SET TotalWeight = TotalWeight + " + dic_water[dic_pulse.First().Key] + " WHERE CupNum = " + dic_pulse.First().Key + ";");
                }

                dic_water[dic_pulse.First().Key] = 0;
            }

            //注液
            if (FADM_Auto.Drip._b_dripStop)
            {
                FADM_Object.Communal._b_stop = true;
            }
            int iInfusionPulse = 0;
            if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
            {
                if (i_zPulse - i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1) >= dic_pulse.First().Value)
                {
                    //剩余量大于等于需加量
                    iInfusionPulse = i_zPulse - dic_pulse.First().Value;


                    ////母液瓶扣减
                    //FADM_Object.Communal._fadmSqlserver.ReviseData(
                    //    "UPDATE bottle_details SET CurrentWeight = CurrentWeight - " + directoryWeight.First().Value + " " +
                    //    "WHERE BottleNum = '" + _i_minBottleNo + "';");


                    ////置位完成标志位
                    //FADM_Object.Communal._fadmSqlserver.ReviseData(
                    //    "UPDATE drop_details SET Finish = 1 WHERE BatchName = '" + _obj_batchName + "' AND " +
                    //    "BottleNum = " + _i_minBottleNo + " AND CupNum = " + _dic_pulse.First().Key + ";");

                    i_pulseT -= dic_pulse.First().Value;
                    lis_data.Add(dic_pulse.First().Key);
                    dic_pulse.Remove(dic_pulse.First().Key);
                }
                else
                {
                    iInfusionPulse = i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1);
                    dic_pulse[dic_pulse.First().Key] -= (i_zPulse - i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1));
                    i_pulseT -= (i_zPulse - i_adjust * (Lib_Card.Configure.Parameter.Correcting_S_Weight + 1));
                }



                if (i_type == 1)
                {
                    FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "注液启动(" + iInfusionPulse + ")");
                    i_mRes = MyModbusFun.Shove(iInfusionPulse, 0);
                    if (-2 == i_mRes)
                        throw new Exception("收到退出消息");
                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Machine_SlopDown == 1)
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "注液启动(" + iInfusionPulse + ")");
                        i_mRes = MyModbusFun.AbsShove(iInfusionPulse, 0);
                        if (-2 == i_mRes)
                            throw new Exception("收到退出消息");
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "注液启动(" + iInfusionPulse + ")");
                        i_mRes = MyModbusFun.Shove(iInfusionPulse, 0);
                        if (-2 == i_mRes)
                            throw new Exception("收到退出消息");
                    }
                }

            }
            else
            {
                if (i_zPulse - i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1) >= dic_pulse.First().Value)
                {
                    //剩余量大于等于需加量
                    iInfusionPulse = i_zPulse - dic_pulse.First().Value;

                    ////母液瓶扣减
                    //FADM_Object.Communal._fadmSqlserver.ReviseData(
                    //    "UPDATE bottle_details SET CurrentWeight = CurrentWeight - " + directoryWeight.First().Value + " " +
                    //    "WHERE BottleNum = '" + _i_minBottleNo + "';");

                    ////置位完成标志位
                    //FADM_Object.Communal._fadmSqlserver.ReviseData(
                    //    "UPDATE drop_details SET Finish = 1 WHERE BatchName = '" + _obj_batchName + "' AND " +
                    //    "BottleNum = " + _i_minBottleNo + " AND CupNum = " + _dic_pulse.First().Key + ";");


                    i_pulseT -= dic_pulse.First().Value;
                    lis_data.Add(dic_pulse.First().Key);
                    dic_pulse.Remove(dic_pulse.First().Key);
                }
                else
                {
                    iInfusionPulse = i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1);
                    dic_pulse[dic_pulse.First().Key] -= (i_zPulse - i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1));
                    i_pulseT -= (i_zPulse - i_adjust * (Lib_Card.Configure.Parameter.Correcting_B_Weight + 1));
                }
                if (i_type == 1)
                {
                    FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "注液启动(" + iInfusionPulse + ")");
                    i_mRes = MyModbusFun.Shove(iInfusionPulse,1);
                    if (-2 == i_mRes)
                        throw new Exception("收到退出消息");
                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Machine_SlopDown == 1)
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "注液启动(" + iInfusionPulse + ")");
                        i_mRes = MyModbusFun.AbsShove(iInfusionPulse, 1);
                        if (-2 == i_mRes)
                            throw new Exception("收到退出消息");
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "注液启动(" + iInfusionPulse + ")");
                        i_mRes = MyModbusFun.Shove(iInfusionPulse,1);
                        if (-2 == i_mRes)
                            throw new Exception("收到退出消息");
                    }
                }
            }


            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "注液完成");


            goto label12;

        //移动到天平位
        label13:
            if (FADM_Auto.Drip._b_dripStop)
            {
                FADM_Object.Communal._b_stop = true;
            }
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找天平位");
            //if ((Lib_Card.Configure.Parameter.Machine_Type == 0 && Lib_Card.Configure.Parameter.Machine_Type_Lv == 0) || Lib_Card.Configure.Parameter.Machine_Type == 1)
            {
                //富士伺服在下面判断 天平状态 原有不动 绿维的放在上面 并且置位 是否回原点

                //判断是否异常
                FADM_Object.Communal.BalanceState("滴液");
            }

            //Lib_SerialPort.Balance.METTLER.bZeroSign = true;
            i_mRes = MyModbusFun.TargetMove(2, 0, 0);
            if (-2 == i_mRes)
                throw new Exception("收到退出消息");

            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达天平位");

            //if (FADM_Object.Communal._b_isZero)
            //{
            //    while (true)
            //    {
            //        //判断是否成功清零
            //        if (Lib_SerialPort.Balance.METTLER._s_balanceValue == 0.0)
            //        {
            //            break;
            //        }
            //        else
            //        {
            //            //再次发调零
            //            Lib_SerialPort.Balance.METTLER.bZeroSign = true;
            //        }
            //        Thread.Sleep(1);
            //    }
            //}

            //记录初始天平读数
            double d_blBalanceValue3 = SteBalance();
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "天平读数：" + d_blBalanceValue3);

            //验证
            if (FADM_Auto.Drip._b_dripStop)
            {
                FADM_Object.Communal._b_stop = true;
            }
            if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
            {
                iInfusionPulse = i_adjust;
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "验证启动(" + iInfusionPulse + ")");
                i_mRes = MyModbusFun.Shove(iInfusionPulse, 0);
                if (-1 == i_mRes)
                    throw new Exception("驱动异常");
                else if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
            }
            else
            {
                iInfusionPulse = i_adjust;
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "验证启动(" + iInfusionPulse + ")");
                i_mRes = MyModbusFun.Shove(iInfusionPulse,1);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
            }
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "验证完成");

            //读取验证重量
            sArg sArg = new sArg();
            sArg._i_minBottleNo = i_minBottleNo;
            sArg._d_blBalanceValue3 = d_blBalanceValue3;
            sArg._s_syringeType = s_syringeType;
            thread = new Thread(WaitBalance);
            thread.Start(sArg);



            //判断当前母液是否滴完
            if (0 < dic_pulse.Count)
            {
                goto label9;
            }

            //移动到母液瓶
            if (FADM_Auto.Drip._b_dripStop)
            {
                FADM_Object.Communal._b_stop = true;
            }
            if (i_minBottleNo == 999)
            {
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找放针母液瓶");
                FADM_Object.Communal._i_optBottleNum = i_minBottleNo;
                i_mRes = MyModbusFun.TargetMove(11, 0, 0);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");

                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达放针母液瓶");

            }
            else
            {
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "寻找" + i_minBottleNo + "号母液瓶");
                FADM_Object.Communal._i_optBottleNum = i_minBottleNo;
                i_mRes = MyModbusFun.TargetMove(0, i_minBottleNo, 0);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "抵达" + i_minBottleNo + "号母液瓶");
            }

            //放针
            if (FADM_Auto.Drip._b_dripStop)
            {
                FADM_Object.Communal._b_stop = true;
            }
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", i_minBottleNo + "号母液瓶放针启动");

            if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
            {
                i_mRes = MyModbusFun.Put(0);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
            }
            else
            {
                i_mRes = MyModbusFun.Put(1);
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
            }
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", i_minBottleNo + "号母液瓶放针完成");
            Communal._b_isGetSyringes = false;
            if (null != thread)
                thread.Join();
            for (int i = 0; i < lis_data.Count; i++)
            {
                //对应值复称值写入
                directoryReturn.Add(lis_data[i], _d_reviewBalance);
            }
            return 0;
        }

        public struct sArg
        {
            public string _s_syringeType;//针筒类型
            public int _i_minBottleNo;//母液瓶号
            public double _d_blBalanceValue3;//天平初始读数
        }

        private static void WaitBalance(object o)
        {
            sArg sArg = (sArg)o;
            double d_temp = sArg._d_blBalanceValue3;
            int i_minBottleNo = sArg._i_minBottleNo;
            string s_syringeType=sArg._s_syringeType;
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "天平稳定读数启动");
            double d_w = SteBalance();
            if (Math.Abs(d_w - d_temp) > 20)
            {
                d_temp = d_w;
                FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "天平复称异常，把实际重量置为0");
            }
            double d_blWeight = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", d_w - d_temp)) : Convert.ToDouble(string.Format("{0:F3}", d_w - d_temp));
            FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "天平稳定读数：" + d_w + ",实际重量：" + d_blWeight);
            _d_reviewBalance = d_blWeight;

            //母液瓶扣减
            double dblRErr = 0;
            if ("小针筒" == s_syringeType || "Little Syringe" == s_syringeType)
                dblRErr = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", d_blWeight - Lib_Card.Configure.Parameter.Correcting_S_Weight)) : Convert.ToDouble(string.Format("{0:F3}", d_blWeight - Lib_Card.Configure.Parameter.Correcting_S_Weight));
            else
                dblRErr = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", d_blWeight - Lib_Card.Configure.Parameter.Correcting_B_Weight)) : Convert.ToDouble(string.Format("{0:F3}", d_blWeight - Lib_Card.Configure.Parameter.Correcting_B_Weight));
            ;
            if (System.Math.Abs(dblRErr) > Lib_Card.Configure.Parameter.Other_AErr_Drip)
            {
                //母液瓶扣减
                FADM_Object.Communal._fadmSqlserver.ReviseData(
                    "UPDATE bottle_details SET CurrentWeight = CurrentWeight - " + d_blWeight + ",  AdjustSuccess = 0 " +
                    "WHERE BottleNum = '" + i_minBottleNo + "';");
            }
            else
            {
                //母液瓶扣减
                FADM_Object.Communal._fadmSqlserver.ReviseData(
                    "UPDATE bottle_details SET CurrentWeight = CurrentWeight - " + d_blWeight + " ,  AdjustSuccess = 1 " +
                    "WHERE BottleNum = '" + i_minBottleNo + "';");
            }
        }


        //天平稳定数据
        public static double SteBalance()
        {
            if (Lib_Card.Configure.Parameter.Machine_Type == 0)
            {
            label15:
                Thread.Sleep(Convert.ToInt32(Lib_Card.Configure.Parameter.Delay_Balance_Read * 1000));
                double d_blBalanceValue4 = Lib_Card.Configure.Parameter.Machine_BalanceType == 0 ? Convert.ToDouble(string.Format("{0:F2}", FADM_Object.Communal.dBalanceValue)) : Convert.ToDouble(string.Format("{0:F3}", FADM_Object.Communal.dBalanceValue));
                Thread.Sleep(Convert.ToInt32(Lib_Card.Configure.Parameter.Delay_Balance_Read * 1000));
                double d_blBalanceValue5 = Lib_Card.Configure.Parameter.Machine_BalanceType == 0 ? Convert.ToDouble(string.Format("{0:F2}", FADM_Object.Communal.dBalanceValue)) : Convert.ToDouble(string.Format("{0:F3}", FADM_Object.Communal.dBalanceValue));


                double d_blDif = Lib_Card.Configure.Parameter.Machine_BalanceType == 0 ? Convert.ToDouble(string.Format("{0:F2}", d_blBalanceValue4 - d_blBalanceValue5)) : Convert.ToDouble(string.Format("{0:F3}", d_blBalanceValue4 - d_blBalanceValue5));
                d_blDif = d_blDif < 0 ? -d_blDif : d_blDif;

                if (d_blDif > Lib_Card.Configure.Parameter.Other_Stable_Value || d_blBalanceValue4 > 6000 || d_blBalanceValue5 > 6000)
                {
                label4:
                    if (6666 == FADM_Object.Communal.dBalanceValue)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm("天平通讯异常,检查恢复后请点是", "滴液", false, 1);
                        else
                            new FADM_Object.MyAlarm("Abnormal communication on the balance,Click Yes after checking the recovery", "Drip", false, 1);
                        goto label4;
                    }
                    else if (7777 == FADM_Object.Communal.dBalanceValue)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm("开机未拿走废液桶,请先拿走废液桶，等待天平清零后重新放置废液桶，然后点确定", "滴液", false, 1);
                        else
                            new FADM_Object.MyAlarm("The balance was turned on but the waste liquid tank was not taken away,Please take away the waste liquid bucket first, wait for the balance to be cleared and re-place the waste liquid bucket, and then click OK", "Drip", false, 1);
                        goto label4;
                    }
                    else if (8888 == FADM_Object.Communal.dBalanceValue)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm("天平超下限,检查恢复后请点是", "滴液", false, 1);
                        else
                            new FADM_Object.MyAlarm("Balance exceeds the lower limit,Click Yes after checking the recovery", "Drip", false, 1);
                        goto label4;
                    }
                    else if (9999 == FADM_Object.Communal.dBalanceValue)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm("天平超上限,检查恢复后请点是", "滴液", false, 1);
                        else
                            new FADM_Object.MyAlarm("The balance exceeds the upper limit,Click Yes after checking the recovery", "Drip", false, 1);

                        goto label4;
                    }
                    //FADM_Object.Communal.BalanceState("滴液");
                    goto label15;
                }

                double d_blRRead = Lib_Card.Configure.Parameter.Machine_BalanceType == 0 ? Convert.ToDouble(string.Format("{0:F2}", (d_blBalanceValue4 + d_blBalanceValue5) / 2)) : Convert.ToDouble(string.Format("{0:F3}", (d_blBalanceValue4 + d_blBalanceValue5) / 2));

                return d_blRRead;
            }
            else
            {
            label15:
                Thread.Sleep(Convert.ToInt32(Lib_Card.Configure.Parameter.Delay_Balance_Read * 1000));
                double d_blBalanceValue4 = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", FADM_Object.Communal._s_balanceValue)) : Convert.ToDouble(string.Format("{0:F3}", FADM_Object.Communal._s_balanceValue));
                Thread.Sleep(Convert.ToInt32(Lib_Card.Configure.Parameter.Delay_Balance_Read * 1000));
                double d_blBalanceValue5 = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", FADM_Object.Communal._s_balanceValue)) : Convert.ToDouble(string.Format("{0:F3}", FADM_Object.Communal._s_balanceValue));


                double d_blDif = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", d_blBalanceValue4 - d_blBalanceValue5)) : Convert.ToDouble(string.Format("{0:F3}", d_blBalanceValue4 - d_blBalanceValue5));
                d_blDif = d_blDif < 0 ? -d_blDif : d_blDif;

                if (d_blDif > Lib_Card.Configure.Parameter.Other_Stable_Value || d_blBalanceValue4 > 6000 || d_blBalanceValue5 > 6000)
                {
                label4:
                    if ("6666" == FADM_Object.Communal._s_balanceValue)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm("天平通讯异常,检查恢复后请点是", "滴液", false, 1);
                        else
                            new FADM_Object.MyAlarm("Abnormal communication on the balance,Click Yes after checking the recovery", "Drip", false, 1);
                        goto label4;
                    }
                    else if ("7777" == FADM_Object.Communal._s_balanceValue)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm("开机未拿走废液桶,请先拿走废液桶，等待天平清零后重新放置废液桶，然后点确定", "滴液", false, 1);
                        else
                            new FADM_Object.MyAlarm("The balance was turned on but the waste liquid tank was not taken away,Please take away the waste liquid bucket first, wait for the balance to be cleared and re-place the waste liquid bucket, and then click OK", "Drip", false, 1);
                        goto label4;
                    }
                    else if ("8888" == FADM_Object.Communal._s_balanceValue)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm("天平超下限,检查恢复后请点是", "滴液", false, 1);
                        else
                            new FADM_Object.MyAlarm("Balance exceeds the lower limit,Click Yes after checking the recovery", "Drip", false, 1);
                        goto label4;
                    }
                    else if ("9999" == FADM_Object.Communal._s_balanceValue)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            new FADM_Object.MyAlarm("天平超上限,检查恢复后请点是", "滴液", false, 1);
                        else
                            new FADM_Object.MyAlarm("The balance exceeds the upper limit,Click Yes after checking the recovery", "Drip", false, 1);

                        goto label4;
                    }
                    //FADM_Object.Communal.BalanceState("滴液");
                    goto label15;
                }

                double d_blRRead = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? Convert.ToDouble(string.Format("{0:F2}", (d_blBalanceValue4 + d_blBalanceValue5) / 2)) : Convert.ToDouble(string.Format("{0:F3}", (d_blBalanceValue4 + d_blBalanceValue5) / 2));

                return d_blRRead;
            }
        }


        //移动到天平前，判断天平状态 
        public static void BalanceState(string s)
        {
            
                int i_mRes = MyModbusFun.CheckBalance();//检查天平
                if (-2 == i_mRes)
                    throw new Exception("收到退出消息");
            
        }
    }
}
