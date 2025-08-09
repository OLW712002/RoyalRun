using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] Transform chunkParent;
    float chunkLength = 10f;
    
    void Start()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            Vector3 chunkPos = transform.position + i * chunkLength * Vector3.forward;
            Instantiate(chunkPrefab, chunkPos, Quaternion.identity, chunkParent);
        }
    }

    void Update()
    {
        
    }
}
