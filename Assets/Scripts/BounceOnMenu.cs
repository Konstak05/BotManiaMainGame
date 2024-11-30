using UnityEngine;
using UnityEngine.UI;

public class BounceOnMenu : MonoBehaviour
{
    public float bounciness = 10f;
    public float mass = 1f;
    public float drag = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 inDirection = collision.contacts[0].normal;
        Vector3 outDirection = -inDirection;
        rb.AddForce(outDirection * bounciness, ForceMode.Impulse);
    }
}