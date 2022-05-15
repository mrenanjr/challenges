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
    public class PullRequestMap : IEntityTypeConfiguration<PullRequest>
    {
        public void Configure(EntityTypeBuilder<PullRequest> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).IsRequired();
        }
    }
}
