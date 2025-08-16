using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float timeBetweenSpawns = 2f;


    void Start()
    {
        StartCoroutine(GenerateObstacle());
    }

    IEnumerator GenerateObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            Instantiate(obstaclePrefab, transform.position, Random.rotation, obstacleParent);
        }
    }

    
}
