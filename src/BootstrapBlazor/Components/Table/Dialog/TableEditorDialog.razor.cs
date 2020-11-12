using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableEditorDialog<TModel>
    {
        /// <summary>
        /// 获得/设置 保存回调委托
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnSaveAsync { get; set; }

        /// <summary>
        /// 重置按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? CloseButtonText { get; set; }

        /// <summary>
        /// 查询按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SaveButtonText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<TableEditorDialog<TModel>>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            CloseButtonText ??= Localizer[nameof(CloseButtonText)];
            SaveButtonText ??= Localizer[nameof(SaveButtonText)];
        }
    }
}
