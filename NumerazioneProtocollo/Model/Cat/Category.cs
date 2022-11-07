using Newtonsoft.Json;

namespace NumerazioneProtocollo.Model.Cat;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
internal class Category
{
    public DateTime? creationDate;
    public string? Description;
    public int? Id;
    public string? Name;

    public override string ToString()
    {
        var s = "";
        if (Id != null) s += Id + " | ";

        if (!string.IsNullOrEmpty(Name)) s += Name + " | ";

        if (!string.IsNullOrEmpty(Description)) s += Description;

        return s;
    }
}