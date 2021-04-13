using System;
using System.IO;
using UnityEngine;

namespace SchloooLib.Persistence
{
    public class SaveFile
    {
        public readonly string SaveFileName;
        public readonly string AbsoluteFilePath;

        private String directoryPath;

        public SaveFile(String absoluteDirectoryPath, String saveFileName, String fileExtension)
        {
            SaveFileName = $"{saveFileName}.{fileExtension}";
            AbsoluteFilePath = absoluteDirectoryPath + SaveFileName;
        }

        public SaveFile(String saveFileName, String fileExtension) : this(Application.persistentDataPath, saveFileName, fileExtension) { }

        public bool FileDirectoryExists()
        {
            return Directory.Exists(directoryPath);
        }

        public void CreateFileDirectory()
        {
            Directory.CreateDirectory(directoryPath);
        }

        public bool SaveFileExists()
        {
            return File.Exists(AbsoluteFilePath);
        }

        public void CreateSaveFile()
        {
            File.Create(AbsoluteFilePath).Close();
        }

        public FileStream OpenSaveFileReadStream()
        {
            return File.OpenRead(AbsoluteFilePath);
        }

        public FileStream OpenSaveFileWriteStream()
        {
            return File.OpenWrite(AbsoluteFilePath);
        }

        public StreamReader OpenSaveFileTextReadStream()
        {
            return File.OpenText(AbsoluteFilePath);
        }
    }
}