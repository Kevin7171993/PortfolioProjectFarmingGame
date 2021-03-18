using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum kSeason
{
    Spring,
    Summer,
    Fall,
    Winter
}
public class TimeManager : MonoBehaviour
{
    public GameTime time;
    public int maxMonth, maxDay;
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

    public void AdvanceDay()
    {
        time.Day++;
        if(time.Day > maxDay)
        {
            time.Day = 1;
            time.Month++;
        }
        if(time.Month > maxMonth)
        {
            time.Month = 1;
            time.Year++;
        }
        GlobalData.gCropManager.UpdateCrops();
    }

    IEnumerator Initializer()
    {
        yield return new WaitForFixedUpdate();
        GlobalData.gTimeManager = this;
        time = new GameTime();
    }
}
