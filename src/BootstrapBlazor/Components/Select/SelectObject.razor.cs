// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件实现类
/// </summary>
[CascadingTypeParameter(nameof(TItem))]
public partial class SelectObject<TItem>
{
    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 是否显示组件右侧扩展箭头 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowAppendArrow { get; set; } = true;

    /// <summary>
    /// 获得/设置 弹窗最小宽度 默认为 null 未设置使用样式中的默认值
    /// </summary>
    [Parameter]
    public int? DropdownMinWidth { get; set; }

    /// <summary>
    /// 获得 显示文字回调方法 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public Func<TItem, string?>? GetTextCallback { get; set; }

    /// <summary>
    /// 获得/设置 右侧下拉箭头图标 默认 fa-solid fa-angle-up
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否可清除 默认 false
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// 获得/设置 右侧清除图标 默认 fa-solid fa-angle-up
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// 获得/设置 下拉列表内容模板
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public RenderFragment<ISelectObjectContext<TItem>>? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 IIconTheme 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("select select-object dropdown")
        .AddClass("disabled", IsDisabled)
        .AddClass("cls", IsClearable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? InputClassName => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(FieldClass, IsNeedValidate)
        .AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? AppendClassString => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// 获得 PlaceHolder 属性
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 弹窗高度 默认 486px;
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 486;

    /// <summary>
    /// 获得/设置 Value 显示模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? Template { get; set; }

    /// <summary>
    /// 获得/设置 清除文本内容 OnClear 回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TItem>>? Localizer { get; set; }

    /// <summary>
    /// 获得 input 组件 Id 方法
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// 获得/设置 内部 Input 组件 Id
    /// </summary>
    private string InputId => $"{Id}_input";

    private string GetStyleString => $"height: {Height}px;";

    private ISelectObjectContext<TItem> _context = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AddRequiredValidator();
        _context = new InternalSelectObjectContext<TItem>() { Component = this };
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (GetTextCallback == null)
        {
            throw new InvalidOperationException("Please set GetTextCallback value");
        }

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectDropdownIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectClearIcon);
    }

    private bool GetClearable() => IsClearable && !IsDisabled;

    /// <summary>
    /// 获得 Text 显示文字
    /// </summary>
    /// <returns></returns>
    private string? GetText() => GetTextCallback(Value);

    /// <summary>
    /// 关闭当前弹窗方法
    /// </summary>
    /// <returns></returns>
    public Task CloseAsync() => InvokeVoidAsync("close", Id);

    private async Task OnClearValue()
    {
        if (OnClearAsync != null)
        {
            await OnClearAsync();
        }

        CurrentValue = default;
        await CloseAsync();
    }
}
