using UnityEngine;
using System.Collections;

public class PlayerCollisionHandle : Player
{
    [SerializeField] float immortalTime = 2f;
    [SerializeField] float speedAmountAdjust = -3f;

    bool isImmortal = false;

    LevelGenerator levelGenerator;
    Animator playerAnimator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isImmortal)
        {
            levelGenerator.AdjustChunkSpeed(speedAmountAdjust);
            playerAnimator.SetTrigger(hitStringInAnimator);
            StartCoroutine(ImmortalProcess());
        }
        
    }

    IEnumerator ImmortalProcess()
    {
        isImmortal = true;
        float elapsedTime = 0f;
        while (elapsedTime < immortalTime)
        {
            ChangeVisibleStatus();
            yield return new WaitForSecondsRealtime(0.1f);
            elapsedTime += 0.1f;
        }
        isImmortal = false;
        TraverseChildAndChangeLayer(gameObject.transform, LayerMask.NameToLayer(defaultLayerString));
    }

    void ChangeVisibleStatus()
    {
        int defaultLayerIndex = LayerMask.NameToLayer(defaultLayerString);
        int invisibleLayerIndex = LayerMask.NameToLayer(invisibleLayerString);
        int targetLayer = (gameObject.layer == defaultLayerIndex) ? invisibleLayerIndex : defaultLayerIndex;
        gameObject.layer = targetLayer;
        TraverseChildAndChangeLayer(gameObject.transform, targetLayer);
    }

    void TraverseChildAndChangeLayer(Transform parent, LayerMask layer)
    {
        foreach(Transform child in parent)
        {
            child.gameObject.layer = layer;
            TraverseChildAndChangeLayer(child, layer);
        }
    }

    public bool PlayerIsImmortal()
    {
        return isImmortal;
    }
}
