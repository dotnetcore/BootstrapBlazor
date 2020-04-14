using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// JSRuntime 扩展操作类
    /// </summary>
    internal static class JSRuntimeExtensions
    {
        /// <summary>
        /// 浏览器执行脚本方法
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="id"></param>
        /// <param name="func"></param>
        /// <param name="args"></param>
        public static void InvokeRun(this IJSRuntime? jsRuntime, string? id, string func, params string[] args)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var para = args != null ? string.Join(",", args.Select(p => $"\"{p}\"")) : "";
                jsRuntime?.InvokeVoidAsync("$.run", $"$('#{id}').{func}({para})");
            }
        }

        /// <summary>
        /// 执行客户端脚本得到一个唯一的客户端 id
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static ValueTask<string> GetClientIdAsync(this IJSRuntime? jsRuntime) => jsRuntime?.InvokeAsync<string>("$.getUID") ?? new ValueTask<string>("");

        /// <summary>
        /// 弹出 Tooltip 组件
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="id"></param>
        /// <param name="method"></param>
        /// <param name="popoverType"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="html"></param>
        public static void Tooltip(this IJSRuntime? jsRuntime, string? id, string method = "", PopoverType popoverType = PopoverType.Tooltip, string? title = "", string? content = "", bool html = false)
        {
            if (!string.IsNullOrEmpty(id)) jsRuntime.InvokeVoidAsync(popoverType == PopoverType.Tooltip ? "$.tooltip" : "$.popover", id, method, title, content, html);
        }

        /// <summary>
        /// 弹出 Toast 弹窗
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="id"></param>
        /// <param name="ref"></param>
        /// <param name="method"></param>
        public static void ShowToast(this IJSRuntime? jsRuntime, string id, object @ref, string method) => jsRuntime?.InvokeVoidAsync("$.showToast", id, @ref, method);

        /// <summary>
        /// 初始化 Carousel 组件
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="id"></param>
        public static void Carousel(this IJSRuntime? jsRuntime, string id) => jsRuntime?.InvokeVoidAsync("$.carousel", id);

        /// <summary>
        /// 初始化 Slider 组件
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="id"></param>
        /// <param name="ref"></param>
        /// <param name="method"></param>
        public static void Slider(this IJSRuntime? jsRuntime, string id, object @ref, string method) => jsRuntime?.InvokeVoidAsync("$.slider", id, @ref, method);

        /// <summary>
        /// 初始化 ConfirmPopover 组件
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="element"></param>
        /// <param name="method"></param>
        public static void Confirm(this IJSRuntime? jsRuntime, ElementReference element, string method) => jsRuntime?.InvokeVoidAsync("$.confirm", element, method);
    }
}
