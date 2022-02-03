// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class TableTreeNode<TItem> where TItem : class
{
    private static readonly Lazy<Func<TItem, object?>> GetKeyFunc = new(() =>
    {
        var property = typeof(TItem).GetProperties().FirstOrDefault(p => p.IsDefined(typeof(KeyAttribute)));
        if (property == null)
        {
            return _ => null;
        }
        HasKey = true;
        var param = Expression.Parameter(typeof(TItem));
        var body = Expression.Property(param, property);
        return Expression.Lambda<Func<TItem, object>>(Expression.Convert(body, typeof(object)), param).Compile();
    });

    /// <summary>
    /// 获取 TItem 是否存在唯一标识
    /// </summary>
    public static bool HasKey { get; private set; }

    /// <summary>
    /// 获得/设置 当前节点值
    /// </summary>
    public TItem Value { get; }

    /// <summary>
    /// 获得/设置 当前节点唯一标识
    /// </summary>
    public object? Key { get; }

    /// <summary>
    /// 获得/设置 是否展开
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    public List<TableTreeNode<TItem>> Children { get; } = new();

    /// <summary>
    /// 获得/设置 是否有子节点
    /// </summary>
    public bool HasChildren { get; set; }

    /// <summary>
    /// 获得/设置 父级节点对象实例
    /// </summary>
    public TableTreeNode<TItem>? Parent { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public TableTreeNode(TItem item)
    {
        Value = item;
        Key = GetKeyFunc.Value(item);
    }
}
