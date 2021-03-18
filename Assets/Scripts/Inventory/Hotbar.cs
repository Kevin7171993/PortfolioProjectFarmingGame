using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    [SerializeField]
    private int size;
    [SerializeField]
    private Vector2 scrollWheel;

    public List<Item> hotbar;
    public int selectedSlot = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(UIManager.UILock) { return; }
        scrollWheel = Input.mouseScrollDelta;

        if(scrollWheel.y > 0.0f) //Scrolling up
        {
            if(selectedSlot - 1 < 0) { selectedSlot = hotbar.Capacity - 1; return; } //if already on left most slot, go to right most slot instead 
            --selectedSlot;
        }
        if(scrollWheel.y < 0.0f) //Scrolling down
        {
            if(selectedSlot + 1 >= hotbar.Capacity) { selectedSlot = 0; return; } //if already on right most slot, go back to the left most slot
            selectedSlot++; //next item
        }
        /*
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DropSelectedItem();
        }
        */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseSelectedItem();
        }
    }

    void UseSelectedItem()
    {
        if(hotbar[selectedSlot] != null)
        {
            hotbar[selectedSlot].Activate();
        }
    }
    void DropSelectedItem()
    {
        if (hotbar[selectedSlot] != null)
        {
            hotbar[selectedSlot].SpawnInWorld();
        }
    }
}
