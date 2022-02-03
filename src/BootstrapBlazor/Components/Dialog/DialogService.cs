// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// Dialog 组件服务
/// </summary>
public class DialogService : BootstrapServiceBase<DialogOption>
{
    /// <summary>
    /// 显示 Dialog 方法
    /// </summary>
    /// <param name="option">弹窗配置信息实体类</param>
    /// <param name="dialog">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    /// <returns></returns>
    public Task Show(DialogOption option, Dialog? dialog = null) => Invoke(option, dialog);

    /// <summary>
    /// 弹出搜索对话框
    /// </summary>
    /// <param name="option">SearchDialogOption 配置类实例</param>
    /// <param name="dialog">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public async Task ShowSearchDialog<TModel>(SearchDialogOption<TModel> option, Dialog? dialog = null)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(SearchDialog<TModel>.ShowLabel)] = option.ShowLabel,
            [nameof(SearchDialog<TModel>.Items)] = option.Items ?? Utility.GenerateColumns<TModel>(item => item.Searchable),
            [nameof(SearchDialog<TModel>.OnResetSearchClick)] = new Func<Task>(async () =>
            {
                option.OnCloseAsync = null;
                option.Dialog.RemoveDialog();
                if (option.OnResetSearchClick != null)
                {
                    await option.OnResetSearchClick();
                }
            }),
            [nameof(SearchDialog<TModel>.OnSearchClick)] = new Func<Task>(async () =>
            {
                option.OnCloseAsync = null;
                option.Dialog.RemoveDialog();
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
        await Invoke(option, dialog);
    }

    /// <summary>
    /// 弹出编辑对话框
    /// </summary>
    /// <param name="option">EditDialogOption 配置类实例</param>
    /// <param name="dialog"></param>
    public async Task ShowEditDialog<TModel>(EditDialogOption<TModel> option, Dialog? dialog = null)
    {
        var parameters = new Dictionary<string, object?>
        {
            [nameof(EditDialog<TModel>.ShowLoading)] = option.ShowLoading,
            [nameof(EditDialog<TModel>.ShowLabel)] = option.ShowLabel,
            [nameof(EditDialog<TModel>.Items)] = option.Items ?? Utility.GenerateColumns<TModel>(item => item.Editable),
            [nameof(EditDialog<TModel>.OnCloseAsync)] = new Func<Task>(async () =>
            {
                option.Dialog.RemoveDialog();
                await option.Dialog.CloseOrPopDialog();
            }),
            [nameof(EditDialog<TModel>.OnSaveAsync)] = new Func<EditContext, Task>(async context =>
            {
                if (option.OnEditAsync != null)
                {
                    var ret = await option.OnEditAsync(context);
                    if (ret)
                    {
                        option.Dialog.RemoveDialog();
                        await option.Dialog.CloseOrPopDialog();
                    }
                }
            }),
            [nameof(EditDialog<TModel>.RowType)] = option.RowType,
            [nameof(EditDialog<TModel>.LabelAlign)] = option.LabelAlign,
            [nameof(EditDialog<TModel>.IsTracking)] = option.IsTracking,
            [nameof(EditDialog<TModel>.ItemChangedType)] = option.ItemChangedType,
            [nameof(ItemsPerRow)] = option.ItemsPerRow,
            [nameof(EditDialog<TModel>.CloseButtonText)] = option.CloseButtonText,
            [nameof(EditDialog<TModel>.SaveButtonText)] = option.SaveButtonText,
            [nameof(EditDialog<TModel>.Model)] = option.Model,
            [nameof(EditDialog<TModel>.BodyTemplate)] = option.DialogBodyTemplate
        };

        option.Component = BootstrapDynamicComponent.CreateComponent<EditDialog<TModel>>(parameters);
        await Invoke(option, dialog);
    }

    /// <summary>
    /// 弹出带结果的对话框
    /// </summary>
    /// <param name="option">对话框参数</param>
    /// <param name="dialog">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    /// <returns></returns>
    public async Task<DialogResult> ShowModal<TDialog>(ResultDialogOption option, Dialog? dialog = null)
        where TDialog : IComponent, IResultDialog
    {
        IResultDialog? resultDialog = null;
        var result = DialogResult.Close;

        option.BodyTemplate = builder =>
        {
            var index = 0;
            builder.OpenComponent(index++, typeof(TDialog));
            if (option.ComponentParamters != null)
            {
                foreach (var p in option.ComponentParamters)
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

                // Modal 与 ModalDialog 的 OnClose 事件陷入死循环
                // option.OnClose -> Modal.Close -> ModalDialog.Close -> ModalDialog.OnClose -> option.OnClose
                option.OnCloseAsync = null;
                await option.Dialog.Close();
                option.ReturnTask.SetResult(result);
            }
        };

        await Invoke(option, dialog);
        return await option.ReturnTask.Task;
    }

    /// <summary>
    /// 弹出保存对话窗方法
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="title"></param>
    /// <param name="saveCallback"></param>
    /// <param name="parameters"></param>
    /// <param name="configureOption"></param>
    /// <param name="dialog"></param>
    /// <returns></returns>
    public async Task ShowSaveDialog<TComponent>(string title, Func<Task<bool>> saveCallback, Dictionary<string, object?>? parameters = null, Action<DialogOption>? configureOption = null, Dialog? dialog = null) where TComponent : ComponentBase
    {
        var option = new DialogOption()
        {
            Title = title,
            Component = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters),
            ShowSaveButton = true,
            OnSaveAsync = saveCallback
        };
        configureOption?.Invoke(option);
        await Invoke(option, dialog);
    }
}
