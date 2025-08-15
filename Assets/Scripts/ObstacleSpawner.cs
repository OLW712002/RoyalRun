using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float timeBetweenSpawns = 2f;

    int obstaclesSpawned = 0;

    void Start()
    {
        StartCoroutine(GenerateObstacle());
    }

    IEnumerator GenerateObstacle()
    {
        while (obstaclesSpawned < 5)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity, obstacleParent);
            obstaclesSpawned++;
        }
    }

    
}
