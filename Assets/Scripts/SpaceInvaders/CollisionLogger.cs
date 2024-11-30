using UnityEngine;

public class CollisionLogger : MonoBehaviour
{
    // This method is called when this object starts colliding with another collider.
    private void OnCollisionEnter(Collision collision)
    {
        // Log the name of the object it collided with
        Debug.Log("Collision started with: " + collision.gameObject.name);
    }

    // This method is called while the object stays in collision with another collider.
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Still colliding with: " + collision.gameObject.name);
    }

    // This method is called when the collision stops.
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision ended with: " + collision.gameObject.name);
    }
}