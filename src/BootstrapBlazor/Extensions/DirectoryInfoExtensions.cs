// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DirectoryInfo 扩展方法
/// </summary>
public static class DirectoryInfoExtensions
{
    /// <summary>
    /// 文件夹拷贝方法
    /// </summary>
    /// <param name="sourceDirInfo"></param>
    /// <param name="destDirName"></param>
    public static void Copy(this DirectoryInfo sourceDirInfo, string destDirName)
    {
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // CopyFile
        foreach (var info in sourceDirInfo.EnumerateFileSystemInfos())
        {
            if (info is FileInfo fi)
            {
                var targetFileName = Path.Combine(destDirName, info.Name);
                fi.CopyTo(targetFileName, true);
            }
            else if (info is DirectoryInfo di)
            {
                var targetFolderName = Path.Combine(destDirName, di.Name);
                Copy(di, targetFolderName);
            }
        }
    }
}
