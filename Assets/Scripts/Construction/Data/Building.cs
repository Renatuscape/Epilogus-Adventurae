using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    string Id { get; set; }
    public Blueprint Blueprint { get; private set; } // Base data, loaded first

    // Output objects
    public Dictionary<Vector2Int, Stack> tileStack; // The actual displayed content on the map

}

public class Address
{
    public string DistrictId { get; set; }

}