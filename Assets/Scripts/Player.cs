using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float damage;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float radius = 5;
    private float currentSpeedForward;
    private float currentSpeedSide;
    private bool isGrounded = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveVertical) > 0.1f)
            currentSpeedForward = Mathf.MoveTowards(currentSpeedForward, maxpeed * moveVertical, acceleration * Time.deltaTime);
        else
            currentSpeedForward = Mathf.MoveTowards(currentSpeedForward, 0, deceleration * Time.deltaTime);

        if (Mathf.Abs(moveHorizontal) > 0.1f)
            currentSpeedSide = Mathf.MoveTowards(currentSpeedSide, maxpeed * moveHorizontal, acceleration * Time.deltaTime);
        else
            currentSpeedSide = Mathf.MoveTowards(currentSpeedSide, 0, deceleration * Time.deltaTime);

        transform.Translate(Vector3.forward * currentSpeedForward * Time.deltaTime);
        transform.Translate(Vector3.right * currentSpeedSide * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ExplosionDamage(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isGrounded = true;
        if (other.gameObject.CompareTag("Obstacle"))
        {
            ExplosionDamage(transform.position);
        }
    }

    private void ExplosionDamage(Vector3 center)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Obstacle"))
            {
                hitCollider.TryGetComponent<Rigidbody>(out Rigidbody rb);
                rb.AddForce((transform.position - hitCollider.transform.position).normalized * -knockbackForce, ForceMode.Impulse);
                hitCollider.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.DecreaseHealth(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}