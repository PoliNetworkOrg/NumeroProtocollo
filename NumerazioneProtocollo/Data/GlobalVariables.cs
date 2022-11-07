using Newtonsoft.Json;
using NumerazioneProtocollo.Model;
using NumerazioneProtocollo.Model.Cat;
using NumerazioneProtocollo.Model.Docs;

namespace NumerazioneProtocollo.Data;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
internal class GlobalVariables
{
    internal static Rif<Docs>? docs;
    internal static Rif<Categories>? categories;
}