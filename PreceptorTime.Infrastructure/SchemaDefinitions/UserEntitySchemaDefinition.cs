using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreceptorTime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Infrastructure.SchemaDefinitions
{
    public class UserEntitySchemaDefinition : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Account)
                .IsRequired();

            builder.Property(x => x.Active)
                .IsRequired();

            builder.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(40);
            //non clusted idx
            builder.HasIndex(x => x.DisplayName)
                .IsUnique();



            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);
            //non clusted idx
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.Token);
            builder.Property(x => x.TokenExpiration);
        }
    }
}
