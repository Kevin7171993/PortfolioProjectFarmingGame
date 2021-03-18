using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static GameObject gPlayer;
    public static InventoryUI gInventoryUI;
    public static HotbarUI gHotbarUI;
    public static Vector3 gTileObjOffset;
    

    public static void GetInfo()
    {
        gPlayer = GameObject.Find("Player");
        gTileObjOffset = gPlayer.GetComponent<TileSelector>().GetTileObjOffset();
    }
}
