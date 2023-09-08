// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Azure.AI.OpenAI;
using BootstrapBlazor.Server.OAuth;
using global::Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BootstrapBlazor.Server.AIChat;

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
    private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }
    private string? Context { get; set; }
    private List<AzureOpenAIChatMessage> Messages { get; } = new();

    [NotNull]
    private string? DisplayName { get; set; }

    [NotNull]
    private string? UserName { get; set; }
    private string? AvatarUrl { get; set; }

    private static string? GetStackClass(ChatRole role) => CssBuilder.Default("msg-stack").AddClass("msg-stack-assistant", role == ChatRole.Assistant).Build();
    ///<inheritdoc/>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id);
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        UserName = state.User.Identity?.Name;
        if (!string.IsNullOrEmpty(UserName))
        {
            if (OAuthHelper.TryGet(UserName, out var user))
            {
                AvatarUrl = user.Avatar_Url;
                DisplayName = Localizer["ChatUserMessageTitle", user.Name, user.Left];
            }
            else
            {
                NavigationManager.NavigateTo("./Account/Logout", true);
            }
        }
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

    private bool IsValid => OAuthHelper.Validate(UserName);

    private async Task GetCompletionsAsync()
    {
        Context = Context?.TrimEnd('\n') ?? string.Empty;
        if (!string.IsNullOrEmpty(Context) && IsValid)
        {
            if (OAuthHelper.TryUpdate(UserName, out var user))
            {
                DisplayName = Localizer["ChatUserMessageTitle", user.Name, user.Left];
            }

            var context = Context;
            Context = string.Empty;
            Messages.Add(new AzureOpenAIChatMessage() { Role = ChatRole.User, Content = context });
            StateHasChanged();
            var msg = new AzureOpenAIChatMessage()
            {
                Role = ChatRole.Assistant,
                Content = "Thinking ..."
            };
            Messages.Add(msg);
            bool first = true;
            await foreach (var chatMessage in OpenAIService.GetChatCompletionsStreamingAsync(context))
            {
                if (first)
                {
                    first = false;
                    msg.Content = string.Empty;
                }

                msg.Content += chatMessage.Content;
                await Task.Delay(50);
                StateHasChanged();
            }
        }
    }

    private Task CreateNewTopic()
    {
        Context = null;
        OpenAIService.CreateNewTopic();
        Messages.Clear();
        return Task.CompletedTask;
    }
}
