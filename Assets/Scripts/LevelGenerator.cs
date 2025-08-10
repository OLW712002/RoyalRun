using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float chunkMoveSpeed = 10f;

    GameObject[] chunks = new GameObject[12];
    
    void Start()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            Vector3 chunkPos = transform.position + i * chunkLength * Vector3.forward;
            chunks[i] = Instantiate(chunkPrefab, chunkPos, Quaternion.identity, chunkParent);
        }
    }

    void Update()
    {
        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i].transform.Translate(Vector3.back * chunkMoveSpeed * Time.deltaTime);
        }
    }
}
