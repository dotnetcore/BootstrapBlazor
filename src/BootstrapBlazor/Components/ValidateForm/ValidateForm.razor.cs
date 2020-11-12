using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ValidateForm 组件类
    /// </summary>
    public sealed partial class ValidateForm
    {
        /// <summary>
        /// A callback that will be invoked when the form is submitted.
        /// If using this parameter, you are responsible for triggering any validation
        /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be valid.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnValidSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be invalid.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnInvalidSubmit { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            if (OnSubmit == null) OnSubmit = _ => Task.CompletedTask;
            if (OnValidSubmit == null) OnValidSubmit = _ => Task.CompletedTask;
            if (OnInvalidSubmit == null) OnInvalidSubmit = _ => Task.CompletedTask;
            return base.OnInitializedAsync();
        }
    }
}
