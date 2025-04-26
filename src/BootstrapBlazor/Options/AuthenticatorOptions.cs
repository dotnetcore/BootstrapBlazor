// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// AuthenticatorOptions class
/// </summary>
public class AuthenticatorOptions
{
    /// <summary>
    /// Gets or sets the secret key used for generating the QR code.
    /// </summary>
    public string? SecretKey { get; set; }

    /// <summary>
    /// Gets or sets the Issuer name
    /// </summary>
    public string? IssuerName { get; set; }

    /// <summary>
    /// Gets or sets the Account name
    /// </summary>
    public string? AccountName { get; set; }

    /// <summary>
    /// Gets or sets the user name
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the OTP hash mode.
    /// </summary>
    public OTPHashMode Algorithm { get; set; }

    /// <summary>
    /// Gets or sets the OTP type.
    /// </summary>
    public OTPType Type { get; set; }

    /// <summary>
    /// Gets or sets the code length. Default is 6.
    /// </summary>
    public int Digits { get; private set; } = 6;

    /// <summary>
    /// Gets or sets the period in seconds for TOTP. Default is 30.
    /// </summary>
    public int Period { get; private set; } = 30;

    /// <summary>
    /// Gets or sets the counter for HOTP. Default is 0.
    /// </summary>
    public int Counter { get; private set; }

    /// <summary>
    /// Gets the <see cref="TOTPInstance"/> instance. Default is null.
    /// </summary>
    public TOTPInstance? Instance { get; }
}

/// <summary>
/// Abstract class representing a Time-based One-Time Password (TOTP) instance.
/// </summary>
public abstract class TOTPInstance
{
    /// <summary>
    /// Generates a one-time password (OTP) URI for the specified parameters.
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public abstract int GetRemainingSeconds(DateTime? timestamp = null);
}

/// <summary>
/// Enum representing the OTP hash modes.
/// </summary>
public enum OTPHashMode
{
    /// <summary>
    /// SHA1 hash mode
    /// </summary>
    Sha1,

    /// <summary>
    /// SHA256 hash mode
    /// </summary>
    Sha256,

    /// <summary>
    /// SHA512 hash mode
    /// </summary>
    Sha512,
}

/// <summary>
/// Enum representing the OTP types.
/// </summary>
public enum OTPType
{
    /// <summary>
    /// Time-based One-Time Password (TOTP) algorithm
    /// </summary>
    Totp,

    /// <summary>
    /// HMAC-based One-Time Password (HOTP) algorithm
    /// </summary>
    Hotp,
}
