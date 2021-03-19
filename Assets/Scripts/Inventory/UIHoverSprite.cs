using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHoverSprite : MonoBehaviour
{
    public Sprite mSprite = null;
    public ItemSlot mSelected = null;
    public ItemSlot mSlot;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        GlobalData.gUIHoverSprite = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(mSlot.mItem != null)
        {
            mSprite = mSlot.mItem.mSprite;
            SelectItem(mSlot);
        }
        if(mSprite != null)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0.0f;
            transform.position = mousePos;
        }
    }

    public void SelectItem(ItemSlot _selected)
    {
        mSelected = _selected;
        GetComponent<SpriteRenderer>().color = mSelected.mItem.GetComponent<SpriteRenderer>().color;
        mSprite = mSelected.mItem.mSprite;
        GetComponent<SpriteRenderer>().sprite = mSprite;
    }

    public void Clear()
    {
        mSelected = null;
        mSprite = null;
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public void DeepClear()
    {
        Clear();
        mSlot.mItem = null;
    }
}
