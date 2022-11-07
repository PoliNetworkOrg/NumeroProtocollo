using Newtonsoft.Json;

namespace NumerazioneProtocollo.Data;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
internal class Constants
{
    public const string PathDocs = "prot_docs.json";
    public const string PathCategories = "prot_categories.json";
    public const string DocId = "Id";
    public const string CategoryId = "Category ID";
}