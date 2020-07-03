using System;
using System.Linq.Expressions;
using Xunit;

namespace UnitTest.Components
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
        }

        [Fact]
        public void TryParse_Ok()
        {
            var exp = Expression.Parameter(typeof(int?));
            var pi = typeof(int?).GetProperty("HasValue");
            var exp_p = Expression.Property(exp, pi);

            var func = Expression.Lambda<Func<int?, bool>>(exp_p, exp).Compile();
            var b = func.Invoke((int)10);
        }
    }
}
