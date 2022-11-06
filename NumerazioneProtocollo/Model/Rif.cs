using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]

    internal class Rif<T>
    {
        public T? obj;

        public Rif() { 
            obj = default(T);
        }
    }
}
