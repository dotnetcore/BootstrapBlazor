﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components;

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
