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
            Callback = callback;
        }
    }
}
