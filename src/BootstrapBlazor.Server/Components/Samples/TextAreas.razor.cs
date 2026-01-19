// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// TextAreas
/// </summary>
public partial class TextAreas
{
    [NotNull]
    private Textarea? TextareaElement { get; set; }

    private string? Text { get; set; }

    private string? KeyText { get; set; }

    private string? ChatText { get; set; }

    private int ScrollValue { get; set; }

    private bool IsAutoScroll { get; set; }

    private CancellationTokenSource? CancelTokenSource { get; set; }

    private int Index { get; set; } = 5;

    private Task ScrollToTop() => TextareaElement.ScrollToTop();

    private Task ScrollToBottom() => TextareaElement.ScrollToBottom();

    private Task ScrollTo20() => TextareaElement.ScrollTo(ScrollValue += 20);

    private void SwitchAutoScroll() => IsAutoScroll = !IsAutoScroll;

    private bool IsRunMock { get; set; }

    private string ChatLocalizerName => IsRunMock ? "TextAreaMockChatStop" : "TextAreaMockChatRun";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        for (int i = 0; i < Index; i++)
        {
            ChatText += $"{((i % 2 == 0) ? "A" : "B")} : {Localizer["TextAreaMockChat"]}{i}{Environment.NewLine}";
        }
    }

    /// <summary>
    /// MockChat
    /// </summary>
    private void MockChat()
    {
        if (CancelTokenSource != null)
        {
            CancelTokenSource.Cancel();
            CancelTokenSource.Dispose();
            CancelTokenSource = null;
        }

        IsRunMock = !IsRunMock;

        if (IsRunMock)
        {
            Task.Run(async () =>
            {
                CancelTokenSource ??= new();
                while (CancelTokenSource != null && !CancelTokenSource.IsCancellationRequested)
                {
                    ChatText += $"{((Index % 2 == 0) ? "A" : "B")} : {Localizer["TextAreaMockChat"]}{Index}{Environment.NewLine}";
                    Index++;
                    await InvokeAsync(StateHasChanged);

                    try
                    {
                        if (CancelTokenSource != null)
                        {
                            await Task.Delay(500, CancelTokenSource.Token);
                        }
                    }
                    catch (TaskCanceledException) { }
                }
            });
        }
    }

    [NotNull]
    private ConsoleLogger? ConsoleLogger { get; set; }

    private Task OnEnterAsync(string val)
    {
        ConsoleLogger.Log($"Trigger Enter Key Event");
        return Task.CompletedTask;
    }

    private Task OnEscAsync(string val)
    {
        ConsoleLogger.Log($"Trigger Esc Key Event");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing)
    {
        if (disposing && CancelTokenSource != null)
        {
            CancelTokenSource.Cancel();
            CancelTokenSource.Dispose();
            CancelTokenSource = null;
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["TextAreaShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DisplayText",
            Description = Localizer["TextAreaDisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["TextAreaIsDisabled"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ScrollToTop",
            Description = Localizer["TextAreaScrollToTop"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "ScrollTo",
            Description = Localizer["TextAreaScrollTo"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "ScrollToBottom",
            Description = Localizer["TextAreaScrollToBottom"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = nameof(Textarea.IsAutoScroll),
            Description = Localizer["TextAreaAutoScroll"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Textarea.UseShiftEnter),
            Description = Localizer["TextAreaUseShiftEnter"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];
}

