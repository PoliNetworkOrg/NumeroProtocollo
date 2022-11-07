using Newtonsoft.Json;

namespace NumerazioneProtocollo.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]

    internal class Rif<T>
    {
        public T? Obj;

        public Rif() { 
            Obj = default(T);
        }
    }
}
