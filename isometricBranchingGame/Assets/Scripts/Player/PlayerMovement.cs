using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 8f;

    [SerializeField]
    public float rotateSpeed = 8f;

    public Vector3 forward, right;
    public GameObject cursor;

    private Animator animator;
    private PlayerCursor playerCursor;
    private Rigidbody rigidbody;
    
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCursor = GetComponent<PlayerCursor>();
        rigidbody = GetComponent<Rigidbody>();

        cursor = playerCursor.cursor;
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


        // Determine which direction to rotate towards
        Vector3 targetDirection = cursor.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = rotateSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);


        transform.rotation = Quaternion.LookRotation(newDirection);



        // todo: make these floats update based on movement relative to local rotation
        // can't setup till character movement is independent of which direction the character is facing
        var localVelocity = transform.InverseTransformDirection(rigidbody.velocity);

        //if (i == 200)
        //{
        //    Debug.Log(transform.rotation);
        //    Debug.Log(cursor.transform.position);
        //    // Lock velocities to 1,0,-1
        //    //var vz = localVelocity.z < 0 ? -1 : (localVelocity.z > 0 ? 1 : 0);
        //    //var vx = localVelocity.x < 0 ? -1 : (localVelocity.x > 0 ? 1 : 0);
        //    //animator.SetFloat("VelocityZ", 0);
        //    //animator.SetFloat("VelocityX", 0);
        //    i = 0;
        //}
        //else
        //{
        //    i++;
        //}


        //Input.GetAxis("Horizontal")

    }


    private void Move()
    {        
        var direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
        var rightMovement = right * movementSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        var forwardMovement = forward * movementSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        // Rotate player to the correct direction 
        //var heading = Vector3.Normalize(rightMovement + forwardMovement);

        // Move player
        transform.position += rightMovement + forwardMovement;
    }
}
