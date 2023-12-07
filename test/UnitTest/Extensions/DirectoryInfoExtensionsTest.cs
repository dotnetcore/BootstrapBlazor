// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
        sourceDirInfo.Copy(destDir);
        Assert.True(Directory.Exists(destDir));
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
