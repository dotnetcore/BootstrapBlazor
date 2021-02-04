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

            option.Component = DynamicComponent.CreateComponent<SearchDialog<TModel>>(new[]
            {
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.Model), option.Model!),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.ShowLabel), option.ShowLabel),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.ResetButtonText), option.ResetButtonText!),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.QueryButtonText), option.QueryButtonText!),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.Items), option.Items!),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.BodyTemplate), option.DialogBodyTemplate!),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.OnResetSearchClick), new Func<Task>(async () =>
                {
                    if(option.OnResetSearchClick != null)
                    {
                        await option.OnResetSearchClick();
                    }
                })),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.OnSearchClick), new Func<Task>(async () =>
                {
                    if(option.OnSearchClick != null)
                    {
                        await option.OnSearchClick();
                    }
                }))
            });

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

            option.Component = DynamicComponent.CreateComponent<EditDialog<TModel>>(new[]
            {
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.Model), option.Model!),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.ShowLabel), option.ShowLabel),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.CloseButtonText), option.CloseButtonText!),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.SaveButtonText), option.SaveButtonText!),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.Items), option.Items!),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.BodyTemplate), option.DialogBodyTemplate!),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.OnCloseAsync), new Func<Task>(async () =>
                {
                    if(option.OnCloseAsync != null)
                    {
                        await option.OnCloseAsync();
                    }
                })),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.OnSaveAsync), new Func<EditContext, Task>(async context =>
                {
                    if(option.OnSaveAsync != null)
                    {
                        var ret = await option.OnSaveAsync(context);
                        if(ret)
                        {
                            await option.Dialog!.Close();
                        }
                    }
                }))
            });

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
                builder.AddMultipleAttributes(2, option.ComponentParamters);
                builder.AddComponentReferenceCapture(1, com => dialog = (IResultDialog)com);
                builder.SetKey(Guid.NewGuid());
                builder.CloseComponent();
            };

            option.FooterTemplate = DynamicComponent.CreateComponent<ResultDialogFooter>(new KeyValuePair<string, object>[]
            {
                new(nameof(ResultDialogFooter.ShowCloseButton), option.ShowCloseButton),
                new(nameof(ResultDialogFooter.ButtonCloseColor), option.ButtonCloseColor),
                new(nameof(ResultDialogFooter.ButtonCloseIcon), option.ButtonCloseIcon),
                new(nameof(ResultDialogFooter.ButtonCloseText), option.ButtonCloseText ?? ResultDialogLocalizer[nameof(option.ButtonCloseText)] ?? ""),
                new(nameof(ResultDialogFooter.OnClickClose), new Func<Task>(async () => {
                    result = DialogResult.Close;
                    await option.OnCloseAsync!();
                })),

                new(nameof(ResultDialogFooter.ShowYesButton), option.ShowYesButton),
                new(nameof(ResultDialogFooter.ButtonYesColor), option.ButtonYesColor),
                new(nameof(ResultDialogFooter.ButtonYesIcon), option.ButtonYesIcon),
                new(nameof(ResultDialogFooter.ButtonYesText), option.ButtonYesText ?? ResultDialogLocalizer[nameof(option.ButtonYesText)] ?? ""),
                new(nameof(ResultDialogFooter.OnClickYes), new Func<Task>(async () => {
                    result = DialogResult.Yes;
                    await option.OnCloseAsync!();
                })),

                new(nameof(ResultDialogFooter.ShowNoButton), option.ShowNoButton),
                new(nameof(ResultDialogFooter.ButtonNoColor), option.ButtonNoColor),
                new(nameof(ResultDialogFooter.ButtonNoIcon), option.ButtonNoIcon),
                new(nameof(ResultDialogFooter.ButtonNoText), option.ButtonNoText?? ResultDialogLocalizer[nameof(option.ButtonNoText)] ?? ""),
                new(nameof(ResultDialogFooter.OnClickNo), new Func<Task>(async () => {
                    result = DialogResult.No;
                    await option.OnCloseAsync!();
                }))
            }).Render();

            var closeCallback = option.OnCloseAsync;
            option.OnCloseAsync = async () =>
            {
                if (await dialog!.OnClosing(result))
                {
                    await dialog!.OnClose(result);
                    if (closeCallback != null) await closeCallback();
                    await option.Dialog!.Close();
                    option.ReturnTask.SetResult(result);
                }
            };
            await base.Show(option);
            return await option.ReturnTask.Task;
        }
    }
}
