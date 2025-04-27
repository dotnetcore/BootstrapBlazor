// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone


namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// OtpServices sample
/// </summary>
public partial class OtpServices
{
    [Inject]
    [NotNull]
    private IStringLocalizer<OtpServices>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ITotpService? TotpService { get; set; }

    private string _content = "";

    private string? _code;

    private double _progress = 0;

    private int _remain = 0;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _content = TotpService.GenerateOtpUri(new OtpOptions()
        {
            AccountName = "BootstrapBlazor",
            IssuerName = "BootstrapBlazor",
            UserName = "Simulator",
            SecretKey = "OMM2LVLFX6QJHMYI",
            Algorithm = OtpHashMode.Sha1,
            Type = OtpType.Totp
        });
        _code = TotpService.Compute("OMM2LVLFX6QJHMYI");
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
        _code = TotpService.Compute("OMM2LVLFX6QJHMYI");
        _remain = TotpService.Instance.GetRemainingSeconds();
        _progress = (30d - _remain) * 100 / 30d;
        StateHasChanged();
    }
}
