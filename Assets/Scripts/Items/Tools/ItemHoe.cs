using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHoe : Item //An item to till the field
{
    [SerializeField]
    private TileSelector ts;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        mSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Activate()
    {
        if(ts == null)
        {
            ts = GlobalData.gPlayer.GetComponent<TileSelector>();
        }
        ts.CheckGameObject();
        if(ts.mSelectedGameObj == null)
        {
            ts.GetTileData();
            ts.TillSoil();
        }
        ts.mSelectedGameObj = null;
    }

    public override void SpawnInWorld()
    {
        
    }
}
