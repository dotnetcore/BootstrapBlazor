using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TableFilterContent : ComponentBase
    {
        /// <summary>
        /// 内部缓存 Key 为 TableFilter Value 为 数据类型的 Condition
        /// </summary>
        private Dictionary<TableFilterBase, ValueTypeConditionBase> Cache { get; set; } = new Dictionary<TableFilterBase, ValueTypeConditionBase>();

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Columns { get; set; }

        /// <summary>
        /// 渲染组件方法
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 渲染正常按钮
            if (Columns != null)
            {
                var index = 0;
                foreach (var header in Columns.Columns)
                {
                    if (header.Filterable && header.FieldType != null)
                    {
                        builder.OpenComponent<TableFilter>(index++);
                        builder.AddAttribute(index++, nameof(TableFilter.FieldKey), header.GetFieldName());
                        builder.AddAttribute(index++, nameof(TableFilter.HeaderText), header.GetDisplayName());
                        builder.AddAttribute(index++, nameof(TableFilter.OnFilter), Columns.OnFilterAsync);
                        builder.AddAttribute(index++, nameof(TableFilter.Filters), this);
                        builder.AddAttribute(index++, nameof(TableFilter.OnReset), new Func<TableFilterBase, Task>(async filter =>
                        {
                            // 重置过滤条件回调方法
                            if (Cache.TryGetValue(filter, out var vf))
                            {
                                vf.Reset();
                                if (filter.OnFilter != null) await filter.OnFilter(Columns.GetFilters());
                            }
                        }));
                        builder.AddAttribute(index++, nameof(TableFilter.OnConfirm), new Func<TableFilterBase, Task>(async filter =>
                        {
                            // 过滤弹窗中点击确认按钮回调方法
                            if (Cache.TryGetValue(filter, out var vf))
                            {
                                vf.AddFilter();
                                if (filter.OnFilter != null) await filter.OnFilter(Columns.GetFilters());
                            }
                        }));
                        builder.AddAttribute(index++, nameof(TableFilter.OnPlus), new Func<TableFilterBase, Task>(filter =>
                        {
                            if (Cache.TryGetValue(filter, out var vf))
                            {
                                vf.Plus();
                            }
                            return Task.CompletedTask;
                        }));
                        builder.AddAttribute(index++, nameof(TableFilter.OnMinus), new Func<TableFilterBase, Task>(filter =>
                        {
                            if (Cache.TryGetValue(filter, out var vf))
                            {
                                vf.Minus();
                            }
                            return Task.CompletedTask;
                        }));

                        // 检查是否设置了过滤模板
                        if (header.FilterTemplate != null)
                        {
                            builder.AddAttribute(index++, nameof(TableFilter.BodyTemplate), header.FilterTemplate);
                        }
                        else
                        {
                            // 获得可为空具体类型
                            var fieldType = Nullable.GetUnderlyingType(header.FieldType) ?? header.FieldType;
                            switch (fieldType.Name)
                            {
                                case nameof(Boolean):
                                    builder.AddAttribute(index++, nameof(TableFilter.BodyTemplate), RenderBoolFilter());
                                    break;
                                case nameof(DateTime):
                                    builder.AddAttribute(index++, nameof(TableFilter.ShowMoreButton), true);
                                    builder.AddAttribute(index++, nameof(TableFilter.BodyTemplate), RenderDateTimeFilter());
                                    break;
                                case nameof(Int32):
                                case nameof(Double):
                                case nameof(Decimal):
                                    builder.AddAttribute(index++, nameof(TableFilter.ShowMoreButton), true);
                                    builder.AddAttribute(index++, nameof(TableFilter.BodyTemplate), RenderNumberFilter());
                                    break;
                                default:
                                    builder.AddAttribute(index++, nameof(TableFilter.ShowMoreButton), true);
                                    builder.AddAttribute(index++, nameof(TableFilter.BodyTemplate), RenderStringFilter());
                                    break;
                            };
                        }
                        builder.CloseComponent();
                    }
                }
            }
        }

        /// <summary>
        /// 添加 Filter 与 Condition 到缓存中
        /// </summary>
        /// <param name="tableFilter"></param>
        /// <param name="filter"></param>
        public void AddFilter(TableFilterBase tableFilter, ValueTypeConditionBase filter)
        {
            Cache.Add(tableFilter, filter);
        }

        private RenderFragment RenderBoolFilter() => builder =>
        {
            var index = 0;
            builder.OpenComponent<BoolFilter>(index++);
            builder.CloseComponent();
        };

        private RenderFragment RenderDateTimeFilter() => builder =>
        {
            var index = 0;
            builder.OpenComponent<DateTimeFilter>(index++);
            builder.CloseComponent();
        };


        private RenderFragment RenderNumberFilter() => builder =>
        {
            var index = 0;
            builder.OpenComponent<NumberFilter>(index++);
            builder.CloseComponent();
        };

        private RenderFragment RenderStringFilter() => builder =>
        {
            var index = 0;
            builder.OpenComponent<StringFilter>(index++);
            builder.CloseComponent();
        };
    }
}
