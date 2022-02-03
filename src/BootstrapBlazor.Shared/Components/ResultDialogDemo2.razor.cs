// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public partial class ResultDialogDemo2 : ComponentBase, IResultDialog
{
    private List<Foo> SelectedRows { get; set; } = new List<Foo>();

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

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var context = BodyContext as FooContext;
        Items = GenerateItems(context?.Count ?? 10);
        Emails = context?.Emails?.Split(";") ?? Array.Empty<string>();

        SelectedRows = Items.Where(i => Emails.Any(mail => mail == i.Email)).ToList();
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions option) => Task.FromResult(new QueryData<Foo>()
    {
        TotalCount = Items.Count,
        Items = Items
    });

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> OnClosing(DialogResult result)
    {
        var ret = true;
        if (result == DialogResult.Yes && !SelectedRows.Any())
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
