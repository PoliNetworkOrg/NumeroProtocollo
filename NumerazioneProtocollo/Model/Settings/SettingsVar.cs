using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Model.Path
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    internal class SettingsVar
    {
        public string? DirPath;
    }
}
