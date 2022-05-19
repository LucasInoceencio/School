using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;
using School.Domain.ValueObjects;

namespace School.Infrastructure.Persistence.Mapping;

internal class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        // Definindo a tabela
        builder.ToTableCustom(ModuleDatabase.ModuloCadastrosGerais, "pessoa");

        builder.EntityMap("pessoa");

        // Definindo demais propriedades
        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnVarchar("name", 100);

        builder.Property(p => p.Lastname)
            .IsRequired()
            .HasColumnVarchar("lastname", 100);

        builder.Property(p => p.BirthDate)
            .HasColumnDateTime("birth_date");

        // Ignorando o mapeamento da propriedade Idade, pois a mesma é calculada em tempo de execução
        builder.Ignore(p => p.Age);

        // Cpf é um struct, por isso houve a necessidade de usar o HasConversion
        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => new Cpf(v))
            .HasColumnName("vr_cpf")
            .HasMaxLength(200);

        // Email é um struct, por isso houve a necessidade de usar o HasConversion
        builder.Property(p => p.Email)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => new Email(v))
            .HasColumnName("vr_email")
            .HasMaxLength(200);

        builder.EntityMap();

        builder.HasIndex(p => p.Cpf)
            .IsUnique()
            .HasDatabaseName("idx_pessoa_cpf");

        builder.Property(p => p.Memo)
            .HasColumnText("historico");

        builder.Property(p => p.Active)
            .HasColumnBoolean("ativo");

        builder.Property(p => p.Type)
            .HasColumnChar("status");
    }
}