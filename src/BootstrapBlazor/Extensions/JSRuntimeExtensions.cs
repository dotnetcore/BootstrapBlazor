using BootstrapBlazor.Components;
using System.Threading.Tasks;

namespace Microsoft.JSInterop
{
    /// <summary>
    /// JSRuntime 扩展操作类
    /// </summary>
    internal static class JSRuntimeExtensions
    {
        /// <summary>
        /// 根据指定菜单 ID 激活侧边栏菜单项
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static void ActiveMenu(this IJSRuntime? jsRuntime, string? menuId)
        {
            if (!string.IsNullOrEmpty(menuId) && jsRuntime != null) jsRuntime.InvokeVoidAsync("$.activeMenu", menuId);
        }

        /// <summary>
        /// 导航条前移一个 Tab
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <returns></returns>
        public static async ValueTask<string> MovePrevTabAsync(this IJSRuntime? jSRuntime) => jSRuntime == null ? "" : await jSRuntime.InvokeAsync<string>("$.movePrevTab");

        /// <summary>
        /// 导航条后移一个 Tab
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <returns></returns>
        public static async ValueTask<string> MoveNextTabAsync(this IJSRuntime? jSRuntime) => jSRuntime == null ? "" : await jSRuntime.InvokeAsync<string>("$.moveNextTab");

        /// <summary>
        /// 移除指定 ID 的导航条
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public static async ValueTask<string> RemoveTabAsync(this IJSRuntime? jSRuntime, string? tabId) => string.IsNullOrEmpty(tabId) || jSRuntime == null ? "" : await jSRuntime.InvokeAsync<string>("$.removeTab", tabId);

        /// <summary>
        /// 启用动画
        /// </summary>
        /// <param name="jSRuntime"></param>
        public static void InitDocument(this IJSRuntime? jSRuntime) => jSRuntime.InvokeVoidAsync("$.initDocument");

        /// <summary>
        /// 弹出 Modal 组件
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="modalId"></param>
        public static void ToggleModal(this IJSRuntime? jSRuntime, string modalId) => jSRuntime.InvokeVoidAsync("$.toggleModal", modalId);

        /// <summary>
        /// 弹出 Toast 组件
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="cate"></param>
        public static void ShowToast(this IJSRuntime? jSRuntime, string title, string message, ToastCategory cate) => jSRuntime.InvokeVoidAsync("$.showToast", title, message, cate.ToString());

        /// <summary>
        /// 弹出 Tooltip 组件
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="id"></param>
        /// <param name="method"></param>
        public static void Tooltip(this IJSRuntime? jSRuntime, string id, string method) => jSRuntime.InvokeVoidAsync("$.tooltip", $"#{id}", method);

        /// <summary>
        /// 显示或者隐藏 网站 Blazor 挂件图标
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="show"></param>
        public static void ToggleBlazor(this IJSRuntime? jSRuntime, bool show) => jSRuntime.InvokeVoidAsync("$.toggleBlazor", show);

        /// <summary>
        /// 显示或者隐藏 网站 Blazor 挂件图标
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="showSidebar"></param>
        /// <param name="showCardTitle"></param>
        /// <param name="fixedTableHeader"></param>
        public static void SetWebSettings(this IJSRuntime? jSRuntime, bool showSidebar, bool showCardTitle, bool fixedTableHeader) => jSRuntime.InvokeVoidAsync("$.setWebSettings", showSidebar, showCardTitle, fixedTableHeader);

        /// <summary>
        /// 初始化 Table 组件
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="id"></param>
        /// <param name="firstRender"></param>
        public static ValueTask InitTableAsync(this IJSRuntime? jSRuntime, string id, bool firstRender) => jSRuntime.InvokeVoidAsync("$.initTable", id, firstRender);
    }
}
