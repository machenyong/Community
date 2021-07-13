using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Common
{
    public class LogMessage
    {
        /// <summary>
        /// IP
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 日志信息/错误内容
        /// </summary>
        public string LogInfo { get; set; }
        /// <summary>
        /// 跟踪信息
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestType { get; set; }
        /// <summary>
        /// 返回状态码
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// 请求路径
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 请求体
        /// </summary>
        public string RequestBody { get; set; }

        public int MyProperty { get; set; }
    }
}
