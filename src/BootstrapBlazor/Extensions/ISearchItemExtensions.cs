// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ISearchItem 接口扩展类</para>
/// <para lang="en">ISearchItem interface extension class</para>
/// </summary>
public static class ISearchItemExtensions
{
    extension(ISearchItem item)
    {
        /// <summary>
        /// <para lang="zh">通过 <see cref="ISearchItem.PropertyType"/> 数据类型推断 <see cref="ISearchFormItemMetadata" /> 实例</para>
        /// <para lang="en"></para>
        /// </summary>
        /// <param name="options"></param>
        public ISearchFormItemMetadata BuildSearchMetadata(SearchFormLocalizerOptions options)
        {
            var fieldType = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType;
            ISearchFormItemMetadata? metaData = null;
            if (fieldType.IsEnum)
            {
                metaData = new SelectSearchMetadata()
                {
                    Items = fieldType.ToSelectList(new SelectedItem() { Value = "", Text = options.SelectAllText }),
                };
            }
            else if (fieldType.IsNumberWithDotSeparator())
            {
                metaData = new NumberSearchMetadata()
                {
                    StartValueLabelText = options.NumberStartValueLabelText,
                    EndValueLabelText = options.NumberEndValueLabelText,
                    ValueType = fieldType
                };
            }
            else if (fieldType.IsBoolean())
            {
                metaData = new SelectSearchMetadata()
                {
                    Items = new List<SelectedItem>()
                    {
                        new SelectedItem() { Value = "", Text = options.BooleanAllText },
                        new SelectedItem() { Value = "True", Text = options.BooleanTrueText },
                        new SelectedItem() { Value = "False", Text = options.BooleanFalseText }
                    }
                };
            }
            else if (fieldType.IsDateTime())
            {
                metaData = new DateTimeRangeSearchMetadata();
            }
            else
            {
                metaData = new StringSearchMetadata() { FilterAction = FilterAction.Contains };
            }

            return metaData;
        }

        /// <summary>
        /// <para lang="zh">通过 SearchItem 实例 Metadata 创建搜索项 UI 方法</para>
        /// <para lang="en">Creates a search item UI method through the SearchItem instance Metadata</para>
        /// </summary>
        public RenderFragment CreateSearchItemComponentByMetadata() => builder =>
        {
            var metaData = item.Metadata;
            switch (metaData)
            {
                case NumberSearchMetadata numberSearchMetadata:
                    builder.AddNumberSearchComponent(item, numberSearchMetadata);
                    break;
                case DateTimeSearchMetadata datetimeSearchMetadata:
                    builder.AddDateTimeSearchComponent(item, datetimeSearchMetadata);
                    break;
                case DateTimeRangeSearchMetadata datetimeRangeSearchMetadata:
                    builder.AddDateTimeRangeSearchComponent(item, datetimeRangeSearchMetadata);
                    break;
                case CheckboxListSearchMetadata checkboxListSearchMetadata:
                    builder.AddCheckboxListSearchComponent(item, checkboxListSearchMetadata);
                    break;
                case MultipleSelectSearchMetadata multipleSelectSearchMetadata:
                    builder.AddMultipleSelectSearchComponent(item, multipleSelectSearchMetadata);
                    break;
                case SelectSearchMetadata selectSearchMetadata:
                    builder.AddSelectSearchComponent(item, selectSearchMetadata);
                    break;
                case StringSearchMetadata stringSearchMetadata:
                    builder.AddStringSearchComponent(item, stringSearchMetadata);
                    break;
            }
        };
    }

