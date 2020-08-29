using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 布尔类型过滤条件
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
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

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
