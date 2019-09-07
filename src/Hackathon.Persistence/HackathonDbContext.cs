using Hackathon.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RFERL.Modules.Framework.Data.EntityFramework.Abstractions;

namespace Hackathon.Persistence
{
    public class HackathonDbContext : BaseDomainDrivenDbContext
    {
        public static readonly string DefaultSchema = "Hackathon";

        public HackathonDbContext(DbContextOptions<HackathonDbContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        public DbSet<Data> Datas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DataEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration());
        }
    }
}
