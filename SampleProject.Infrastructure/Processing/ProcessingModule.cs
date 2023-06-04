using System.Reflection;
using Autofac;
using MediatR;
using SampleProject.Application;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.DomainEvents;
using SampleProject.Application.Configuration.Processing;

using SampleProject.Infrastructure.Logging;
using SampleProject.Infrastructure.Processing.InternalCommands;

namespace SampleProject.Infrastructure.Processing
{
    public class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(
                typeof(DomainEventsDispatcherNotificationHandlerDecorator<>), 
                typeof(INotificationHandler<>));

            builder.RegisterType<CommandsScheduler>()
                .As<ICommandsScheduler>()
                .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));
        }
    }
}