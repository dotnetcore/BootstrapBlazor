// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BootstrapBlazor.Components;

internal static class VirtualizeHelper
{
    public static RenderFragment Render<TItem>(
        ItemsProviderDelegate<TItem> itemsProvider,
        RenderFragment<TItem> itemContent,
        RenderFragment<PlaceholderContext> placeholder,
        float itemSize,
        int overscanCount,
#if NET11_0_OR_GREATER
        int initialItemIndex,
        VirtualizeAnchorMode anchorMode,
        IEqualityComparer<TItem> itemComparer,
#endif
        Action<Virtualize<TItem>> componentRef) => builder =>
    {
        builder.OpenComponent<Virtualize<TItem>>(0);
        builder.AddAttribute(1, nameof(Virtualize<TItem>.ItemsProvider), itemsProvider);
        builder.AddAttribute(2, nameof(Virtualize<TItem>.ItemContent), itemContent);
        builder.AddAttribute(3, nameof(Virtualize<TItem>.Placeholder), placeholder);
        builder.AddAttribute(4, nameof(Virtualize<TItem>.ItemSize), itemSize);
        builder.AddAttribute(5, nameof(Virtualize<TItem>.OverscanCount), overscanCount);
#if NET11_0_OR_GREATER
        builder.AddAttribute(6, nameof(Virtualize<TItem>.InitialItemIndex), initialItemIndex);
        builder.AddAttribute(7, nameof(Virtualize<TItem>.AnchorMode), anchorMode);
        builder.AddAttribute(8, nameof(Virtualize<TItem>.ItemComparer), itemComparer);
#endif
        builder.AddComponentReferenceCapture(9, component => componentRef((Virtualize<TItem>)component));
        builder.CloseComponent();
    };

    public static RenderFragment Render<TItem>(
        ICollection<TItem> items,
        RenderFragment<TItem> itemContent,
        float itemSize,
        int overscanCount,
#if NET11_0_OR_GREATER
        int initialItemIndex,
        VirtualizeAnchorMode anchorMode,
#endif
        Action<Virtualize<TItem>> componentRef) => builder =>
    {
        builder.OpenComponent<Virtualize<TItem>>(0);
        builder.AddAttribute(1, nameof(Virtualize<TItem>.Items), items);
        builder.AddAttribute(2, nameof(Virtualize<TItem>.ItemContent), itemContent);
        builder.AddAttribute(3, nameof(Virtualize<TItem>.ItemSize), itemSize);
        builder.AddAttribute(4, nameof(Virtualize<TItem>.OverscanCount), overscanCount);
#if NET11_0_OR_GREATER
        builder.AddAttribute(5, nameof(Virtualize<TItem>.InitialItemIndex), initialItemIndex);
        builder.AddAttribute(6, nameof(Virtualize<TItem>.AnchorMode), anchorMode);
#endif
        builder.AddComponentReferenceCapture(7, component => componentRef((Virtualize<TItem>)component));
        builder.CloseComponent();
    };
}
