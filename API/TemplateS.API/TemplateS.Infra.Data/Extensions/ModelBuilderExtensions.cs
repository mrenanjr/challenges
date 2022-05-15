using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TemplateS.Domain.Common;
using TemplateS.Domain.Entities;

namespace TemplateS.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder SeedData(this ModelBuilder modelBuilder)
        {
            var users = new[]
            {
                new User {
                    Id = Guid.Parse("e5d33252-cab3-4138-a7ac-ee84bbabbc12"),
                    Name = "Admin",
                    Email = "admin@teste.com",
                    Password = "7C4A8D09CA3762AF61E59520943DC26494F8941B"
                },
            };

            modelBuilder.Entity<User>().HasData(users);

            return modelBuilder;
        }

        public static ModelBuilder ApplyGlobalStandards(this ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    switch (property.Name)
                    {
                        case nameof(BaseEntity.Id):
                            property.IsKey();
                            property.SetDefaultValueSql("NEWID()");
                            break;
                        case nameof(BaseEntity.UpdatedDate):
                            property.IsNullable = true;
                            break;
                        case nameof(BaseEntity.CreatedDate):
                            property.IsNullable = false;
                            property.SetColumnType("datetime");
                            property.SetDefaultValueSql("CURRENT_TIMESTAMP");
                            break;
                    }
                }
            }

            return builder;
        }
    }
}
