using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    ObstacleSpawner obstacleSpawner;
    Vector3 lastTrackingPos;
    bool hasInit = false;

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
        if (!hasInit)
        {
            Debug.Log("null");
            lastTrackingPos = this.transform.position;
            hasInit = true;
            return;
        }

        bool isOutOfCameraView = transform.position.z < Camera.main.transform.position.z - 5 || transform.position.y < -10;
        bool isStuck = lastTrackingPos.z - transform.position.z < 2;
        if (isOutOfCameraView || isStuck)
        {
            if (isStuck) Debug.Log(lastTrackingPos + "" + transform.position);
            CancelInvoke("CheckObstaclePos");
            obstacleSpawner.DecreaseObstacleInScene();
            Destroy(gameObject);
        }
    }
}
