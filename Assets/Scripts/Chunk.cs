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

    LevelGenerator levelGenerator;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        if (transform.position.z < 30) return;
        SpawnFences();
        SpawnCoins();
        SpawnApple();
    }

    public void Init(LevelGenerator lg, ScoreKeeper sk)
    {
        levelGenerator = lg;
        scoreKeeper = sk;
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
        Apple newApple = Instantiate(applePrefab, spawnPos, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
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
            Coin newCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreKeeper);
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
