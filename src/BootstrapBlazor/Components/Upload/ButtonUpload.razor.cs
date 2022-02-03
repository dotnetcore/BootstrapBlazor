// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class ButtonUpload<TValue>
{
    private bool IsUploadButtonDisabled => IsDisabled || (IsSingle && UploadFiles.Any());

    private string? BrowserButtonClassString => CssBuilder.Default("btn btn-browser")
        .AddClass(BrowserButtonClass)
        .Build();

    /// <summary>
    /// 获得/设置 浏览按钮图标 默认 fa fa-folder-open-o
    /// </summary>
    [Parameter]
    public string BrowserButtonIcon { get; set; } = "fa fa-folder-open-o";

    /// <summary>
    /// 获得/设置 上传按钮样式 默认 btn-primary
    /// </summary>
    [Parameter]
    public string BrowserButtonClass { get; set; } = "btn-primary";

    /// <summary>
    /// 获得/设置 浏览按钮显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? BrowserButtonText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Upload<TValue>>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        BrowserButtonText ??= Localizer[nameof(BrowserButtonText)];
    }
}
