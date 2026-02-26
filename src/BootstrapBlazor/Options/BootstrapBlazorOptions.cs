// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">组件全局配置类</para>
/// <para lang="en">Global configuration class for components</para>
/// </summary>
public class BootstrapBlazorOptions : IOptions<BootstrapBlazorOptions>
{
    /// <summary>
    /// <para lang="zh">获得/设置 Toast 组件默认延时。默认为 0</para>
    /// <para lang="en">Gets or sets the default delay for the Toast component, default is 0</para>
    /// </summary>
    public int ToastDelay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Message 组件默认延时。默认为 0</para>
    /// <para lang="en">Gets or sets the default delay for the Message component, default is 0</para>
    /// </summary>
    public int MessageDelay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Swal 组件默认延时。默认为 0</para>
    /// <para lang="en">Gets or sets the default delay for the Swal component, default is 0</para>
    /// </summary>
    public int SwalDelay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 回退默认语言文化。默认为 "en"（英语）</para>
    /// <para lang="en">Gets or sets the fallback default language culture, default is "en" (English)</para>
    /// </summary>
    public string FallbackCulture { get; set; } = "en";

    /// <summary>
    /// <para lang="zh">获得/设置 Toast 组件全局默认显示位置。默认为 null。设置后将覆盖站点级配置</para>
    /// <para lang="en">Gets or sets the default position for the Toast component globally, default is null. When set, it overrides the site-wide setting</para>
    /// </summary>
    public Placement? ToastPlacement { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件内置本地化语言列表。默认为 null</para>
    /// <para lang="en">Gets or sets the list of built-in localization languages for components, default is null</para>
    /// </summary>
    public List<string>? SupportedCultures { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用全局异常捕获功能。默认为 true</para>
    /// <para lang="en">Gets or sets whether to enable global exception capture functionality, default is true</para>
    /// </summary>
    public bool EnableErrorLogger { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否在全局异常捕获时显示 Toast 弹窗。默认为 true</para>
    /// <para lang="en">Gets or sets whether to enable show toast popup when global exception capture, default is true</para>
    /// </summary>
    public bool ShowErrorLoggerToast { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用基于 <see cref="ILogger"/> 的错误日志记录。默认为 true</para>
    /// <para lang="en">Gets or sets a value indicating whether error logging using an <see cref="ILogger"/> is enabled, default is true</para>
    /// </summary>
    public bool EnableErrorLoggerILogger { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否回退到回退默认语言文化。默认为 true</para>
    /// <para lang="en">Gets or sets whether to fall back to the fallback culture, default is true</para>
    /// </summary>
    public bool EnableFallbackCulture { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否忽略缺失文化的日志信息。默认为 null（未设置）。使用 <see cref="JsonLocalizationOptions.IgnoreLocalizerMissing"/> 的默认值</para>
    /// <para lang="en">Gets or sets whether to ignore missing culture log information, default is null (not set). Uses the default value of <see cref="JsonLocalizationOptions.IgnoreLocalizerMissing"/></para>
    /// </summary>
    public bool? IgnoreLocalizerMissing { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用从服务获取本地化资源。默认为 false（不禁用）</para>
    /// <para lang="en">Gets or sets whether to disable fetching localization resources from the service, default is false (not disabled)</para>
    /// </summary>
    public bool? DisableGetLocalizerFromService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用获取 <see cref="ResourceManagerStringLocalizer"/> 类型的本地化资源。默认为 false（不禁用）</para>
    /// <para lang="en">Gets or sets whether to disable fetching localization resources of type <see cref="ResourceManagerStringLocalizer"/>, default is false (not disabled)</para>
    /// </summary>
    public bool? DisableGetLocalizerFromResourceManager { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 默认文化信息。启用多语言时该参数无效</para>
    /// <para lang="en">Gets or sets the default culture information. This parameter is invalid when multi-culture is enabled</para>
    /// </summary>
    public string? DefaultCultureInfo { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用按 Enter 自动提交表单功能。默认为 null（未设置）</para>
    /// <para lang="en">Gets or sets whether to disable the automatic form submission feature by pressing Enter, default is null (not set)</para>
    /// </summary>
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 JavaScript 模块脚本版本号。默认为 null</para>
    /// <para lang="en">Gets or sets the JavaScript module script version number, default is null</para>
    /// </summary>
    public string? JSModuleVersion { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="TableSettings"/> 配置实例</para>
    /// <para lang="en">Gets or sets the <see cref="TableSettings"/> configuration instance</para>
    /// </summary>
    public TableSettings TableSettings { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="EditDialogSettings"/> 配置实例</para>
    /// <para lang="en">Gets or sets the <see cref="EditDialogSettings"/> configuration instance</para>
    /// <para><version>10.3.3</version></para>
    /// </summary>
    public EditDialogSettings EditDialogSettings { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="ModalSettings"/> 配置实例</para>
    /// <para lang="en">Gets or sets the <see cref="ModalSettings"/> configuration instance</para>
    /// </summary>
    public ModalSettings ModalSettings { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="StepSettings"/> 配置实例</para>
    /// <para lang="en">Gets or sets the <see cref="StepSettings"/> configuration instance</para>
    /// </summary>
    public StepSettings StepSettings { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="ConnectionHubOptions"/> 配置</para>
    /// <para lang="en">Gets or sets the <see cref="ConnectionHubOptions"/> configuration</para>
    /// </summary>
    public ConnectionHubOptions ConnectionHubOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="WebClientOptions"/> 配置</para>
    /// <para lang="en">Gets or sets the <see cref="WebClientOptions"/> configuration</para>
    /// </summary>
    public WebClientOptions WebClientOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="IpLocatorOptions"/> 配置</para>
    /// <para lang="en">Gets or sets the <see cref="IpLocatorOptions"/> configuration</para>
    /// </summary>
    public IpLocatorOptions IpLocatorOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="ScrollOptions"/> 配置</para>
    /// <para lang="en">Gets or sets the <see cref="ScrollOptions"/> configuration</para>
    /// </summary>
    public ScrollOptions ScrollOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="ContextMenuOptions"/> 配置</para>
    /// <para lang="en">Gets or sets the <see cref="ContextMenuOptions"/> configuration</para>
    /// </summary>
    public ContextMenuOptions ContextMenuOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 CacheManagerOptions 配置</para>
    /// <para lang="en">Gets or sets the CacheManagerOptions configuration</para>
    /// </summary>
    public CacheManagerOptions CacheManagerOptions { get; set; } = new();

    /// <summary>
    /// <para lang="zh">获得/设置 网站是否启用纪念模式。默认为 false</para>
    /// <para lang="en">Gets or sets whether the website uses memorial mode. Default is false</para>
    /// </summary>
    public bool IsMemorialMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否将 <see cref="Select{TValue}"/> 等组件的 <see cref="IsPopover"/> 参数默认值更改为 true。默认为 false</para>
    /// <para lang="en">Gets or sets whether the <see cref="IsPopover"/> parameter default value is changed to true for components such as <see cref="Select{TValue}"/> default false</para>
    /// </summary>
    public bool IsPopover { get; set; }

    BootstrapBlazorOptions IOptions<BootstrapBlazorOptions>.Value => this;

    /// <summary>
    /// <para lang="zh">获取支持的语言集合</para>
    /// <para lang="en">Gets the collection of supported languages</para>
    /// </summary>
    public IList<CultureInfo> GetSupportedCultures() => SupportedCultures?.Select(name => new CultureInfo(name)).ToList()
        ?? [new("en"), new("zh")];
}
