using Domain.Interfaces.Generics;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infrastructure.Repository.Generics
{
	public class RepositoryGeneric<T> : IGeneric<T>, IDisposable where T : class
	{
		private	readonly DbContextOptions<ContextBase> _dbContext;

        public RepositoryGeneric()
        {
            _dbContext = new DbContextOptions<ContextBase>();
        }

		public async Task Add(T Objeto)
		{
			using (var data = new ContextBase(_dbContext))
			{
				await data.Set<T>().AddAsync(Objeto);
				await data.SaveChangesAsync();
			}
		}

		public async Task Update(T Objeto)
		{
			using (var data = new ContextBase(_dbContext))
			{
				data.Set<T>().Update(Objeto);
				await data.SaveChangesAsync();
			}
		}

		public async Task Delete(T Objeto)
		{
			using (var data = new ContextBase(_dbContext))
			{
				data.Set<T>().Remove(Objeto);
				await data.SaveChangesAsync();
			}
		}

		public async Task<T> GetEntityById(int Id)
		{
			using (var data = new ContextBase(_dbContext))
			{
				return await data.Set<T>().FindAsync(Id);
			}
		}

		public async Task<List<T>> List()
		{
			using (var data = new ContextBase(_dbContext))
			{
				return await data.Set<T>().ToListAsync();
			}
		}
		

		#region IDisposable https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose

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
