using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 8f;

    Vector3 forward, right;

    // Start is called before the first frame update
    void Start()
    {
        // Make forward of player match the forward of the camera 
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Move();
        }
    }


    private void Move()
    {

        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var rightMovement = right * movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        var forwardMovement = forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        // Rotate player to the correct direction 
        var heading = Vector3.Normalize(rightMovement + forwardMovement);
        transform.forward = heading;

        // Move player
        transform.position += rightMovement + forwardMovement;
    }
}
