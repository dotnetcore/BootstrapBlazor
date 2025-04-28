// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITotpService interface
/// </summary>
public interface ITotpService
{
    /// <summary>
    /// Generates a one-time password (OTP) URI for the specified parameters.
    /// </summary>
    /// <param name="options">the instance of <see cref="OtpOptions"/></param>
    /// <returns></returns>
    string GenerateOtpUri(OtpOptions? options = null);

    /// <summary>
    /// Computes the Time-based One-Time Password (TOTP) for the given secret key and timestamp.
    /// </summary>
    /// <param name="secretKey"></param>
    /// <param name="period"></param>
    /// <param name="mode"></param>
    /// <param name="digits"></param>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    string Compute(string secretKey, int period = 30, OtpHashMode mode = OtpHashMode.Sha1, int digits = 6, DateTime? timestamp = null);

    /// <summary>
    /// Computes the remaining seconds until the next TOTP expiration for the given secret key and timestamp.
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    int GetRemainingSeconds(DateTime? timestamp = null);

    /// <summary>
    /// Generates a random secret key for OTP authentication.
    /// </summary>
    /// <returns></returns>
    string GenerateSecretKey(int length = 20);

    /// <summary>
    /// Gets the secret key bytes from the given input string.
    /// </summary>
    /// <returns></returns>
    byte[] GetSecretKeyBytes(string input);

    /// <summary>
    /// Verifies the given TOTP code against the expected value using the provided secret key.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    bool Verify(string code, DateTime? timestamp = null);

    /// <summary>
    /// Gets the <see cref="TotpInstanceBase"/> instance. Default is null.
    /// </summary>
    TotpInstanceBase Instance { get; }
}
