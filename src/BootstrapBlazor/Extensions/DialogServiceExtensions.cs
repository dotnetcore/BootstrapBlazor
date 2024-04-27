// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// DialogService 扩展方法
/// </summary>
public static class DialogServiceExtensions
{
    /// <summary>
    /// 弹出搜索对话框
    /// </summary>
    /// <param name="service">DialogService 服务实例</param>
    /// <param name="option">SearchDialogOption 配置类实例</param>
    /// <param name="dialog">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public static async Task ShowSearchDialog<TModel>(this DialogService service, SearchDialogOption<TModel> option, Dialog? dialog = null)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(SearchDialog<TModel>.ShowUnsetGroupItemsOnTop)] = option.ShowUnsetGroupItemsOnTop,
            [nameof(SearchDialog<TModel>.ShowLabel)] = option.ShowLabel,
            [nameof(SearchDialog<TModel>.Items)] = option.Items ?? Utility.GenerateColumns<TModel>(item => item.Searchable),
            [nameof(SearchDialog<TModel>.OnResetSearchClick)] = new Func<Task>(async () =>
            {
                await option.CloseDialogAsync();
                if (option.OnResetSearchClick != null)
                {
                    await option.OnResetSearchClick();
                }
            }),
            [nameof(SearchDialog<TModel>.OnSearchClick)] = new Func<Task>(async () =>
            {
                await option.CloseDialogAsync();
                if (option.OnSearchClick != null)
                {
                    await option.OnSearchClick();
                }
            }),
            [nameof(SearchDialog<TModel>.RowType)] = option.RowType,
            [nameof(SearchDialog<TModel>.LabelAlign)] = option.LabelAlign,
            [nameof(ItemsPerRow)] = option.ItemsPerRow,
            [nameof(SearchDialog<TModel>.ResetButtonText)] = option.ResetButtonText,
            [nameof(SearchDialog<TModel>.QueryButtonText)] = option.QueryButtonText,
            [nameof(SearchDialog<TModel>.Model)] = option.Model,
            [nameof(SearchDialog<TModel>.BodyTemplate)] = option.DialogBodyTemplate
        };
        option.Component = BootstrapDynamicComponent.CreateComponent<SearchDialog<TModel>>(parameters);
        await service.Show(option, dialog);
    }

    /// <summary>
    /// 弹出编辑对话框
    /// </summary>
    /// <param name="service">DialogService 服务实例</param>
    /// <param name="option">EditDialogOption 配置类实例</param>
    /// <param name="dialog"></param>
    public static async Task ShowEditDialog<TModel>(this DialogService service, EditDialogOption<TModel> option, Dialog? dialog = null)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(EditDialog<TModel>.ShowUnsetGroupItemsOnTop)] = option.ShowUnsetGroupItemsOnTop,
            [nameof(EditDialog<TModel>.ShowLoading)] = option.ShowLoading,
            [nameof(EditDialog<TModel>.ShowLabel)] = option.ShowLabel,
            [nameof(EditDialog<TModel>.Items)] = option.Items ?? Utility.GenerateColumns<TModel>(item => !item.Ignore),
            [nameof(EditDialog<TModel>.OnCloseAsync)] = option.OnCloseAsync,
            [nameof(EditDialog<TModel>.OnSaveAsync)] = new Func<EditContext, Task>(async context =>
            {
                if (option.OnEditAsync != null)
                {
                    var ret = await option.OnEditAsync(context);
                    if (ret)
                    {
                        await option.CloseDialogAsync();
                    }
                }
            }),
            [nameof(EditDialog<TModel>.RowType)] = option.RowType,
            [nameof(EditDialog<TModel>.LabelAlign)] = option.LabelAlign,
            [nameof(EditDialog<TModel>.ItemChangedType)] = option.ItemChangedType,
            [nameof(EditDialog<TModel>.IsTracking)] = option.IsTracking,
            [nameof(EditDialog<TModel>.ItemsPerRow)] = option.ItemsPerRow,
            [nameof(EditDialog<TModel>.CloseButtonText)] = option.CloseButtonText,
            [nameof(EditDialog<TModel>.SaveButtonText)] = option.SaveButtonText,
            [nameof(EditDialog<TModel>.Model)] = option.Model,
            [nameof(EditDialog<TModel>.DisableAutoSubmitFormByEnter)] = option.DisableAutoSubmitFormByEnter,
            [nameof(EditDialog<TModel>.BodyTemplate)] = option.DialogBodyTemplate,
            [nameof(EditDialog<TModel>.FooterTemplate)] = option.DialogFooterTemplate
        };

        option.Component = BootstrapDynamicComponent.CreateComponent<EditDialog<TModel>>(parameters);
        await service.Show(option, dialog);
    }

    /// <summary>
    /// 弹出带结果的对话框
    /// </summary>
    /// <param name="service">DialogService 服务实例</param>
    /// <param name="option">对话框参数</param>
    /// <param name="dialog">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    /// <returns></returns>
    public static async Task<DialogResult> ShowModal<TDialog>(this DialogService service, ResultDialogOption option, Dialog? dialog = null)
        where TDialog : IComponent, IResultDialog
    {
        IResultDialog? resultDialog = null;
        var result = DialogResult.Unset;

        option.BodyTemplate = builder =>
        {
            var index = 0;
            builder.OpenComponent(index++, typeof(TDialog));
            if (option.ComponentParameters != null)
            {
                foreach (var p in option.ComponentParameters)
                {
                    builder.AddAttribute(index++, p.Key, p.Value);
                }
            }
            builder.AddComponentReferenceCapture(index++, com => resultDialog = (IResultDialog)com);
            builder.CloseComponent();
        };

        option.FooterTemplate = BootstrapDynamicComponent.CreateComponent<ResultDialogFooter>(new Dictionary<string, object?>
        {
            [nameof(ResultDialogFooter.ButtonCloseText)] = option.ButtonCloseText,
            [nameof(ResultDialogFooter.ButtonNoText)] = option.ButtonNoText,
            [nameof(ResultDialogFooter.ButtonYesText)] = option.ButtonYesText,
            [nameof(ResultDialogFooter.ShowCloseButton)] = option.ShowCloseButton,
            [nameof(ResultDialogFooter.ButtonCloseColor)] = option.ButtonCloseColor,
            [nameof(ResultDialogFooter.ButtonCloseIcon)] = option.ButtonCloseIcon,
            [nameof(ResultDialogFooter.OnClickClose)] = new Func<Task>(async () =>
            {
                result = DialogResult.Close;
                if (option.OnCloseAsync != null) { await option.OnCloseAsync(); }
            }),

            [nameof(ResultDialogFooter.ShowYesButton)] = option.ShowYesButton,
            [nameof(ResultDialogFooter.ButtonYesColor)] = option.ButtonYesColor,
            [nameof(ResultDialogFooter.ButtonYesIcon)] = option.ButtonYesIcon,
            [nameof(ResultDialogFooter.OnClickYes)] = new Func<Task>(async () =>
            {
                result = DialogResult.Yes;
                if (option.OnCloseAsync != null) { await option.OnCloseAsync(); }
            }),

            [nameof(ResultDialogFooter.ShowNoButton)] = option.ShowNoButton,
            [nameof(ResultDialogFooter.ButtonNoColor)] = option.ButtonNoColor,
            [nameof(ResultDialogFooter.ButtonNoIcon)] = option.ButtonNoIcon,
            [nameof(ResultDialogFooter.OnClickNo)] = new Func<Task>(async () =>
            {
                result = DialogResult.No;
                if (option.OnCloseAsync != null) { await option.OnCloseAsync(); }
            })
        }).Render();

        var closeCallback = option.OnCloseAsync;
        option.OnCloseAsync = async () =>
        {
            if (resultDialog != null && await resultDialog.OnClosing(result))
            {
                await resultDialog.OnClose(result);
                if (closeCallback != null)
                {
                    await closeCallback();
                }

                option.OnCloseAsync = null;
                if (result == DialogResult.Unset)
                {
                    result = DialogResult.Close;
                }
                else
                {
                    await option.CloseDialogAsync();
                }
                option.ReturnTask.SetResult(result);
            }
            else
            {
                result = DialogResult.Close;
            }
        };

        await service.Show(option, dialog);
        return await option.ReturnTask.Task;
    }

    /// <summary>
    /// 弹出带保存按钮对话窗方法
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="service">DialogService 服务实例</param>
    /// <param name="title">弹窗标题</param>
    /// <param name="saveCallback">点击保存按钮回调委托方法 返回 true 时关闭弹窗</param>
    /// <param name="parametersFactory">TComponent 组件所需参数</param>
    /// <param name="configureOption"><see cref="DialogOption"/> 实例配置回调方法</param>
    /// <param name="dialog"></param>
    /// <returns></returns>
    public static async Task ShowSaveDialog<TComponent>(this DialogService service, string title, Func<Task<bool>>? saveCallback = null, Action<Dictionary<string, object?>>? parametersFactory = null, Action<DialogOption>? configureOption = null, Dialog? dialog = null) where TComponent : ComponentBase
    {
        var option = new DialogOption()
        {
            Title = title,
            ShowSaveButton = true,
            OnSaveAsync = saveCallback
        };
        var parameters = new Dictionary<string, object?>();
        parametersFactory?.Invoke(parameters);
        option.Component = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters);
        configureOption?.Invoke(option);
        await service.Show(option, dialog);
    }

    /// <summary>
    /// 弹出带关闭按钮对话窗方法
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="parametersFactory"></param>
    /// <param name="configureOption"></param>
    /// <param name="dialog"></param>
    /// <returns></returns>
    public static async Task ShowCloseDialog<TComponent>(this DialogService service, string title, Action<Dictionary<string, object?>>? parametersFactory = null, Action<DialogOption>? configureOption = null, Dialog? dialog = null) where TComponent : ComponentBase
    {
        var option = new DialogOption()
        {
            Title = title
        };
        var parameters = new Dictionary<string, object?>();
        parametersFactory?.Invoke(parameters);
        option.Component = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters);
        configureOption?.Invoke(option);
        await service.Show(option, dialog);
    }

    /// <summary>
    /// 弹出表单对话窗方法
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="service">DialogService 服务实例</param>
    /// <param name="title">弹窗标题</param>
    /// <param name="parametersFactory">TComponent 组件所需参数</param>
    /// <param name="configureOption"><see cref="DialogOption"/> 实例配置回调方法</param>
    /// <param name="dialog"></param>
    /// <returns></returns>
    public static async Task ShowValidateFormDialog<TComponent>(this DialogService service, string title, Func<DialogOption, Dictionary<string, object?>>? parametersFactory = null, Action<DialogOption>? configureOption = null, Dialog? dialog = null) where TComponent : ComponentBase
    {
        var option = new DialogOption()
        {
            Title = title,
            ShowFooter = false,
        };
        var parameters = parametersFactory?.Invoke(option);
        option.Component = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters);
        configureOption?.Invoke(option);
        await service.Show(option, dialog);
    }
}
