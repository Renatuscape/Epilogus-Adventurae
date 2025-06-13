public class TileData
{
    public TileType Type { get; private set; }
    public TileTheme Theme { get; private set; }

    public TileData(string input)
    {
        Type = TileTools.GetTileType(input);
        Theme = TileTools.GetTileTheme(input);
    }
}