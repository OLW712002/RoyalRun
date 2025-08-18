using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] float[] fencePos = {-2.5f, 0, 2.5f };

    void Start()
    {
        int fenceIndex = Random.Range(0, 3);
        Vector3 spawnPos = new Vector3(fencePos[fenceIndex], transform.position.y, transform.position.z);
        Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);
    }

    void Update()
    {
        
    }
}
