using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BoolFilter
    {
        private string Value { get; set; } = "";

        private IEnumerable<SelectedItem> Items => new SelectedItem[] {
            new SelectedItem("", "全部"),
            new SelectedItem("true", "选中"),
            new SelectedItem("false", "未选中")
        };

        /// <summary>
        /// 
        /// </summary>
        protected override void ResetFilterCondition()
        {
            Value = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<FilterKeyValueAction> BuildConditions()
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
