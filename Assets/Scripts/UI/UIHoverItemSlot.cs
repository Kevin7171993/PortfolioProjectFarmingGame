using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverItemSlot : ItemSlot
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position + (Vector3.one * 9999);
        if(mItem != null)
            mItem.transform.localPosition = Vector3.zero;
    }

    public override void OnMouseDown()
    {
        return;
    }
    public override void OnMouseOver()
    {
        return;
    }
    public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { return; }
    public override void OnPointerEnter(PointerEventData eventData) { return; }
    public override void OnPointerExit(PointerEventData eventData) { return; }
}
