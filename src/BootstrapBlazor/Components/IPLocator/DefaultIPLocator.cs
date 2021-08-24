// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    ///
    /// </summary>
    internal class DefaultIPLocator : IIPLocator
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual Task<string> Locate(IPLocatorOption option) => Task.FromResult(string.Empty);

        /// <summary>
        ///
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        protected static async Task<string> Locate<T>(IPLocatorOption option) where T : class
        {
            string? ret = null;
            try
            {
                if (!string.IsNullOrEmpty(option.Url) && !string.IsNullOrEmpty(option.IP) && option.HttpClient != null)
                {
                    var url = string.Format(option.Url, option.IP);
                    using var token = new CancellationTokenSource(option.RequestTimeout);
                    var result = await option.HttpClient.GetFromJsonAsync<T>(url, token.Token);
                    ret = result?.ToString();
                }
            }
            catch (Exception ex)
            {
                option.Logger?.LogError(ex, option.Url, option.IP);
            }
            return ret ?? string.Empty;
        }
    }
}
