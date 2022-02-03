// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 单元格内按钮组件
/// </summary>
public class TableCellPopconfirmButton : PopConfirmButtonBase, IDisposable
{
    /// <summary>
    /// 获得/设置 Table 扩展按钮集合实例
    /// </summary>
    [CascadingParameter]
    protected TableExtensionButton? Buttons { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 点击按钮是否选中正行 默认 true 选中
    /// </summary>
    [Parameter]
    public bool AutoSelectedRowWhenClick { get; set; } = true;

    /// <summary>
    /// 获得/设置 点击按钮是否选中正行 默认 true 选中
    /// </summary>
    [Parameter]
    public bool AutoRenderTableWhenClick { get; set; }

    /// <summary>
    /// 获得/设置 是否显示本按钮 默认 true 显示
    /// </summary>
    /// <remarks>一般是通过 context 进行业务判断是否需要显示功能按钮</remarks>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Buttons?.AddButton(this);

        if (Size == Size.None)
        {
            Size = Size.ExtraSmall;
        }

        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        Content ??= Localizer[nameof(Content)];
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Buttons?.RemoveButton(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
