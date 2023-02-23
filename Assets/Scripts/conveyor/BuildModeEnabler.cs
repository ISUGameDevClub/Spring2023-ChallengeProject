using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BuildModeEnabler : MonoBehaviour
{
    public List<GameObject> gameObjectsList; // The list of game objects to place in the grid
    public List<List<GameObject>> minorGameObjectsList;
    public GameObject buildModeObject; // The game object to use in build mode
    public Tilemap tilemap; // The tilemap component to use as the grid
    public Grid grid;
    public GameObject boxMover;

    public int gridSize = 1; // The size of each cell in the grid
    public bool isBuildMode ; // A flag to indicate whether the game is in build mode
    public bool isEarseMode ; // A flag to indicate whether the game is in build mode

    private GameObject previewObject; // The preview object shown during drag and drop
    private Vector3 previewOffset; // The offset between the mouse and the preview object's center
    private GameObject gameObjectToPlace; // The game object being placed


    public float searchLength;
    private bool made = false;
    private GameObject lastGameObjectclickd;



    private void Start()
    {
        gameObjectsList = boxMover.GetComponent<BoxMover>().conveyorList;
    }

    void Update()
    {
        if (isEarseMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray from the mouse position to the scene


                // Check if the ray hits a cell in the grid
                if (hit.collider != null)
                {
                    int targetIndex = gameObjectsList.IndexOf(hit.collider.gameObject);
                    if (targetIndex >= 0)
                    {
                        int count = gameObjectsList.Count - targetIndex;
                        for (int i = 0; i < count; i++)
                        {
                            GameObject obj = gameObjectsList[targetIndex + i];
                            CanBeDestroyedCheck(obj);
                        }
                        gameObjectsList.RemoveRange(targetIndex, count);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                int targetIndex = gameObjectsList.IndexOf(hit.collider.gameObject);
                if (targetIndex >= 0)
                {
                    List<GameObject> remainingObjects = new List<GameObject>();
                    for (int i = targetIndex + 1; i < gameObjectsList.Count; i++)
                    {
                        remainingObjects.Add(gameObjectsList[i]);
                    }
                    gameObjectsList.RemoveRange(targetIndex, gameObjectsList.Count - targetIndex);
                    for (int i = 0; i < remainingObjects.Count; i++)
                    {
                        for(i = 0; ;i++ )
                        {
                            if (minorGameObjectsList[i] == null)
                            {
                                minorGameObjectsList[i].Add(remainingObjects[i]);
                            }
                        }
                    }
                }
            }
        }

        if (isBuildMode)
        {
            // Check if the left mouse button is down
            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray from the mouse position to the scene
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                // Check if the ray hits a cell in the grid
                if (hit.collider != null && hit.transform.gameObject.GetComponent<LastGameObjectChecker>().isLastGameObject)
                {
                    Debug.Log("test 2");

                    lastGameObjectclickd = hit.transform.gameObject;

                    MakeCon(hit);
                }
            }
            // Check if the left mouse button is held down
            else if (Input.GetMouseButton(0) && previewObject != null)
            {

                // Move the preview object to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                previewObject.transform.position = GetWorldPosition(GetNeighborCell(lastGameObjectclickd.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition))) + previewOffset;

                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
                Debug.Log(hit.collider);

                if (hit.collider == null && !OverlapCheck(previewObject.transform.position) && made == false)
                {
                    Debug.Log("test 4  " + OverlapCheck(previewObject.transform.position) + "    " + previewObject.transform.position) ;

                    lastGameObjectclickd = previewObject;
                    SaveCon();
                    MakeCon(hit);
                    made = true;
                }
                else
                {
                    if (hit.collider != null && Vector2.Distance(lastGameObjectclickd.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > (gridSize ) * searchLength)
                    {
                        made = false;
                    } else if (Vector2.Distance(lastGameObjectclickd.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) ) > (gridSize + gridSize) * searchLength && !OverlapCheck(previewObject.transform.position))
                    {
                        lastGameObjectclickd = previewObject;
                        SaveCon();
                        MakeCon(hit);
                        made = true;
                    }
                }
                
                if(OverlapCheck(previewObject.transform.position) && GetClosestCellCenter(previewObject.transform.position) != GetClosestCellCenter(lastGameObjectclickd.transform.position))
                {
                    previewObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);

                } else
                {
                    previewObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

                }
            }
            // Check if the left mouse button is released
            else if (Input.GetMouseButtonUp(0) && previewObject != null)
            {
                Debug.Log("test 5");


                if (GetClosestCellCenter(previewObject.transform.position) != GetClosestCellCenter(lastGameObjectclickd.transform.position) && !OverlapCheck(previewObject.transform.position))
                {
                    SaveCon();
                }
                else
                {
                    CanBeDestroyedCheck(previewObject);
                }
            }



        }


    }

    public Vector2 GetClosestCellCenter(Vector2 position)
    {
        Vector3Int cellPosition = grid.WorldToCell(position);
        Vector3 cellCenter = grid.GetCellCenterWorld(cellPosition);

        // Check the 8 neighboring cells if distance is greater than cell size
        float distance = Vector2.Distance(position, cellCenter);
        if (distance > gridSize)
        {
            float closestDistance = distance;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int neighborPosition = new Vector3Int(cellPosition.x + x, cellPosition.y + y);
                    Vector2 neighborCenter = grid.GetCellCenterWorld(neighborPosition);
                    float neighborDistance = Vector2.Distance(position, neighborCenter);

                    if (neighborDistance < closestDistance)
                    {
                        closestDistance = neighborDistance;
                        cellCenter = neighborCenter;
                    }
                }
            }
        }

        return cellCenter;
    }

    public Vector3Int GetNeighborCell(Vector2 Start, Vector2 dragDirection)
    {
        Vector3Int cellPosition = grid.WorldToCell(Start);

        if (Vector2.Distance(Start, dragDirection) > gridSize * searchLength )
        {
            Vector2 dir = (Start - dragDirection);

            // Check horizontal neighbors
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x < 0)
                {
                    cellPosition.x++;
                }
                else
                {
                    cellPosition.x--;
                }
            }
            // Check vertical neighbors
            else
            {
                if (dir.y < 0)
                {
                    cellPosition.y++;
                }
                else
                {
                    cellPosition.y--;
                }
            }
        }
        return cellPosition;
    }

    public Vector3 GetWorldPosition(Vector3Int gridPosition)
    {
        return grid.CellToWorld(gridPosition) + new Vector3(grid.cellSize.x / 2f, grid.cellSize.y / 2f, 0f);
    }

    public void SaveCon()
    {
        previewObject.GetComponent<SpriteRenderer>().color = new Color(99f, 99f, 99f, 225f);
        gameObjectsList.Add(previewObject);
        previewObject = null;
    }

    public void MakeCon(RaycastHit2D hit)
    {
        // Create a preview object at the hit point
        previewObject = Instantiate(buildModeObject, hit.point, Quaternion.identity);
        previewObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        previewOffset = new Vector2(previewObject.transform.position.x, previewObject.transform.position.y) - hit.point;
    }

    public bool OverlapCheck(Vector3 spaceToTest)
    {
        foreach(GameObject temp in gameObjectsList)
        {
            if (GetClosestCellCenter(spaceToTest) == GetClosestCellCenter(temp.transform.position))
            {
                return true;
            }
        }

        return false;
    }

    public void CanBeDestroyedCheck(GameObject temp)
    {
        if (temp.GetComponent<LastGameObjectChecker>().canBeDestroyed)
        {
            Destroy(temp);
        }
    }
}



