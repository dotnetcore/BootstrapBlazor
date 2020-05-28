using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 弹窗类服务基类
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    public abstract class PopupServiceBase<TOption>
    {
        private List<Func<TOption, Task>> Subscribes { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PopupServiceBase()
        {
            Subscribes = new List<Func<TOption, Task>>(100);
        }

        /// <summary>
        /// 显示窗口方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public void Show(TOption option)
        {
            Subscribes.ForEach(async callback => await callback.Invoke(option));
        }

        /// <summary>
        /// 订阅弹窗事件
        /// </summary>
        /// <param name="callback"></param>
        internal void Subscribe(Func<TOption, Task> callback)
        {
            Subscribes.Add(callback);
        }

        /// <summary>
        /// 退订弹窗事件
        /// </summary>
        /// <param name="callback"></param>
        internal void UnSubscribe(Func<TOption, Task> callback)
        {
            Subscribes.Remove(callback);
        }
    }
}
