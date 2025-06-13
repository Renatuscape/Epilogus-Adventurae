using System.Collections.Generic;

public static class TileTools
{
    private static readonly Dictionary<char, TileTheme> ThemeDictionary = new()
    {
       { 'G', TileTheme.Grass },
       { 'R', TileTheme.Road },
       { 'W', TileTheme.Wall },
       { 'D', TileTheme.Dirt },
       { '~', TileTheme.Water },
       { 'T', TileTheme.Trail },
       { 'S', TileTheme.Stone }
    };

    // Convert string to tile enum
    public static TileType GetTileType(string input)
    {
        if (input.Length < 1)
        {
            Report.WriteError("Tile type was not the correct length: " + input);
            return TileType.Walkable;
        }
        else
        {
            char type = input[0];

            if (char.IsDigit(type))
            {
                // Convert '0' -> 0, '1' -> 1, etc.
                int value = type - '0';
                return (TileType)value;
            }
            else if (type == '-')
            {
                return TileType.Path;
            }
            else
            {
                Report.WriteError("Tile type was not recognised: " + input);
                return TileType.Walkable;
            }
        }
    }

    public static TileTheme GetTileTheme(string input)
    {
        if (input.Length < 2)
        {
            Report.WriteError("Tile theme was not the correct length: " + input);
            return TileTheme.Grass;
        }
        else
        {
            char themeKey = input[1];

            if (ThemeDictionary.ContainsKey(themeKey))
            {
                return ThemeDictionary[themeKey];
            }
            else
            {
                Report.WriteError("Tile theme was not recognised: " + input);
                return TileTheme.Grass;
            }
        }
    }

    public static TileData[,] GenerateTileLayout(string[] layoutArray)
    {
        if (layoutArray == null || layoutArray.Length == 0)
        {
            Report.WriteError("Layout was empty.");
            return null;
        }
        else
        {
            int height = layoutArray.Length;
            int width = layoutArray[0].Split(' ').Length;

            var tiles = new TileData[width, height];

            for (int y = 0; y < height; y++)
            {
                string[] row = layoutArray[y].Split(' ');

                for (int x = 0; x < row.Length; x++)
                {
                    tiles[x, y] = new TileData(row[x]);
                }
            }

            return tiles;
        }
    }
}

// The tile's functionality
public enum TileType
{
    Walkable = 0,
    Solid = 1,
    Overlay = 2,
    Path = 99,
}

// The tile's appearance, which in turn will be affected by palette
public enum TileTheme
{
    Grass,
    Road,
    Wall,
    Dirt,
    Water,
    Trail,
    Stone,
}