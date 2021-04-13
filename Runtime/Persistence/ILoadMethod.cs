using UnityEngine;

namespace SchloooLib.Persistence
{
    public interface ILoadMethod
    {
        T Deserialize<T>(SaveFile saveFile) where T : Object;
    }
}