using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Rigidbody2D rb;
    public float smoothTime = 0.3f;
    public Transform rayPointG;
    public float rayDistanceG;
    
    protected bool isGrounded;
    protected Vector2 currentVelocity = Vector2.zero;
    private int groundLayerIndex;


    private bool facingRight = true;

    //public Animator animator;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        groundLayerIndex = LayerMask.NameToLayer("groundLayer");
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found! Ensure it's attached to the GameObject.");
        }

        /*if (animator == null)
        {
            Debug.LogError("Animator not found! Ensure it's attached to the GameObject.");
        }*/
    }
    void Update()
    {
        float horizontalInput = 0f;
        groundCheck();

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;


        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;

        }

        Move(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
        FlipCharacterTowardsCursor();
    }


    public void Move(float horizontalInput)
    {
        if (rb == null)
        {
            return;
        }

        /*if (animator == null)
        {
            return;
        }*/

        float targetVelocityX = horizontalInput * moveSpeed;
        float newVelocityX = Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

        //animator.SetFloat("Speed", Mathf.Abs(newVelocityX));

        FlipCharacter(horizontalInput);
    }
    void FlipCharacterTowardsCursor()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // If the mouse is to the right of the player
        if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        // If the mouse is to the left of the player
        else if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    private void FlipCharacter(float horizontalInput)
    {
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            //animator.SetBool("IsJumping", true);
        }
    }

    public void groundCheck()
    {
        if (rayPointG == null)
        {
            return; // Exit the method if rayPointG is null
        }

        RaycastHit2D hit = Physics2D.Raycast(rayPointG.position, Vector2.down, rayDistanceG);

        if (hit.collider != null)
        {

            if (hit.collider.gameObject.layer == groundLayerIndex)
            {
                isGrounded = true;
                //animator.SetBool("IsJumping", false);
            }
            else
            {
                isGrounded = false;
                //animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            isGrounded = false;
            //animator.SetBool("IsJumping", true);
        }
        Debug.DrawRay(rayPointG.position, Vector2.down * rayDistanceG, Color.red);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag("Complete"))
        {
            gameObject.tag = "Complete";
            //GameManager.Instance.CheckForCompletion();
        }*/
    }
    public void PickupItem()
    {

    }
}