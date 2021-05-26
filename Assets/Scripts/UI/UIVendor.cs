using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVendor : MonoBehaviour
{
    [SerializeField]
    Vector3 mHidePos;
    [SerializeField]
    RectTransform AnchorPoint;
    [SerializeField]
    List<PurchaseButton> pb;
    List<Item> parsedList;
    [SerializeField]
    private Item dummyItem;
    [SerializeField]
    private UI_Anim_UpDownArrow upArrow, downArrow;

    //Variables added for scrolling functionalities
    private Vector2 scrollWheel;
    bool isEnabled = false;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        GlobalData.gUIVendor = this;
        transform.position = mHidePos;
        index = 0;
        upArrow.mEnabled = false;
        downArrow.mEnabled = false;
        upArrow.mVisible = false;
        downArrow.mVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }

        //Scrolling item list codes
        if(isEnabled)
        {
            scrollWheel = Input.mouseScrollDelta;
            if (scrollWheel.y < 0.0f) //scroll down
            {
                if(index + pb.Count < parsedList.Count) { index++; upArrow.mVisible = true; }
                else
                {
                    return; 
                }

                for (int i = 0; i < pb.Count; i++)
                {
                    if(index + 1 < parsedList.Count)
                    {
                        pb[i].ParseItem(parsedList[index + i]);
                    }
                    else
                    {
                        pb[i].ParseDummy(dummyItem);
                        index++;
                    }
                }
                if (index + pb.Count >= parsedList.Count) { downArrow.mVisible = false; }
            }
            else if (scrollWheel.y > 0.0f) //scroll up
            {
                if (index - 1 >= 0) { index--; downArrow.mVisible = true; }
                else 
                {
                    return;
                }
                for (int i = 0; i < pb.Count; i++)
                {
                    if (index + 1 < parsedList.Count)
                    {
                        pb[i].ParseItem(parsedList[index + i]);
                    }
                    else
                    {
                        pb[i].ParseDummy(dummyItem);
                    }
                }
                if(index - 1 <= 0) { upArrow.mVisible = false; }
            }
        }
    }

    public void Open(List<Item> listing)
    {
        if(UIManager.UILock) { return; }
        GlobalData.gObjects.OpenInventoryUI();
        transform.position = AnchorPoint.position;
        isEnabled = true;
        upArrow.mEnabled = true;
        downArrow.mEnabled = true;
        upArrow.mVisible = false;
        downArrow.mVisible = true;

        index = 0;
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
        GlobalData.gObjects.CloseInventoryUI();
        index = 0;
        upArrow.mEnabled = false;
        downArrow.mEnabled = false;
        upArrow.mVisible = false;
        downArrow.mVisible = false;
    }
}
