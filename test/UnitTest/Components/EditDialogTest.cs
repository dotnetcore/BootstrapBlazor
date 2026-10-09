// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace UnitTest.Components;

public class EditDialogTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task CreateNextModelAsync_RejectsObsoleteModel()
    {
        var initial = new Foo { Name = "initial" };
        var next = new Foo { Name = "next" };
        var savedModels = new List<object>();
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, initial);
            pb.Add(a => a.KeepOpenAfterSave, true);
            pb.Add(a => a.CreateNextModelAsync, () => Task.FromResult(next));
            pb.Add(a => a.OnSaveAsync, context =>
            {
                savedModels.Add(context.Model);
                return Task.FromResult(true);
            });
        });
        var previousForm = cut.FindComponent<ValidateForm>().Instance;
        var oldContext = new EditContext(initial);
        await cut.InvokeAsync(() => previousForm.OnValidSubmit!(oldContext));
        Assert.Same(next, cut.FindComponent<ValidateForm>().Instance.Model);

        await cut.InvokeAsync(() => previousForm.OnValidSubmit!(oldContext));
        Assert.Single(savedModels);

        var currentForm = cut.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => currentForm.OnValidSubmit!(new EditContext(next)));
        Assert.Equal(new object[] { initial, next }, savedModels);

        var external = new Foo { Name = "external" };
        cut.Render(pb => pb.Add(a => a.Model, external));
        await cut.InvokeAsync(() => currentForm.OnValidSubmit!(new EditContext(next)));
        Assert.Equal(2, savedModels.Count);
    }

    [Fact]
    public async Task SaveValueTypeModel_Ok()
    {
        var saved = false;
        var cut = Context.Render<EditDialog<ValueTypeModel>>(pb =>
        {
            pb.Add(a => a.Model, new ValueTypeModel(1));
            pb.Add(a => a.BodyTemplate, model => builder => builder.AddContent(0, model.Value));
            pb.Add(a => a.FooterTemplate, model => builder => builder.AddContent(0, model.Value));
            pb.Add(a => a.OnSaveAsync, context =>
            {
                saved = true;
                return Task.FromResult(false);
            });
        });
        var form = cut.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        Assert.True(saved);
    }

    private readonly record struct ValueTypeModel(int Value);

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async Task CreateNextModelAsync_ValueTypeModelPreservesOnRender(int initialValue)
    {
        var cut = Context.Render<EditDialog<ValueTypeModel>>(pb =>
        {
            pb.Add(a => a.Model, new ValueTypeModel(initialValue));
            pb.Add(a => a.BodyTemplate, model => builder => builder.AddContent(0, $"body-{model.Value}"));
            pb.Add(a => a.FooterTemplate, model => builder => builder.AddContent(0, $"footer-{model.Value}"));
            pb.Add(a => a.KeepOpenAfterSave, true);
            pb.Add(a => a.OnSaveAsync, context => Task.FromResult(true));
            pb.Add(a => a.CreateNextModelAsync, () => Task.FromResult(new ValueTypeModel(2)));
        });
        var form = cut.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        Assert.Equal(new ValueTypeModel(2), cut.FindComponent<ValidateForm>().Instance.Model);

        cut.Render();
        Assert.Equal(new ValueTypeModel(2), cut.FindComponent<ValidateForm>().Instance.Model);
        cut.Contains("body-2");
        cut.Contains("footer-2");

        cut.Render(pb => pb.Add(a => a.Model, new ValueTypeModel(3)));
        Assert.Equal(new ValueTypeModel(3), cut.FindComponent<ValidateForm>().Instance.Model);
        cut.Contains("body-3");
        cut.Contains("footer-3");
    }

    [Fact]
    public async Task CreateNextModelAsync_ValueTypeFailurePersistsOnRender()
    {
        var saveCount = 0;
        var cut = Context.Render<EditDialog<ValueTypeModel>>(pb =>
        {
            pb.Add(a => a.Model, new ValueTypeModel(1));
            pb.Add(a => a.BodyTemplate, model => builder => builder.AddContent(0, model.Value));
            pb.Add(a => a.KeepOpenAfterSave, true);
            pb.Add(a => a.OnSaveAsync, context =>
            {
                saveCount++;
                return Task.FromResult(true);
            });
            pb.Add(a => a.CreateNextModelAsync, () => Task.FromException<ValueTypeModel>(new InvalidOperationException("Initialization failed")));
        });
        var form = cut.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        cut.Render();
        Assert.NotEmpty(cut.Find("[role=alert]").TextContent);
        Assert.True(cut.FindComponents<Button>().Single(button => button.Instance.ButtonType == ButtonType.Submit).Instance.IsDisabled);
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        Assert.Equal(1, saveCount);
    }

    [Fact]
    public void EqualReferenceModels_ExternalModelStillReplacesCurrentModel()
    {
        var initial = new ReferenceTypeModel(1);
        var external = new ReferenceTypeModel(1);
        var cut = Context.Render<EditDialog<ReferenceTypeModel>>(pb =>
        {
            pb.Add(a => a.Model, initial);
            pb.Add(a => a.BodyTemplate, model => builder => builder.AddContent(0, model.Value));
        });
        cut.Render(pb => pb.Add(a => a.Model, external));
        Assert.Same(external, cut.FindComponent<ValidateForm>().Instance.Model);
    }

    private sealed record ReferenceTypeModel(int Value);

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task CreateNextModelAsync_Failed(bool returnNull)
    {
        var saveCount = 0;
        var model = new Foo();
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.Add(a => a.KeepOpenAfterSave, true);
            pb.Add(a => a.OnSaveAsync, context =>
            {
                saveCount++;
                return Task.FromResult(true);
            });
            pb.Add(a => a.CreateNextModelAsync, () => returnNull
                ? Task.FromResult<Foo>(null!)
                : Task.FromException<Foo>(new InvalidOperationException("Initialization failed")));
        });
        var form = cut.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        Assert.Equal(1, saveCount);
        Assert.Same(model, cut.FindComponent<ValidateForm>().Instance.Model);
        Assert.NotEmpty(cut.Find("[role=alert]").TextContent);
        Assert.False(cut.FindComponent<DialogCloseButton>().Instance.IsDisabled);
        Assert.True(cut.FindComponents<Button>().Single(button => button.Instance.ButtonType == ButtonType.Submit).Instance.IsDisabled);
        Assert.True(await cut.InvokeAsync(cut.Instance.CanCloseAsync));

        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        Assert.Equal(1, saveCount);
    }

    [Fact]
    public async Task Saving_BlocksCloseAndDuplicateSubmit()
    {
        var save = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var saveCount = 0;
        var model = new Foo();
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.Add(a => a.OnSaveAsync, context =>
            {
                saveCount++;
                return save.Task;
            });
        });
        var form = cut.FindComponent<ValidateForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        cut.WaitForAssertion(() => Assert.Equal(1, saveCount));
        Assert.False(await cut.InvokeAsync(cut.Instance.CanCloseAsync));
        Assert.All(cut.FindComponents<Button>(), button => Assert.True(button.Instance.IsDisabled));
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        Assert.Equal(1, saveCount);

        save.SetResult(false);
        await submit;
        Assert.True(await cut.InvokeAsync(cut.Instance.CanCloseAsync));
        Assert.All(cut.FindComponents<Button>(), button => Assert.False(button.Instance.IsDisabled));
    }

    [Fact]
    public async Task SaveException_ReleasesBusyState()
    {
        var model = new Foo();
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.Add(a => a.OnSaveAsync, context => Task.FromException<bool>(new InvalidOperationException("Save failed")));
        });
        var form = cut.FindComponent<ValidateForm>().Instance;
        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model))));
        Assert.True(await cut.InvokeAsync(cut.Instance.CanCloseAsync));
        Assert.All(cut.FindComponents<Button>(), button => Assert.False(button.Instance.IsDisabled));
    }

    [Fact]
    public async Task CreateNextModelAsync_Ok()
    {
        var initialModel = new Foo { Name = "initial" };
        var nextModel = new Foo { Name = "next" };
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, initialModel);
            pb.Add(a => a.KeepOpenAfterSave, true);
            pb.Add(a => a.OnSaveAsync, context => Task.FromResult(true));
            pb.Add(a => a.CreateNextModelAsync, () => Task.FromResult(nextModel));
            pb.Add(a => a.BodyTemplate, foo => builder => builder.AddContent(0, $"body-{foo.Name}"));
            pb.Add(a => a.FooterTemplate, foo => builder => builder.AddContent(0, $"footer-{foo.Name}"));
        });
        var firstForm = cut.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => firstForm.OnValidSubmit!(new EditContext(initialModel)));

        Assert.Same(initialModel, cut.Instance.Model);
        Assert.Same(nextModel, cut.FindComponent<ValidateForm>().Instance.Model);
        Assert.NotSame(firstForm, cut.FindComponent<ValidateForm>().Instance);
        cut.Contains("body-next");
        cut.Contains("footer-next");

        cut.Render();
        Assert.Same(nextModel, cut.FindComponent<ValidateForm>().Instance.Model);

        var externalModel = new Foo { Name = "external" };
        cut.Render(pb => pb.Add(a => a.Model, externalModel));
        Assert.Same(externalModel, cut.FindComponent<ValidateForm>().Instance.Model);
        cut.Contains("body-external");
        cut.Contains("footer-external");
    }

    [Fact]
    public void Items_Ok()
    {
        var foo = new Foo();
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
        });

        cut.Contains("bb-editor");
    }
}
