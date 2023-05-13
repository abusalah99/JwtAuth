namespace jwtauth;

public interface IPythonScriptExcutor
{
    Task<string> Excute(string rootPath, string scriptName, string audioFile , string model);
}
