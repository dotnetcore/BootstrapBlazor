// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class PopoverConfirm : IDisposable
{
    private RenderFragment? Content { get; set; }

    private PopoverConfirmOption? Options { get; set; }

    /// <summary>
    /// 获得/设置 PopoverConfirm 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private PopoverService? PopoverService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PopoverService.Register(this, Show);
    }

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        // 生成代码后，调用 javascript 进行弹窗操作
        if (Options?.Callback != null)
        {
            await Options.Callback();
        }
    }

    private Task Show(PopoverConfirmOption option)
    {
        Options = option;
        Content = builder =>
        {
            var index = 0;
            builder.OpenComponent<PopoverConfirmBox>(index++);
            builder.AddAttribute(index++, nameof(PopoverConfirmBox.SourceId), option.ButtonId);
            builder.AddAttribute(index++, nameof(PopoverConfirmBox.OnConfirm), option.OnConfirm);
            builder.AddAttribute(index++, nameof(PopoverConfirmBox.OnClose), option.OnClose);

            builder.AddAttribute(index++, nameof(PopoverConfirmBox.Title), option.Title);
            builder.AddAttribute(index++, nameof(PopoverConfirmBox.Content), option.Content);

            builder.AddAttribute(index++, nameof(PopoverConfirmBox.CloseButtonText), option.CloseButtonText);
            builder.AddAttribute(index++, nameof(PopoverConfirmBox.CloseButtonColor), option.CloseButtonColor);
            builder.AddAttribute(index++, nameof(PopoverConfirmBox.ConfirmButtonText), option.ConfirmButtonText);
            builder.AddAttribute(index++, nameof(PopoverConfirmBox.ConfirmButtonColor), option.ConfirmButtonColor);
            builder.AddAttribute(index++, nameof(PopoverConfirmBox.Icon), option.Icon);

            builder.CloseComponent();
        };

        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            PopoverService.UnRegister(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
