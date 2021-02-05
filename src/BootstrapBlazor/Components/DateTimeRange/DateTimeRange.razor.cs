// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class DateTimeRange
    {
        /// <summary>
        /// 获得 组件 DOM 实例
        /// </summary>
        private ElementReference PickerRange { get; set; }

        /// <summary>
        /// 获得 组件样式名称
        /// </summary>
        private string? ClassString => CssBuilder.Default("datetime-range")
            .AddClass("disabled", IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 组件弹窗样式名称
        /// </summary>
        private string? BodyClassString => CssBuilder.Default("datetime-range-body collapse")
            .AddClass("show", IsShown)
            .Build();

        /// <summary>
        /// 获得 组件弹窗位置
        /// </summary>
        private string? PlacementString => CssBuilder.Default()
            .AddClass("top", Placement == Placement.Top)
            .AddClass("bottom", Placement == Placement.Bottom)
            .AddClass("left", Placement == Placement.Left)
            .AddClass("right", Placement == Placement.Right)
            .Build();

        private string? BarStyleString => CssBuilder.Default()
            .AddClass("padding-right: 16px;", AllowNull)
            .Build();

        /// <summary>
        /// 获得 组件小图标样式
        /// </summary>
        private string? DateTimePickerIconClassString => CssBuilder.Default("datetime-range-input-icon")
            .AddClass("disabled", IsDisabled)
            .Build();

        /// <summary>
        /// 获得 用户选中的时间范围
        /// </summary>
        internal DateTimeRangeValue SelectedValue { get; } = new DateTimeRangeValue();

        private DateTime StartValue { get; set; }

        private string? StartValueString => (Value == null || Value.Start == DateTime.MinValue) ? null : Value.Start.ToString(DateFormat);

        private DateTime EndValue { get; set; }

        private string? EndValueString => (Value == null || Value.End == DateTime.MinValue) ? null : Value.End.ToString(DateFormat);

        [NotNull]
        private string? StartPlaceHolderText { get; set; }

        [NotNull]
        private string? EndPlaceHolderText { get; set; }

        [NotNull]
        private string? SeparateText { get; set; }

        [NotNull]
        private string? DateFormat { get; set; }

        /// <summary>
        /// 获得/设置 清空按钮文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ClearButtonText { get; set; }

        /// <summary>
        /// 获得/设置 确定按钮文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ConfirmButtonText { get; set; }

        /// <summary>
        /// 获得/设置 弹窗位置 默认为 Auto
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Auto;

        /// <summary>
        /// 获得/设置 是否显示本组件默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool IsShown { get; set; }

        /// <summary>
        /// 获得/设置 是否允许为空 默认为 true
        /// </summary>
        [Parameter]
        public bool AllowNull { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示快捷侧边栏 默认不显示
        /// </summary>
        [Parameter]
        public bool ShowSidebar { get; set; }

        /// <summary>
        /// 获得/设置 侧边栏快捷选项集合
        /// </summary>
        [Parameter]
        [NotNull]
        public IEnumerable<DateTimeRangeSidebarItem>? SidebarItems { get; set; }

        /// <summary>
        /// 点击确认按钮回调委托方法
        /// </summary>
        [Parameter]
        public Func<DateTimeRangeValue, Task>? OnConfirm { get; set; }

        /// <summary>
        /// 点击清空按钮回调委托方法
        /// </summary>
        [Parameter]
        public Func<DateTimeRangeValue, Task>? OnClearValue { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<DateTimeRange>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            StartValue = Value.Start;
            EndValue = Value.End;

            if (StartValue == DateTime.MinValue) StartValue = DateTime.Now;
            if (EndValue == DateTime.MinValue) EndValue = StartValue.AddMonths(1);

            StartPlaceHolderText ??= Localizer[nameof(StartPlaceHolderText)];
            EndPlaceHolderText ??= Localizer[nameof(EndPlaceHolderText)];
            SeparateText ??= Localizer[nameof(SeparateText)];

            ClearButtonText ??= Localizer[nameof(ClearButtonText)];
            ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];

            DateFormat ??= Localizer[nameof(DateFormat)];

            if (SidebarItems == null)
            {
                SidebarItems = new DateTimeRangeSidebarItem[]
                {
                    new DateTimeRangeSidebarItem{ Text = Localizer["Last7Days"], StartDateTime = DateTime.Today.AddDays(-7), EndDateTime = DateTime.Today },
                    new DateTimeRangeSidebarItem{ Text = Localizer["Last30Days"], StartDateTime = DateTime.Today.AddDays(-30), EndDateTime = DateTime.Today },
                    new DateTimeRangeSidebarItem{ Text = Localizer["ThisMonth"], StartDateTime = DateTime.Today.AddDays(1- DateTime.Today.Day), EndDateTime = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddMonths(1).AddDays(-1) },
                    new DateTimeRangeSidebarItem{ Text = Localizer["LastMonth"], StartDateTime = DateTime.Today.AddDays(1- DateTime.Today.Day).AddMonths(-1), EndDateTime = DateTime.Today.AddDays(1- DateTime.Today.Day).AddDays(-1) },
                };
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (!IsDisabled)
                    await JSRuntime.InvokeVoidAsync(PickerRange, "bb_datetimeRange");
            }
        }

        private async Task OnClickSidebarItem(DateTimeRangeSidebarItem item)
        {
            SelectedValue.Start = item.StartDateTime;
            SelectedValue.End = item.EndDateTime;
            StartValue = item.StartDateTime;
            EndValue = StartValue.AddMonths(1);
            await ClickConfirmButton();
        }

        /// <summary>
        /// 点击 清除按钮调用此方法
        /// </summary>
        /// <returns></returns>
        private async Task ClickClearButton()
        {
            Value = new DateTimeRangeValue();
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            if (OnClearValue != null) await OnClearValue(Value);

            StartValue = DateTime.Today;
            EndValue = StartValue.AddMonths(1);
            SelectedValue.Start = DateTime.MinValue;
            SelectedValue.End = DateTime.MinValue;
        }

        /// <summary>
        /// 点击 确认时调用此方法
        /// </summary>
        private async Task ClickConfirmButton()
        {
            Value = SelectedValue;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            if (OnConfirm != null) await OnConfirm(Value);
            await JSRuntime.InvokeVoidAsync(PickerRange, "bb_datetimeRange", "hide");
        }

        /// <summary>
        /// 更新年时间方法
        /// </summary>
        /// <param name="d"></param>
        internal void UpdateStart(DateTime d)
        {
            StartValue = StartValue.AddYears(d.Year - StartValue.Year).AddMonths(d.Month - StartValue.Month);
            EndValue = StartValue.AddMonths(1);
            StateHasChanged();
        }

        /// <summary>
        /// 更新年时间方法
        /// </summary>
        /// <param name="d"></param>
        internal void UpdateEnd(DateTime d)
        {
            EndValue = EndValue.AddYears(d.Year - EndValue.Year).AddMonths(d.Month - EndValue.Month);
            StartValue = EndValue.AddMonths(-1);
            StateHasChanged();
        }

        /// <summary>
        /// 更新值方法
        /// </summary>
        /// <param name="d"></param>
        internal void UpdateValue(DateTime d)
        {
            if (SelectedValue.Start == DateTime.MinValue)
            {
                SelectedValue.Start = d;
            }
            else if (SelectedValue.End == DateTime.MinValue)
            {
                if (d < SelectedValue.Start)
                {
                    SelectedValue.End = SelectedValue.Start;
                    SelectedValue.Start = d;
                }
                else
                {
                    SelectedValue.End = d;
                }
            }
            else
            {
                SelectedValue.Start = d;
                SelectedValue.End = DateTime.MinValue;
            }

            if (d.Year < StartValue.Year || d.Month < StartValue.Month)
            {
                UpdateStart(d);
            }
            else if (d.Year > EndValue.Year || d.Month > EndValue.Month)
            {
                UpdateEnd(d);
            }
            else
            {
                StateHasChanged();
            }
        }
    }
}
