using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TemplateS.Domain.Common;
using TemplateS.Domain.Entities;

namespace TemplateS.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder SeedData(this ModelBuilder modelBuilder) => modelBuilder;

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
