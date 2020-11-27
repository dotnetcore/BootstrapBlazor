// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
