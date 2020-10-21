using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 时间类型过滤条件
    /// </summary>
    public partial class DateTimeFilter
    {
        private DateTime? Value1 { get; set; }

        private FilterAction Action1 { get; set; } = FilterAction.GreaterThanOrEqual;

        private DateTime? Value2 { get; set; }

        private FilterAction Action2 { get; set; } = FilterAction.LessThanOrEqual;

        [NotNull]
        private IEnumerable<SelectedItem>? Items { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<TableFilter>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            Items = new SelectedItem[]
            {
                new SelectedItem("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"]),
                new SelectedItem("LessThanOrEqual", Localizer["LessThanOrEqual"]),
                new SelectedItem("GreaterThan", Localizer["GreaterThan"]),
                new SelectedItem("LessThan", Localizer["LessThan"]),
                new SelectedItem("Equal", Localizer["Equal"]),
                new SelectedItem("NotEqual", Localizer["NotEqual"])
            };

            base.OnInitialized();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            Value1 = null;
            Value2 = null;
            Action1 = FilterAction.GreaterThanOrEqual;
            Action2 = FilterAction.LessThanOrEqual;
            Count = 0;
            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<FilterKeyValueAction> GetFilterConditions()
        {
            var filters = new List<FilterKeyValueAction>();
            if (Value1 != null) filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value1,
                FilterAction = Action1
            });
            if (Count > 0 && Value2 != null) filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value2,
                FilterAction = Action2,
                FilterLogic = Logic
            });
            return filters;
        }
    }
}
