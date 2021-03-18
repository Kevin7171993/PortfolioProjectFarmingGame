using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum kItemType
{
    none,
    seed,
    tool
}
public class Item : MonoBehaviour
{
    public Sprite mSprite;
    public string mName;
    public string id;
    public kItemType type;
    public int quantity;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(quantity <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public virtual void Activate()
    {

    }

    public virtual void SpawnInWorld()
    {

    }
}

public class EmptyItem : Item
{
    public override void Start()
    {
        mName = "Empty";
    }
}
