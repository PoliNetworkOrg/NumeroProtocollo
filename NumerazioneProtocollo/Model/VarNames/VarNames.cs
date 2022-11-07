using System.Data;
using NumerazioneProtocollo.Data;
using NumerazioneProtocollo.Model.Docs;

namespace NumerazioneProtocollo.Model.VarNames;

internal class VarNames
{
    private readonly Func<object?, Document, object?> _handleId1;
    private readonly string v;


    private VarNames(string v, Func<object?, Document, object?> handleId1)
    {
        this.v = v;
        _handleId1 = handleId1;
    }


    internal static IEnumerable<VarNames> GenerateListDocumentsHead()
    {
        List<VarNames> varNames = new()
        {
            new VarNames(Constants.DocId, Document.HandleId),
            new VarNames(Constants.CategoryId, Document.HandleCategoryId),
            new VarNames("Category Name", Document.HandleCategoryName),
            new VarNames("File name", Document.HandleFileName),
            new VarNames("File path", Document.HandleFilePath),
            new VarNames("Creation date", Document.HandleCreationDate),
            new VarNames("Year", Document.HandleYear)
        };
        return varNames;
    }


    internal string GetName()
    {
        return v;
    }


    internal object? GetValue(Document document)
    {
        var x = _handleId1(null, document);
        return x;
    }

    internal void UpdateDocumentFromHeadAndDataRow(DataGridViewRow rowAdded, Document document,
        DataGridView dataGridView_doc)
    {
        var findString = FindHeadString(dataGridView_doc);
        if (findString != null) _handleId1(rowAdded.Cells[findString.Value].Value, document);
    }

    private int? FindHeadString(DataGridView dataGridView_doc)
    {
        return FindHeadStringWithHeader(dataGridView_doc, v);
    }

    public static int? FindHeadStringWithHeader(DataGridView dataGridView_doc, string v)
    {
        for (var i = 0; i < dataGridView_doc.Columns.Count; i++)
        {
            var column = dataGridView_doc.Columns[i];
            if (column.Name == v)
                return i;
        }

        return null;
    }

    internal void UpdateDocumentFromHeadAndDataRow(DataRow rowAdded, Document document)
    {
        _handleId1(rowAdded[v], document);
    }
}