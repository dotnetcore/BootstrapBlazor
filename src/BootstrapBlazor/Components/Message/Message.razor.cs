// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Message 组件
/// </summary>
public partial class Message
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("message")
        .AddClass("is-bottom", Placement != Placement.Top)
        .Build();

    /// <summary>
    /// 获得 Toast 组件样式设置
    /// </summary>
    private string? StyleName => CssBuilder.Default()
        .AddClass("top: 1rem;", Placement != Placement.Bottom)
        .AddClass("bottom: 1rem;", Placement == Placement.Bottom)
        .Build();

    private readonly List<MessageOption> _messages = [];

    private IEnumerable<MessageOption> MessagesForRender => Placement == Placement.Bottom
        ? _messages.AsEnumerable().Reverse()
        : _messages;

    /// <summary>
    /// 获得/设置 显示位置 默认为 Top
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Top;

    /// <summary>
    /// ToastServices 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    public MessageService? MessageService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 注册 Message 弹窗事件
        MessageService.Register(this, Show);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop);

    private static string? GetAutoHideString(MessageOption option) => option.IsAutoHide ? "true" : null;

    private static string? GetItemClassString(MessageOption option) => CssBuilder.Default("alert")
        .AddClass($"alert-{option.Color.ToDescriptionString()}", option.Color != Color.None)
        .AddClass($"border-{option.Color.ToDescriptionString()}", option.ShowBorder)
        .AddClass("shadow", option.ShowShadow)
        .AddClass("alert-bar", option.ShowBar)
        .AddClass(option.ClassString)
        .Build();

    private static string? GetItemStyleString(MessageOption option) => CssBuilder.Default(option.StyleString)
        .Build();

    private string GetItemId(MessageOption option) => $"{Id}_{option.GetHashCode()}";

    private string? _msgId;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!string.IsNullOrEmpty(_msgId))
        {
            await InvokeVoidAsync("show", Id, _msgId);
        }
    }

    /// <summary>
    /// 设置 容器位置方法
    /// </summary>
    /// <param name="placement"></param>
    public void SetPlacement(Placement placement)
    {
        Placement = placement;
        StateHasChanged();
    }

    private async Task Show(MessageOption option)
    {
        if (option.ShowMode == MessageShowMode.Single)
        {
            _messages.Clear();
        }

        if (!_messages.Contains(option))
        {
            _messages.Add(option);
        }
        _msgId = GetItemId(option);
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// 清除 Message 方法 由 JSInvoke 触发
    /// </summary>
    [JSInvokable]
    public void Clear(string id)
    {
        var option = _messages.Find(i => GetItemId(i) == id);
        if (option != null)
        {
            _messages.Remove(option);
        }

        StateHasChanged();
    }

    /// <summary>
    /// OnDismiss 回调方法 由 JSInvoke 触发
    /// </summary>
    /// <param name="id"></param>
    [JSInvokable]
    public async ValueTask Dismiss(string id)
    {
        var option = _messages.Find(i => GetItemId(i) == id);
        if (option is { OnDismiss: not null })
        {
            await option.OnDismiss();
            _messages.Remove(option);
            StateHasChanged();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            MessageService.UnRegister(this);
        }
    }
}
