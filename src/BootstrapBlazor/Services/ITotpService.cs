// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ITotpService interface</para>
/// <para lang="en">ITotpService interface</para>
/// </summary>
public interface ITotpService
{
    /// <summary>
    /// <para lang="zh">Generates a one-time password (OTP) URI for the specified parameters.</para>
    /// <para lang="en">Generates a one-time password (OTP) URI for the specified parameters.</para>
    /// </summary>
    /// <param name="options">the instance of <see cref="OtpOptions"/></param>
    string GenerateOtpUri(OtpOptions? options = null);

    /// <summary>
    /// <para lang="zh">Computes the Time-based One-Time Password (TOTP) for the given secret key and timestamp.</para>
    /// <para lang="en">Computes the Time-based One-Time Password (TOTP) for the given secret key and timestamp.</para>
    /// </summary>
    /// <param name="secretKey"></param>
    /// <param name="period"></param>
    /// <param name="mode"></param>
    /// <param name="digits"></param>
    /// <param name="timestamp"></param>
    string Compute(string secretKey, int period = 30, OtpHashMode mode = OtpHashMode.Sha1, int digits = 6, DateTime? timestamp = null);

    /// <summary>
    /// <para lang="zh">Computes the remaining seconds until the next TOTP expiration for the given secret key and timestamp.</para>
    /// <para lang="en">Computes the remaining seconds until the next TOTP expiration for the given secret key and timestamp.</para>
    /// </summary>
    /// <param name="timestamp"></param>
    int GetRemainingSeconds(DateTime? timestamp = null);

    /// <summary>
    /// <para lang="zh">Generates a random secret key for OTP authentication.</para>
    /// <para lang="en">Generates a random secret key for OTP authentication.</para>
    /// </summary>
    string GenerateSecretKey(int length = 20);

    /// <summary>
    /// <para lang="zh">获得 the secret key bytes from the given input string.</para>
    /// <para lang="en">Gets the secret key bytes from the given input string.</para>
    /// </summary>
    byte[] GetSecretKeyBytes(string input);

    /// <summary>
    /// <para lang="zh">Verifies the given TOTP code against the expected value using the provided secret key.</para>
    /// <para lang="en">Verifies the given TOTP code against the expected value using the provided secret key.</para>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="timestamp"></param>
    bool Verify(string code, DateTime? timestamp = null);

    /// <summary>
    /// <para lang="zh">获得 the <see cref="TotpInstanceBase"/> 实例. 默认为 null.</para>
    /// <para lang="en">Gets the <see cref="TotpInstanceBase"/> instance. Default is null.</para>
    /// </summary>
    TotpInstanceBase Instance { get; }
}
