// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Modal 弹窗组件
    /// </summary>
    public abstract class ModalBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 ModalDialog 集合
        /// </summary>
        protected List<ModalDialogBase> Dialogs { get; private set; } = new List<ModalDialogBase>(50);

        /// <summary>
        /// 获得/设置 是否后台关闭弹窗
        /// </summary>
        [Parameter]
        public bool IsBackdrop { get; set; }

        /// <summary>
        /// 获得/设置 是否开启淡入淡出动画 默认为 true 开启动画
        /// </summary>
        [Parameter]
        public bool IsFade { get; set; } = true;

        /// <summary>
        /// 获得/设置 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 弹窗状态切换方法
        /// </summary>
        public abstract ValueTask Toggle();

        /// <summary>
        /// 显示弹窗方法
        /// </summary>
        /// <returns></returns>
        public abstract ValueTask Show();

        /// <summary>
        /// 关闭弹窗方法
        /// </summary>
        /// <returns></returns>
        public abstract ValueTask Close();

        /// <summary>
        /// 添加对话窗方法
        /// </summary>
        /// <param name="dialog"></param>
        public void AddDialog(ModalDialogBase dialog)
        {
            if (!Dialogs.Any()) dialog.IsShown = true;
            Dialogs.Add(dialog);
        }

        /// <summary>
        /// 显示指定对话框方法
        /// </summary>
        /// <param name="dialog"></param>
        public void ShowDialog(ModalDialogBase dialog)
        {
            Dialogs.ForEach(d => d.IsShown = d == dialog);
            StateHasChanged();
        }
    }
}
