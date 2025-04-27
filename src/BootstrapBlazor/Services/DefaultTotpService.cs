// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultTotpService : ITotpService
{
    public TotpInstanceBase Instance { get; } = new DefaultTotpInstance();

    public string Compute(string secretKey, DateTime? timestamp = null) => "";

    public string GenerateOtpUri(OtpOptions? options = null) => "";

    public string GenerateSecretKey(int length = 20) => "";

    public int GetRemainingSeconds(DateTime? timestamp = null) => 0;

    public byte[] GetSecretKeyBytes(string input) => [];

    public bool Verify(string code, DateTime? timestamp = null) => false;

    class DefaultTotpInstance : TotpInstanceBase
    {
        public override int GetRemainingSeconds(DateTime? timestamp = null) => 0;

        public override bool Verify(string code, DateTime? timestamp = null) => false;
    }
}
