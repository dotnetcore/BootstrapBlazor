using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

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
        [Parameter] public IEnumerable<TItem>? Items { get; set; }

        /// <summary>
        /// 点击选择框方法
        /// </summary>
        protected override void OnToggleClick()
        {
            if (!IsDisabled && State == CheckboxState.UnChecked)
            {
                State = CheckboxState.Checked;
                OnStateChanged?.Invoke(State, Value);
            }
        }
    }
}
