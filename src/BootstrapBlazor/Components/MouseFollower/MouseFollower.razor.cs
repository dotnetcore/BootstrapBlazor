namespace BootstrapBlazor.Components;

/// <summary>
/// MouseFollower
/// 鼠标跟随器组件
/// </summary>
[BootstrapModuleAutoLoader("MouseFollower/MouseFollower.razor.js", JSObjectReference = true)]
public partial class MouseFollower
{
    /// <summary>
    /// 获得/设置 Container 容器
    /// </summary>
    private ElementReference? Container { get; set; }

    /// <summary>
    /// 获得/设置 GlobalMode 全局模式
    /// </summary>
    [Parameter]
    public bool GlobalMode { get; set; } = false;

    /// <summary>
    /// 存在游标元素。如果未指定，将自动创建游标。
    /// Existed cursor element. If not specified, the cursor will be created automatically.
    /// </summary>
    [Parameter]
    public RenderFragment? FollowerTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Body 子元素
    /// </summary>
    [Parameter]
    public RenderFragment? FollowerBody { get; set; }

    /// <summary>
    /// MouseFollowerOptions
    /// </summary>
    [Parameter]
    public MouseFollowerOptions? Options { get; set; }

    /// <inheritdoc/>
    protected override async Task InvokeInitAsync()
    {
        //await base.InvokeInitAsync();
        if (Options is null)
        {
            Options = new MouseFollowerOptions();
        }

        await InvokeVoidAsync("init", GlobalMode, FollowerTemplate, Container, Options);
    }
}
