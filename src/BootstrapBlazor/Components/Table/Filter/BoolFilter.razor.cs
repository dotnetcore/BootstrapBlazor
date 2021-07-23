// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 布尔类型过滤条件
    /// </summary>
    public partial class BoolFilter
    {
        private string Value { get; set; } = "";

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
            base.OnInitialized();

            Items = new SelectedItem[]
            {
                new SelectedItem("", Localizer["BoolFilter.AllText"]?.Value ?? "All"),
                new SelectedItem("true", Localizer["BoolFilter.TrueText"]?.Value ?? "True"),
                new SelectedItem("false", Localizer["BoolFilter.FalseText"]?.Value ?? "False")
            };

            if (TableFilter != null)
            {
                TableFilter.ShowMoreButton = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            Value = "";
            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<FilterKeyValueAction> GetFilterConditions()
        {
            var filters = new List<FilterKeyValueAction>();
            if (!string.IsNullOrEmpty(Value)) filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value == "" ? (object?)null : (Value == "true"),
                FilterAction = FilterAction.Equal
            });
            return filters;
        }
    }
}
