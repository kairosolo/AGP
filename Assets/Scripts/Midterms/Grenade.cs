using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float radius = 5;

    private void OnCollisionEnter(Collision collision)
    {
        Explosion();
    }

    private void Explosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.TryGetComponent<Mouse>(out Mouse mouse);
                mouse.TriggerMouseDeath();
            }
        }
        Destroy(gameObject);
    }
}