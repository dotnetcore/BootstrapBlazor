// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Frame component encapsulates the Html iframe element</para>
/// <para lang="en">Frame component encapsulates the Html iframe element</para>
/// </summary>
public partial class IFrame
{
    /// <summary>
    /// <para lang="zh">获得/设置 the URL of the webpage to be loaded in the Frame</para>
    /// <para lang="en">Gets or sets the URL of the webpage to be loaded in the Frame</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Src { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 数据 to be passed</para>
    /// <para lang="en">Gets or sets the data to be passed</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public object? Data { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Frame loads the 数据 passed by the page</para>
    /// <para lang="en">Gets or sets Frame loads the data passed by the page</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<object?, Task>? OnPostDataAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Callback method after the page is loaded.</para>
    /// <para lang="en">Gets or sets Callback method after the page is loaded.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnReadyAsync { get; set; }

    private string? ClassString => CssBuilder.Default("bb-frame")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private object? _lastData;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _lastData = Data;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_lastData != Data)
        {
            _lastData = Data;
            await PushData(Data);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new
    {
        Data,
        TriggerPostDataCallback = nameof(TriggerPostData),
        TriggerLoadedCallback = nameof(TriggerLoaded)
    });

    /// <summary>
    /// <para lang="zh">Method to push 数据</para>
    /// <para lang="en">Method to push data</para>
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task PushData(object? data) => InvokeVoidAsync("execute", Id, data);

    /// <summary>
    /// <para lang="zh">Called by JavaScript</para>
    /// <para lang="en">Called by JavaScript</para>
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerPostData(object? data)
    {
        if (OnPostDataAsync != null)
        {
            await OnPostDataAsync(data);
        }
    }

    /// <summary>
    /// <para lang="zh">Called by JavaScript</para>
    /// <para lang="en">Called by JavaScript</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerLoaded()
    {
        if (OnReadyAsync != null)
        {
            await OnReadyAsync();
        }
    }
}
