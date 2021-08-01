// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ColorPicker
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得/设置 input 类型 placeholder 属性
        /// </summary>
        protected string? PlaceHolder { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("placeholder", out var ph))
            {
                PlaceHolder = ph?.ToString();
            }
            if (string.IsNullOrEmpty(PlaceHolder) && FieldIdentifier.HasValue)
            {
                PlaceHolder = FieldIdentifier.Value.GetPlaceHolder();
            }
        }
    }
}
