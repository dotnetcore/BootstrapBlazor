using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageService
    {
        List<Action<MessageOption>> Subscribes { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MessageService()
        {
            Subscribes = new List<Action<MessageOption>>();
        }

        /// <summary>
        /// 显示窗口方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public void Show(MessageOption option)
        {
            Subscribes.AsParallel().ForAll(callback => callback.Invoke(option));
        }

        /// <summary>
        /// 订阅弹窗事件
        /// </summary>
        /// <param name="callback"></param>
        internal void Subscribe(Action<MessageOption> callback)
        {
            Subscribes.Add(callback);
        }

        /// <summary>
        /// 退订弹窗事件
        /// </summary>
        /// <param name="callback"></param>
        internal void UnSubscribe(Action<MessageOption> callback)
        {
            Subscribes.Remove(callback);
        }
    }
}
