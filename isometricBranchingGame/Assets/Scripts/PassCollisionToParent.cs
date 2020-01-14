using UnityEngine;
/// <summary>
/// This script passes the collisions of this object to a parent script
/// </summary>
public class PassCollisionToParent : MonoBehaviour
{
    /// <summary>
    /// Script on the parent that will handle collisions
    /// </summary>
    private IParentWithChildColliders parentScript;

     void Start()
    {
        // Get the script on the parent that will handle collisions
        parentScript = transform.parent.GetComponent<IParentWithChildColliders>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        parentScript.HandleOnCollisionEnterFromChild(name, collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        parentScript.HandleOnCollisionExitFromChild(name, collider);
    }
}
