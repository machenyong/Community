using Community.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common
{
    //日志中间件
    public class LogMiddleware
    {
        //请求委托(只读)
        private readonly RequestDelegate _next;
        //多个日志中间件
        private readonly ILogger<LogMiddleware> _logger;
        public LogMiddleware(RequestDelegate next,ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        //异步写入 任务 调用
        public async Task Invoke(HttpContext context)
        {
            //日志消息
            LogMessage loggerMessage = NLogHelper.GetLogMessage(context);
            //生成基本日志
            string message=NLogHelper.GetLog(loggerMessage);
            //记录日志
            NLogHelper.Info(message);
            //等待调用
            await _next.Invoke(context);
        }
    }
}
