namespace Lib_Card.ADT8940D1.OutPut.Water
{
    /// <summary>
    /// 加水
    /// </summary>
    public abstract class Water
    {
        /// <summary>
        /// 加水打开
        /// 异常：
        ///     1：转盘正在运行
        /// </summary>
        /// <returns>0：正常；-1：异常；</returns>
        public abstract int Water_On();

        /// <summary>
        /// 加水关闭
        /// </summary>
        /// <returns>0：正常；-1：异常；</returns>
        public abstract int Water_Off();
    }
}
