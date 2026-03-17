using UnityEngine;

public class SpringTest : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private float playerForce = 10f;

    [SerializeField] private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Rigidbody>(out Rigidbody otherRb))
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            otherRb.AddForce(Vector3.up * playerForce, ForceMode.Impulse);
        }
    }
}