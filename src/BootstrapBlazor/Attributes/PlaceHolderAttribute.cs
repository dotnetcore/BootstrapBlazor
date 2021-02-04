// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PlaceHolderAttribute : Attribute
    {
        /// <summary>
        /// 获得 Order 属性
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeholder"></param>
        public PlaceHolderAttribute(string placeholder)
        {
            Text = placeholder;
        }
    }
}
