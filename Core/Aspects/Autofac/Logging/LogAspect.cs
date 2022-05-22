using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Core.Aspects.Autofac.Logging
{
    /// <summary>
    /// LogAspect
    /// </summary>
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogAspect(Type loggerService = null)
        {
            if (loggerService == null)
            {
                loggerService = typeof(PostgreSqlLogger);
            }
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new ArgumentException(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)ServiceTool.ServiceProvider.GetService(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase?.Info(GetLogDetail(invocation));
        }

        private string GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name,
                });
            }
            var logDetail = new LogDetail
            {
                FullName = invocation.TargetType.FullName,
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                UserId = _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.NameIdentifier).ToInt32(),
                DateTime = DateTime.Now.ToString()
            };
            return JsonConvert.SerializeObject(logDetail);
        }
    }
}
