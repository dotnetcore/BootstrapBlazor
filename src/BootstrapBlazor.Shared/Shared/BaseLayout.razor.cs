// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BaseLayout
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<BaseLayout>? Localizer { get; set; }

        [NotNull]
        private string? DownloadText { get; set; }

        [NotNull]
        private string? HomeText { get; set; }

        [NotNull]
        private string? IntroductionText { get; set; }

        [NotNull]
        private string? ComponentsText { get; set; }

        [NotNull]
        private string? FlowText { get; set; }

        [NotNull]
        private string? InstallAppText { get; set; }

        [NotNull]
        private string? InstallText { get; set; }

        [NotNull]
        private string? CancelText { get; set; }

        private static bool Installable = false;

        [NotNull]
        private static Action? OnInstallable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            DownloadText ??= Localizer[nameof(DownloadText)];
            HomeText ??= Localizer[nameof(HomeText)];
            IntroductionText ??= Localizer[nameof(IntroductionText)];
            ComponentsText ??= Localizer[nameof(ComponentsText)];
            FlowText ??= Localizer[nameof(FlowText)];
            InstallAppText ??= Localizer[nameof(InstallAppText)];
            InstallText ??= Localizer[nameof(InstallText)];
            CancelText ??= Localizer[nameof(CancelText)];

            OnInstallable = () => InvokeAsync(StateHasChanged);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public static Task PWAInstallable()
        {
            Installable = true;
            OnInstallable.Invoke();
            return Task.CompletedTask;
        }

        private async Task InstallClicked()
        {
            Installable = false;
            await JSRuntime.InvokeVoidAsync("BlazorPWA.installPWA");
        }
    }
}
