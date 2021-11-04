// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
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
        [NotNull]
        private Foo Model { get; set; } = new();

        [NotNull]
        private IEnumerable<Foo>? Items { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = Foo.GenerateFoo(LocalizerFoo);
        }

        private Task OnSelectedItemChanged(Foo foo)
        {
            Model = Utility.Clone(foo);
            StateHasChanged();
            return Task.CompletedTask;
        }

        private string OnGetDisplayText(Foo foo) => foo.Name ?? "";
    }
}
