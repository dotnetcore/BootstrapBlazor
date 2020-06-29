using System.Linq;
using Xunit;

namespace UnitTest
{
    /// <summary>
    /// 
    /// </summary>
    public class LambdaTest
    {
        [Fact]
        public void Range_Ok()
        {
            int v = 10;
            var t = v.Subtract(1).Range("20", "30");
            Assert.Equal(20, t);
        }
    }
}
