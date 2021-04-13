using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SchloooLib.Persistence;
using UnityEngine;

namespace SchloooLib.Temporal
{
    public class BinaryLoader : ILoadMethod
    {
        public T Deserialize<T>(SaveFile saveFile) where T : Object
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = saveFile.OpenSaveFileReadStream();
            
            T saveData = binaryFormatter.Deserialize(fileStream) as T;
            fileStream.Close();
            return saveData;
        }
    }
}