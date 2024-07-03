// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews2;

/// <summary>
/// 锁定示例
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

    private Task OnLockChangedCallbackAsync(string[] panels, bool state)
    {
        IsLock = state;
        return Task.CompletedTask;
    }
}
