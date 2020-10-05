using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// SweetAlert 弹窗服务
    /// </summary>
    public class SwalService : PopupServiceBase<SwalOption>
    {
        /// <summary>
        /// 异步回调方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<bool> ShowModal(SwalOption option)
        {
            var cb = Cache.FirstOrDefault().Callback;
            if (cb != null)
            {
                await cb.Invoke(option);
            }
            return await option.ReturnTask.Task;
        }
    }
}
