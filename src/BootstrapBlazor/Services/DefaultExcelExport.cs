// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultExcelExport : ITableExcelExport
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols = null, string? fileName = null) where TItem : class
    {
        return Task.FromResult(false);
    }
}
