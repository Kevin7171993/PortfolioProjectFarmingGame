using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;
public class ItemSlot : MonoBehaviour
{
    public Item mItem = null;
    public UIHoverSprite mHoverSprite = null;
    // Start is called before the first frame update
    void Start()
    {
        mHoverSprite = GameObject.Find("HoverSprite").GetComponent<UIHoverSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    public void Register(Item _item)
    {
        mItem = _item;
    }

    public void Unregister()
    {
        mItem = null;
    }

    public void Refresh()
    {
        if(mItem != null)
        {
            mItem.transform.position = transform.position;
            mItem.transform.parent = this.transform;
        }
    }

    public void SwapItem(ItemSlot v2) //swap item between two slots
    {
        Item buffer = mItem;
        mItem = v2.mItem;
        v2.mItem = buffer;
    }

    public virtual void OnMouseDown()
    {
        if(mHoverSprite != null && mHoverSprite.mSelected == null) //First slot selected
        {
            if(mItem == null) { return; }
            mHoverSprite.SelectItem(this);
            mItem.GetComponent<SpriteRenderer>().sortingOrder = -50;
        }
        else if(mHoverSprite != null && mHoverSprite.mSelected != null) //Second slot selected
        {
            if (mItem != null) //same item, add them together
            {
                if (mHoverSprite.mSelected.mItem.id == mItem.id && mHoverSprite.mSelected != this)
                {
                    mItem.quantity += mHoverSprite.mSelected.mItem.quantity;
                    Destroy(mHoverSprite.mSelected.mItem.gameObject);
                    mHoverSprite.DeepClear();
                    Refresh();
                    mItem.GetComponent<SpriteRenderer>().sortingOrder = 50;
                    gInventoryUI.ReflectToInventory();
                    gHotbarUI.ReflectToHotbar();
                    return;
                }
            }
             //Different item swap their position
             //Swap item in the UI
             SwapItem(mHoverSprite.mSelected);
             mHoverSprite.mSelected.Refresh();
             mHoverSprite.Clear();
             Refresh();
             mItem.GetComponent<SpriteRenderer>().sortingOrder = 50;
             gInventoryUI.ReflectToInventory();
             gHotbarUI.ReflectToHotbar();
        }
    }

    public virtual void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (mHoverSprite != null && mHoverSprite.mSelected == null) //Split item
            {
                if(mItem == null) { return; }
                if(mItem.quantity == 1) //if there is only 1 item, pick it up
                {
                    mHoverSprite.SelectItem(this);
                    mItem.GetComponent<SpriteRenderer>().sortingOrder = -50;
                }
                else //otherwise split it
                {
                    GameObject buf = mItem.gameObject;
                    int originalQuantity = mItem.quantity;
                    int dividedQuantity = originalQuantity/2;
                    int remainedQuantity = mItem.quantity - dividedQuantity;

                    mItem.quantity = remainedQuantity;

                    mHoverSprite.mSlot.mItem = Instantiate(buf).GetComponent<Item>();
                    mHoverSprite.mSlot.mItem.quantity = dividedQuantity;
                    mHoverSprite.mSlot.mItem.transform.localScale = mItem.transform.lossyScale;
                    mHoverSprite.SelectItem(mHoverSprite.mSlot);
                }
            }
        }
    }
}
