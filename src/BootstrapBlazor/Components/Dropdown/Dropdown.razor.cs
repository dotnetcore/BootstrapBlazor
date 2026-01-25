// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Dropdown 下拉框组件</para>
/// <para lang="en">Dropdown Component</para>
/// </summary>
public partial class Dropdown<TValue>
{
    /// <summary>
    /// <para lang="zh">获得 按钮弹出方向集合</para>
    /// <para lang="en">Get Direction Class Collection</para>
    /// </summary>
    private string? DirectionClassName => CssBuilder.Default("btn-group")
        .AddClass(Direction.ToDescriptionString())
        .AddClass($"{Direction.ToDescriptionString()}-center", MenuAlignment == Alignment.Center && (Direction == Direction.Dropup || Direction == Direction.Dropdown))
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 按钮样式集合</para>
    /// <para lang="en">Get Button Class Collection</para>
    /// </summary>
    private string? ButtonClassName => CssBuilder.Default("btn")
        .AddClass("dropdown-toggle", !ShowSplit)
        .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 按钮样式集合</para>
    /// <para lang="en">Get Button Class Collection</para>
    /// </summary>
    private string? ClassName => CssBuilder.Default("btn dropdown-toggle")
        .AddClass("dropdown-toggle-split")
        .AddClass($"btn-primary", Color == Color.None)
        .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 是否分裂式按钮</para>
    /// <para lang="en">Get Is Split Button</para>
    /// </summary>
    private string? DropdownToggle => !ShowSplit ? ToggleString : null;

