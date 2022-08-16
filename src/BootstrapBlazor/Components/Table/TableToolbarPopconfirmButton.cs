// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class TableToolbarPopconfirmButton<TItem> : PopConfirmButtonBase
{
    /// <summary>
    /// 获得/设置 是否显示 默认 true 显示
    /// </summary>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// 获得/设置 Table Toolbar 实例
    /// </summary>
    [CascadingParameter]
    protected TableToolbar<TItem>? Toolbar { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Toolbar?.AddButton(this);

        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        Content ??= Localizer[nameof(Content)];
    }

    /// <summary>
    /// DisposeAsyncCore 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsyncCore(bool disposing)
    {
        Toolbar?.RemoveButton(this);
        await base.DisposeAsyncCore(disposing);
    }
}
