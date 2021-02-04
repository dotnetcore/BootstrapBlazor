// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class StructTest
    {
        [Fact]
        public void KeyValue_Test()
        {
            var cache = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("1", 12)
            };
            var c = cache.FirstOrDefault(i => i.Key == "12");
            Assert.Null(c.Key);
        }

        [Fact]
        public void Struct_Test()
        {
            var cache = new List<(string, object)>
            {
                ("1", 12)
            };
            var c = cache.FirstOrDefault(i => i.Item1 == "12");
            Assert.Null(c.Item1);
        }
    }
}
