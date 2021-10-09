// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    partial class AutoFills
    {
        private Dummy Model { get; set; } = new Dummy();

        [NotNull]
        private IEnumerable<Dummy>? Items { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = Foo.GenerateFoo(LocalizerFoo).Select(i => Dummy.ConvertFromFoo(i));
        }

        private ConcurrentDictionary<int, string> TitleCache { get; } = new();

        private string GetTitle(int id) => TitleCache.GetOrAdd(id, key => Foo.GetTitle());

        private Task OnSelectedItemChanged(Dummy dummy)
        {
            Model = Utility.Clone(dummy);
            StateHasChanged();
            return Task.CompletedTask;
        }

        private class Dummy : Foo, ISelectedItem
        {
            /// <summary>
            /// 
            /// </summary>
            [AutoGenerateColumn(Editable = false)]
            public string Text { get; set; } = "";

            /// <summary>
            /// 
            /// </summary>
            [AutoGenerateColumn(Editable = false)]
            public string Value { get; set; } = "";

            /// <summary>
            /// 
            /// </summary>
            [AutoGenerateColumn(Editable = false)]
            public bool Active { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [AutoGenerateColumn(Editable = false)]
            public bool IsDisabled { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [AutoGenerateColumn(Editable = false)]
            public string GroupName { get; set; } = "";

            /// <summary>
            /// 
            /// </summary>
            /// <param name="foo"></param>
            /// <returns></returns>
            public static Dummy ConvertFromFoo(Foo foo)
            {
                return new Dummy()
                {
                    Address = foo.Address,
                    Complete = foo.Complete,
                    Count = foo.Count,
                    DateTime = foo.DateTime,
                    Education = foo.Education,
                    Id = foo.Id,
                    Name = foo.Name,
                    Text = foo.Name ?? string.Empty,
                    Value = foo.Id.ToString()
                };
            }
        }
    }
}
