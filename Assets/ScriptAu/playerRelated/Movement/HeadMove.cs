using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMove : MonoBehaviour
{
    private bool facingRight;
    private void Update()
    {
        FlipCharacterHeadCursor();
    }
    void FlipCharacterHeadCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        RotateHeadTowardsCursor();
    }
    private void RotateHeadTowardsCursor()
    {
        Transform headTransform = transform.Find("HeadPivot");
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the direction from the gun to the mouse
        Vector3 direction = mousePos - headTransform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the arm
        if (facingRight)
        {
            headTransform.rotation = Quaternion.Euler(0f, 0f, angle);  // No flip
        }
        else
        {
            // When the player is flipped, adjust the angle to keep the arm correct
            headTransform.rotation = Quaternion.Euler(180f, 0f, -angle);
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0);
    }
}
