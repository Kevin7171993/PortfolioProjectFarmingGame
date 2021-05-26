using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpingHelper : MonoBehaviour
{
    public enum ValueType
    {
        X = 0,
        Y = 1,
        Z = 2
    }
    public Vector3 valA; //5
    public Vector3 valB; //3
    public float journeyTime;
    public ValueType Axis;
    float time;
    Vector3 scale = Vector3.one;
    public bool repeatable;
    public bool pause = true;

    // Update is called once per frame
    void Update()
    {
        if (pause) { return; }
        time += Time.deltaTime / journeyTime;
        if (repeatable)
        {
            if (time > 1.0f)
            {
                time = 0.0f;

                //swap without declaring new variable
                valB = valA + valB; //8
                valA = valB - valA; //3
                valB -= valA; //5
            }
        }
        else
        {
            if (time > 1.0f)
            {
                time = 1.0f;
            }
        }
        scale[(int)Axis] = Vector3.Slerp(valA, valB, time)[(int)Axis];
        transform.localScale = scale;
    }
    public void SetDefault()
    {
        transform.localScale = Vector3.one;
    }

    public void Toggle()
    {
        if (pause)
        {
            pause = false;
        }
        else
        {
            pause = true;
        }
    }

    public void SetPause(bool _pause)
    {
        pause = _pause;
    }

}
