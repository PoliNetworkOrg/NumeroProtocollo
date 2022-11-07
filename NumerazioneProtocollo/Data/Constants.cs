using Newtonsoft.Json;

namespace NumerazioneProtocollo.Data;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
internal class Constants
{
    public const string PathDocs = "prot_docs.json";
    public const string PathCategories = "prot_categories.json";
    public const string PathOfPaths = "paths.json";

    public const string DocId = "Id";
    public const string CategoryId = "Category ID";

    internal static string GetPathCategories()
    {
        var x = GlobalVariables.paths?.Obj?.PathCategories;
        return string.IsNullOrEmpty(x) ? Constants.PathCategories : x;
    }

    internal static string GetPathDocuments()
    {
        var x = GlobalVariables.paths?.Obj?.PathDocs;
        return string.IsNullOrEmpty(x) ? Constants.PathDocs : x;
    }
}