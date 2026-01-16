// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableCellPopConfirmButton 单元格内按钮组件</para>
/// <para lang="en">TableCellPopConfirmButton 单元格内buttoncomponent</para>
/// </summary>
[JSModuleNotInherited]
public class TableCellPopConfirmButton : PopConfirmButtonBase, ITableCellButton
{
    /// <summary>
    /// <para lang="zh">获得/设置 Table 扩展按钮集合实例</para>
    /// <para lang="en">Gets or sets Table 扩展buttoncollectioninstance</para>
    /// </summary>
    [CascadingParameter]
    protected TableExtensionButton? Buttons { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoSelectedRowWhenClick { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoRenderTableWhenClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示本按钮 默认 true 显示</para>
    /// <para lang="en">Gets or sets whetherdisplay本button Default is true display</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>一般是通过 context 进行业务判断是否需要显示功能按钮</remarks>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized 方法</para>
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
    /// <para lang="zh">DisposeAsyncCore</para>
    /// <para lang="en">DisposeAsyncCore</para>
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override ValueTask DisposeAsync(bool disposing)
    {
        Buttons?.RemoveButton(this);
        return base.DisposeAsync(disposing);
    }
}
