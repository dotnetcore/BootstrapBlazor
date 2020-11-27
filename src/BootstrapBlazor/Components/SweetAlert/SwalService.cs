// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
