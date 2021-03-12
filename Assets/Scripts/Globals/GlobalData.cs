using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static GameObject Player;
    public static Vector3 TileObjOffset;

    public static void GetInfo()
    {
        Player = GameObject.Find("Player");
        TileObjOffset = Player.GetComponent<TileSelector>().GetTileObjOffset();
    }
}
