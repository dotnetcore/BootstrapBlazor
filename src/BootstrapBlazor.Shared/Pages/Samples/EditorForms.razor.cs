// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class EditorForms
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        private Foo Model { get; } = new Foo()
        {
            Name = "张三",
            Count = 23,
            Address = "测试地址",
            DateTime = new DateTime(1997, 12, 05),
            Education = EnumEducation.Middel
        };

        private Foo ValidateModel { get; } = new Foo()
        {
            Name = "张三",
            Count = 23,
            DateTime = new DateTime(1997, 12, 05),
            Education = EnumEducation.Middel
        };

        [NotNull]
        private IEnumerable<SelectedItem>? Hobbys { get; set; }

        private List<SelectedItem> DummyItems { get; } = new List<SelectedItem>()
        {
            new SelectedItem("1", "1"),
            new SelectedItem("2", "2"),
            new SelectedItem("3", "3"),
            new SelectedItem("4", "4"),
            new SelectedItem("5", "5")
        };

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Hobbys = Foo.GenerateHobbys(Localizer);
        }

        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Model",
                Description = "当前绑定数据模型",
                Type = "TModel",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FieldItems",
                Description = "绑定列模板",
                Type = "RenderFragment<TModel>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Buttons",
                Description = "按钮模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "是否显示 Label",
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "AutoGenerateAllItem",
                Description = "是否生成所有属性",
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ItemsPerRow",
                Description = "每行显示组件数量",
                Type = "int?",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RowType",
                Description = "设置组件布局方式",
                Type = "RowType",
                ValueList = "Row|Inline",
                DefaultValue = "Row"
            },
            new AttributeItem() {
                Name = "LabelAlign",
                Description = "Inline 布局模式下标签对齐方式",
                Type = "Alignment",
                ValueList = "None|Left|Center|Right",
                DefaultValue = "None"
            }
        };

        private static IEnumerable<AttributeItem> GetEditorItemAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Field",
                Description = "当前绑定数据值",
                Type = "TValue",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FieldType",
                Description = "绑定列数据类型",
                Type = "Type",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Editable",
                Description = "是否允许编辑",
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Readonly",
                Description = "是否只读",
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Text",
                Description = "编辑列前置标签名",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditTemplate",
                Description = "列编辑模板",
                Type = "RenderFragment<object>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
