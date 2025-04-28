// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// OtpInputs samples
/// </summary>
public partial class OtpInputs
{
    private string _value = "818924";

    private OtpInputType _otpInputType;

    private string _placeHolder = "X";

    private bool _readonly = false;

    private bool _disabled = false;

    private LoginModel _model = new() { UserName = "Admin", Password = "" };
}
