using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyModbus;

namespace SmartDyeing.FADM_Object
{
    public class TCPModBus
    {
        public TCPModBus() { }

        //
        public ModbusClient _modbusClient;

        //是否已连接
        public bool _b_Connect = false;

        //连接IP
        public string _s_IP = "192.168.1.2";
        //连接端口
        public int _i_port = 502;

        //重连次数
        int _i_count = 0;

        //连接
        public int Connect()
        {
            try
            {
                _modbusClient = new ModbusClient(_s_IP, _i_port);  // IP地址和端口
                _modbusClient.Connect(); // 连接到服务器
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        //关闭连接
        public int Disconnect()
        {
            _modbusClient.Disconnect(); // 断开连接
            return 0;
        }

        //重连
        public int ReConnect()
        {
            Lib_Log.Log.writeLogException("注意,重连!!!!!");
            Console.WriteLine("注意,重连!!!!!");
            try
            {
                _modbusClient.Disconnect(); // 断开连接
                _modbusClient.Connect(); // 连接到服务器
                Lib_Card.CardObject.bPLCStatus = true;
                return 0;
            }
            catch 
            {
                return -1; 
            }
        }


        //读寄存器数据
        //startingAddress读取寄存器开始地址；num读取寄存器数量；values返回寄存器的值

        public int Read(int startingAddress, int num, ref int[] values)
        {
            lock (this)
            {
                try
                {
                    //_modbusClient.Connect(); // 连接到服务器
                    values = _modbusClient.ReadHoldingRegisters(startingAddress, num);
                    //_modbusClient.Disconnect(); // 断开连接
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("读寄存器数据异常" + ex.Message);
                    return -1;
                }
            }
        }

        //写寄存器数据
        //startingAddress读取寄存器开始地址；values返回寄存器的值
        public int Write(int startingAddress, int[] values)
        {
            lock (this)
            {
            labRewrite:
                try
                {
                    //_modbusClient.Connect(); // 连接到服务器
                    _modbusClient.WriteMultipleRegisters(startingAddress, values);
                    // _modbusClient.Disconnect(); // 断开连接
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("写寄存器数据异常" + ex.Message);
                    goto labRewrite;
                    return -1;
                }
            }
        }

        public bool getConnect()
        {
            try
            {
                //bool _b_istrue =_modbusClient.Available(2000);
                return _modbusClient.Connected; 
            }
            catch
            {
                return false;
            }
        }


    }
}
