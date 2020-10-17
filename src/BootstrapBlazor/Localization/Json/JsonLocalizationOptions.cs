using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// LocalizationOptions 配置类
    /// </summary>
    public class JsonLocalizationOptions : LocalizationOptions
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public JsonLocalizationOptions()
        {
            ResourcesPath = "Resources";
        }

        /// <summary>
        /// 获得/设置 资源定位方式 默认为 TypeBased 基于类型定位 TypeFullName.CultureName.json 格式
        /// </summary>
        public ResourcesType ResourcesType { get; set; } = ResourcesType.TypeBased;
    }
}
