// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Global configuration class for components
/// </summary>
public class BootstrapBlazorOptions : IOptions<BootstrapBlazorOptions>
{
    /// <summary>
    /// Gets or sets the default delay for the Toast component, default is 0
    /// </summary>
    public int ToastDelay { get; set; }

    /// <summary>
    /// Gets or sets the default delay for the Message component, default is 0
    /// </summary>
    public int MessageDelay { get; set; }

    /// <summary>
    /// Gets or sets the default delay for the Swal component, default is 0
    /// </summary>
    public int SwalDelay { get; set; }

    /// <summary>
    /// Gets or sets the fallback default language culture, default is "en" (English)
    /// </summary>
    public string FallbackCulture { get; set; } = "en";

    /// <summary>
    /// Gets or sets the default position for the Toast component globally, default is null. When set, it overrides the site-wide setting.
    /// </summary>
    public Placement? ToastPlacement { get; set; }

    /// <summary>
    /// Gets or sets the list of built-in localization languages for components, default is null
    /// </summary>
    public List<string>? SupportedCultures { get; set; }

    /// <summary>
    /// Gets or sets whether to enable global exception capture functionality, default is true
    /// </summary>
    public bool EnableErrorLogger { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to fall back to the fallback culture, default is true
    /// </summary>
    public bool EnableFallbackCulture { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to ignore missing culture log information, default is null (not set)
    /// </summary>
    /// <remarks>Uses the default value of <see cref="JsonLocalizationOptions.IgnoreLocalizerMissing"/></remarks>
    public bool? IgnoreLocalizerMissing { get; set; }

    /// <summary>
    /// Gets or sets whether to disable fetching localization resources from the service, default is false (not disabled)
    /// </summary>
    public bool? DisableGetLocalizerFromService { get; set; }

    /// <summary>
    /// Gets or sets whether to disable fetching localization resources of type <see cref="ResourceManagerStringLocalizer"/>, default is false (not disabled)
    /// </summary>
    public bool? DisableGetLocalizerFromResourceManager { get; set; }

    /// <summary>
    /// Gets or sets the default culture information
    /// </summary>
    /// <remarks>This parameter is invalid when multi-culture is enabled</remarks>
    public string? DefaultCultureInfo { get; set; }

    /// <summary>
    /// Gets or sets whether to disable the automatic form submission feature by pressing Enter, default is null (not set)
    /// </summary>
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// Gets or sets the JavaScript module script version number, default is null
    /// </summary>
    public string? JSModuleVersion { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="TableSettings"/> configuration instance
    /// </summary>
    public TableSettings TableSettings { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="ModalSettings"/> configuration instance
    /// </summary>
    public ModalSettings ModalSettings { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="StepSettings"/> configuration instance
    /// </summary>
    public StepSettings StepSettings { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="ConnectionHubOptions"/> configuration
    /// </summary>
    public ConnectionHubOptions ConnectionHubOptions { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="WebClientOptions"/> configuration
    /// </summary>
    public WebClientOptions WebClientOptions { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="IpLocatorOptions"/> configuration
    /// </summary>
    public IpLocatorOptions IpLocatorOptions { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="ScrollOptions"/> configuration
    /// </summary>
    public ScrollOptions ScrollOptions { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="ContextMenuOptions"/> configuration
    /// </summary>
    public ContextMenuOptions ContextMenuOptions { get; set; } = new();

    /// <summary>
    /// Gets or sets the CacheManagerOptions configuration
    /// </summary>
    public CacheManagerOptions CacheManagerOptions { get; set; } = new();

    /// <summary>
    /// Get or sets website use memorial mode. default is false
    /// </summary>
    public bool IsMemorialMode { get; set; }

    BootstrapBlazorOptions IOptions<BootstrapBlazorOptions>.Value => this;

    /// <summary>
    /// Gets the collection of supported languages
    /// </summary>
    /// <returns></returns>
    public IList<CultureInfo> GetSupportedCultures() => SupportedCultures?.Select(name => new CultureInfo(name)).ToList()
        ?? [new("en"), new("zh")];
}
