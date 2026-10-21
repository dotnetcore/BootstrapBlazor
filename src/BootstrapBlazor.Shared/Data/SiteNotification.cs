// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// Public notification data, excluding webhook credentials and author contact details.
/// </summary>
/// <param name="Kind">The notification kind.</param>
/// <param name="Message">The message or commit description.</param>
/// <param name="IsUpdating">Whether the website is updating.</param>
/// <param name="Author">The commit author's display name.</param>
/// <param name="Branch">The branch name.</param>
/// <param name="Url">An HTTP or HTTPS commit link.</param>
/// <param name="Timestamp">The commit timestamp.</param>
/// <param name="TotalCount">The number of commits.</param>
public sealed record SiteNotification(
    string Kind,
    string? Message = null,
    bool IsUpdating = false,
    string? Author = null,
    string? Branch = null,
    string? Url = null,
    string? Timestamp = null,
    int TotalCount = 0);
