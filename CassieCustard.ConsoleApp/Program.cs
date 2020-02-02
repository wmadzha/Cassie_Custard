using System;
using CassieCustard.Cassandra.Core;
using CassieCustard.Template.DataAccess;
namespace CassieCustard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SampleModuleContext Context = SetupContext();
            // Add Sample Data
            AddSampleData_Model1(Context);
            // Query Data Against Cassandra
            GetLatestData_Model1(Context);
            Console.ReadLine();
        }
        /// <summary>
        /// Setup The NoSQL Database Context
        /// </summary>
        /// <returns>Business Domain Context</returns>
        static SampleModuleContext SetupContext()
        {
            // This Details is based on Azure Cosmos DB Cassandra API
           return new SampleModuleContext(new CassandraSettings()
            {
                CassandraContactPoint = "samplename.cassandra.cosmos.azure.com",
                CassandraPort = 10350, // Default Port
                KeySpaceName = "samplesacename",
                Password = "samplefakepasswordkeyeEfGkNwPZSCqseHrBTeN09mgBVybjVlunUqlaCaL4BrD74an3oCPb5FPQ==",
                UserName = "sampleusername"
            });
        }
        /// <summary>
        /// Add 3 Sample Data
        /// </summary>
        /// <param name="ctx">Cassandra Business Module Context</param>
        static void AddSampleData_Model1(SampleModuleContext ctx)
        {
            Console.WriteLine("Adding Data To Cassandra" + Environment.NewLine);
            ctx.Model1.Add(new BusinessModel_1() { BusinessModelName = "SampleModel1_Name1" });
            ctx.Model1.Add(new BusinessModel_1() { BusinessModelName = "SampleModel1_Name2" });
            ctx.Model1.Add(new BusinessModel_1() { BusinessModelName = "SampleModel1_Name3" });
        }
        /// <summary>
        /// Get Latest Data For Model 1
        /// </summary>
        /// <param name="ctx">Cassandra Business Module Context</param>
        static void GetLatestData_Model1(SampleModuleContext ctx)
        {
            Console.WriteLine("Get Data List" + Environment.NewLine);
            foreach (var a in ctx.Model1.GetAll())
            {
                Console.WriteLine(a.Id + " : " + a.BusinessModelName);
            }
        }
    }
}
