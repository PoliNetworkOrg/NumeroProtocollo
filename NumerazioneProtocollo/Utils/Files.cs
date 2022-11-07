﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NumerazioneProtocollo.Model;

namespace NumerazioneProtocollo.Utils;

internal static class Files
{
    internal static void LoadFile<T>(Rif<T>? rif, string path)
    {
        if (!File.Exists(path))
        {
            var obj = new JObject();
            File.WriteAllText(path, JsonConvert.SerializeObject(obj));
        }

        var jsonRead = File.ReadAllText(path);
        var objRead = JsonConvert.DeserializeObject<T>(jsonRead);
        if (rif != null) rif.Obj = objRead;
    }

    internal static void SaveFile<T>(Rif<T>? rif, string path)
    {
        if (rif == null)
            return;

        var json = JsonConvert.SerializeObject(rif.Obj);
        File.WriteAllText(path, json);
    }
}