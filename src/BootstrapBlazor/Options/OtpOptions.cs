// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">OtpOptions class</para>
///  <para lang="en">OtpOptions class</para>
/// </summary>
public class OtpOptions
{
    /// <summary>
    ///  <para lang="zh">获得/设置 the secret key used for generating the QR code.</para>
    ///  <para lang="en">Gets or sets the secret key used for generating the QR code.</para>
    /// </summary>
    public string? SecretKey { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the Issuer name</para>
    ///  <para lang="en">Gets or sets the Issuer name</para>
    /// </summary>
    public string? IssuerName { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the Account name</para>
    ///  <para lang="en">Gets or sets the Account name</para>
    /// </summary>
    public string? AccountName { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the user name</para>
    ///  <para lang="en">Gets or sets the user name</para>
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the OTP hash mode.</para>
    ///  <para lang="en">Gets or sets the OTP hash mode.</para>
    /// </summary>
    public OtpHashMode Algorithm { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the OTP 类型.</para>
    ///  <para lang="en">Gets or sets the OTP type.</para>
    /// </summary>
    public OtpType Type { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the code length. 默认为 6.</para>
    ///  <para lang="en">Gets or sets the code length. Default is 6.</para>
    /// </summary>
    public int Digits { get; set; } = 6;

    /// <summary>
    ///  <para lang="zh">获得/设置 the period in seconds for TOTP. 默认为 30.</para>
    ///  <para lang="en">Gets or sets the period in seconds for TOTP. Default is 30.</para>
    /// </summary>
    public int Period { get; set; } = 30;

    /// <summary>
    ///  <para lang="zh">获得/设置 the counter for Hotp. 默认为 0.</para>
    ///  <para lang="en">Gets or sets the counter for Hotp. Default is 0.</para>
    /// </summary>
    public int Counter { get; set; }
}

/// <summary>
///  <para lang="zh">Abstract class representing a Time-based One-Time Password (TOTP) 实例.</para>
///  <para lang="en">Abstract class representing a Time-based One-Time Password (TOTP) instance.</para>
/// </summary>
public abstract class TotpInstanceBase
{
    /// <summary>
    ///  <para lang="zh">Get the remaining seconds until the next TOTP expiration for the given secret key and timestamp.</para>
    ///  <para lang="en">Get the remaining seconds until the next TOTP expiration for the given secret key and timestamp.</para>
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public abstract int GetRemainingSeconds(DateTime? timestamp = null);

    /// <summary>
    ///  <para lang="zh">Verify the given TOTP code against the expected value using the provided secret key.</para>
    ///  <para lang="en">Verify the given TOTP code against the expected value using the provided secret key.</para>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public abstract bool Verify(string code, DateTime? timestamp = null);
}

/// <summary>
///  <para lang="zh">Abstract class representing an HMAC-based One-Time Password (HOTP) 实例.</para>
///  <para lang="en">Abstract class representing an HMAC-based One-Time Password (HOTP) instance.</para>
/// </summary>
public abstract class HotpInstanceBase
{
    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="counter"></param>
    /// <returns></returns>
    public abstract bool Verify(string code, long counter);
}

/// <summary>
///  <para lang="zh">Enum representing the OTP hash modes.</para>
///  <para lang="en">Enum representing the OTP hash modes.</para>
/// </summary>
public enum OtpHashMode
{
    /// <summary>
    ///  <para lang="zh">SHA1 hash mode</para>
    ///  <para lang="en">SHA1 hash mode</para>
    /// </summary>
    Sha1,

    /// <summary>
    ///  <para lang="zh">SHA256 hash mode</para>
    ///  <para lang="en">SHA256 hash mode</para>
    /// </summary>
    Sha256,

    /// <summary>
    ///  <para lang="zh">SHA512 hash mode</para>
    ///  <para lang="en">SHA512 hash mode</para>
    /// </summary>
    Sha512,
}

/// <summary>
///  <para lang="zh">Enum representing the OTP 类型s.</para>
///  <para lang="en">Enum representing the OTP types.</para>
/// </summary>
public enum OtpType
{
    /// <summary>
    ///  <para lang="zh">Time-based One-Time Password (TOTP) algorithm</para>
    ///  <para lang="en">Time-based One-Time Password (TOTP) algorithm</para>
    /// </summary>
    Totp,

    /// <summary>
    ///  <para lang="zh">HMAC-based One-Time Password (HOTP) algorithm</para>
    ///  <para lang="en">HMAC-based One-Time Password (HOTP) algorithm</para>
    /// </summary>
    Hotp
}
