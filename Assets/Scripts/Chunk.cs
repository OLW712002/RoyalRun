using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float[] lanes = {-2.5f, 0, 2.5f };

    List<int> lanesAvailable = new List<int> { 0, 1, 2 };

    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    void SpawnFences()
    {
        int numFences = Random.Range(0, 3);
        for (int i = 0; i < numFences; i++)
        {
            int selectedLane = SelectLane();

            Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

    void SpawnApple()
    {
        if (lanesAvailable.Count <= 0 || Random.value > appleSpawnChance) return;

        int selectedLane = SelectLane();

        Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Instantiate(applePrefab, spawnPos, Quaternion.identity, this.transform);
    }

    void SpawnCoins()
    {
        if (lanesAvailable.Count <= 0 || Random.value > coinSpawnChance) return;
        int numCoin = Random.Range(1, 6);
        int selectedLane = SelectLane();
        for (int i = 0; i < numCoin; i++)
        {
            float coinZPos = transform.position.z + (int)Mathf.Pow(-1, i) * (i+1)/2;

            Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, coinZPos);
            Instantiate(coinPrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

    int SelectLane()
    {
        int selectLaneAvailableIndex = Random.Range(0, lanesAvailable.Count);
        int selectedLane = lanesAvailable[selectLaneAvailableIndex];
        lanesAvailable.RemoveAt(selectLaneAvailableIndex);
        return selectedLane;
    }
}
