using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum kScreenTransition
{
    none = -1,
    FadeInFadeOut = 1
}
public class ScreenTransition : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer screenTransition;
    Color colbuf;
    kScreenTransition selectedTransition = kScreenTransition.none;
    IEnumerator mCoroutine;
    float t1, t2;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.gScreenTransition = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (selectedTransition)
        {
            case kScreenTransition.FadeInFadeOut:
                {
                    mCoroutine = FadeInRoutine(t1, t2);
                    StartCoroutine(mCoroutine);
                    selectedTransition = kScreenTransition.none;
                    break;
                }
            default:
                break;
        }
    }

    //Set colors
    public void SetFilterColor(Color col)
    {
        screenTransition.color = col;
    }

    //--Fade in Fade out
    public void FadeInFadeOut(float fadeInTime, float fadeOutTime)
    {
        selectedTransition = kScreenTransition.FadeInFadeOut;
        t1 = fadeInTime;
        t2 = fadeOutTime;
    }
    private bool FadeIn(float time)
    {
        if(screenTransition.color.a < 1.0f)
        {
            colbuf.a = time * Time.deltaTime;
            screenTransition.color += colbuf;
            return false;
        }
        return true;
    }
    private bool FadeOut(float time)
    {
        if (screenTransition.color.a > 0.0f)
        {
            colbuf.a = time * Time.deltaTime;
            screenTransition.color -= colbuf;
            return false;
        }
        return true;
    }

    private IEnumerator FadeInRoutine(float t1, float t2)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (FadeIn(t1))
            {
                //StopCoroutine("FadeInRoutine");
                mCoroutine = FadeOutRoutine(t2);
                StartCoroutine(mCoroutine);
                yield break;
            }
        }
    }
    private IEnumerator FadeOutRoutine(float t1)
    {
        yield return new WaitForSecondsRealtime(1f);
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (FadeOut(t1))
            {
                UIManager.UILock = false;
                yield break;
            }
        }
    }
    //--End
}
