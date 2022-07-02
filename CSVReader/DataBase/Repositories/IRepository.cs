using System;
using System.Linq;

namespace CSVReader.DataBase.Repositories
{
    internal interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> SelectAll();

        T Select(int id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);

        void DeleteAll();

        void Save();
    }
}
