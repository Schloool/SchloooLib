using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SchloooLib.Persistence
{
    public class BinarySaver : ISaveMethod
    {
        public void Serialize(object saveObject, SaveFile saveFile)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = saveFile.OpenSaveFileWriteStream();
            formatter.Serialize(fileStream, saveObject);
            
            fileStream.Close();
        }
    }
}