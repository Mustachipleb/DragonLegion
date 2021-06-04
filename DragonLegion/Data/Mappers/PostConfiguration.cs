using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonLegion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DragonLegion.Data.Mappers
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder
                .HasOne(p => p.Author)
                .WithMany();
        }
    }
}
