using UnityEngine;

public class TileMapCameraController : MonoBehaviour
{
    public Grid grid; // reference to the tile map
    public int startingTileX; // the x-coordinate of the starting tile
    public int startingTileY; // the y-coordinate of the starting tile
    public float offsetX; // the offset from the center of the starting tile
    public float offsetY; // the offset from the center of the starting tile
    public float boarderOffset; // increases offset of boarder measered in cells
    public int numCellsVertical; // the number of grid cells to display vertically

    private void Start()
    {
        SetCamera();
    }

    private void Update()
    {
        SetCamera();
    }

    public void SetCamera()
    {
        // get the size of a single tile
        Vector3 cellSize = grid.cellSize;

        // get the position of the starting tile
        Vector3 startingTilePos = grid.GetCellCenterWorld(new Vector3Int(startingTileX, startingTileY, 0));

        // set the camera's position to the center of the starting tile, plus the specified offset
        transform.position = startingTilePos + new Vector3(offsetX, offsetY, -10);

        // set the camera's orthographic size to show the specified number of grid cells vertically
        Camera.main.orthographicSize = (numCellsVertical * cellSize.y / 2) + (boarderOffset * cellSize.y / 2);
    }

}
