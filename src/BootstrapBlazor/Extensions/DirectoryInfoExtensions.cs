// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DirectoryInfo 扩展方法</para>
/// <para lang="en">DirectoryInfo extensions method</para>
/// </summary>
public static class DirectoryInfoExtensions
{
    /// <summary>
    /// <para lang="zh">将当前目录的内容复制到指定的目标目录</para>
    /// <para lang="en">Copies the contents of the current directory to a specified destination directory</para>
    /// </summary>
    /// <param name="dir">
    ///   <para lang="zh">源目录</para>
    ///   <para lang="en">The source directory to copy from.</para>
    /// </param>
    /// <param name="destinationDir">
    ///   <para lang="zh">目标目录路径</para>
    ///   <para lang="en">The path of the destination directory where the contents will be copied.</para>
    /// </param>
    /// <param name="recursive">
    ///   <para lang="zh">是否递归复制子目录及其内容</para>
    ///   <para lang="en"><see langword="true"/> to copy all subdirectories and their contents recursively; otherwise, <see langword="false"/>.</para>
    /// </param>
    /// <param name="overwrite">
    ///   <para lang="zh">是否覆盖目标文件</para>
    ///   <para lang="en"><see langword="true"/> to overwrite existing files in the destination directory; otherwise, <see langword="false"/>.</para>
    /// </param>
    public static void Copy(this DirectoryInfo dir, string destinationDir, bool recursive = true, bool overwrite = true)
    {
        // Check if the source directory exists
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");
        }

        // Create the destination directory
        Directory.CreateDirectory(destinationDir);

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);

            // 是否覆盖已存在的文件
            file.CopyTo(targetFilePath, overwrite);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            // Cache directories before we start copying
            var dirs = dir.GetDirectories();

            foreach (var subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                Copy(subDir, newDestinationDir, true);
            }
        }
    }
}
