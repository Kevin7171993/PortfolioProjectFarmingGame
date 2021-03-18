using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public LinkedList<Crop> CropsList = new LinkedList<Crop>();
    // Start is called before the first frame update
    void Start()
    {
        IEnumerator mCoroutine = Initializer();
        StartCoroutine(mCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCrops()
    {
        foreach(Crop i in CropsList)
        {
            i.UpdateGrowth();
        }
    }

    IEnumerator Initializer()
    {
        yield return new WaitForFixedUpdate();
        GlobalData.gCropManager = this;
    }
}
