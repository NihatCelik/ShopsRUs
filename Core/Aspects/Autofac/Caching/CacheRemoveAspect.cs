using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    /// <summary>
    /// CacheRemoveAspect
    /// </summary>
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private readonly ICacheManager _cacheManager;
        public CacheRemoveAspect(string pattern = "")
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            if (string.IsNullOrEmpty(_pattern))
            {
                string targetTypeName = invocation.TargetType.Name;
                targetTypeName = targetTypeName.Replace("CommandHandler", string.Empty);
                targetTypeName = targetTypeName.Replace("Create", string.Empty);
                targetTypeName = targetTypeName.Replace("Update", string.Empty);
                targetTypeName = targetTypeName.Replace("Delete", string.Empty);
                _pattern = "Get" + targetTypeName;
            }
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
