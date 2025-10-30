using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : IDomainObject
    {
        void Add(T entity);
        void Delete<T>(int id);
        T? ReadById(int id);
        List<T> ReadAll();
        void Update(T entity);
    }
}
