using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorBehavior : MonoBehaviour
{
    public List<Item> mShopListing;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(UIManager.UILock) { return; }
        GlobalData.gUIVendor.Open(mShopListing);
    }
}
