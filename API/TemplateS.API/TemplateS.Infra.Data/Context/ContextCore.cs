using Microsoft.EntityFrameworkCore;
using TemplateS.Domain.Entities;
using TemplateS.Infra.Data.Extensions;
using TemplateS.Infra.Data.Mappings;

namespace TemplateS.Infra.Data.Context
{
    public class ContextCore : DbContext
    {
        public ContextCore(DbContextOptions<ContextCore> options) : base(options) { }

        #region "DbSets"

        public DbSet<Person> Persons { get; set; }
        public DbSet<City> Cities { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new CityMap());

            modelBuilder.ApplyGlobalStandards();
            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }
    }
}