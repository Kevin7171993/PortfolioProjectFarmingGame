using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUI : InventoryUI
{

    [SerializeField]
    protected Hotbar mHotbar;
    // Start is called before the first frame update
    public override void Start()
    {
        ItemSlot[] buffer = GetComponentsInChildren<ItemSlot>();
        for(int i = 0; i < buffer.Length; ++i)
        {
            mSlots[i] = buffer[i];
        }
        ParseHotbar(ref mHotbar);
        GlobalData.gHotbarUI = this;
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Open()
    {
        return;
    }

    public override void Close()
    {
        return;
    }

    public override void ClearSlots() //Clear out the UI inventory slots
    {
        for (int i = 0; i < mSlots.Count; i++)
        {
            mSlots[i].Unregister();
        }
    }

    public void ParseHotbar(ref Hotbar bar)
    {
        for(int i = 0; i < bar.hotbar.Count; ++i)
        {
            mSlots[i].Register(bar.hotbar[i]);
            mSlots[i].Refresh();
        }
    }

    public void ReflectToHotbar()
    {
        for (int i = 0; i < mSlots.Count; ++i)
        {
            mHotbar.hotbar[i] = mSlots[i].mItem;
        }
    }
}
