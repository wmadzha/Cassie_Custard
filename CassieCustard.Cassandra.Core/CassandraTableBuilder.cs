using System;
namespace CassieCustard.Cassandra.Core
{
    internal static class TableEntityBuilder
    {
        public static string GenerateCreateQuery(this Type obj, string DatabaseName)
        {
            string QueryBuilder = "Create TABLE IF NOT EXISTS " + DatabaseName + "." + obj.Name + " (";
            foreach (var a in obj.GetProperties())
            {
                QueryBuilder = QueryBuilder + a.Name + " " + a.PropertyType.ToCassandraType() + ",";
            }
            return QueryBuilder + " PRIMARY KEY (Id))";
        }

        public static string ToCassandraType(this Type type)
        {
            if (type == typeof(string))
                return "text";
            else if (type == typeof(int))
                return "integer";
            else if (type == typeof(Int16))
                return "integer";
            else if (type == typeof(Int32))
                return "integer";
            else if (type == typeof(Int64))
                return "integer";
            else if (type == typeof(UInt16))
                return "integer";
            else if (type == typeof(UInt32))
                return "integer";
            else if (type == typeof(UInt64))
                return "integer";
            else if (type == typeof(bool))
                return "boolean";
            else if (type == typeof(Boolean))
                return "boolean";
            else if (type == typeof(Guid))
                return "uuid";
            else
                return "string";
        }

    }
}
