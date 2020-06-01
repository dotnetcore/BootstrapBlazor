using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// MenuItem 组件
    /// </summary>
    public class MenuItem
    {
        private readonly List<MenuItem> _items = new List<MenuItem>();

        /// <summary>
        /// 
        /// </summary>
        internal MenuItem? Parent { get; set; }

        /// <summary>
        /// 获得/设置 组件数据源
        /// </summary>
        public IEnumerable<MenuItem> Items => _items;

        /// <summary>
        /// 获得/设置 导航菜单文本内容
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 导航菜单链接地址
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 获得/设置 是否激活
        /// </summary>
        /// <value></value>
        public bool IsActive { get; set; }

        /// <summary>
        /// 获得/设置 图标字符串
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 添加 Menutem 方法 由 MenuItem 方法加载时调用
        /// </summary>
        /// <param name="item">Menutem 实例</param>
        public void AddItem(MenuItem item)
        {
            item.Parent = this;
            _items.Add(item);
        }
    }
}
