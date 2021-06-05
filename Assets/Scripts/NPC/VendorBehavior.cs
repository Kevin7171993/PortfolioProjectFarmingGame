using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorBehavior : MonoBehaviour
{
    public List<Item> mShopListing;
    public string mName;
    public string stringKey;
    private Dialogue mDialogue;
    // Start is called before the first frame update
    void Start()
    {
        mDialogue = GetComponent<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnMouseUp()
    {
        if(UIManager.UILock) { yield break; }
        GlobalData.gDialogueBoxUI.PrintDialogue(mDialogue.data.dialogues[stringKey][Mathf.RoundToInt(Random.Range(0,3))], mName);
        //GlobalData.gDialogueBoxUI.PrintDialogue(mDialogue.data.dialogues[stringKey], mName);
        while (GlobalData.gDialogueBoxUI.mEnabled == true)
        {
            yield return null;
        }
        GlobalData.gUIVendor.Open(mShopListing);
    }
}
