// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ResultDialogDemo2 : ComponentBase, IResultDialog
    {
        private IEnumerable<Foo> SelectedRows { get; set; } = Array.Empty<Foo>();

        [NotNull]
        private IEnumerable<Foo>? Items { get; set; }

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
        private object? Count { get; set; }

        [Inject]
        [NotNull]
        private MessageService? MessageService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = GenerateItems((int)(Count ?? 10));
            Emails ??= Array.Empty<string>();

            SelectedRows = Items.Where(i => Emails.Any(mail => mail == i.Email));
        }

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
        protected static List<Foo> GenerateItems(int startId) => new List<Foo>(Enumerable.Range(startId, 10).Select(i => new Foo()
        {
            Id = i,
            Name = $"张三 {i:d4}",
            Email = $"zhangsan{i:d4}@163.com"
        }));

        /// <summary>
        /// 
        /// </summary>
        public class Foo
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
}
