using Microsoft.JSInterop;
using System.Collections.Generic;
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
        /// Invoke $.run 方法
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
        /// 调用 JSInvoke 方法
        /// </summary>
        /// <param name="jsRuntime">IJSRuntime 实例</param>
        /// <param name="el">Element 实例或者组件 Id</param>
        /// <param name="ref">DotNetObjectReference 实例</param>
        /// <param name="func">Javascript 方法</param>
        /// <param name="args">Javascript 参数</param>
        public static void Invoke(this IJSRuntime? jsRuntime, object? el = null, string? func = null, object? @ref = null, params object[] args)
        {
            var paras = new List<object>();
            if (el != null) paras.Add(el);
            if (@ref != null) paras.Add(@ref);
            if (args != null) paras.AddRange(args);
            jsRuntime?.InvokeVoidAsync($"$.{func}", paras.ToArray());
        }

        /// <summary>
        /// 调用 JSInvoke 方法
        /// </summary>
        /// <param name="jsRuntime">IJSRuntime 实例</param>
        /// <param name="el">Element 实例或者组件 Id</param>
        /// <param name="func">Javascript 方法</param>
        /// <param name="args">Javascript 参数</param>
        public static async ValueTask<TValue> InvokeAsync<TValue>(this IJSRuntime jsRuntime, object? el = null, string? func = null, params object[] args)
        {
            var paras = new List<object>();
            if (el != null) paras.Add(el);
            if (args != null) paras.AddRange(args);
            return await jsRuntime.InvokeAsync<TValue>($"$.{func}", paras.ToArray());
        }
    }
}
