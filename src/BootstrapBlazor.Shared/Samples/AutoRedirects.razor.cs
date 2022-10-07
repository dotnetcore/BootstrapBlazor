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
    [NotNull]
    private BlockLogger? Trace { get; set; }

    private Task<bool> OnBeforeRedirectAsync()
    {
        Trace.Log("准备跳转地址");
        return Task.FromResult(true);
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        new AttributeItem() {
            Name = nameof(AutoRedirect.Interval),
            Description = "Time interval",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "60000"
        },
        new AttributeItem() {
            Name = nameof(AutoRedirect.RedirectUrl),
            Description = "Redirect address",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(AutoRedirect.IsForceLoad),
            Description = "Whether to force redirection",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(AutoRedirect.OnBeforeRedirectAsync),
            Description = "Callback method before address jump",
            Type = "Func<Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
