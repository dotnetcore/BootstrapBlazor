using BootstrapBlazor.Enums;

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
    /// 获得/设置 MouseFollowerMode 模式
    /// </summary>
    [Parameter]
    public MouseFollowerMode FollowerMode { get; set; } = MouseFollowerMode.Normal;

    /// <summary>
    /// 获得/设置 GlobalMode 全局模式
    /// </summary>
    [Parameter]
    public bool GlobalMode { get; set; } = false;

    /// <summary>
    /// 获得/设置 Content 文本，图标，图片路径，视频路径
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 RenderFragment 实例
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// MouseFollowerOptions
    /// </summary>
    [Parameter]
    public MouseFollowerOptions? Options { get; set; }

    /// <inheritdoc/>
    protected override async Task InvokeInitAsync()
    {
        Options ??= new MouseFollowerOptions();

        await InvokeVoidAsync("init", GlobalMode, Container, Options);

        switch (FollowerMode)
        {
            case MouseFollowerMode.Text:
                await InvokeVoidAsync("SetText", Container, Content);
                break;
            case MouseFollowerMode.Icon:
                await InvokeVoidAsync("SetIcon", Container, Content);
                break;
            case MouseFollowerMode.Image:
                await InvokeVoidAsync("SetImage", Container, Content);
                break;
            case MouseFollowerMode.Video:
                await InvokeVoidAsync("SetVideo", Container, Content);
                break;
            case MouseFollowerMode.Normal:
                await InvokeVoidAsync("SetNormal", Container, Options);
                break;
            default:
                await InvokeVoidAsync("SetNormal", Container, Options);
                break;
        }
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await InvokeVoidAsync("destory", Container);
        await base.DisposeAsync(disposing);
    }
}
