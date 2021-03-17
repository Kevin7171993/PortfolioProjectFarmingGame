using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHoverSprite : MonoBehaviour
{
    public Sprite mSprite = null;
    public ItemSlot mSelected = null;

    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
}
