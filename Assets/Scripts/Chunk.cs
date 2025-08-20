using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] float[] lanes = {-2.5f, 0, 2.5f };

    void Start()
    {
        List<int> lanesAvailable = new List<int> { 0, 1, 2 };
        int numFences = Random.Range(0, 3);
        for (int i = 0; i < numFences; i++)
        {
            int selectLaneAvailableIndex = Random.Range(0, lanesAvailable.Count);
            int selectedLane = lanesAvailable[selectLaneAvailableIndex];

            Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);
            lanesAvailable.RemoveAt(selectLaneAvailableIndex);
        }

        
    }

    void Update()
    {
        
    }
}
