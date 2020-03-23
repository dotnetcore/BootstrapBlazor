using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Alert 警告框组件
    /// </summary>
    public abstract class AlertBase : ComponentBase
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
        protected string ClassName => CssBuilder.Default("alert")
          .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
          .AddClass(Class)
        .Build();

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 自定义样式
        /// </summary>
        [Parameter] public string Class { get; set; } = "";

        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        [Parameter] public bool ShowDismiss { get; set; }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 关闭警告框回调方法
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            base.SetParametersAsync(parameters);
            if (!OnClick.HasDelegate)
            {
                OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                {
                    Class = "collapse";
                });
            }
            return Task.CompletedTask;
        }
    }
}
