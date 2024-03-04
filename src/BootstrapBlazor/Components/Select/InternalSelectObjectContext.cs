// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class InternalSelectObjectContext<TItem> : ISelectObjectContext<TItem>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    public SelectObject<TItem>? Component { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task CloseAsync() => Component.CloseAsync();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(TItem value) => Component.SetValue(value);
}
