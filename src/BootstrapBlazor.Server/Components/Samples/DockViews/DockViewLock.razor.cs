// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.DockViews;

/// <summary>
/// DockViewLock 示例
/// </summary>
public partial class DockViewLock
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DockViewLock>? Localizer { get; set; }

    private bool IsLock { get; set; } = true;

    private string? LockText { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        LockText = IsLock ? "解锁" : "锁定";
    }

    private void OnToggleLock()
    {
        IsLock = !IsLock;
        if (IsLock == true)
        {
            LockText = "解锁";
        }
        else
        {
            LockText = "锁定";
        }
    }

    private Task OnLockChangedCallbackAsync(bool state)
    {
        IsLock = state;
        return Task.CompletedTask;
    }
}
