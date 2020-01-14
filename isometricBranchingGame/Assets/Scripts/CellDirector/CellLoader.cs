using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellLoader : MonoBehaviour, IParentWithChildColliders
{

    
    /// <summary>
    /// The cell the player is currently in.
    /// </summary>
    [SerializeField]
    public GameObject currentCell;
    [SerializeField]
    public GameObject firstCell;

    private Dictionary<string, string> cellZoneNames;

    private CellInfo currentCellInfo;
    private Dictionary<string, Vector3> spawnPositions = new Dictionary<string, Vector3> { };

    /// <summary>
    /// This is the list of cells already used. 
    /// </summary>
    private List<GameObject> usedCells = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(firstCell);
        usedCells.Add(firstCell);
        currentCell = firstCell;
        currentCellInfo = firstCell.GetComponent<CellInfo>();
        usedCells.Add(firstCell);

        cellZoneNames = new Dictionary<string, string>
        {
            { "CellZoneTopRight", "TopRightSibling" },
            { "CellZoneTopLeft", "TopLeftSibling"},
            { "CellZoneBottomLeft", "BottomLeftSibling"},
            { "CellZoneBottomRight", "BottomRightSibling" },
            { "CellZoneLeft", "LeftSibling" },
            { "CellZoneRight", "RightSibling" }
        };
        
        foreach (var cellZoneName in cellZoneNames)
        {
            Debug.Log(cellZoneName.Key);
            //Debug.Log(cellZoneName.Value);
            Debug.Log(GameObject.Find(cellZoneName.Key));
            spawnPositions.Add(cellZoneName.Key, GameObject.Find(cellZoneName.Key).transform.position);
        }
        //Debug.Log(JsonUtility.ToJson(cellZoneNames));
        Debug.Log(JsonUtility.ToJson(spawnPositions["CellZoneTopRight"]));
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <see cref="IParentWithChildColliders.HandleOnCollisionEnterFromChild(string, Collision)"/>
    public void HandleOnCollisionEnterFromChild(string name, Collider collider)
    {

        var t = firstCell.GetComponent<CellInfo>().GetType();
        Debug.Log(name);
        Debug.Log(t);
        //foreach (var i in t.GetProperties())
        //{
        //    Debug.Log(i.Name);
        //}
        //var t1 = t.GetProperty(cellZoneNames[name]);
        var t1 = t.GetField("TopRightSibling");
        Debug.Log(t1);
        var t2 = t1.GetValue(currentCellInfo);
        // Get collision zone's cell if one exists
        var zoneCell = (GameObject)currentCellInfo.GetType().GetField(cellZoneNames[name]).GetValue(currentCellInfo);
        Debug.Log(zoneCell);
        // Check if a new cell is needed at the collision's zone
        if (zoneCell == null)
        {
            // Get new location
            var spawnPosition = spawnPositions[name];
            //var spawnPosition = spawnPositionOffsets[name];
            zoneCell = BuildNewCell(spawnPosition);
            currentCellInfo.GetType().GetField(cellZoneNames[name]).SetValue(currentCellInfo, zoneCell);
        }
        
    }


    /// <see cref="IParentWithChildColliders.HandleOnCollisionExitFromChild(string, Collision)"/>
    public void HandleOnCollisionExitFromChild(string name, Collider collider)
    {
        
    }



    /// <summary>
    /// Build a new Cell into the scene
    /// </summary>
    /// <param name="vector3">Location to spawn</param>
    /// <returns>New cell GameObject</returns>
    public GameObject BuildNewCell(Vector3 vector3)
    {
        // Get unused cell
        var newCell = GetRandomUnusedCell();
        // Add cell to scene
        Instantiate(newCell, vector3, Quaternion.identity, null);
        // Add cell to used list
        usedCells.Add(newCell);      

        return newCell;
    }

    private GameObject GetRandomUnusedCell()
    {
        var freeCells = GameObject.FindGameObjectsWithTag("Cell").ToList()
            .Except(usedCells).ToList();
        if(freeCells.Count() == 0)
        {
            return firstCell;
        }
        var i = new System.Random().Next(0, freeCells.Count() - 1);
        var newCell = freeCells[i];

        return newCell;
    }
}
