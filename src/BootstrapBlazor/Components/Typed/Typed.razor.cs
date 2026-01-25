// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Typed 组件类</para>
/// <para lang="en">Typed Component Class</para>
/// </summary>
public partial class Typed
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件显示文字，默认 null</para>
    /// <para lang="en">Gets or sets the component display text. Default is null</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件配置实例，默认 null</para>
    /// <para lang="en">Gets or sets the component configuration instance. Default is null</para>
    /// </summary>
    [Parameter]
    public TypedOptions? Options { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 打字结束回调方法，默认 null</para>
    /// <para lang="en">Gets or sets the callback method when typing is complete. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnCompleteAsync { get; set; }

    private string? _text;

    private TypedOptions? _options;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
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
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Text, Options, new
    {
        TriggerComplete = nameof(TriggerComplete)
    });

    /// <summary>
    /// <para lang="zh">打字结束方法，由 JavaScript 触发</para>
    /// <para lang="en">Called when typing is complete, triggered by JavaScript</para>
    /// </summary>
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
