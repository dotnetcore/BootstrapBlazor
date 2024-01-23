// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 任意选择
/// </summary>
public partial class SelectObject
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("select dropdown select-tree")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? InputClassName => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? AppendClassName => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得 PlaceHolder 属性
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Icon 图标 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    /// <summary>
    /// 获得/设置 下拉箭头 Icon 图标
    /// </summary>
    [Parameter]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否可编辑 默认 false
    /// </summary>
    [Parameter]
    public bool IsEdit { get; set; }

    /// <summary>
    /// 获得/设置 下拉列表内容
    /// </summary>
    [Parameter]
    public RenderFragment<string?>? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 编辑时处理程序
    /// </summary>
    [Parameter]
    public Func<string, Task<string>>? SelectValueChanged { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SelectObject>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// 获得 input 组件 Id 方法
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// 获得/设置 Select 内部 Input 组件 Id
    /// </summary>
    private string? InputId => $"{Id}_input";

    private async Task OnChange(ChangeEventArgs args)
    {
        if (args.Value is string v)
        {
            if (SelectValueChanged != null)
            {
                v = await SelectValueChanged.Invoke(v);
            }
            CurrentValueAsString = v;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectTreeDropdownIcon);
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
    }

    /// <summary>
    /// 设置选中内容
    /// </summary>
    /// <param name="value"></param>
    public void SetSelectValue(string value)
    {
        CurrentValueAsString = value;
        StateHasChanged();
    }
}
