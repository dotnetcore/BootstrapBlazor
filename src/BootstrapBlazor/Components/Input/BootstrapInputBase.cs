using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BootstrapInputTextBase 组件
    /// </summary>
    public abstract class BootstrapInputBase<TItem> : ValidateInputBase<TItem>
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected override string? ClassName => CssBuilder.Default("form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// OnInitialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (AdditionalAttributes != null)
            {
                if (!AdditionalAttributes.TryGetValue("type", out var _))
                {
                    AdditionalAttributes.Add("type", "text");
                }
            }
        }
    }
}
