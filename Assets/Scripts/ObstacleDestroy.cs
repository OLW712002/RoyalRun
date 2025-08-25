using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("CheckObstaclePos", 0f, 5f);
    }

    void CheckObstaclePos()
    {
        if (transform.position.z < Camera.main.transform.position.z - 5 || transform.position.y < -10)
        {
            CancelInvoke("CheckObstaclePos");
            Destroy(gameObject);
        }
    }
}
