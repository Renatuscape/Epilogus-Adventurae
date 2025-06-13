using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI output;
    public (int x, int y) player = (4, 3);
    public Character npc;
    public Character npc2;
    public (int x, int y) poi = (7, 5); // Set your target position
    public float npcTimer = 1;
    public float npcTimer2 = 2;
    public MapBuilder mapBuilder;

    void Start()
    {
        MapController.blueprint = new Blueprint("", null);
        npc = new Character();
        npc2 = new Character();
        npc2.coor = (5, 2);

        PrintMapWithPlayer();
        mapBuilder.BuildMap();
    }

    private void Update()
    {
        int newX = player.x;
        int newY = player.y;

        // Check for arrow key input
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            newX--;
            Move(newX, newY);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            newX++;
            Move(newX, newY);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            newY--;
            Move(newX, newY);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            newY++;
            Move(newX, newY);
        }

        npcTimer -= Time.deltaTime;

        if (npcTimer < 0)
        {
            npcTimer = Random.Range(0.3f, 0.5f);

            npc.IncreaseIndex(ref poi, player);
            PrintMapWithPlayer();
        }

        npcTimer2 -= Time.deltaTime;

        if (npcTimer2 < 0)
        {
            npcTimer2 = Random.Range(0.5f, 2f);

            npc2.IncreaseIndex(ref poi, player);
            PrintMapWithPlayer();
        }
    }

    private void Move(int newX, int newY)
    {
        if (newX >= 0 && newX < MapController.blueprint.Layout.GetLength(0) &&
    newY >= 0 && newY < MapController.blueprint.Layout.GetLength(1))
        {
            var tileType = MapController.blueprint.Layout[newX, newY].Type;

            if (newX == npc.coor.x && newY == npc.coor.y)
            {
                Debug.LogError("Oops! Don't walk into anyone.");
            }
            else if (tileType != TileType.Walkable && tileType != TileType.Path)
            {
                Debug.LogWarning("Can't walk through a " + tileType.ToString());
            }
            else if ((player.x == newX && player.y == newY) == false)
            {
                player = (newX, newY);
            }

            PrintMapWithPlayer();
        }
    }

    private void PrintMapWithPlayer()
    {
        string grid = "";
        for (int y = 0; y < MapController.blueprint.Layout.GetLength(1); y++)
        {
            string row = "\t\t";
            for (int x = 0; x < MapController.blueprint.Layout.GetLength(0); x++)
            {
                // Check if this is the player position
                if (x == player.x && y == player.y)
                {
                    row += "<color=#00FFFF>(P)</color>\t";
                }
                else if (x == poi.x && y == poi.y)
                {
                    row += "<color=#FFFF00>_*_</color>\t";
                }
                else if (x == npc.coor.x && y == npc.coor.y)
                {
                    row += "<color=#FF6600><N></color>\t";
                }
                else if (x == npc2.coor.x && y == npc2.coor.y)
                {
                    row += "<color=#FF00FF><N></color>\t";
                }
                else if (MapController.blueprint.Layout[x, y] != null)
                {
                    row += MapController.blueprint.Layout[x, y].Type.ToString().Substring(0, 3) + "\t";
                }
                else
                {
                    row += ".\t";
                }
            }
            grid += ($"Row {y}: {row}\n\n");
        }
        output.text = grid;
    }
}
