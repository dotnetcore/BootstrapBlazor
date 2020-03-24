using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Utils
{
    /// <summary>
    /// Css 生成操作类
    /// </summary>
    public class CssBuilder
    {
        private readonly List<string> stringBuffer;

        /// <summary>
        /// Creates a CssBuilder used to define conditional CSS classes used in a component.
        /// Call Build() to return the completed CSS Classes as a string.
        /// </summary>
        /// <param name="value"></param>
        public static CssBuilder Default(string value = "") => new CssBuilder(value);

        /// <summary>
        /// Creates a CssBuilder used to define conditional CSS classes used in a component.
        /// Call Build() to return the completed CSS Classes as a string.
        /// </summary>
        /// <param name="value"></param>
        protected CssBuilder(string value)
        {
            stringBuffer = new List<string>();
            AddClass(value);
        }

        /// <summary>
        /// Adds a raw string to the builder that will be concatenated with the next class or value added to the builder.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value)
        {
            if (!string.IsNullOrEmpty(value)) stringBuffer.Add(value);
            return this;
        }

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value, bool when = true) => when ? AddClass(value) : this;

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value, Func<bool> when) => AddClass(value, when());

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">Function that returns a CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(Func<string> value, bool when = true) => when ? AddClass(value()) : this;

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">Function that returns a CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(Func<string> value, Func<bool> when) => AddClass(value, when());

        /// <summary>
        /// Adds a conditional nested CssBuilder to the builder with space separator.
        /// </summary>
        /// <param name="builder">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(CssBuilder builder, bool when = true) => when ? AddClass(builder.Build()) : this;

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="builder">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(CssBuilder builder, Func<bool> when) => AddClass(builder, when());

        /// <summary>
        /// Adds a conditional CSS Class when it exists in a dictionary to the builder with space separator.
        /// Null safe operation.
        /// </summary>
        /// <param name="additionalAttributes">Additional Attribute splat parameters</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClassFromAttributes(IDictionary<string, object>? additionalAttributes)
        {
            if (additionalAttributes != null && additionalAttributes.TryGetValue("class", out var c))
            {
                var classList = c.ToString() ?? "";
                AddClass(classList);
            }
            return this;
        }

        /// <summary>
        /// Finalize the completed CSS Classes as a string.
        /// </summary>
        /// <returns>string</returns>
        public string Build() => stringBuffer.Any() ? string.Join(" ", stringBuffer) : "";

        /// <summary>
        /// ToString should only and always call Build to finalize the rendered string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Build();
    }
}
