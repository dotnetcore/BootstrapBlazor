using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Switch 开关组件
    /// </summary>
    public abstract class CollapseBase : BootstrapComponentBase
    {
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        protected override string? ClassName => CssBuilder.Default("btn")
            .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None )
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("disabled",  IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();



        /// <summary>
        /// 是否a标签链接
        /// </summary>
        [Parameter] public bool IsLink { get; set; } = false;

        /// <summary>
        /// 是否展开折叠面板
        /// </summary>
        [Parameter] public bool IsExpanded { get; set; } = false;


        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        ///
        /// </summary>
        [Parameter] public Size Size { get; set; } = Size.None;

        /// <summary>
        /// 获得 开关 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "true" : null;

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        [Parameter] public string? Value { get; set; }

        /// <summary>
        /// 获得/设置 组件 id 属性
        /// </summary>
        [Parameter]
        public override string? Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 链接Id
        /// </summary>
        protected string? LinkId => "#" + Id;




    }
}
