using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ตำแหน่งของ Player
    public Vector3 offset; // การเลื่อนกล้องจากตำแหน่งผู้เล่น
    public float smoothSpeed = 0.125f; // ความราบรื่นของการติดตาม

    void LateUpdate()
    {
        // Return early if player is null
        if (player == null)
        {
            return;
        }
        
        // คำนวณตำแหน่งที่กล้องควรไป
        Vector3 desiredPosition = player.position + offset;
        
        // ทำให้การเคลื่อนไหวของกล้องราบรื่นโดยใช้ Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // ปรับตำแหน่งของกล้อง
        transform.position = smoothedPosition;
    }
}