using System;
using System.IO;
using static System.Reflection.Metadata.BlobBuilder;

public class LoggerFile
{
    public string LoggerFilePath { get; private set; } = @"C:\Users\kharc\source\repos\1\ConsoleApp1\Logger.txt";

    public LoggerFile() {}

    public string LoggerFileInitial(string loggerFilePath)
    {
        return LoggerFilePath = loggerFilePath;
    }

    public void LogError(Exception ex, string message = "")
    {
        var logMessage = $"{DateTime.Now}: {message} {Environment.NewLine}Exception: {ex.Message}{Environment.NewLine}StackTrace: {ex.StackTrace}{Environment.NewLine}";
        File.AppendAllText(LoggerFilePath, logMessage);
    }
}
