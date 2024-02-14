using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject bulletPrefab; // Prefab of the bullet
    [SerializeField] private float shootingInterval = 2f; // Interval between shots
    [SerializeField] private float bulletSpeed = 10f; // Speed of the bullet

    NavMeshAgent agent;
    private bool isFollow;
    private bool isShooting;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Start shooting coroutine
        StartCoroutine(ShootRoutine());

    }

    private void Update()
    {
       
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 2)
        {
            isFollow = true;
        }
        if (isFollow)
        {
            Vector2 Destination = new Vector2(target.position.x - 0.5f, target.position.y - 0.5f);
            agent.SetDestination(Destination);
            isShooting = true;
            
        }

       

    }


    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (isFollow)
            {
                // Instantiate bullet
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                // Calculate direction towards the target
                Vector3 direction = (target.position - transform.position).normalized;

                // Set bullet speed and direction
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                // Destroy bullet after 2 seconds (Adjust as needed)
                Destroy(bullet, 2f);
            }

            // Wait for shooting interval
            yield return new WaitForSeconds(shootingInterval);
        }
    }

   
}
