using System.Globalization;

namespace LearningDotNet.Common;

public static class FluentValidationsHelper
{
    public static bool BeAValidDate(string value, CultureInfo cultureInfo)
    {
        return DateOnly.TryParse(value, cultureInfo, out _);
    }
}