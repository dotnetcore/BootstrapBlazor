namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗服务类
    /// </summary>
    public class ToastService : PopupServiceBase<ToastOption>
    {
        /// <summary>
        /// Toast 调用成功快捷方法
        /// </summary>
        /// <param name="title">Title 属性</param>
        /// <param name="content">Content 属性</param>
        /// <param name="autoHide">自动隐藏属性默认为 true</param>
        /// <returns></returns>
        public void Success(string? title = null, string? content = null, bool autoHide = true)
        {
            Show(new ToastOption()
            {
                Category = ToastCategory.Success,
                IsAutoHide = autoHide,
                Title = title ?? "",
                Content = content ?? ""
            });
        }

        /// <summary>
        /// Toast 调用错误快捷方法
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="autoHide"></param>
        /// <returns></returns>
        public void Error(string? title = null, string? content = null, bool autoHide = true)
        {
            Show(new ToastOption()
            {
                Category = ToastCategory.Error,
                IsAutoHide = autoHide,
                Title = title ?? "",
                Content = content ?? ""
            });
        }

        /// <summary>
        /// Toast 调用提示信息快捷方法
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="autoHide"></param>
        /// <returns></returns>
        public void Information(string? title = null, string? content = null, bool autoHide = true)
        {
            Show(new ToastOption()
            {
                Category = ToastCategory.Information,
                IsAutoHide = autoHide,
                Title = title ?? "",
                Content = content ?? ""
            });
        }
    }
}
