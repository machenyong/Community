using Community.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using API;
using ServiceStack.Redis;
using RabbitMQ.Client;
using Community.Model;

namespace API.Controllers
{

    [ApiController]
    public class MqController : ControllerBase
    {

        //秒杀Redis
        [Route("api/Mq/SeckillRequest")]
        [HttpGet]
        public string SeckillRequest()
        {
            try
            {
                using (RedisClient redisClient = new RedisClient("127.0.0.1", 6379, null, 1))
                {
                    Random random = new Random();
                    int b = random.Next(99, 499);
                    Thread.Sleep(b);
                    int a = redisClient.Get<int>("r1");

                    if (a <= 0)
                    {
                        return "秒杀失败";
                    }
                    else
                    {
                        redisClient.Decr("r1");
                        redisClient.AddItemToList("user", "第" + a + "个人抢成功了");
                        return "秒杀成功";
                    }
                }
            }
            catch (Exception)
            {
                return "秒杀失败!";
            }

        }


        //入队
        [Route("api/Mq/order")]
        [HttpGet]
        public void order(int UserId=1,int GoodsId=1,decimal price=21)
        {
            //存的数据
            ParameterModel parameter = new ParameterModel()
            {
                GoodsId = GoodsId,
                UserId = UserId,
                Price=price,
            };

            Thread.Sleep(1);
            //消息
            string message = JsonConvert.SerializeObject(parameter);
            //编码
            var body = Encoding.UTF8.GetBytes(message);
            //发布一条消息路由键必须小于255字节。
            MQClass.conn.CreateModel().BasicPublish(exchange: "", routingKey: "hi", basicProperties: null, body: body);

            //直接调用出队方法
            Receiver();
        }

        //出队
        [Route("api/Mq/Receiver")]
        [HttpGet]
        public string Receiver()
        {
            //定义变量默认为true
            bool i = true;
            //数据
            string data1 = "";

            //声明一个队列（这个队列必须和发布者队列一致）
            MQClass.conn.CreateModel().QueueDeclare(queue: "hi", durable: false, exclusive: false, autoDelete: false, arguments: null);
            //循环
            while (i)
            {
                //获取RabbitMq一条数据
                BasicGetResult result = MQClass.conn.CreateModel().BasicGet("hi", true);
                //判断是否为空
                if (result != null)
                {
                    Add(result);
                }
                else
                {
                    i = false;
                }
                Thread.Sleep(500);
            }
            return data1;
        }


        [Route("api/Mq/Add")]
        [HttpGet]
        public void Add(BasicGetResult basic)
        {
            
        }
    }
}
