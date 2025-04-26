// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone


namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Authenticators sample
/// </summary>
public partial class Authenticators
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Anchors>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ITOTPService? TOTPService { get; set; }

    private string _content = "";

    private string? _code;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _content = TOTPService.GenerateOtpUri();
        _code = TOTPService.Compute("OMM2LVLFX6QJHMYI");
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        await Task.Delay(1000);
        _code = TOTPService.Compute("OMM2LVLFX6QJHMYI");
        StateHasChanged();
    }
}
