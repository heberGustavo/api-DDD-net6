using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
	public class ContextBase : IdentityDbContext<ApplicationUser>
	{
		public ContextBase(DbContextOptions<ContextBase> options) : base(options)
		{
		}

		public DbSet<Message> Messages { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

		#region Métodos Protegidos

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//If not configured in Program.cs
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(ObterStringConexao());
				base.OnConfiguring(optionsBuilder);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			//To map the primary key in Identity table
			builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(x => x.Id);

			base.OnModelCreating(builder);
		}

		#endregion

		#region Métodos Privados

		private string ObterStringConexao()
		{
			return "Password=root;Persist Security Info=True;User ID=root;Initial Catalog=master;Data Source=DESKTOP-N25IT39\\SQLEXPRESS"
		}

		#endregion
	}
}