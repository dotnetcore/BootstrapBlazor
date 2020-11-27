// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

//namespace BootstrapBlazor.Localization.Json
//{
//    public class JsonHtmlLocalizerFactory : IHtmlLocalizerFactory
//    {
//        private readonly IStringLocalizerFactory _stringLocalizerFactory;

//        public JsonHtmlLocalizerFactory(IStringLocalizerFactory stringLocalizerFactory)
//        {
//            _stringLocalizerFactory = stringLocalizerFactory;
//        }

//        public IHtmlLocalizer Create(Type resourceSource)
//            => new JsonHtmlLocalizer(_stringLocalizerFactory.Create(resourceSource));

//        public IHtmlLocalizer Create(string baseName, string location)
//        {
//            var index = 0;
//            if (baseName.StartsWith(location, StringComparison.OrdinalIgnoreCase))
//            {
//                index = location.Length;
//            }

//            if (baseName.Length > index && baseName[index] == '.')
//            {
//                index += 1;
//            }

//            var relativeName = baseName.Substring(index);

//            return new JsonHtmlLocalizer(_stringLocalizerFactory.Create(baseName, location));
//        }
//    }
//}
