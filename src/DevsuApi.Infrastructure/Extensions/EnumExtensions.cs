using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevsuApi.Infrastructure.Extensions;

public class EnumMemberConverter<T> : ValueConverter<T, string> where T : struct, Enum
{
    public EnumMemberConverter() : base(
        v => v.GetDatabaseName(), // Enum to DB
        v => EnumExtensions.GetEnumFromDatabaseName<T>(v)) // DB to Enum
    { }
}

public static class EnumExtensions
{
    public static string GetDatabaseName(this Enum enumValue)
    {
        var member = enumValue.GetType()
                              .GetMember(enumValue.ToString())
                              .FirstOrDefault();

        var attribute = member?.GetCustomAttribute<EnumMemberAttribute>();
        return attribute?.Value ?? enumValue.ToString();
    }

    public static T GetEnumFromDatabaseName<T>(string value) where T : struct, Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            var attribute = field.GetCustomAttribute<EnumMemberAttribute>();
            if (attribute?.Value == value)
            {
                var fieldValue = field.GetValue(null);
                if (fieldValue != null)
                    return (T)fieldValue;
            }
        }
        return Enum.Parse<T>(value, true);
    }
}
