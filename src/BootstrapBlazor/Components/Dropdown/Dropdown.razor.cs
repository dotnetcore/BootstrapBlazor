// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// Dropdown 下拉框组件
/// </summary>
public partial class Dropdown<TValue>
{
    /// <summary>
    /// 获得 按钮弹出方向集合
    /// </summary>
    /// <returns></returns>
    private string? DirectionClassName => CssBuilder.Default("btn-group")
        .AddClass(Direction.ToDescriptionString())
        .AddClass($"{Direction.ToDescriptionString()}-center", MenuAlignment == Alignment.Center && (Direction == Direction.Dropup || Direction == Direction.Dropdown))
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ButtonClassName => CssBuilder.Default("btn")
        .AddClass("dropdown-toggle", !ShowSplit)
        .AddClass($"btn-primary", Color == Color.None)
        .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .Build();

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default("btn dropdown-toggle")
        .AddClass("dropdown-toggle-split")
        .AddClass($"btn-primary", Color == Color.None)
        .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .Build();

    /// <summary>
    /// 获得 是否分裂式按钮
    /// </summary>
    private string? DropdownToggle => !ShowSplit ? ToggleString : null;

    /// <summary>
    /// 菜单对齐方式样式
    /// </summary>
    private string? MenuAlignmentClass => CssBuilder.Default("dropdown-menu shadow")
        .AddClass($"dropdown-menu-{MenuAlignment.ToDescriptionString()}", MenuAlignment == Alignment.Right)
        .Build();

    /// <summary>
    /// 获得/设置 设置当前项是否 Active 方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", () => item.Value == CurrentValueAsString)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 选项模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 按钮内容模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem?>? ButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否开启分裂式 默认 false
    /// </summary>
    [Parameter]
    public bool ShowSplit { get; set; }

    /// <summary>
    /// 获得/设置 OnClick 事件
    /// <para><see cref="ShowSplit"/> 为 true 时生效</para>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// 获得/设置 OnClick 事件不刷新父组件
    /// <para><see cref="ShowSplit"/> 为 true 时生效</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickWithoutRender { get; set; }

    /// <summary>
    /// 获得/设置 是否为异步按钮，默认为 false 如果为 true 表示是异步按钮，点击按钮后禁用自身并且等待异步完成，过程中显示 loading 动画
    /// <para><see cref="ShowSplit"/> 为 true 时生效</para>
    /// </summary>
    [Parameter]
    public bool IsAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否异步结束后是否保持禁用状态，默认为 false
    /// </summary>
    /// <remarks><see cref="IsAsync"/> 开启时有效</remarks>
    [Parameter]
    public bool IsKeepDisabled { get; set; }

    /// <summary>
    /// 获得/设置 显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 正在加载动画图标 默认为 fa-solid fa-spin fa-spinner
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 获取菜单对齐方式 默认 none 未设置
    /// </summary>
    [Parameter]
    public Alignment MenuAlignment { get; set; }

    /// <summary>
    /// 获得/设置 下拉选项方向 默认 Dropdown 向下
    /// </summary>
    [Parameter]
    public Direction Direction { get; set; }

    /// <summary>
    /// 获得/设置 组件尺寸 默认 none 未设置
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// 获得/设置 是否固定按钮文字 更改下拉框选项时按钮文字保持不变 默认 false 不固定
    /// </summary>
    [Parameter]
    public bool IsFixedButtonText { get; set; }

    /// <summary>
    /// 获得/设置 下拉菜单中是否显示固定文字 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFixedButtonTextInDropdown { get; set; }

    /// <summary>
    /// 获得/设置 固定按钮显示文字 默认 null
    /// </summary>
    [Parameter]
    public string? FixedButtonText { get; set; }

    /// <summary>
    ///  获得/设置 Items 模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? ItemsTemplate { get; set; }

    /// <summary>
    /// SelectedItemChanged 回调方法
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// 获得 IconTheme 实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    [NotNull]
    private List<SelectedItem>? DataSource { get; set; }

    /// <summary>
    /// 当前选择项实例
    /// </summary>
    private SelectedItem? SelectedItem { get; set; }

    /// <summary>
    /// 获得/设置 是否当前正在异步执行操作
    /// </summary>
    private bool _isAsyncLoading;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 合并 Items 与 Options 集合
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
    /// 下拉框选项点击时调用此方法
    /// </summary>
    protected async Task OnItemClick(SelectedItem item)
    {
        item.Active = true;
        SelectedItem = item;
        CurrentValueAsString = item.Value;

        // 触发 SelectedItemChanged 事件
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
        if (IsAsync)
        {
            IsDisabled = IsKeepDisabled;
            _isAsyncLoading = false;
        }
        StateHasChanged();
    }


    /// <summary>
    /// 处理点击方法
    /// </summary>
    /// <returns></returns>
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
