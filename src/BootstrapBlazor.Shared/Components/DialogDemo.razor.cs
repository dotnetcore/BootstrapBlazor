// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public partial class DialogDemo
{
    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    public string? Value { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Value = DateTime.Now.ToString();
    }

    private Task OnClickButton() => DialogService.Show(new DialogOption()
    {
        Title = $"弹窗 {Value}",
        Component = BootstrapDynamicComponent.CreateComponent<DialogDemo>()
    });
}
