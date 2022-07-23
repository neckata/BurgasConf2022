using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        protected string Schema => "OnlineShop";

        public ApplicationDbContext(
      DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Action> Actions { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrWhiteSpace(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(true, cancellationToken);
        }
    }
}