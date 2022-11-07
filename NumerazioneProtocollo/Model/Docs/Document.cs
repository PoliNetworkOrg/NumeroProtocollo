using Newtonsoft.Json;
using NumerazioneProtocollo.Data;
using NumerazioneProtocollo.Model.Cat;

namespace NumerazioneProtocollo.Model.Docs;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
internal class Document
{
     public int? category;
    public DateTime? creationDate;
    public string? fileName;
    public string? filePath;
    public int? id;
    public int? year;


    private static object? GetValue(DataGridViewRow rowAdded, string docId, DataGridView dataGridView_doc)
    {
        var id = VarNames.VarNames.FindHeadStringWithHeader(dataGridView_doc, docId);
        if (id == null)
            return null;

        var x = rowAdded.Cells[id.Value];

        return x;
    }

    internal static int? GetId(DataGridViewRow rowAdded, DataGridView dataGridView_doc)
    {
        return GetValueFromHeader(rowAdded, dataGridView_doc, Constants.DocId);
    }

    internal static int? GetCategory(DataGridViewRow rowAdded, DataGridView dataGridView_doc)
    {
        return GetValueFromHeader(rowAdded, dataGridView_doc, Constants.CategoryId);
    }


    private static int? GetValueFromHeader(DataGridViewRow rowAdded, DataGridView dataGridViewDoc, string docId)
    {
        var id = GetValue(rowAdded, docId, dataGridViewDoc);
        switch (id)
        {
            case null:
                return null;
            case int idInt:
                return idInt;
            default:
                try
                {
                    return Convert.ToInt32(id);
                }
                catch
                {
                    // ignored
                }

                return null;
        }
    }


    public static object? HandleId(object? idParam, Document document)
    {
        switch (idParam)
        {
            case null:
                return document.id;
            case int idParamInt:
                document.id = idParamInt;
                return document.id;
        }

        var value = TryGetValueInt(idParam);
        if (value == null)
            return document.id;

        document.id = value.Value;
        return document.id;
    }


    private static int? TryGetValueInt(object? idParam)
    {
        if (idParam == null)
            return null;


        try
        {
            return Convert.ToInt32(idParam);
        }
        catch
        {
            // ignored
        }

        return null;
    }

    private static DateTime? TryGetValueDateTime(object? idParam)
    {
        if (idParam == null)
            return null;


        try
        {
            return Convert.ToDateTime(idParam);
        }
        catch
        {
            // ignored
        }

        return null;
    }

    internal static object? HandleCategoryId(object? arg1, Document document)
    {
        switch (arg1)
        {
            case null:
                return document.category;
            case int idParamInt:
                document.category = idParamInt;
                return document.category;
        }

        var value = TryGetValueInt(arg1);
        if (value == null)
            return document.category;

        document.category = value.Value;
        return document.category;
    }

    internal static object? HandleCategoryName(object? arg1, Document document)
    {
        return Categories.GetCategoryName(document.category);
    }

    internal static object? HandleFileName(object? arg1, Document document)
    {
        switch (arg1)
        {
            case null:
                return document.fileName;
            case string idParamInt:
                document.fileName = idParamInt;
                return document.fileName;
        }

        var value = arg1.ToString();
        if (value == null)
            return document.fileName;

        document.fileName = value;
        return document.fileName;
    }

    internal static object? HandleFilePath(object? arg1, Document document)
    {
        switch (arg1)
        {
            case null:
                return document.filePath;
            case string idParamInt:
                document.filePath = idParamInt;
                return document.filePath;
        }

        var value = arg1.ToString();
        if (value == null)
            return document.filePath;

        document.filePath = value;
        return document.filePath;
    }

    internal static object? HandleCreationDate(object? arg1, Document document)
    {
        switch (arg1)
        {
            case null:
                return document.creationDate;
            case DateTime idParamInt:
                document.creationDate = idParamInt;
                return document.creationDate;
        }

        var value = TryGetValueDateTime(arg1);
        if (value == null)
            return document.creationDate;

        document.creationDate = value.Value;
        return document.creationDate;
    }

    internal static object? HandleYear(object? arg1, Document document)
    {
        switch (arg1)
        {
            case null:
                return document.year;
            case int idParamInt:
                document.year = idParamInt;
                return document.year;
        }

        var value = TryGetValueInt(arg1);
        if (value == null)
            return document.year;

        document.year = value.Value;
        return document.year;
    }

    internal static Document Get(DataGridViewRow rowAdded, DataGridView dataGridView_doc)
    {
        GlobalVariables.docs ??= new Rif<Docs>();
        GlobalVariables.docs.Obj ??= new Docs();
        GlobalVariables.docs.Obj.documents ??= new List<Document>();

        var doc = Docs.Get(rowAdded, dataGridView_doc);
        if (doc != null)
            return doc;


        Document document = new();
        foreach (var head in Model.VarNames.HeadList.HeadListVar) head.UpdateDocumentFromHeadAndDataRow(rowAdded, document, dataGridView_doc);


        return document;
    }
}