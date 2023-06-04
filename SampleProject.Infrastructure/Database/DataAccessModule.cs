using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Application;

using SampleProject.Application.Configuration.Data;
using SampleProject.Domain.SeedWork;

using SampleProject.Infrastructure.Domain;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Database
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public DataAccessModule(string databaseConnectionString)
        {
            this._databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StronglyTypedIdValueConverterSelector>()
                .As<IValueConverterSelector>()
                .SingleInstance();
        }
    }
}