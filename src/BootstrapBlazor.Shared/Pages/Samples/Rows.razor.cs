// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Rows
    {
        private RowFoo Model { get; } = new()
        {
            Name = "张三",
            Count = 23,
            Address = "测试地址",
            DateTime = new DateTime(1997, 12, 05),
            Educations = new List<EnumEducation> { EnumEducation.Middel }
        };

        private List<string> testvalue { get; set; } = new List<string>();
        private readonly List<EnumEducation> Educations = new List<EnumEducation> { EnumEducation.Middel, EnumEducation.Primary };
        private List<SelectedItem> Items = new List<SelectedItem>();


        private void test(EventArgs e)
        {
            Items = new List<SelectedItem>
        {
           new SelectedItem
           {
               Text = "aaa",
               Value = "bbb"
           },
                      new SelectedItem
           {
               Text = "ccc",
               Value = "ddd"
           }

        };
            Model.Address = "aaaaa";
        }

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "ItemsPerRow",
                Description = "设置一行显示几个控件",
                Type = "enum",
                ValueList = " One,Two,Three,Four,Six,Twelve ",
                DefaultValue = " One "
            },
            new AttributeItem() {
                Name = "RowType",
                Description = "设置排版格式，子Row如果不指定，会使用父Row的设置",
                Type = "enum?",
                ValueList = "Normal, FormInline,FormRow",
                DefaultValue = "null"
            },
            new AttributeItem() {
                Name = "ColSpan",
                Description = "设置子Row跨父Row列数",
                Type = "int?",
                ValueList = "-",
                DefaultValue = "null"
            },
            new AttributeItem() {
                Name = "MaxCount",
                Description = "设置行内最多显示的控件数",
                Type = "int?",
                ValueList = "-",
                DefaultValue = "null"
            }
        };

        private class RowFoo : Foo
        {
            /// <summary>
            /// 
            /// </summary>
            [Required(ErrorMessage = "请选择学历")]
            [Display(Name = "学历")]
            [AutoGenerateColumn(Order = 60)]
            public List<EnumEducation>? Educations { get; set; }
        }
    }
}
