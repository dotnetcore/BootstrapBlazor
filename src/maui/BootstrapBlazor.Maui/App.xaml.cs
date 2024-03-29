// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Maui;

/// <summary>
/// 
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// 
    /// </summary>
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
}
