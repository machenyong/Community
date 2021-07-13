using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common
{
    //日志中间件扩展
    public static class LogMiddlewareExtensions
    {
        //使用日志中间件
        public static IApplicationBuilder UseLog(this IApplicationBuilder builder)
        {
            //多个日志中间件
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
