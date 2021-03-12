using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileSelector : MonoBehaviour
{
    public Tilemap baseTilemap;
    public List<Sprite> farmabletilesprites;


    //Selected Tile Datas
    [SerializeField]
    public Sprite mSelectedTileSprite; //Sprite
    [SerializeField]
    private Vector3 mSelectedTileWorldPos; //World Pos
    [SerializeField]
    private Vector3Int mSelectedTilePos; //Tilemap Pos

    //Selected Object Data
    [SerializeField]
    private GameObject mSelectedGameObj = null;

    //GameObject Data
    public GameObject objTilledSoil, objCrop;
    [SerializeField]
    private Vector3 TileObjOffset;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.TileObjOffset = TileObjOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckGameObject();
            //PlantCrop();
            if (mSelectedGameObj == null)
            {
                GetTileData();
                TillSoil();
                Debug.Log("Tilled Soil");
            }
            mSelectedGameObj = null;
        }
    }

    bool CheckValidTile(Sprite SelectedSprite, ref List<Sprite> SpritesToCheck)
    {
        if(SelectedSprite == null)
        {
            return false;
            Debug.Log("Null tile");
        }
        for (int i = 0; i < SpritesToCheck.Count; i++)
        {
            if(SelectedSprite == SpritesToCheck[i])
            {
                return true;
                Debug.Log("Valid farm tile");
            }
        }
        Debug.Log("Invalid farm tile");
        return false;
    }
    void GetTileData()
    {
        var mV = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileMapPos = baseTilemap.WorldToCell(mV);
        Tile mTile = baseTilemap.GetTile<Tile>(tileMapPos);

        mSelectedTileWorldPos = baseTilemap.CellToWorld(tileMapPos);
        mSelectedTilePos = tileMapPos;
        mSelectedTileSprite = mTile.sprite;
    }
    void TillSoil()
    {
        if (CheckValidTile(mSelectedTileSprite, ref farmabletilesprites))
        {
            Instantiate(objTilledSoil, mSelectedTileWorldPos + TileObjOffset, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
        }
    }
    void PlantCrop()
    {
        if(mSelectedGameObj == null) { return; }
        if(mSelectedGameObj.GetComponent<TilledSoil>() != null)
        {
            Instantiate(objCrop, mSelectedTileWorldPos + TileObjOffset, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
        }
    }
    void CheckGameObject()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            mSelectedGameObj = hit.collider.gameObject;
            return;
        }
        else
        {
            mSelectedGameObj = null;
        }
    }

    //Getters
    public Vector3 GetTileObjOffset()
    {
        return TileObjOffset;
    }
}
