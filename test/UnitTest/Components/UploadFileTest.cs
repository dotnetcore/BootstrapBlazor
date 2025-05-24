// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace UnitTest.Components;

public class UploadFileTest
{
    [Fact]
    public async Task RequestBase64ImageFileAsync_Ok()
    {
        UploadFile uploadFile = new()
        {
            File = new MockBrowserFile("UploadTestFile", "image/png")
        };
        await uploadFile.RequestBase64ImageFileAsync(format: "image/png", maxWidth: 320, maxHeight: 240, maxAllowedSize: 512000, token: default);
    }

    private class MockBrowserFile(string name = "UploadTestFile", string contentType = "text") : IBrowserFile
    {
        public string Name { get; } = name;

        public DateTimeOffset LastModified { get; } = DateTimeOffset.Now;

        public long Size { get; } = 10;

        public string ContentType { get; } = contentType;

        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
        {
            return new MemoryStream([0x01, 0x02]);
        }
    }
}
