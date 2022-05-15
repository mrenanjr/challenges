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
    public class ContactMap :IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.Whatsapp).HasMaxLength(20);
        }
    }
}
