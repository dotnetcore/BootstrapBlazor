// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Components.Authorization;

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

    private string? Context { get; set; }

    private List<AzureOpenAIChatMessage> Messages { get; } = [];

    private static string? GetStackClass(ChatRole role) => CssBuilder.Default("msg-stack").AddClass("msg-stack-assistant", role == ChatRole.Assistant).Build();

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
        }
    }

    private void CreateNewTopic()
    {
        Context = null;
        OpenAIService.CreateNewTopic();
        Messages.Clear();
    }
}
