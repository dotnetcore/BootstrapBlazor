// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Cameras
    {
#nullable disable
        /// <summary>
        /// 
        /// </summary>
        private Logger Trace { get; set; }
#nullable restore

        private Task OnInit(IEnumerable<DeviceItem> devices)
        {
            var cams = string.Join("", devices.Select(i => i.Label));
            Trace?.Log($"初始化摄像头完成 {cams}");
            return Task.CompletedTask;
        }

        private Task OnError(string err)
        {
            Trace?.Log($"发生错误 {err}");
            return Task.CompletedTask;
        }

        private Task OnStart()
        {
            Trace?.Log("打开摄像头");
            return Task.CompletedTask;
        }

        private Task OnClose()
        {
            Trace?.Log("关闭摄像头");
            return Task.CompletedTask;
        }

        private Task OnCapture()
        {
            Trace?.Log("拍照完成");
            return Task.CompletedTask;
        }
    }
}
