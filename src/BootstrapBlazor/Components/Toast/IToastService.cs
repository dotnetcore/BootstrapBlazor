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
        /// 
        /// </summary>
        /// <param name="callback"></param>
        void Subscribe(Action<ToastOption> callback);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        void UnSubscribe(Action<ToastOption> callback);

        ///// <summary>
        ///// 增加弹窗时回调方法
        ///// </summary>
        //Action<NotifyCollectionChangedEventArgs>? OnAdd { get; set; }

        ///// <summary>
        ///// 移除弹窗时回调方法
        ///// </summary>
        //Action<NotifyCollectionChangedEventArgs>? OnRemove { get; set; }
    }
}
