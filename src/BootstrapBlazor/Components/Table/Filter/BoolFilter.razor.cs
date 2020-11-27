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
                new SelectedItem("", Localizer["BoolFilter.AllText"] ?? "All"),
                new SelectedItem("true", Localizer["BoolFilter.TrueText"] ?? "True"),
                new SelectedItem("false", Localizer["BoolFilter.FalseText"] ?? "False")
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