    extension(RenderTreeBuilder builder)
    {
        private void AddStringSearchComponent(ISearchItem item, StringSearchMetadata stringSearchMetadata)
        {
            builder.OpenComponent<BootstrapInput<string>>(0);
            builder.AddAttribute(10, nameof(BootstrapInput<>.Value), stringSearchMetadata.Value);
            builder.AddAttribute(20, nameof(BootstrapInput<>.OnValueChanged), stringSearchMetadata.ValueChangedHandler);
            builder.AddAttribute(30, nameof(BootstrapInput<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(40, nameof(BootstrapInput<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(50, nameof(BootstrapInput<>.DisplayText), item.Text);
            builder.AddAttribute(60, nameof(BootstrapInput<>.SkipValidate), true);
            builder.AddAttribute(70, nameof(BootstrapInput<>.PlaceHolder), stringSearchMetadata.PlaceHolder);
            builder.CloseComponent();
        }

        private void AddNumberSearchComponent(ISearchItem item, NumberSearchMetadata numberSearchMetadata)
        {
            if (item.ShowLabel ?? true)
            {
                builder.OpenComponent<BootstrapLabel>(100);
                builder.AddAttribute(101, nameof(BootstrapLabel.Value), item.Text);
                builder.AddAttribute(102, nameof(BootstrapLabel.ShowLabelTooltip), item.ShowLabelTooltip);
                builder.CloseComponent();
            }

            builder.OpenComponent<BootstrapInputGroup>(0);
            builder.AddAttribute(1, nameof(BootstrapInputGroup.ChildContent), new RenderFragment(builder =>
            {
                builder.OpenComponent<BootstrapInputGroupLabel>(10);
                builder.AddAttribute(11, nameof(BootstrapInputGroupLabel.DisplayText), numberSearchMetadata.StartValueLabelText);
                builder.CloseComponent();

                builder.OpenComponent<BootstrapInput<string>>(20);
                builder.AddAttribute(21, nameof(BootstrapInput<>.Value), numberSearchMetadata.StartValue);
                builder.AddAttribute(22, nameof(BootstrapInput<>.OnValueChanged), numberSearchMetadata.StartValueChangedHandler);
                builder.AddAttribute(23, nameof(BootstrapInput<>.SkipValidate), true);
                builder.CloseComponent();

                builder.OpenComponent<BootstrapInputGroupLabel>(30);
                builder.AddAttribute(31, nameof(BootstrapInputGroupLabel.DisplayText), numberSearchMetadata.EndValueLabelText);
                builder.CloseComponent();

                builder.OpenComponent<BootstrapInput<string>>(40);
                builder.AddAttribute(41, nameof(BootstrapInput<>.Value), numberSearchMetadata.EndValue);
                builder.AddAttribute(42, nameof(BootstrapInput<>.OnValueChanged), numberSearchMetadata.EndValueChangedHandler);
                builder.AddAttribute(43, nameof(BootstrapInput<>.SkipValidate), true);
                builder.CloseComponent();
            }));
            builder.CloseComponent();
        }

        private void AddSelectSearchComponent(ISearchItem item, SelectSearchMetadata selectSearchMetadata)
        {
            builder.OpenComponent<Select<string>>(0);
            builder.AddAttribute(10, nameof(Select<>.Value), selectSearchMetadata.Value);
            builder.AddAttribute(20, nameof(Select<>.OnValueChanged), selectSearchMetadata.ValueChangedHandler);
            builder.AddAttribute(30, nameof(Select<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(40, nameof(Select<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(50, nameof(Select<>.DisplayText), item.Text);
            builder.AddAttribute(60, nameof(Select<>.Items), selectSearchMetadata.Items);
            builder.AddAttribute(70, nameof(Select<>.PlaceHolder), selectSearchMetadata.PlaceHolder);
            builder.AddAttribute(80, nameof(Select<>.SkipValidate), true);
            builder.AddAttribute(90, nameof(Select<>.ShowSearch), selectSearchMetadata.ShowSearch);
            builder.CloseComponent();
        }

        private void AddMultipleSelectSearchComponent(ISearchItem item, MultipleSelectSearchMetadata multipleSelectSearchMetadata)
        {
            builder.OpenComponent<MultiSelect<string>>(0);
            builder.AddAttribute(10, nameof(MultiSelect<>.Value), multipleSelectSearchMetadata.Value);
            builder.AddAttribute(20, nameof(MultiSelect<>.OnValueChanged), multipleSelectSearchMetadata.ValueChangedHandler);
            builder.AddAttribute(30, nameof(MultiSelect<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(40, nameof(MultiSelect<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(50, nameof(MultiSelect<>.DisplayText), item.Text);
            builder.AddAttribute(60, nameof(MultiSelect<>.Items), multipleSelectSearchMetadata.Items);
            builder.AddAttribute(70, nameof(MultiSelect<>.PlaceHolder), multipleSelectSearchMetadata.PlaceHolder);
            builder.AddAttribute(80, nameof(MultiSelect<>.SkipValidate), true);
            builder.CloseComponent();
        }

        private void AddCheckboxListSearchComponent(ISearchItem item, CheckboxListSearchMetadata checkboxListSearchMetadata)
        {
            builder.OpenComponent<CheckboxList<string>>(0);
            builder.AddAttribute(10, nameof(CheckboxList<>.Value), checkboxListSearchMetadata.Value);
            builder.AddAttribute(20, nameof(CheckboxList<>.OnValueChanged), checkboxListSearchMetadata.ValueChangedHandler);
            builder.AddAttribute(30, nameof(CheckboxList<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(40, nameof(CheckboxList<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(50, nameof(CheckboxList<>.DisplayText), item.Text);
            builder.AddAttribute(60, nameof(CheckboxList<>.Items), checkboxListSearchMetadata.Items);
            builder.AddAttribute(70, nameof(CheckboxList<>.SkipValidate), true);
            builder.CloseComponent();
        }

        private void AddDateTimeRangeSearchComponent(ISearchItem item, DateTimeRangeSearchMetadata datetimeRangeSearchMetadata)
        {
            builder.OpenComponent<DateTimeRange>(0);
            builder.AddAttribute(10, nameof(DateTimeRange.Value), datetimeRangeSearchMetadata.Value);
            builder.AddAttribute(20, nameof(DateTimeRange.OnValueChanged), datetimeRangeSearchMetadata.ValueChangedHandler);
            builder.AddAttribute(30, nameof(DateTimeRange.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(40, nameof(DateTimeRange.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(50, nameof(DateTimeRange.DisplayText), item.Text);
            builder.AddAttribute(60, nameof(DateTimeRange.SkipValidate), true);
            builder.CloseComponent();
        }

        private void AddDateTimeSearchComponent(ISearchItem item, DateTimeSearchMetadata datetimeSearchMetadata)
        {
            builder.OpenComponent<DateTimePicker<DateTime?>>(0);
            builder.AddAttribute(10, nameof(DateTimePicker<>.Value), datetimeSearchMetadata.Value);
            builder.AddAttribute(20, nameof(DateTimePicker<>.OnValueChanged), datetimeSearchMetadata.ValueChangedHandler);
            builder.AddAttribute(30, nameof(DateTimePicker<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(40, nameof(DateTimePicker<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(50, nameof(DateTimePicker<>.DisplayText), item.Text);
            builder.AddAttribute(60, nameof(DateTimePicker<>.SkipValidate), true);
            builder.AddAttribute(70, nameof(DateTimePicker<>.DatePlaceHolderText), datetimeSearchMetadata.PlaceHolder);
            builder.CloseComponent();
        }
    }

    extension(IEnumerable<ISearchItem>? items)
    {
        /// <summary>
        /// <para lang="zh">将 ISearchItem 集合转换为 FilterKeyValueAction 实例</para>
        /// <para lang="en">Converts a collection of ISearchItem to an instance of FilterKeyValueAction</para>
        /// </summary>
        public FilterKeyValueAction ToFilter()
        {
            var action = new FilterKeyValueAction()
            {
                Filters = []
            };

            if (items == null)
            {
                return action;
            }

            foreach (var item in items)
            {
                var filter = item.GetFilter();
                if (filter != null)
                {
                    action.Filters.Add(filter);
                }
            }

            return action;
        }
    }
}
