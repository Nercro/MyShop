using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T1> : IRepository<T1> where T1 : BaseEntity
    {
        internal DataContext dataContext;
        internal DbSet<T1> dbSet;

        public SQLRepository(DataContext context)
        {
            dataContext = context;
            dbSet = context.Set<T1>();
        }

        public IQueryable<T1> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            dataContext.SaveChanges();
        }

        public void Delete(string id)
        {
            var t = Find(id);
            if (dataContext.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);
            }

            dbSet.Remove(t);
        }

        public T1 Find(string id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T1 t)
        {
            dbSet.Add(t);
        }

        public void Update(T1 t1)
        {
            dbSet.Attach(t1);
            dataContext.Entry(t1).State = EntityState.Modified;
        }
    }
}
