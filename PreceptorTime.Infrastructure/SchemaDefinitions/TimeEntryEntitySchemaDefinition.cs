using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreceptorTime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreceptorTime.Infrastructure.SchemaDefinitions
{
    public class TimeEntryEntitySchemaDefinition : IEntityTypeConfiguration<TimeEntry>
    {
        public void Configure(EntityTypeBuilder<TimeEntry> builder)
        {
            builder.ToTable("TimeEntries");
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.NoAction); ;

            builder.HasOne(s => s.Teacher)
                .WithMany()
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Hours)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(x => x.Notes)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
