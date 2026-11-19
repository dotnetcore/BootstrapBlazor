// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License

using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

if (args.Length == 2 && args[0] == "--clean")
{
    CleanGeneratedExtensions(args[1]);
    return 0;
}

if (args.Length != 5)
{
    Console.Error.WriteLine("Usage: dotnet run --file GenerateLlmsDocs.cs -- <project.assets.json> <target framework> <core docs directory> <working directory> <publish directory>");
    Console.Error.WriteLine("       dotnet run --file GenerateLlmsDocs.cs -- --clean <output directory>");
    return 1;
}

var core = Path.GetFullPath(args[2]);
if (!File.Exists(Path.Combine(core, "llms.txt")))
{
    Console.Error.WriteLine($"BootstrapBlazor documentation index not found: {Path.Combine(core, "llms.txt")}. Check that the preceding 'dotnet llms-docs' command completed successfully and used this output directory.");
    return 1;
}

using var assets = JsonDocument.Parse(File.ReadAllText(args[0]));
var root = assets.RootElement;
var target = root.GetProperty("targets").GetProperty(args[1]);
var libraries = root.GetProperty("libraries");
var packageFolders = root.GetProperty("packageFolders").EnumerateObject().Select(folder => folder.Name).ToArray();
var output = Path.GetFullPath(args[3]);
var publish = Path.GetFullPath(args[4]);
CopyDocs(core, output);
var indexPath = Path.Combine(output, "llms.txt");
var index = new StringBuilder(File.ReadAllText(indexPath));
CleanGeneratedExtensions(output);
index.AppendLine().AppendLine("## BootstrapBlazor Extensions");
var linkPattern = new Regex(@"^- \[[^\]]+\]\(components/([^/\\)]+\.txt)\)(?:\s*-\s*.*)?$");

foreach (var package in target.EnumerateObject().OrderBy(package => package.Name, StringComparer.OrdinalIgnoreCase))
{
    var packageName = package.Name.Split('/');
    if (packageName.Length != 2 || !packageName[0].StartsWith("BootstrapBlazor.", StringComparison.OrdinalIgnoreCase)
        || !libraries.TryGetProperty(package.Name, out var library)
        || library.GetProperty("type").GetString() != "package")
    {
        continue;
    }

    var packagePath = library.GetProperty("path").GetString()!;
    var directory = packageFolders.Select(folder => Path.Combine(folder, packagePath))
        .FirstOrDefault(Directory.Exists)
        ?? throw new DirectoryNotFoundException($"Restored package directory not found for {package.Name}. Run 'dotnet restore' for the server project and retry.");
    var files = library.GetProperty("files").EnumerateArray().Select(file => file.GetString()!).ToArray();
    var indexFiles = files.Where(file => Regex.IsMatch(file, @"^skills/[^/]+/references/llms\.txt$"))
        .OrderBy(file => file, StringComparer.Ordinal).ToList();
    if (indexFiles.Count == 0)
    {
        Console.Error.WriteLine($"No LLMs docs in {package.Name}");
        continue;
    }

    var relativeDirectory = $"extensions/{packageName[0]}";
    var destination = Path.Combine(output, "extensions", packageName[0]);
    var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    index.AppendLine().AppendLine($"### {packageName[0]}").AppendLine();
    foreach (var indexFile in indexFiles)
    {
        var source = Path.Combine(directory, indexFile.Replace('/', Path.DirectorySeparatorChar));
        var referenceDirectory = Path.GetDirectoryName(source)!;
        foreach (var line in File.ReadLines(source))
        {
            var match = linkPattern.Match(line);
            if (!match.Success)
            {
                continue;
            }

            var name = match.Groups[1].Value;
            if (!seen.Add(name))
            {
                continue;
            }
            var document = Path.Combine(referenceDirectory, "components", name);
            if (!File.Exists(document))
            {
                throw new FileNotFoundException($"LLMs document referenced by '{source}' was not found in {package.Name}. The package documentation is incomplete.", document);
            }
            Directory.CreateDirectory(destination);
            var destinationFile = Path.Combine(destination, name);
            if (File.Exists(destinationFile))
            {
                throw new IOException($"Cannot copy LLMs documentation from {package.Name}: the destination file already exists and will not be overwritten: {destinationFile}");
            }
            File.Copy(document, destinationFile);
            index.AppendLine(line.Replace($"(components/{name})", $"({relativeDirectory}/{name})"));
        }
    }
}

File.WriteAllText(indexPath, index.ToString(), new UTF8Encoding(false));
CleanGeneratedExtensions(publish);
CopyDocs(output, publish);
Console.WriteLine($"Merged LLMs documentation published to {Path.Combine(publish, "llms.txt")}");
return 0;

static void CopyDocs(string source, string destination)
{
    var options = new EnumerationOptions
    {
        RecurseSubdirectories = true,
        AttributesToSkip = FileAttributes.ReparsePoint
    };
    foreach (var file in Directory.EnumerateFiles(source, "*.txt", options))
    {
        var target = Path.Combine(destination, Path.GetRelativePath(source, file));
        Directory.CreateDirectory(Path.GetDirectoryName(target)!);
        File.Copy(file, target, overwrite: true);
    }
}

static void CleanGeneratedExtensions(string output)
{
    var extensions = Path.Combine(output, "extensions");
    if (!Directory.Exists(extensions))
    {
        return;
    }

    var options = new EnumerationOptions
    {
        RecurseSubdirectories = true,
        AttributesToSkip = FileAttributes.ReparsePoint
    };
    foreach (var file in Directory.EnumerateFiles(extensions, "*.txt", options))
    {
        if (!File.ReadAllText(file).Contains("<!-- Generated:", StringComparison.Ordinal))
        {
            continue;
        }

        File.Delete(file);
        File.Delete(file + ".br");
        File.Delete(file + ".gz");
    }

    foreach (var directory in Directory.EnumerateDirectories(extensions, "*", options).OrderByDescending(path => path.Length))
    {
        if (!Directory.EnumerateFileSystemEntries(directory).Any())
        {
            Directory.Delete(directory);
        }
    }
}
