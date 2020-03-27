namespace BootstrapBlazor.Components
{
    /// <summary>
    /// CheckBox 组件状态枚举值
    /// </summary>
    public enum CheckboxState
    {
        /// <summary>
        /// 未选中
        /// </summary>
        UnChecked,
        /// <summary>
        /// 选中
        /// </summary>
        Checked,
        /// <summary>
        /// 混合模式
        /// </summary>
        Mixed
    }

    /// <summary>
    ///
    /// </summary>
    public static class CheckboxStateExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ToCss(this CheckboxState state)
        {
            var ret = "false";
            switch (state)
            {
                case CheckboxState.Checked:
                    ret = "true";
                    break;
                case CheckboxState.Mixed:
                    ret = "mixed";
                    break;
                case CheckboxState.UnChecked:
                    break;
            }
            return ret;
        }
    }
}
