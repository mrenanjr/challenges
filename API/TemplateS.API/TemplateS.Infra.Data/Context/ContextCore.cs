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
        public DbSet<User> Users { get; set; }
        public DbSet<PullRequest> PullRequests { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new CityMap());
            modelBuilder.ApplyConfiguration(new PullRequestMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ContactMap());

            modelBuilder.ApplyGlobalStandards();
            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }
    }
}