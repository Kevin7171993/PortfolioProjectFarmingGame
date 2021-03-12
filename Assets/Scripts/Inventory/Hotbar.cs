using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    [SerializeField]
    private int size;
    [SerializeField]
    private int selectedSlot = 0;
    [SerializeField]
    private Vector2 scrollWheel;

    public List<Item> hotbar;
    // Start is called before the first frame update
    void Start()
    {
        hotbar = new List<Item>(size);
    }

    // Update is called once per frame
    void Update()
    {
        if(UIManager.UILock) { return; }
        scrollWheel = Input.mouseScrollDelta;

        if(scrollWheel.y > 0.0f) //Scrolling up
        {
            if(selectedSlot - 1 < 0) { selectedSlot = hotbar.Capacity - 1; } //if already on left most slot, go to right most slot instead 
            --selectedSlot;
        }
        if(scrollWheel.y < 0.0f) //Scrolling down
        {
            if(selectedSlot + 1 >= hotbar.Capacity) { selectedSlot = 0; } //if already on right most slot, go back to the left most slot
            selectedSlot++; //next item
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DropSelectedItem();
        }
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
