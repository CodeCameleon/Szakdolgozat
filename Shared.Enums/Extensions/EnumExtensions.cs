using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Shared.Enums.Extensions;

/// <summary>
/// Az enumokhoz tartozó általános kiterjesztések.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Lekéri az enum értékéhez tartozó megjelenítendő nevet.
    /// </summary>
    /// <param name="value">A keresett enum érték.</param>
    /// <returns>A megjelenítendő név ha létezik, különben az enum értéke szövegként.</returns>
    public static string GetDisplayName(this Enum value)
    {
        Type type = value.GetType();

        FieldInfo? fieldInfo = type.GetField(value.ToString());

        DisplayAttribute? displayAttribute = fieldInfo?.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.GetName() ?? value.ToString();
    }
}
