using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float timeBetweenSpawns = 2f;
    [SerializeField] int maxObstaclesInScene = 5;
    [SerializeField] float spawnWidth = 2f;

    int obstaclesCountInScene = 0;

    void Start()
    {
        StartCoroutine(GenerateObstacle());
    }

    IEnumerator GenerateObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            if (obstaclesCountInScene > maxObstaclesInScene) continue;
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            Instantiate(obstacle, spawnPosition, Random.rotation, obstacleParent).GetComponent<ObstacleDestroy>().Init(this);
            obstaclesCountInScene++;
        }
    }

    public void DecreaseObstacleInScene()
    {
        obstaclesCountInScene--;
    }
}
