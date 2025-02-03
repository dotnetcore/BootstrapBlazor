// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TypedJs 组件类
/// </summary>
public partial class Typed
{
    /// <summary>
    /// 获得/设置 组件显示文字 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 组件配置 <see cref="TypedOptions"/> 实例 默认 null
    /// </summary>
    [Parameter]
    public TypedOptions? Options { get; set; }

    /// <summary>
    /// 获得/设置 打字结束回调方法 默认 null
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
    /// 打字结束方法 由 Javascript 触发
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
