using Newtonsoft.Json;
using NumerazioneProtocollo.Model;
using NumerazioneProtocollo.Model.Cat;
using NumerazioneProtocollo.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Data
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]

    internal class GlobalVariables
    {
        internal static Rif<Docs>? docs;
        internal static Rif<Categories>? categories;
    }
}
