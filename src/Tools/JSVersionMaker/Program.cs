// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using System.Xml;

namespace JSVersionMaker;

class Program
{
    const string CONFIG_FILENAME = "versionconfig.json";

    static void Main(params string[] args)
    {
#if DEBUG
        //if (args.Length == 0)
        //{
        //    args = new string[] { "D:\\Argo\\src\\BootstrapBlazor\\src\\BootstrapBlazor\\BootstrapBlazor.csproj" };
        //}
#endif

        if (args.Length == 0)
        {
            Console.WriteLine("Do not provider project path");
            return;
        }

        ProcessFiles(args[0]);
    }

    static void ProcessFiles(string projectFileName)
    {
        var version = GetVersion(projectFileName);
        var projectPath = Path.GetDirectoryName(projectFileName);
        if (projectPath != null && Directory.Exists(projectPath))
        {
            var excludeFiles = GetExcludeFiles(projectPath);
            foreach (var file in Directory.EnumerateFiles(projectPath, "*.js", SearchOption.AllDirectories))
            {
                try
                {
                    if (ExcludeFiles(excludeFiles, file))
                    {
                        continue;
                    }
                    var content = File.ReadAllText(file, System.Text.Encoding.UTF8);
                    if (content.Contains("?v=$version"))
                    {
                        content = content.Replace("?v=$version", $"?v={version}");
                        File.WriteAllText(file, content, System.Text.Encoding.UTF8);
                        Console.WriteLine($"Version: {file} -- ver: {version}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Version: {file} -- ver: {version} -- failed: {ex.Message}");
                }
            }
        }
    }

    static bool ExcludeFiles(List<string> files, string file)
    {
        return files.Any(f => f.EndsWith("*.*") ? FolderCheck(f) : FileCheck(f));

        bool FolderCheck(string excludeFile) => file.StartsWith(excludeFile.TrimEnd('*', '.'), StringComparison.OrdinalIgnoreCase);

        bool FileCheck(string excludeFile) => file.Equals(excludeFile, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 通过项目文件获得版本号信息
    /// </summary>
    /// <param name="projectFile"></param>
    /// <returns></returns>
    static string GetVersion(string projectFile)
    {
        string? ver = null;
        if (File.Exists(projectFile))
        {
            try
            {
                var doc = new System.Xml.XmlDocument();
                doc.Load(projectFile);
                var root = doc.DocumentElement;
                if (root != null)
                {
                    foreach (XmlNode node in root.ChildNodes)
                    {
                        if (node.Name == "PropertyGroup")
                        {
                            foreach (XmlNode item in node.ChildNodes)
                            {
                                if (item.Name == "Version")
                                {
                                    ver = item.InnerText;
                                    break;
                                }
                            }
                        }
                        if (ver != null)
                        {
                            break;
                        }
                    }
                }
            }
            catch { }
        }
        return ver ?? "7.0.0.0";
    }

    static List<string> GetExcludeFiles(string projectPath)
    {
        var files = new List<string>();
        var configFileName = Path.Combine(projectPath, CONFIG_FILENAME);
        if (File.Exists(configFileName))
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(configFileName);
            var config = builder.Build();
            var section = config.GetSection("exclude");
            if (section.Exists())
            {
                var v = section.Get<List<string>>();
                if (v != null)
                {
                    if (OperatingSystem.IsWindows())
                    {
                        files.AddRange(v.Select(f => Path.Combine(projectPath, f.Replace("/", "\\"))));
                    }
                    else
                    {
                        files.AddRange(v.Select(f => Path.Combine(projectPath, f.Replace("\\", "/"))));
                    }
                }
            }
        }
        return files;
    }
}
