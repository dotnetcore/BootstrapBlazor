// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

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
            [nameof(SearchDialog<TModel>.Items)] = option.Items ?? Utility.GenerateColumns<TModel>(item => item.GetSearchable()),
            [nameof(SearchDialog<TModel>.OnResetSearchClick)] = new Func<Task>(async () =>
            {
                if (option.OnResetSearchClick != null)
                {
                    await option.OnResetSearchClick();
                }
            }),
            [nameof(SearchDialog<TModel>.OnSearchClick)] = new Func<Task>(async () =>
            {
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
    /// <param name="service"><see cref="DialogService"/> 服务实例</param>
    /// <param name="option"><see cref="ITableEditDialogOption{TModel}"/> 配置类实例</param>
    /// <param name="dialog"></param>
    public static async Task ShowEditDialog<TModel>(this DialogService service, EditDialogOption<TModel> option, Dialog? dialog = null)
    {
        option.Component = BootstrapDynamicComponent.CreateComponent<EditDialog<TModel>>(option.ToParameter());
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
        if (option.BodyTemplate == null)
        {
            IResultDialog? resultDialog = null;
            option.GetDialog = () => resultDialog;
            option.BodyTemplate = builder =>
            {
                builder.OpenComponent(0, typeof(TDialog));
                if (option.ComponentParameters != null)
                {
                    builder.AddMultipleAttributes(10, option.ComponentParameters);
                }
                builder.AddComponentReferenceCapture(30, com => resultDialog = (IResultDialog)com);
                builder.CloseComponent();
            };
        }

        option.FooterTemplate ??= BootstrapDynamicComponent.CreateComponent<ResultDialogFooter>(new Dictionary<string, object?>
        {
            [nameof(ResultDialogFooter.ButtonNoText)] = option.ButtonNoText,
            [nameof(ResultDialogFooter.ButtonYesText)] = option.ButtonYesText,
            [nameof(ResultDialogFooter.ShowYesButton)] = option.ShowYesButton,
            [nameof(ResultDialogFooter.ButtonYesColor)] = option.ButtonYesColor,
            [nameof(ResultDialogFooter.ButtonYesIcon)] = option.ButtonYesIcon,
            [nameof(ResultDialogFooter.ShowNoButton)] = option.ShowNoButton,
            [nameof(ResultDialogFooter.ButtonNoColor)] = option.ButtonNoColor,
            [nameof(ResultDialogFooter.ButtonNoIcon)] = option.ButtonNoIcon
        }).Render();

        if (option.ResultTask.Task.IsCompleted)
        {
            option.ResultTask = new();
        }
        await service.Show(option, dialog);
        return await option.ResultTask.Task;
    }

    /// <summary>
    /// 弹出带结果的对话框
    /// </summary>
    /// <param name="service">DialogService 服务实例</param>
    /// <param name="title">对话框标题，优先级高于 <see cref="DialogOption.Title"/></param>
    /// <param name="content">对话框 <see cref="MarkupString"/> 文本参数</param>
    /// <param name="option"><see cref="ResultDialogOption"/> 对话框参数实例</param>
    /// <param name="dialog">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public static Task<DialogResult> ShowModal(this DialogService service, string title, string content, ResultDialogOption? option = null, Dialog? dialog = null)
    {
        option ??= new();
        if (!string.IsNullOrEmpty(title))
        {
            option.Title = title;
        }
        if (!string.IsNullOrEmpty(content))
        {
            IResultDialog? resultDialog = null;
            option.GetDialog = () => resultDialog;
            option.BodyTemplate = builder =>
            {
                builder.OpenComponent(0, typeof(ResultDialog));
                builder.AddAttribute(20, nameof(ResultDialog.Content), content);
                builder.AddComponentReferenceCapture(30, com => resultDialog = (IResultDialog)com);
                builder.CloseComponent();
            };
        }
        return ShowModal<ResultDialog>(service, option, dialog);
    }

    private class ResultDialog : ComponentBase, IResultDialog
    {
        [Parameter]
        public string? Content { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddMarkupContent(0, Content);
        }

        public Task OnClose(DialogResult result)
        {
            return Task.CompletedTask;
        }
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

    /// <summary>
    /// 显示异常信息对话框扩展方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="fragment"></param>
    /// <param name="dialog"></param>
    /// <returns></returns>
    public static async Task ShowErrorHandlerDialog(this DialogService service, RenderFragment fragment, Dialog? dialog = null)
    {
        var option = new DialogOption
        {
            Title = "Error",
            IsScrolling = true,
            BodyTemplate = fragment
        };
        await service.Show(option, dialog);
    }
}
