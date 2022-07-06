using System;
using System.Linq;
using System.Linq.Expressions;

namespace CSVReader.DataBase.Repositories
{
    internal interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> SelectAll();

        IQueryable<T> SelectAll(Record filter);

        IQueryable<T> SelectAll(params Expression<Func<Record, bool>>[] filters);

        T Select(int id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);

        void DeleteAll();

        void Save();
    }
}
