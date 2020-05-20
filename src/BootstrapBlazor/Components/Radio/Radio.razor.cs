using System.Collections.Generic;

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

        private void OnChanged(CheckboxState state, SelectedItem val)
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
                OnStateChanged?.Invoke(state, val);
                if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(val);
            }
        }
    }
}
