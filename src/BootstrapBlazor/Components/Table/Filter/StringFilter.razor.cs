using System.Collections.Generic;

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

        /// <summary>
        /// 
        /// </summary>
        protected override FilterLogic Logic { get; set; } = FilterLogic.Or;

        private IEnumerable<SelectedItem> Items => new SelectedItem[] {
            new SelectedItem("Contains", "包含"),
            new SelectedItem("Equal", "等于"),
            new SelectedItem("NotEqual", "不等于"),
            new SelectedItem("NotContains", "不包含"),
        };

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
