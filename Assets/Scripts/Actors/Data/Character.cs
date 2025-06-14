
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    public (int x, int y) targetPoi;
    public (int x, int y) coor = (0, 2);
    public GameObject corpus;
    public List<Behaviour> behaviours = new();

    // NPC movement and point of interest


    public void Move(ref (int x, int y) poi, (int x, int y) player)
    {

    }
}
