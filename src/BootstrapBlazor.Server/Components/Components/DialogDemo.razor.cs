// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class DialogDemo
{
    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    private string Title { get; } = DateTime.Now.ToString();

    private Task OnClickButton() => DialogService.Show(new DialogOption()
    {
        Title = "Pop-up",
        IsDraggable = true,
        IsKeyboard = true,
        IsBackdrop = true,
        Component = BootstrapDynamicComponent.CreateComponent<DialogDemo>()
    });
}
