// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    internal class UserAgent
    {
        private readonly string _userAgent;

        /// <summary>
        /// 
        /// </summary>
        public ClientBrowser Browser { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public ClientOS OS { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAgent"></param>
        public UserAgent(string userAgent)
        {
            _userAgent = userAgent;
            Browser = new ClientBrowser(_userAgent);
            OS = new ClientOS(_userAgent);
        }
    }
}
