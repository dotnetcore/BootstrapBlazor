// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIPLocator
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="option">定位器配置信息</param>
        /// <returns>定位器定位结果</returns>
        Task<string> Locate(IPLocatorOption option);
    }
}
