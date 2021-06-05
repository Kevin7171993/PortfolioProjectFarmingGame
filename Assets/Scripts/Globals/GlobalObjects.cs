using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;
public class GlobalObjects : MonoBehaviour
{
    [SerializeField]
    InventoryUI inventoryUI;
    [SerializeField]
    Color ScreenTransitionUILockColor;
    [SerializeField]
    Vector3 ItemSlotHoverScale;

    public string Language;

    private void Awake()
    {
        gObjects = this;
    }
    void Start()
    {
        GetInfo();
        gItemSlotHoverScale = ItemSlotHoverScale;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.mActive)
            {
                CloseInventoryUI();
                gUIVendor.Hide();
            }
            else
                OpenInventoryUI();
        }
        if(UIManager.UILock)
        {
            gScreenTransition.SetFilterColor(ScreenTransitionUILockColor);
        }
        else
        {
            gScreenTransition.SetFilterColor(Color.clear);
        }
    }

    public void OpenInventoryUI()
    {
        //inventoryUI.gameObject.SetActive(true);
        inventoryUI.Open();
        gUIPlayerMoney.ResetLerp();
        gUIPlayerMoney.SetInventoryAnchor();
    }
    public void CloseInventoryUI()
    {
        inventoryUI.Close();
        gUIPlayerMoney.SetNormalAnchor();
        //inventoryUI.gameObject.SetActive(false);
    }
}