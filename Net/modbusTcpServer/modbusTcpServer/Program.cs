using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NModbus;
using NModbus.Data;

namespace modbusTcpServer
{
    internal static class Utility
    {
        internal static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int startIndex, int size)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var enumerable = source as T[] ?? source.ToArray();
            int num = enumerable.Count();

            if (startIndex < 0 || num < startIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (size < 0 || startIndex + size > num)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            return enumerable.Skip(startIndex).Take(size);
        }
    }
    /// <summary>
    /// A simple implementation of the point source. All registers are 
    /// </summary>
    /// <typeparam name="TPoint"></typeparam>
    internal class MyPointSource<TPoint> : IPointSource<TPoint>
    {
        //Only create this if referenced.
        private readonly Lazy<TPoint[]> _points;

        private readonly object _syncRoot = new object();

        public MyPointSource(ushort size)
        {
            _points = new Lazy<TPoint[]>(() => new TPoint[size]);
        }

        public TPoint[] ReadPoints(ushort startAddress, ushort numberOfPoints)
        {
            lock (_syncRoot)
            {
                return _points.Value
                    .Slice(startAddress, numberOfPoints)
                    .ToArray();
            }
        }

        public void WritePoints(ushort startAddress, TPoint[] points)
        {
            lock (_syncRoot)
            {
                for (ushort index = 0; index < points.Length; index++)
                {
                    try
                    {
                        _points.Value[startAddress + index] = points[index];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
    internal class MySlaveDataStore : ISlaveDataStore
    {
        private readonly IPointSource<ushort> _holdingRegisters = null;
        private readonly IPointSource<ushort> _inputRegisters = null;
        private readonly IPointSource<bool> _coilDiscretes = null;
        private readonly IPointSource<bool> _coilInputs = null;

        public IPointSource<ushort> HoldingRegisters => _holdingRegisters;

        public IPointSource<ushort> InputRegisters => _inputRegisters;

        public IPointSource<bool> CoilDiscretes => _coilDiscretes;

        public IPointSource<bool> CoilInputs => _coilInputs;

        public MySlaveDataStore()
        {
            _holdingRegisters = new MyPointSource<ushort>(ushort.MaxValue);
            _inputRegisters = new MyPointSource<ushort>(ushort.MaxValue);
            _coilDiscretes = new MyPointSource<bool>(ushort.MaxValue);
            _coilInputs = new MyPointSource<bool>(ushort.MaxValue);
        }
        public MySlaveDataStore(ushort maxSize)
        {
            _holdingRegisters = new MyPointSource<ushort>(maxSize);
            _inputRegisters = new MyPointSource<ushort>(maxSize);
            _coilDiscretes = new MyPointSource<bool>(maxSize);
            _coilInputs = new MyPointSource<bool>(maxSize);
        }
    }
    class Program
    {
        static ushort _maxSize = ushort.Parse(ConfigurationManager.AppSettings.Get("maxSize"));
        static bool _enableAutoUpdate = ConfigurationManager.AppSettings.Get("enableAutoUpdate") == "1";
        static byte _maxDeviceId = byte.Parse(ConfigurationManager.AppSettings.Get("maxDeviceId"));
        static int _port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
        private static bool bValue = false;
        private static int _updateInterval = int.Parse(ConfigurationManager.AppSettings.Get("updateInterval"));
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                StartModbusTcpSlave();
            }).Start();

            //new Thread(() =>
            //{
            //    Thread.Sleep(2000);
            //    using (TcpClient client = new TcpClient("127.0.0.1", _port))
            //    {
            //        while (_enableAutoWriteCoil)
            //        {
            //            var factory = new ModbusFactory();

            //            IModbusMaster master = factory.CreateMaster(client);
                       
            //            bValue = !bValue;
            //            for (byte deviceId = 1; deviceId <= _maxDeviceId; deviceId++)
            //            {
            //                for (ushort addr = 0; addr < _maxSize; addr++)
            //                {
            //                    master.WriteMultipleCoils(deviceId, addr, new bool[] { bValue });
            //                }
            //            }

            //            Thread.Sleep(_updateInterval*1000);
            //        }
                    
            //    }
            //}).Start();
            //new Thread(() =>
            //{
            //    Thread.Sleep(2000);
            //    using (TcpClient client = new TcpClient("127.0.0.1", _port))
            //    {
            //        while (_enableAutoWriteHoldingRegister)
            //        {
            //            var factory = new ModbusFactory();

            //            IModbusMaster master = factory.CreateMaster(client);
  
            //            ushort sData = (ushort)new Random().Next(0, ushort.MaxValue);
            //            for (byte deviceId = 1; deviceId <= _maxDeviceId; deviceId++)
            //            {
            //                for (ushort addr = 0; addr < _maxSize; addr++)
            //                {
            //                    master.WriteMultipleRegisters(deviceId, addr, new ushort[] { sData });
            //                }
            //            }

            //            Thread.Sleep(_updateInterval);
            //        }

            //    }
            //}).Start();
            new Thread(() =>
            {
                if (!_enableAutoUpdate)
                {
                    return;
                }
                while (true)
                {
                    Thread.Sleep(_updateInterval * 1000);

                    bValue = !bValue;
                    ushort sData = (ushort)new Random().Next(0, short.MaxValue);
                    foreach (var slave in SlaveDataStores.Keys)
                    {
                        for (ushort addr = 0; addr < _maxSize; addr++)
                        {
                            SlaveDataStores[slave].CoilInputs.WritePoints(addr, new bool[]{bValue});
                            //master.WriteMultipleRegisters(deviceId, addr, new ushort[] { sData });
                        }
                        for (ushort addr = 0; addr < _maxSize; addr++)
                        {
                            SlaveDataStores[slave].CoilDiscretes.WritePoints(addr, new bool[] { bValue });
                        }
                        for (ushort addr = 0; addr < _maxSize; addr++)
                        {
                            SlaveDataStores[slave].HoldingRegisters.WritePoints(addr, new ushort[] { sData });
                        }
                        for (ushort addr = 0; addr < _maxSize; addr++)
                        {
                            SlaveDataStores[slave].InputRegisters.WritePoints(addr, new ushort[] { sData });
                        }
                    }

                    Console.WriteLine($"自动更新完毕, Coil Status【{bValue}】, Input Coil【{bValue}】, Holding Register【{sData}】, Input Register 【{sData}】");
                }
            }).Start();

            Console.WriteLine($"监听端口为 【{_port}】");
            Console.WriteLine($"支持的设备为 【1 - { _maxDeviceId}】");
            Console.WriteLine($"寄存器最大个数为 【{_maxSize}】");
            if (_enableAutoUpdate)
            {
                Console.WriteLine($"启用自动更新成功， 更新间隔为 【{_updateInterval}秒】");
            }
            Console.WriteLine("服务启动成功，按任意键退出服务...");
            Console.ReadKey();
        }
        public static Dictionary<byte, MySlaveDataStore> SlaveDataStores = new Dictionary<byte, MySlaveDataStore>();
        /// <summary>
        ///     Simple Modbus TCP slave example.
        /// </summary>
        public static void StartModbusTcpSlave()
        {
            IPAddress address = new IPAddress(new byte[] { 0, 0, 0, 0 });

            // create and start the TCP slave
            TcpListener slaveTcpListener = new TcpListener(address, _port);
            slaveTcpListener.Start();

            IModbusFactory factory = new ModbusFactory();

            IModbusSlaveNetwork network = factory.CreateSlaveNetwork(slaveTcpListener);

            for (byte i = 1; i <= _maxDeviceId; i++)
            {
                var dataStore = new MySlaveDataStore(_maxSize);
                IModbusSlave slave = factory.CreateSlave(i, dataStore);
                network.AddSlave(slave);
                SlaveDataStores.Add(i, dataStore);
            }

            network.ListenAsync().GetAwaiter().GetResult();

            // prevent the main thread from exiting
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
