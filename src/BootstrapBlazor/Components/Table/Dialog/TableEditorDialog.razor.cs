using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableEditorDialog<TModel>
    {
#nullable disable
        /// <summary>
        /// 获得/设置 保存回调委托
        /// </summary>
        [Parameter]
        public Func<EditContext, Task> OnSaveAsync { get; set; }
#nullable restore
    }
}
