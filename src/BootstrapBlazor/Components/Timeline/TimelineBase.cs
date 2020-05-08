using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 时间线组件基类
    /// </summary>
    public abstract class TimelineBase : IdComponentBase
    {
        /// <summary>
        /// 获得 Timeline 样式
        /// </summary>
        protected string? ClassName => CssBuilder.Default("timeline")
            .Build();

        /// <summary>
        /// 获得/设置 绑定数据集
        /// </summary>
        [Parameter]
        public IEnumerable<TimelineItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 是否反转
        /// </summary>
        [Parameter]
        public bool Reverse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (Reverse)
            {
                var arr = Items.Reverse();
                Items = arr;
            }
        }
    }
}
