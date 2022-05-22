using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using FluentValidation;
using MediatR;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        private readonly ConfigurationManager _configuration;

        public AutofacBusinessModule()
        {
        }

        public AutofacBusinessModule(ConfigurationManager configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().AsClosedTypesOf(typeof(IValidator<>));

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().Where(t => t.FullName.StartsWith("Business.Adapters"));

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                            {
                                Selector = new AspectInterceptorSelector()
                            }).SingleInstance().InstancePerDependency();
        }
    }
}

