// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Misc;

public class EditorItemTest
{
    [Fact]
    public void EditorItem_Ok()
    {
        var lookup = new List<SelectedItem> { new("1", "Test") };
        var items = new List<SelectedItem> { new("2", "Item") };
        var componentParameters = new List<KeyValuePair<string, object>> { new("Key", "Value") };
        var validateRules = new List<IValidator>();
        Action<TableCellArgs> onCellRender = _ => { };

        var item = new EditorItem<Foo>("Name", typeof(string), "Text")
        {
            Ignore = true,
            Readonly = true,
            Required = true,
            RequiredErrorMessage = "Error",
            ShowLabelTooltip = true,
            PlaceHolder = "PlaceHolder",
            SkipValidate = true,
            Step = "1",
            Rows = 3,
            Cols = 6,
            ComponentType = typeof(BootstrapInput<string>),
            ComponentParameters = componentParameters,
            Items = items,
            Order = 10,
            Lookup = lookup,
            ShowSearchWhenSelect = true,
            IsPopover = true,
            LookupStringComparison = StringComparison.Ordinal,
            LookupServiceKey = "test-key",
            LookupServiceData = "test-data",
            LookupService = null,
            OnCellRender = onCellRender,
            ValidateRules = validateRules,
            GroupName = "Group1",
            GroupOrder = 5
        };
        Assert.Equal("Name", item.GetFieldName());
        Assert.Equal(typeof(string), item.PropertyType);
        Assert.True(item.Ignore);
        Assert.True(item.Readonly);
        Assert.True(item.Required);
        Assert.Equal("Error", item.RequiredErrorMessage);
        Assert.True(item.ShowLabelTooltip);
        Assert.Equal("PlaceHolder", item.PlaceHolder);
        Assert.True(item.SkipValidate);
        Assert.Equal("1", item.Step);
        Assert.Equal(3, item.Rows);
        Assert.Equal(6, item.Cols);
        Assert.Equal("Text", item.Text);
        Assert.Equal("Text", item.GetDisplayName());
        Assert.Equal(typeof(BootstrapInput<string>), item.ComponentType);
        Assert.Same(componentParameters, item.ComponentParameters);
        Assert.Same(items, item.Items);
        Assert.Equal(10, item.Order);
        Assert.Same(lookup, item.Lookup);
        Assert.True(item.ShowSearchWhenSelect);
        Assert.True(item.IsPopover);
        Assert.Equal(StringComparison.Ordinal, item.LookupStringComparison);
        Assert.Equal("test-key", item.LookupServiceKey);
        Assert.Equal("test-data", item.LookupServiceData);
        Assert.Null(item.LookupService);
        Assert.Same(onCellRender, item.OnCellRender);
        Assert.Same(validateRules, item.ValidateRules);
        Assert.Equal("Group1", item.GroupName);
        Assert.Equal(5, item.GroupOrder);

        item.Text = "NewText";
        Assert.Equal("NewText", item.Text);
    }

    [Fact]
    public void EditorItem_DefaultValues()
    {
        var item = new EditorItem<Foo>("Name", typeof(string));
        Assert.False(item.SkipValidate);
        Assert.Null(item.Ignore);
        Assert.Null(item.Readonly);
        Assert.Null(item.Required);
        Assert.Null(item.RequiredErrorMessage);
        Assert.Null(item.ShowLabelTooltip);
        Assert.Null(item.PlaceHolder);
        Assert.Null(item.Step);
        Assert.Equal(0, item.Rows);
        Assert.Equal(0, item.Cols);
        Assert.Equal(0, item.Order);
        Assert.Equal(0, item.GroupOrder);
        Assert.Null(item.GroupName);
        Assert.Null(item.ComponentType);
        Assert.Null(item.ComponentParameters);
        Assert.Null(item.Items);
        Assert.Null(item.Lookup);
        Assert.False(item.ShowSearchWhenSelect);
        Assert.False(item.IsPopover);
        Assert.Equal(StringComparison.OrdinalIgnoreCase, item.LookupStringComparison);
        Assert.Null(item.LookupServiceKey);
        Assert.Null(item.LookupServiceData);
        Assert.Null(item.LookupService);
        Assert.Null(item.OnCellRender);
        Assert.Null(item.ValidateRules);
    }

    [Fact]
    public void EditTemplate_Ok()
    {
        var item = new EditorItem<Foo>("Name", typeof(string));
        IEditorItem editorItem = item;
        Assert.Null(item.EditTemplate);
        Assert.Null(editorItem.EditTemplate);

        item.EditTemplate = BootstrapDynamicComponent.CreateComponent<MockEditor>(new Dictionary<string, object?>()
        {
            { "Parameter1", "Test" }
        }).RenderEditTemplate<Foo>();
        Assert.NotNull(item.EditTemplate);
        Assert.NotNull(editorItem.EditTemplate);

        var context = new BunitContext();
        var cut = context.Render(pb =>
        {
            pb.AddContent(0, editorItem.EditTemplate.Invoke(new Foo { Name = "Test" }));
        });
        Assert.Equal("Test", cut.Markup);

        editorItem.EditTemplate = null;
        Assert.NotNull(item.EditTemplate);
    }

    class MockEditor : ComponentBase
    {
        [Parameter]
        public Foo? Model { get; set; }

        [Parameter]
        public string? Parameter1 { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, Model?.Name);
        }
    }
}
