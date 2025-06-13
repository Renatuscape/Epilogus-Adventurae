// Holds the recipe for a default map. Layout should be loaded from json
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Blueprint
{
    public static List<Blueprint> Repository { get; private set; }

    // ID for the map, which ties to name and functionality
    public string Id { get; private set; }

    // ID for which tileset the map should use by default (town, woods, crypt, etc)
    public string Palette { get; private set; }

    // Actual tile layout where place 0 represents function and place 1 represents appearance
    // - in space 0 represents hardcoded path that cannot be erased(or exit, at the edge of the map)
    public TileData[,] Layout { get; private set; }
    private string[] layoutArray;

    //// Player changes to the map should be stored like this
    ////public Dictionary<Vector2Int, Tile> playerChanges;
    //// Serialise to JSON like this
    ////public List<(Vector2Int, Tile)> playerChanges;

    //// Convert Dictionary to List for saving
    //public List<TileChange> GetChangesForSave()
    //{
    //    return changeDict.Select(kvp => new TileChange { position = kvp.Key, tile = kvp.Value }).ToList();
    //}

    //// Convert List back to Dictionary after loading
    //public void LoadChanges(List<TileChange> changes)
    //{
    //    changeDict = changes.ToDictionary(c => c.position, c => c.tile);
    //}

    public Blueprint(string id, string[] layoutArray)
    {
        Id = id;

        if (layoutArray == null || layoutArray.Length == 0)
        {
            layoutArray = new string[] {
            "1A 1A 1A 1A 1A 1A 1A 1A 1A 1A",
            "1A 0A 0A 0A 0A 0A 0A 0A 0A 1A",
            "-A -A -A 0A 0A 0A 0A 0A 0A 1A",
            "1A 0A -A 0A 0A 0A 0A 0A 0A 1A",
            "1A 0A -A -A -A -A -A -A 0A 1A",
            "1A 0A 0A 0A 0A 0A 0A -A 0A 1A",
            "1A 1A 1A 1A 1A 0A 1A -A 1A 1A" };
        }
        else
        {
            this.layoutArray = layoutArray;
        }

        Layout = TileTools.GenerateTileLayout(layoutArray);

        Repository.Add(this);
    }

    public TileData GetTileData(int x, int y)
    {
        return Layout[x, y];
    }

    public static Blueprint FindById(string id)
    {
        return Repository.FirstOrDefault(x => x.Id == id);
    }
}