namespace BootstrapBlazor.Components;

/// <summary>
/// MouseFollower
/// 鼠标跟随器组件
/// </summary>
[BootstrapModuleAutoLoader("MouseFollower/MouseFollower.razor.js", JSObjectReference = true, AutoInvokeInit = false)]
public partial class MouseFollower
{
    /// <summary>
    /// 获得/设置 RenderFragment 实例
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 EChart DOM 元素实例
    /// </summary>
    private ElementReference Element { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await InvokeVoidAsync("init");
        }
    }
}
