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
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Calendar
    {
        [NotNull]
        private string? PreviousMonth { get; set; }

        [NotNull]
        private string? NextMonth { get; set; }

        [NotNull]
        private string? Today { get; set; }

        [NotNull]
        private string? PreviousWeek { get; set; }

        [NotNull]
        private string? NextWeek { get; set; }

        [NotNull]
        private string? WeekText { get; set; }

        [NotNull]
        private List<string>? WeekLists { get; set; }

        [NotNull]
        private string? WeekHeaderText { get; set; }

        [NotNull]
        private List<string>? Months { get; set; }

        /// <summary>
        /// 获得 当前日历框年月
        /// </summary>
        private string? GetTitle() => Localizer["Title", Value.Year, Months.ElementAt(Value.Month - 1)];

        /// <summary>
        /// 获得 当前日历周文字
        /// </summary>
        [NotNull]
        private string? WeekNumberText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Calendar>? Localizer { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            PreviousMonth = Localizer[nameof(PreviousMonth)];
            NextMonth = Localizer[nameof(NextMonth)];
            Today = Localizer[nameof(Today)];
            WeekLists = Localizer[nameof(WeekLists)].Value.Split(',').ToList();
            PreviousWeek = Localizer[nameof(PreviousWeek)];
            NextWeek = Localizer[nameof(NextWeek)];
            WeekText = Localizer[nameof(WeekText)];
            WeekHeaderText = Localizer[nameof(WeekHeaderText)];
            WeekNumberText = Localizer[nameof(WeekNumberText), GetWeekCount()];
            Months = Localizer[nameof(Months)].Value.Split(',').ToList();
        }
    }
}
