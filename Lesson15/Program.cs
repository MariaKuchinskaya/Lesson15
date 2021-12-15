using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Lesson15
{
    class Program
    {
        private static List<string> FilesFound { get; } = new List<string>();
        static void Main(string[] args)
        {
            try
            {
                CreateFile();

                FindFile(@"C:\Mary");

                if (FilesFound.Count == 0)
                {
                    throw new Exception("File was not found");
                }
                else
                {
                    ZipFile.CreateFromDirectory(@"C:\Mary\test.txt", @"C:\Mary\text.zip");
                    Console.WriteLine(@"New text path: {C:\Mary\text.zip}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void FindFile(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists)
            {
                GetFileInDirectory(path);
            }

        }

        private static void GetFileInDirectory(string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    using var streamReader = new StreamReader(file);
                    var contents = streamReader.ReadToEnd().ToLower();

                    if (contents.Contains("7866fgvygftgybuhnuygbb"))
                    {
                        FilesFound.Add(file);
                    }
                }

                GetFileInDirectory(directory);
            }
        }

        private static void CreateFile()
        {
            string path = @"C:\Mary";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string subFolderRelativePath = @"SubMary";
            DirectoryInfo subFolder = dirInfo.CreateSubdirectory(subFolderRelativePath);

            using FileStream fstream = new FileStream($"{subFolder.FullName}\\test.txt", FileMode.OpenOrCreate);
            byte[] array = System.Text.Encoding.Default.GetBytes("7866fgvygftgybuhnuygbb");
            fstream.Write(array, 0, array.Length);
            
        }
        
    }
}


