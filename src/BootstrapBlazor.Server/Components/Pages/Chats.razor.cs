// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Azure.AI.OpenAI;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// AI 聊天示例
/// </summary>
public partial class Chats
{
    [Inject]
    [NotNull]
    private IAzureOpenAIService? OpenAIService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Chats>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IBrowserFingerService? BrowserFingerService { get; set; }

    private string? Context { get; set; }

    private List<AzureOpenAIChatMessage> Messages { get; } = [];

    private static string? GetStackClass(ChatRole role) => CssBuilder.Default("msg-stack").AddClass("msg-stack-assistant", role == ChatRole.Assistant).Build();

    private static readonly ConcurrentDictionary<string, int> _cache = new();

    private string? _code;

    private readonly int _totalCount = 50;

    private int _currentCount;

    private bool _isDisabled;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _code = await GetFingerCodeAsync();
        _currentCount = _cache.GetOrAdd(_code, key => _totalCount);
        _isDisabled = _currentCount < 1;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("scroll", Id);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id);

    private async Task GetCompletionsAsync()
    {
        Context = Context?.TrimEnd('\n') ?? string.Empty;
        if (!string.IsNullOrEmpty(Context))
        {
            var context = Context;
            Context = string.Empty;
            Messages.Add(new AzureOpenAIChatMessage() { Role = ChatRole.User, Content = context });
            var msg = new AzureOpenAIChatMessage()
            {
                Role = ChatRole.Assistant,
                Content = "Thinking ..."
            };
            Messages.Add(msg);
            StateHasChanged();

            bool first = true;
            await foreach (var chatMessage in OpenAIService.GetChatCompletionsStreamingAsync(context))
            {
                if (first)
                {
                    first = false;
                    msg.Content = string.Empty;
                }

                await Task.Delay(50);
                if (!string.IsNullOrEmpty(chatMessage.Content))
                {
                    msg.Content += chatMessage.Content;
                    StateHasChanged();
                }
            }

            _ = Task.Run(async () =>
            {
                await Task.Delay(100);
                if (!string.IsNullOrEmpty(_code))
                {
                    _currentCount = _cache.AddOrUpdate(_code, key => _totalCount, (key, number) => number - 1);
                    _isDisabled = _currentCount < 1;
                }
                else
                {
                    _isDisabled = true;
                }
                await InvokeAsync(StateHasChanged);
            });
        }
    }

    private async Task<string> GetFingerCodeAsync()
    {
        var code = await BrowserFingerService.GetFingerCodeAsync();
        code ??= "BootstrapBlazor";
        return code;
    }

    private void CreateNewTopic()
    {
        Context = null;
        OpenAIService.CreateNewTopic();
        Messages.Clear();
    }
}
