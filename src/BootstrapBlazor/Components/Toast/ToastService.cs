// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.Extensions.Options;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗服务类
    /// </summary>
    public class ToastService : PopupServiceBase<ToastOption>, IDisposable
    {
        private IDisposable? _optionsReloadToken;
        private BootstrapBlazorOptions _option;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="option"></param>
        public ToastService(IOptionsMonitor<BootstrapBlazorOptions> option)
        {
            _option = option.CurrentValue;
            _optionsReloadToken = option.OnChange(op => _option = op);
        }

        /// <summary>
        /// Show 方法
        /// </summary>
        /// <param name="option"></param>
        public override void Show(ToastOption option)
        {
            if (!option.ForceDelay && _option.ToastDelay != 0) option.Delay = _option.ToastDelay;

            base.Show(option);
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _optionsReloadToken?.Dispose();
                _optionsReloadToken = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
