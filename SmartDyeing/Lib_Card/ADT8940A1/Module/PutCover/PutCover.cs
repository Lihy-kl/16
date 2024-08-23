using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Card.ADT8940A1.Module.PutCover
{

    public abstract class PutCover
    {
        /// <summary>
        /// 放盖
        /// 异常：
        ///     1：Z轴正在运行
        ///     2：Z轴正限位已通
        ///     3：X轴正在运行
        ///     4：X轴伺服器报警
        ///     5：Y轴正在运行
        ///     6：Y轴伺服器报警
        ///     7：气缸未在上限位
        ///     8：气缸下超时
        ///     9：气缸上超时
        ///    10：脉冲计算异常
        ///    11：接液盘收回超时
        ///    12：接液盘未收回
        ///    13：抓手A打开超时
        ///    14：抓手B打开超时
        /// </summary>
        /// <param name="iCylinderVersion">0：单控上下气缸；1：双控上下气缸</param>
        /// <returns>0：正常；-1：异常；-2；收到退出消息</returns>
        public abstract int Put(int iCylinderVersion);
    }
}
