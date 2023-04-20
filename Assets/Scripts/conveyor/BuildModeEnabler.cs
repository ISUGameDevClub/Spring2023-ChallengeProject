using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BuildModeEnabler : MonoBehaviour
{
    public List<GameObject> gameObjectsList; // The list of game objects to place in the grid
    public List<List<GameObject>> minorGameObjectsList = new List<List<GameObject>>();
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
    public GameObject endPointGameObject;


    public float searchLength;
    private bool made = false;
    private bool combined = false;
    public GameObject lastGameObjectclickd;

    public MoneyManager moneyManager;
    public float priceOfCon = 1f;
    public float returnPriceOfCon = 1f;



    private void Start()
    {


        for (int i = 0; i < 20; i++)
        {
            List<GameObject> temp = new List<GameObject>();
            minorGameObjectsList.Add(temp);
        }
        minorGameObjectsList[0].Add(endPointGameObject);
        endPointGameObject.GetComponent<LastGameObjectChecker>().minorList = minorGameObjectsList[0];
        endPointGameObject.GetComponent<LastGameObjectChecker>().minorListNumber = 0;

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
                // Check if the ray hits a cell in the grid
                if (hit.collider != null)
                {
                    int targetIndex = gameObjectsList.IndexOf(hit.collider.gameObject);
                    if (targetIndex >= 0)
                    {
                        int count = gameObjectsList.Count - targetIndex;
                        for (int i = 0; i < count; i++)
                        {
                            GameObject obj = gameObjectsList[targetIndex];
                            CanBeDestroyedCheck(obj);
                        }
                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (hit.collider != null)
                {
                    List<GameObject> tempList = ReturnList(hit.collider.gameObject);

                    int targetIndex = tempList.IndexOf(hit.collider.gameObject);
                    if (targetIndex >= 0)
                    {
                        int count = tempList.Count - targetIndex;
                        for (int i = 0; i < count; i++)
                        {
                            GameObject obj = tempList[targetIndex];
                            CanBeDestroyedCheck(obj);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (hit.collider != null)
                {
                    int empty = -1;
                    for (int i = 0; empty == -1; i++)
                    {
                        Debug.Log(minorGameObjectsList[i].Count);
                        if (minorGameObjectsList[i].Count == 0)
                        {
                            empty = i;
                        }
                    }

                    List<GameObject> tempList = ReturnList(hit.collider.gameObject);


                    int targetIndex = tempList.IndexOf(hit.collider.gameObject);
                    if (targetIndex >= 0)
                    {
                        List<GameObject> remainingObjects = new List<GameObject>();
                        for (int i = targetIndex + 1; i < tempList.Count; i++)
                        {
                            minorGameObjectsList[empty].Add(tempList[i]);
                            LastGameObjectChecker tempLGOC = tempList[i].GetComponent<LastGameObjectChecker>();
                            tempLGOC.minorList = minorGameObjectsList[empty];
                            tempLGOC.minorListNumber = empty;
                        }
                        tempList.RemoveRange(targetIndex, tempList.Count - targetIndex);
                        CanBeDestroyedCheck(hit.collider.gameObject);
                    }
                }
            }
        }

        if (isBuildMode)
        {
            // Cast a ray from the mouse position to the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            // Check if the left mouse button is down
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click" + hit);

                // Check if the ray hits a cell in the grid
                if (hit.collider != null && hit.transform.tag == "Conveyor" && hit.transform.gameObject.GetComponent<LastGameObjectChecker>().isLastGameObject)
                {
                    lastGameObjectclickd = hit.transform.gameObject;

                    MakeCon(hit);
                }
            }
            // Check if the left mouse button is held down
            else if (Input.GetMouseButton(0) && previewObject != null && !combined)
            {

                lastGameObjectclickd.SnapToMouse();
                // Move the preview object to the mouse position
                Vector3 previewObjectTempPos = GetWorldPosition(GetNeighborCell(lastGameObjectclickd.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition))) + previewOffset;

                if (IsInCameraView(previewObjectTempPos))
                {
                    previewObject.transform.position = previewObjectTempPos;
                }else
                {
                    previewObject.transform.position = lastGameObjectclickd.transform.position;
                }

                if (OverlapEndCheck(previewObject.transform.position) > -1)
                {
                    CombineLists();
                    combined = true;
                }
                // Debug.Log(hit.collider);

                if (hit.collider == null && !OverlapCheck(previewObject.transform.position) && made == false && moneyManager.checkPrice(priceOfCon))
                {
                    //Debug.Log("test 4  " + OverlapCheck(previewObject.transform.position) + "    " + previewObject.transform.position) ;

                    SaveCon();
                    MakeCon(hit);
                    made = true;
                }
                else
                {
                    if (Vector2.Distance(lastGameObjectclickd.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > (gridSize) * searchLength)
                    {
                        made = false;
                    }
                    else if (Vector2.Distance(lastGameObjectclickd.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > (gridSize + gridSize) * searchLength && !OverlapCheck(previewObject.transform.position) && moneyManager.checkPrice(priceOfCon))
                    {
                        SaveCon();
                        MakeCon(hit);
                        made = true;
                    }
                }

                if (OverlapCheck(previewObject.transform.position) && GetClosestCellCenter(previewObject.transform.position) != GetClosestCellCenter(lastGameObjectclickd.transform.position))
                {
                    previewObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);

                }
                else
                {
                    previewObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

                }
            }
            // Check if the left mouse button is released
            else if (Input.GetMouseButtonUp(0) && previewObject != null)
            {
                // Debug.Log("test 5");


                if (GetClosestCellCenter(previewObject.transform.position) != GetClosestCellCenter(lastGameObjectclickd.transform.position) && !OverlapCheck(previewObject.transform.position) && moneyManager.checkPrice(priceOfCon))
                {
                    SaveCon();
                }
                else
                {
                    CanBeDestroyedCheck(previewObject);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                combined = false;

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
        moneyManager.addMoney(-priceOfCon);
        previewObject.GetComponent<SpriteRenderer>().color = new Color(99f, 99f, 99f, 225f);
        ReturnList(lastGameObjectclickd).Add(previewObject);
        LastGameObjectChecker lGOC = previewObject.GetComponent<LastGameObjectChecker>();
        lGOC.minorList = ReturnList(lastGameObjectclickd);
        lGOC.minorListNumber = lastGameObjectclickd.GetComponent<LastGameObjectChecker>().minorListNumber;
        lastGameObjectclickd = previewObject;
        previewObject = null;
    }

    public void MakeCon(RaycastHit2D hit)
    {
        // Create a preview object at the hit point
        previewObject = Instantiate(buildModeObject, hit.point, Quaternion.identity);
        previewOffset = new Vector2(previewObject.transform.position.x, previewObject.transform.position.y) - hit.point;
        previewObject.transform.position = GetWorldPosition(GetNeighborCell(lastGameObjectclickd.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition))) + previewOffset;
        previewObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
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

        foreach(List<GameObject> templist in minorGameObjectsList)
        {
            if(templist.Count > 0)
            {
                foreach (GameObject temp in templist)
                {
                    if (GetClosestCellCenter(spaceToTest) == GetClosestCellCenter(temp.transform.position))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public int OverlapEndCheck(Vector3 spaceToTest)
    {
        int i = 0;
        foreach (List<GameObject> templist in minorGameObjectsList)
        {
            if (templist.Count > 0)
            {          
                if (GetClosestCellCenter(spaceToTest) == GetClosestCellCenter(templist[0].transform.position))
                {
                    return i;
                }
            }
            i++;
        } 
            return -1;
    }


    public void CanBeDestroyedCheck(GameObject temp)
    {
        LastGameObjectChecker lGOC = temp.GetComponent<LastGameObjectChecker>();

        if (lGOC.canBeDestroyed)
        {
            if (lGOC.minorListNumber != -1)
            {
                minorGameObjectsList[lGOC.minorListNumber].Remove(temp);

                Destroy(temp);
                moneyManager.addMoney(returnPriceOfCon);
            }
            else if(gameObjectsList.IndexOf(temp) != -1)
            {
                gameObjectsList.Remove(temp);

                Destroy(temp);
                moneyManager.addMoney(returnPriceOfCon);
            }
            else
            {
                Destroy(temp);
                moneyManager.addMoney(returnPriceOfCon);
            }
        }

    }

    bool IsInCameraView(Vector2 point)
    {
        Camera mainCamera = Camera.main;

        Vector2 cameraPosition = mainCamera.transform.position;
        Vector2 cameraSize = new Vector2(mainCamera.orthographicSize * mainCamera.aspect, mainCamera.orthographicSize);

        Rect cameraViewRect = new Rect(cameraPosition - cameraSize, cameraSize * 2f);

        return cameraViewRect.Contains(point);
    }

    public List<GameObject> ReturnList(GameObject temp)
    {
        List<GameObject> tempList = gameObjectsList;

        LastGameObjectChecker lGOC = temp.GetComponent<LastGameObjectChecker>().GetComponent<LastGameObjectChecker>();

        if (lGOC.minorListNumber != -1)
        {
            tempList = minorGameObjectsList[lGOC.minorListNumber];

        }
        return tempList;

    }

    public void CombineLists()
    {
        int listNum = OverlapEndCheck(previewObject.transform.position);

        int newListNum = lastGameObjectclickd.GetComponent<LastGameObjectChecker>().minorListNumber;

        foreach (GameObject temp in minorGameObjectsList[listNum])
        {
            LastGameObjectChecker tempLGOC = temp.GetComponent<LastGameObjectChecker>();
                
                tempLGOC.minorListNumber = newListNum;
        }

        ReturnList(lastGameObjectclickd).AddRange(minorGameObjectsList[listNum]);
        minorGameObjectsList[listNum] = new List<GameObject>();

        CanBeDestroyedCheck(previewObject);    
    }

    public void SetBuildMode(bool Enable)
    {
        isBuildMode = Enable;
        isEarseMode = false;
    }
    public void SetEraseMode(bool Enable)
    {
        isEarseMode = Enable;
        isBuildMode = false;
    }
}


public static class GameObjectExtensions
{
    public static void SnapToMouse(this GameObject gameObject)
    {
        // Get the current mouse position in screen coordinates
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position from screen coordinates to world coordinates
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, gameObject.transform.position.z - Camera.main.transform.position.z));

        // Calculate the direction to point the object at
        Vector3 direction = mousePos - gameObject.transform.position;

        // Calculate the angle between the current forward direction of the object and the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Round the angle to the nearest 90 degrees
        angle = Mathf.Round(angle / 90f) * 90f;

        // Snap the object's rotation to the nearest 90-degree angle
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + -90));
        gameObject.SnapTo90Degree();
    }

        public static void SnapTo90Degree(this GameObject gameObject)
    {
        // Get the current rotation of the game object
        Quaternion rotation = gameObject.transform.rotation;

        // Calculate the current rotation angle in degrees
        float angle = rotation.eulerAngles.z;

        // Calculate the target rotation angle in degrees
        float targetAngle = Mathf.Round(angle / 90f) * 90f;

        // Set the target rotation of the game object
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, targetAngle));
    }
}




