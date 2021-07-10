// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor
{
    /// <summary>
    /// 
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// 淡入效果
        /// </summary>
        [Description("animate__fadeIn")]
        FadeIn,

        /// <summary>
        /// 淡出效果
        /// </summary>
        [Description("animate__fadeOut")]
        FadeOut,
    }
}
