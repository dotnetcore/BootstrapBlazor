// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public partial class ValidateForms
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    [NotNull]
    private BlockLogger? Trace2 { get; set; }

    [NotNull]
    private BlockLogger? Trace3 { get; set; }

    [NotNull]
    private BlockLogger? Trace4 { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbys { get; set; }

    [NotNull]
    private ValidateForm? FooForm { get; set; }

    [NotNull]
    private ValidateForm? ComplexForm { get; set; }

    [NotNull]
    private ComplexFoo? ComplexModel { get; set; }

    private List<IValidator> CustomerRules { get; } = new();

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CustomerRules.Add(new FormItemValidator(new EmailAddressAttribute()));
    }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        // 切换线程 模拟异步通过 webapi 加载数据
        await Task.Yield();

        Model = new() { Name = "Name", Education = EnumEducation.Primary, DateTime = DateTime.Now };

        // 初始化参数
        Hobbys = Foo.GenerateHobbys(LocalizerFoo);
        ComplexModel = new ComplexFoo()
        {
            Dummy = new Dummy1() { Dummy2 = new Dummy2() },
        };
    }

    private Task OnInvalidSubmit1(EditContext context)
    {
        Trace.Log(Localizer["OnInvalidSubmitLog"]);
        return Task.CompletedTask;
    }

    private async Task OnValidSubmit1(EditContext context)
    {
        await Task.Delay(1000);
        Trace.Log(Localizer["OnValidSubmitLog"]);
    }

    private void OnFiledChanged(string field,object? value)
    {
        Trace.Log($"{field}:{value}");
    }

    private async Task OnValidSubmit(EditContext context)
    {
        Trace2.Log(Localizer["OnValidSubmitSatringLog"]);
        await Task.Delay(3000);
        Trace2.Log(Localizer["OnValidSubmitDoneLog"]);
    }

    private Task OnInvalidSubmit(EditContext context)
    {
        Trace2.Log(Localizer["OnInvalidSubmitCallBackLog"]);
        return Task.CompletedTask;
    }

    private Task OnInvalidSubmitAddress(EditContext context)
    {
        Trace3.Log(Localizer["OnInvalidSubmitCallBackLog"]);
        return Task.CompletedTask;
    }

    private Task OnInvalidComplexModel(EditContext context)
    {
        Trace4.Log(Localizer["OnInvalidSubmitCallBackLog"]);
        return Task.CompletedTask;
    }

    private Task OnValidComplexModel(EditContext context)
    {
        Trace4.Log(Localizer["OnValidSubmitCallBackLog"]);
        ComplexForm.SetError("Dummy.Dummy2.Name", Localizer["DatabaseExistLog"]);
        return Task.CompletedTask;
    }

    private Task OnValidSetError(EditContext context)
    {
        FooForm.SetError<Foo>(f => f.Name, Localizer["DatabaseExistLog"]);
        return Task.CompletedTask;
    }

    private ConcurrentDictionary<FieldIdentifier, object?> GetValueChagnedFieldCollection() => ComplexForm?.ValueChagnedFields ?? new ConcurrentDictionary<FieldIdentifier, object?>();

    #region 动态更改表单内验证组件
    [NotNull]
    private BlockLogger? Trace5 { get; set; }

    private bool ShowAddress { get; set; }

    private Foo DynamicModel { get; set; } = new Foo();

    private Task OnInvalidDynamicModel(EditContext context)
    {
        Trace5.Log(Localizer["OnInvalidSubmitCallBackLog"]);
        return Task.CompletedTask;
    }

    private Task OnValidDynamicModel(EditContext context)
    {
        Trace5.Log(Localizer["OnValidSubmitCallBackLog"]);
        return Task.CompletedTask;
    }

    private void OnValidateChange()
    {
        ShowAddress = true;
    }

    private void OnValidateReset()
    {
        ShowAddress = false;
    }
    #endregion

    private class ComplexFoo : Foo
    {
        [NotNull]
        public Dummy1? Dummy { get; set; }
    }

    private class Dummy1
    {
        [NotNull]
        public Dummy2? Dummy2 { get; set; }
    }

    private class Dummy2
    {
        [Required]
        public string? Name { get; set; }
    }

    #region 参数说明
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Model",
                Description = Localizer["Model"],
                Type = "object",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ValidateAllProperties",
                Description = Localizer["ValidateAllProperties"],
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowRequiredMark",
                Description = Localizer["ShowRequiredMark"],
                Type = "bool",
                ValueList = "true/false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = Localizer["ChildContent"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnValidSubmit",
                Description = Localizer["OnValidSubmit"],
                Type = "EventCallback<EditContext>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnInvalidSubmit",
                Description = Localizer["OnInvalidSubmit"],
                Type = "EventCallback<EditContext>",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
            new MethodItem()
            {
                Name = "SetError",
                Description = Localizer["SetError"],
                Parameters = "PropertyName, ErrorMessage",
                ReturnValue = " — "
            }
    };
    #endregion
}
