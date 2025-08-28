using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 100f;

    const string playerTag = "Player";

    private void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            OnPickup();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup();
}
