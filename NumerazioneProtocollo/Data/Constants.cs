using Newtonsoft.Json;

namespace NumerazioneProtocollo.Data;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
internal class Constants
{
    public const string PathDocs = "prot_docs.json";
    public const string PathCategories = "prot_categories.json";
    public const string PathOfSettings = "settings.json";

    public const string DocId = "Id";
    public const string CategoryId = "Category ID";

    internal static string GetPathCategories()
    {
        var x = GlobalVariables.paths?.Obj?.DirPath;
        if (string.IsNullOrEmpty(x))
            return Constants.PathCategories;
        
        return x +  "/" + Constants.PathCategories;
    }

    internal static string GetPathDocuments()
    {
        var x = GlobalVariables.paths?.Obj?.DirPath;
        if (string.IsNullOrEmpty(x))
            return Constants.PathDocs;

        return x + "/" + Constants.PathDocs;
    }
}