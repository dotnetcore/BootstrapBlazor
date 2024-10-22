// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// FieldIdentifier 扩展操作类
/// </summary>
public static class FieldIdentifierExtensions
{
    /// <summary>
    /// 获取显示名称方法
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <returns></returns>
    public static string GetDisplayName(this FieldIdentifier fieldIdentifier) => Utility.GetDisplayName(fieldIdentifier.Model, fieldIdentifier.FieldName);

    /// <summary>
    /// 获取 PlaceHolder 方法
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <returns></returns>
    public static string? GetPlaceHolder(this FieldIdentifier fieldIdentifier) => Utility.GetPlaceHolder(fieldIdentifier.Model, fieldIdentifier.FieldName);

    /// <summary>
    /// 获取显示名称方法
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <returns></returns>
    public static RangeAttribute? GetRange(this FieldIdentifier fieldIdentifier) => Utility.GetRange(fieldIdentifier.Model, fieldIdentifier.FieldName);
}
