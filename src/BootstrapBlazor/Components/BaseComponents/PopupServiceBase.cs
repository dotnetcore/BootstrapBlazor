using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 弹窗类服务基类
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    public abstract class PopupServiceBase<TOption>
    {
        private List<(int Key, Func<TOption, Task> Callback)> Callbacks { get; set; } = new List<(int, Func<TOption, Task>)>();

        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual void Show(TOption option)
        {
            Callbacks.LastOrDefault().Callback.Invoke(option);
        }

        /// <summary>
        /// 注册弹窗事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        internal void Register(int key, Func<TOption, Task> callback)
        {
            Callbacks.Add((key, callback));
        }

        /// <summary>
        /// 注销弹窗事件
        /// </summary>
        internal void UnRegister(int key)
        {
            var item = Callbacks.FirstOrDefault(i => i.Key == key);
            if (item != default) Callbacks.Remove(item);
        }
    }
}
