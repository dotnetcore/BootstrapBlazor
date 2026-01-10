// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 正站通知组件
/// </summary>
[JSModuleAutoLoader("Components/GlobalToast.razor.js", JSObjectReference = true)]
public partial class GlobalToast
{
    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Interop, nameof(ShowToast));

    /// <summary>
    /// 显示通知窗口
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task ShowToast()
    {
        // 英文环境不通知
        if (CultureInfo.CurrentUICulture.Name == "en-US")
        {
            return;
        }

        var option = new ToastOption()
        {
            Category = ToastCategory.Information,
            Title = "Gitee 评选活动",
            IsAutoHide = false,
            ChildContent = RenderVote,
            PreventDuplicates = true
        };
        await Toast.Show(option);
    }
}
