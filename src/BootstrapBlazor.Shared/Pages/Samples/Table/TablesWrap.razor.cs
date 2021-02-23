// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 折行演示示例代码
    /// </summary>
    public sealed partial class TablesWrap
    {
        private static readonly Random random = new Random();

        [NotNull]
        private IEnumerable<Foo>? CellItems { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            CellItems = Enumerable.Range(1, 4).Select(i => new Foo()
            {
                Id = i,
                Name = Localizer["Foo.Name", $"{i:d4}"],
                DateTime = DateTime.Now.AddDays(i - 1),
                Address = Localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
                Count = random.Next(1, 100),
                Complete = random.Next(1, 100) > 50,
                Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
            }).ToList();
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("$.table_wrap");
            }
        }
    }
}
