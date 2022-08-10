using ApiPractice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(20);
            builder.Property(p => p.Price).HasDefaultValue(1000);
            builder.Property(p => p.isDeleted).HasDefaultValue(false);
            builder.Property(p => p.CreatedTime).HasDefaultValue(DateTime.Now);
        }
    }
}
