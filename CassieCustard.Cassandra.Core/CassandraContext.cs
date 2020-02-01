using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Cassandra;
using Cassandra.Mapping;
namespace CassieCustard.Cassandra.Core
{
    /// <summary>
    /// Abstract Context That Will Be Inherited By Domain Classess
    /// </summary>
    public abstract class CassandraContext
    {
        protected abstract void SetupEntites();
        protected CassandraComponent _CassandraComponent { get; set; }
        protected CassandraSettings _Settings { get; set; }
        public CassandraContext(CassandraSettings settings)
        {
            _Settings = settings;
            _Settings.KeySpaceName = settings.KeySpaceName.ToLower();
            _CassandraComponent = new CassandraComponent();
            var Options = new SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);
            Options.SetHostNameResolver((ipAddress) => _Settings.CassandraContactPoint);

            _CassandraComponent.Cluster = Cluster.Builder()
                        .WithCredentials(_Settings.UserName, _Settings.Password)
                        .WithPort(_Settings.CassandraPort)
                        .AddContactPoint(_Settings.CassandraContactPoint)
                        .WithSSL(Options)
                        .Build();
            _CassandraComponent.Session = _CassandraComponent.Cluster.Connect();
            _CassandraComponent.Session.Execute("CREATE KEYSPACE IF NOT EXISTS " + _Settings.KeySpaceName.ToLower() + " WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 1 };");
            _CassandraComponent.Session = _CassandraComponent.Cluster.Connect(_Settings.KeySpaceName.ToLower());
            _CassandraComponent.Mapper = new Mapper(_CassandraComponent.Session);
            this.SetupEntites();
        }
        /// <summary>
        ///  Taken From CassandraCSharpDriver - Regarding Connection To Cassandra Instance For Establishment Of A Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
    }
    /// <summary>
    /// Componenet Reuse For Entity Table Set
    /// </summary>
    public class CassandraComponent
    {
        public IMapper Mapper { get; set; }
        public Cluster Cluster { get; set; }
        public ISession Session { get; set; }
    }
    /// <summary>
    /// Setting Object For Defining Which Cassandra We Are Dealing With
    /// </summary>
    public class CassandraSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CassandraContactPoint { get; set; }
        public int CassandraPort { get; set; }
        public string KeySpaceName { get; set; }
    }
}
