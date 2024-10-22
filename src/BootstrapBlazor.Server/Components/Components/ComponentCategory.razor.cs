// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

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

    private List<ComponentCard> Cards { get; } = [];

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
