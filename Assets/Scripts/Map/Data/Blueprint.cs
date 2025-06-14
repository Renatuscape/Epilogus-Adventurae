// Holds the recipe for a default map. Layout should be loaded from json
using System;
using System.Collections.Generic;
using System.Linq;

//[Serializable]
public class Blueprint
{

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

    public Blueprint(string id, BlueprintType type, string[] layoutArray)
    {
        Id = id;

        if (type == BlueprintType.District)
        {
            if (layoutArray == null || layoutArray.Length == 0)
            {
                layoutArray = new string[] {
                "1W 1W 1W 1W 1~ 1~ 1~ 1W 1W 1W",
                "1W 0G 0G 0G 1~ 1~ 1~ 0G 0G 1W",
                "-R -R -R 0G 1~ 0G 0G 0G 0G 1W",
                "1W 0G -R 0G 1~ 0G 0G -R 0G 1W",
                "1W 0G -R 0G 0G 0G 0G -R 0G 1W",
                "1W 0G -R -R -R 0R -R -R 0G 1G",
                "1W 0G -R 0G 0G 0G 0G -R 0G 1W",
                "1W 0G -R 0G 1~ 1~ 0G -R 0G 1W",
                "1W 0G -R 0G 1~ 1~ 0G -R 0G 1W",
                "1W 0G -R 0G 0G 0G 0G -R 0G 1W",
                "1W 0G -R -R -R -R -R -R 0G 1W",
                "1W 0G 0G 0G 0G 0G 0G -R 0G 1W",
                "1W 0G 0G 0G 0G 0G 0G -R 0G 1W",
                "1W 1W 1W 1~ 1~ 1~ 1~ -R 1W 1W", };
            }
            else
            {
                this.layoutArray = layoutArray;
            }

            Layout = TileTools.GenerateTileLayout(layoutArray);

            Repository.Districts.Add(this);
        }
        else if (type == BlueprintType.Building)
        {
            if (layoutArray == null || layoutArray.Length == 0)
            {
                layoutArray = new string[] {
                "2B 2B",
                "2B 2B",
                "1B 1B",
                "1B 1B",
                "0B 0B",
                "0B 0B", };
            }
            else
            {
                this.layoutArray = layoutArray;
            }

            Layout = TileTools.GenerateTileLayout(layoutArray);

            Repository.Districts.Add(this);
        }
    }

    public TileData GetTileData(int x, int y)
    {
        return Layout[x, y];
    }
}

public enum BlueprintType
{
    District,
    Building
}