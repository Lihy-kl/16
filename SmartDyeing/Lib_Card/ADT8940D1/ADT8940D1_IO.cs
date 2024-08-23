namespace Lib_Card.ADT8940D1
{
    public class ADT8940D1_IO
    {
        #region 输入

        #region 转盘
        /// <summary>
        /// 转盘原点信号
        /// </summary>
        public static int InPut_T_Origin { get; set; }

        /// <summary>
        /// 转盘上到位信号
        /// </summary>
        public static int InPut_T_Up { get; set; }

        /// <summary>
        /// 转盘下到位信号
        /// </summary>
        public static int InPut_T_Down { get; set; }

        #endregion

        #region 按钮
        /// <summary>
        /// 转盘回零按钮信号
        /// </summary>
        public static int InPut_T_Home { get; set; }

        /// <summary>
        /// 转盘下一杯按钮信号
        /// </summary>
        public static int InPut_T_Next { get; set; }

        /// <summary>
        /// 急停按钮信号
        /// </summary>
        public static int InPut_Stop { get; set; }

        #endregion

        #region 备用输入
        /// <summary>
        /// 备用输入1
        /// </summary>
        public static int InPut_Spare_1 { get; set; }

        /// <summary>
        /// 备用输入2
        /// </summary>
        public static int InPut_Spare_2 { get; set; }

        /// <summary>
        /// 备用输入3
        /// </summary>
        public static int InPut_Spare_3 { get; set; }

        /// <summary>
        /// 备用输入4
        /// </summary>
        public static int InPut_Spare_4 { get; set; }

        /// <summary>
        /// 备用输入5
        /// </summary>
        public static int InPut_Spare_5 { get; set; }

        /// <summary>
        /// 备用输入6
        /// </summary>
        public static int InPut_Spare_6 { get; set; }

        /// <summary>
        /// 备用输入7
        /// </summary>
        public static int InPut_Spare_7 { get; set; }

        /// <summary>
        /// 备用输入8
        /// </summary>
        public static int InPut_Spare_8 { get; set; }

        /// <summary>
        /// 备用输入9
        /// </summary>
        public static int InPut_Spare_9 { get; set; }

        /// <summary>
        /// 备用输入10
        /// </summary>
        public static int InPut_Spare_10 { get; set; }

        /// <summary>
        /// 备用输入11
        /// </summary>
        public static int InPut_Spare_11 { get; set; }

        /// <summary>
        /// 备用输入12
        /// </summary>
        public static int InPut_Spare_12 { get; set; }

        /// <summary>
        /// 备用输入13
        /// </summary>
        public static int InPut_Spare_13 { get; set; }

        /// <summary>
        /// 备用输入14
        /// </summary>
        public static int InPut_Spare_14 { get; set; }

        /// <summary>
        /// 备用输入15
        /// </summary>
        public static int InPut_Spare_15 { get; set; }

        /// <summary>
        /// 备用输入16
        /// </summary>
        public static int InPut_Spare_16 { get; set; }

        /// <summary>
        /// 备用输入17
        /// </summary>
        public static int InPut_Spare_17 { get; set; }
        #endregion

        #endregion

        #region 输出

        #region 转盘上下
        /// <summary>
        /// 转盘下
        /// </summary>
        public static int OutPut_Turntable { get; set; }
        #endregion

        #region 加水
        /// <summary>
        /// 加水
        /// </summary>
        public static int OutPut_Water { get; set; }
        #endregion

        #region 备用输出

        /// <summary>
        /// 备用输出1
        /// </summary>
        public static int OutPut_Spare_1 { get; set; }

        /// <summary>
        /// 备用输出2
        /// </summary>
        public static int OutPut_Spare_2 { get; set; }

        /// <summary>
        /// 备用输出3
        /// </summary>
        public static int OutPut_Spare_3 { get; set; }

        /// <summary>
        /// 备用输出4
        /// </summary>
        public static int OutPut_Spare_4 { get; set; }

        /// <summary>
        /// 备用输出5
        /// </summary>
        public static int OutPut_Spare_5 { get; set; }

        /// <summary>
        /// 备用输出6
        /// </summary>
        public static int OutPut_Spare_6 { get; set; }

        /// <summary>
        /// 备用输出7
        /// </summary>
        public static int OutPut_Spare_7 { get; set; }

        /// <summary>
        /// 备用输出8
        /// </summary>
        public static int OutPut_Spare_8 { get; set; }

        /// <summary>
        /// 备用输出9
        /// </summary>
        public static int OutPut_Spare_9 { get; set; }

        #endregion

        #endregion

        #region 轴

        #region 转盘轴

        /// <summary>
        /// 转盘轴
        /// </summary>
        public static int Axis_T { get; set; }

        #endregion

        #region 备用轴

        /// <summary>
        /// 备用轴1
        /// </summary>
        public static int Axis_Spare_1 { get; set; }

        /// <summary>
        /// 备用轴2
        /// </summary>
        public static int Axis_Spare_2 { get; set; }

        /// <summary>
        /// 备用轴3
        /// </summary>
        public static int Axis_Spare_3 { get; set; }

        #endregion

        #endregion
    }
}
