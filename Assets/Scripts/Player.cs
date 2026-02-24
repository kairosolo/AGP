using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Grenade grenade;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float sensitivity = 2f;

    private float xRotation;
    private bool isGrounded;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Grenade grenadeClone = Instantiate(grenade, cameraTransform.position + cameraTransform.forward, Quaternion.identity);
            grenadeClone.GetComponent<Rigidbody>().AddForce(5f * cameraTransform.forward, ForceMode.Impulse);
        }

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (transform.forward * moveZ + transform.right * moveX).normalized;

        Vector3 targetVelocity = moveDir * walkSpeed;
        Vector3 velocityChange = (targetVelocity - rb.linearVelocity);

        velocityChange.y = 0;

        rb.AddForce(velocityChange * acceleration, ForceMode.Acceleration);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f) isGrounded = true;
    }
}