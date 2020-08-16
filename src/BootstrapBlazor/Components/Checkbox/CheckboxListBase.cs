using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// CheckboxList 组件基类
    /// </summary>
    public abstract class CheckboxListBase<TItem> : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 数据源
        /// </summary>
        [Parameter]
        public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();

        /// <summary>
        /// 获得/设置 Checkbox 组件布局样式 默认值 col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2
        /// </summary>
        [Parameter]
        public string CheckboxClass { get; set; } = "col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2";

        /// <summary>
        /// 获得/设置 显示列字段名称
        /// </summary>
        [Parameter]
        public string? TextField { get; set; }

        /// <summary>
        /// 获得/设置 是否选中列字段名称
        /// </summary>
        [Parameter]
        public string? CheckedField { get; set; }

        /// <summary>
        /// 获得/设置 SelectedItemChanged 方法
        /// </summary>
        [Parameter]
        public EventCallback<TItem> OnSelectedChanged { get; set; }

        #region SetProperty Methods
        /// <summary>
        /// Checkbox 组件选项状态改变时触发此方法
        /// </summary>
        /// <param name="item"></param>
        /// <param name="v"></param>
        protected async Task OnValueChanged(TItem item, bool v)
        {
            if (!string.IsNullOrEmpty(CheckedField))
            {
                var invoker = SetPropertyValueLambdaCache.GetOrAdd((typeof(TItem), CheckedField), key => item.SetPropertyValueLambda<TItem, bool>(key.FieldName).Compile());
                invoker.Invoke(item, v);

                if (OnSelectedChanged.HasDelegate) await OnSelectedChanged.InvokeAsync(item);
            }
        }

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Action<TItem, bool>> SetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Action<TItem, bool>>();
        #endregion

        #region GetProperty Methods
        /// <summary>
        /// 获得 Checkbox 组件显示名称方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetDisplayText(TItem item)
        {
            var ret = "未设置";
            if (!string.IsNullOrEmpty(TextField))
            {
                var invoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TItem), TextField), key => item.GetPropertyValueLambda<TItem, object>(key.FieldName).Compile());
                var v = invoker.Invoke(item);
                ret = v?.ToString() ?? "";
            }
            return ret;
        }

        /// <summary>
        /// 获得 Checkbox 组件状态方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected bool GetChecked(TItem item)
        {
            var ret = false;
            if (!string.IsNullOrEmpty(CheckedField))
            {
                var invoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TItem), CheckedField), key => item.GetPropertyValueLambda<TItem, object>(key.FieldName).Compile());
                var v = invoker.Invoke(item);
                if (v is bool b)
                {
                    ret = b;
                }
            }
            return ret;
        }

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Func<TItem, object>> GetPropertyValueLambdaCache { get; set; } = new ConcurrentDictionary<(Type, string), Func<TItem, object>>();
        #endregion
    }
}
