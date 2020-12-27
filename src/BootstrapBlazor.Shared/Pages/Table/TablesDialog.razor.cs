// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TablesDialog
    {
#nullable disable
        private Modal Modal { get; set; }

        private Table<Product> ProductTable { get; set; }
#nullable restore

        private List<Product> Products { get; set; } = new List<Product>();

        private List<Product> ProductSelectItems { get; set; } = new List<Product>();

        private bool _confirm;

        private IEnumerable<Product> SelectedRows { get; set; } = Enumerable.Empty<Product>();

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ProductSelectItems = Enumerable.Range(1, 5).Select(i => new Product()
            {
                Id = i,
                Name = $"项目 {i:d4}",
                Type = $"商品 {random.Next(1000, 2000)} 类",
                Counter = random.Next(1, 100),
                Price = random.Next(100, 1000),
                DateTime = DateTime.Now.AddDays(i - 1)
            }).ToList();
        }

        private async Task ShowDialog(IEnumerable<Product> items)
        {
            await Modal.Toggle();
        }

        private async Task OnConfirm()
        {
            _confirm = true;
            await Modal.Toggle();
            await ProductTable.QueryAsync();
        }

        private Task<bool> OnSaveAsync(Product item)
        {
            var oldItem = Products.FirstOrDefault(i => i.Id == item.Id);
            if (oldItem != null)
            {
                oldItem.Sum = item.Sum;
            }
            return Task.FromResult(true);
        }

        private Task<bool> OnDeleteAsync(IEnumerable<Product> items)
        {
            Products.RemoveAll(p => items.Contains(p));
            return Task.FromResult(true);
        }

        private Task<QueryData<Product>> OnQueryEditAsync(QueryPageOptions options)
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
            return Task.FromResult(new QueryData<Product>()
            {
                Items = items,
                TotalCount = total
            });
        }

        private Task<QueryData<Product>> OnQueryProductAsync(QueryPageOptions options)
        {
            var items = ProductSelectItems;

            var total = items.Count;
            // 内存分页
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<Product>()
            {
                Items = items,
                TotalCount = total,
            });
        }

        private class Product
        {
            [DisplayName("编号")]
            public int Id { get; set; }

            [DisplayName("类别")]
            public string? Type { get; set; }

            [DisplayName("项目")]
            public string? Name { get; set; }

            [DisplayName("价格")]
            public int Price { get; set; } = 1;

            [DisplayName("数量")]
            public int Counter { get; set; }

            [DisplayName("日期")]
            public DateTime? DateTime { get; set; }

            [DisplayName("金额")]
            public int Sum { get; set; }
        }
    }
}
