using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrefab : MonoBehaviour
{
    public TileData data;
    public SpriteRenderer rend;

    public void Initialise(TileData data)
    {
        rend = GetComponent<SpriteRenderer>();
        this.data = data;

        SetTileType();
    }

    public void SetTileType()
    {
        if (data.Theme == TileTheme.Grass)
        {
            rend.color = Color.green;
        }
        else if (data.Theme == TileTheme.Water)
        {
            rend.color = Color.blue;
        }
        else if (data.Theme == TileTheme.Road)
        {
            rend.color = Color.gray;
        }
        else if (data.Theme == TileTheme.Wall)
        {
            rend.color = Color.black;
        }
    }
}
