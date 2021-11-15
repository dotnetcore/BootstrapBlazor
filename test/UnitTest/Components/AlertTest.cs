// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components
{
    public class AlertTest : TestBase
    {
        [Fact]
        public void ShowDismiss_Ok()
        {
            var cut = Context.RenderComponent<Alert>(new ComponentParameter[]
            {
               ComponentParameter.CreateParameter(nameof(Alert.ShowDismiss), true)
            });

            Assert.Contains("button", cut.Markup);
        }

        [Fact]
        public void DisableShowDismiss_Ok()
        {
            var cut = Context.RenderComponent<Alert>(new ComponentParameter[]
            {
               ComponentParameter.CreateParameter(nameof(Alert.ShowDismiss), false)
            });

            Assert.DoesNotContain("button", cut.Markup);
        }

        [Fact]
        public void ShowBar_Ok()
        {
            var cut = Context.RenderComponent<Alert>(new ComponentParameter[]
            {
               ComponentParameter.CreateParameter(nameof(Alert.ShowBar), true)
            });

            Assert.Contains("is-bar", cut.Markup);
        }

        [Fact]
        public void DisableShowBar_Ok()
        {
            var cut = Context.RenderComponent<Alert>(new ComponentParameter[]
            {
               ComponentParameter.CreateParameter(nameof(Alert.ShowBar), false)
            });

            Assert.DoesNotContain("is-bar", cut.Markup);
        }

        [Fact]
        public void OnDismissHandle_Ok()
        {
            string message = "";
            var cut = Context.RenderComponent<Alert>(new ComponentParameter[]
            {
               ComponentParameter.CreateParameter(nameof(Alert.ShowDismiss), true),
               ComponentParameter.CreateParameter(nameof(Alert.OnDismiss), new Func<Task>(()=>
               {
                   message = "Alert Dismissed";
                   return  Task.CompletedTask;
                }))
            });

            cut.Find("button").Click();

            Assert.Equal("Alert Dismissed", message);

            //判断是否关闭
            Assert.Contains("d-none", cut.Markup);
        }

        [Fact]
        public void ChildContent_Ok()
        {
            var cut = Context.RenderComponent<Alert>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(Alert.ChildContent), BuildeComponent()),
            });

            Assert.Contains("I am Alert", cut.Markup);

            RenderFragment BuildeComponent() => builder =>
            {
                builder.OpenElement(1, "div");
                builder.AddContent(2, "I am Alert");
                builder.CloseElement();
            };
        }

        [Fact]
        public void Color_Ok()
        {
            var cut = Context.RenderComponent<Alert>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(Alert.Color), Color.Primary),
            });

            Assert.Contains("alert-primary", cut.Markup);
        }

        [Fact]
        public void Icon_Ok()
        {
            var cut = Context.RenderComponent<Alert>(new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(nameof(Alert.Icon), "fa fa-check-circle"),
            });

            Assert.Contains("fa fa-check-circle", cut.Markup);
        }
    }
}
