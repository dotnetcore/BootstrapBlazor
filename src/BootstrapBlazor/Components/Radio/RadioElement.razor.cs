namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RadioElement
    {
        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // 根据值设置是否选中
            State = Value.Active ? CheckboxState.Checked : CheckboxState.UnChecked;
        }
    }
}
