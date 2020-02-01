using CassieCustard.Cassandra.Core;
namespace CassieCustard.Template.DataAccess
{
    public class SampleModuleContext : CassandraContext
    {
        public SampleModuleContext(CassandraSettings settings) : base(settings)
        {

        }
        public ICassandraEntitySet<BusinessModel_2> Model2 { get; set; }
        public ICassandraEntitySet<BusinessModel_1> Model1 { get; set; }
        protected override void SetupEntites()
        {
            Model1 = new CassandraEntitySet<BusinessModel_1>(this._CassandraComponent, this._Settings.KeySpaceName);
            Model2 = new CassandraEntitySet<BusinessModel_2>(this._CassandraComponent, this._Settings.KeySpaceName);
        }
    }
  
}
