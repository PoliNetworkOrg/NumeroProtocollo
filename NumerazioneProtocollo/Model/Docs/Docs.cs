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
        public List<Document>? documents = new List<Document>();

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
                this.documents.Add(doc);
            }
        }

        private Tuple<bool, int> GetIfPresent(Document doc)
        {
            this.documents ??= new List<Document>();

            for (int i = 0; i < documents.Count; i++)
            {
                var document = documents[i];
                if (doc.id == document.id)
                {
                    return new Tuple<bool, int>(true, i);
                }
            }

            return new Tuple<bool, int>(false, -1);
        }
    }
}
