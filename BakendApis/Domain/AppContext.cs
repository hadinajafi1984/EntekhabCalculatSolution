using BackendApis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApis.Domain
{
    public class AppLicationContext:DbContext
    {
        public AppLicationContext(DbContextOptions<AppLicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PersonnelData> PersonelDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        }
    }
}
