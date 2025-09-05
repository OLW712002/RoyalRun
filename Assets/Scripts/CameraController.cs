using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedupParticle;
    [SerializeField] float minFOV = 35f;
    [SerializeField] float maxFOV = 85f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5f;

    float targetFOV = 35f;

    CinemachineCamera cinemachineCamera;

    private void Start()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVCoroutine(speedAmount));
        if (speedAmount > 0) speedupParticle.Play();
    }

    IEnumerator ChangeFOVCoroutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedModifier, minFOV, maxFOV);
        float elapsedTime = 0;

        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }
    }

    public float GetFOVInterpolation()
    {
        return Mathf.InverseLerp(minFOV, maxFOV, targetFOV);
    }
}
