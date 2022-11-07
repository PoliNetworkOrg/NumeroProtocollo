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
            if (Data.GlobalVariables.categories is not { obj.categories: { } }) 
                return null;
            
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

        internal void Add(string name)
        {
            this.categories ??= new List<Category>();
            bool isPresent = GetIfPresent(name);
            if (!isPresent)
            {
                Category cat = new()
                {
                    creationDate = DateTime.Now,
                    Name= name,
                    Id = GetNewIdCat(), 
                    Description = null
                };
                this.categories.Add(cat);
            }
        }

        private int? GetNewIdCat()
        {
            this.categories ??= new List<Category>();
            var x = this.categories.Select(x => x.Id).Where(x => x != null).ToList();
            if (x.Count > 0)
            {
                var max = x.Max();
                if (max != null)
                {
                    return max.Value + 1;
                }
            }

            return 1;
        }

        private bool GetIfPresent(string name)
        {
            var nameLower = name.ToLower();
            this.categories ??= new List<Category>();
            for (int i=0; i<this.categories.Count; i++)
            {
                var cat = this.categories[i];
                if (cat != null)
                {
                    if (cat.Name != null)
                        if (cat.Name.ToLower() == nameLower)
                        return true;
                }
            }

            return false;
        }
    }
}
