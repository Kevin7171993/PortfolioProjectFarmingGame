using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //Standard InventoryUI variables
    public bool mActive;
    public Vector3 mHidePos, mOffset;
    public int slotsPerRow;
    public List<ItemSlot> mSlots;
    [SerializeField]
    protected Inventory mInventory;

    //Item slot scaling animation variables
    private bool mPlayedTransition = false;
    [SerializeField]
    private float popSpeed, popAccelerationFactor;
    private float popcSpeed;
    private int pop_cSlot = 0;

    //InventoryUI scaling animation varaibles
    [SerializeField]
    private float UI_popSpeed, UI_popAccelerationFactor;
    private float UI_popcSpeed;

    //InventoryUI core behavior functions
    public virtual void Start()
    {
        mHidePos = new Vector3(9999.0f, 9999.0f, 0.0f);
        ItemSlot[] buffer = GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < buffer.Length; i++)
        {
            mSlots[i] = buffer[i];
        }
        ParseInventory(ref mInventory);
        popcSpeed = popSpeed;
        UI_popcSpeed = UI_popSpeed;
        GlobalData.gInventoryUI = this;
    }

    public virtual void Update()
    {
        if (mActive) //Inventory UI is active
        {
            var v = Camera.main.transform.position;
            v += mOffset;
            v.z = 0.0f;
            transform.position = v;
            UIPopIn();
            PopIn(popcSpeed, popAccelerationFactor);
        }
        else //Inventory UI is inactive
        {
            if(!UIPopOut()) { return; }
            var v = Camera.main.transform.position;
            v.z = 0.0f;
            transform.position = v + mHidePos;
            //if (mPlayedTransition)
            //{ 
            //    PopOut();
            //    mPlayedTransition = false;
            //}
        }
    }

    public virtual void Open()
    {
        if(UIManager.UILock) { return; }
        UIManager.UILock = true;
        mActive = true;
    }
    public virtual void Close()
    {
        UIManager.UILock = false;
        //ClearSlots();
        mActive = false;
        mPlayedTransition = false;
        PopOut();
    }
    public virtual void ClearSlots() //Clear out the UI inventory slots
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
        }
    }

    //Item slot scaling animation functions
    //these functions can be taken out willy nilly and the inventory will still work
    private void PopIn(float speed, float accel)
    {
        if(!mPlayedTransition)
        {
            float x = speed * Time.deltaTime;
            Vector3 popcVector = new Vector3(x, x, x);
            if(pop_cSlot >= mSlots.Count) { mPlayedTransition = true; return; }
            if(mSlots[pop_cSlot].mItem != null)
            {
                float totalScale = mSlots[pop_cSlot].mItem.transform.localScale.x + mSlots[pop_cSlot].mItem.transform.localScale.y + mSlots[pop_cSlot].mItem.transform.localScale.z;
                if(totalScale < 3.0f)
                {
                    mSlots[pop_cSlot].mItem.transform.localScale += popcVector;
                    totalScale = mSlots[pop_cSlot].mItem.transform.localScale.x + mSlots[pop_cSlot].mItem.transform.localScale.y + mSlots[pop_cSlot].mItem.transform.localScale.z;
                    if(totalScale > 0.3f && mSlots[pop_cSlot+1].mItem != null)
                    {
                        mSlots[pop_cSlot + 1].mItem.transform.localScale += popcVector / 2.0f;
                    }
                    popcSpeed += accel;
                }
                if(totalScale > 2.9999f)
                {
                    mSlots[pop_cSlot].mItem.transform.localScale = Vector3.one;
                    popcSpeed = popSpeed;
                    ++pop_cSlot;
                }
            }
            else
            {
                popcSpeed = popSpeed;
                ++pop_cSlot;
            }
        }
    }
    private void PopOut()
    {
        for (int i = 0; i < mSlots.Count; i++)
        {
            if(mSlots[i].mItem != null)
            {
                mSlots[i].mItem.transform.localScale = Vector3.zero;
                pop_cSlot = 0;
            }
        }
    }

    //InventoryUI scaling animation functions
    private void UIPopIn()
    {
        float totalScale = transform.localScale.x + transform.localScale.y + transform.localScale.z;
        float x = UI_popcSpeed * Time.deltaTime;
        Vector3 UI_cVector = new Vector3(x, x, x);
        if(totalScale < 3.0f)
        {
            transform.localScale += UI_cVector;
            UI_popcSpeed += UI_popAccelerationFactor;
        }
        if(totalScale >= 3.0f)
        {
            transform.localScale = Vector3.one;
            UI_popcSpeed = UI_popSpeed;
        }
    }
    private bool UIPopOut()
    {
        float totalScale = transform.localScale.x + transform.localScale.y + transform.localScale.z;
        float x = UI_popcSpeed * Time.deltaTime;
        Vector3 UI_cVector = new Vector3(x, x, x);
        if (totalScale > 0.0f)
        {
            transform.localScale -= UI_cVector;
            UI_popcSpeed += UI_popAccelerationFactor;
            return false;
        }
        if (totalScale <= 0.0f)
        {
            transform.localScale = Vector3.zero;
            UI_popcSpeed = UI_popSpeed;
            return true;
        }
        return false;
    }
}
