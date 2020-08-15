namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Textarea 组件基类
    /// </summary>
    public abstract class TextareaBase : ValidateBase<string?>
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();
    }
}
