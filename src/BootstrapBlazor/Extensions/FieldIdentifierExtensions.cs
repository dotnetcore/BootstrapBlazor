// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace Microsoft.AspNetCore.Components.Forms
{
    /// <summary>
    /// FieldIdentifier 扩展操作类
    /// </summary>
    public static class FieldIdentifierExtensions
    {
        /// <summary>
        /// 获取显示名称方法
        /// </summary>
        /// <param name="fieldIdentifier"></param>
        /// <returns></returns>
        public static string GetDisplayName(this FieldIdentifier fieldIdentifier) => Utility.GetDisplayName(fieldIdentifier.Model, fieldIdentifier.FieldName);
    }
}
