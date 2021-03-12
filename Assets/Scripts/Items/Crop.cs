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
    public kCropType cropType;
    public float GrowthTime;
    public float GrowthStage;
    public kSeason GrowSeason;
    public GameTime timePlanted;
    public List<Sprite> CropSprites;

    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }
}
