using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int money;
    [SerializeField]
    private int size;
    [SerializeField]
    public List<Item> items;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i] != null)
            {
                items[i].transform.localScale = Vector3.zero;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
