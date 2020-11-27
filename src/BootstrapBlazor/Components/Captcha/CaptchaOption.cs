// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Captcha 滑块验证码组件
    /// </summary>
    public class CaptchaOption
    {
        /// <summary>
        /// 获得/设置 验证码图片宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 获得/设置 验证码图片高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 获得/设置 拼图边长
        /// </summary>
        public int SideLength { get; set; } = 42;

        /// <summary>
        /// 获得/设置 拼图直径
        /// </summary>
        public int Diameter { get; set; } = 9;

        /// <summary>
        /// 获得/设置 拼图 X 位置
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// 获得/设置 拼图 Y 位置
        /// </summary>
        public int OffsetY { get; set; }

        /// <summary>
        /// 获得/设置 拼图宽度
        /// </summary>
        public int BarWidth { get; set; }

        /// <summary>
        /// 获得/设置 拼图背景图片路径
        /// </summary>
        public string ImageUrl { get; set; } = "images/Pic0.jpg";
    }
}
