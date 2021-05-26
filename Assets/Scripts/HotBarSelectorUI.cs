using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;
public class HotBarSelectorUI : MonoBehaviour
{
    [SerializeField]
    Hotbar mHotbar;
    [SerializeField]
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gHotbarUI.mSlots[mHotbar.selectedSlot].transform.position + offset;
        this.transform.SetParent(gHotbarUI.mSlots[mHotbar.selectedSlot].transform);
    }
}
