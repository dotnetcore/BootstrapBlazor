//using System;
//using Microsoft.AspNetCore.Mvc.Localization;
//using Microsoft.Extensions.Localization;

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
