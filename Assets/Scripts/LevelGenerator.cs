using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] chunkPrefab;
    [SerializeField] GameObject checkpointChunkPrefab;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] GameManager gameManager;

    [Header("Level Settings")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float chunkMoveSpeed = 10f;
    [SerializeField] float minChunkMoveSpeed = 2f;
    [SerializeField] float maxChunkMoveSpeed = 15f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;
    [SerializeField] int checkpointChunkFrequency = 8;

    const string playerString = "Player";
    
    float baseChunkMoveSpeed;
    int numChunksBeforeCheckpoint;

    List<GameObject> chunks = new List<GameObject>();
    GameObject player;
    PlayerMovement playerMovement;
    Animator playerAnimator;
    CameraController cameraController;

    private void Awake()
    {
        player = GameObject.Find(playerString);
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        cameraController = FindFirstObjectByType<CameraController>();
    }

    void Start()
    {
        numChunksBeforeCheckpoint = checkpointChunkFrequency;
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

        bool isCheckpointChunk;
        GameObject chunkToSpawn;
        if (numChunksBeforeCheckpoint == 0)
        {
            isCheckpointChunk = true;
            chunkToSpawn = checkpointChunkPrefab;
            numChunksBeforeCheckpoint = checkpointChunkFrequency;
        }
        else
        {
            isCheckpointChunk = false;
            chunkToSpawn = chunkPrefab[Random.Range(0, chunkPrefab.Length)];
            numChunksBeforeCheckpoint--;
        }
        GameObject newChunk = Instantiate(chunkToSpawn, newChunkPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
        newChunk.GetComponent<Chunk>().Init(this, scoreKeeper);
        if (isCheckpointChunk) newChunk.GetComponent<Checkpoint>().Init(gameManager);
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
        cameraController.ChangeCameraFOV(value);
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Mathf.Clamp(Physics.gravity.z - value, minGravityZ, maxGravityZ));

        Vector2 backMoveLimit = playerMovement.GetBackLimitRange;
        playerMovement.AdjustBackLimit(Mathf.Lerp(backMoveLimit.x, backMoveLimit.y, cameraController.GetFOVInterpolation()));
    }
}
