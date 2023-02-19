using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    [SerializeField] private Tile tilePrefab;

    private Dictionary<Vector2, Tile> tiles;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();

        for (float x = 0; x < width; x++)
        {
            for (float y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity); 
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.SetCoordinates(new Vector2(x, y));
                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
    }

    public void TileClicked(Tile tile)
    {
        Debug.Log("You clicked a tile at "+tile.GetCoordinates());
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
