using FluentMigrator.Builders.Create;
using FluentMigrator.Builders.Create.Table;
using School.Infrastructure.Mapping;

namespace School.Infrastructure.Migrations;

public static class MigratorHelper
{
    private static readonly bool _isOracle;
    private static readonly int _maxLengthOracle = 30;

    static MigratorHelper()
    {
        //_isOracle = System.Configuration.ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ProviderName.IndexOf("oracle", StringComparison.OrdinalIgnoreCase) >= 0;
        _isOracle = false;
    }

    public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax CustomTable(
        this ICreateExpressionRoot entity,
        ModuleDatabase module,
        string tableName)
    {
        var fullName = module.Value + tableName;
        if (fullName.Length > _maxLengthOracle)
            throw new ArgumentException($"Não é permitido criar tabelas com nome superior a {_maxLengthOracle} caracteres.");

        if (_isOracle)
            return entity.Table(fullName.ToUpper());

        return entity.Table(fullName.ToLower());
    }

    private static ICreateTableColumnAsTypeSyntax WithCustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        if (columnName.Length > _maxLengthOracle)
            throw new ArgumentException($"Não é permitido criar colunas com nome superior a {_maxLengthOracle} caracteres.");

        if (_isOracle)
            return property.WithColumn(columnName.ToUpper());

        return property.WithColumn(columnName.ToLower());
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithVarcharCustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName, int length)
    {
        return property.WithCustomColumn("vr_" + columnName).AsString(length);
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithTextCustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        return property.WithCustomColumn("tx_" + columnName).AsString();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithInteger16CustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        return property.WithCustomColumn("in_" + columnName).AsInt16();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithInteger32CustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        return property.WithCustomColumn("in_" + columnName).AsInt32();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithInteger64CustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        return property.WithCustomColumn("in_" + columnName).AsInt64();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithDecimalCustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        return property.WithCustomColumn("nm_" + columnName).AsDecimal();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithDecimalCustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName, int size, int precision)
    {
        return property.WithCustomColumn("nm_" + columnName).AsDecimal(size, precision);
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithDateCustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        return property.WithCustomColumn("dt_" + columnName).AsDateTime();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithBooleanCustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        return property.WithCustomColumn("bl_" + columnName).AsBoolean();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithCharCustomColumn(
        this ICreateTableWithColumnSyntax property, string columnName)
    {
        return property.WithCustomColumn("ch_" + columnName).AsString(1);
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithEntityColumns(
        this ICreateTableWithColumnSyntax property)
    {
        return property
            .WithDateCustomColumn("date_hour_register")
            .WithVarcharCustomColumn("registered_by", 100)
            .WithDateCustomColumn("date_hour_changed").Nullable()
            .WithVarcharCustomColumn("changed_by", 100).Nullable();
    }
}