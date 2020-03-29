using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// CollapseBody 组件
    /// </summary>
    public class CollapseBody : ComponentBase
    {
        /// <summary>
        /// 获得 折叠展示样式集合
        /// </summary>
        protected string? CollapseClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected bool IsCollapsing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected string? Id { get; set; }

        /// <summary>
        /// 是否展开折叠面板
        /// </summary>
        [Parameter] public bool IsCollapsed { get; set; } = true;

        /// <summary>
        /// 获得/设置 子组件实例
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject] IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            CollapseClass = CssBuilder.Default("collapse")
                .AddClass("collapsing", IsCollapsing)
                .AddClass("show", !IsCollapsed)
                .Build();
        }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            builder.OpenElement(index++, "div");
            builder.AddAttribute(index++, "class", CollapseClass);
            builder.AddAttribute(index++, "id", Id);

            builder.OpenElement(index++, "div");
            builder.AddAttribute(index++, "class", "card card-body");
            builder.AddContent(index++, ChildContent);
            builder.CloseElement();

            builder.CloseElement();
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // 客户端组件生成后通过 invoke 生成客户端组件 id
            if (firstRender)
            {
                await base.OnAfterRenderAsync(firstRender);

                Id = await JSRuntime.GetClientIdAsync();
            }
        }

        /// <summary>
        /// 动画支持
        /// </summary>
        /// <returns></returns>
        public Task DoAnimationsAsync(bool collapsed) => Task.Run(async () =>
        {
            // 生成 Id
            await InvokeAsync(StateHasChanged).ConfigureAwait(false);

            // 调用客户端动画效果
            if (!string.IsNullOrEmpty(Id)) JSRuntime.Collapse(Id, collapsed);
        });
    }
}
