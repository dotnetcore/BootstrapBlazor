// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 
/// </summary>
public sealed partial class TablesToolbar
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 2, 4, 10, 20 };

    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(Localizer);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // 设置记录总数
        var total = Items.Count;

        // 内存分页
        var items = Items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true
        });
    }

    private Task<QueryData<Foo>> OnSearchQueryAsync(QueryPageOptions options)
    {
        var items = Items.AsEnumerable();
        if (!string.IsNullOrEmpty(options.SearchText))
        {
            // 针对 SearchText 进行模糊查询
            items = items.Where(i => (i.Address ?? "").Contains(options.SearchText)
                    || (i.Name ?? "").Contains(options.SearchText));
        }

        // 设置记录总数
        var total = items.Count();

        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true
        });
    }

    private Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
    {
        items.ToList().ForEach(foo => Items.Remove(foo));
        return Task.FromResult(true);
    }

    private async Task DownloadAsync(IEnumerable<Foo> items)
    {
        // 构造弹窗配置信息，进行弹窗操作
        var cate = ToastCategory.Information;
        var title = "自定义下载示例";
        var content = "请先选择数据，然后点击下载按钮";
        if (items.Any())
        {
            cate = ToastCategory.Success;
            content = $"开始打包选中的 {items.Count()} 条数据，完成后自动关闭本窗口";
        }

        var option = new ToastOption()
        {
            Category = cate,
            Title = title,
            Content = content,
        };

        // 弹出 Toast
        await ToastService.Show(option);

        // 如果已选择下载项进行打包下载操作
        if (items.Any())
        {
            // 禁止自动关闭
            option.IsAutoHide = false;

            // 开启后台进程进行数据处理
            // 传递 Option 过去是为了异步操作结束后可以关闭弹窗
            await MockDownLoadAsync();

            // 关闭 option 相关联的弹窗
            await option.Close();

            // 弹窗告知下载完毕
            await ToastService.Show(new ToastOption()
            {
                Category = ToastCategory.Success,
                Title = "自定义下载示例",
                Content = "数据下载完毕",
            });
        }
    }

    private static async Task MockDownLoadAsync()
    {
        // 此处模拟打包下载数据耗时 5 秒
        await Task.Delay(5000);
    }
}
