// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DialogHeaderFoo
    {
        [NotNull]
        private IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnValueChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = new[]
            {
                new SelectedItem("beijing", "北京"),
                new SelectedItem("shanghai", "上海")
            };
        }

        private async Task OnSelectedItemChanged(SelectedItem item)
        {
            Value = item.Value;
            if (OnValueChanged != null)
            {
                await OnValueChanged(Value);
            }
        }
    }
}
