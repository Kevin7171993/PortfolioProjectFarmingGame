using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHoverSprite : MonoBehaviour
{
    public Sprite mSprite = null;
    public ItemSlot mSelected = null;
    public ItemSlot mSlot, mOver;
    [SerializeField]
    UnityEngine.UI.Text QuantityText;
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
        //Old code, get rid of it
        //if (mSelected.mItem.GetComponent<UnityEngine.UI.Image>() != null)
        //    GetComponent<UnityEngine.UI.Image>().color = mSelected.mItem.GetComponent<UnityEngine.UI.Image>().color;
        //else
        GetComponent<UnityEngine.UI.Image>().color = mSelected.mItem.GetComponent<UnityEngine.UI.Image>().color;
        mSprite = mSelected.mItem.mSprite;
        mSelected.mItem.transform.parent = mSlot.transform;
        mSelected.mItem.transform.localScale = Vector3.one;
        GetComponent<RectTransform>().sizeDelta = mSelected.mItem.GetComponent<RectTransform>().sizeDelta;
        GetComponent<UnityEngine.UI.Image>().sprite = mSprite;
        QuantityText.text = mSelected.mItem.quantity.ToString();
        QuantityText.color = mSelected.mItem.quantityText.color;
    }

    public void Clear()
    {
        mSelected = null;
        mSprite = null;
        GetComponent<UnityEngine.UI.Image>().color = Color.clear;
        GetComponent<UnityEngine.UI.Image>().sprite = null;
        QuantityText.text = "";
        QuantityText.color = Color.clear;
    }

    public void DeepClear()
    {
        mSlot.mItem = null;
        Clear();
    }
}
