// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="ITableEditDialogOption{TModel}"/> 扩展类方法
/// </summary>
public static class TableEditDialogOptionExtensions
{
    /// <summary>
    /// 将 <see cref="ITableEditDialogOption{TModel}"/> 配置类转化为参数集合扩展方法
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="option"></param>
    public static Dictionary<string, object?> ToParameter<TModel>(this ITableEditDialogOption<TModel> option) => new()
    {
        [nameof(EditDialog<TModel>.ShowUnsetGroupItemsOnTop)] = option.ShowUnsetGroupItemsOnTop,
        [nameof(EditDialog<TModel>.ShowLoading)] = option.ShowLoading,
        [nameof(EditDialog<TModel>.ShowLabel)] = option.ShowLabel,
        [nameof(EditDialog<TModel>.Items)] = option.Items ?? Utility.GenerateColumns<TModel>(item => !item.Ignore),
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
        [nameof(EditDialog<TModel>.OnCloseAsync)] = option.OnCloseAsync,
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
