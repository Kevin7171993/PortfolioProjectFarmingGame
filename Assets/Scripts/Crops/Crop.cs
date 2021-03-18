using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum kCropType
{
    none,
    potato
}
public class Crop : MonoBehaviour
{
    public string CropName;
    public bool watered;
    public kCropType cropType;
    public int TotalGrowthDays = 0;
    public int GrowthTime;
    public float GrowthStage;
    public kSeason GrowSeason;
    public GameTime timePlanted;
    public List<Sprite> CropSprites;
    public List<Color> CropColor;

    // Start is called before the first frame update
    public virtual void Start()
    {
        GetComponent<SpriteRenderer>().color = CropColor[0];
        GetComponent<SpriteRenderer>().sprite = CropSprites[0];
        GlobalData.gCropManager.CropsList.AddLast(this);
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public virtual void UpdateGrowth()
    {
        if(watered)
        {
            ++TotalGrowthDays;
            watered = false;
        }
    }
}
