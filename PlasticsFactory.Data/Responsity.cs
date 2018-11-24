using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlasticsFactory.Data
{
    public class Responsity<T> : IResponsity<T> where T : class
    {
        internal PlasticFactoryEntities db;
        internal DbSet<T> dbSet;
        public Responsity()
        {
            this.db = new PlasticFactoryEntities();
            this.dbSet = db.Set<T>();
        }
        public IList<T> GetData(Func<T, bool> where, params Expression<Func<T, object>>[] navigations)
        {
            List<T> list;
            IQueryable<T> query = db.Set<T>();
            foreach (Expression<Func<T, object>> item in navigations)
            {
                query = query.Include<T, object>(item);
            }
            list = query.AsNoTracking()
                .Where(where)
                .ToList();
            return list;
        }
        public void sqlQuery(string query)
        {
            try
            {
                db.Database.SqlQuery<T>(query);
            }
            catch
            {
                throw;
            }
        }
        #region Insert,Update
        public bool Add(T item)
        {
            try
            {
                dbSet.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool Add(IList<T> item)
        {
            try
            {
                dbSet.AddRange(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(T obj)
        {
            using (var d = new PlasticFactoryEntities())
                try
                {
                    DbSet dSet = d.Set<T>();
                    dSet.Attach(obj);
                    d.Entry(obj).State = EntityState.Modified;
                    d.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool Delete(int ID)
        {
            try
            {
                T obj = dbSet.Find(ID);
                dbSet.Remove(obj);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(T item)
        {
            try
            {
                if (db.Entry(item).State == EntityState.Detached)
                {
                    dbSet.Attach(item);
                }
                dbSet.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
