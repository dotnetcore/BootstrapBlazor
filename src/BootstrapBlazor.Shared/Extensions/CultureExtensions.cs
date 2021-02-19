// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ICulture
    /// </summary>
    public interface ICultureStorage
    {
        /// <summary>
        /// 
        /// </summary>
        public CultureStorageMode Mode { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CultureStorageMode
    {
        /// <summary>
        /// 
        /// </summary>
        Webapi,

        /// <summary>
        /// 
        /// </summary>
        LocalStorage
    }
}
