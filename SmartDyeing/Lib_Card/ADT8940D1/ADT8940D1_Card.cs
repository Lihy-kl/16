using Demo;
using Lib_Card.ADT8940A1;
using System;

namespace Lib_Card.ADT8940D1
{

    public class ADT8940D1_Card : Base.Card
    {
        public override void CardInit()
        {
            int iRes = Adt8940a1m.adt8940a1_initial();
            switch (iRes)
            {
                case 0:
                    throw new Exception("未检查到板卡");

                case -1:
                    throw new Exception("未安装端口驱动程序");

                case -2:
                    throw new Exception("PCI桥存在故障");

                case -3:
                    throw new Exception("拨码开关设置重复");

                case -4:
                    throw new Exception("拨码开关读取异常");

                default:
                    //清除缓存
                    if (1 == Adt8940a1m.adt8940a1_reset_fifo(1))
                        throw new Exception("板卡清除缓存异常");
                    //设定脉冲工作方式和加速度
                    if (-1 != ADT8940D1_IO.Axis_T)
                    {
                        if (1 == Adt8940a1m.adt8940a1_set_pulse_mode(1, ADT8940D1_IO.Axis_T, 1, 0, 0))
                            throw new Exception("转盘轴设定脉冲工作方式异常");
                        if (1 == Adt8940a1m.adt8940a1_set_acc(1, ADT8940D1_IO.Axis_T, 40))
                            throw new Exception("转盘轴设定加速度异常");
                    }
                    break;
            }

        }

        public override int GetVersion()
        {
            int iRes = Adt8940a1m.adt8940a1_get_lib_version(0);
            return iRes;

        }

        public override int ReadInPut(int iInPutNo)
        {
            int iRes = Adt8940a1m.adt8940a1_read_bit(1, iInPutNo);
            if (0 == iRes)
                return 0;
            else if (1 == iRes)
                return 1;
            else
                return -1;

        }

        public override int ReadOutPut(int iOutPutNo)
        {
            int iRes = Adt8940a1m.adt8940a1_get_out(1, iOutPutNo);
            if (0 == iRes)
                return 0;
            else if (1 == iRes)
                return 1;
            else
                return -1;
        }

        public override int WriteOutPut(int iOutPutNo, int iStatus)
        {
            if (0 == Adt8940a1m.adt8940a1_write_bit(1, iOutPutNo, iStatus))
                return 0;
            return -1;
        }

        public override int ReadAxisStatus(int iAxisNo)
        {
            if (0 == Adt8940a1m.adt8940a1_get_status(1, iAxisNo, out int iStatus))
                return iStatus;
            return -1;
        }

        public override int ReadAxisSpeed(int iAxisNo, ref int iSpeed)
        {
            if (0 == Adt8940a1m.adt8940a1_get_speed(1, iAxisNo, out iSpeed))
                return 0;
            return -1;
        }

        public override int ReadAxisCommandPosition(int iAxisNo, ref int iPosition)
        {
            if (0 == Adt8940a1m.adt8940a1_get_command_pos(1, iAxisNo, out iPosition))
                return 0;
            return -1;
        }

        public override int ClearAxisPosition(int iAxisNo)
        {
            if (1 == Adt8940a1m.adt8940a1_set_command_pos(1, iAxisNo, 0))
                return -1;
            return 0;
        }

        public override int SetHomeMode(int iAxisNo, int iHomeDir, int iOffset)
        {
            if (0 != Adt8940a1m.adt8940a1_SetHomeMode_Ex(
                1, iAxisNo, iHomeDir, 0, 0, -1, 2000, 400, iOffset))
                return -1;
            return 0;
        }

        public override int SetHomeSpeed(int iAxisNo, HomeArg s_HomeArg)
        {
            if (0 != Adt8940a1m.adt8940a1_SetHomeSpeed_Ex(
                1, iAxisNo, s_HomeArg.Home_LSpeed, s_HomeArg.Home_HSpeed,
                s_HomeArg.Home_CSpeed, s_HomeArg.Home_USpeed, 200))
                return -1;
            return 0;
        }

        public override int GetHomeStatus(int iAxisNo)
        {
            return Adt8940a1m.adt8940a1_GetHomeStatus_Ex(1, iAxisNo);
        }

        public override int Home(int iAxisNo)
        {
            if (1 == Adt8940a1m.adt8940a1_HomeProcess_Ex(1, iAxisNo))
                return -1;
            return 0;

        }

        public override int RelativeMove(int iAxisNo, MoveArg s_MoveArg)
        {
            if (1 == Adt8940a1m.adt8940a1_symmetry_relative_move(
                1, iAxisNo, s_MoveArg.Pulse, s_MoveArg.LSpeed, s_MoveArg.HSpeed, s_MoveArg.Time))
                return -1;
            return 0;
        }

        public override int AbsoluteMove(int iAxisNo, MoveArg s_MoveArg)
        {
            if (1 == Adt8940a1m.adt8940a1_symmetry_absolute_move(
                1, iAxisNo, s_MoveArg.Pulse, s_MoveArg.LSpeed, s_MoveArg.HSpeed, s_MoveArg.Time))
                return -1;
            return 0;
        }

        public override int DecStop(int iAxisNo)
        {
            if (1 == Adt8940a1m.adt8940a1_dec_stop(1, iAxisNo))
                return -1;
            return 0;
        }

        public override int SuddnStop(int iAxisNo)
        {
            if (1 == Adt8940a1m.adt8940a1_sudden_stop(1, iAxisNo))
                return -1;
            return 0;
        }

        public override int ReadAxisActualPosition(int iAxisNo, ref int iPosition)
        {
            if (0 == Adt8940a1m.adt8940a1_get_actual_pos(1, iAxisNo, out iPosition))
                return 0;
            return -1;
        }

        public override int SetAxisActualPosition(int iAxisNo)
        {
            if (1 == Adt8940a1m.adt8940a1_set_actual_pos(1, iAxisNo, 0))
                return -1;
            return 0;
        }

        public override int SetAxisCommandPosition(int iAxisNo, int iPosition)
        {
            if (1 == Adt8940a1m.adt8940a1_set_command_pos(1, iAxisNo, iPosition))
                return -1;
            return 0;
           
        }
    }
}
