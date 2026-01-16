// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Button 按钮组件</para>
/// <para lang="en">Button component</para>
/// </summary>
public partial class Button : ButtonBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否自动获取焦点 默认 false 不自动获取焦点</para>
    /// <para lang="en">Gets or sets whether to auto focus. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsAutoFocus { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 html button 实例</para>
    /// <para lang="en">Gets or sets the html button instance</para>
    /// </summary>
    protected ElementReference ButtonElement { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (IsAutoFocus)
            {
                await FocusAsync();
            }
        }
    }

    /// <summary>
    /// <para lang="zh">OnClickButton 方法</para>
    /// <para lang="en">OnClickButton method</para>
    /// </summary>
    protected virtual async Task OnClickButton()
    {
        if (IsAsync && ButtonType == ButtonType.Button)
        {
            IsAsyncLoading = true;
            IsDisabled = true;
            await Task.Yield();
        }

        await HandlerClick();

        // 恢复按钮
        if (IsAsync && ButtonType == ButtonType.Button)
        {
            IsDisabled = IsKeepDisabled;
            IsAsyncLoading = false;
        }
    }

    /// <summary>
    /// <para lang="zh">自动获得焦点方法</para>
    /// <para lang="en">Auto focus method</para>
    /// </summary>
    /// <returns></returns>
    public ValueTask FocusAsync() => ButtonElement.FocusAsync();
}
