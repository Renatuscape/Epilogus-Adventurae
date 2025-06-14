using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the active data currently being displayed in the scene
public class Board : MonoBehaviour
{
    // Data
    public Blueprint Blueprint { get; private set; } // Base data, loaded first

    // Output objects
    public Dictionary<Vector2Int, Stack> tileStack; // The actual displayed content on the map


    public Board(string blueprintId)
    {
        Blueprint = Repository.FindBlueprintById(blueprintId, BlueprintType.District);
    }

    // void LoadFromBlueprint(string id)
    // Flips tile stack to default layout

    // void LoadCustomisation
    // Gets player data 

    // void RefreshBoard
    // Refresh view with any changes

    // void RestoreBoard
    // Erase all customisation
}

public class BoardCustomisation
{
    public string blueprintId;
    public Dictionary<Vector2Int, TileData> customisation; // Player changes to the map, 
}