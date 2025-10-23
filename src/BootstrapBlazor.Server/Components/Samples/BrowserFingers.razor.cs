// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    private string? _clientHubId;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _code = await GetFingerCodeAsync();
        _clientHubId = await GetClientHubIdAsync();
    }

    private Task<string?> GetFingerCodeAsync() => BrowserFingerService.GetFingerCodeAsync();

    private Task<string?> GetClientHubIdAsync() => BrowserFingerService.GetClientHubIdAsync();

    private MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = "GetFingerCodeAsync",
            Description = Localizer["GetFingerCodeAsync"],
            Parameters = " — ",
            ReturnValue = "Task<string?>"
        },
        new()
        {
            Name = "GetClientHubIdAsync",
            Description = Localizer["GetClientHubIdAsync"],
            Parameters = " — ",
            ReturnValue = "Task<string?>"
        }
    ];
}
