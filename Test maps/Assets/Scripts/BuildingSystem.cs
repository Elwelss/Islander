using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;
    public GameObject prefab6;
    public GameObject prefab7;
    public GameObject prefab8;
    public GameObject prefab9;
    public GameObject prefab10;
    public GameObject prefab11;
    public GameObject prefab12;
    public GameObject prefab13;
    public GameObject prefab14;
    public GameObject prefab15;
    public GameObject prefab16;
    public GameObject prefab17;
    public GameObject prefab18;
    public GameObject prefab19;
    public GameObject prefab20;

    private PlaceableObject objectToPlace;

    #region Unity methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            InitializeWithObject(prefab1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            InitializeWithObject(prefab2);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            InitializeWithObject(prefab3);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            InitializeWithObject(prefab4);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            InitializeWithObject(prefab5);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            InitializeWithObject(prefab6);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            InitializeWithObject(prefab7);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            InitializeWithObject(prefab8);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            InitializeWithObject(prefab9);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            InitializeWithObject(prefab10);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            InitializeWithObject(prefab11);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            InitializeWithObject(prefab12);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            InitializeWithObject(prefab13);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            InitializeWithObject(prefab14);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            InitializeWithObject(prefab15);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            InitializeWithObject(prefab16);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            InitializeWithObject(prefab17);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            InitializeWithObject(prefab18);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            InitializeWithObject(prefab19);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            InitializeWithObject(prefab20);
        }

        if (!objectToPlace)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            objectToPlace.Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanBePlaced(objectToPlace))
            {
                objectToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);
            }
            else
            {
                Destroy(objectToPlace.gameObject);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(objectToPlace.gameObject);
        }
    }

    #endregion

    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }

    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x + 1, area.size.y + 1, area.size.z);
        
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach (var b in baseArray)
        {
            if (b == whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y, 
                        start.x + size.x, start.y + size.y);
    }

    #endregion
}
