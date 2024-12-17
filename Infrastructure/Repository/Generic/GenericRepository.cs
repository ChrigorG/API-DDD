using Domain.Interface.Generic;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infrastructure.Repository.Generic
{
    public class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public GenericRepository()
        {
            _dbContextOptions = new DbContextOptions<AppDbContext>();
        }

        public async Task Add(T entity)
        {
            using (var data = new AppDbContext(_dbContextOptions))
            {
                await data.Set<T>().AddAsync(entity);
                await data.SaveChangesAsync();
            }
        }
       
        public async Task<List<T>> Get()
        {
            using (var data = new AppDbContext(_dbContextOptions))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<T?> Get(int id)
        {
            using (var data = new AppDbContext(_dbContextOptions))
            {
                return await data.Set<T>().FindAsync(id);
            }
        }

        public async Task Update(T entity)
        {
            using (var data = new AppDbContext(_dbContextOptions))
            {
                data.Set<T>().Update(entity);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T entity)
        {
            using (var data = new AppDbContext(_dbContextOptions))
            {
                data.Set<T>().Remove(entity);
                await data.SaveChangesAsync();
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
