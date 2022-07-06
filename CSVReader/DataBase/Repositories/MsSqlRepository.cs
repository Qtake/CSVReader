using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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

        public IQueryable<Record> SelectAll(Record filter)
        {
            IQueryable<Record> filteredRecords = _context.Records;

            if (filter.Date != null)
            {
                filteredRecords = filteredRecords.Where(x => x.Date == filter.Date);
            }

            if (!string.IsNullOrEmpty(filter.Firstname))
            {
                filteredRecords = filteredRecords.Where(x => x.Firstname == filter.Firstname);
            }

            if (!string.IsNullOrEmpty(filter.Surname))
            {
                filteredRecords = filteredRecords.Where(x => x.Surname == filter.Surname);
            }

            if (!string.IsNullOrEmpty(filter.Patronymic))
            {
                filteredRecords = filteredRecords.Where(x => x.Patronymic == filter.Patronymic);
            }

            if (!string.IsNullOrEmpty(filter.City))
            {
                filteredRecords = filteredRecords.Where(x => x.City == filter.City);
            }

            if (!string.IsNullOrEmpty(filter.Country))
            {
                filteredRecords = filteredRecords.Where(x => x.Country == filter.Country);
            }

            return filteredRecords;
        }

        public IQueryable<Record> SelectAll(params Expression<Func<Record, bool>>[] filters)
        {
            IQueryable<Record> filteredRecords = _context.Records;

            foreach (var item in filters)
            {
                filteredRecords = filteredRecords.Where(item);
            }

            return filteredRecords;
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
