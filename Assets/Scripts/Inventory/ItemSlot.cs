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
        }
    }

    public void SwapItem(ItemSlot v2) //swap item between two slots
    {
        Item buffer = mItem;
        mItem = v2.mItem;
        v2.mItem = buffer;
    }

    public void OnMouseDown()
    {
        if(mHoverSprite != null && mHoverSprite.mSelected == null)
        {
            mHoverSprite.SelectItem(this);
            mItem.GetComponent<SpriteRenderer>().sortingOrder = -50;
        }
        else if(mHoverSprite != null && mHoverSprite.mSelected != null)
        {
            //Swap item in the UI
            SwapItem(mHoverSprite.mSelected);
            mHoverSprite.mSelected.Refresh();
            mHoverSprite.Clear();
            Refresh();
            mItem.GetComponent<SpriteRenderer>().sortingOrder = 50;
            //reflect changes to actual player inventory
            GetComponentInParent<InventoryUI>().ReflectToInventory();
        }
        
    }
}
