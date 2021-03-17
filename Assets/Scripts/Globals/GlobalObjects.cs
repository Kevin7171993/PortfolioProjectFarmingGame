using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;
public class GlobalObjects : MonoBehaviour
{
    [SerializeField]
    InventoryUI inventoryUI;
    void Start()
    {
        GlobalData.GetInfo();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.mActive)
                CloseInventoryUI();
            else
                OpenInventoryUI();
        }
    }

    void OpenInventoryUI()
    {
        //inventoryUI.gameObject.SetActive(true);
        inventoryUI.Open();
    }
    void CloseInventoryUI()
    {
        inventoryUI.Close();
        //inventoryUI.gameObject.SetActive(false);
    }
}