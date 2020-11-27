// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Pagination
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Pagination>? Localizer { get; set; }

        [NotNull]
        private string? AiraPageLabel { get; set; }

        [NotNull]
        private string? AiraPrevPageText { get; set; }

        [NotNull]
        private string? AiraFirstPageText { get; set; }

        [NotNull]
        private string? AiraNextPageText { get; set; }

        [NotNull]
        private string? PrePageInfoText { get; set; }

        [NotNull]
        private string? RowInfoText { get; set; }

        [NotNull]
        private string? PageInfoText { get; set; }

        [NotNull]
        private string? TotalInfoText { get; set; }

        [NotNull]
        private string? SelectItemsText { get; set; }

        [NotNull]
        private string? LabelString { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            AiraPageLabel ??= Localizer[nameof(AiraPageLabel)];
            AiraPrevPageText ??= Localizer[nameof(AiraPrevPageText)];
            AiraFirstPageText ??= Localizer[nameof(AiraFirstPageText)];
            AiraNextPageText ??= Localizer[nameof(AiraNextPageText)];
            PrePageInfoText ??= Localizer[nameof(PrePageInfoText)];
            RowInfoText ??= Localizer[nameof(RowInfoText)];
            PageInfoText ??= Localizer[nameof(PageInfoText)];
            TotalInfoText ??= Localizer[nameof(TotalInfoText)];
            SelectItemsText ??= Localizer[nameof(SelectItemsText)];
            LabelString ??= Localizer[nameof(LabelString)];
        }

        /// <summary>
        /// 获得页码设置集合
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectedItem> GetPageItems()
        {
            var pages = PageItemsSource ?? new List<int>() { 20, 40, 80, 100, 200 };
            var ret = new List<SelectedItem>();
            for (var i = 0; i < pages.Count(); i++)
            {
                var item = new SelectedItem(pages.ElementAt(i).ToString(), string.Format(SelectItemsText, pages.ElementAt(i)));
                ret.Add(item);
                if (pages.ElementAt(i) >= TotalCount) break;
            }
            return ret;
        }

        private string GetPageInfoText() => string.Format(PageInfoText, StarIndex, EndIndex);

        private string GetTotalInfoText() => string.Format(TotalInfoText, TotalCount);

        private string GetLabelString => string.Format(LabelString, PageIndex);
    }
}
