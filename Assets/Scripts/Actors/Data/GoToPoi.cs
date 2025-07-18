﻿
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoToPoi : Behaviour
{
    private List<(int x, int y)> npcPath = new();
    private int currentPathIndex = 0;

    public override void StartBehaviourt(Character character)
    {
        this.character = character;

        timer = 1;

        while (true)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                IncreaseIndex(ref character.targetPoi);
            }
        }
    }

    public void IncreaseIndex(ref (int x, int y) poi)
    {
        if (Random.Range(0, 100) > 25)
        {
            // Calculate new path if we don't have one or reached the end
            if (npcPath.Count == 0 || currentPathIndex >= npcPath.Count || character.targetPoi != poi)
            {
                npcPath = FindPath(character.coor, poi);
                currentPathIndex = 0;
                character.targetPoi = poi;
            }

            // Move along the path
            if (npcPath.Count > 0 && currentPathIndex < npcPath.Count)
            {
                var nextPosition = npcPath[currentPathIndex];

                // Make sure player isn't blocking the path
                //  if path is not blocked 
                {
                    character.coor = nextPosition;
                    currentPathIndex++;

                    if (character.coor == poi)
                    {
                        // Clear the path data when reaching POI
                        npcPath.Clear();
                        currentPathIndex = 0;

                        // Set new random POI

                        while (poi == character.coor || (MapController.blueprint.Layout[poi.x, poi.y].Type == TileType.Path || MapController.blueprint.Layout[poi.x, poi.y].Type == TileType.Walkable) == false)
                        {
                            poi.y = Random.Range(0, MapController.blueprint.Layout.GetLength(1));
                            poi.x = Random.Range(0, MapController.blueprint.Layout.GetLength(0));
                        }

                        Debug.Log($"NPC reached POI! New target: ({poi.x}, {poi.y})");
                    }
                }
            }
        }
    }

    private List<(int x, int y)> FindPath((int x, int y) start, (int x, int y) target)
    {
        return FindPathWithCosts(start, target);
    }

    private List<(int x, int y)> FindPathWithCosts((int x, int y) start, (int x, int y) target)
    {
        var distances = new Dictionary<(int x, int y), int>();
        var previous = new Dictionary<(int x, int y), (int x, int y)?>();
        var unvisited = new HashSet<(int x, int y)>();

        // Initialize all valid tiles
        for (int x = 0; x < MapController.blueprint.Layout.GetLength(0); x++)
        {
            for (int y = 0; y < MapController.blueprint.Layout.GetLength(1); y++)
            {
                if (IsValidTileForPath(x, y, new TileType[] { TileType.Path, TileType.Walkable }))
                {
                    distances[(x, y)] = int.MaxValue;
                    previous[(x, y)] = null;
                    unvisited.Add((x, y));
                }
            }
        }

        distances[start] = 0;
        (int x, int y)[] directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };

        while (unvisited.Count > 0)
        {
            // Find unvisited tile with minimum distance
            var current = unvisited.OrderBy(tile => distances[tile]).First();
            unvisited.Remove(current);

            if (current == target)
                break;

            if (distances[current] == int.MaxValue)
                break; // No path possible

            foreach (var dir in directions)
            {
                int newX = current.x + dir.x;
                int newY = current.y + dir.y;
                var neighbor = (newX, newY);

                if (unvisited.Contains(neighbor))
                {
                    // Cost: Path tiles = 1, Walkable tiles = 10 (heavily penalized)
                    int moveCost = GetTileCost(newX, newY);
                    int newDistance = distances[current] + moveCost;

                    if (newDistance < distances[neighbor])
                    {
                        distances[neighbor] = newDistance;
                        previous[neighbor] = current;
                    }
                }
            }
        }

        // Reconstruct path
        var path = new List<(int x, int y)>();
        var currentPos = target;

        while (previous.ContainsKey(currentPos) && previous[currentPos].HasValue)
        {
            path.Add(currentPos);
            currentPos = previous[currentPos].Value;
        }

        path.Reverse();
        return path;
    }

    private int GetTileCost(int x, int y)
    {
        var tile = MapController.blueprint.Layout[x, y];
        if (tile == null) return int.MaxValue;

        return tile.Type switch
        {
            TileType.Path => 1,        // Preferred - low cost
            TileType.Walkable => 10,   // Heavily penalized - high cost
            _ => int.MaxValue          // Impassable
        };
    }

    private bool IsValidTileForPath(int x, int y, TileType[] allowedTypes)
    {
        if (x < 0 || x >= MapController.blueprint.Layout.GetLength(0) ||
            y < 0 || y >= MapController.blueprint.Layout.GetLength(1))
            return false;

        var tile = MapController.blueprint.Layout[x, y];
        if (tile == null) return false;

        foreach (var allowedType in allowedTypes)
        {
            if (tile.Type == allowedType)
                return true;
        }
        return false;
    }
}