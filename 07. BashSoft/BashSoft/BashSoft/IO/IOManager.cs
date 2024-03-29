﻿namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class IOManager
    {
        public static void TraverseDirectory(int depth)
        {
            var path = GetCurrentDirectoryPath();
            OutputWriter.WriteEmptyLine();
            var initialIdentation = path.Split(new []{ '\\' }, StringSplitOptions.RemoveEmptyEntries).Length;
            var subfolders = new Queue<string>();
            subfolders.Enqueue(path);

            while (subfolders.Count != 0)
            {
                //Dequeue the folder from the start of te queue
                var currentPath = subfolders.Dequeue();
                var identation = currentPath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Length - initialIdentation;

                if (depth - identation < 0)
                {
                    foreach (var folder in subfolders)
                    {
                        OutputWriter.WriteMessageOnNewLine($"{new string('-', identation)}{folder}");
                    }

                    break;
                }

                //Print the folder path
                OutputWriter.WriteMessageOnNewLine($"{new string('-', identation)}{currentPath}");

                try
                {
                    //Display files in directory
                    foreach (var file in Directory.GetFiles(currentPath))
                    {
                        var indexOfLastSlash = file.LastIndexOf('\\');
                        var filename = file.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + filename);
                    }

                    //Add all it's subfolders to the end of the queue
                    foreach (var directoryPath in Directory.GetDirectories(currentPath))
                    {
                        subfolders.Enqueue(directoryPath);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnauthorizedAccessException);
                }
            }
        }

        public static void CreateDirectoryInCurrentFolder(string name)
        {
            var path = GetCurrentDirectoryPath() + "\\" + name;

            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                OutputWriter.DisplayException(ExceptionMessages.ForbiddenSymbolsContainedInName);
            }
        }

        public static string GetCurrentDirectoryPath()
        {
            return SessionData.currentPath;
        }

        public static void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    var currenthPath = GetCurrentDirectoryPath();
                    var indexOfLastSlash = currenthPath.LastIndexOf('\\');
                    var newPath = currenthPath.Substring(0, indexOfLastSlash);

                    if (!newPath.Contains("\\"))
                    {
                        newPath += "\\";
                    }

                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToGoHigherInPartitionHierarchy);
                }
            }
            else
            {
                var currentPath = GetCurrentDirectoryPath();
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public static void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                return;
            }

            SessionData.currentPath = absolutePath;
        }
    }
}
