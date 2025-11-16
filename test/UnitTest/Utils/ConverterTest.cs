// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace UnitTest.Utils;

public class ConverterTest
{
    [Fact]
    public void NullableBool_Test()
    {
        Assert.True(BindConverter.TryConvertTo<SortOrder?>("Desc", CultureInfo.CurrentUICulture, out var _));
        Assert.True(BindConverter.TryConvertTo<SortOrder?>("2", CultureInfo.CurrentUICulture, out var _));
        Assert.Throws<InvalidCastException>(() => BindConverter.TryConvertTo<bool>("true", CultureInfo.InvariantCulture, out var b));
    }

    [Fact]
    public void ConvertTo_Test()
    {
        Assert.True("true".TryConvertTo<bool>(out var v1));
        Assert.True(v1);

        Assert.True("false".TryConvertTo<bool>(out var v2));
        Assert.False(v2);

        Assert.True(SortOrder.Asc.ToString().TryConvertTo<SortOrder>(out var v3));
        Assert.Equal(SortOrder.Asc, v3);

        var guid = Guid.NewGuid();
        Assert.True(guid.ToString().TryConvertTo<Guid>(out var v4));
        Assert.Equal(guid, v4);

        Assert.True("true".TryConvertTo(typeof(bool), out var v5));
        Assert.Equal(true, v5);

        Assert.True("false".TryConvertTo(typeof(bool), out var v6));
        Assert.Equal(false, v6);
    }

    [Fact]
    public void ChangedType_Ok()
    {
        var v = Convert.ChangeType("1", typeof(int));
        Assert.Equal(1, v);
    }
}
