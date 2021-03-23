// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 每行显示多少组件的枚举
    /// </summary>
    public enum ItemsPerRow
    {
        /// <summary>
        /// 每行一个
        /// </summary>
        [Description("12")]
        One,

        /// <summary>
        /// 每行两个
        /// </summary>
        [Description("6")]
        Two,

        /// <summary>
        /// 每行三个
        /// </summary>
        [Description("4")]
        Three,

        /// <summary>
        /// 每行四个
        /// </summary>
        [Description("3")]
        Four,

        /// <summary>
        /// 每行六个
        /// </summary>
        [Description("2")]
        Six,

        /// <summary>
        /// 每行12个
        /// </summary>
        [Description("1")]
        Twelve
    }
}
