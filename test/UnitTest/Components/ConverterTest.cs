// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;
using Xunit;

namespace UnitTest.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class ConverterTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void NullableBool_Test()
        {
            Assert.True(BindConverter.TryConvertTo<SortOrder?>("Desc", CultureInfo.CurrentUICulture, out var _));
            Assert.True(BindConverter.TryConvertTo<SortOrder?>("2", CultureInfo.CurrentUICulture, out var _));
            Assert.Throws<InvalidCastException>(() => BindConverter.TryConvertTo<bool>("true", CultureInfo.InvariantCulture, out var b));
        }
    }
}
