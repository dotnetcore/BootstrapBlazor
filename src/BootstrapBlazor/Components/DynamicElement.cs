using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Renders an element with the specified name and attributes. This is useful
    /// when you want to combine a set of attributes declared at compile time with
    /// another set determined at runtime.
    /// </summary>
    public class DynamicElement : ComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the element to render.
        /// </summary>
        [Parameter] public string TagName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public ElementReference? ElementRef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public Action<ElementReference>? ElementRefChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            builder.OpenElement(index++, TagName);
            if (AdditionalAttributes != null)
            {
                if (AdditionalAttributes.Remove("@onclick", out var v))
                {
                    builder.AddAttribute(index++, "onclick", v);
                }
                builder.AddMultipleAttributes(index++, AdditionalAttributes);
            }
            if (ChildContent != null) builder.AddContent(index++, ChildContent);
            if (ElementRefChanged != null) builder.AddElementReferenceCapture(index++, capturedRef =>
            {
                ElementRef = capturedRef;
                ElementRefChanged.Invoke(capturedRef);
            });
            builder.CloseElement();
        }
    }
}
