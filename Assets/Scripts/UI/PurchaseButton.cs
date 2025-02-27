using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalData;

public class PurchaseButton : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    [SerializeField]
    GameObject mItemObj;
    [SerializeField]
    Item mItem;
    [SerializeField]
    int price;
    string sprice;
    [SerializeField]
    Image sr, itemSr, currencySr;
    [SerializeField]
    Text itemName, cost;
    [SerializeField]
    LerpRotate coinImg;
    [SerializeField]
    Color hoverColor;
    Color originalColor;
    bool sellable = false;
    // Start is called before the first frame update
    void Start()
    {
        //Clear();
        //ParseItem(mItemObj.GetComponent<Item>());
        originalColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if(!sellable) { return; }
        if (gUIHoverSprite.mSelected == null) //nothing is selected
        {
            if (gPlayer.GetComponent<Inventory>().money >= price)
            {
                gUIHoverSprite.mSlot.mItem = Instantiate(mItemObj).GetComponent<Item>();
                gUIHoverSprite.SelectItem(gUIHoverSprite.mSlot);
                gPlayer.GetComponent<Inventory>().money -= price;
            }
        }
        else if (gUIHoverSprite.mSelected.mItem.id == mItem.id) //if selected item has the same id, it means it's the same item
        {
            if (gPlayer.GetComponent<Inventory>().money >= price)
            {
                gUIHoverSprite.mSelected.mItem.quantity++;
                gPlayer.GetComponent<Inventory>().money -= price;
            }
        }
        else //if different kind of item is selected
        {
            if(gUIHoverSprite.mSelected.mItem.basePrice >= 0) //anything worth 0 or higher is sellable
            {
                gPlayer.GetComponent<Inventory>().money += gUIHoverSprite.mSelected.mItem.basePrice * gUIHoverSprite.mSelected.mItem.quantity;
                Destroy(gUIHoverSprite.mSelected.mItem);
                gUIHoverSprite.mSelected.Unregister();
                gUIHoverSprite.Clear();
            }
        }
    }

    public void ParseItem(Item _item)
    {
        mItem = _item;
        mItemObj = _item.gameObject;
        itemName.text = _item.mName;
        price = mItem.basePrice;
        cost.text = "$" + price.ToString(); //Change this when i have a sprite for currency
        itemSr.sprite = _item.mSprite;
        itemSr.color = _item.GetComponent<Image>().color;
        itemSr.GetComponent<RectTransform>().sizeDelta = _item.GetComponent<RectTransform>().sizeDelta;
        sellable = true;
    }

    public void ParseDummy(Item _item)
    {
        mItem = _item;
        itemName.text = "";
        price = -9999;
        sprice = "";
        cost.text = "";
        itemSr.sprite = _item.mSprite;
        itemSr.color = Color.clear;
        sellable = false;
    }

    public void Clear()
    {
        mItem = null;
        price = 0;
        itemName.text = "";
        cost.text = "";
        itemSr.sprite = null;
    }

    public virtual void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //if(Input.GetMouseButtonUp(0)) { return; }
        switch (eventData.button)
        {
            case UnityEngine.EventSystems.PointerEventData.InputButton.Left: //Select Item
                {
                    if (!sellable) { return; }
                    if (gUIHoverSprite.mSelected == null) //nothing is selected
                    {
                        if (gPlayer.GetComponent<Inventory>().money >= price)
                        {
                            gUIHoverSprite.mSlot.mItem = Instantiate(mItemObj).GetComponent<Item>();
                            gUIHoverSprite.SelectItem(gUIHoverSprite.mSlot);
                            gPlayer.GetComponent<Inventory>().money -= price;
                        }
                    }
                    else if (gUIHoverSprite.mSelected.mItem.id == mItem.id) //if selected item has the same id, it means it's the same item
                    {
                        if (gPlayer.GetComponent<Inventory>().money >= price)
                        {
                            gUIHoverSprite.mSelected.mItem.quantity++;
                            gPlayer.GetComponent<Inventory>().money -= price;
                        }
                    }
                    else //if different kind of item is selected
                    {
                        if (gUIHoverSprite.mSelected.mItem.basePrice >= 0) //anything worth 0 or higher is sellable
                        {
                            gPlayer.GetComponent<Inventory>().money += gUIHoverSprite.mSelected.mItem.basePrice * gUIHoverSprite.mSelected.mItem.quantity;
                            Destroy(gUIHoverSprite.mSelected.mItem);
                            gUIHoverSprite.mSelected.Unregister();
                            gUIHoverSprite.Clear();
                        }
                    }
                    break;
                }
            default:
                break;
        }
    }
    public virtual void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GetComponent<Image>().color = hoverColor;
        coinImg.SetPause(false);
    }
    public virtual void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GetComponent<Image>().color = originalColor;
        coinImg.SetPause(true);
        coinImg.SetDefault();
    }

}
