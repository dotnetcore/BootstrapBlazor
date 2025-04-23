// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Dialog component
/// </summary>
public partial class Dialog : IDisposable
{
    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    [NotNull]
    private Modal? _modal = null;

    [NotNull]
    private Func<Task>? _onShownAsync = null;

    [NotNull]
    private Func<Task>? _onCloseAsync = null;

    private readonly Dictionary<Dictionary<string, object>, (bool IsKeyboard, bool IsBackdrop)> DialogParameters = [];
    private Dictionary<string, object>? _currentParameter;
    private bool _isKeyboard = false;
    private bool _isBackdrop = false;
    private bool? _isFade = null;

    private string? ClassString => CssBuilder.Default()
        .AddClass("modal-multiple", DialogParameters.Count > 1)
        .AddClass("show", DialogParameters.Count > 0)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // Register Dialog popup event
        DialogService.Register(this, Show);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_currentParameter != null)
        {
            await _modal.Show();
        }
    }

    private async Task Show(DialogOption option)
    {
        _onShownAsync = async () =>
        {
            if (option.OnShownAsync != null)
            {
                await option.OnShownAsync();
            }
        };

        _onCloseAsync = async () =>
        {
            // Callback OnCloseAsync
            if (option.OnCloseAsync != null)
            {
                await option.OnCloseAsync();
            }

            // Remove current DialogParameter
            if (_currentParameter != null)
            {
                DialogParameters.Remove(_currentParameter);

                // Support for multiple dialogs
                var p = DialogParameters.LastOrDefault();
                _currentParameter = p.Key;
                _isKeyboard = p.Value.IsKeyboard;
                _isBackdrop = p.Value.IsBackdrop;

                StateHasChanged();
            }
        };

        _isKeyboard = option.IsKeyboard;
        _isBackdrop = option.IsBackdrop;
        _isFade = option.IsFade;

        option.Modal = _modal;

        var parameters = option.ToAttributes();
        var content = option.BodyTemplate ?? option.Component?.Render();
        if (content != null)
        {
            parameters.Add(nameof(ModalDialog.BodyTemplate), content);
        }

        if (option.HeaderTemplate != null)
        {
            parameters.Add(nameof(ModalDialog.HeaderTemplate), option.HeaderTemplate);
        }

        if (option.HeaderToolbarTemplate != null)
        {
            parameters.Add(nameof(ModalDialog.HeaderToolbarTemplate), option.HeaderToolbarTemplate);
        }

        if (option.FooterTemplate != null)
        {
            parameters.Add(nameof(ModalDialog.FooterTemplate), option.FooterTemplate);
        }

        if (!string.IsNullOrEmpty(option.Class))
        {
            parameters.Add(nameof(ModalDialog.Class), option.Class);
        }

        if (option.OnSaveAsync != null)
        {
            parameters.Add(nameof(ModalDialog.OnSaveAsync), option.OnSaveAsync);
        }

        if (option.CloseButtonText != null)
        {
            parameters.Add(nameof(ModalDialog.CloseButtonText), option.CloseButtonText);
        }
        if (option.CloseButtonIcon != null)
        {
            parameters.Add(nameof(ModalDialog.CloseButtonIcon), option.CloseButtonIcon);
        }

        if (option.SaveButtonText != null)
        {
            parameters.Add(nameof(ModalDialog.SaveButtonText), option.SaveButtonText);
        }
        if (option.SaveButtonIcon != null)
        {
            parameters.Add(nameof(ModalDialog.SaveButtonIcon), option.SaveButtonIcon);
        }

        if (option is ResultDialogOption resultOption)
        {
            parameters.Add(nameof(ModalDialog.ResultTask), resultOption.ResultTask);
            if (resultOption.GetDialog != null)
            {
                parameters.Add(nameof(ModalDialog.GetResultDialog), resultOption.GetDialog);
            }
        }

        // Save current Dialog parameters
        _currentParameter = parameters;

        // Add ModalDialog to the container
        DialogParameters.Add(parameters, (_isKeyboard, _isBackdrop));
        await InvokeAsync(StateHasChanged);
    }

    private static RenderFragment RenderDialog(int index, Dictionary<string, object> parameter) => builder =>
    {
        builder.OpenComponent<ModalDialog>(100 + index);
        builder.AddMultipleAttributes(101 + index, parameter);
        builder.SetKey(parameter);
        builder.CloseComponent();
    };

    /// <summary>
    /// Dispose method
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DialogService.UnRegister(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
