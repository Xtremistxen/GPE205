using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 10; // Horizontal speed
    public float jumpForce = 5f; // How strong the jump is

    private Rigidbody2D characterBody;
    private Vector2 inputMovement;

    public Transform groundCheck; // Empty GameObject at feet
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;

    void Start()
    {
        characterBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal only
        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // this will add upward force
            characterBody.velocity = new Vector2(characterBody.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Only set horizontal velocity — leave Y alone!
        characterBody.velocity = new Vector2(inputMovement.x * speed, characterBody.velocity.y);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    internal void TakeDamage(int v)
    {
        throw new NotImplementedException();
    }
}
