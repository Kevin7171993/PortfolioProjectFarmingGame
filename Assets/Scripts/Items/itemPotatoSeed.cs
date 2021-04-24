using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPotatoSeed : GenericSeed
{
    public TileSelector ts;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        if(ts == null)
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

    public override void SpawnInWorld()
    {
        
    }
}
