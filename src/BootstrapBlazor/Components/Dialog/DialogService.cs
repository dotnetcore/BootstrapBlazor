// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Dialog 组件服务
    /// </summary>
    public class DialogService : PopupServiceBase<DialogOption>
    {
        private IStringLocalizer<EditDialog<DialogOption>> EditDialogLocalizer { get; set; }

        private IStringLocalizer<SearchDialog<DialogOption>> SearchDialogLocalizer { get; set; }

        private IStringLocalizer<ResultDialogOption> ResultDialogLocalizer { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="editLocalizer"></param>
        /// <param name="seachLocalizer"></param>
        /// <param name="resultDialogLocalizer"></param>
        public DialogService(
            IStringLocalizer<EditDialog<DialogOption>> editLocalizer,
            IStringLocalizer<SearchDialog<DialogOption>> seachLocalizer,
            IStringLocalizer<ResultDialogOption> resultDialogLocalizer)
        {
            EditDialogLocalizer = editLocalizer;
            SearchDialogLocalizer = seachLocalizer;
            ResultDialogLocalizer = resultDialogLocalizer;
        }

        /// <summary>
        /// 弹出搜索对话框
        /// </summary>
        /// <param name="option">SearchDialogOption 配置类实例</param>
        public async Task ShowSearchDialog<TModel>(SearchDialogOption<TModel> option)
        {
            option.ResetButtonText ??= SearchDialogLocalizer[nameof(option.ResetButtonText)];
            option.QueryButtonText ??= SearchDialogLocalizer[nameof(option.QueryButtonText)];

            var parameters = new List<KeyValuePair<string, object>>
            {
                new(nameof(SearchDialogOption<TModel>.ShowLabel), option.ShowLabel),
                new(nameof(SearchDialogOption<TModel>.Items), option.Items ?? Utility.GenerateColumns<TModel>(item => item.Searchable)),
                new(nameof(SearchDialogOption<TModel>.OnResetSearchClick), new Func<Task>(async () =>
                {
                    option.OnCloseAsync = null;
                    option.Dialog.RemoveDialog();
                    if(option.OnResetSearchClick != null)
                    {
                        await option.OnResetSearchClick();
                    }
                })),
                new(nameof(SearchDialogOption<TModel>.OnSearchClick), new Func<Task>(async () =>
                {
                    option.OnCloseAsync = null;
                    option.Dialog.RemoveDialog();
                    if(option.OnSearchClick != null)
                    {
                        await option.OnSearchClick();
                    }
                }))
            };
            if (!string.IsNullOrEmpty(option.ResetButtonText))
            {
                parameters.Add(new(nameof(SearchDialogOption<TModel>.ResetButtonText), option.ResetButtonText));
            }

            if (!string.IsNullOrEmpty(option.QueryButtonText))
            {
                parameters.Add(new(nameof(SearchDialogOption<TModel>.QueryButtonText), option.QueryButtonText));
            }

            if (option.Model != null)
            {
                parameters.Add(new(nameof(SearchDialogOption<TModel>.Model), option.Model));
            }

            if (option.DialogBodyTemplate != null)
            {
                parameters.Add(new(nameof(SearchDialogOption<TModel>.BodyTemplate), option.DialogBodyTemplate));
            }

            option.Component = BootstrapDynamicComponent.CreateComponent<SearchDialog<TModel>>(parameters);

            await base.Show(option);
        }

        /// <summary>
        /// 弹出编辑对话框
        /// </summary>
        /// <param name="option">EditDialogOption 配置类实例</param>
        public async Task ShowEditDialog<TModel>(EditDialogOption<TModel> option)
        {
            option.CloseButtonText ??= EditDialogLocalizer[nameof(option.CloseButtonText)];
            option.SaveButtonText ??= EditDialogLocalizer[nameof(option.SaveButtonText)];

            var parameters = new List<KeyValuePair<string, object>>
            {
                new(nameof(EditDialog<TModel>.ShowLoading), option.ShowLoading),
                new(nameof(EditDialog<TModel>.ShowLabel), option.ShowLabel),
                new(nameof(EditDialog<TModel>.Items), option.Items ?? Utility.GenerateColumns<TModel>(item => item.Editable)),
                new(nameof(EditDialog<TModel>.OnCloseAsync), new Func<Task>(async () =>
                {
                    option.Dialog.RemoveDialog();
                    await option.Dialog.CloseOrPopDialog();
                })),
                new(nameof(EditDialog<TModel>.OnSaveAsync), new Func<EditContext, Task>(async context =>
                {
                    if(option.OnSaveAsync != null)
                    {
                        var ret = await option.OnSaveAsync(context);
                        if(ret)
                        {
                            option.Dialog.RemoveDialog();
                            await option.Dialog.CloseOrPopDialog();
                        }
                    }
                }))
            };

            if (!string.IsNullOrEmpty(option.CloseButtonText))
            {
                parameters.Add(new(nameof(EditDialog<TModel>.CloseButtonText), option.CloseButtonText));
            }

            if (!string.IsNullOrEmpty(option.SaveButtonText))
            {
                parameters.Add(new(nameof(EditDialog<TModel>.SaveButtonText), option.SaveButtonText));
            }

            if (option.Model != null)
            {
                parameters.Add(new(nameof(EditDialog<TModel>.Model), option.Model));
            }

            if (option.DialogBodyTemplate != null)
            {
                parameters.Add(new(nameof(EditDialog<TModel>.BodyTemplate), option.DialogBodyTemplate));
            }

            option.Component = BootstrapDynamicComponent.CreateComponent<EditDialog<TModel>>(parameters);

            await base.Show(option);
        }

        /// <summary>
        /// 弹出带结果的对话框
        /// </summary>
        /// <param name="option">对话框参数</param>
        /// <returns></returns>
        public async Task<DialogResult> ShowModal<TDialog>(ResultDialogOption option)
            where TDialog : IComponent, IResultDialog
        {
            IResultDialog? dialog = null;
            var result = DialogResult.Close;

            option.BodyTemplate = builder =>
            {
                builder.OpenComponent(0, typeof(TDialog));
                builder.AddMultipleAttributes(1, option.ComponentParamters);
                builder.AddComponentReferenceCapture(2, com => dialog = (IResultDialog)com);
                builder.CloseComponent();
            };

            option.FooterTemplate = BootstrapDynamicComponent.CreateComponent<ResultDialogFooter>(new KeyValuePair<string, object>[]
            {
                new(nameof(ResultDialogFooter.ShowCloseButton), option.ShowCloseButton),
                new(nameof(ResultDialogFooter.ButtonCloseColor), option.ButtonCloseColor),
                new(nameof(ResultDialogFooter.ButtonCloseIcon), option.ButtonCloseIcon),
                new(nameof(ResultDialogFooter.ButtonCloseText), option.ButtonCloseText ?? ResultDialogLocalizer[nameof(option.ButtonCloseText)]?.Value ?? ""),
                new(nameof(ResultDialogFooter.OnClickClose), new Func<Task>(async () => {
                    result = DialogResult.Close;
                    if(option.OnCloseAsync !=null) { await option.OnCloseAsync(); }
                })),

                new(nameof(ResultDialogFooter.ShowYesButton), option.ShowYesButton),
                new(nameof(ResultDialogFooter.ButtonYesColor), option.ButtonYesColor),
                new(nameof(ResultDialogFooter.ButtonYesIcon), option.ButtonYesIcon),
                new(nameof(ResultDialogFooter.ButtonYesText), option.ButtonYesText ?? ResultDialogLocalizer[nameof(option.ButtonYesText)]?.Value ?? ""),
                new(nameof(ResultDialogFooter.OnClickYes), new Func<Task>(async () => {
                    result = DialogResult.Yes;
                    if(option.OnCloseAsync !=null) { await option.OnCloseAsync(); }
                })),

                new(nameof(ResultDialogFooter.ShowNoButton), option.ShowNoButton),
                new(nameof(ResultDialogFooter.ButtonNoColor), option.ButtonNoColor),
                new(nameof(ResultDialogFooter.ButtonNoIcon), option.ButtonNoIcon),
                new(nameof(ResultDialogFooter.ButtonNoText), option.ButtonNoText ?? ResultDialogLocalizer[nameof(option.ButtonNoText)]?.Value ?? ""),
                new(nameof(ResultDialogFooter.OnClickNo), new Func<Task>(async () => {
                    result = DialogResult.No;
                    if(option.OnCloseAsync !=null) { await option.OnCloseAsync(); }
                }))
            }).Render();

            var closeCallback = option.OnCloseAsync;
            option.OnCloseAsync = async () =>
            {
                if (dialog != null && await dialog.OnClosing(result))
                {
                    await dialog.OnClose(result);
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
            await base.Show(option);
            return await option.ReturnTask.Task;
        }
    }
}
