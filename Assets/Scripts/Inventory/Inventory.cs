using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StandardFunctionLib;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int size;
    public List<Item> items;
    // Start is called before the first frame update
    void Start()
    {
        //items = new List<Item>(size);
        //for (int i = 0; i < items.Count; i++)
        //{
        //    
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
