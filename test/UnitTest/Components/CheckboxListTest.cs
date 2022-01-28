// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class CheckboxListTest : BootstrapBlazorTestBase
{
    IEnumerable<SelectedItem>? Items1 { get; set; } = new List<SelectedItem>(new List<SelectedItem> {
                new SelectedItem { Text = "Item 1", Value = "1" },
                new SelectedItem { Text = "Item 2", Value = "2" },
                new SelectedItem { Text = "Item 3", Value = "3" },
                new SelectedItem { Text = "Item 4", Value = "4" }
     });

    string Value1 { get; set; } = "1,3";
    IEnumerable<int>? Value2 { get; set; } = new int[] { 3, 4 };

    [Fact]
    public void Components_Ok()
    {

        var cut = Context.RenderComponent<CheckboxList<string>>(builder => {
            builder.Add(a => a.DisplayText, "长沙");
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.Value, Value1);
            builder.Add(a => a.Items, Items1);
            }
        );
        Assert.Contains("class=\"form-control checkbox-list\"", cut.Markup);
    }
    
    [Fact]
    public void Items_Ok()
    {

        var cut = Context.RenderComponent<CheckboxList<string>>(builder => {
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.Items, Items1);
            }
        );
        Assert.Contains("Item 3", cut.Markup);
    }
    
    [Fact]
    public void Value_String_Ok()
    {

        var cut = Context.RenderComponent<CheckboxList<string>>(builder => {
            builder.Add(a => a.Value, Value1);
            builder.Add(a => a.Items, Items1);
            }
        );
        Assert.Contains("form-check is-checked", cut.Markup);
        Assert.Contains("Item 3", cut.Markup);
    }

    //[Fact]
    //public void Value_IEnumerable_Ok()
    //{

    //    var cut = Context.RenderComponent<CheckboxList<string>>(builder => {
    //        builder.Add(a => a.Value, Value2);
    //        builder.Add(a => a.Items, Items1);
    //        }
    //    );
    //    Assert.Contains("form-check is-checked", cut.Markup);
    //    Assert.Contains("Item 4", cut.Markup);
    //}

    private RenderFragment GenerateCheckboxList() => builder =>
    {
        builder.OpenComponent(0, typeof(CheckboxList<string>));
        builder.AddAttribute(1, nameof(CheckboxList<string>.DisplayText), "长沙市");
        builder.AddAttribute(2, nameof(CheckboxList<string>.ShowLabel), true);
        builder.AddAttribute(2, nameof(CheckboxList<string>.Value), Value1);
        builder.AddAttribute(7, nameof(CheckboxList<string>.Items), Items1);
        builder.CloseComponent();
    };

    [Fact]
    public void Generate_Ok()
    {
        var cut = Context.Render(GenerateCheckboxList());
         
        Assert.Contains("form-check is-checked", cut.Markup);
        Assert.Contains("Item 3", cut.Markup);
    }

}