    /// <summary>
    /// <para lang="zh">菜单对齐方式样式</para>
    /// <para lang="en">Menu Alignment Class</para>
    /// </summary>
    private string? MenuAlignmentClass => CssBuilder.Default("dropdown-menu shadow")
        .AddClass($"dropdown-menu-{MenuAlignment.ToDescriptionString()}", MenuAlignment == Alignment.Right)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 设置当前项是否 Active 方法</para>
    /// <para lang="en">Gets or sets Set Item Active Method</para>
    /// </summary>
    /// <param name="item"></param>
    protected string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", () => item.Value == CurrentValueAsString)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 颜色 默认 Color.Primary 无设置</para>
    /// <para lang="en">Gets or sets Color. Default is Color.Primary</para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 绑定数据集</para>
    /// <para lang="en">Gets or sets Data Items</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项模板</para>
    /// <para lang="en">Gets or sets Item Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮内容模板</para>
    /// <para lang="en">Gets or sets Button Content Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem?>? ButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启分裂式 默认 false</para>
    /// <para lang="en">Gets or sets Is Split Button. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowSplit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 OnClick 事件</para>
    /// <para lang="zh"><see cref="ShowSplit"/> 为 true 时生效</para>
    /// <para lang="en">Gets or sets OnClick Event</para>
    /// <para lang="en">Effective when <see cref="ShowSplit"/> is true</para>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 OnClick 事件不刷新父组件</para>
    /// <para lang="zh"><see cref="ShowSplit"/> 为 true 时生效</para>
    /// <para lang="en">Gets or sets OnClick Event without render parent</para>
    /// <para lang="en">Effective when <see cref="ShowSplit"/> is true</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickWithoutRender { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为异步按钮，默认为 false 如果为 true 表示是异步按钮，点击按钮后禁用自身并且等待异步完成，过程中显示 loading 动画</para>
    /// <para lang="zh"><see cref="ShowSplit"/> 为 true 时生效</para>
    /// <para lang="en">Gets or sets Is Async Button. Default is false. If true, button is disabled and shows loading animation on click</para>
    /// <para lang="en">Effective when <see cref="ShowSplit"/> is true</para>
    /// </summary>
    [Parameter]
    public bool IsAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否异步结束后是否保持禁用状态，默认为 false</para>
    /// <para lang="en">Gets or sets Keep Disabled after async completion. Default is false</para>
    /// </summary>
    /// <remarks><see cref="IsAsync"/> 开启时有效</remarks>
    [Parameter]
    public bool IsKeepDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示图标</para>
    /// <para lang="en">Gets or sets Icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 正在加载动画图标 默认为 fa-solid fa-spin fa-spinner</para>
    /// <para lang="en">Gets or sets Loading Icon. Default is fa-solid fa-spin fa-spinner</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 获取菜单对齐方式 默认 none 未设置</para>
    /// <para lang="en">Gets or sets Menu Alignment. Default is none</para>
    /// </summary>
    [Parameter]
    public Alignment MenuAlignment { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉选项方向 默认 Dropdown 向下</para>
    /// <para lang="en">Gets or sets Dropdown Direction. Default is Dropdown (down)</para>
    /// </summary>
    [Parameter]
    public Direction Direction { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件尺寸 默认 none 未设置</para>
    /// <para lang="en">Gets or sets Size. Default is none</para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否固定按钮文字 更改下拉框选项时按钮文字保持不变 默认 false 不固定</para>
    /// <para lang="en">Gets or sets Whether Fixed Button Text. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsFixedButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉菜单中是否显示固定文字 默认 false 不显示</para>
    /// <para lang="en">Gets or sets Whether Show Fixed Button Text in Dropdown. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowFixedButtonTextInDropdown { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 固定按钮显示文字 默认 null</para>
    /// <para lang="en">Gets or sets Fixed Button Text. Default is null</para>
    /// </summary>
    [Parameter]
    public string? FixedButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Items 模板 默认 null</para>
    /// <para lang="en">Gets or sets Items Template. Default is null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ItemsTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">SelectedItemChanged 回调方法</para>
    /// <para lang="en">SelectedItemChanged Callback</para>
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得 IconTheme 实例</para>
    /// <para lang="en">Get IconTheme Instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    [NotNull]
    private List<SelectedItem>? DataSource { get; set; }

    /// <summary>
    /// <para lang="zh">当前选择项实例</para>
    /// <para lang="en">Current Selected Item Instance</para>
    /// </summary>
    private SelectedItem? SelectedItem { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否当前正在异步执行操作</para>
    /// <para lang="en">Gets or sets Is Async Loading</para>
    /// </summary>
    private bool _isAsyncLoading;

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet Method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 合并 Items 与 Options 集合
        // Merge Items and Options collection
        Items ??= [];

        if (!Items.Any() && typeof(TValue).IsEnum())
        {
            Items = typeof(TValue).ToSelectList();
        }

        DataSource = [.. Items];

        SelectedItem = DataSource.Find(i => i.Value.Equals(CurrentValueAsString, StringComparison.OrdinalIgnoreCase))
            ?? DataSource.Find(i => i.Active)
            ?? DataSource.FirstOrDefault();

        FixedButtonText ??= SelectedItem?.Text;
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonLoadingIcon);
    }

    private IEnumerable<SelectedItem> GetItems() => (IsFixedButtonText && !ShowFixedButtonTextInDropdown)
        ? Items.Where(i => i.Text != FixedButtonText)
        : Items;

    /// <summary>
    /// <para lang="zh">下拉框选项点击时调用此方法</para>
    /// <para lang="en">Call this when item clicked</para>
    /// </summary>
    protected async Task OnItemClick(SelectedItem item)
    {
        item.Active = true;
        SelectedItem = item;
        CurrentValueAsString = item.Value;

        // 触发 SelectedItemChanged 事件
        // Trigger SelectedItemChanged event
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged.Invoke(SelectedItem);
        }
    }

    private string? ButtonText => IsFixedButtonText ? FixedButtonText : SelectedItem?.Text;

    private async Task OnClickButton()
    {
        if (IsAsync)
        {
            _isAsyncLoading = true;
            IsDisabled = true;
            StateHasChanged();
            await Task.Yield();
        }

        await HandlerClick();

        // 恢复按钮
        // Restore button
        if (IsAsync)
        {
            IsDisabled = IsKeepDisabled;
            _isAsyncLoading = false;
        }
        StateHasChanged();
    }


    /// <summary>
    /// <para lang="zh">处理点击方法</para>
    /// <para lang="en">Handle Click Method</para>
    /// </summary>
    private async Task HandlerClick()
    {
        IsNotRender = true;
        if (OnClickWithoutRender != null)
        {
            await OnClickWithoutRender();
        }
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }
}
