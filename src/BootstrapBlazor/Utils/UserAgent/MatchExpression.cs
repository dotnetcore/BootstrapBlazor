// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BootstrapBlazor.Components
{
    /// <summary>
    ///
    /// </summary>
    internal class MatchExpression
    {
        /// <summary>
        ///
        /// </summary>
        public List<Regex> Regexes { get; set; } = new List<Regex>();

        /// <summary>
        ///
        /// </summary>
        public Action<System.Text.RegularExpressions.Match, object> Action { get; set; } = (_, _) => { };
    }
}
