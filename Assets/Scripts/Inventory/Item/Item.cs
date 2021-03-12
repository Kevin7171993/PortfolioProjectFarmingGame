using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum kItemType
{
    none,
    seed
}
public class Item : MonoBehaviour
{
    public Sprite mSprite;
    public string name;
    public string id;
    public kItemType type;
    public string quantity;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void Activate()
    {

    }

    public virtual void SpawnInWorld()
    {

    }
}
