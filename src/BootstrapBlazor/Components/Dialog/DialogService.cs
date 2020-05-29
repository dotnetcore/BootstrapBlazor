using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Dialog 组件服务
    /// </summary>
    public class DialogService : PopupServiceBase<DialogOption>
    {
        /// <summary>
        /// 显示窗口方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public void Show<TComponent>(DialogOption option) where TComponent : ComponentBase
        {
            option.ComponentType = typeof(TComponent);
            Show(option);
        }
    }
}
