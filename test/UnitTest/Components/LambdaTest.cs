using BootstrapBlazor.Components;
using System;
using System.Linq;
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

        [Fact]
        public void NullContains_Ok()
        {
            var dummy = new Dummy();
            var filter = new FilterKeyValueAction();
            filter.FieldKey = "Foo";
            filter.FilterAction = FilterAction.Contains;
            filter.FieldValue = "";
            var invoker = filter.GetFilterLambda<Dummy>().Compile();
            var ret = invoker.Invoke(dummy);
            Assert.False(ret);
        }

        public class Dummy
        {
            public string? Foo { get; set; }
        }
    }
}
