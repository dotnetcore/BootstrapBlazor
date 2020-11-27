// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
        public void TryParse_Ok()
        {
            var exp = Expression.Parameter(typeof(int?));
            var pi = typeof(int?).GetProperty("HasValue");
            var exp_p = Expression.Property(exp, pi);

            var func = Expression.Lambda<Func<int?, bool>>(exp_p, exp).Compile();
            var b = func.Invoke(10);
        }

        [Fact]
        public void NullContains_Ok()
        {
            var dummy = new Dummy();
            var filter = new FilterKeyValueAction
            {
                FieldKey = "Foo",
                FilterAction = FilterAction.Contains,
                FieldValue = ""
            };
            var invoker = filter.GetFilterLambda<Dummy>().Compile();
            var ret = invoker.Invoke(dummy);
            Assert.False(ret);
        }

        public class Dummy
        {
            public string Foo { get; set; }
        }
    }
}
