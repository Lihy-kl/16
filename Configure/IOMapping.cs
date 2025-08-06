using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configure
{
    public class IOMapping
    {
        #region 输入

        #region X轴

        /// <summary>
        /// X轴A相反馈脉冲
        /// </summary>
        public static int InPut_X_APulse { get; set; }

        /// <summary>
        /// X轴B相反馈脉冲
        /// </summary>
        public static int InPut_X_BPulse { get; set; }


        /// <summary>
        /// X轴反限位
        /// </summary>
        public static int InPut_X_Reverse { get; set; }

        /// <summary>
        /// X轴正限位
        /// </summary>
        public static int InPut_X_Corotation { get; set; }

        /// <summary>
        /// X轴原点
        /// </summary>
        public static int InPut_X_Origin { get; set; }

        /// <summary>
        /// X轴准备信号
        /// </summary>
        public static int InPut_X_Ready { get; set; }

        /// <summary>
        /// X轴报警信号
        /// </summary>
        public static int InPut_X_Alarm { get; set; }

        #endregion

        #region Y轴

        /// <summary>
        /// Y轴A相反馈脉冲
        /// </summary>
        public static int InPut_Y_APulse { get; set; }

        /// <summary>
        /// Y轴B相反馈脉冲
        /// </summary>
        public static int InPut_Y_BPulse { get; set; }

        /// <summary>
        /// Y轴反限位
        /// </summary>
        public static int InPut_Y_Reverse { get; set; }

        /// <summary>
        /// Y轴正限位
        /// </summary>
        public static int InPut_Y_Corotation { get; set; }

        /// <summary>
        /// Y轴原点
        /// </summary>
        public static int InPut_Y_Origin { get; set; }

        /// <summary>
        /// Y轴准备
        /// </summary>
        public static int InPut_Y_Ready { get; set; }

        /// <summary>
        /// Y轴报警
        /// </summary>
        public static int InPut_Y_Alarm { get; set; }
        #endregion

        #region Z轴

        /// <summary>
        /// Z轴A相反馈脉冲
        /// </summary>
        public static int InPut_Z_APulse { get; set; }

        /// <summary>
        /// Z轴B相反馈脉冲
        /// </summary>
        public static int InPut_Z_BPulse { get; set; }

        /// <summary>
        /// Z轴正限位
        /// </summary>
        public static int InPut_Z_Corotation { get; set; }

        /// <summary>
        /// Z轴反限位
        /// </summary>
        public static int InPut_Z_Reverse { get; set; }

        /// <summary>
        /// Z轴准备
        /// </summary>
        public static int InPut_Z_Ready { get; set; }

        /// <summary>
        /// Z轴原点
        /// </summary>
        public static int InPut_Z_Origin { get; set; }

        /// <summary>
        /// Z轴报警
        /// </summary>
        public static int InPut_Z_Alarm { get; set; }

        #endregion

        #region 上下气缸
        /// <summary>
        /// 气缸上到位信号
        /// </summary>
        public static int InPut_Cylinder_Up { get; set; }

        /// <summary>
        /// 气缸下到位信号
        /// </summary>
        public static int InPut_Cylinder_Down { get; set; }

        /// <summary>
        /// 气缸中间信号
        /// </summary>
        public static int InPut_Cylinder_Mid { get; set; }
        #endregion

        #region 抓手
        /// <summary>
        /// 抓手A松开到位信号
        /// </summary>
        public static int InPut_Tongs_A { get; set; }

        /// <summary>
        /// 抓手B松开到位信号
        /// </summary>
        public static int InPut_Tongs_B { get; set; }

        /// <summary>
        /// 针筒感应器信号
        /// </summary>
        public static int InPut_Syringe { get; set; }

        #endregion

        #region 接液盘
        /// <summary>
        /// 接液盘收回到位信号
        /// </summary>
        public static int InPut_Tray_In { get; set; }

        /// <summary>
        /// 接液盘伸出到位信号
        /// </summary>
        public static int InPut_Tray_Out { get; set; }
        #endregion

        #region 光幕
        /// <summary>
        /// 光幕A阻挡信号
        /// </summary>
        public static int InPut_Sunx_A { get; set; }

        /// <summary>
        /// 光幕B阻挡信号
        /// </summary>
        public static int InPut_Sunx_B { get; set; }

        /// <summary>
        /// 后光幕
        /// </summary>
        public static int InPut_Back { get; set; }

        #endregion

        #region 按钮信号
        /// <summary>
        /// 急停按钮信号
        /// </summary>
        public static int InPut_Stop { get; set; }

        #endregion



        #region 泄压气缸

        /// <summary>
        /// 泄压下到位
        /// </summary>
        public static int InPut_Decompression_Down { get; set; }

        /// <summary>
        /// 泄压上到位
        /// </summary>
        public static int InPut_Decompression_Up { get; set; }

        #endregion

        #region 阻挡

        /// <summary>
        /// 阻挡出限位
        /// </summary>
        public static int InPut_Block_Out { get; set; }

        /// <summary>
        /// 阻挡回限位
        /// </summary>
        public static int InPut_Block_In { get; set; }

        /// <summary>
        /// 气缸慢速中限位
        /// </summary>
        public static int InPut_Slow_Cylinder_Mid { get; set; }

        /// <summary>
        /// 气缸阻挡限位
        /// </summary>
        public static int InPut_Cylinder_Block { get; set; }



        #endregion

        #region 撑盖
        public static int InPut_SupportCover { get; set; }

        #endregion

        #region 抓手泄压

        /// <summary>
        /// 抓手A泄压
        /// </summary>
        public static int InPut_Tongs_A_Decompression { get; set; }

        /// <summary>
        /// 抓手B泄压
        /// </summary>
        public static int InPut_Tongs_B_Decompression { get; set; }



        #endregion

        #endregion

        #region 输出

        #region X轴

        /// <summary>
        /// X轴脉冲
        /// </summary>
        public static int OutPut_X_Pulse { get; set; }

        /// <summary>
        /// X轴方向
        /// </summary>
        public static int OutPut_X_Direction { get; set; }

        /// <summary>
        /// X轴矢能
        /// </summary>
        public static int OutPut_X_Power { get; set; }

        /// <summary>
        /// X轴复位
        /// </summary>
        public static int OutPut_X_Reset { get; set; }
        #endregion

        #region Y轴
        /// <summary>
        /// Y轴脉冲
        /// </summary>
        public static int OutPut_Y_Pulse { get; set; }

        /// <summary>
        /// Y轴方向
        /// </summary>
        public static int OutPut_Y_Direction { get; set; }

        /// <summary>
        /// Y轴矢能
        /// </summary>
        public static int OutPut_Y_Power { get; set; }

        /// <summary>
        /// Y轴复位
        /// </summary>
        public static int OutPut_Y_Reset { get; set; }

        #endregion

        #region Z轴
        /// <summary>
        /// Z轴脉冲
        /// </summary>
        public static int OutPut_Z_Pulse { get; set; }

        /// <summary>
        /// Z轴方向
        /// </summary>
        public static int OutPut_Z_Direction { get; set; }

        /// <summary>
        /// Z轴矢能
        /// </summary>
        public static int OutPut_Z_Power { get; set; }

        /// <summary>
        /// Z轴复位
        /// </summary>
        public static int OutPut_Z_Reset { get; set; }
        #endregion


        #region 上下气缸
        /// <summary>
        /// 气缸下
        /// </summary>
        public static int OutPut_Cylinder_Down { get; set; }

        /// <summary>
        /// 气缸上
        /// </summary>
        public static int OutPut_Cylinder_Up { get; set; }

        #endregion

        #region 抓手
        /// <summary>
        /// 抓手关闭
        /// </summary>
        public static int OutPut_TongsOff { get; set; }

        /// <summary>
        /// 抓手打开
        /// </summary>
        public static int OutPut_TongsOn { get; set; }

        #endregion

        #region 接液盘
        /// <summary>
        /// 接液盘伸出
        /// </summary>
        public static int OutPut_Tray { get; set; }
        #endregion

        #region 停止搅拌
        /// <summary>
        /// 停止搅拌打开
        /// </summary>
        public static int OutPut_Blender { get; set; }
        #endregion

        #region 加水
        /// <summary>
        /// 加水打开
        /// </summary>
        public static int OutPut_Water { get; set; }
        #endregion

        #region 废液回收
        /// <summary>
        /// 废液回收打开
        /// </summary>
        public static int OutPut_Waste { get; set; }
        #endregion

        #region 蜂鸣器(报警)
        /// <summary>
        /// 蜂鸣器打开
        /// </summary>
        public static int OutPut_Buzzer { get; set; }
        #endregion

        #region 泄压气缸
        /// <summary>
        /// 泄压气缸下
        /// </summary>
        public static int OutPut_Decompression { get; set; }

        #endregion

        #region 红灯
        /// <summary>
        /// 红灯
        /// </summary>
        public static int OutPut_Red { get; set; }

        #endregion

        #region 绿灯
        /// <summary>
        /// 绿灯
        /// </summary>
        public static int OutPut_Green { get; set; }

        #endregion

        #region 阻挡
        /// <summary>
        /// 阻挡出
        /// </summary>
        public static int OutPut_Block_Out { get; set; }

        /// <summary>
        /// 阻挡回
        /// </summary>
        public static int OutPut_Block_In { get; set; }

        /// <summary>
        /// 气缸慢下阀
        /// </summary>
        public static int OutPut_Slow_Cylinder { get; set; }

        #endregion

        #region 洗针
        /// <summary>
        /// 洗针进水阀
        /// </summary>
        public static int OutPut_Wash_In { get; set; }

        /// <summary>
        /// 洗针排水阀
        /// </summary>
        public static int OutPut_Wash_Out { get; set; }

        /// <summary>
        /// 洗针吹气阀
        /// </summary>
        public static int OutPut_Wash_Blow { get; set; }

        #endregion

        #region 抓手泄压

        /// <summary>
        /// 抓手泄压阀
        /// </summary>
        public static int OutPut_Tongs_Decompression { get; set; }


        #endregion

        #endregion
    }
}
