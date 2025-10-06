using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] ParticleSystem collisionParticle;
    [SerializeField] AudioSource collisionAudioSource;

    CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShakeCamera();
        GenerateParticles(collision);
    }

    void ShakeCamera()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = Mathf.Min((1 / distance) * shakeModifier, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void GenerateParticles(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticle.transform.position = contactPoint.point;
        collisionParticle.Play();
        collisionAudioSource.Play();
        //Instantiate(collisionParticle, contactPoint.point, Quaternion.identity, transform);
    }
}
