using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 自动生成客户端 ID 组件基类
    /// </summary>
    public abstract class IdComponentBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 组件 id 属性
        /// </summary>
        [Parameter] public virtual string? Id { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            // 客户端组件生成后通过 invoke 生成客户端组件 id
            if (firstRender)
            {
                if (JSRuntime != null && string.IsNullOrEmpty(Id))
                {
                    // 生成 Id
                    Id = await JSRuntime.InvokeAsync<string>(func: "getUID");
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }
            }
        }
    }
}
