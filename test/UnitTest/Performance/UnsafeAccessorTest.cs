// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Runtime.CompilerServices;

namespace UnitTest.Performance;

public class UnsafeAccessorTest
{
    [Fact]
    public void GetField_Ok()
    {
        var dummy = new Dummy();
        dummy.SetName("test");
        Assert.Equal("test", GetNameField(dummy));
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_name")]
    static extern ref string GetNameField(Dummy @this);

    private class Dummy
    {
        private string? _name;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string? GetName()
        {
            return _name;
        }

        public void SetName(string? name)
        {
            _name = name;
        }
    }
}
