// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Avatars 组件
/// </summary>
public sealed partial class Avatars
{
    private async Task<string> GetUrlAsync()
    {
        // 模拟异步获取图像地址
        await Task.Delay(500);
        return $"{WebsiteOption.CurrentValue.AssetRootPath}images/Argo-C.png";
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Size",
            Description = Localizer["Size"],
            Type = "Size",
            ValueList = "ExtraSmall|Small|Medium|Large|ExtraLarge|ExtraExtraLarge",
            DefaultValue = "None"
        },
        new()
        {
            Name = "IsBorder",
            Description = Localizer["IsBorder"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsCircle",
            Description = Localizer["IsCircle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsIcon",
            Description = Localizer["IsIcon"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsText",
            Description = Localizer["IsText"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Icon",
            Description = Localizer["Icon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-user"
        },
        new()
        {
            Name = "Text",
            Description = Localizer["Text"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Url",
            Description = Localizer["Url"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "GetUrlAsync",
            Description = Localizer["GetUrlAsync"],
            Type = "Func<Task<string>>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
