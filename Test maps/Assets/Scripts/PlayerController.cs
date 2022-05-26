using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public TileBase tileToSpawn;
    public Tilemap tilemapToDrawOn;
    public float cameraOffset = 25;

    public void SetTile(TileBase tile)
    {
        tileToSpawn = tile;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraOffset);
            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
            Debug.Log(pos);
            Vector3Int cell = tilemapToDrawOn.WorldToCell(pos);
            Debug.Log(cell);

            tilemapToDrawOn.SetTile(cell, tileToSpawn);
        }
    }
}
