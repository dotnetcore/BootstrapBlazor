// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorDataAnnotationsValidator 验证组件
/// </summary>
public class BootstrapBlazorDataAnnotationsValidator : ComponentBase
{
    /// <summary>
    /// 获得/设置 当前编辑数据上下文
    /// </summary>
    [CascadingParameter]
    [NotNull]
    private EditContext? CurrentEditContext { get; set; }

    /// <summary>
    /// 获得/设置 当前编辑窗体上下文
    /// </summary>
    [CascadingParameter]
    private ValidateForm? ValidateForm { get; set; }

    [Inject]
    [NotNull]
    private IServiceProvider? Provider { get; set; }

    /// <summary>
    /// 初始化方法
    /// </summary>
    protected override void OnInitialized()
    {
        if (ValidateForm == null)
        {
            throw new InvalidOperationException($"{nameof(Components.BootstrapBlazorDataAnnotationsValidator)} requires a cascading " +
                $"parameter of type {nameof(Components.ValidateForm)}. For example, you can use {nameof(Components.BootstrapBlazorDataAnnotationsValidator)} " +
                $"inside an {nameof(Components.ValidateForm)}.");
        }

        CurrentEditContext.AddEditContextDataAnnotationsValidation(ValidateForm, Provider);
    }

    /// <summary>
    /// 手动验证表单方法
    /// </summary>
    /// <returns></returns>
    internal bool Validate() => CurrentEditContext.Validate();
}
