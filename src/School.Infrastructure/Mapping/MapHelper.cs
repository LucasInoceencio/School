using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace School.Infrastructure.Mapping;

public static class MapHelper
{
    private static readonly bool _isOracle;
    private static readonly int _maxLengthOracle = 30;
    static MapHelper()
    {
        //_isOracle = System.Configuration.ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ProviderName.IndexOf("oracle", StringComparison.OrdinalIgnoreCase) >= 0;
        _isOracle = false;
    }

    public static EntityTypeBuilder ToTableCustom(this EntityTypeBuilder entity, ModuleDatabase module, string tableName)
    {
        var fullName = module.Value + tableName;
        if (fullName.Length > _maxLengthOracle)
            throw new ArgumentException($"Não é permitido criar tabelas com nome superior a {_maxLengthOracle} caracteres.");

        if (_isOracle)
            return entity.ToTable(fullName.ToUpper());

        return entity.ToTable(fullName.ToLower());
    }

    private static PropertyBuilder<T> HasColumnNameCustom<T>(this PropertyBuilder<T> property, string columnName)
    {
        if (columnName.Length > _maxLengthOracle)
            throw new ArgumentException($"Não é permitido criar colunas com nome superior a {_maxLengthOracle} caracteres.");

        if (_isOracle)
            return property.HasColumnName(columnName.ToUpper());

        return property.HasColumnName(columnName.ToLower());
    }

    public static PropertyBuilder<string> HasColumnVarchar(this PropertyBuilder<string> property, string columnName, int length)
    {
        return property.HasColumnNameCustom("vr_" + columnName)
            .HasMaxLength(length);
    }

    public static PropertyBuilder<string> HasColumnText(this PropertyBuilder<string> property, string columnName)
    {
        return property.HasColumnNameCustom("tx_" + columnName)
            .HasColumnType("text");
    }

    public static PropertyBuilder<int> HasColumnInteger(this PropertyBuilder<int> property, string columnName)
    {
        return property.HasColumnNameCustom("in_" + columnName);
    }

    public static PropertyBuilder<int?> HasColumnInteger(this PropertyBuilder<int?> property, string columnName)
    {
        return property.HasColumnNameCustom("in_" + columnName);
    }

    public static PropertyBuilder<decimal> HasColumnDecimal(this PropertyBuilder<decimal> property, string columnName, int precision)
    {
        return property.HasColumnNameCustom("nm_" + columnName)
            .HasPrecision(precision);
    }

    public static PropertyBuilder<decimal?> HasColumnDecimal(this PropertyBuilder<decimal?> property, string columnName, int precision)
    {
        return property.HasColumnNameCustom("nm_" + columnName)
            .HasPrecision(precision);
    }

    public static PropertyBuilder<DateTime> HasColumnDateTime(this PropertyBuilder<DateTime> property, string columnName)
    {
        return property
            .HasConversion(
                v => v.ToUniversalTime(),
                v => v.ToLocalTime())
            .HasColumnNameCustom("dt_" + columnName);
    }

    public static PropertyBuilder<DateTime?> HasColumnDateTime(this PropertyBuilder<DateTime?> property, string columnName)
    {
        return property.HasColumnNameCustom("dt_" + columnName);
    }

    public static PropertyBuilder<bool> HasColumnBoolean(this PropertyBuilder<bool> property, string columnName)
    {
        return property.HasColumnNameCustom("bl_" + columnName);
    }

    public static PropertyBuilder<bool?> HasColumnBoolean(this PropertyBuilder<bool?> property, string columnName)
    {
        return property.HasColumnNameCustom("bl_" + columnName);
    }

    public static PropertyBuilder<char> HasColumnChar(this PropertyBuilder<char> property, string columnName)
    {
        return property.HasColumnNameCustom("ch_" + columnName);
    }

    public static KeyBuilder HasNameCustom(this KeyBuilder keyBuilder, string name)
    {
        if (name.Length > _maxLengthOracle)
            throw new ArgumentException($"Não é permitido nomes com mais de {_maxLengthOracle} caracteres.");
        if (_isOracle)
            return keyBuilder.HasName(name.ToUpper());

        return keyBuilder.HasName(name);
    }
}