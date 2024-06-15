// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Mask
{
    [Inject]
    [NotNull]
    private IMaskService? MaskService { get; set; }

    private bool IsMasking { get; set; }

    private string? ClassString() => CssBuilder.Default("mask")
        .AddClass("show", IsMasking)
        .Build();

    private RenderFragment? BodyTemplate { get; set; }

    private int ZIndex { get; set; } = 1000;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (MaskService is MaskService service)
        {
            service.Register(this, Show, Close);
        }
    }

    private Task Show(MaskOption? option)
    {
        if (option != null)
        {
            if (option.BodyTemplate != null)
            {
                BodyTemplate = option.BodyTemplate;
            }
            ZIndex = option.ZIndex;
            IsMasking = true;
            StateHasChanged();
        }

        return Task.CompletedTask;
    }

    private Task Close()
    {
        IsMasking = false;
        StateHasChanged();

        return Task.CompletedTask;
    }
}
