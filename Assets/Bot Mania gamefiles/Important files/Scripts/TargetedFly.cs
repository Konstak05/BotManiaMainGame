using UnityEngine;

public class TargetedFly : MonoBehaviour
{
    public GameObject targetPrefab;
    public float speed;
    public float range;
    public float range2;
    public float Turn;
    public Transform target;

    private void Start()
    {
        // Find all instances of the target prefab
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetPrefab.tag);

        // Check if there are any target prefabs in the scene
        if (targets.Length > 0)
        {
            // Choose the closest target within range
            float closestDistance = Mathf.Infinity;
            foreach (GameObject t in targets)
            {
                float distance = Vector3.Distance(transform.position, t.transform.position);
                if (distance < closestDistance && distance <= range && distance >= range2)
                {
                    closestDistance = distance;
                    target = t.transform;
                }
            }

            // If a target was found within range, start flying towards it
            if (target != null)
            {
                Vector3 direction = (transform.position - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
                GetComponent<Rigidbody>().velocity = -transform.forward * speed;
            }
        }
    }

    private void Update()
    {
        // Find all instances of the target prefab
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetPrefab.tag);

        // Check if there are any target prefabs in the scene
        if (targets.Length > 0)
        {
            // Choose the closest target within range
            float closestDistance = Mathf.Infinity;
            foreach (GameObject t in targets)
            {
                float distance = Vector3.Distance(transform.position, t.transform.position);
                if (distance < closestDistance && distance <= range && distance >= range2)
                {
                    closestDistance = distance;
                    target = t.transform;
                }
            }

            // If a target was found within range, fly towards it
            if (target != null)
            {
                Vector3 direction = (transform.position - target.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                float rotationSpeed = Turn; // adjust this value to control the speed of the rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
                //transform.rotation = rotation;
                GetComponent<Rigidbody>().velocity = -transform.forward * speed;
            }
        }
    }
}