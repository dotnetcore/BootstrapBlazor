// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
