using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Model.Docs
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]

    internal class Docs
    {
        public List<Document>? documents = new();

        internal static Document? Get(DataGridViewRow rowAdded, DataGridView dataGridView_doc)
        {
            Data.GlobalVariables.docs ??= new Rif<Docs>();
            Data.GlobalVariables.docs.obj ??= new Docs();
            Data.GlobalVariables.docs.obj.documents ??= new List<Document>();

            int? id = Model.Docs.Document.GetId(rowAdded, dataGridView_doc);
            int? category = Model.Docs.Document.GetCategory(rowAdded, dataGridView_doc);
            if (id == null || category == null)
                return null;

            for (int i=0; i< Data.GlobalVariables.docs.obj.documents.Count; i++)
            {
                var x = Data.GlobalVariables.docs.obj.documents[i];
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
            this.documents ??= new List<Document>();
            this.documents.RemoveAt(indexList.Value);
        }

        private int? GetIndexList(int id)
        {
            this.documents ??= new List<Document>();
            for (int i=0; i< this.documents.Count; i++)
            {
                var doc = this.documents[i];
                if (doc == null) continue;
                if (doc.id == id)
                    return i;
            }

            return null;
        }

        internal void HandleEdit(Document doc)
        {
            this.documents ??= new List<Document>(); 

            Tuple<bool, int> isPresent = GetIfPresent(doc);
            if (isPresent.Item1)
            {
                this.documents[isPresent.Item2] = doc;
            }
            else
            {
                if (!string.IsNullOrEmpty(doc.fileName))
                    this.documents.Add(doc);
            }
        }

        private Tuple<bool, int> GetIfPresent(Document doc)
        {
            this.documents ??= new List<Document>();

            for (int i = 0; i < documents.Count; i++)
            {
                var document = documents[i];
                if (doc.id == document.id && doc.category == document.category)
                {
                    return new Tuple<bool, int>(true, i);
                }
            }

            return new Tuple<bool, int>(false, -1);
        }
    }
}
