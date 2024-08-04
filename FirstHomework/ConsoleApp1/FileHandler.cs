using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

public class FileHandler : LoggerFile
{
    public string _loadFilePath { get; private set; } = @"C:\Users\kharc\source\repos\1\ConsoleApp1\books.txt";
    public string _saveFilePath { get; private set; } = @"C:\Users\kharc\source\repos\1\ConsoleApp1\data_books.txt";

    public FileHandler()
    {
        string _loadFilePath;
        string _saveFilePath;
    }

    public FileHandler(string loadFilePath, string saveFilePath)
    {
        _loadFilePath = loadFilePath;
        _saveFilePath = saveFilePath;
    }

    public async Task<T> ReadJsonAsync<T>(string _loadFilePath)
    {
        return JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(_loadFilePath));
    }

    public async Task SaveJsonAsync<T>(string _loadFilePath, T content)
    {
        await File.WriteAllTextAsync(_saveFilePath, JsonConvert.SerializeObject(content));
    }
}
