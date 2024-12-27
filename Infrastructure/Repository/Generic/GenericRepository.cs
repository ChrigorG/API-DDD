using Domain.Interface.Generic;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infrastructure.Repository.Generic
{
    public abstract class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
    {
        protected readonly AppDbContext _db;

        public GenericRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<T?> Add(T entity)
        {
            try
            {
                await _db.Set<T>().AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<T>> Get()
        {
            return await _db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> Get(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T?> Update(T entity)
        {
            try
            {
                _db.Set<T>().Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<T?> Delete(T entity)
        {
            try
            {
                _db.Set<T>().Remove(entity);
                await _db.SaveChangesAsync();
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        #region Disposed // https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle? _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle?.Dispose();
                    _safeHandle = null;
                }

                _disposedValue = true;
            }
        }
        #endregion
    }
}
