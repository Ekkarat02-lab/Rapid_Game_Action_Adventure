using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    void Start()
    {
        Application.targetFrameRate = 60; // ตั้งค่า Frame Rate ที่ 60 FPS
    }
    
    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }
 
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}