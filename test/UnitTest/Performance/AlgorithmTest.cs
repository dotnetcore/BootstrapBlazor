// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Security.Cryptography;
using System.Text;

namespace UnitTest.Performance;

public class AlgorithmTest
{
    [Fact]
    public void RSA_Test()
    {
        //using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //var publicKey = rsa.ToXmlString(false);
        //var base64PublicKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(publicKey));
        //var privateKey = rsa.ToXmlString(true);
        //var base64PrivateKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(privateKey));

        var publicKey = Encoding.UTF8.GetString(Convert.FromBase64String("PFJTQUtleVZhbHVlPjxNb2R1bHVzPnVHVUNETHk2ZDUzSlVlcTR6TmFLajVzcTl2SUxHRE1oOElkNm16cU0yWDF2VkJQZkF3STk0aUpjQ0s0dVhKSzBNK0p1bnp4VjhlTExTdHhqK0pmaEdCZVhCR1drZldKNTRUUjdlVVFCOTBiRHpNWnEwSUNMakZwZ3kvaVFFUllpbGpCUGV3VjE2STMvUnRSZnJZN0s2RVYrdlNybVNmVWRycnRHSTZMWkR6MD08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+"));
        var privateKey = Encoding.UTF8.GetString(Convert.FromBase64String("PFJTQUtleVZhbHVlPjxNb2R1bHVzPnVHVUNETHk2ZDUzSlVlcTR6TmFLajVzcTl2SUxHRE1oOElkNm16cU0yWDF2VkJQZkF3STk0aUpjQ0s0dVhKSzBNK0p1bnp4VjhlTExTdHhqK0pmaEdCZVhCR1drZldKNTRUUjdlVVFCOTBiRHpNWnEwSUNMakZwZ3kvaVFFUllpbGpCUGV3VjE2STMvUnRSZnJZN0s2RVYrdlNybVNmVWRycnRHSTZMWkR6MD08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPjBiWlQvT1ROSXIvUGJidTRsR0ZUVDFWb0NqMHlUU2pjRWQ0bkd0MHZUODZkZHNPckhkYVV3RkQvUFdCZW9EdmJ2YWVtM3NTOUg1NVBHL3NFZkswYU53PT08L1A+PFE+NFJnZXNBK0t2NXBsSWwvSFNvVUovQ3JhYmhtUnVoRERVTXM4TkRvQXRMaWw0RVRnSGxpZUt4eisxclhMTGhMNXdkOTI5QUMzUnV2ckVUNHdPUE9ZS3c9PTwvUT48RFA+cHVrSUpkTHhWa1AxMDIvQ2RBNldZU0Vud21aOG41N3lzU0h3VzloSmJLcVU5MDY2NWUvQXl1UnNrYXdmQkVkQUdNM1Q2YUFLcXB5MGVCK3NyWktVdHc9PTwvRFA+PERRPjJQdE9RYnVKclcrZ3hBVDk4SWpVZWorNDlkOHlDZUcwMWNKRUU3aENDL1BlK3BTS1V0WnNDZlZXZHhVaGVoV1NxdC9HSkNvNGdtWlMzL2xKdE95a3B3PT08L0RRPjxJbnZlcnNlUT5CZmFhc2dhTHNiUXlhV1RGRUtDVER0ZlUrYVdwd2hLVFB3VVpjTG9RTjBqOEFsRlo5aGtkZWNGdzdXanhxb1V2YlFwNmRJR09MdE9ZcVoydlN6UVVzZz09PC9JbnZlcnNlUT48RD5xWXRlWmgzR0NpUVBydjEyYkFtOEg2elp4WFBxVlpiMlZ2WUsrdDNNRWxJdVlnMWZXYUhmQ3FUeklKd2ZUc2twWGllTjlXWUIxRVhuREc4MmtLWDl0WngzZXQxcFFTSGNmZDRwek9OMWpKQ21tbTJPSzZZRmVUQ1ArSEVURDBja0hQcGhLaXRVazcvbGZCSXB4alhmSDFmZGhpci9EYjFwd1pxM001ejAzUlU9PC9EPjwvUlNBS2V5VmFsdWU+"));

        // 公钥加密
        using RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider();
        rsa1.FromXmlString(publicKey);

        var dataBytes = Encoding.UTF8.GetBytes("2024-08-08");
        byte[] encryptedData = rsa1.Encrypt(dataBytes, false);

        // 私钥解密
        using RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider();
        rsa2.FromXmlString(privateKey);
        var decryptedData = rsa2.Decrypt(encryptedData, false);
        var data = Encoding.UTF8.GetString(decryptedData);
    }
}
