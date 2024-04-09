// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components.Extensions;

internal static class ITableColumnExtensions
{
    public static async Task<object?> FormatValue(this ITableColumn col, object? value, TableExportOptions options)
    {
        var ret = value;
        if (ret != null)
        {
            if (options.EnableLookup && col.Lookup != null)
            {
                ret = col.Lookup.FirstOrDefault(i => i.Value.Equals(value?.ToString(), col.LookupStringComparison))?.Text;
            }
            if (options.EnableFormat && col.Formatter != null)
            {
                // 格式化回调委托
                ret = await col.Formatter(value);
            }
            else if (options.EnableFormat && !string.IsNullOrEmpty(col.FormatString))
            {
                // 格式化字符串
                ret = Utility.Format(value, col.FormatString);
            }
            else if (options.UseEnumDescription && col.PropertyType.IsEnum())
            {
                ret = col.PropertyType.ToDescriptionString(value?.ToString());
            }
            else if (options.AutoMergeArray && value is IEnumerable<object> v)
            {
                ret = string.Join(options.ArrayDelimiter, v);
            }
        }
        return ret;
    }
}
