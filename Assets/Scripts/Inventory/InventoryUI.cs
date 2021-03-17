using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public bool mActive;
    public Vector3 mHidePos;
    public int slotsPerRow;
    [SerializeField]
    List<ItemSlot> mSlots;
    [SerializeField]
    Inventory mInventory;

    // Start is called before the first frame update
    void Start()
    {
        mHidePos = new Vector3(9999.0f, 9999.0f, 0.0f);
        ItemSlot[] buffer = GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < buffer.Length; i++)
        {
            mSlots[i] = buffer[i];
        }
        ParseInventory(ref mInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (mActive) //Inventory UI is active
        {
            var v = Camera.main.transform.position;
            v.z = 0.0f;
            transform.position = v;
        }
        else //Inventory UI is inactive
        {
            var v = Camera.main.transform.position;
            v.z = 0.0f;
            transform.position = v + mHidePos;
        }
    }

    public void Open()
    {
        if(UIManager.UILock) { return; }
        UIManager.UILock = true;
        mActive = true;
        
    }
    public void Close()
    {
        UIManager.UILock = false;
        //ClearSlots();
        mActive = false;
    }

    public void ClearSlots() //Clear out the UI inventory slots
    {
        for (int i = 0; i < mSlots.Count; i++)
        {
            mSlots[i].Unregister();
        }
    }

    public void ParseInventory(ref Inventory inv) //helper function used to read inventory and parse it into the UI
    {
        ClearSlots();
        for (int i = 0; i < inv.items.Count; i++)
        {
            mSlots[i].Register(inv.items[i]);
            mSlots[i].Refresh();
        }
    }

    public void ReflectToInventory()
    {
        for (int i = 0; i < mSlots.Count; i++)
        {
            mInventory.items[i] = mSlots[i].mItem;
            /*
            if (i < mHotbarSlots.Count)
            {
                mHotbar.hotbar[i] = mHotbarSlots[i].mItem;
            }
            */
        }
    }
}
