// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableSearchDialog<TModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Parameter]
        [NotNull]
        public Func<Task>? OnResetSearchClick { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Parameter]
        [NotNull]
        public Func<Task>? OnSearchClick { get; set; }

        /// <summary>
        /// 重置按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ResetButtonText { get; set; }

        /// <summary>
        /// 查询按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? QueryButtonText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<TableSearchDialog<TModel>>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ResetButtonText ??= Localizer[nameof(ResetButtonText)];
            QueryButtonText ??= Localizer[nameof(QueryButtonText)];

            if (OnSearchClick == null) OnSearchClick = () => Task.CompletedTask;
            if (OnResetSearchClick == null) OnResetSearchClick = () => Task.CompletedTask;
        }
    }
}
