using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    private GridManager gridManager;
    private Vector2 coordinates;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        gridManager.TileClicked(this);
    }

    public void SetCoordinates(Vector2 pos)
    {
        coordinates = pos;
    }

    public Vector2 GetCoordinates()
    {
        return coordinates;
    }
}
