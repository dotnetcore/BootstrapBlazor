// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MethodTable
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<MethodTable>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        [NotNull]
        public string? Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public IEnumerable<MethodItem>? Items { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Title ??= Localizer[nameof(Title)];

        }
    }
}
