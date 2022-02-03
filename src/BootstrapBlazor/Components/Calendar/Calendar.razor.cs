// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Calendar
{
    [NotNull]
    private string? PreviousYear { get; set; }

    [NotNull]
    private string? NextYear { get; set; }

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
    /// 获得 周日期
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    private string GetWeekDayString(int offset) => $"{Value.AddDays(offset - (int)Value.DayOfWeek).Day}";

    /// <summary>
    /// 获得 周日期样式
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    private string? GetWeekDayClassString(int offset) => CssBuilder.Default("week-header")
            .AddClass("is-today", Value.AddDays(offset - (int)Value.DayOfWeek) == DateTime.Today)
            .Build();

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        PreviousYear = Localizer[nameof(PreviousYear)];
        NextYear = Localizer[nameof(NextYear)];
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
