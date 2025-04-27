// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// OtpOptions class
/// </summary>
public class OtpOptions
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
    public OtpHashMode Algorithm { get; set; }

    /// <summary>
    /// Gets or sets the OTP type.
    /// </summary>
    public OtpType Type { get; set; }

    /// <summary>
    /// Gets or sets the code length. Default is 6.
    /// </summary>
    public int Digits { get; set; } = 6;

    /// <summary>
    /// Gets or sets the period in seconds for TOTP. Default is 30.
    /// </summary>
    public int Period { get; set; } = 30;

    /// <summary>
    /// Gets or sets the counter for Hotp. Default is 0.
    /// </summary>
    public int Counter { get; set; }
}

/// <summary>
/// Abstract class representing a Time-based One-Time Password (TOTP) instance.
/// </summary>
public abstract class TotpInstanceBase
{
    /// <summary>
    /// Get the remaining seconds until the next TOTP expiration for the given secret key and timestamp.
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public abstract int GetRemainingSeconds(DateTime? timestamp = null);

    /// <summary>
    /// Verify the given TOTP code against the expected value using the provided secret key.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public abstract bool Verify(string code, DateTime? timestamp = null);
}

/// <summary>
/// Abstract class representing an HMAC-based One-Time Password (HOTP) instance.
/// </summary>
public abstract class HotpInstanceBase
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="code"></param>
    /// <param name="counter"></param>
    /// <returns></returns>
    public abstract bool Verify(string code, long counter);
}

/// <summary>
/// Enum representing the OTP hash modes.
/// </summary>
public enum OtpHashMode
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
public enum OtpType
{
    /// <summary>
    /// Time-based One-Time Password (TOTP) algorithm
    /// </summary>
    Totp,

    /// <summary>
    /// HMAC-based One-Time Password (HOTP) algorithm
    /// </summary>
    Hotp
}
