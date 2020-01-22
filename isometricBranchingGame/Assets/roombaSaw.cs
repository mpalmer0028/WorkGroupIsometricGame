using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roombaSaw : MonoBehaviour
{
    public GameObject droneBlade;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      droneBlade.transform.Rotate(0, 0, 50*Time.deltaTime); // rotate saw blade
    }
}
