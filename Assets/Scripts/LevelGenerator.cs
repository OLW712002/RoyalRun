using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float chunkMoveSpeed = 10f;
    [SerializeField] float minChunkMoveSpeed = 2f;
    [SerializeField] float maxChunkMoveSpeed = 15f;

    const string playerString = "Player";
    
    float baseChunkMoveSpeed;

    List<GameObject> chunks = new List<GameObject>();
    GameObject player;
    PlayerMovement playerMovement;
    Animator playerAnimator;

    private void Awake()
    {
        player = GameObject.Find(playerString);
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        baseChunkMoveSpeed = chunkMoveSpeed;
        for (int i = 0; i < startingChunkAmount; i++)
        {
            GenerateNewChunk();
        }
    }

    void Update()
    {
        MoveChunks();
    }

    void GenerateNewChunk()
    {
        Vector3 newChunkPos;
        if (chunks.Count == 0) newChunkPos = transform.position;
        else newChunkPos = (chunks[chunks.Count - 1].transform.position.z + chunkLength) * Vector3.forward;
        GameObject newChunk = Instantiate(chunkPrefab, newChunkPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunks[i].transform.Translate(Vector3.back * chunkMoveSpeed * Time.deltaTime);
            if (chunk.transform.position.z <= Camera.main.transform.position.z - 5)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                GenerateNewChunk();
            }
        }
    }

    public void AdjustChunkSpeed(float value)
    {
        chunkMoveSpeed = Mathf.Clamp(chunkMoveSpeed += value, minChunkMoveSpeed, maxChunkMoveSpeed);
        playerAnimator.SetFloat(playerMovement.GetRunSpeedString(), chunkMoveSpeed / baseChunkMoveSpeed);
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - value);
    }
}
