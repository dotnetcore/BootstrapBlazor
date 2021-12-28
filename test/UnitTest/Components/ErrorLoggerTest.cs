// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using System;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class ErrorLoggerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnErrorAsync_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<ErrorLogger>(pb =>
            {
                pb.Add(e => e.ShowToast, false);
                pb.AddChildContent<Button>(pb =>
                {
                    pb.Add(b => b.OnClick, () =>
                    {
                        var a = 0;
                        _ = 1 / a;
                    });
                });
            });
        });
        // 无 Swal 弹窗
        Assert.DoesNotContain("<div class=\"toast-header\">", cut.Markup);

        var errorLogger = cut.FindComponent<ErrorLogger>();
        errorLogger.SetParametersAndRender(pb =>
        {
            pb.Add(e => e.ShowToast, true);
        });
        var button = cut.Find("button");
        button.TriggerEvent("onclick", EventArgs.Empty);
    }

    [Fact]
    public void OnErrorHandleAsync_Ok()
    {
        var tcs = new TaskCompletionSource<bool>();
        var cut = Context.RenderComponent<ErrorLogger>(pb =>
        {
            pb.Add(e => e.OnErrorHandleAsync, (logger, exception) =>
            {
                tcs.SetResult(true);
                return Task.CompletedTask;
            });
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.OnClick, () =>
                {
                    var a = 0;
                    _ = 1 / a;
                });
            });
        });
        var button = cut.Find("button");
        button.TriggerEvent("onclick", EventArgs.Empty);
        Assert.True(tcs.Task.Result);
    }
}
