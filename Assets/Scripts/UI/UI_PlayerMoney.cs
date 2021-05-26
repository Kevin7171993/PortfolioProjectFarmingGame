using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_PlayerMoney : MonoBehaviour
{
    [SerializeField]
    Text mDisplaymoney;
    int int_dispMoney;
    [SerializeField]
    Inventory mTruemoney;
    [SerializeField]
    Transform InventoryAnchor, NormalAnchor;
    public float journeyTime;
    [SerializeField]
    float time = 0.0f;
    float lerpA, lerpB;
    bool pause, completed;
    int buf;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.gUIPlayerMoney = this;
        int_dispMoney = 0;
        mDisplaymoney.text = "0";
        lerpA = int_dispMoney;
        lerpB = mTruemoney.money;
        SetNormalAnchor();
    }

    // Update is called once per frame
    void Update()
    {
        if(pause){ return; }
        if(completed)
        {
            if(int.TryParse(mDisplaymoney.text, out buf))
            {
                if(buf != mTruemoney.money)
                {
                    UpdateLerp();
                }
            }
            return;
        }
        time += Time.fixedDeltaTime / journeyTime;
        LerpMoney();
        if(time >= 0.95f)
        {
            time = 1.0f;
            SetDisplay(mTruemoney.money);
            completed = true;
        }
    }

    public void SetDisplay(int amount)
    {
        mDisplaymoney.text = amount.ToString();
    }

    public void LerpMoney()
    {
        int_dispMoney = Mathf.CeilToInt(Mathf.Lerp(int_dispMoney, mTruemoney.money, time));
        SetDisplay(int_dispMoney);
    }

    public void ResetLerp()
    {
        int_dispMoney = 0;
        lerpA = int_dispMoney;
        lerpB = mTruemoney.money;
        time = 0.0f;
        completed = false;
    }

    public void UpdateLerp()
    {
        time = 0.0f;
        completed = false;
    }

    public void SetInventoryAnchor()
    {
        transform.SetParent(InventoryAnchor);
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    public void SetNormalAnchor()
    {
        transform.SetParent(NormalAnchor);
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
