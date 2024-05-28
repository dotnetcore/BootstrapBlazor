// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// 抽屉服务扩展方法
/// </summary>
public static class DrawerExtensions
{
    /// <summary>
    /// 弹出编辑抽屉
    /// </summary>
    /// <param name="service"><see cref="DrawerService"/> 服务实例</param>
    /// <param name="editOption"><see cref="TableEditDrawerOption{TModel}"/> 配置类实例</param>
    /// <param name="option"><see cref="DrawerOption"/> 配置类实例</param>
    public static async Task ShowEditDrawer<TModel>(this DrawerService service, TableEditDrawerOption<TModel> editOption, DrawerOption option)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(EditDialog<TModel>.ShowUnsetGroupItemsOnTop)] = editOption.ShowUnsetGroupItemsOnTop,
            [nameof(EditDialog<TModel>.ShowLoading)] = editOption.ShowLoading,
            [nameof(EditDialog<TModel>.ShowLabel)] = editOption.ShowLabel,
            [nameof(EditDialog<TModel>.Items)] = editOption.Items ?? Utility.GenerateColumns<TModel>(item => !item.Ignore),
            [nameof(EditDialog<TModel>.OnCloseAsync)] = editOption.OnCloseAsync,
            [nameof(EditDialog<TModel>.OnSaveAsync)] = new Func<EditContext, Task>(async context =>
            {
                if (editOption.OnEditAsync != null)
                {
                    var ret = await editOption.OnEditAsync(context);
                }
            }),
            [nameof(EditDialog<TModel>.RowType)] = editOption.RowType,
            [nameof(EditDialog<TModel>.LabelAlign)] = editOption.LabelAlign,
            [nameof(EditDialog<TModel>.ItemChangedType)] = editOption.ItemChangedType,
            [nameof(EditDialog<TModel>.IsTracking)] = editOption.IsTracking,
            [nameof(EditDialog<TModel>.ItemsPerRow)] = editOption.ItemsPerRow,
            [nameof(EditDialog<TModel>.CloseButtonText)] = editOption.CloseButtonText,
            [nameof(EditDialog<TModel>.SaveButtonText)] = editOption.SaveButtonText,
            [nameof(EditDialog<TModel>.Model)] = editOption.Model,
            [nameof(EditDialog<TModel>.DisableAutoSubmitFormByEnter)] = editOption.DisableAutoSubmitFormByEnter,
            [nameof(EditDialog<TModel>.BodyTemplate)] = editOption.BodyTemplate,
            [nameof(EditDialog<TModel>.FooterTemplate)] = editOption.FooterTemplate
        };

        option.ChildContent = BootstrapDynamicComponent.CreateComponent<EditDialog<TModel>>(parameters).Render();
        await service.Show(option);
    }
}
