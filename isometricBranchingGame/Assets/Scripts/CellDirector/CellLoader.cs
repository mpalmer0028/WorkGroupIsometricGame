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

    /// <summary>
    /// The first cell to start in.
    /// </summary>
    [SerializeField]
    public GameObject firstCell;

    /// <summary>
    /// A Dictionary with a cell's zone names as the keys, and CellInfo Field names as the values
    /// </summary>
    private static Dictionary<string, string> cellZoneNames = new Dictionary<string, string>
        {
            { "CellZoneTopRight", "TopRightSibling" },
            { "CellZoneTopLeft", "TopLeftSibling"},
            { "CellZoneBottomLeft", "BottomLeftSibling"},
            { "CellZoneBottomRight", "BottomRightSibling" },
            { "CellZoneLeft", "LeftSibling" },
            { "CellZoneRight", "RightSibling" }
        };

    /// <summary>
    /// The main class that sits on the current cell and hold info for other gameobjects to reference
    /// </summary>
    private CellInfo currentCellInfo;

    /// <summary>
    /// A Dictionary with a cell's zone names as the keys, and their positions as the values
    /// </summary>
    private Dictionary<string, Vector3> spawnPositions = new Dictionary<string, Vector3> { };

    /// <summary>
    /// List of cells that haven't been used
    /// </summary>
    private List<GameObject> freeCells;

    // Start is called before the first frame update
    void Start()
    {
        //Clone the first cell prefab so that changes don't stick to the prefab
        firstCell = Instantiate(firstCell);
        // Get the free cells from the Cells folder
        freeCells = Resources.LoadAll("Cells").Cast<GameObject>()
            .ToList();

        // remove the first cell
        freeCells.Remove(firstCell);

        currentCell = firstCell;
        currentCellInfo = firstCell.GetComponent<CellInfo>();

        foreach (var cellZoneName in cellZoneNames)
        {
            spawnPositions.Add(cellZoneName.Key, GameObject.Find(cellZoneName.Key).transform.position);
            //Debug.Log(cellZoneName.Key+" "+JsonUtility.ToJson(spawnPositions[cellZoneName.Key]));
        }
    }

    /// <see cref="IParentWithChildColliders.HandleOnCollisionEnterFromChild(string, Collision)"/>
    public void HandleOnCollisionEnterFromChild(string name, Collider collider)
    {
        // Get collision zone's cell if one exists
        var zoneCell = (GameObject)currentCellInfo.GetType().GetField(cellZoneNames[name]).GetValue(currentCellInfo);

        // Check if a new cell is needed at the collision's zone
        if (zoneCell == null)
        {
            // Get new location
            var spawnPosition = spawnPositions[name];

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
        // Remove cell from free list
        freeCells.Remove(newCell);

        return newCell;
    }

    private GameObject GetRandomUnusedCell()
    {
        
        Debug.Log(JsonUtility.ToJson(freeCells.AsQueryable().Select(x => x.name).ToList()));
        if (freeCells.Count() == 0)
        {
            return firstCell;
        }
        var i = new System.Random().Next(0, freeCells.Count() - 1);
        var newCell = freeCells[i];

        return newCell;
    }
}
