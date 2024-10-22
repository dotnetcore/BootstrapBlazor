﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class ResultDialogDemo2 : ComponentBase, IResultDialog
{
    private List<Foo> SelectedRows { get; set; } = [];

    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Parameter]

    public IEnumerable<string>? Emails { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Parameter]
    public EventCallback<IEnumerable<string>> EmailsChanged { get; set; }

    [CascadingParameter(Name = "BodyContext")]
    private object? BodyContext { get; set; }

    [Inject]
    [NotNull]
    private MessageService? MessageService { get; set; }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions option)
    {
        // 模拟查询数据
        var context = BodyContext as FooContext;
        Items = GenerateItems(context?.Count ?? 10);
        var data = new QueryData<Foo>()
        {
            TotalCount = Items.Count,
            Items = Items
        };

        // 处理选中行
        Emails = context?.Emails?.Split(";") ?? [];
        SelectedRows.AddRange(Items.Where(i => Emails.Any(mail => mail == i.Email)));
        return Task.FromResult(data);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<bool> OnClosing(DialogResult result)
    {
        var ret = true;
        if (result == DialogResult.Yes && SelectedRows.Count == 0)
        {
            await MessageService.Show(new MessageOption()
            {
                Content = "请至少选择一位用户！"
            });
            ret = false;
        }
        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    public async Task OnClose(DialogResult result)
    {
        if (result == DialogResult.Yes)
        {
            if (EmailsChanged.HasDelegate)
            {
                Emails = SelectedRows.Where(r => !string.IsNullOrEmpty(r.Email)).Select(r => r.Email!).ToList();
                await EmailsChanged.InvokeAsync(Emails);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private static List<Foo> GenerateItems(int startId) => new(Enumerable.Range(startId, 10).Select(i => new Foo()
    {
        Id = i,
        Name = $"张三 {i:d4}",
        Email = $"zhangsan{i:d4}@163.com"
    }));

    /// <summary>
    /// 
    /// </summary>
    public class FooContext
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Emails { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    private class Foo
    {
        /// <summary>
        ///
        /// </summary>
        [DisplayName("员工ID")]
        public int? Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DisplayName("员工姓名")]
        public string? Name { get; set; }
        /// <summary>
        ///
        /// </summary>
        [DisplayName("员工邮箱")]
        public string? Email { get; set; }
    }
}
