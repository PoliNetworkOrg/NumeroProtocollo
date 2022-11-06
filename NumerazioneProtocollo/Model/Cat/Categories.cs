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

    internal class Categories
    {
        public List<Category>? categories;


        public static string? GetCategoryName(int? category)
        {
            if (Data.GlobalVariables.categories != null &&
              Data.GlobalVariables.categories.obj != null &&
              Data.GlobalVariables.categories.obj.categories != null)
                foreach (var cat in Data.GlobalVariables.categories.obj.categories)
                {
                    if (cat != null)
                    {
                        if (cat.Id == category)
                            return cat.Name;
                    }
                }

            return null;
        }

    }
}
