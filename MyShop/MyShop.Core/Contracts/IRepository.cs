using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    public interface IRepository<T1> where T1 : BaseEntity
    {
        IQueryable<T1> Collection();
        void Commit();
        void Delete(string id);
        T1 Find(string id);
        void Insert(T1 t);
        void Update(T1 t1);
    }
}