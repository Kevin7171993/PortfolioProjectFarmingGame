using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSeed : Item
{
    public TileSelector ts;
    [SerializeField]
    protected GameObject cropPrefab;
    public override void Activate()
    {
        if (ts == null)
        {
            ts = GlobalData.gPlayer.GetComponent<TileSelector>();
        }
        ts.CheckGameObject();
        if (ts.mSelectedGameObj == null) { return; } //No object clicked to begin with
        TilledSoil buf = null;
        buf = ts.mSelectedGameObj.GetComponent<TilledSoil>();
        if (buf != null)
        {
            GameObject buf1;
            buf1 = Instantiate(cropPrefab, ts.mSelectedGameObj.transform);
            buf1.transform.parent = ts.mSelectedGameObj.transform;
            --quantity;
        }
    }
}
