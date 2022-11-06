using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Model.Cat
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]

    internal class Category
    {
        public int? Id;
        public string? Name;
        public string? Description; 
        public DateTime? creationDate;

        public override string ToString()
        {
            string s = "";
            if (this.Id != null)
            {
                s += this.Id + " | ";
            }

            if (!string.IsNullOrEmpty(this.Name))
            {
                s += this.Name + " | ";
            }

            if (!string.IsNullOrEmpty(this.Description))
            {
                s += this.Description;
            }

            return s;
        }
    }
}
