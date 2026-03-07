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
    /// <summary>
    /// <para lang="zh">创建 搜索项的 RenderFragment 实例</para>
    /// <para lang="en">Creates a RenderFragment instance for the search item</para>
    /// </summary>
    /// <param name="item"></param>
    public static RenderFragment CreateRenderFragment(this ISearchItem item) => builder =>
    {
        var metaData = item.MetaData;
        switch (metaData)
        {
            case NumberSearchMetaData numberSearchMetaData:
                builder.AddNumberSearchComponent(item, numberSearchMetaData);
                break;
            case DateTimeSearchMetaData datetimeSearchMetaData:
                builder.AddDateTimeSearchComponent(item, datetimeSearchMetaData);
                break;
            case DateTimeRangeSearchMetaData datetimeRangeSearchMetaData:
                builder.AddDateTimeRangeSearchComponent(item, datetimeRangeSearchMetaData);
                break;
            case CheckboxListSearchMetaData checkboxListSearchMetaData:
                builder.AddCheckboxListSearchComponent(item, checkboxListSearchMetaData);
                break;
            case MultipleSelectSearchMetaData multipleSelectSearchMetaData:
                builder.AddMultipleSelectSearchComponent(item, multipleSelectSearchMetaData);
                break;
            case SelectSearchMetaData selectSearchMetaData:
                builder.AddSelectSearchComponent(item, selectSearchMetaData);
                break;
            case StringSearchMetaData stringSearchMetaData:
                builder.AddStringSearchComponent(item, stringSearchMetaData);
                break;
        }
    };

    extension(RenderTreeBuilder builder)
    {
        private void AddStringSearchComponent(ISearchItem item, StringSearchMetaData stringSearchMetaData)
        {
            builder.OpenComponent<BootstrapInput<string>>(0);
            builder.AddAttribute(1, nameof(BootstrapInput<>.Value), stringSearchMetaData.Value);
            builder.AddAttribute(2, nameof(BootstrapInput<>.OnValueChanged), stringSearchMetaData.ValueChangedHandler);
            builder.AddAttribute(3, nameof(BootstrapInput<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(4, nameof(BootstrapInput<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(5, nameof(BootstrapInput<>.DisplayText), item.Text);
            builder.AddAttribute(6, nameof(BootstrapInput<>.SkipValidate), true);
            builder.CloseComponent();
        }

        private void AddNumberSearchComponent(ISearchItem item, NumberSearchMetaData numberSearchMetaData)
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
                builder.AddAttribute(11, nameof(BootstrapInputGroupLabel.DisplayText), numberSearchMetaData.StartValueLabelText);
                builder.CloseComponent();

                builder.OpenComponent<BootstrapInput<string>>(20);
                builder.AddAttribute(21, nameof(BootstrapInput<>.Value), numberSearchMetaData.StartValue);
                builder.AddAttribute(22, nameof(BootstrapInput<>.OnValueChanged), numberSearchMetaData.StartValueChangedHandler);
                builder.AddAttribute(23, nameof(BootstrapInput<>.SkipValidate), true);
                builder.CloseComponent();

                builder.OpenComponent<BootstrapInputGroupLabel>(30);
                builder.AddAttribute(31, nameof(BootstrapInputGroupLabel.DisplayText), numberSearchMetaData.EndValueLabelText);
                builder.CloseComponent();

                builder.OpenComponent<BootstrapInput<string>>(40);
                builder.AddAttribute(41, nameof(BootstrapInput<>.Value), numberSearchMetaData.EndValue);
                builder.AddAttribute(42, nameof(BootstrapInput<>.OnValueChanged), numberSearchMetaData.EndValueChangedHandler);
                builder.AddAttribute(43, nameof(BootstrapInput<>.SkipValidate), true);
                builder.CloseComponent();
            }));
            builder.CloseComponent();
        }

        private void AddSelectSearchComponent(ISearchItem item, SelectSearchMetaData selectSearchMetaData)
        {
            builder.OpenComponent<Select<string>>(0);
            builder.AddAttribute(1, nameof(Select<>.Value), selectSearchMetaData.Value);
            builder.AddAttribute(2, nameof(Select<>.OnValueChanged), selectSearchMetaData.ValueChangedHandler);
            builder.AddAttribute(3, nameof(Select<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(4, nameof(Select<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(5, nameof(Select<>.DisplayText), item.Text);
            builder.AddAttribute(6, nameof(Select<>.Items), selectSearchMetaData.Items);
            builder.AddAttribute(7, nameof(Select<>.PlaceHolder), selectSearchMetaData.PlaceHolder);
            builder.AddAttribute(8, nameof(Select<>.SkipValidate), true);
            builder.CloseComponent();
        }

        private void AddMultipleSelectSearchComponent(ISearchItem item, MultipleSelectSearchMetaData multipleSelectSearchMetaData)
        {
            builder.OpenComponent<MultiSelect<string>>(0);
            builder.AddAttribute(1, nameof(MultiSelect<>.Value), multipleSelectSearchMetaData.Value);
            builder.AddAttribute(2, nameof(MultiSelect<>.OnValueChanged), multipleSelectSearchMetaData.ValueChangedHandler);
            builder.AddAttribute(3, nameof(MultiSelect<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(4, nameof(MultiSelect<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(5, nameof(MultiSelect<>.DisplayText), item.Text);
            builder.AddAttribute(6, nameof(MultiSelect<>.Items), multipleSelectSearchMetaData.Items);
            builder.AddAttribute(7, nameof(MultiSelect<>.PlaceHolder), multipleSelectSearchMetaData.PlaceHolder);
            builder.AddAttribute(8, nameof(MultiSelect<>.SkipValidate), true);
            builder.CloseComponent();
        }

        private void AddCheckboxListSearchComponent(ISearchItem item, CheckboxListSearchMetaData checkboxListSearchMetaData)
        {
            builder.OpenComponent<CheckboxList<string>>(0);
            builder.AddAttribute(1, nameof(CheckboxList<>.Value), checkboxListSearchMetaData.Value);
            builder.AddAttribute(2, nameof(CheckboxList<>.OnValueChanged), checkboxListSearchMetaData.ValueChangedHandler);
            builder.AddAttribute(3, nameof(CheckboxList<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(4, nameof(CheckboxList<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(5, nameof(CheckboxList<>.DisplayText), item.Text);
            builder.AddAttribute(6, nameof(CheckboxList<>.Items), checkboxListSearchMetaData.Items);
            builder.AddAttribute(7, nameof(CheckboxList<>.SkipValidate), true);
            builder.CloseComponent();
        }

        private void AddDateTimeRangeSearchComponent(ISearchItem item, DateTimeRangeSearchMetaData datetimeRangeSearchMetaData)
        {
            builder.OpenComponent<DateTimeRange>(0);
            builder.AddAttribute(1, nameof(DateTimeRange.Value), datetimeRangeSearchMetaData.Value);
            builder.AddAttribute(2, nameof(DateTimeRange.OnValueChanged), datetimeRangeSearchMetaData.ValueChangedHandler);
            builder.AddAttribute(3, nameof(DateTimeRange.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(4, nameof(DateTimeRange.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(5, nameof(DateTimeRange.DisplayText), item.Text);
            builder.AddAttribute(6, nameof(DateTimeRange.SkipValidate), true);
            builder.CloseComponent();
        }

        private void AddDateTimeSearchComponent(ISearchItem item, DateTimeSearchMetaData datetimeSearchMetaData)
        {
            builder.OpenComponent<DateTimePicker<DateTime?>>(0);
            builder.AddAttribute(1, nameof(DateTimePicker<>.Value), datetimeSearchMetaData.Value);
            builder.AddAttribute(2, nameof(DateTimePicker<>.OnValueChanged), datetimeSearchMetaData.ValueChangedHandler);
            builder.AddAttribute(3, nameof(DateTimePicker<>.ShowLabel), item.ShowLabel ?? true);
            builder.AddAttribute(4, nameof(DateTimePicker<>.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(5, nameof(DateTimePicker<>.DisplayText), item.Text);
            builder.AddAttribute(6, nameof(DateTimePicker<>.SkipValidate), true);
            builder.AddAttribute(7, nameof(DateTimePicker<>.DatePlaceHolderText), datetimeSearchMetaData.PlaceHolder);
            builder.CloseComponent();
        }
    }
}
