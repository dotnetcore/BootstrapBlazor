// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class TcpSocketPropertyConverterTest
{
    [Fact]
    public void UInt16Converter_Ok()
    {
        var converter = new SocketDataUInt16LittleEndianConverter();
        var actual = converter.Convert(new byte[] { 0xFF, 0x00 });
        Assert.Equal((ushort)0xFF, actual);
    }

    [Fact]
    public void Int16Converter_Ok()
    {
        var converter = new SocketDataInt16LittleEndianConverter();
        var actual = converter.Convert(new byte[] { 0xFF, 0x00 });
        Assert.Equal((short)0xFF, actual);
    }

    [Fact]
    public void UInt32Converter_Ok()
    {
        var converter = new SocketDataUInt32LittleEndianConverter();
        var actual = converter.Convert(new byte[] { 0xFF, 0x00, 0x00, 0x00 });
        Assert.Equal((uint)0xFF, actual);
    }

    [Fact]
    public void Int32Converter_Ok()
    {
        var converter = new SocketDataInt32LittleEndianConverter();
        var actual = converter.Convert(new byte[] { 0xFF, 0x00, 0x00, 0x00 });
        Assert.Equal(0xFF, actual);
    }

    [Fact]
    public void UInt64Converter_Ok()
    {
        var converter = new SocketDataUInt64LittleEndianConverter();
        var actual = converter.Convert(new byte[] { 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
        Assert.Equal((ulong)0xFF, actual);
    }

    [Fact]
    public void Int64Converter_Ok()
    {
        var converter = new SocketDataInt64LittleEndianConverter();
        var actual = converter.Convert(new byte[] { 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
        Assert.Equal((long)0xFF, actual);
    }

    [Fact]
    public void SingleConverter_Ok()
    {
        var converter = new SocketDataSingleLittleEndianConverter();
        var actual = converter.Convert(new byte[] { 0xC3, 0xF5, 0x48, 0x40 });
        Assert.Equal((float)3.14, actual);
    }

    [Fact]
    public void DoubleConverter_Ok()
    {
        var converter = new SocketDataDoubleLittleEndianConverter();
        var actual = converter.Convert(new byte[] { 0x1F, 0x85, 0xEB, 0x51, 0xB8, 0x1E, 0x09, 0x40 });
        Assert.Equal((double)3.14, actual);
    }
}
