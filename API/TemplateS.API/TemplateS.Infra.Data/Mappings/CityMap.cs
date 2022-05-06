using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Entities;

namespace TemplateS.Infra.Data.Mappings
{
    public class CityMap : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Uf).HasMaxLength(2).IsRequired();

            builder.HasMany(x => x.Persons).WithOne(x => x.City).HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
