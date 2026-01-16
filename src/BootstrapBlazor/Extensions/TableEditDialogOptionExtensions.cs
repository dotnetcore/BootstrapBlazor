// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh"><see cref="ITableEditDialogOption{TModel}"/> 扩展类方法</para>
///  <para lang="en"><see cref="ITableEditDialogOption{TModel}"/> 扩展类方法</para>
/// </summary>
public static class TableEditDialogOptionExtensions
{
    /// <summary>
    ///  <para lang="zh">将 <see cref="ITableEditDialogOption{TModel}"/> 配置类转化为参数集合扩展方法</para>
    ///  <para lang="en">将 <see cref="ITableEditDialogOption{TModel}"/> 配置类转化为参数collection扩展方法</para>
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="option"></param>
    public static Dictionary<string, object?> ToParameter<TModel>(this ITableEditDialogOption<TModel> option) => new()
    {
        [nameof(EditDialog<TModel>.ShowUnsetGroupItemsOnTop)] = option.ShowUnsetGroupItemsOnTop,
        [nameof(EditDialog<TModel>.ShowLoading)] = option.ShowLoading,
        [nameof(EditDialog<TModel>.ShowLabel)] = option.ShowLabel,
        [nameof(EditDialog<TModel>.Items)] = option.Items ?? Utility.GenerateColumns<TModel>(item => !item.GetIgnore()),
        [nameof(EditDialog<TModel>.RowType)] = option.RowType,
        [nameof(EditDialog<TModel>.LabelAlign)] = option.LabelAlign,
        [nameof(EditDialog<TModel>.ItemChangedType)] = option.ItemChangedType,
        [nameof(EditDialog<TModel>.IsTracking)] = option.IsTracking,
        [nameof(EditDialog<TModel>.ItemsPerRow)] = option.ItemsPerRow,
        [nameof(EditDialog<TModel>.CloseButtonText)] = option.CloseButtonText,
        [nameof(EditDialog<TModel>.CloseButtonIcon)] = option.CloseButtonIcon,
        [nameof(EditDialog<TModel>.SaveButtonText)] = option.SaveButtonText,
        [nameof(EditDialog<TModel>.SaveButtonIcon)] = option.SaveButtonIcon,
        [nameof(EditDialog<TModel>.Model)] = option.Model,
        [nameof(EditDialog<TModel>.DisableAutoSubmitFormByEnter)] = option.DisableAutoSubmitFormByEnter,
        [nameof(EditDialog<TModel>.BodyTemplate)] = option.DialogBodyTemplate,
        [nameof(EditDialog<TModel>.FooterTemplate)] = option.DialogFooterTemplate,
        [nameof(EditDialog<TModel>.OnSaveAsync)] = new Func<EditContext, Task<bool>>(async context =>
        {
            var ret = false;
            if (option.OnEditAsync != null)
            {
                ret = await option.OnEditAsync(context);
            }
            return ret;
        })
    };
}
