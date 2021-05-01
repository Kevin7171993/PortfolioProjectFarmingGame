using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Anim_UpDownArrow : MonoBehaviour
{
    [SerializeField]
    Transform mOriginPos;
    [SerializeField]
    List<Vector3> mListPos;
    [SerializeField]
    float intervalModifier = 1.0f;
    [SerializeField]
    float step = 0.0f;
    public bool mEnabled = false;
    public bool mVisible = false;
    private bool mHasChild;
    int i = 0;

    UnityEngine.UI.Image sr;
    [SerializeField]
    UnityEngine.UI.Image childsr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!mEnabled) { return; }
        step += Time.deltaTime * intervalModifier;
        if(step >= 1.0f)
        {
            step = 0.0f;
            ++i;
            if(i >= mListPos.Count) { i = 0; }
            transform.position = mOriginPos.transform.position + mListPos[i];
        }

        if(mVisible)
        {
            if (sr.color.a <= 1.0f)
            {
                Color col = sr.color;
                col.a = 1.0f;
                sr.color = col;
                if(childsr != null)
                    childsr.color = Color.white;
            }
        }
        else
        {
            if(sr.color.a >= 0.0f)
            {
                Color col = sr.color;
                col.a = 0.0f;
                sr.color = col;
                if(childsr != null)
                    childsr.color = Color.clear;
            }
        }
    }

    void ResetStep()
    {
        step = 0.0f;
    }
}
