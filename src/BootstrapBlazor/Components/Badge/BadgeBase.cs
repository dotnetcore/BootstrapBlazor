using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Badge 徽章组件
    /// </summary>
    public abstract class BadgeBase : ComponentBase
    {
        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        /// <returns></returns>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected string ClassName => CssBuilder.Default("badge")
          .AddClass($"badge-{Color.ToDescriptionString()}", Color != Color.None)
          .AddClass($"badge-pill", IsPill)
          .AddClass(Class)
        .Build();

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 样式名称
        /// </summary>
        [Parameter] public string Class { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否显示为胶囊形式
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsPill { get; set; }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
