using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo : MonoBehaviour
{
    [SerializeField]
    public GameObject TopLeftSibling,
        TopRightSibling,
        BottomLeftSibling,
        BottomRightSibling,
        LeftSibling,
        RightSibling;

    // Start is called before the first frame update
    void Start()
    {
        TopLeftSibling = null;
        TopRightSibling = null;
        BottomLeftSibling = null;
        BottomRightSibling = null;
        LeftSibling = null;
        RightSibling = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
