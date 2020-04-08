using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗服务类
    /// </summary>
    public class ToastService
    {
        /// <summary>
        /// Toast 弹窗集合
        /// </summary>
        ObservableCollection<ToastOption> ToastPool { get; set; }

        List<Action<ToastOption>> Subscribes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ToastService()
        {
            Subscribes = new List<Action<ToastOption>>();
            ToastPool = new ObservableCollection<ToastOption>();
        }

        /// <summary>
        /// 显示窗口方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public void Show(ToastOption option)
        {
            ToastPool.Add(option);
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
