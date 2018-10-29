using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlasticsFactory.Data
{
    public interface IResponsity<T>where T :class
    {
        IList<T> GetData(Func<T, bool> where, params Expression<Func<T, object>>[] navigations);
        bool Update(T item);
        void sqlQuery(string query);
        bool Add(IList<T> item);
        bool Add(T item);
        bool Delete(int ID);
        bool Delete(T item);
    }
}
