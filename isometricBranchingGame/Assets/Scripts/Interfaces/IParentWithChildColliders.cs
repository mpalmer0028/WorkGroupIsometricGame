/// <summary>
/// This interface makes script can handle collisions from child objects 
/// </summary>
public interface IParentWithChildColliders
{
    /// <summary>
    /// Handle OnCollisionEnter event from child
    /// </summary>
    /// <param name="name">Name of the child object</param>
    /// <param name="collision">The collision from the child</param>
    void HandleOnCollisionEnterFromChild(string name, UnityEngine.Collider collider);

    /// <summary>
    /// Handle OnCollisionExit event from child
    /// </summary>
    /// <param name="name">Name of the child object</param>
    /// <param name="collision">The collision from the child</param>
    void HandleOnCollisionExitFromChild(string name, UnityEngine.Collider collider);    
}