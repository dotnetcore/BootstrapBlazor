using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class NumberFilter
    {
        private int? Value1 { get; set; }

        private FilterAction Action1 { get; set; } = FilterAction.GreaterThanOrEqual;

        private int? Value2 { get; set; }

        private FilterAction Action2 { get; set; } = FilterAction.LessThanOrEqual;

        private IEnumerable<SelectedItem> Items => new SelectedItem[] {
            new SelectedItem("GreaterThanOrEqual", "大于等于"),
            new SelectedItem("LessThanOrEqual", "小于等于"),
            new SelectedItem("GreaterThan", "大于"),
            new SelectedItem("LessThan", "小于"),
            new SelectedItem("Equal", "等于"),
            new SelectedItem("NotEqual", "不等于")
        };

        /// <summary>
        /// 
        /// </summary>
        protected override void ResetFilter()
        {
            Value1 = null;
            Value2 = null;
            Action1 = FilterAction.GreaterThanOrEqual;
            Action2 = FilterAction.LessThanOrEqual;
            Count = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<FilterKeyValueAction> BuildFilters()
        {
            var filters = new List<FilterKeyValueAction>();
            if (Value1 != null) filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value1,
                FilterAction = Action1
            });
            if (Value2 != null) filters.Add(new FilterKeyValueAction()
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
