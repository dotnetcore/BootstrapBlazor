// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 浏览器指纹服务示例
/// </summary>
public partial class BrowserFingers
{
    [Inject]
    [NotNull]
    private IBrowserFingerService? BrowserFingerService { get; set; }

    private string? _code;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _code = await GetFingerCodeAsync();
    }

    private Task<string?> GetFingerCodeAsync() => BrowserFingerService.GetFingerCodeAsync();

    private MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = "GetFingerCodeAsync",
            Description = Localizer["GetFingerCodeAsync"],
            Parameters = " — ",
            ReturnValue = "Task<string?>"
        }
    ];
}
