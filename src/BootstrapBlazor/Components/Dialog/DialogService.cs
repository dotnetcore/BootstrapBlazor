// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="editLocalizer"></param>
        /// <param name="seachLocalizer"></param>
        public DialogService(IStringLocalizer<EditDialog<DialogOption>> editLocalizer, IStringLocalizer<SearchDialog<DialogOption>> seachLocalizer)
        {
            EditDialogLocalizer = editLocalizer;
            SearchDialogLocalizer = seachLocalizer;
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
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.Columns), option.Columns!),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.BodyTemplate), option.DialogBodyTemplate!),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.OnResetSearchClick), new Func<Task>(async () =>
                {
                    if(option.OnResetSearchClick != null)
                    {
                        await option.OnResetSearchClick();
                    }

                    if(option.ResetBodyTemplate != null) await option.ResetBodyTemplate();
                })),
                new KeyValuePair<string, object>(nameof(SearchDialogOption<TModel>.OnSearchClick), new Func<Task>(async () =>
                {
                    if(option.OnSearchClick != null)
                    {
                        await option.OnSearchClick();
                    }

                    if(option.ResetBodyTemplate != null) await option.ResetBodyTemplate();
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
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.Columns), option.Columns!),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.BodyTemplate), option.DialogBodyTemplate!),
                new KeyValuePair<string, object>(nameof(EditDialog<TModel>.OnCloseAsync), new Func<Task>(async () =>
                {
                    if(option.OnCloseAsync != null)
                    {
                        await option.OnCloseAsync();
                    }

                    if(option.ResetBodyTemplate != null) await option.ResetBodyTemplate();
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

                    if(option.ResetBodyTemplate != null) await option.ResetBodyTemplate();
                }))
            });

            await base.Show(option);
        }
    }
}
