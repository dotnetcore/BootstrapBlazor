// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FloatingLabels
    {
        private string? PlaceHolderText { get; set; }

        private byte[] ByteArray { get; set; } = new byte[] { 0x01, 0x12, 0x34, 0x56 };

        private static string ByteArrayFormatter(byte[] source) => Convert.ToBase64String(source);

        [NotNull]
        private Foo? Model { get; set; }

        private static string DateTimeFormatter(DateTime source) => source.ToString("yyyy-MM-dd");

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PlaceHolderText = Localizer["PlaceHolder"];
            Model = new Foo() { Name = FLocalizer["TestName"] };
        }

        private IEnumerable<AttributeItem> GetAttributes() => new[]
        {
            new AttributeItem() {
                Name = "ChildContent",
                Description = FLocalizer["Att1"]!,
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowLabel",
                Description = FLocalizer["Att2"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = FLocalizer["Att3"]!,
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FormatString",
                Description = FLocalizer["Att4"]!,
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Formatter",
                Description = FLocalizer["Att5"]!,
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "type",
                Description = FLocalizer["Att6"]!,
                Type = "string",
                ValueList = "text / number / email / url / password",
                DefaultValue = "text"
            },
            new AttributeItem()
            {
                Name = "IsDisabled",
                Description = FLocalizer["Att7"]!,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
        };
    }
}
