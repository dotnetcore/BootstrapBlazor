// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UnitTest.Components;

public class EditorFormTest : BootstrapBlazorTestBase
{
    [Fact]
    public void CascadedEditContext_Error()
    {
        var foo = new Foo();
        Assert.Throws<InvalidCastException>(() => Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<EditorForm<Dummy>>(pb =>
            {
                pb.Add(a => a.Model, new Dummy());
            });
        }));
    }

    [Fact]
    public void CanWrite_Ok()
    {
        var foo = new Dummy();
        var cut = Context.RenderComponent<EditorForm<Dummy>>(pb =>
        {
            pb.Add(a => a.Model, foo);
        });

        // 只读属性渲染成 Display 组件
        cut.Contains("form-control is-display");
    }

    [Fact]
    public void CascadedEditContext_Ok()
    {
        var foo = new Foo();
        Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<EditorForm<Foo>>();
        });
    }

    [Fact]
    public void Model_Error()
    {
        Assert.ThrowsAny<ArgumentNullException>(() =>
        {
            Context.RenderComponent<EditorForm<Foo>>(pb =>
            {
                pb.Add(a => a.Model, null);
            });
        });
    }

    [Fact]
    public void Items_Ok()
    {
        var foo = new Foo();
        Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.Items, new List<InternalTableColumn>
            {
                new("Id", typeof(int)),
                new("Name", typeof(string))
            });
        });
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AutoGenerateAllItem_True(bool autoGenerate)
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.AutoGenerateAllItem, autoGenerate);
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.FieldItems, GenerateEditorItems(foo));
        });
    }

    [Fact]
    public void IsDisplay_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.IsDisplay, true);
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.FieldItems, GenerateEditorItems(foo));
        });
    }

    [Fact]
    public void Textarea_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.IsDisplay, true);
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, CreateTextAreaItem());
        });

        RenderFragment<Foo> CreateTextAreaItem() => f => builder =>
        {
            builder.OpenComponent<EditorItem<Foo, string>>(0);
            builder.AddAttribute(1, nameof(EditorItem<Foo, string>.Field), f.Name);
            builder.AddAttribute(2, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
            builder.AddAttribute(3, nameof(EditorItem<Foo, string>.ComponentType), typeof(Textarea));
            builder.AddAttribute(4, nameof(EditorItem<Foo, string>.Rows), 0);
            builder.CloseComponent();

            builder.OpenComponent<EditorItem<Foo, string>>(0);
            builder.AddAttribute(1, nameof(EditorItem<Foo, string>.Field), f.Address);
            builder.AddAttribute(2, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Address), typeof(string)));
            builder.AddAttribute(3, nameof(EditorItem<Foo, string>.ComponentType), typeof(Textarea));
            builder.AddAttribute(4, nameof(EditorItem<Foo, string>.Rows), 3);
            builder.CloseComponent();
        };
    }

    [Fact]
    public void IsSearch_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.AddCascadingValue("IsSearch", true);
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.FieldItems, GenerateEditorItems(foo));
        });
    }

    [Fact]
    public void Buttons_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.Buttons, builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.CloseComponent();
            });
        });
    }

    [Fact]
    public void Alignment_Right()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.ItemsPerRow, 1);
            pb.Add(a => a.ItemChangedType, ItemChangedType.Add);
            pb.Add(a => a.RowType, RowType.Inline);
            pb.Add(a => a.LabelAlign, Alignment.Right);
            pb.Add(a => a.LabelWidth, 80);
        });
        cut.Contains("row g-3 form-inline form-inline-end");
        cut.Contains("col-12");
        cut.Contains("--bb-row-label-width: 80px;");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.LabelAlign, Alignment.Center);
        });
        cut.Contains("row g-3 form-inline form-inline-center");
    }

    [Fact]
    public void FieldChanged_Ok()
    {
        var cut = Context.RenderComponent<MockEditorItem>(pb =>
        {
            pb.Add(a => a.Field, "Nama");
            pb.Add(a => a.FieldChanged, EventCallback.Factory.Create<string>(this, v => { }));
        });
        cut.InvokeAsync(() => cut.Instance.Test());
    }

    [Fact]
    public void DisplayName_Ok()
    {
        var cut = Context.RenderComponent<EditorItem<Foo, string>>();
        Assert.Equal("", cut.Instance.GetDisplayName());
        Assert.Equal("", cut.Instance.GetFieldName());
    }

    [Fact]
    public void EditTemplate_Coverage()
    {
        var editorItem = new EditorItem<Foo, string>();
        IEditorItem item = editorItem;
        item.EditTemplate = null;
        Assert.Null(editorItem.Field);
    }

    [Fact]
    public void EditorItem_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<EditorForm<Foo>>(pb =>
            {
                pb.Add(a => a.AutoGenerateAllItem, false);
                pb.Add(a => a.FieldItems, f => builder =>
                {
                    var index = 0;
                    builder.OpenComponent<EditorItem<Foo, string>>(index++);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Name);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Readonly), true);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.SkipValidate), false);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Text), "Test-Text");
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.ComponentType), typeof(BootstrapInput<string>));
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.ComponentParameters), new KeyValuePair<string, object>[]
                    {
                        new("type", "text")
                    });
                    builder.CloseComponent();

                    builder.OpenComponent<EditorItem<Foo, string>>(index++);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Address);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Address), typeof(string)));
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Rows), 3);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.ValidateRules), new List<IValidator>
                    {
                        new FormItemValidator(new RequiredAttribute())
                    });
                    builder.CloseComponent();

                    builder.OpenComponent<EditorItem<Foo, int>>(index++);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, int>.Field), f.Count);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, int>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Count), typeof(int)));
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, int>.Step), "3");
                    builder.CloseComponent();

                    builder.OpenComponent<EditorItem<Foo, bool>>(index++);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.Field), f.Complete);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Complete), typeof(bool)));
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.Items), new List<SelectedItem>
                    {
                        new("True", "Test-True"),
                        new("False", "Test-False")
                    });
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.Lookup), new List<SelectedItem>
                    {
                        new("True", "Test-True"),
                        new("False", "Test-False")
                    });
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.ShowSearchWhenSelect), false);
                    builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.IsPopover), false);
                    builder.CloseComponent();
                });
            });
        });
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShowLabelTooltip_Ok(bool showTooltip)
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.ShowLabelTooltip, showTooltip);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, f => builder =>
            {
                var index = 0;
                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Name);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Readonly), true);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Text), "Test-Text");
                builder.CloseComponent();
            });
        });
        var display = cut.FindComponent<Display<string>>();
        Assert.Equal(showTooltip, display.Instance.ShowLabelTooltip);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void EditorItem_ShowLabelTooltip_Ok(bool showTooltip)
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, f => builder =>
            {
                var index = 0;
                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Name);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Readonly), true);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Text), "Test-Text");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.ShowLabelTooltip), showTooltip);
                builder.CloseComponent();
            });
        });
        var display = cut.FindComponent<Display<string>>();
        Assert.Equal(showTooltip, display.Instance.ShowLabelTooltip);
    }

    [Fact]
    public void EditorItem_Editable_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, f => builder =>
            {
                var index = 0;
                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Name);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                builder.CloseComponent();

                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Address);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Address), typeof(string)));
                builder.CloseComponent();
            });
        });
        cut.Contains("地址");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.FieldItems, f => builder =>
            {
                var index = 0;
                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Name);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                builder.CloseComponent();

                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Address);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Address), typeof(string)));
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Ignore), true);
                builder.CloseComponent();
            });
        });
        cut.DoesNotContain("地址");
    }

    [Fact]
    public void Order_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, f => builder =>
            {
                var index = 0;
                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Name);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Text), "Test-Text");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Order), 1);
                builder.CloseComponent();
            });
        });
        var item = cut.FindComponent<EditorItem<Foo, string>>();
        Assert.Equal(1, item.Instance.Order);
    }

    [Fact]
    public void ColumnOrderCallback_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.AutoGenerateAllItem, true);
            pb.Add(a => a.ColumnOrderCallback, cols =>
            {
                return cols.OrderByDescending(i => i.Order);
            });
        });
        var editor = cut.Instance;
        var itemsField = editor.GetType().GetField("_formItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField);
        Assert.NotNull(itemsField);

        var v = itemsField.GetValue(editor) as List<IEditorItem>;
        Assert.NotNull(v);
        Assert.Equal(new List<int>() { 70, 60, 50, 40, 20, 10, 1 }, v.Select(i => i.Order));
    }

    [Fact]
    public void LookupServiceKey_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, f => builder =>
            {
                var index = 0;
                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Name);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Text), "Test-Text");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.LookupServiceKey), "FooLookup");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.LookupServiceData), true);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.LookupStringComparison), StringComparison.OrdinalIgnoreCase);
                builder.CloseComponent();
            });
        });
        var select = cut.FindComponent<Select<string>>();
        var lookupService = Context.Services.GetRequiredService<ILookupService>();
        var lookup = lookupService.GetItemsByKey("FooLookup");
        Assert.Equal(lookup!.Count(), select.Instance.Items.Count());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GroupName_Order_Ok(bool showUnsetGroupItemsOnTop)
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<EditorForm<Foo>>(pb =>
        {
            pb.AddCascadingValue("IsSearch", true);
            pb.Add(a => a.ShowUnsetGroupItemsOnTop, showUnsetGroupItemsOnTop);
            pb.Add(a => a.Model, foo);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, f => builder =>
            {
                var index = 0;
                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Name);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Text), "Test-Text");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Order), 1);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.GroupName), "Test-Group-1");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.GroupOrder), 1);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.EditTemplate), new RenderFragment<Foo>(foo => builder => builder.AddContent(0, "Test")));
                builder.CloseComponent();

                builder.OpenComponent<EditorItem<Foo, string>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Field), f.Address);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Address), typeof(string)));
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Text), "Test-Address");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.Order), 1);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.GroupName), "Test-Group-2");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, string>.GroupOrder), 2);
                builder.CloseComponent();

                builder.OpenComponent<EditorItem<Foo, bool>>(index++);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.Field), f.Complete);
                builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Complete), typeof(bool)));
                builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.Text), "Test-Complete");
                builder.AddAttribute(index++, nameof(EditorItem<Foo, bool>.Order), 1);
                builder.CloseComponent();
            });
        });
    }

    private static RenderFragment<Foo> GenerateEditorItems(Foo foo) => f => builder =>
    {
        builder.OpenComponent<EditorItem<Foo, string>>(0);
        builder.AddAttribute(1, nameof(EditorItem<Foo, string>.Field), f.Name);
        builder.AddAttribute(2, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Name), typeof(string)));
        builder.AddAttribute(3, nameof(EditorItem<Foo, string>.EditTemplate), new RenderFragment<Foo>(f => b =>
        {
            b.OpenComponent<Button>(0);
            b.CloseComponent();
        }));
        builder.CloseComponent();

        builder.OpenComponent<EditorItem<Foo, string>>(0);
        builder.AddAttribute(1, nameof(EditorItem<Foo, string>.Field), f.Address);
        builder.AddAttribute(2, nameof(EditorItem<Foo, string>.FieldExpression), Utility.GenerateValueExpression(foo, nameof(Foo.Address), typeof(string)));
        builder.AddAttribute(3, nameof(EditorItem<Foo, string>.Ignore), true);
        builder.CloseComponent();
    };

    [Fact]
    public void CheckboxList_Manual()
    {
        var dummy = new Dummy();
        var cut = Context.RenderComponent<EditorForm<Dummy>>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, new RenderFragment<Dummy>(dummy => builder =>
            {
                builder.OpenComponent<EditorItem<Dummy, List<string>>>(0);
                builder.AddAttribute(1, nameof(EditorItem<Dummy, List<string>>.Field), dummy.Names);
                builder.AddAttribute(2, nameof(EditorItem<Dummy, List<string>>.FieldExpression), Utility.GenerateValueExpression(dummy, nameof(Dummy.Names), typeof(List<string>)));
                builder.AddAttribute(2, nameof(EditorItem<Dummy, List<string>>.Items), new List<SelectedItem>()
                {
                    new("1", "Test1"),
                    new("2", "Test2")
                });
                builder.CloseComponent();
            }));
        });
    }

    [Fact]
    public void CheckboxList_Auto()
    {
        var dummy = new Dummy();
        var cut = Context.RenderComponent<EditorForm<Dummy>>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.Add(a => a.FieldItems, new RenderFragment<Dummy>(dummy => builder =>
            {
                builder.OpenComponent<EditorItem<Dummy, List<string>>>(0);
                builder.AddAttribute(1, nameof(EditorItem<Dummy, List<string>>.Field), dummy.Names);
                builder.AddAttribute(2, nameof(EditorItem<Dummy, List<string>>.FieldExpression), Utility.GenerateValueExpression(dummy, nameof(Dummy.Names), typeof(List<string>)));
                builder.AddAttribute(3, nameof(EditorItem<Dummy, List<string>>.Items), new List<SelectedItem>()
                {
                    new("1", "Test1"),
                    new("2", "Test2")
                });
                builder.CloseComponent();
            }));
        });
        Assert.Contains("checkbox-list form-control", cut.Markup);
    }

    [Fact]
    public void Select_Ok()
    {
        var dummy = new Dummy();
        var cut = Context.RenderComponent<EditorForm<Dummy>>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.Add(a => a.FieldItems, new RenderFragment<Dummy>(dummy => builder =>
            {
                builder.OpenComponent<EditorItem<Dummy, string>>(0);
                builder.AddAttribute(1, nameof(EditorItem<Dummy, string>.Field), dummy.Select);
                builder.AddAttribute(2, nameof(EditorItem<Dummy, string>.FieldExpression), Utility.GenerateValueExpression(dummy, nameof(Dummy.Select), typeof(string)));
                builder.AddAttribute(3, nameof(EditorItem<Dummy, List<string>>.Items), new List<SelectedItem>()
                {
                    new("1", "Test1"),
                    new("2", "Test2")
                });
                builder.CloseComponent();
            }));
        });
        Assert.Contains("select dropdown", cut.Markup);
    }

    [Fact]
    public void Select_NullableBool_Items()
    {
        var dummy = new Dummy();
        var cut = Context.RenderComponent<EditorForm<Dummy>>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, new RenderFragment<Dummy>(dummy => builder =>
            {
                builder.OpenComponent<EditorItem<Dummy, bool?>>(0);
                builder.AddAttribute(1, nameof(EditorItem<Dummy, bool?>.Field), true);
                builder.AddAttribute(2, nameof(EditorItem<Dummy, bool?>.FieldExpression), Utility.GenerateValueExpression(dummy, nameof(Dummy.Test2), typeof(bool?)));
                builder.AddAttribute(3, nameof(EditorItem<Dummy, bool?>.ComponentType), typeof(Select<bool?>));
                builder.CloseComponent();
            }));
        });
        Assert.Contains("select dropdown", cut.Markup);
        Assert.Contains("test-null", cut.Markup);
        Assert.Contains("test-true", cut.Markup);
        Assert.Contains("test-false", cut.Markup);
    }

    [Fact]
    public void Select_NullableBool_Ok()
    {
        var dummy = new Dummy();
        var cut = Context.RenderComponent<EditorForm<Dummy>>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.Add(a => a.AutoGenerateAllItem, false);
            pb.Add(a => a.FieldItems, new RenderFragment<Dummy>(dummy => builder =>
            {
                builder.OpenComponent<EditorItem<Dummy, bool?>>(0);
                builder.AddAttribute(1, nameof(EditorItem<Dummy, bool?>.Field), true);
                builder.AddAttribute(2, nameof(EditorItem<Dummy, bool?>.FieldExpression), Utility.GenerateValueExpression(dummy, nameof(Dummy.Test3), typeof(bool?)));
                builder.CloseComponent();
            }));
        });
        Assert.Contains("class=\"switch\"", cut.Markup);
    }

    [Fact]
    public void Cols_Ok()
    {
        var dummy = new Dummy();
        var cut = Context.RenderComponent<EditorForm<Dummy>>(pb =>
        {
            pb.Add(a => a.Model, dummy);
            pb.Add(a => a.AutoGenerateAllItem, true);
        });
        Assert.Contains("col-12 col-sm-12", cut.Markup);
    }

    private class Dummy
    {
        public string? Name { get; }

        [DefaultValue(false)]
        [AutoGenerateColumn(ComponentType = typeof(NullSwitch))]
        public bool? Test { get; set; }

        [DefaultValue("1")]
        [AutoGenerateColumn(ComponentType = typeof(NullSwitch))]
        [NullableBoolItems(FalseValueDisplayText = "test-false", NullValueDisplayText = "test-null", TrueValueDisplayText = "test-true")]
        public bool? Test2 { get; set; }

        public bool? Test3 { get; set; }

        public List<string>? Names { get; set; }

        [AutoGenerateColumn(ComponentType = typeof(Select<string>), Cols = 12)]
        public string? Select { get; set; }
    }

    private class MockEditorItem : EditorItem<Foo, string>
    {
        public async Task Test()
        {
            if (FieldChanged.HasDelegate)
            {
                await FieldChanged.InvokeAsync();
            }
        }
    }
}
