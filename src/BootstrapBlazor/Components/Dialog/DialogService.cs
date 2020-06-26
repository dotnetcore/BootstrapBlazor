using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Dialog 组件服务
    /// </summary>
    public class DialogService : PopupServiceBase<DialogOption>
    {
        /// <summary>
        /// 
        /// </summary>
        protected DialogOption? CurrentOption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        public override void Show(DialogOption option)
        {
            CurrentOption?.Modal?.Toggle();
            CurrentOption = option;
            base.Show(option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task Close() => CurrentOption?.Modal?.Toggle() ?? Task.CompletedTask;
    }
}
