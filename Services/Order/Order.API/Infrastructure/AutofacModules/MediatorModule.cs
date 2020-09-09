using Autofac;
using MediatR;
using Order.API.Application.Command;

namespace Order.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateOrderCommand>();
            builder.RegisterType<CreateOrderCommandHandler>().AsImplementedInterfaces().InstancePerDependency();
            //Necessary for constructor of Mediator class.
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();       
        }
    }
}