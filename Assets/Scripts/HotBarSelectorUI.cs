using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;
public class HotBarSelectorUI : MonoBehaviour
{
    [SerializeField]
    Hotbar mHotbar;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gHotbarUI.mSlots[mHotbar.selectedSlot].transform.position;
        this.transform.parent = gHotbarUI.mSlots[mHotbar.selectedSlot].transform;
    }
}
