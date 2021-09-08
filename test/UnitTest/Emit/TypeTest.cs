// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Emit
{
    public class TypeTest
    {
        [Fact]
        public void CreateType_Ok()
        {
            var cols = new Foo[]
            {
                new("Id", typeof(int)),
                new("Name", typeof(string))
            };

            // 创建动态类型基类是 DynamicObject
            var instanceType = EmitHelper.CreateTypeByName("Test", cols, typeof(DynamicObject));
            Assert.Equal(typeof(DynamicObject), instanceType.BaseType);

            // 创建动态类型实例
            var instance = Activator.CreateInstance(instanceType);
            Assert.NotNull(instance);

            var properties = instance.GetType().GetProperties().Select(p => p.Name);
            Assert.True(cols.Select(c => c.FieldName).SequenceEqual(properties));
        }

        private class Foo : ITableColumn
        {
            public Foo(string fieldName, Type propertyType) => (FieldName, PropertyType) = (fieldName, propertyType);

            public string FieldName { get; }

            public Type PropertyType { get; }

            public bool Editable { get; set; }

            public bool Readonly { get; set; }

            public bool SkipValidate { get; set; }

            public string Text { get; set; }

            public IEnumerable<SelectedItem> Data { get; set; }

            public object Step { get; set; }

            public int Rows { get; set; }

            public RenderFragment<object> EditTemplate { get; set; }

            public System.Type ComponentType { get; set; }

            public IEnumerable<SelectedItem> Lookup { get; set; }

            public bool Sortable { get; set; }

            public bool DefaultSort { get; set; }

            public SortOrder DefaultSortOrder { get; set; }

            public bool Filterable { get; set; }

            public bool Searchable { get; set; }

            public int? Width { get; set; }

            public bool Fixed { get; set; }

            public bool Visible { get; set; }

            public bool AllowTextWrap { get; set; }

            public bool TextEllipsis { get; set; }

            public string CssClass { get; set; }

            public BreakPoint ShownWithBreakPoint { get; set; }

            public RenderFragment<object> Template { get; }

            public RenderFragment<object> SearchTemplate { get; set; }

            public RenderFragment FilterTemplate { get; set; }

            public IFilter Filter { get; set; }

            public string FormatString { get; set; }

            public Func<object, Task<string>> Formatter { get; set; }

            public Alignment Align { get; set; }

            public bool ShowTips { get; set; }

            public int Order { get; set; }

            public Action<TableCellArgs> OnCellRender { get; set; }

            public IEnumerable<KeyValuePair<string, object>> ComponentParameters { get; set; }

            public string GetDisplayName() => Text ?? FieldName;

            public string GetFieldName() => FieldName;
        }
    }
}
