using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// /// </summary>
    public abstract class FilterBase : ComponentBase, IFilter
    {
        /// <summary>
        /// 
        /// </summary>
        protected int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected virtual FilterLogic Logic { get; set; }

        /// <summary>
        /// 获得/设置 相关 Field 字段名称
        /// </summary>
        protected string FieldKey { get; set; } = "";

        /// <summary>
        /// 获得/设置 所属 TableFilter 实例
        /// </summary>
        [CascadingParameter]
        protected TableFilterBase? TableFilter { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TableFilter?.Filters?.AddFilter(TableFilter, this);
            FieldKey = TableFilter?.FieldKey ?? "";
        }

        /// <summary>
        /// 重置方法
        /// </summary>
        public void Reset()
        {
            ResetFilterCondition();
            TableFilter?.ResetFilter();
            StateHasChanged();
        }

        /// <summary>
        /// 重置过滤条件方法
        /// </summary>
        protected abstract void ResetFilterCondition();

        /// <summary>
        /// 获得 添加 IFilter 实例到 Column 集合中
        /// </summary>
        /// <returns></returns>
        public void AddFilter()
        {
            if (BuildConditions().Any()) TableFilter?.AddFilter(this);
        }

        /// <summary>
        /// 获得 过滤窗口的所有条件
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FilterKeyValueAction> GetFilterConditions()
        {
            return BuildConditions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<FilterKeyValueAction> BuildConditions();

        /// <summary>
        /// 增加过滤条件方法
        /// </summary>
        public virtual void Plus()
        {
            if (Count == 0)
            {
                Count++;
                StateHasChanged();
            }
        }

        /// <summary>
        /// 减少过滤条件方法
        /// </summary>
        public virtual void Minus()
        {
            if (Count == 1)
            {
                Count--;
                StateHasChanged();
            }
        }
    }
}
