// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// CustomPopConfirmContent 自定义组件
/// </summary>
public partial class CustomPopConfirmContent
{
    [Inject, NotNull]
    private IStringLocalizer<CustomPopConfirmContent>? Localizer { get; set; }

    [CascadingParameter(Name = "PopoverConfirmButtonCloseAsync")]
    [NotNull]
    private Func<Task>? OnCloseAsync { get; set; }

    private async Task OnClick()
    {
        await OnCloseAsync();
    }
}
