// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">DirectoryInfo 扩展方法</para>
///  <para lang="en">DirectoryInfo 扩展方法</para>
/// </summary>
public static class DirectoryInfoExtensions
{
    /// <summary>
    ///  <para lang="zh">Copies the 内容s of the current directory to a specified destination directory.</para>
    ///  <para lang="en">Copies the contents of the current directory to a specified destination directory.</para>
    /// </summary>
    /// <remarks>This method creates the destination directory if it does not already exist. Files in the
    /// source directory are copied to the destination directory, and if <paramref name="recursive"/> is <see
    /// langword="true"/>, all subdirectories and their contents are also copied recursively.</remarks>
    /// <param name="dir">The source directory to copy from.</param>
    /// <param name="destinationDir">The path of the destination directory where the contents will be copied.</param>
    /// <param name="recursive"><see langword="true"/> to copy all subdirectories and their contents recursively; otherwise, <see
    /// langword="false"/>.</param>
    /// <exception cref="DirectoryNotFoundException">Thrown if the source directory specified by <paramref name="dir"/> does not exist.</exception>
    public static void Copy(this DirectoryInfo dir, string destinationDir, bool recursive = true)
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
            file.CopyTo(targetFilePath);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                Copy(subDir, newDestinationDir, true);
            }
        }
    }
}
