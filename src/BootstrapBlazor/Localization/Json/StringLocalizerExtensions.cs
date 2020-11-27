// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System;
using System.Linq.Expressions;

namespace Microsoft.Extensions.Localization
{
    /// <summary>
    /// IStringLocalizer 扩展方法
    /// </summary>
    public static class StringLocalizerExtensions
    {
        /// <summary>
        /// 通过 Lambda 表达式获取指定类型的语言文字信息
        /// </summary>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="stringLocalizer"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public static LocalizedString? GetString<TResource>(
            this IStringLocalizer stringLocalizer,
            Expression<Func<TResource, string>> propertyExpression)
        {
            var member = propertyExpression.Body as MemberExpression;
            if (member == null) throw new InvalidOperationException($"{nameof(propertyExpression)}'s Body property must be a instance of {nameof(MemberExpression)}");
            return stringLocalizer[member.Member.Name];
        }
    }
}
