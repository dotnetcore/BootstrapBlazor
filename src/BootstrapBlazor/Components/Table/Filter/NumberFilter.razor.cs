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
    /// 数字类型过滤条件
    /// </summary>
    public partial class NumberFilter
    {
        private int? Value1 { get; set; }

        private FilterAction Action1 { get; set; } = FilterAction.GreaterThanOrEqual;

        private int? Value2 { get; set; }

        private FilterAction Action2 { get; set; } = FilterAction.LessThanOrEqual;

        [Inject]
        [NotNull]
        private IStringLocalizer<TableFilter>? Localizer { get; set; }

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
                new SelectedItem("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"] ?? "GreaterThanOrEqual"),
                new SelectedItem("LessThanOrEqual", Localizer["LessThanOrEqual"] ?? "LessThanOrEqual"),
                new SelectedItem("GreaterThan", Localizer["GreaterThan"] ?? "GreaterThan"),
                new SelectedItem("LessThan", Localizer["LessThan"] ?? "LessThan"),
                new SelectedItem("Equal", Localizer["Equal"] ?? "Equal"),
                new SelectedItem("NotEqual", Localizer["NotEqual"] ?? "NotEqual")
            };
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
