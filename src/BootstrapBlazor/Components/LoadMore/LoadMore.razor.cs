namespace BootstrapBlazor.Components;

public partial class LoadMore
    {
    /// <summary>
    /// 初始化加载JS
    /// </summary>
    /// <returns></returns>
        protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Invoke = Interop, OnLoading = nameof(OnLoading)});
    /// <summary>
    /// 是否刷新数据成功
    /// </summary>
    private bool IsSuccess = true;
    /// <summary>
    /// 加载数据方法由 JSInvoke 调用
    /// </summary>
    [JSInvokable]
    public async Task OnLoading()
    {
        if (OnLoadingAsync != null)
        {
            IsSuccess = await OnLoadingAsync();

            if (!IsSuccess)
            {
                await InvokeAsync(StateHasChanged);
            }
        }
    }
    /// <summary>
    /// 加载数据的方法
    /// </summary>
    [Parameter]
    public Func<Task<bool>>? OnLoadingAsync { get; set; }
    /// <summary>
    /// 加载数据的方法
    /// </summary>
    [Parameter]
    public Func<Task<bool>>? OnFailRetryAsync { get; set; }
    /// <summary>
    /// 重新获取数据方法
    /// </summary>
    public async Task OnRetryAsync()
    {
        IsSuccess = true;
        if (OnFailRetryAsync != null)
        {
            IsSuccess = await OnFailRetryAsync();
        }
        await InvokeAsync(StateHasChanged);
    }
    /// <summary>
    /// 加载文字--默认无文字
    /// </summary>
    [Parameter]
    public string? LoadingText { get; set; }
    /// <summary>
    /// 加载失败文字--默认获取失败
    /// </summary>
    [Parameter]
    public string? FailText { get; set; } = "获取失败";
    /// <summary>
    /// 默认css样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-load-more")
        .AddClass("text-center")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
    /// <summary>
    /// 默认获取失败css样式
    /// </summary>
    private string? FailClassString => CssBuilder.Default("bb-load-more-fail-text")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
    /// <summary>
    /// 默认获取失败css样式
    /// </summary>
    private string? FailButtonClassString => CssBuilder.Default("bb-load-more-fail-button")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
    /// <summary>
    /// 默认加载图标样式
    /// </summary>
    private string? ClassLoadingIcon => CssBuilder.Default("fa fa-spinner fa-spin")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
    /// <summary>
    /// Style样式
    /// </summary>
    private string? StyleString => CssBuilder.Default()
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();
}

