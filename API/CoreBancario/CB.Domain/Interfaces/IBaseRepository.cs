using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CB.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Create(T obj);

        IEnumerable<T> GetAll();

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        void Update(T obj);

        void Delete(T obj);
    }
}
