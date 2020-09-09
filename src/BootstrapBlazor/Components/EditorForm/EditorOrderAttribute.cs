using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorOrderAttribute : Attribute
    {
        /// <summary>
        /// 获得 Order 属性
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public EditorOrderAttribute(int order)
        {
            Order = order;
        }
    }
}
