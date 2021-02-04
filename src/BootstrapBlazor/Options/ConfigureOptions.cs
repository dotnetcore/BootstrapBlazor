// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureOptions<TOption> : ConfigureFromConfigurationOptions<TOption> where TOption : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ConfigureOptions(IConfiguration config)
            : base(config.GetSection(typeof(TOption).Name))
        {

        }
    }
}
