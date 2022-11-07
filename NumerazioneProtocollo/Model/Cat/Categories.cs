using Newtonsoft.Json;
using NumerazioneProtocollo.Data;

namespace NumerazioneProtocollo.Model.Cat;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
internal class Categories
{
    public List<Category>? categories;


    public static string? GetCategoryName(int? category)
    {
        if (GlobalVariables.categories is not { Obj.categories: { } })
            return null;

        foreach (var cat in GlobalVariables.categories.Obj.categories)
        {
            if (cat == null) continue;
            if (cat.Id == category)
                return cat.Name;
        }

        return null;
    }

    internal void Add(string name)
    {
        categories ??= new List<Category>();
        var isPresent = GetIfPresentFromName(name);
        if (isPresent.Item1) return;
        Category cat = new()
        {
            creationDate = DateTime.Now,
            Name = name,
            Id = GetNewIdCat(),
            Description = null
        };
        categories.Add(cat);
    }

    private int? GetNewIdCat()
    {
        categories ??= new List<Category>();
        var x = categories.Select(x => x.Id).Where(x => x != null).ToList();
        if (x.Count <= 0)
            return 1;
        var max = x.Max();
        return max != null ? max.Value + 1 : 1;
    }

    private Tuple<bool> GetIfPresentFromName(string name)
    {
        var nameLower = name.ToLower();
        categories ??= new List<Category>();
        for (var i = 0; i < categories.Count; i++)
        {
            var cat = categories[i];
            if (cat is not { Name: { } }) continue;
            if (cat.Name.ToLower() == nameLower)
                return new Tuple<bool>(true);
        }

        return new Tuple<bool>(false);
    }


    private Tuple<bool, int> GetIfPresentFromId(int id)
    {
        categories ??= new List<Category>();
        for (var i = 0; i < categories.Count; i++)
        {
            var cat = categories[i];
            if (cat is not { Id: { } }) continue;
            if (cat.Id == id)
                return new Tuple<bool, int>(true, i);
        }

        return new Tuple<bool, int>(false, -1);
    }

    internal void EditName(int value, string text)
    {
        categories ??= new List<Category>();
        var present = GetIfPresentFromId(value);
        if (!present.Item1) return;
        var cat = categories[present.Item2];
        cat.Name = text;
    }

    internal void DeleteFromId(int value)
    {
        categories ??= new List<Category>();
        var present = GetIfPresentFromId(value);
        if (present.Item1) categories.RemoveAt(present.Item2);
    }
}