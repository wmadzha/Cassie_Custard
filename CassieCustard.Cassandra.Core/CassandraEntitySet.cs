using System;
using System.Collections.Generic;
using System.Linq;
namespace CassieCustard.Cassandra.Core
{
    public interface ICassandraEntity
    {
        Guid Id { get; set; }
    }
    public interface ICassandraEntitySet<T> where T : ICassandraEntity
    {
        T GetById(Guid entityId);
        List<T> GetAll();
        T Add(T entity);
        bool Delete(T entity);
        bool Delete(Guid entityId);
        bool Edit(T entity);
        bool DropTable();
    }
    public class CassandraEntitySet<T> : ICassandraEntitySet<T> where T : ICassandraEntity
    {
        private CassandraComponent _Comp { get; set; }
        private string _TableName { get; set; }
        private string _KeySpaceName { get; set; }
        public CassandraEntitySet(CassandraComponent Comp, string KeySpaceName)
        {
            _Comp = Comp;
            _Comp.Session.Execute(typeof(T).GenerateCreateQuery(KeySpaceName));
            _TableName = typeof(T).Name;
            _KeySpaceName = KeySpaceName;
        }
        public T Add(T entity)
        {
            _Comp.Mapper.Insert<T>(entity);
            return entity;
        }
        public bool Delete(T entity)
        {
            _Comp.Mapper.Delete<T>("WHERE Id = ?", Guid.NewGuid());
            return true;
        }
        public bool Delete(Guid entityId)
        {
            _Comp.Mapper.Delete<T>("WHERE Id = ?", Guid.NewGuid());
            return true;
        }
        public bool Edit(T entity)
        {
            if (Delete(entity.Id))
            {
                _Comp.Mapper.Insert<T>(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<T> GetAll()
        {
            return _Comp.Mapper.Fetch<T>("SELECT * FROM " + _KeySpaceName + "." + _TableName).ToList();
        }
        public T GetById(Guid entityId)
        {
            return _Comp.Mapper.FirstOrDefault<T>("SELECT * FROM " + _KeySpaceName + "." + _TableName + " where Id = ?", entityId);
        }
        public bool DropTable()
        {
            _Comp.Session.Execute("DROP TABLE IF EXISTS " + _KeySpaceName + "." + _TableName);
            return true;
        }
    }
}
