// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
