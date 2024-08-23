namespace Lib_Card.ADT8940D1.OutPut.Turntable
{
    /// <summary>
    /// 转盘上下
    /// </summary>
    public abstract class Turntable
    {
        /// <summary>
        /// 转盘上
        /// 异常：
        ///     1：转盘正在运行
        ///     2：加水正在运行
        ///     3：转盘上超时
        /// </summary>
        /// <returns>0：正常；-1：异常；</returns>
        public abstract int Turntable_Up();

        /// <summary>
        /// 转盘下
        /// 异常：
        ///     1：转盘正在运行
        ///     2：转盘下超时
        /// </summary>
        /// <returns>0：正常；-1：异常；</returns>
        public abstract int Turntable_Down();
    }
}
