﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System.Reflection;

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

    /// <summary>
    /// 获得 <see cref="RequiredValidator"/> 实例
    /// </summary>
    /// <param name="fieldIdentifier"></param>
    /// <param name="localizerFactory"></param>
    /// <returns></returns>
    public static RequiredValidator? GetRequiredValidator(this FieldIdentifier fieldIdentifier, IStringLocalizerFactory localizerFactory)
    {
        RequiredValidator? validator = null;
        var pi = fieldIdentifier.Model.GetType().GetPropertyByName(fieldIdentifier.FieldName);
        if (pi != null)
        {
            var required = pi.GetCustomAttribute<RequiredAttribute>(true);
            if (required != null)
            {
                validator = new RequiredValidator()
                {
                    LocalizerFactory = localizerFactory,
                    ErrorMessage = required.ErrorMessage,
                    AllowEmptyString = required.AllowEmptyStrings
                };
            }
        }
        return validator;
    }
}
