// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 单选框组件
    /// </summary>
    public class RadioBase<TItem> : CheckboxBase<TItem>
    {
        /// <summary>
        /// 获得/设置 绑定数据源
        /// </summary>
        [Parameter]
        public IEnumerable<TItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 是否为竖向排列 默认为 false
        /// </summary>
        [Parameter]
        public bool IsVertical { get; set; }

        /// <summary>
        /// 点击选择框方法
        /// </summary>
        protected override async Task OnToggleClick()
        {
            if (!IsDisabled && State == CheckboxState.UnChecked)
            {
                State = CheckboxState.Checked;
                if (OnStateChanged != null) await OnStateChanged.Invoke(State, Value);
            }
        }
    }
}
