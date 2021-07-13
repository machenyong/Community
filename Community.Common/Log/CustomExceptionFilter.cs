using Community.IRepository;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Common
{
    //自定义异常过滤器
    public class CustomExceptionFilter : IAsyncExceptionFilter
    {
        //NLogHelper的接口
        private readonly INLogHelper _logHelper;

        public CustomExceptionFilter(INLogHelper logHelper)
        {
            _logHelper = logHelper;
        }

        //异常异步进行
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理，则进行处理
            if (context.ExceptionHandled == false)
            {
                // 记录错误信息
                _logHelper.LogError(context.Exception);
                // 设置为true，表示异常已经被处理了，其它捕获异常的地方就不会再处理了
                context.ExceptionHandled = true;
            }
            //完成任务
            return Task.CompletedTask;
        }
    }
}
