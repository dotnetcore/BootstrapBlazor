using Microsoft.AspNetCore.Components;
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
        private List<(ComponentBase Key, Func<TOption, Task> Callback)> Cache { get; set; } = new List<(ComponentBase, Func<TOption, Task>)>();

        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual void Show(TOption option)
        {
            Func<TOption, Task>? cb = null;
            if (typeof(IPopupHost).IsAssignableFrom(typeof(TOption)))
            {
                var op = option as IPopupHost;
                cb = Cache.FirstOrDefault(i => i.Key == op!.Host).Callback;
            }
            if (cb == null)
            {
                cb = Cache.FirstOrDefault().Callback;
            }
            cb?.Invoke(option);
        }

        /// <summary>
        /// 注册弹窗事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        internal void Register(ComponentBase key, Func<TOption, Task> callback)
        {
            Cache.Add((key, callback));
        }

        /// <summary>
        /// 注销弹窗事件
        /// </summary>
        internal void UnRegister(ComponentBase key)
        {
            var item = Cache.FirstOrDefault(i => i.Key == key);
            if (item.Key != null) Cache.Remove(item);
        }
    }
}
