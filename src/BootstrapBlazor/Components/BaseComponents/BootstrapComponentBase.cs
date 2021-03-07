// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Bootstrap Blazor 组件基类
    /// </summary>
    public abstract class BootstrapComponentBase : ComponentBase, IDisposable
    {
        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject]
        [NotNull]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 获得/设置 Row 组件实例
        /// </summary>
        [CascadingParameter]
        protected Row? ParentRow { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (ParentRow != null && (ParentRow.MaxCount == null || ParentRow.Items.Count < ParentRow.MaxCount))
            {
                var rf = new RowInfo();
                if (this is Row row)
                {
                    rf.ColSpan = row.ColSpan;
                    rf.IsRow = true;
                    if (row.RowType == null)
                    {
                        row.SetRowType(ParentRow.RowType);
                    }
                }
                rf.Content = async b =>
                {
                    if (this is Row r)
                    {
                        r.FirstRender = false;
                    }
                    this.BuildRenderTree(b);
                    await this.OnAfterRenderAsync(false);
                    this.OnAfterRender(false);
                };
                ParentRow.Items.Add(rf);
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
