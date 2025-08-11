using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float chunkMoveSpeed = 10f;

    //GameObject[] chunks = new GameObject[12];
    List<GameObject> chunks = new List<GameObject>();
    
    void Start()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            Vector3 chunkPos = transform.position + i * chunkLength * Vector3.forward;
            GameObject chunk = Instantiate(chunkPrefab, chunkPos, Quaternion.identity, chunkParent);
            chunks.Add(chunk);
        }
    }

    void Update()
    {
        MoveChunks();
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunks[i].transform.Translate(Vector3.back * chunkMoveSpeed * Time.deltaTime);
            if (chunk.transform.position.z <= Camera.main.transform.position.z)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                Vector3 newChunkPos = transform.position + (startingChunkAmount * chunkLength + Camera.main.transform.position.z) * Vector3.forward;
                GameObject newChunk = Instantiate(chunkPrefab, newChunkPos, Quaternion.identity, chunkParent);
                chunks.Add(newChunk);
            }
        }
    }
}
