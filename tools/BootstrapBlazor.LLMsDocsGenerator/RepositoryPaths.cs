// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.LLMsDocsGenerator;

internal static class RepositoryPaths
{
    public static string? FindRepoRoot(string path)
    {
        var fullPath = Path.GetFullPath(path);
        var directory = Directory.Exists(fullPath)
            ? new DirectoryInfo(fullPath)
            : new FileInfo(fullPath).Directory;

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "skill-index.json")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        return null;
    }

    public static string ToRepoPath(string repoRoot, string fullPath)
    {
        return Path.GetRelativePath(repoRoot, fullPath).Replace('\\', '/');
    }

    public static string FromRepoPath(string repoRoot, string repoPath)
    {
        return Path.GetFullPath(Path.Combine(repoRoot, repoPath.Replace('/', Path.DirectorySeparatorChar)));
    }
}
