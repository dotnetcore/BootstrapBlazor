// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Buffers.Binary;
using System.Reflection;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base class for converting socket data into a specified entity type.
/// </summary>
/// <typeparam name="TEntity">The type of entity to convert the socket data into.</typeparam>
public abstract class SocketDataConverterBase<TEntity> : ISocketDataConverter<TEntity>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract bool TryConvertTo(ReadOnlyMemory<byte> data, [NotNullWhen(true)] out TEntity? entity);

    /// <summary>
    /// 将字节数据转换为指定实体类型的实例。
    /// </summary>
    /// <param name="data"></param>
    /// <param name="entity"></param>
    protected virtual bool Parse(ReadOnlyMemory<byte> data, TEntity entity)
    {
        // 使用 SocketDataFieldAttribute 特性获取数据转换规则
        var ret = false;
        if (entity != null)
        {
            var properties = entity.GetType().GetProperties()
                .Where(p => p.CanWrite).ToList();
            foreach (var p in properties)
            {
                var attr = p.GetCustomAttribute<SocketDataFieldAttribute>(false);
                if (attr is { Type: not null })
                {
                    var encodingName = attr.EncodingName;
                    var start = attr.Start;
                    var length = attr.Length;

                    if (data.Length >= start + length)
                    {
                        var buffer = data.Slice(start, length);
                        p.SetValue(entity, ParseValue(buffer, attr.Type, encodingName));
                    }
                }
            }
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为指定类型的值。
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="type"></param>
    /// <param name="encodingName"></param>
    /// <returns></returns>
    protected virtual object? ParseValue(ReadOnlyMemory<byte> buffer, Type type, string? encodingName)
    {
        // 根据类型转换数据
        if (type == typeof(byte[]))
        {
            return ParseByte(buffer);
        }
        else if (type == typeof(string))
        {
            return ParseString(buffer, encodingName);
        }
        else if (type == typeof(int))
        {
            return ParseInt(buffer);
        }
        else if (type == typeof(long))
        {
            return ParseLong(buffer);
        }
        else if (type == typeof(double))
        {
            return ParseDouble(buffer);
        }
        else if (type == typeof(float))
        {
            return ParseSingle(buffer);
        }
        else if (type == typeof(short))
        {
            return ParseShort(buffer);
        }
        else if (type == typeof(ushort))
        {
            return ParseUShort(buffer);
        }
        else if (type == typeof(uint))
        {
            return ParseUInt(buffer);
        }
        else if (type == typeof(ulong))
        {
            return ParseULong(buffer);
        }
        else if (type == typeof(bool))
        {
            return ParseBool(buffer);
        }
        else if (type.IsEnum)
        {
            return ParseEnum(buffer, type);
        }
        else
        {
            return ParseValueResolve(buffer, type, encodingName);
        }
    }

    /// <summary>
    /// 将字节缓冲区转换为字节数组
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual byte[] ParseByte(ReadOnlyMemory<byte> buffer)
    {
        return buffer.ToArray();
    }

    /// <summary>
    /// 将字节缓冲区转换为字符串
    /// </summary>
    /// <returns></returns>
    protected virtual string? ParseString(ReadOnlyMemory<byte> buffer, string? encodingName)
    {
        var encoding = string.IsNullOrEmpty(encodingName) ? Encoding.UTF8 : Encoding.GetEncoding(encodingName);
        return encoding.GetString(buffer.Span);
    }

    /// <summary>
    /// 将字节缓冲区转换为整形
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual int ParseInt(ReadOnlyMemory<byte> buffer)
    {
        var ret = 0;
        Span<byte> paddedSpan = stackalloc byte[4];
        buffer.Span.CopyTo(paddedSpan[(4 - buffer.Length)..]);

        if (BinaryPrimitives.TryReadInt32BigEndian(paddedSpan, out var v))
        {
            ret = v;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为长整形
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual long ParseLong(ReadOnlyMemory<byte> buffer)
    {
        long ret = 0;
        Span<byte> paddedSpan = stackalloc byte[8];
        buffer.Span.CopyTo(paddedSpan[(8 - buffer.Length)..]);

        if (BinaryPrimitives.TryReadInt64BigEndian(paddedSpan, out var v))
        {
            ret = v;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为双精度浮点数
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual float ParseSingle(ReadOnlyMemory<byte> buffer)
    {
        float ret = 0;
        Span<byte> paddedSpan = stackalloc byte[4];
        buffer.Span.CopyTo(paddedSpan[(4 - buffer.Length)..]);
        if (BinaryPrimitives.TryReadSingleBigEndian(paddedSpan, out var v))
        {
            ret = v;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为双精度浮点数
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual short ParseShort(ReadOnlyMemory<byte> buffer)
    {
        short ret = 0;
        Span<byte> paddedSpan = stackalloc byte[2];
        buffer.Span.CopyTo(paddedSpan[(2 - buffer.Length)..]);
        if (BinaryPrimitives.TryReadInt16BigEndian(paddedSpan, out var v))
        {
            ret = v;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为双精度浮点数
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual double ParseDouble(ReadOnlyMemory<byte> buffer)
    {
        double ret = 0;
        Span<byte> paddedSpan = stackalloc byte[8];
        buffer.Span.CopyTo(paddedSpan[(8 - buffer.Length)..]);
        if (BinaryPrimitives.TryReadDoubleBigEndian(paddedSpan, out var v))
        {
            ret = v;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为无符号短整形
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual ushort ParseUShort(ReadOnlyMemory<byte> buffer)
    {
        ushort ret = 0;
        Span<byte> paddedSpan = stackalloc byte[2];
        buffer.Span.CopyTo(paddedSpan[(2 - buffer.Length)..]);
        if (BinaryPrimitives.TryReadUInt16BigEndian(paddedSpan, out var v))
        {
            ret = v;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为无符号整形
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual uint ParseUInt(ReadOnlyMemory<byte> buffer)
    {
        uint ret = 0;
        Span<byte> paddedSpan = stackalloc byte[4];
        buffer.Span.CopyTo(paddedSpan[(4 - buffer.Length)..]);
        if (BinaryPrimitives.TryReadUInt32BigEndian(paddedSpan, out var v))
        {
            ret = v;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为无符号长整形
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual ulong ParseULong(ReadOnlyMemory<byte> buffer)
    {
        ulong ret = 0;
        Span<byte> paddedSpan = stackalloc byte[8];
        buffer.Span.CopyTo(paddedSpan[(8 - buffer.Length)..]);
        if (BinaryPrimitives.TryReadUInt64BigEndian(paddedSpan, out var v))
        {
            ret = v;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为布尔类型
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    protected virtual bool ParseBool(ReadOnlyMemory<byte> buffer)
    {
        var ret = false;
        if (buffer.Length == 1)
        {
            ret = buffer.Span[0] != 0x00;
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为枚举类型
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual object? ParseEnum(ReadOnlyMemory<byte> buffer, Type type)
    {
        object? ret = null;
        if (buffer.Length == 1)
        {
            var v = buffer.Span[0];
            if (Enum.TryParse(type, v.ToString(), out var enumValue))
            {
                ret = enumValue;
            }
        }
        return ret;
    }

    /// <summary>
    /// 将字节缓冲区转换为指定类型的值，允许子类自定义补充解析逻辑。
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="type"></param>
    /// <param name="encodingName"></param>
    /// <returns></returns>
    protected virtual object? ParseValueResolve(ReadOnlyMemory<byte> buffer, Type type, string? encodingName)
    {
        return null;
    }
}
