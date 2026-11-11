// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// <para lang="zh">日期组件弹窗示例</para>
/// </summary>
public partial class DatePickerDialogForm
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DatePickerDialogForm>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表单日期。</para>
    /// <para lang="en">Gets or sets the form date.</para>
    /// </summary>
    [Required]
    public DateTime? DateValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义侧边栏日期。</para>
    /// <para lang="en">Gets or sets the custom sidebar date.</para>
    /// </summary>
    public DateTime? SidebarDateValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表单日期时间。</para>
    /// <para lang="en">Gets or sets the form date and time.</para>
    /// </summary>
    public DateTime? DateTimeValue { get; set; } = DateTime.Now;

    /// <summary>
    /// <para lang="zh">获得/设置 表单日期范围。</para>
    /// <para lang="en">Gets or sets the form date range.</para>
    /// </summary>
    public DateTimeRangeValue RangeValue { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 自定义侧边栏日期范围。</para>
    /// <para lang="en">Gets or sets the custom sidebar date range.</para>
    /// </summary>
    public DateTimeRangeValue SidebarRangeValue { get; set; } = new();
}
