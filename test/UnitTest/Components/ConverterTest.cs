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
            Assert.Throws<InvalidCastException>(() => BindConverter.TryConvertTo<bool>("true", CultureInfo.InvariantCulture, out var b));
        }
    }
}
