using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Mouse : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3.5f;
    public float rotationSpeed = 12f;

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    private Vector3 moveInput;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(horizontal, vertical);
        input = Vector2.ClampMagnitude(input, 1f);

        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveY", input.y);
        animator.SetBool("IsWalking", input.magnitude > 0.01f);

        moveInput = new Vector3(input.x, 0f, input.y);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = moveInput * moveSpeed;
        Vector3 newPosition = rb.position + velocity * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}