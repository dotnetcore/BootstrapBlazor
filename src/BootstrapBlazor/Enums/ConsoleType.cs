// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// JS Console Enum
/// </summary>
public enum ConsoleType
{
    /// <summary>
    /// console.log
    /// </summary>
    [Description("log")] Log,

    /// <summary>
    /// console.warn
    /// </summary>
    [Description("warn")] Warn,

    /// <summary>
    /// console.error
    /// </summary>
    [Description("error")] Error,

    /// <summary>
    /// console.info
    /// </summary>
    [Description("info")] Info,

    /// <summary>
    /// console.assert
    /// </summary>
    [Description("assert")] Assert,

    /// <summary>
    /// console.dir
    /// </summary>
    [Description("dir")] Dir,

    /// <summary>
    /// console.time
    /// </summary>
    [Description("time")] Time,

    /// <summary>
    /// console.timeEnd
    /// </summary>
    [Description("timeEnd")] TimeEnd,

    /// <summary>
    /// console.count
    /// </summary>
    [Description("count")] Count,

    /// <summary>
    /// console.group
    /// </summary>
    [Description("group")] Group,

    /// <summary>
    /// console.count
    /// </summary>
    [Description("groupEnd")] GroupEnd,

    /// <summary>
    /// console.table
    /// </summary>
    [Description("table")] Table,

    /// <summary>
    /// console.trace
    /// </summary>
    [Description("trace")] Trace,
}
