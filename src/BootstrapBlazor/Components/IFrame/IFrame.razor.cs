// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Frame component encapsulates the Html iframe element
/// </summary>
public partial class IFrame
{
    /// <summary>
    /// Gets or sets the URL of the webpage to be loaded in the Frame
    /// </summary>
    [Parameter]
    public string? Src { get; set; }

    /// <summary>
    /// Gets or sets the data to be passed
    /// </summary>
    [Parameter]
    public object? Data { get; set; }

    /// <summary>
    /// Gets or sets Frame loads the data passed by the page
    /// </summary>
    [Parameter]
    public Func<object?, Task>? OnPostDataAsync { get; set; }

    /// <summary>
    /// Gets or sets Callback method after the page is loaded.
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
    /// Method to push data
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task PushData(object? data) => InvokeVoidAsync("execute", Id, data);

    /// <summary>
    /// Called by JavaScript
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
    /// Called by JavaScript
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
