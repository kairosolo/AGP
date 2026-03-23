using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float fallThreshold = -1f;

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float jumpForce = 6f;

    private Vector3 inputDirection;
    private bool isGrounded;

    private void Update()
    {
        if (!GameManager.Instance.IsGameStart) return;
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (rb.linearVelocity.y < fallThreshold)
        {
            transform.position = GameManager.Instance.checkPointPosition;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetVelocity = inputDirection.normalized * maxSpeed;
        Vector3 velocityChange = targetVelocity - rb.linearVelocity;
        velocityChange.y = 0f; // Don't change vertical velocity

        rb.AddForce(velocityChange * acceleration, ForceMode.Acceleration);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}