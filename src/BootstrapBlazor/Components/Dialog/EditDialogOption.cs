// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 编辑弹窗配置类
    /// </summary>
    public class EditDialogOption<TModel> : DialogOption
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EditDialogOption()
        {
            IsScrolling = true;
            Size = Size.ExtraLarge;
            ShowCloseButton = false;
            ShowFooter = false;
            ShowLabel = true;
        }

        /// <summary>
        /// 获得/设置 是否显示标签 默认为 true 显示标签
        /// </summary>
        public bool ShowLabel { get; set; }

        /// <summary>
        /// 获得/设置 查询时是否显示正在加载中动画 默认为 false
        /// </summary>
        public bool ShowLoading { get; set; }

        /// <summary>
        /// 获得/设置 编辑框模型
        /// </summary>
        public TModel? Model { get; set; }

        /// <summary>
        /// 获得 编辑项集合
        /// </summary>
        public IEnumerable<IEditorItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 EditDialog Body 模板
        /// </summary>
        public RenderFragment<TModel>? DialogBodyTemplate { get; set; }

        /// <summary>
        /// 获得/设置 保存回调委托
        /// </summary>
        public Func<EditContext, Task<bool>>? OnSaveAsync { get; set; }

        /// <summary>
        /// 获得/设置 关闭按钮文本
        /// </summary>
        public string? CloseButtonText { get; set; }

        /// <summary>
        /// 获得/设置 查询按钮文本
        /// </summary>
        public string? SaveButtonText { get; set; }
    }
}
