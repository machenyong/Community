using Community.Common;
using Community.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common
{
    public class NLogHelper : INLogHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<NLogHelper> _logger;
        //每创建一个Logger都会有一定的性能损耗，所以定义静态变量
        private static Logger logger;

        #region 初始化
        public NLogHelper(IHttpContextAccessor httpContextAccessor, ILogger<NLogHelper> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        static NLogHelper()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        #endregion

        #region Info 记录日志
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            logger.Info(msg);
        }
        #endregion

        #region Error 记录错误日志
        /// <summary>
        /// 记录错误日志
        /// </summary>

        public static void Error(string msg, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Error(msg);
            }
            else
            {
                logger.Error(ex, msg);
            }
        }
        public void LogError(Exception ex)
        {
            //日志消息
            LogMessage logMessage = new LogMessage();
            //IP
            logMessage.IpAddress = _httpContextAccessor.HttpContext.Request.Host.Host;
            //是否为内部异常
            if (ex.InnerException != null)
            {
                logMessage.LogInfo = ex.InnerException.Message;
            }
            else
            {
                logMessage.LogInfo = ex.Message;
            }
            //跟踪信息
            logMessage.StackTrace = ex.StackTrace;
            //操作人
            logMessage.OperationName = "admin";
            //记录错误日志
            _logger.LogError(ErrorFormat(logMessage));
        }
        /// <summary>
        /// 生成报错信息
        /// </summary>
        /// <param name="logMessage"></param>
        /// <returns></returns>
        public static string ErrorFormat(LogMessage logMessage)
        {
            StringBuilder strError = new StringBuilder();
            strError.Append("操作人: " + logMessage.OperationName + "\r\n");
            strError.Append("Ip:" + logMessage.IpAddress + "\r\n");
            strError.Append("错误内容: " + logMessage.LogInfo + "\r\n");
            strError.Append("跟踪:" + logMessage.StackTrace + "\r\n");
            return strError.ToString();
        }
        #endregion

        #region Debug 记录调试日志
        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(string msg, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Debug(msg);
            }
            else
            {
                logger.Debug(ex, msg);
            }
        }
        #endregion

        #region Fatal 严重致命错误日志
        /// <summary>
        /// 严重致命错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(String msg, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Fatal(msg);
            }
            else
            {
                logger.Fatal(ex, msg);
            }
        }
        #endregion

        #region Warn 警告日志
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(String msg)
        {
            try
            {
                logger.Warn(msg);
            }
            catch { }
        }
        #endregion

        #region 生成日志消息的构造函数
        /// <summary>
        /// 生成基础的日志消息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static LogMessage GetLogMessage(HttpContext context)
        {
            //日志消息
            LogMessage loggerMessage = new LogMessage()
            {
                IpAddress = context.Request.Host.ToString(),
                LogInfo = "",
                OperationName = "",
                RequestBody = context.Request.Body.ToString(),
                RequestType = context.Request.Method,
                RequestUrl = context.Request.Path,
                StackTrace = "",
                StatusCode = context.Response.StatusCode.ToString()
            };
            return loggerMessage;
        }
        /// <summary>
        /// 生成基础日志
        /// </summary>
        /// <param name="loggerMessage"></param>
        public static string GetLog(LogMessage loggerMessage)
        {
            string message =
                "IP:" + loggerMessage.IpAddress + Environment.NewLine +
                "请求体:" + loggerMessage.RequestBody + Environment.NewLine +
                "请求路径:" + loggerMessage.RequestUrl + Environment.NewLine +
                "请求类型:" + loggerMessage.RequestType + Environment.NewLine +
                "返回结果:" + loggerMessage.StatusCode + Environment.NewLine;
            return message;
        }
        #endregion

        }
}
