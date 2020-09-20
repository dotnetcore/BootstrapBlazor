using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 枚举类型过滤组件
    /// </summary>
    public partial class EnumFilter
    {
        private string Value { get; set; } = "";

        private IEnumerable<SelectedItem> Items { get; set; } = Enumerable.Empty<SelectedItem>();

#nullable disable
        /// <summary>
        /// 内部使用
        /// </summary>
        private Type EnumType { get; set; }

        /// <summary>
        /// 获得/设置 相关枚举类型
        /// </summary>
        [Parameter]
        public Type Type { get; set; }
#nullable restore

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

            EnumType = Nullable.GetUnderlyingType(Type) ?? Type;
            Items = EnumType.ToSelectList(new SelectedItem("", "全选"));
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
            if (!string.IsNullOrEmpty(Value) && Enum.TryParse(EnumType, Value, out var val))
            {
                if (!string.IsNullOrEmpty(Value)) filters.Add(new FilterKeyValueAction()
                {
                    FieldKey = FieldKey,
                    FieldValue = val,
                    FilterAction = FilterAction.Equal
                });
            }
            return filters;
        }
    }
}
