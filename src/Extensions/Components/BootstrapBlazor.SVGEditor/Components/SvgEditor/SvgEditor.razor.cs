using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SvgEditor
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        [NotNull]
        public string? PreLoad { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<string, Task>? OnSaveChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await InvokeVoidAsync("init", PreLoad, Interop, nameof(GetContent));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [JSInvokable]

        public async Task GetContent(string value)
        {
            if (OnSaveChanged != null)
            {
                await OnSaveChanged(value);
            }
        }
    }
}
