// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;
using System.Text;

namespace UnitTest.Mock;

internal class MockJSStreamReference : IJSStreamReference
{
    public long Length { get; private set; }

    public ValueTask<Stream> OpenReadStreamAsync(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
    {
        Length = 4;
        var stream = new MemoryStream(Encoding.UTF8.GetBytes("Mock"));
        return ValueTask.FromResult(stream as Stream);
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}
