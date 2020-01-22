using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakApart : MonoBehaviour
{
    /// <summary>
    /// These are the pieces of the GameObject that will be spawned when the it is destroyed
    /// </summary>
    public GameObject Pieces;
    
    private void OnMouseDown()
    {
        Instantiate(Pieces, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
}
