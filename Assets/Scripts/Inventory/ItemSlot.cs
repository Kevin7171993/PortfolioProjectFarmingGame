using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GlobalData;
public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item mItem = null;
    public UIHoverSprite mHoverSprite = null;
    bool lockRefresh = false;
    public string debugStr = "Item Slot Swap";
    // Start is called before the first frame update
    void Start()
    {
        mHoverSprite = GameObject.Find("HoverSprite").GetComponent<UIHoverSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lockRefresh) { return; }
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
            mItem.transform.SetParent(this.transform);
        }
        lockRefresh = false;
    }

    public void SwapItem(ItemSlot v2) //swap item between two slots
    {
        Debug.Log(debugStr);
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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left: //Select Item
                {
                    if (gUIHoverSprite != null && gUIHoverSprite.mSelected == null) //First slot selected
                    {
                        if (mItem == null) { return; }
                        gUIHoverSprite.SelectItem(this);
                        mItem.GetComponent<RectTransform>().position = new Vector3(9999, 9999, 0);
                        lockRefresh = true;
                    }
                    else if (gUIHoverSprite != null && gUIHoverSprite.mSelected != null) //Second slot selected
                    {
                        if (mItem != null) //same item, add them together
                        {
                            if (gUIHoverSprite.mSelected.mItem.id == mItem.id && gUIHoverSprite.mSelected != this)
                            {
                                mItem.quantity += gUIHoverSprite.mSelected.mItem.quantity;
                                Destroy(gUIHoverSprite.mSelected.mItem.gameObject);
                                gUIHoverSprite.DeepClear();
                                Refresh();
                                gInventoryUI.ReflectToInventory();
                                gHotbarUI.ReflectToHotbar();
                                return;
                            }
                        }
                        //Different item swap their position
                        //Swap item in the UI
                        SwapItem(gUIHoverSprite.mSelected);
                        gUIHoverSprite.mSelected.Refresh();
                        gUIHoverSprite.DeepClear();
                        Refresh();
                        gInventoryUI.ReflectToInventory();
                        gHotbarUI.ReflectToHotbar();
                    }
                    break;
                }
            case PointerEventData.InputButton.Right: //Split Item
                {
                    if (gUIHoverSprite != null && gUIHoverSprite.mSelected == null) //Split item
                    {
                        if (mItem == null) { return; }
                        if (mItem.quantity == 1) //if there is only 1 item, pick it up
                        {
                            gUIHoverSprite.SelectItem(this);
                            mItem.GetComponent<RectTransform>().position = new Vector3(9999, 9999, 0);
                            lockRefresh = true;
                        }
                        else //otherwise split it
                        {
                            GameObject buf = mItem.gameObject;
                            int originalQuantity = mItem.quantity;
                            int dividedQuantity = originalQuantity / 2;
                            int remainedQuantity = mItem.quantity - dividedQuantity;

                            mItem.quantity = remainedQuantity;

                            gUIHoverSprite.mSlot.mItem = Instantiate(buf).GetComponent<Item>();
                            gUIHoverSprite.mSlot.mItem.quantity = dividedQuantity;
                            gUIHoverSprite.mSlot.mItem.transform.localScale = mItem.transform.lossyScale;
                            gUIHoverSprite.mSlot.mItem.transform.localPosition = Vector3.zero;
                            gUIHoverSprite.SelectItem(gUIHoverSprite.mSlot);
                        }
                    }
                    break;
                }
            default:
                break;
        }
    }

    public void CheckSlot()
    {
        if(mItem == null && transform.childCount > 0)
        {
            mItem = transform.GetChild(0).GetComponent<Item>();
            gUIHoverSprite.Clear();
            gUIHoverSprite.DeepClear();
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (mItem != null)
        {
            mItem.transform.localScale = gItemSlotHoverScale;
        }
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (mItem != null)
        {
            mItem.transform.localScale = Vector3.one;
        }
    }
}
