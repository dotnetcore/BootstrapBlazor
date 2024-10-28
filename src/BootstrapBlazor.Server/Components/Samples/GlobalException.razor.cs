﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class GlobalException
{
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    private static void OnClick()
    {
        // NET6.0 采用 ErrorLogger 统一处理
        var a = 0;
        _ = 1 / a;
    }

    private Task OnErrorHandleAsync(ILogger logger, Exception ex) => SwalService.Show(new SwalOption()
    {
        Category = SwalCategory.Error,
        Title = "Oops...",
        Content = ex.Message,
        ShowFooter = true,
        FooterTemplate = BootstrapDynamicComponent.CreateComponent<SwalFooter>().Render()
    });

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(ErrorLogger.ChildContent),
            Description = "子组件模板",
            Type = nameof(RenderTemplate),
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ErrorLogger.ErrorContent),
            Description = "异常显示模板",
            Type = nameof(RenderTemplate),
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ErrorLogger.ShowToast),
            Description = "是否显示错误消息弹窗",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        }
    ];
}
