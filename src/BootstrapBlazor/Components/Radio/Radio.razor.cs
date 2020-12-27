// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Radio
    {
        private IEnumerable<SelectedItem> DataItems => Items ?? new SelectedItem[1] {
            new SelectedItem("", DisplayText ?? "")
            {
                Active = State == CheckboxState.Checked
            }
        };

        private async Task OnChanged(CheckboxState state, SelectedItem val)
        {
            // 子选项点击后，更新其余组件
            if (!IsDisabled)
            {
                // 通知其余兄弟控件
                if (state == CheckboxState.Checked && Items != null)
                {
                    foreach (var item in Items)
                    {
                        item.Active = item == val;
                    }

                    StateHasChanged();
                }

                // 触发外界 OnStateChanged 事件
                if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(val);
                if (OnStateChanged != null) await OnStateChanged.Invoke(state, val);
            }
        }
    }
}
