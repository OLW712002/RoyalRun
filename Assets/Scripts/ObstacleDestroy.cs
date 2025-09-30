using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    ObstacleSpawner obstacleSpawner;
    Transform lastTrackingPos;

    public void Init(ObstacleSpawner os)
    {
        obstacleSpawner = os;
    }

    void Start()
    {
        InvokeRepeating("CheckObstaclePos", 0f, 5f);
    }

    void CheckObstaclePos()
    {
        if (lastTrackingPos == null)
        {
            Debug.Log("null");
            lastTrackingPos = this.transform;
            return;
        }
        bool isOutOfCameraView = transform.position.z < Camera.main.transform.position.z - 5 || transform.position.y < -10;
        bool isStuck = lastTrackingPos.position.z - transform.position.z < 5;

        if (isOutOfCameraView || isStuck)
        {
            CancelInvoke("CheckObstaclePos");
            obstacleSpawner.DecreaseObstacleInScene();
            Destroy(gameObject);
        }
    }
}
