using NumerazioneProtocollo.Model.Docs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Model.VarNames
{
    internal class VarNames
    {
        private string v;

        private Func<object?, Document, object?> handleId1;


        public VarNames(string v, Func<object?, Document, object?> handleId1)
        {
            this.v = v;
            this.handleId1 = handleId1;
        }



        internal static IEnumerable<Model.VarNames.VarNames> GenerateListDocumentsHead()
        {
            List<VarNames> varNames = new()
            {
                new VarNames(Data.Constants.DocId, Model.Docs.Document.HandleId),
                new VarNames(Data.Constants.CategoryId, Model.Docs.Document.HandleCategoryId),
                new VarNames("Category Name", Model.Docs.Document.HandleCategoryName),
                new VarNames("File name", Model.Docs.Document.HandleFileName),
                new VarNames("Creation date", Model.Docs.Document.HandleCreationDate),
                new VarNames("Year", Model.Docs.Document.HandleYear)
            };
            return varNames;
        }

   
        internal string GetName()
        {
            return this.v;
        }

     

        internal object? GetValue(Document document)
        {
            var x = this.handleId1(null, document);
            return x;
        }

        internal void UpdateDocumentFromHeadAndDataRow(DataGridViewRow rowAdded, Document document, DataGridView dataGridView_doc)
        {
            var findString = findHeadString(dataGridView_doc);
            if (findString != null)
            {
                this.handleId1(rowAdded.Cells[findString.Value].Value, document);
            }
        }

        private int? findHeadString(DataGridView dataGridView_doc)
        {
            return findHeadStringWithHeader(dataGridView_doc, this.v);
        }

        public static int? findHeadStringWithHeader(DataGridView dataGridView_doc, string v)
        {
            for (int i = 0; i < dataGridView_doc.Columns.Count; i++)
            {
                var column = dataGridView_doc.Columns[i];
                if (column.Name == v)
                    return i;
            }
            return null;
        }

        internal void UpdateDocumentFromHeadAndDataRow(DataRow rowAdded, Document document)
        {
            this.handleId1(rowAdded[this.v], document);
        }

    }
}
