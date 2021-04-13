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
    public partial class TablesAutoRefresh
    {
        private static readonly Random random = new();

        [NotNull]
        private List<Foo>? Items { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        private List<Foo> AutoItems { get; set; } = new List<Foo>();

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            AutoItems = Foo.GenerateFoo(Localizer).Take(2).ToList();
        }

        private Task<QueryData<Foo>> OnRefreshQueryAsync(QueryPageOptions options)
        {
            // 设置记录总数
            var total = AutoItems.Count;
            var foo = Foo.Generate(Localizer);
            foo.Id = total++;
            foo.Name = Localizer["Foo.Name", total.ToString("D4")];
            foo.Address = Localizer["Foo.Address", $"{random.Next(1000, 2000)}"];

            AutoItems.Insert(0, foo);

            if (AutoItems.Count > 10)
            {
                AutoItems.RemoveRange(10, 1);
                total = 10;
            }

            // 内存分页
            var items = AutoItems.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<Foo>()
            {
                Items = items,
                TotalCount = total
            });
        }
    }
}
