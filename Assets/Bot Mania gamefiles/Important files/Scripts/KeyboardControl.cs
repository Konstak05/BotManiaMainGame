using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        Vector3 currentVelocity = rb.velocity;
        currentVelocity.x = movement.x * moveSpeed;
        currentVelocity.z = movement.z * moveSpeed;
        rb.velocity = currentVelocity;

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-movement), Time.deltaTime * rotationSpeed);
        }
    }
}