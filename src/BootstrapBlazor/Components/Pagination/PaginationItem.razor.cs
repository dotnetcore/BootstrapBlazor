using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// PaginationItem 组件
    /// </summary>
    public partial class PaginationItem
    {
        private string? ClassName => CssBuilder.Default("page-item")
            .AddClass("active", Active)
            .Build();

        /// <summary>
        /// 获得/设置 是否 Active 状态
        /// </summary>
        [Parameter]
        public bool Active { get; set; }

        /// <summary>
        /// 获得/设置 页码
        /// </summary>
        [Parameter]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 获得/设置 点击页码时回调方法
        /// </summary>
        [Parameter]
        public EventCallback<int> OnClick { get; set; }

        [NotNull]
        private string? LabelString { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Pagination>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            LabelString ??= Localizer[nameof(LabelString)];
        }

        private string GetLabelString => string.Format(LabelString, PageIndex);
    }
}
