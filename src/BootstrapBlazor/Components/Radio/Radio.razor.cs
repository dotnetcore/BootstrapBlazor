// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
