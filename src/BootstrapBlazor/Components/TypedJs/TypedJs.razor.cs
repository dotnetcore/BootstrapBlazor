// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TypedJs 组件类
/// </summary>
public partial class TypedJs
{
    /// <summary>
    /// 获得/设置 组件显示文字 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 组件配置 <see cref="TypedJsOptions"/> 实例 默认 null
    /// </summary>
    [Parameter]
    public TypedJsOptions? Options { get; set; }

    private string? _lastOptions;

    private string? _text;

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
            _lastOptions = Options?.ToString();
            _text = Text;
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
        TriggerStop = nameof(TriggerStop)
    });

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public Task TriggerStart()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public Task TriggerStop()
    {
        return Task.CompletedTask;
    }

    private bool UpdateParameters()
    {
        var ret = false;
        if (Text != _text || Options?.ToString() != _lastOptions)
        {
            _text = Text;
            _lastOptions = Options?.ToString();
            ret = true;
        }
        return ret;
    }
}
