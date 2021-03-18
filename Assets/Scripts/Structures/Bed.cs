using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() //Sleep
    {
        if(UIManager.UILock) { return; }
        UIManager.UILock = true;
        GlobalData.gScreenTransition.FadeInFadeOut(1.0f, 1.0f);
        GlobalData.gTimeManager.AdvanceDay();
    }
}
