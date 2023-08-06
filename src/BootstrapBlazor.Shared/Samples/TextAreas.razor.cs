// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// TextAreas
/// </summary>
public partial class TextAreas
{
    [NotNull]
    private Textarea? TextareaElement { get; set; }

    private string? Text { get; set; }

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

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
            Name = nameof(BootstrapBlazor.Components.Textarea.IsAutoScroll),
            Description = Localizer["TextAreaAutoScroll"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };
}

