using Task = Microsoft.Build.Utilities.Task;

namespace CreateJs;

public class CreateJs : Task
{

    public string FilePath { get; set; }

    public override bool Execute()
    {
        if (!File.Exists(FilePath))
        {
            Log.LogError("File not found: {0}", FilePath);
            return false;
        }
        var content = File.ReadAllText(FilePath);
        content = content.Replace("$version", "bar");
        var path = $"{Path.GetDirectoryName(FilePath)}\\{Path.GetFileNameWithoutExtension(FilePath)}.min{Path.GetExtension(FilePath)}";
        File.WriteAllText(path, content);
        Log.LogMessage(path);
        return true;
    }
}
