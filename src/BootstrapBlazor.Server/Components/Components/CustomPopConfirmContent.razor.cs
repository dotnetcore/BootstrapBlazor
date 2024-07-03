// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
