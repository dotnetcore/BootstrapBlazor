using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗服务类
    /// </summary>
    public class ToastService
    {
        List<Action<ToastOption>> Subscribes { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ToastService()
        {
            Subscribes = new List<Action<ToastOption>>();
        }

        /// <summary>
        /// 显示窗口方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public void Show(ToastOption option)
        {
            Subscribes.AsParallel().ForAll(callback => callback.Invoke(option));
        }

        /// <summary>
        /// 订阅弹窗事件
        /// </summary>
        /// <param name="callback"></param>
        internal void Subscribe(Action<ToastOption> callback)
        {
            Subscribes.Add(callback);
        }

        /// <summary>
        /// 退订弹窗事件
        /// </summary>
        /// <param name="callback"></param>
        internal void UnSubscribe(Action<ToastOption> callback)
        {
            Subscribes.Remove(callback);
        }
    }
}
