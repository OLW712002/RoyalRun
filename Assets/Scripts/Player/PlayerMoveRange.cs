using UnityEngine;

public class PlayerMoveRange : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Material lineMaterial;
    [SerializeField] float yPosMoveRange = 0.4f;

    LineRenderer lineRenderer;

    float horizontalLimit;
    float forwardLimit;
    float backLimit;

    void Start()
    {
        horizontalLimit = playerMovement.GetHorizontalLimit;
        forwardLimit = playerMovement.GetForwardLimit;
        backLimit = playerMovement.GetBackLimit;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 5;

        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.material = lineMaterial;

        Vector3[] points = new Vector3[5];
        points[0] = new Vector3(horizontalLimit, yPosMoveRange, forwardLimit);
        points[1] = new Vector3(horizontalLimit, yPosMoveRange, backLimit);
        points[2] = new Vector3(-horizontalLimit, yPosMoveRange, backLimit);
        points[3] = new Vector3(-horizontalLimit, yPosMoveRange, forwardLimit);
        points[4] = new Vector3(horizontalLimit, yPosMoveRange, forwardLimit);

        lineRenderer.SetPositions(points);
    }

    void Update()
    {
        
    }
}
