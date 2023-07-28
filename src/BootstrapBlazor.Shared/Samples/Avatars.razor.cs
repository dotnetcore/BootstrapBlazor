// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Avatars 组件
/// </summary>
public sealed partial class Avatars
{
    private static async Task<string> GetUrlAsync()
    {
        // 模拟异步获取图像地址
        await Task.Delay(500);
        return "./images/Argo-C.png";
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
    };
}
