// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 字符串类型过滤条件
    /// </summary>
    public partial class StringFilter
    {
        private string Value1 { get; set; } = "";

        private FilterAction Action1 { get; set; } = FilterAction.GreaterThanOrEqual;

        private string Value2 { get; set; } = "";

        private FilterAction Action2 { get; set; } = FilterAction.LessThanOrEqual;

        [Inject]
        [NotNull]
        private IStringLocalizer<TableFilter>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override FilterLogic Logic { get; set; } = FilterLogic.Or;

        [NotNull]
        private IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = new SelectedItem[]
            {
                new SelectedItem("Contains", Localizer["Contains"] ?? "Contains"),
                new SelectedItem("Equal", Localizer["Equal"] ?? "Equal"),
                new SelectedItem("NotEqual", Localizer["NotEqual"] ?? "NotEqual"),
                new SelectedItem("NotContains", Localizer["NotContains"] ?? "NotContains")
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            Value1 = "";
            Value2 = "";
            Action1 = FilterAction.Contains;
            Action2 = FilterAction.Contains;
            Logic = FilterLogic.Or;
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
            if (!string.IsNullOrEmpty(Value1)) filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value1,
                FilterAction = Action1
            });
            if (Count > 0 && !string.IsNullOrEmpty(Value2)) filters.Add(new FilterKeyValueAction()
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
