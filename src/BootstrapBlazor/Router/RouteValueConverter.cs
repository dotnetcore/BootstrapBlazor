// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
using System.Globalization;

namespace BootstrapBlazor.Components;

[ExcludeFromCodeCoverage]
internal abstract class RouteValueConverter
{
    private static readonly ConcurrentDictionary<Type, RouteValueConverter> _converters = [];

    public static bool TryGet(Type targetType, [MaybeNullWhen(false)] out RouteValueConverter converter)
    {
        if (_converters.TryGetValue(targetType, out converter))
        {
            return true;
        }

        converter = Create(targetType);
        if (converter is null)
        {
            return false;
        }

        _converters.TryAdd(targetType, converter);
        return true;
    }

    public static RouteValueConverter GetRouteConstraint(string template, string segment, string constraint)
    {
        if (constraint.Length == 0)
        {
            throw new ArgumentException(
                $"Malformed segment '{segment}' in route '{template}' contains an empty constraint.");
        }

        var targetType = constraint switch
        {
            "bool" => typeof(bool),
            "datetime" => typeof(DateTime),
            "decimal" => typeof(decimal),
            "double" => typeof(double),
            "float" => typeof(float),
            "guid" => typeof(Guid),
            "int" => typeof(int),
            "long" => typeof(long),
            _ => null
        };

        if (targetType is null || !TryGet(targetType, out var converter))
        {
            throw new ArgumentException($"Unsupported constraint '{constraint}' in route '{template}'.");
        }

        return converter;
    }

    public abstract bool TryParse(ReadOnlySpan<char> value, [MaybeNullWhen(false)] out object? result);

    public abstract object? Parse(ReadOnlySpan<char> value, string destinationName);

    public abstract Array ParseMultiple(StringValues values, string destinationName);

    private static RouteValueConverter? Create(Type targetType) => targetType switch
    {
        var type when type == typeof(string) => new TypedConverter<string>(TryParseString),
        var type when type == typeof(bool) => new TypedConverter<bool>(bool.TryParse),
        var type when type == typeof(bool?) => new NullableConverter<bool>(bool.TryParse),
        var type when type == typeof(DateTime) => new TypedConverter<DateTime>(TryParseDateTime),
        var type when type == typeof(DateTime?) => new NullableConverter<DateTime>(TryParseDateTime),
        var type when type == typeof(DateOnly) => new TypedConverter<DateOnly>(TryParseDateOnly),
        var type when type == typeof(DateOnly?) => new NullableConverter<DateOnly>(TryParseDateOnly),
        var type when type == typeof(TimeOnly) => new TypedConverter<TimeOnly>(TryParseTimeOnly),
        var type when type == typeof(TimeOnly?) => new NullableConverter<TimeOnly>(TryParseTimeOnly),
        var type when type == typeof(decimal) => new TypedConverter<decimal>(TryParseDecimal),
        var type when type == typeof(decimal?) => new NullableConverter<decimal>(TryParseDecimal),
        var type when type == typeof(double) => new TypedConverter<double>(TryParseDouble),
        var type when type == typeof(double?) => new NullableConverter<double>(TryParseDouble),
        var type when type == typeof(float) => new TypedConverter<float>(TryParseFloat),
        var type when type == typeof(float?) => new NullableConverter<float>(TryParseFloat),
        var type when type == typeof(Guid) => new TypedConverter<Guid>(Guid.TryParse),
        var type when type == typeof(Guid?) => new NullableConverter<Guid>(Guid.TryParse),
        var type when type == typeof(int) => new TypedConverter<int>(TryParseInt),
        var type when type == typeof(int?) => new NullableConverter<int>(TryParseInt),
        var type when type == typeof(long) => new TypedConverter<long>(TryParseLong),
        var type when type == typeof(long?) => new NullableConverter<long>(TryParseLong),
        _ => null
    };

    private static bool TryParseString(ReadOnlySpan<char> value, out string result)
    {
        result = value.ToString();
        return true;
    }

    private static bool TryParseDateTime(ReadOnlySpan<char> value, out DateTime result)
        => DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

    private static bool TryParseDateOnly(ReadOnlySpan<char> value, out DateOnly result)
        => DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

    private static bool TryParseTimeOnly(ReadOnlySpan<char> value, out TimeOnly result)
        => TimeOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

    private static bool TryParseDecimal(ReadOnlySpan<char> value, out decimal result)
        => decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);

    private static bool TryParseDouble(ReadOnlySpan<char> value, out double result)
        => double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);

    private static bool TryParseFloat(ReadOnlySpan<char> value, out float result)
        => float.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);

    private static bool TryParseInt(ReadOnlySpan<char> value, out int result)
        => int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);

    private static bool TryParseLong(ReadOnlySpan<char> value, out long result)
        => long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);

    protected delegate bool TryParseValue<T>(ReadOnlySpan<char> value, [MaybeNullWhen(false)] out T result);

    private class TypedConverter<T>(TryParseValue<T> parser) : RouteValueConverter
    {
        public override bool TryParse(ReadOnlySpan<char> value, [MaybeNullWhen(false)] out object? result)
        {
            if (parser(value, out var parsedValue))
            {
                result = parsedValue;
                return true;
            }

            result = null;
            return false;
        }

        public override object? Parse(ReadOnlySpan<char> value, string destinationName)
        {
            if (!parser(value, out var result))
            {
                throw new InvalidOperationException(
                    $"Cannot parse the value '{value.ToString()}' as type '{typeof(T)}' for '{destinationName}'.");
            }

            return result;
        }

        public override Array ParseMultiple(StringValues values, string destinationName)
        {
            var result = new T?[values.Count];
            for (var i = 0; i < values.Count; i++)
            {
                if (!parser(values[i].AsSpan(), out result[i]))
                {
                    throw new InvalidOperationException(
                        $"Cannot parse the value '{values[i]}' as type '{typeof(T)}' for '{destinationName}'.");
                }
            }

            return result;
        }
    }

    private sealed class NullableConverter<T> : TypedConverter<T?> where T : struct
    {
        public NullableConverter(TryParseValue<T> parser)
            : base(CreateNullableParser(parser))
        {
        }

        private static TryParseValue<T?> CreateNullableParser(TryParseValue<T> parser)
        {
            return TryParseNullable;

            bool TryParseNullable(ReadOnlySpan<char> value, out T? result)
            {
                if (value.IsEmpty)
                {
                    result = null;
                    return true;
                }

                if (parser(value, out var parsedValue))
                {
                    result = parsedValue;
                    return true;
                }

                result = null;
                return false;
            }
        }
    }
}
