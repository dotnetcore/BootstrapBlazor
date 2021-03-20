// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Foo = BootstrapBlazor.Shared.Pages.Components.Foo;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TablesBase : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        protected ToastService? ToastService { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected static readonly Random random = new();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static List<Foo> GenerateItems() => Enumerable.Range(1, 80).Select(i => new Foo()
        {
            Id = i,
            Name = $"张三 {i:d4}",
            DateTime = DateTime.Now.AddDays(i - 1),
            Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
            Count = random.Next(1, 100),
            Complete = random.Next(1, 100) > 50,
            Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
        }).ToList();

        /// <summary>
        /// 
        /// </summary>
        protected static List<Foo> Items { get; } = GenerateItems();

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<SelectedItem> Hobbys => Foo.GenerateHobbys(Localizer);
    }
}
