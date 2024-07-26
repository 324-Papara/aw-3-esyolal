// AutofacModule.cs
using System.Data;
using Autofac;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Para.Bussiness.Cqrs;
using Para.Data.Context;
using Para.Data.DapperRepository;
using Para.Data.UnitOfWork;
using Para.Schema.Validators;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

        builder.RegisterType<CustomerRepository>().SingleInstance();


        builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).Assembly)
           .AsImplementedInterfaces();

        builder.Register(c =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ParaDbContext>();
            var connectionString = c.Resolve<IConfiguration>().GetConnectionString("MsSqlConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new ParaDbContext(optionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(CustomerValidator).Assembly)
               .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
               .AsImplementedInterfaces();
    }
}
