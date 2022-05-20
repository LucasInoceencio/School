using FluentMigrator;
using School.Infrastructure.Persistence.Mapping;

namespace School.Infrastructure.Persistence.Migrations;

[Migration(20220519153025, "Creating person table")]
public class PersonMigrator : Migration
{
    public override void Down() { }

    public override void Up()
    {
        Create.CustomTable(ModuleDatabase.ModuloCadastrosGerais, "person")
            .WithInteger64CustomColumn("id").PrimaryKey().Identity()
            .WithBooleanCustomColumn("active")
            .WithVarcharCustomColumn("name", 100)
            .WithVarcharCustomColumn("lastname", 100)
            .WithDateCustomColumn("birth_date")
            .WithVarcharCustomColumn("cpf", 200).Unique()
            .WithVarcharCustomColumn("email", 200)
            .WithCharCustomColumn("type")
            .WithTextCustomColumn("memo").Nullable()
            .WithEntityColumns();
    }
}