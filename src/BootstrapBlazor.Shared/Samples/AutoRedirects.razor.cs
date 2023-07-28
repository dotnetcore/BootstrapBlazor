// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// AutoRedirects
/// </summary>
public partial class AutoRedirects
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task<bool> OnBeforeRedirectAsync()
    {
        Logger.Log("Ready to redirect");
        return Task.FromResult(true);
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        new() {
            Name = nameof(AutoRedirect.Interval),
            Description = "Time interval",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "60000"
        },
        new() {
            Name = nameof(AutoRedirect.RedirectUrl),
            Description = "Redirect address",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(AutoRedirect.IsForceLoad),
            Description = "Whether to force redirection",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(AutoRedirect.OnBeforeRedirectAsync),
            Description = "Callback method before address jump",
            Type = "Func<Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
