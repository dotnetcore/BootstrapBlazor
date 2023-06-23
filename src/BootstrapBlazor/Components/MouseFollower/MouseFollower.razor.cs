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
    /// 获得/设置 Text 文本
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 Icon 图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 ImagePath 路径
    /// </summary>
    [Parameter]
    public string? ImagePath { get; set; }

    /// <summary>
    /// 获得/设置 VideoPath 路径
    /// </summary>
    [Parameter]
    public string? VideoPath { get; set; }

    /// <summary>
    /// 存在游标元素。如果未指定，将自动创建游标。
    /// Existed cursor element. If not specified, the cursor will be created automatically.
    /// </summary>
    [Parameter]
    public RenderFragment? FollowerTemplate { get; set; }

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

        await InvokeVoidAsync("init", GlobalMode, FollowerTemplate, Container, Options);

        switch (FollowerMode)
        {
            case MouseFollowerMode.Text:
                await InvokeVoidAsync("SetText", Container, Text);
                break;
            case MouseFollowerMode.Icon:
                await InvokeVoidAsync("SetIcon", Container, Icon);
                break;
            case MouseFollowerMode.Image:
                await InvokeVoidAsync("SetImage", Container, ImagePath);
                break;
            case MouseFollowerMode.Video:
                await InvokeVoidAsync("SetVideo", Container, VideoPath);
                break;
            case MouseFollowerMode.Normal:
                await InvokeVoidAsync("SetNormal", Container, Options);
                break;
            default:
                await InvokeVoidAsync("SetNormal", Container, Options);
                break;
        }
    }
}
