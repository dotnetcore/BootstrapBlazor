// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
                .Where(p => p.GetCustomAttribute(typeof(SocketDataFieldAttribute), false) != null && p.CanWrite).ToList();
            foreach (var p in properties)
            {
                var attr = p.GetCustomAttribute<SocketDataFieldAttribute>(false);
                if (attr != null)
                {
                    var type = attr.Type;
                    if (type == null)
                    {
                        continue;
                    }

                    var encodingName = attr.EncodingName;
                    var start = attr.Start;
                    var length = attr.Length;

                    if (data.Length >= start + length)
                    {
                        var buffer = data.Slice(start, length);
                        p.SetValue(entity, ParseValue(buffer, type, encodingName));
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
        if (type == typeof(string))
        {
            var encoding = string.IsNullOrEmpty(encodingName) ? Encoding.UTF8 : Encoding.GetEncoding(encodingName);
            return encoding.GetString(buffer.Span);
        }
        else if (type == typeof(int))
        {
            return BitConverter.ToInt32(buffer.Span);
        }
        else if (type == typeof(long))
        {
            return BitConverter.ToInt64(buffer.Span);
        }
        else if (type == typeof(double))
        {
            return BitConverter.ToDouble(buffer.Span);
        }
        else if (type == typeof(byte[]))
        {
            return buffer.ToArray();
        }
        if (type.IsEnum)
        {
            return Enum.ToObject(type, BitConverter.ToInt32(buffer.Span));
        }
        else if (type == typeof(bool))
        {
            return BitConverter.ToBoolean(buffer.Span);
        }
        else if (type == typeof(float))
        {
            return BitConverter.ToSingle(buffer.Span);
        }
        else if (type == typeof(short))
        {
            return BitConverter.ToInt16(buffer.Span);
        }
        else if (type == typeof(ushort))
        {
            return BitConverter.ToUInt16(buffer.Span);
        }
        else if (type == typeof(uint))
        {
            return BitConverter.ToUInt32(buffer.Span);
        }
        else if (type == typeof(ulong))
        {
            return BitConverter.ToUInt64(buffer.Span);
        }
        else
        {
            return ParseValueResolve(buffer, type, encodingName);
        }
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
