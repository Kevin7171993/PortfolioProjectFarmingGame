using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static GameObject gPlayer;
    public static InventoryUI gInventoryUI;
    public static HotbarUI gHotbarUI;
    public static UIDialogueBox gDialogueBoxUI;
    public static TimeManager gTimeManager;
    public static CropManager gCropManager;
    public static ScreenTransition gScreenTransition;
    public static UIHoverSprite gUIHoverSprite;
    public static UIVendor gUIVendor;
    public static UI_PlayerMoney gUIPlayerMoney;
    public static Vector3 gTileObjOffset;
    public static Vector3 gItemSlotHoverScale;
    public static GlobalObjects gObjects;

    public static void GetInfo()
    {
        gPlayer = GameObject.Find("Player");
        gTileObjOffset = gPlayer.GetComponent<TileSelector>().GetTileObjOffset();
    }
}
