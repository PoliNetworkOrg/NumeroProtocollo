using Newtonsoft.Json.Linq;
using NumerazioneProtocollo.Model;
using NumerazioneProtocollo.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerazioneProtocollo.Utils
{
    internal static class Files
    {
        internal static void LoadFile<T>(Rif<T>? rif, string path)
        {
            if (!File.Exists(path))
            {
                var obj = new JObject();
                File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(obj));
            }

            var jsonRead = File.ReadAllText(path);
            var objRead = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonRead);
            if (rif != null)
            {
                rif.obj = objRead;
            }
        }

        internal static void SaveFile<T>(Rif<T>? rif, string path)
        {
            if (rif == null)
                return;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(rif.obj);
            File.WriteAllText(path, json);
        }
    }
}
