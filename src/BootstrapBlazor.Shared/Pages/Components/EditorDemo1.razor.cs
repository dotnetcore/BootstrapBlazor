// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EditorDemo1
    {
        [NotNull]
        private Logger? Trace { get; set; }

        private readonly Foo Model = new Foo();

        private Task OnSubmit(EditContext context)
        {
            Trace.Log("OnSubmit 回调委托");
            return Task.CompletedTask;
        }
    }
}
