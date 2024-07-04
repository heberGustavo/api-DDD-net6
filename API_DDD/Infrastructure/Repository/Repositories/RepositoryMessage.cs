using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Repositories
{
	public class RepositoryMessage : RepositoryGeneric<Message>, IMessage
	{
		private readonly DbContextOptions<ContextBase> _dbContext;

		public RepositoryMessage()
		{
			_dbContext = new DbContextOptions<ContextBase>();
		}

		public async Task<List<Message>> ListMessage(Expression<Func<Message, bool>> exMessage)
		{
			using (var context = new ContextBase(_dbContext))
			{
				return await context.Messages.Where(exMessage).AsNoTracking().ToListAsync();
			}
		}
	}
}
