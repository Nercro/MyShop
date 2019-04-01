using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T1> where T1 : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T1> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T1).Name;
            items = cache[className] as List<T1>;
            if (items == null)
            {
                items = new List<T1>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T1 t)
        {
            items.Add(t);
        }

        public void Update(T1 t1)
        {
            T1 t1ToUpdate = items.Find(i => i.ID == t1.ID);

            if (t1ToUpdate != null)
            {
                t1ToUpdate = t1;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

        public T1 Find(string id)
        {
            T1 t1 = items.Find(i => i.ID == id);
            if (t1 != null)
            {
                return t1;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

        public IQueryable<T1> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string id)
        {
            T1 t1ToDelete = items.Find(i => i.ID == id);

            if (t1ToDelete != null)
            {
                items.Remove(t1ToDelete);
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }
    }
}
