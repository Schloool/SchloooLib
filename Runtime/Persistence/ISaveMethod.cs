namespace SchloooLib.Persistence
{
    public interface ISaveMethod
    {
        void Serialize(object saveObject, SaveFile saveFile);
    }
}