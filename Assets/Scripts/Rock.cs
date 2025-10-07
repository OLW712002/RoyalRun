using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float collisionFXCooldown = 1f;
    [SerializeField] ParticleSystem collisionParticle;
    [SerializeField] AudioSource collisionAudioSource;

    CinemachineImpulseSource cinemachineImpulseSource;
    bool collisionFXIsPlaying = false;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShakeCamera();
        if (!collisionFXIsPlaying) StartCoroutine(PlayCollisionFXCoroutine(collision));
    }

    void ShakeCamera()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = Mathf.Min((1 / distance) * shakeModifier, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void PlayCollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticle.transform.position = contactPoint.point;
        collisionParticle.Play();
        collisionAudioSource.Play();
        //Instantiate(collisionParticle, contactPoint.point, Quaternion.identity, transform);
    }

    IEnumerator PlayCollisionFXCoroutine(Collision collision)
    {
        collisionFXIsPlaying = true;
        PlayCollisionFX(collision);
        yield return new WaitForSeconds(collisionFXCooldown);
        collisionFXIsPlaying = false;
    }
}
