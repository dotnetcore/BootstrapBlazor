using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹窗服务接口定义
    /// </summary>
    public interface IToastService
    {
        /// <summary>
        /// 显示 Toast 弹窗方法
        /// </summary>
        /// <param name="option"></param>
        void Show(ToastOption option);

        /// <summary>
        /// 订阅服务
        /// </summary>
        /// <param name="callback"></param>
        void Subscribe(Action<ToastOption> callback);

        /// <summary>
        /// 退订服务
        /// </summary>
        /// <param name="callback"></param>
        void UnSubscribe(Action<ToastOption> callback);
    }
}
