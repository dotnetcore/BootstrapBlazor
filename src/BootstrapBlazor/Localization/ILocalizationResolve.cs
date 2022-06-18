// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Globalization;

namespace BootstrapBlazor.Localization;

/// <summary>
/// ILocalizationResolve 服务
/// </summary>
public interface ILocalizationResolve
{
    /// <summary>
    /// 通过文化信息与语言键值获取值方法
    /// </summary>
    /// <param name="culture"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    string? GetJsonStringByCulture(CultureInfo culture, string name);
}
