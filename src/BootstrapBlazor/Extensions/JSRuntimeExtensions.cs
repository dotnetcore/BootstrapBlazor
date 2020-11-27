// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// JSRuntime 扩展操作类
    /// </summary>
    internal static class JSRuntimeExtensions
    {
        /// <summary>
        /// 调用 JSInvoke 方法
        /// </summary>
        /// <param name="jsRuntime">IJSRuntime 实例</param>
        /// <param name="el">Element 实例或者组件 Id</param>
        /// <param name="func">Javascript 方法</param>
        /// <param name="args">Javascript 参数</param>
        public static async ValueTask InvokeVoidAsync(this IJSRuntime jsRuntime, object? el = null, string? func = null, params object[] args)
        {
            var paras = new List<object>();
            if (el != null) paras.Add(el);
            if (args != null) paras.AddRange(args);
            await jsRuntime.InvokeVoidAsync($"$.{func}", paras.ToArray());
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
