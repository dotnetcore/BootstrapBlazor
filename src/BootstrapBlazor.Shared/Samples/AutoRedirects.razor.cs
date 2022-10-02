// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class AutoRedirects
{
    [Inject]
    [NotNull]
    private IStringLocalizer<AutoRedirects>? Localizer { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private Task<bool> OnBeforeRedirectAsync()
    {
        Trace.Log("准备跳转地址");
        return Task.FromResult(true);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = nameof(AutoRedirect.Interval),
            Description = "时间间隔",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "60000"
        },
        new AttributeItem() {
            Name = nameof(AutoRedirect.RedirectUrl),
            Description = "重定向地址",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(AutoRedirect.IsForceLoad),
            Description = "是否强制重定向",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(AutoRedirect.OnBeforeRedirectAsync),
            Description = "地址跳转前回调方法",
            Type = "Func<Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
