// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Global configuration class for components</para>
/// <para lang="en">Global configuration class for components</para>
/// </summary>
public class BootstrapBlazorOptions : IOptions<BootstrapBlazorOptions>
{
    /// <summary>
    /// <para lang="zh">获得/设置 the default delay for the Toast component, default is 0</para>
    /// <para lang="en">Gets or sets the default delay for the Toast component, default is 0</para>
    /// </summary>
    public int ToastDelay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the default delay for the Message component, default is 0</para>
    /// <para lang="en">Gets or sets the default delay for the Message component, default is 0</para>
    /// </summary>
    public int MessageDelay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the default delay for the Swal component, default is 0</para>
    /// <para lang="en">Gets or sets the default delay for the Swal component, default is 0</para>
    /// </summary>
    public int SwalDelay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the fallback default language culture, default is "en" (English)</para>
    /// <para lang="en">Gets or sets the fallback default language culture, default is "en" (English)</para>
    /// </summary>
    public string FallbackCulture { get; set; } = "en";

    /// <summary>
    /// <para lang="zh">获得/设置 the default position for the Toast component globally, default is null. When set, it overrides the site-wide setting</para>
    /// <para lang="en">Gets or sets the default position for the Toast component globally, default is null. When set, it overrides the site-wide setting</para>
    /// </summary>
    public Placement? ToastPlacement { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the list of built-in localization languages for components, default is null</para>
    /// <para lang="en">Gets or sets the list of built-in localization languages for components, default is null</para>
    /// </summary>
    public List<string>? SupportedCultures { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to enable global 异常 capture functionality, default is true</para>
    /// <para lang="en">Gets or sets whether to enable global exception capture functionality, default is true</para>
    /// </summary>
    public bool EnableErrorLogger { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to enable show toast popup when global 异常 capture, default is true</para>
    /// <para lang="en">Gets or sets whether to enable show toast popup when global exception capture, default is true</para>
    /// </summary>
    public bool ShowErrorLoggerToast { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 a value indicating 是否 error logging using an <see cref="ILogger"/> is enabled, default is true</para>
    /// <para lang="en">Gets or sets a value indicating whether error logging using an <see cref="ILogger"/> is enabled, default is true</para>
    /// </summary>
    public bool EnableErrorLoggerILogger { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to fall back to the fallback culture, default is true</para>
    /// <para lang="en">Gets or sets whether to fall back to the fallback culture, default is true</para>
    /// </summary>
    public bool EnableFallbackCulture { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to ignore missing culture log information, default is null (not set)</para>
    /// <para lang="en">Gets or sets whether to ignore missing culture log information, default is null (not set)</para>
    /// </summary>
    /// <remarks>Uses the default value of <see cref="JsonLocalizationOptions.IgnoreLocalizerMissing"/></remarks>
    public bool? IgnoreLocalizerMissing { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to disable fetching localization resources from the service, default is false (not disabled)</para>
    /// <para lang="en">Gets or sets whether to disable fetching localization resources from the service, default is false (not disabled)</para>
    /// </summary>
    public bool? DisableGetLocalizerFromService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to disable fetching localization resources of 类型 <see cref="ResourceManagerStringLocalizer"/>, default is false (not disabled)</para>
    /// <para lang="en">Gets or sets whether to disable fetching localization resources of type <see cref="ResourceManagerStringLocalizer"/>, default is false (not disabled)</para>
    /// </summary>
    public bool? DisableGetLocalizerFromResourceManager { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the default culture information</para>
    /// <para lang="en">Gets or sets the default culture information</para>
    /// </summary>
    /// <remarks>This parameter is invalid when multi-culture is enabled</remarks>
    public string? DefaultCultureInfo { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to disable the automatic form submission feature by pressing Enter, default is null (not set)</para>
    /// <para lang="en">Gets or sets whether to disable the automatic form submission feature by pressing Enter, default is null (not set)</para>
    /// </summary>
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the JavaScript module script version number, default is null</para>
    /// <para lang="en">Gets or sets the JavaScript module script version number, default is null</para>
    /// </summary>
    public string? JSModuleVersion { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="TableSettings"/> configuration 实例</para>
    /// <para lang="en">Gets or sets the <see cref="TableSettings"/> configuration instance</para>
    /// </summary>
    public TableSettings TableSettings { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="ModalSettings"/> configuration 实例</para>
    /// <para lang="en">Gets or sets the <see cref="ModalSettings"/> configuration instance</para>
    /// </summary>
    public ModalSettings ModalSettings { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="StepSettings"/> configuration 实例</para>
    /// <para lang="en">Gets or sets the <see cref="StepSettings"/> configuration instance</para>
    /// </summary>
    public StepSettings StepSettings { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="ConnectionHubOptions"/> configuration</para>
    /// <para lang="en">Gets or sets the <see cref="ConnectionHubOptions"/> configuration</para>
    /// </summary>
    public ConnectionHubOptions ConnectionHubOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="WebClientOptions"/> configuration</para>
    /// <para lang="en">Gets or sets the <see cref="WebClientOptions"/> configuration</para>
    /// </summary>
    public WebClientOptions WebClientOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="IpLocatorOptions"/> configuration</para>
    /// <para lang="en">Gets or sets the <see cref="IpLocatorOptions"/> configuration</para>
    /// </summary>
    public IpLocatorOptions IpLocatorOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="ScrollOptions"/> configuration</para>
    /// <para lang="en">Gets or sets the <see cref="ScrollOptions"/> configuration</para>
    /// </summary>
    public ScrollOptions ScrollOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="ContextMenuOptions"/> configuration</para>
    /// <para lang="en">Gets or sets the <see cref="ContextMenuOptions"/> configuration</para>
    /// </summary>
    public ContextMenuOptions ContextMenuOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 the CacheManagerOptions configuration</para>
    /// <para lang="en">Gets or sets the CacheManagerOptions configuration</para>
    /// </summary>
    public CacheManagerOptions CacheManagerOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">Get or sets website use memorial mode. default is false</para>
    /// <para lang="en">Get or sets website use memorial mode. default is false</para>
    /// </summary>
    public bool IsMemorialMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="Select{TValue}"/> 等组件是否将 <see cref="IsPopover"/> 参数默认值更改为 true 默认 false</para>
    /// <para lang="en">Gets or sets whether the <see cref="IsPopover"/> parameter default value is changed to true for components such as <see cref="Select{TValue}"/> default false</para>
    /// </summary>
    public bool IsPopover { get; set; }

    BootstrapBlazorOptions IOptions<BootstrapBlazorOptions>.Value => this;

    /// <summary>
    /// <para lang="zh">获得 the 集合 of supported languages</para>
    /// <para lang="en">Gets the collection of supported languages</para>
    /// </summary>
    public IList<CultureInfo> GetSupportedCultures() => SupportedCultures?.Select(name => new CultureInfo(name)).ToList()
        ?? [new("en"), new("zh")];
}
