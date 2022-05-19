using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities.Core;

namespace School.Infrastructure.Persistence.Mapping;

public static class BaseMapping
{
    public static void EntityMap<T>(this EntityTypeBuilder<T> builder) where T : Entity
    {
        builder.Property(p => p.DateHourRegister)
            .IsRequired()
            .HasColumnDateTime("date_hour_register");

        builder.Property(p => p.RegisteredBy)
            .HasColumnVarchar("registered_by", 100);

        builder.Property(p => p.DateHourChange)
            .HasColumnDateTime("date_hour_changed");

        builder.Property(p => p.ChangedBy)
            .HasColumnVarchar("changed_by", 100);
    }

    public static void EntityMap<T>(this EntityTypeBuilder<T> builder, string name) where T : Entity
    {
        // Chave primária
        builder.HasKey(p => p.Id)
            .HasNameCustom("pk_" + name);

        builder.Property(p => p.Id)
            .HasColumnOrder(0)
            .HasColumnInteger("id");
    }
}