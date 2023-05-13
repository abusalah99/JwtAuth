namespace jwtauth;

public class PythonScriptExcutor : IPythonScriptExcutor
{
    public async Task<string> Excute(string rootPath, string scriptName,
        string audioFile, string model)
    {
        rootPath += @"\Services\PythonService\";
        string arguments = $"{rootPath + @"Scripts\" + scriptName} " +
            $"{audioFile} " + $"{model}";

        ProcessStartInfo start = new ()
        {
            FileName = rootPath + @"Interpreter\python.exe",
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
        };
        using (Process process = await Task.Run(() => Process.Start(start)))
        {
            StreamReader reader = process.StandardOutput;
            var result = reader.ReadToEnd().Trim();
            Console.WriteLine(result);
            return result;
        }
    }
}
