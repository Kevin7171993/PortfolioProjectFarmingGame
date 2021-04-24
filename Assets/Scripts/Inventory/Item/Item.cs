using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public int basePrice;
    [SerializeField]
    Text quantityText;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //quantityText = GetComponent<Text>();
        //transform.localScale = Vector3.zero;
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
