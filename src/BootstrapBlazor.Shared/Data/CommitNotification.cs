// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// Public commit information displayed by dispatch subscribers.
/// </summary>
/// <param name="Author"></param>
/// <param name="Timestamp"></param>
/// <param name="Message"></param>
/// <param name="Url"></param>
/// <param name="Branch"></param>
/// <param name="TotalCount"></param>
public sealed record CommitNotification(string? Author, DateTimeOffset Timestamp, string? Message, string? Url, string Branch, int TotalCount);
