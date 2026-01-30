// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Alert 组件示例
/// </summary>
public partial class Alerts
{
    [NotNull]
    private string? Title { get; set; }

    [NotNull]
    private string? SubTitle { get; set; }

    [NotNull]
    private string? BaseUsageText { get; set; }

    [NotNull]
    private string? CloseButtonUsageText { get; set; }

    [NotNull]
    private string? WithIconUsageText { get; set; }

    [NotNull]
    private string? ShowBarUsageText { get; set; }

    [NotNull]
    private string? IntroText1 { get; set; }

    [NotNull]
    private string? IntroText2 { get; set; }

    [NotNull]
    private string? IntroText3 { get; set; }

    [NotNull]
    private string? IntroText4 { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Alerts>? Localizer { get; set; }

    [NotNull]
    private string? AlertPrimaryText { get; set; }

    [NotNull]
    private string? AlertSecondaryText { get; set; }

    [NotNull]
    private string? AlertSuccessText { get; set; }

    [NotNull]
    private string? AlertDangerText { get; set; }

    [NotNull]
    private string? AlertWarningText { get; set; }

    [NotNull]
    private string? AlertInfoText { get; set; }

    [NotNull]
    private string? AlertDarkText { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Title ??= Localizer[nameof(Title)];
        SubTitle ??= Localizer[nameof(SubTitle)];
        BaseUsageText ??= Localizer[nameof(BaseUsageText)];
        IntroText1 ??= Localizer[nameof(IntroText1)];
        CloseButtonUsageText ??= Localizer[nameof(CloseButtonUsageText)];
        IntroText2 ??= Localizer[nameof(IntroText2)];
        WithIconUsageText ??= Localizer[nameof(WithIconUsageText)];
        IntroText3 ??= Localizer[nameof(IntroText3)];
        ShowBarUsageText ??= Localizer[nameof(ShowBarUsageText)];
        IntroText4 ??= Localizer[nameof(IntroText4)];
        AlertPrimaryText ??= Localizer[nameof(AlertPrimaryText)];
        AlertSecondaryText ??= Localizer[nameof(AlertSecondaryText)];
        AlertDangerText ??= Localizer[nameof(AlertDangerText)];
        AlertSuccessText ??= Localizer[nameof(AlertSuccessText)];
        AlertWarningText ??= Localizer[nameof(AlertWarningText)];
        AlertInfoText ??= Localizer[nameof(AlertInfoText)];
        AlertDarkText ??= Localizer[nameof(AlertDarkText)];
    }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    /// <summary>
    /// 关闭警告框回调方法
    /// </summary>
    /// <returns></returns>
    private Task DismissClick()
    {
        Logger.Log("Alert Dismissed");
        return Task.CompletedTask;
    }
}
