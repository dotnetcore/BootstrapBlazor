// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// 树形数据演示示例代码
    /// </summary>
    public partial class TablesTree
    {
        [NotNull]
        private IEnumerable<FooTree>? TreeItems { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        private int level = 0;

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TreeItems = FooTree.Generate(Localizer);
        }

        private Task<IEnumerable<FooTree>> OnTreeExpand(FooTree foo) => Task.FromResult(FooTree.Generate(Localizer, level++ < 2, foo.Id + 10).Select(i =>
         {
             i.Name = Localizer["Foo.Name", $"{foo.Id:d2}{i.Id:d2}"];
             return i;
         }));

        private class FooTree : Foo
        {
            private static readonly Random random = new();

            public IEnumerable<FooTree>? Children { get; set; }

            public bool HasChildren { get; set; }

            public static IEnumerable<FooTree> Generate(IStringLocalizer<Foo> localizer, bool hasChildren = true, int seed = 0) => Enumerable.Range(1, 2).Select(i => new FooTree()
            {
                Id = i + seed,
                Name = localizer["Foo.Name", $"{seed:d2}{(i + seed):d2}"],
                DateTime = System.DateTime.Now.AddDays(i - 1),
                Address = localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
                Count = random.Next(1, 100),
                Complete = random.Next(1, 100) > 50,
                Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel,
                HasChildren = hasChildren
            }).ToList();
        }
    }
}
