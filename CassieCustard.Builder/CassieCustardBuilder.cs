using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CassieCustard.Template.DataAccess;
using CassieCustard.Cassandra.Core;
namespace CassieCustard.Builder
{
    public static class CaseyCake
    {
        public static IServiceCollection SetupCassieCustard(this IServiceCollection svc, IConfiguration config)
        {
            CassandraSettings noSqlDb = new CassandraSettings()
            {
                CassandraPort = 10350,
                CassandraContactPoint = config.GetSection("CassandraContactPoint_ModuleName").Value,
                KeySpaceName = config.GetSection("KeySpaceName_ModuleName").Value,
                Password = config.GetSection("Password_Cassandra_ModuleName").Value,
                UserName = config.GetSection("UserName_Cassandra_ModuleName").Value
            };
            svc.AddScoped(noSqldb => new SampleModuleContext(noSqlDb));
            return svc;
        }
    }
}
