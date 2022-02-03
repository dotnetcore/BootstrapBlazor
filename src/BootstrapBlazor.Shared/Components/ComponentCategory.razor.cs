// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class ComponentCategory
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? Desc { get; set; }

    private List<ComponentCard> Cards { get; } = new List<ComponentCard>();

    internal void Add(ComponentCard card) => Cards.Add(card);

    private int CardCount => Cards.Where(c => !c.IsHide).Count();

    private bool IsRendered { get; set; }

    private string? ClassString => CssBuilder.Default("coms-cate")
        .AddClass("d-none", IsRendered && CardCount == 0)
        .Build();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            IsRendered = true;
        }
    }
}
