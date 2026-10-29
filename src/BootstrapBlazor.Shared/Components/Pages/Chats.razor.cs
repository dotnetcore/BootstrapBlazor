// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Components.Pages;

/// <summary>
/// <para lang="zh">AI 流式聊天示例。</para>
/// <para lang="en">AI streaming chat sample.</para>
/// </summary>
public partial class Chats
{
    [Inject]
    [NotNull]
    private ISampleChatService? ChatService { get; set; }

    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? WebsiteOption { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private ILogger<Chats>? Logger { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Chats>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IBrowserFingerService? BrowserFingerService { get; set; }

    private string? Context { get; set; }

    private List<SampleChatMessage> Messages { get; } = [];

    private static string? GetStackClass(bool isAssistant) => CssBuilder.Default("msg-stack")
        .AddClass("msg-stack-assistant", isAssistant)
        .Build();

    private const int TotalCount = ISampleChatService.TotalCount;
    private readonly CancellationTokenSource _lifetime = new();
    private string? _code;
    private int _currentCount;
    private bool _isDisabled = true;
    private bool _isSending;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            try
            {
                _code = await GetFingerCodeAsync();
                await RefreshQuotaAsync();
                StateHasChanged();
            }
            catch (OperationCanceledException) when (_lifetime.IsCancellationRequested)
            {
                Logger.LogDebug("Chat initialization was cancelled on navigation.");
            }
            catch (Exception ex) when (!_lifetime.IsCancellationRequested)
            {
                Logger.LogError(ex, "Failed to initialize chat quota.");
                await ToastService.Error(Localizer["ChatsTitle"], Localizer["ChatFailed"]);
            }
        }
        else
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
        if (_isDisabled || _isSending || _code == null)
        {
            return;
        }
        Context = Context?.TrimEnd('\n') ?? string.Empty;
        if (!string.IsNullOrEmpty(Context))
        {
            if (Context.Length > 8000)
            {
                await ToastService.Information(Localizer["ChatsTitle"], Localizer["ChatTooLong"]);
                return;
            }
            _isSending = true;
            _isDisabled = true;
            var context = Context;
            Context = string.Empty;
            Messages.Add(new SampleChatMessage() { Content = context });
            var msg = new SampleChatMessage()
            {
                IsAssistant = true,
                Content = "Thinking ..."
            };
            Messages.Add(msg);
            await Task.Yield();

            bool first = true;
            bool completed = false;
            try
            {
                await foreach (var update in ChatService.StreamAsync(new() { Code = _code, Content = context }, _lifetime.Token))
                {
                    if (!string.IsNullOrEmpty(update.Content))
                    {
                        if (first)
                        {
                            first = false;
                            msg.Content = string.Empty;
                        }
                        msg.Content += update.Content;
                    }
                    if (update.Completed && update.Remaining is { } remaining)
                    {
                        completed = true;
                        _currentCount = remaining;
                    }
                    // TODO: NET 11 误报警告
#pragma warning disable BL0012 // Unnecessary StateHasChanged call
                    StateHasChanged();
#pragma warning restore BL0012 // Unnecessary StateHasChanged call
                }
                if (!completed)
                {
                    throw new EndOfStreamException("Chat generation ended without completion.");
                }
            }
            catch (OperationCanceledException) when (_lifetime.IsCancellationRequested)
            {
                Logger.LogDebug("Chat generation was cancelled on navigation.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Chat generation failed.");
                msg.Content = first ? Localizer["ChatFailed"] : $"{msg.Content}\n\n{Localizer["ChatFailed"]}";
                if (!_lifetime.IsCancellationRequested)
                {
                    await ToastService.Error(Localizer["ChatsTitle"], Localizer["ChatFailed"]);
                }
            }
            finally
            {
                _isSending = false;
                if (!_lifetime.IsCancellationRequested)
                {
                    try
                    {
                        await RefreshQuotaAsync();
                    }
                    catch (Exception ex)
                    {
                        _isDisabled = true;
                        Logger.LogError(ex, "Failed to refresh chat quota.");
                        await ToastService.Error(Localizer["ChatsTitle"], Localizer["ChatFailed"]);
                    }
                }
            }
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
        if (_isSending)
        {
            return;
        }
        Context = null;
        Messages.Clear();
    }

    private async Task RefreshQuotaAsync()
    {
        _currentCount = await ChatService.GetRemainingAsync(_code!, _lifetime.Token);
        _isDisabled = _currentCount < 1;
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            await _lifetime.CancelAsync();
            _lifetime.Dispose();
        }
        await base.DisposeAsync(disposing);
    }
}
