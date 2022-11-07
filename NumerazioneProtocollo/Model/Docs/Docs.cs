using Newtonsoft.Json;
using NumerazioneProtocollo.Data;

namespace NumerazioneProtocollo.Model.Docs;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
internal class Docs
{
    public List<Document>? documents = new();

    internal static Document? Get(DataGridViewRow rowAdded, DataGridView dataGridView_doc)
    {
        GlobalVariables.docs ??= new Rif<Docs>();
        GlobalVariables.docs.Obj ??= new Docs();
        GlobalVariables.docs.Obj.documents ??= new List<Document>();

        var id = Document.GetId(rowAdded, dataGridView_doc);
        var category = Document.GetCategory(rowAdded, dataGridView_doc);
        if (id == null || category == null)
            return null;

        for (var i = 0; i < GlobalVariables.docs.Obj.documents.Count; i++)
        {
            var x = GlobalVariables.docs.Obj.documents[i];
            if (x == null)
                continue;

            if (x.id == id && x.category == category)
                return x;
        }

        return null;
    }

    internal void Delete(int value)
    {
        var indexList = GetIndexList(value);
        if (indexList == null) return;
        documents ??= new List<Document>();
        documents.RemoveAt(indexList.Value);
    }

    private int? GetIndexList(int id)
    {
        documents ??= new List<Document>();
        for (var i = 0; i < documents.Count; i++)
        {
            var doc = documents[i];
            if (doc == null) continue;
            if (doc.id == id)
                return i;
        }

        return null;
    }

    internal void HandleEdit(Document doc)
    {
        documents ??= new List<Document>();

        var isPresent = GetIfPresent(doc);
        if (isPresent.Item1)
        {
            documents[isPresent.Item2] = doc;
        }
        else
        {
            if (!string.IsNullOrEmpty(doc.fileName))
                documents.Add(doc);
        }
    }

    private Tuple<bool, int> GetIfPresent(Document doc)
    {
        documents ??= new List<Document>();

        for (var i = 0; i < documents.Count; i++)
        {
            var document = documents[i];
            if (doc.id == document.id && doc.category == document.category) return new Tuple<bool, int>(true, i);
        }

        return new Tuple<bool, int>(false, -1);
    }
}