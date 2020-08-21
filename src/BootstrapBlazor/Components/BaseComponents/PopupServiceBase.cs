using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 弹窗类服务基类
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    public abstract class PopupServiceBase<TOption>
    {
        private Func<TOption, Task>? Callback { get; set; }

        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual void Show(TOption option)
        {
            Callback?.Invoke(option);
        }

        /// <summary>
        /// 注册弹窗事件
        /// </summary>
        /// <param name="callback"></param>
        internal void Register(Func<TOption, Task> callback)
        {
            if (Callback != null)
            {
                (string ServiceName, string ServiceTag) context = ("未知服务", "<Unknown />");
                if (GetType() == typeof(DialogService)) context = (nameof(DialogService), "<Dialog />");
                else if (GetType() == typeof(MessageService)) context = (nameof(MessageService), "<Message />");
                else if (GetType() == typeof(ToastService)) context = (nameof(ToastService), "<Toast />");

                throw new InvalidOperationException($"{context.ServiceName} 服务已经注册，请移除重复标签 {context.ServiceTag}");
            }
            Callback = callback;
        }
    }
}
