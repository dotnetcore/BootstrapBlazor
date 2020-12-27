// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
