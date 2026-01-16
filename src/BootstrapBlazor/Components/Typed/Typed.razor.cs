// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TypedJs 组件类</para>
/// <para lang="en">TypedJs component类</para>
/// </summary>
public partial class Typed
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件显示文字 默认 null 未设置</para>
    /// <para lang="en">Gets or sets componentdisplay文字 Default is null 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件配置 <see cref="TypedOptions"/> 实例 默认 null</para>
    /// <para lang="en">Gets or sets component配置 <see cref="TypedOptions"/> instance Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public TypedOptions? Options { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 打字结束回调方法 默认 null</para>
    /// <para lang="en">Gets or sets 打字结束callback method Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnCompleteAsync { get; set; }

    private string? _text;

    private TypedOptions? _options;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _text = Text;
            _options = Options;
        }
        else if (UpdateParameters())
        {
            await InvokeVoidAsync("update", Id, Text, Options);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Text, Options, new
    {
        TriggerComplete = nameof(TriggerComplete)
    });

    /// <summary>
    /// <para lang="zh">打字结束方法 由 Javascript 触发</para>
    /// <para lang="en">打字结束方法 由 Javascript 触发</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerComplete()
    {
        if (OnCompleteAsync != null)
        {
            await OnCompleteAsync();
        }
    }

    private bool UpdateParameters()
    {
        if (Text != _text)
        {
            _text = Text;
            return true;
        }

        if (Options?.Equals(_options) ?? false)
        {
            return true;
        }

        _options = Options;
        return false;
    }
}
