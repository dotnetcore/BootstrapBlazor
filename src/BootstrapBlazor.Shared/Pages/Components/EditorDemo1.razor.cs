// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EditorDemo1
    {
#nullable disable
        private Logger Trace { get; set; }
#nullable restore

        private readonly Foo Model = new Foo();

        private class Foo
        {
            [Required]
            [DisplayName("姓名")]
            public string? Name { get; set; }
        }

        private Task OnSubmit(EditContext context)
        {
            Trace.Log("OnSubmit 回调委托");
            return Task.CompletedTask;
        }
    }
}
