using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilledSoil : MonoBehaviour
{
    public bool watered = false;
    Crop mCrop;
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
        if (UIManager.UILock) { return; } //Don't do anything when UI is open
        if(GlobalData.Player.GetComponent<TileSelector>().objCrop.GetComponent<Crop>() != null) //to be edited to plant specific crop that player is currently holding
        {
            mCrop = Instantiate(GlobalData.Player.GetComponent<TileSelector>().objCrop.GetComponent<Crop>(), transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
        }
    }
}
