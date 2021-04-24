using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVendor : MonoBehaviour
{
    [SerializeField]
    Vector3 mHidePos, mOffset;
    [SerializeField]
    List<PurchaseButton> pb;
    List<Item> parsedList;
    [SerializeField]
    private Item dummyItem;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.gUIVendor = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }

    public void Open(List<Item> listing)
    {
        if(UIManager.UILock) { return; }
        GlobalData.gInventoryUI.Open();
        var v = Camera.main.transform.position;
        v += mOffset;
        v.z = 0.0f;
        transform.position = v;
        parsedList = new List<Item>(listing);
        int i = 0;
        while(i < pb.Count)
        {
            switch (i)
            {
                case var _ when i >= parsedList.Count: // that's all the list have that can be parsed, so quit it
                    {
                        //TODO: Add code to parse for dummy item
                        pb[i].ParseDummy(dummyItem);
                        break;
                    }
                case var _ when i < parsedList.Count: //when the parsed list have an item, parse it
                    {
                        pb[i].ParseItem(parsedList[i]);
                        break;
                    }
                default:
                    break;
            }
            ++i;
        }
    }
    public void Hide()
    {
        transform.position = mHidePos;
        GlobalData.gInventoryUI.Close();
        index = 0;
    }
}
