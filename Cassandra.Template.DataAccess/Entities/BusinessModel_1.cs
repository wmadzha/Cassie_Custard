using CassieCustard.Cassandra.Core;
using System;
namespace CassieCustard.Template.DataAccess
{
    public class BusinessModel_1 : ICassandraEntity
    {
        public Guid Id { get; set; }
        public string BusinessModelName { get; set; }

        public BusinessModel_1()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
