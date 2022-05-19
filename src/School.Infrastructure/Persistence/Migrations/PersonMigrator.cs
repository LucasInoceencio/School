using FluentMigrator;
using FluentMigrator.Infrastructure;

namespace School.Infrastructure.Persistence.Migrations;

[Migration(20220519153025, "Creating person table")]
public class PersonMigrator : Migration
{
    public override void Down() { }

    public override void Up()
    {
        Create.Table("person")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("active").AsBoolean()
            .WithColumn("name").AsString()
            .WithColumn("lastname").AsString()
            .WithColumn("birth_date").AsDate()
            .WithColumn("cpf").AsString().Unique()
            .WithColumn("email").AsString()
            .WithColumn("type").AsString(1)
            .WithColumn("memo").AsString()
            .WithColumn("date_hour_register").AsDate()
            .WithColumn("registered_by").AsString()
            .WithColumn("date_hour_changed").AsDate()
            .WithColumn("changed_by").AsString();
    }
}