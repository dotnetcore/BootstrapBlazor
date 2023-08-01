using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::Microsoft.AspNetCore.Components;
using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using BootstrapBlazor.Shared.Samples;
using BootstrapBlazor.Shared.Services;
using BootstrapBlazor.Shared.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 工具栏示例代码
/// </summary>
public partial class TablesToolbar
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }
    private static IEnumerable<int> PageItemsSource => new int[]
    {
        2,
        4,
        10,
        20
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // Set the total number of records
        var total = Items.Count;
        // memory paging
        var items = Items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total, IsSorted = true, IsFiltered = true, IsSearch = true });
    }

    private Task<QueryData<Foo>> OnSearchQueryAsync(QueryPageOptions options)
    {
        var items = Items.AsEnumerable();
        if (!string.IsNullOrEmpty(options.SearchText))
        {
            // Fuzzy query against SearchText
            items = items.Where(i => (i.Address ?? "").Contains(options.SearchText) || (i.Name ?? "").Contains(options.SearchText));
        }

        // Set the total number of records
        var total = items.Count();
        // memory paging
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total, IsSorted = true, IsFiltered = true, IsSearch = true });
    }

    private async Task DownloadAsync(IEnumerable<Foo> items)
    {
        // Construct pop-up window configuration information and perform pop-up window operations
        var cate = ToastCategory.Information;
        var title = "Custom download example";
        var content = "Please select the data first, then click the download button";
        if (items.Any())
        {
            cate = ToastCategory.Success;
            content = $"start packing selected {items.Count()} data, this window will be closed automatically after completion";
        }

        var option = new ToastOption()
        {
            Category = cate,
            Title = title,
            Content = content,
        };
        // 弹出 Toast
        await ToastService.Show(option);
        // If the download item is selected for package download operation
        if (items.Any())
        {
            // Disable automatic shutdown
            option.IsAutoHide = false;
            // Start a background process for data processing
            // Passing Option used to be used to close the popup after the asynchronous operation
            await MockDownLoadAsync();
            // Close the popup associated with the option
            option.Close();
            // A pop-up window informs that the download is complete
            await ToastService.Show(new ToastOption() { Category = ToastCategory.Success, Title = "Custom download example", Content = "data download complete", });
        }
    }

    private static async Task MockDownLoadAsync()
    {
        // It takes 5 seconds to simulate the package download data here
        await Task.Delay(5000);
    }

    private Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
    {
        items.ToList().ForEach(foo => Items.Remove(foo));
        return Task.FromResult(true);
    }
}
