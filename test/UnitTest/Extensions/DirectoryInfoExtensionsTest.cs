// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

public class DirectoryInfoExtensionsTest
{
    [Fact]
    public void Copy_Ok()
    {
        var rootDir = Path.Combine(AppContext.BaseDirectory, "test");
        if (!Directory.Exists(rootDir))
        {
            Directory.CreateDirectory(rootDir);
        }


        // 创建 SourceDir
        var sourceDir = CreateDir(Path.Combine(rootDir, "test1"));
        // 创建临时测试目录
        CreateDir(Path.Combine(sourceDir, "test"));
        // 创建临时测试文件
        using var file = File.OpenWrite(Path.Combine(sourceDir, "test.log"));
        file.Close();

        var destDir = Path.Combine(rootDir, "test2");
        if (Directory.Exists(destDir))
        {
            Directory.Delete(destDir, true);
        }

        var sourceDirInfo = new DirectoryInfo(sourceDir);
        sourceDirInfo.Copy(destDir, true);
        Assert.True(Directory.Exists(destDir));

        // 测试源文件夹不存在的情况
        var sourceDirNotExists = new DirectoryInfo(Path.Combine(rootDir, "notexists"));
        var ex = Assert.Throws<DirectoryNotFoundException>(() => sourceDirNotExists.Copy(destDir, true));
        Assert.NotNull(ex);
    }

    private static string CreateDir(string dirName)
    {
        if (!Directory.Exists(dirName))
        {
            Directory.CreateDirectory(dirName);
        }
        return dirName;
    }
}
