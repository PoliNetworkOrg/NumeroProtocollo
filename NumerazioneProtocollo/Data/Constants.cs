using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Data
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]

    internal class Constants
    {
        public const string PathDocs = "prot_docs.json";
        public const string PathCategories = "prot_categories.json";
        public const string DocId = "Id";
    }
}
