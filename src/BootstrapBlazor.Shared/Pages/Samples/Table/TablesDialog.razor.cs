// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TablesDialog
    {
        private static readonly Random random = new Random();

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        [NotNull]
        private Modal? Modal { get; set; }

        [NotNull]
        private Table<Foo>? ProductTable { get; set; }

        [NotNull]
        private List<Foo>? Products { get; set; }

        [NotNull]
        private List<Foo>? ProductSelectItems { get; set; }

        private bool _confirm;

        private IEnumerable<Foo> SelectedRows { get; set; } = Enumerable.Empty<Foo>();

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Products = new List<Foo>();

            ProductSelectItems = Enumerable.Range(1, 5).Select(i => new Foo()
            {
                Id = i,
                Name = Localizer["Foo.Name", $"{i:d4}"],
                DateTime = DateTime.Now.AddDays(i - 1),
                Address = Localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
                Count = random.Next(1, 100),
                Complete = random.Next(1, 100) > 50,
                Education = EnumEducation.Primary,
                Hobby = new string[] { "1" }
            }).ToList();
        }

        private async Task ShowDialog(IEnumerable<Foo> items)
        {
            await Modal.Toggle();
        }

        private async Task OnConfirm()
        {
            _confirm = true;
            await Modal.Toggle();
            await ProductTable.QueryAsync();
        }

        private Task<bool> OnSaveAsync(Foo item)
        {
            var oldItem = Products.FirstOrDefault(i => i.Id == item.Id);
            if (oldItem != null)
            {
                oldItem.Count = item.Count;
            }
            return Task.FromResult(true);
        }

        private Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
        {
            Products.RemoveAll(p => items.Contains(p));
            return Task.FromResult(true);
        }

        private Task<QueryData<Foo>> OnQueryEditAsync(QueryPageOptions options)
        {
            var items = Products;
            if (_confirm)
            {
                items.Clear();
                items.AddRange(SelectedRows);
            }
            _confirm = false;

            var total = items.Count;
            // 内存分页
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
            return Task.FromResult(new QueryData<Foo>()
            {
                Items = items,
                TotalCount = total,
                IsFiltered = true,
                IsSearch = true,
                IsSorted = true
            });
        }

        private Task<QueryData<Foo>> OnQueryProductAsync(QueryPageOptions options)
        {
            var items = ProductSelectItems;

            var total = items.Count;
            // 内存分页
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<Foo>()
            {
                Items = items,
                TotalCount = total,
            });
        }
    }
}
