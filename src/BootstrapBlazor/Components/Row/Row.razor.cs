// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 每行显示多少组件的枚举
    /// </summary>
    public enum ItemsPerRowEnum
    {
        /// <summary>
        /// 每行一个
        /// </summary>
        One = 12,

        /// <summary>
        /// 每行两个
        /// </summary>
        Two = 6,

        /// <summary>
        /// 每行三个
        /// </summary>
        Three = 4,

        /// <summary>
        /// 每行四个
        /// </summary>
        Four = 3,

        /// <summary>
        /// 每行六个
        /// </summary>
        Six = 2,

        /// <summary>
        /// 每行12个
        /// </summary>
        Twelve = 1
    }

    /// <summary>
    /// 行内格式枚举
    /// </summary>
    public enum RowTypeEnum
    {
        /// <summary>
        /// 默认格式
        /// </summary>
        Normal,

        /// <summary>
        /// 在表单中使用 Label 在上，控件充满
        /// </summary>
        FormRow,

        /// <summary>
        /// 表单中使用 label 在左，控件不充满
        /// </summary>
        FormInline
    }

    /// <summary>
    /// 记录 Row 中的控件信息
    /// </summary>
    internal class RowInfo
    {
        /// <summary>
        /// 如果控件本身是 Row，记录他的 ColSpan
        /// </summary>
        public int? ColSpan { get; set; }

        /// <summary>
        /// 指示控件本身是否为 Row
        /// </summary>
        public bool IsRow { get; set; }

        /// <summary>
        /// 控件的RenderFragement
        /// </summary>
        public RenderFragment? Content { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Row
    {
        private string? ClassString => CssBuilder.Default()
            .AddClassFromAttributes(AdditionalAttributes)
            .AddClass("row", RowType == null || RowType == RowTypeEnum.Normal)
            .AddClass("form-row", RowType == RowTypeEnum.FormRow)
            .AddClass("form-inline", RowType == RowTypeEnum.FormInline)
            .Build();

        /// <summary>
        /// 获得/设置 是否显示骨架屏组件 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowSkeleton { get; set; }

        /// <summary>
        /// 获得/设置 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 设置行格式
        /// </summary>
        [Parameter]
        public RowTypeEnum? RowType { get; set; }

        /// <summary>
        /// 获得/设置 记录子控件
        /// </summary>
        internal List<RowInfo> Items { get; } = new List<RowInfo>();

        /// <summary>
        /// 获得/设置 是否第一次渲染 默认 true
        /// </summary>
        public bool FirstRender { get; set; } = true;

        /// <summary>
        /// 获得/设置 最多一共显示多少个控件
        /// </summary>
        [Parameter]
        public int? MaxCount { get; set; }

        /// <summary>
        /// 获得/设置 子 Row 跨父 Row 列数 默认为 null
        /// </summary>
        [Parameter]
        public int? ColSpan { get; set; }

        /// <summary>
        /// 获得/设置 设置一行显示多少个子组件
        /// </summary>
        [Parameter]
        public ItemsPerRowEnum ItemsPerRow { get; set; }

        /// <summary>
        /// AfterRender
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                FirstRender = false;
                StateHasChanged();
            }
        }

        /// <summary>
        /// 设置 RowType 属性值
        /// </summary>
        /// <param name="rowType"></param>
        internal void SetRowType(RowTypeEnum? rowType) => RowType = rowType;
    }
}
