// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

static class SocketDataPropertyExtensions
{
    public static ISocketDataPropertyConverter? GetConverter(this SocketDataPropertyAttribute attribute)
    {
        return attribute.GetConverterByType() ?? attribute.GetDefaultConverter();
    }

    private static ISocketDataPropertyConverter? GetConverterByType(this SocketDataPropertyAttribute attribute)
    {
        ISocketDataPropertyConverter? converter = null;
        var converterType = attribute.ConverterType;
        if (converterType != null)
        {
            var converterParameters = attribute.ConverterParameters;
            var c = Activator.CreateInstance(converterType, converterParameters);
            if(c is ISocketDataPropertyConverter v)
            {
                converter = v;
            }
        }
        return converter;
    }

    private static ISocketDataPropertyConverter? GetDefaultConverter(this SocketDataPropertyAttribute attribute)
    {
        ISocketDataPropertyConverter? converter = null;
        var type = attribute.Type;
        if (type != null)
        {
            if (type == typeof(byte[]))
            {
                converter = new SocketDataByteArrayConverter();
            }
            else if (type == typeof(string))
            {
                converter = new SocketDataStringConverter(attribute.EncodingName);
            }
            else if (type.IsEnum)
            {
                converter = new SocketDataEnumConverter(attribute.Type);
            }
            else if (type == typeof(bool))
            {
                converter = new SocketDataBoolConverter();
            }
            else if (type == typeof(short))
            {
                converter = new SocketDataInt16BigEndianConverter();
            }
            else if (type == typeof(int))
            {
                converter = new SocketDataInt32BigEndianConverter();
            }
            else if (type == typeof(long))
            {
                converter = new SocketDataInt64BigEndianConverter();
            }
            else if (type == typeof(float))
            {
                converter = new SocketDataSingleBigEndianConverter();
            }
            else if (type == typeof(double))
            {
                converter = new SocketDataDoubleBigEndianConverter();
            }
            else if (type == typeof(ushort))
            {
                converter = new SocketDataUInt16BigEndianConverter();
            }
            else if (type == typeof(uint))
            {
                converter = new SocketDataUInt32BigEndianConverter();
            }
            else if (type == typeof(ulong))
            {
                converter = new SocketDataUInt64BigEndianConverter();
            }
        }
        return converter;
    }

    public static object? ConvertTo(this SocketDataPropertyAttribute attribute, ReadOnlyMemory<byte> data)
    {
        object? ret = null;
        var start = attribute.Offset;
        var length = attribute.Length;

        if (data.Length >= start + length)
        {
            var buffer = data.Slice(start, length);
            var converter = attribute.GetConverter();
            if (converter != null)
            {
                ret = converter.Convert(buffer);
            }
        }
        return ret;
    }
}
