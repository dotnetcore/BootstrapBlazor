using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DataAnnotationsValidator 验证组件
    /// </summary>
    public class BootstrapBlazorDataAnnotationsValidator : ComponentBase
    {
        /// <summary>
        /// 获得/设置 当前编辑数据上下文
        /// </summary>
        [CascadingParameter]
        EditContext? CurrentEditContext { get; set; }

        /// <summary>
        /// 获得/设置 当前编辑窗体上下文
        /// </summary>
        [CascadingParameter]
        public ValidateFormBase? EditForm { get; set; }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(DataAnnotationsValidator)} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {nameof(DataAnnotationsValidator)} inside an EditForm.");
            }

            if (EditForm == null)
            {
                throw new InvalidOperationException($"{nameof(DataAnnotationsValidator)} requires a cascading parameter of type {nameof(ValidateFormBase)}. For example, you can use {nameof(DataAnnotationsValidator)} inside an EditForm.");
            }

            CurrentEditContext.AddEditContextDataAnnotationsValidation(EditForm);
        }

        /// <summary>
        /// 表单验证方法
        /// </summary>
        /// <returns></returns>
        public void Validate()
        {
            if (CurrentEditContext != null)
            {
                var valid = CurrentEditContext.Validate();

                if (EditForm != null)
                {
                    if (valid && EditForm.OnValidSubmit.HasDelegate) EditForm.OnValidSubmit.InvokeAsync(CurrentEditContext);
                    else if (valid && EditForm.OnSubmit.HasDelegate) EditForm.OnSubmit.InvokeAsync(CurrentEditContext);
                    else if (EditForm.OnInvalidSubmit.HasDelegate) EditForm.OnInvalidSubmit.InvokeAsync(CurrentEditContext);
                }
            }
        }
    }
}
