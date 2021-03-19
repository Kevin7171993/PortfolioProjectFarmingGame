using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVendor : MonoBehaviour
{
    [SerializeField]
    Vector3 mHidePos, mOffset;
    [SerializeField]
    List<PurchaseButton> pb;
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

    public void Open()
    {
        if(UIManager.UILock) { return; }
        GlobalData.gInventoryUI.Open();
        var v = Camera.main.transform.position;
        v += mOffset;
        v.z = 0.0f;
        transform.position = v;
    }
    public void Hide()
    {
        transform.position = mHidePos;
        GlobalData.gInventoryUI.Close();
    }
}
