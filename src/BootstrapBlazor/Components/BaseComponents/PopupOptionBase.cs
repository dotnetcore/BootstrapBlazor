using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 弹窗类配置项基类
    /// </summary>
    public abstract class PopupOptionBase
    {
        /// <summary>
        /// 获得/设置 Toast Body 子组件
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 是否自动隐藏
        /// </summary>
        public bool IsAutoHide { get; set; } = true;

        /// <summary>
        /// 获得/设置 自动隐藏时间间隔
        /// </summary>
        public int Delay { get; set; } = 4000;
    }
}
