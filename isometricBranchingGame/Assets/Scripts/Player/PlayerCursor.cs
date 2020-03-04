using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField]
    public GameObject cursor;
    [SerializeField]
    public float slider;

    // Start is called before the first frame update
    void Start()
    {
        cursor = Instantiate(cursor, new Vector3(0f, 0f, 0f), Quaternion.identity);
        GetComponent<PlayerMovement>().cursor = cursor;
}

    // Update is called once per frame
    void Update()
    {
        var plane = new Plane(Vector3.down, gameObject.transform.position.y);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;
        if(plane.Raycast(ray,out distanceToPlane))
        {
            cursor.transform.position = ray.GetPoint(distanceToPlane);
        }
    }
}
