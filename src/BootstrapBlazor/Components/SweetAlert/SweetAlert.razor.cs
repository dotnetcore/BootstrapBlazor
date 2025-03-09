// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// SweetAlert component
/// </summary>
public partial class SweetAlert : IAsyncDisposable
{
    /// <summary>
    /// Gets or sets the Modal container component instance
    /// </summary>
    [NotNull]
    private Modal? ModalContainer { get; set; }

    /// <summary>
    /// Gets or sets the DialogServices service instance
    /// </summary>
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    private bool IsShowDialog { get; set; }

    private bool IsAutoHide { get; set; }

    private int Delay { get; set; }

    private CancellationTokenSource DelayToken { get; set; } = new();

    [NotNull]
    private Dictionary<string, object>? DialogParameter { get; set; }

    [NotNull]
    private Func<Task>? OnCloseAsync { get; set; }

    private SweetContext _context = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // Register Swal popup event
        SwalService.Register(this, Show);

        // Set OnCloseAsync callback method
        OnCloseAsync = () =>
        {
            IsShowDialog = false;
            DialogParameter = null;
            if (AutoHideCheck())
            {
                DelayToken.Cancel();
            }
            if (_context != null)
            {
                _context.ConfirmTask.TrySetResult(_context.Value);
            }
            StateHasChanged();
            return Task.CompletedTask;
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (IsShowDialog)
        {
            // Open popup
            await ModalContainer.Show();

            // Auto close handling logic
            if (AutoHideCheck())
            {
                try
                {
                    if (DelayToken.IsCancellationRequested)
                    {
                        DelayToken.Dispose();
                        DelayToken = new();
                    }
                    await Task.Delay(Delay, DelayToken.Token);
                    await ModalContainer.Close();

                    if (OnCloseCallbackAsync != null)
                    {
                        await OnCloseCallbackAsync();
                    }
                }
                catch (TaskCanceledException) { }
            }
        }
    }

    private bool AutoHideCheck() => IsAutoHide && Delay > 0;

    private Func<Task>? OnCloseCallbackAsync = null;

    private async Task Show(SwalOption option)
    {
        if (!IsShowDialog)
        {
            // Ensure only one popup is opened
            IsShowDialog = true;

            IsAutoHide = option.IsAutoHide;
            Delay = option.Delay;

            option.Modal = ModalContainer;
            if (option.IsConfirm)
            {
                _context = new() { ConfirmTask = new() };
                option.ConfirmContext = _context;
            }
            var parameters = option.ToAttributes();
            parameters.Add(nameof(ModalDialog.BodyTemplate), BootstrapDynamicComponent.CreateComponent<SweetAlertBody>(option.Parse()).Render());

            DialogParameter = parameters;

            OnCloseCallbackAsync = AutoHideCheck() ? option.OnCloseAsync : null;

            // Render UI to prepare popup Dialog
            await InvokeAsync(StateHasChanged);
        }
    }

    private RenderFragment RenderDialog() => builder =>
    {
        if (DialogParameter != null)
        {
            var index = 0;
            builder.OpenComponent<ModalDialog>(index++);
            builder.SetKey(DialogParameter);
            builder.AddMultipleAttributes(index++, DialogParameter);
            builder.CloseComponent();
        }
    };

    private bool disposed;

    /// <summary>
    /// Dispose method
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!disposed && disposing)
        {
            disposed = true;

            if (IsShowDialog)
            {
                // Close popup
                DelayToken.Cancel();
                await ModalContainer.Close();
                IsShowDialog = false;
            }

            // Release Token
            DelayToken.Dispose();

            // Unregister service
            SwalService.UnRegister(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
