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
