using Autofac;
using Business.Constants;
using Business.DependencyResolvers;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace Business
{
    public partial class BusinessStartup
    {
        protected readonly IHostEnvironment HostEnvironment;
        public IConfiguration Configuration { get; }

        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddDependencyResolvers(Configuration, new ICoreModule[]
            {
                new CoreModule()
            });

            services.AddSingleton<ConfigurationManager>();

            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            services.AddAutoMapper(typeof(ConfigurationManager));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()?.GetName();
            };
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddDbContext<ProjectDbContext>();
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddDbContext<ProjectDbContext>();
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddDbContext<ProjectDbContext>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        }
    }
}
