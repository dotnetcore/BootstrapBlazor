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
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // TODO: 此处应该检查 html5 type 类型检查
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
