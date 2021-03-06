# Cassie_Custard

Cassie Custard is an ORM helper template for dealing with Cassandra datastore endpoint. Its motivation is to simplify session initialization and entity definition mapping that we might have inside our modules/boundaries. Having multiple instance of Cassandra end point in our system require a new CassandraContext object to be initialized.

# Its Lightweight

It consists of a small chunk of code, and depending on how we design the entity model inside our module we may just define them as CassandraEntitySet provided that the model designed is implementing ICassandraEntity interface. 

# How To Play 

1. Create a new Azure Cosmos DB ( Cassandra DB ) in Azure

2. Get The Credentials 

3. Enter Details 

- Contact Point

- Keyspace Name ( Created on Azure or Hardcoded ( Code first Approach ) )

- Username

- Password

# Special Thanks To

DataStax ( CassandraCSharpDriver ) - [View License](https://github.com/datastax/csharp-driver/blob/master/LICENSE) 

# Cassandra Repo 

https://github.com/apache/cassandra

