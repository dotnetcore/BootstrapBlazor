// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class PinyinServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetFirstLetters_Ok()
    {
        var service = Context.Services.GetRequiredService<IPinyinService>();
        var result = service.GetFirstLetters("重庆市");

        Assert.Equal(["CQS", "ZQS"], result);

        result = service.GetFirstLetters("重庆市", PinyinLetterCaseCategory.LowercaseLetter);
        Assert.Equal(["cqs", "zqs"], result);
    }

    [Fact]
    public async Task GetPinyin_Ok()
    {
        var service = Context.Services.GetRequiredService<IPinyinService>();
        var result = service.GetPinyin("重庆市");
        Assert.Equal(["CHONG QING SHI", "ZHONG QING SHI"], result);
    }

    [Fact]
    public async Task IsChinese_Ok()
    {
        var service = Context.Services.GetRequiredService<IPinyinService>();
        var result = service.IsChinese('重');
        Assert.True(result);

        result = service.IsChinese('a');
        Assert.False(result);
    }

    [Fact]
    public async Task ContainsChinese_Ok()
    {
        var service = Context.Services.GetRequiredService<IPinyinService>();
        var result = service.ContainsChinese("abc重def");
        Assert.True(result);

        result = service.ContainsChinese("abc");
        Assert.False(result);
    }
}
