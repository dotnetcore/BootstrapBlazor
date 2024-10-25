// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        IResultDialog? resultDialog = null;
        option.GetDialog = () => resultDialog;
        option.BodyTemplate = builder =>
        {
            var index = 0;
            builder.OpenComponent(index++, typeof(TDialog));
            if (option.ComponentParameters != null)
            {
                builder.AddMultipleAttributes(1, option.ComponentParameters);
            }
            builder.AddComponentReferenceCapture(index++, com => resultDialog = (IResultDialog)com);
            builder.CloseComponent();
        };

        option.FooterTemplate = BootstrapDynamicComponent.CreateComponent<ResultDialogFooter>(new Dictionary<string, object?>
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
    /// 弹出带确认结果的文本对话框
    /// </summary>
    /// <param name="service">DialogService 服务实例</param>
    /// <param name="content">对话框文本参数</param>
    /// <param name="title">对话框标题，优先级低于ResultDialogOption</param>
    /// <param name="elementType">文本的html标签</param>
    /// <param name="isMarkUpString">文本是否为MarkupString</param>
    /// <param name="option">对话框参数</param>
    /// <param name="dialog">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    /// <returns></returns>
    public static async Task<DialogResult> ShowLiteralConfirmModal(this DialogService service, string content, string? title = null, string elementType = "text", bool isMarkUpString = false, ResultDialogOption? option = null, Dialog? dialog = null)
    {
        IResultDialog? resultDialog = null;
        option ??= new();
        option.Title ??= title;
        option.GetDialog = () => resultDialog;
        option.BodyTemplate = builder =>
        {
            var index = 0;
            builder.OpenElement(index++, elementType);
            if (option.ComponentParameters != null)
            {
                builder.AddMultipleAttributes(1, option.ComponentParameters);
            }
            if (isMarkUpString) builder.AddMarkupContent(index++, content); else builder.AddContent(index++, content);
            builder.CloseElement();
        };

        option.FooterTemplate = BootstrapDynamicComponent.CreateComponent<ResultDialogFooter>(new Dictionary<string, object?>
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
}
