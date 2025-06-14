using System.Collections.Generic;
using System.Linq;

public static class Repository
{
    public static List<Blueprint> Districts { get; set; } = new();
    public static List<Blueprint> Buildings { get; set; } = new();

    public static Blueprint FindBlueprintById(string id, BlueprintType type)
    {
        if (type == BlueprintType.Building)
        {
            return Buildings.FirstOrDefault(x => x.Id == id);
        }
        else
        {
            return Districts.FirstOrDefault(x => x.Id == id);
        }
    }
}