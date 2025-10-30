using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly AppDBContext dataBaseContext;
        private readonly DbSet<T> dbSet;

        public EntityRepository(AppDBContext context)
        {
            dataBaseContext = context;
            dbSet = dataBaseContext.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public void Delete<T>(int id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
                dbSet.Remove(entity);
        }
        public T? ReadById(int id)
        {
            return dbSet.Find(id);
        }
        public List<T> ReadAll()
        { 
            return dbSet.ToList(); 
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            dataBaseContext.Entry(entity).State = EntityState.Modified;
        }
        public void SaveChanges()
        {
            dataBaseContext.SaveChanges(); 
        }


    }
}
