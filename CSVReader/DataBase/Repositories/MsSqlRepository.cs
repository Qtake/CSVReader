using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CSVReader.DataBase.Repositories
{
    internal class MsSqlRepository : IRepository<Record>
    {
        private readonly ApplicationContext _context;
        private bool _isDisposed;

        public MsSqlRepository(string connectionString)
        {
            _context = new ApplicationContext(connectionString);
            _isDisposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool state)
        {
            if (!_isDisposed)
            {
                if (state)
                {
                    _context.Dispose();
                }
            }
            _isDisposed = true;
        }

        public IQueryable<Record> SelectAll()
        {
            return _context.Records;
        }

        public Record Select(int id)
        {
            Record? record = _context.Records.Find(id);

            if (record != null)
            {
                return record;
            }
            else
            {
                throw new NullReferenceException();
            }

        }

        public void Add(Record item)
        {
            _context.Records.Add(item);
        }

        public void Update(Record item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Record? item = Select(id);

            if (item != null)
            {
                _context.Records.Remove(item);
            }
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DROP TABLE [Records]");
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
