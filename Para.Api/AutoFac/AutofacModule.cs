using Autofac;
using Para.Data.UnitOfWork;
using System.Data;
using Microsoft.Data.SqlClient;

public class AutofacModule : Module
{
    private readonly IConfiguration _configuration;
     public AutofacModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void Load(ContainerBuilder builder)
    {
       builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();

        var connectionString = _configuration.GetConnectionString("MsSqlConnection");

        builder.Register(c => new SqlConnection(connectionString))
            .As<IDbConnection>()
            .InstancePerLifetimeScope();
    }
}
