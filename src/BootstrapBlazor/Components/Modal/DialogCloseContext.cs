// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

internal sealed class DialogCloseContext
{
    private enum CloseState
    {
        Open,
        Checking,
        Closing,
        Closed
    }

    private CloseState _state;
    private readonly Dictionary<IComponent, (Func<Task<bool>> CanCloseAsync, Func<bool> IsBusy)> _editors = [];

    internal bool IsClosing => _state is CloseState.Closing or CloseState.Closed;

    internal bool IsClosed => _state == CloseState.Closed;

    internal bool IsCloseRequested => _state != CloseState.Open;

    internal object Presentation { get; private set; } = new();

    internal void Register(IComponent editor, Func<Task<bool>> canCloseAsync, Func<bool> isBusy)
        => _editors[editor] = (canCloseAsync, isBusy);

    internal void UnRegister(IComponent editor) => _editors.Remove(editor);

    internal async Task<bool> TryCloseAsync(Func<Task<bool>>? onClosingAsync, Func<bool>? isCurrent = null)
    {
        if (_state != CloseState.Open || _editors.Values.Any(editor => editor.IsBusy()))
        {
            return false;
        }

        _state = CloseState.Checking;
        var presentation = Presentation;
        bool IsChecking() => _state == CloseState.Checking && ReferenceEquals(presentation, Presentation);
        try
        {
            foreach (var editor in _editors.Values.ToArray())
            {
                if (!IsChecking() || !await editor.CanCloseAsync())
                {
                    return false;
                }
            }
            if (!IsChecking()
                || !await InvokeClosingAsync(onClosingAsync)
                || !IsChecking()
                || _editors.Values.Any(editor => editor.IsBusy())
                || isCurrent?.Invoke() == false)
            {
                return false;
            }

            _state = CloseState.Closing;
            return true;
        }
        finally
        {
            if (IsChecking())
            {
                _state = CloseState.Open;
            }
        }
    }

    internal static async Task<bool> InvokeClosingAsync(Func<Task<bool>>? callbacks)
    {
        var result = true;
        if (callbacks != null)
        {
            foreach (Func<Task<bool>> callback in callbacks.GetInvocationList())
            {
                result &= await callback();
            }
        }
        return result;
    }

    internal void Closed() => _state = CloseState.Closed;

    internal void CancelClose(object presentation)
    {
        if (_state == CloseState.Closing && ReferenceEquals(presentation, Presentation))
        {
            _state = CloseState.Open;
        }
    }

    internal void Reopen()
    {
        if (IsClosed)
        {
            Presentation = new object();
            _state = CloseState.Open;
        }
    }
}
