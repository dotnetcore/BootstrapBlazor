// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
internal class DefaultExcelExport : ITableExcelExport
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn> cols, IJSRuntime jsRuntime) where TItem : class
    {
        return Task.FromResult(false);
    }
}
