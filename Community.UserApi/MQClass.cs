using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API
{
    public class MQClass
    {
        public static ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest", Port = 5672 };
        public static IConnection connection = null;
        public static IModel channel = null;
        public static object obj = new object();
        public static IConnection conn { get {
                if (connection == null)
                {
                    Console.WriteLine("测试"+ Thread.CurrentThread.ManagedThreadId.ToString("00"));
                    lock (obj)
                    {
                        if (connection == null)
                        {
                            Console.WriteLine("测试_1");
                            connection = factory.CreateConnection();
                            channel = connection.CreateModel();
                        }
                    }
                }
                return connection;
                
            }  }


    }
}
