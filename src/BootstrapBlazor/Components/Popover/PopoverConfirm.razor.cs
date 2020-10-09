using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PopoverConfirm
    {
        /// <summary>
        /// 获得/设置 PopoverConfirm 服务实例
        /// </summary>
        [Inject] private PopoverService? PopoverService { get; set; }
    }
}
