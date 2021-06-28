// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 动态表格示例
    /// </summary>
    public partial class TablesDynamic
    {
        [NotNull]
        private DataTableDynamicContext? DataTableDynamicContext { get; set; }

        private DataTable UserData { get; } = new DataTable();

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 初始化 DataTable
            InitDataTable();

            // 初始化动态类型上下文实例
            DataTableDynamicContext = new DataTableDynamicContext()
            {
                DataTable = UserData
            };
        }

        private void InitDataTable()
        {
            UserData.Columns.Add(nameof(Foo.DateTime), typeof(DateTime));
            UserData.Columns.Add(nameof(Foo.Name), typeof(string));
            UserData.Columns.Add(nameof(Foo.Count), typeof(int));

            Foo.GenerateWrapFoo(Localizer).ForEach(f =>
            {
                UserData.Rows.Add(f.DateTime, f.Name, f.Count);
            });
        }
    }
}
