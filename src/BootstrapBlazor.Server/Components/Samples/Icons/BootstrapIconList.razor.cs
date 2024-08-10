using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Server.Components.Samples.Icons;

/// <summary>
/// BootstrapIconList
/// </summary>
public partial class BootstrapIconList : ComponentBase
{
    /// <summary>
    /// 获得/设置 拷贝成功提示文字
    /// </summary>
    [Parameter]
    public string? CopiedTooltipText { get; set; }
}

