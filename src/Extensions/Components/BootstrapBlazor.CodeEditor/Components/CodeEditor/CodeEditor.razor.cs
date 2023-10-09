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
    public partial class CodeEditor
    {
        private const string MONACO_VS_PATH = "./_content/BootstrapBlazor.CodeEditor/monaco-editor/min/vs";

        /// <summary>
        /// Language used by the editor: csharp, javascript, ...
        /// </summary>
        [Parameter]
        public string Language { get; set; } = "csharp";

        /// <summary>
        /// Theme of the editor.
        /// </summary>
        [Parameter]
        public string Theme { get; set; } = "vs";

        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        [Parameter]
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public Func<string?, Task>? OnValueChanged { get; set; }

        /// <summary />
        protected override async Task OnParametersSetAsync()
        {
            await InvokeVoidAsync(
                "monacoSetOptions",
                Id,
                new { Value, Theme = Theme, Language });
        }

        /// <summary />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                var options = new
                {
                    Value,
                    Language,
                    Theme = Theme,
                    Path = MONACO_VS_PATH,
                    LineNumbers = true,
                    ReadOnly = false,
                };
                await InvokeVoidAsync("init", Id, Interop, options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Focus() => await InvokeVoidAsync("focus");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Resize() => await InvokeVoidAsync("resize");

        /// <summary />
        [JSInvokable]
        public async Task UpdateValueAsync(string value)
        {
            Value = value;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            if (OnValueChanged != null)
            {
                await OnValueChanged(Value);
            }
        }
    }
}
