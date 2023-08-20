using UnityEngine;

public class S_EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    public float movementSpeed = 5.0f;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Check if the target (player) exists before moving and rotating towards it
        if (target != null)
        {
            // Calculate the direction from the enemy to the target
            Vector3 directionToTarget = target.position - transform.position;

            // Rotate the enemy to face the target (player)
            if (directionToTarget != Vector3.zero)
            {
                transform.LookAt(target);

                // Apply a 90-degree rotation along the Z-axis
                Vector3 eulerRotation = transform.rotation.eulerAngles;
                eulerRotation.y += 90.0f;
                transform.rotation = Quaternion.Euler(eulerRotation);
            }

            // Move the enemy towards the player
            transform.position += -transform.right * movementSpeed * Time.deltaTime;
        }
        else
        {
            // Target (player) is destroyed, so destroy this enemy
            Destroy(gameObject);
        }
    }
}
