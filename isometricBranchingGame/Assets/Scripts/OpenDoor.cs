using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    public GameObject player;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            animator.SetBool("IsOpen", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            animator.SetBool("IsOpen", false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.SetTrigger("openDoorTop");
            animator.SetBool("IsOpen", !animator.GetBool("IsOpen"));
        }
    }
}
