using System.Linq;
using System.Collections.Generic;

namespace RBlogOnNetCore.EF
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
        void InsertList(IList<T> entitys);
        void Update(T entity);
        void Delete(T entity);
        void DeleteList(IList<T> entitys);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
}
