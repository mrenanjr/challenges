using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateS.Domain.Entities;

namespace TemplateS.Infra.Data.Mappings
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Cpf).HasMaxLength(11).IsRequired();
            builder.Property(x => x.Age).IsRequired();

            builder.HasOne(x => x.City).WithMany(x => x.Persons).HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Contact).WithOne(x => x.Person).HasForeignKey<Contact>(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}