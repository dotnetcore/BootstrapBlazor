using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Progress 进度条组件
    /// </summary>
    public abstract class ProgressBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected override string? ClassName => CssBuilder.Default("progress-bar")
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("progress-bar-striped", IsStriped)
            .AddClass("progress-bar-animated", IsAnimated)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Style 集合
        /// </summary>
        protected string? StyleName => CssBuilder.Default()
            .AddClass($"width:{SetValue}%;")
            .Build();

        /// <summary>
        /// 获得 HeightStyle 集合
        /// </summary>
        protected string? StyleHeightName => CssBuilder.Default()
            .AddClass($"height:{Height}px;")
            .Build();

        /// <summary>
        /// 设置值进度条的值
        /// </summary>
        public int? setValue = 0;

        /// <summary>
        /// 获得/设置 控件高度默认 20px
        /// </summary>
        [Parameter] public int Height { get; set; } = 15;

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 是否显示进度条值
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsShowValue { get; set; }

        /// <summary>
        /// 获得/设置 是否显示为条纹
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsStriped { get; set; } = false;

        /// <summary>
        /// 获得/设置 是否动画
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsAnimated { get; set; } = false;

        /// <summary>
        /// 获得/设置 组件进度值
        /// </summary>
        [Parameter]
        public int? SetValue
        {
            get
            {
                return setValue;
            }
            set
            {
                if (value <= 0)
                {
                    setValue = 0;
                }
                else if (value >= 100)
                {
                    setValue = 100;
                }
                setValue = value;
            }
        }

    }
}
