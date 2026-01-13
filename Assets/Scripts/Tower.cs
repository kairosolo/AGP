using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private float searchTime;

    private float currentSearchTime;

    private void Update()
    {
        currentSearchTime -= Time.deltaTime;

        if (currentSearchTime <= 0f)
        {
            currentSearchTime = searchTime;
            CheckClosestEnemy();
        }
    }

    private void CheckClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        Collider closestEnemy = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (Collider collider in hitColliders)
        {
            if (!collider.CompareTag("Enemy"))
                continue;

            float dist = Vector3.Distance(collider.transform.position, currentPos);

            if (dist < minDist)
            {
                minDist = dist;
                closestEnemy = collider;
            }
        }

        if (closestEnemy != null &&
            closestEnemy.TryGetComponent(out Enemy enemy))
        {
            enemy.DecreaseHealth(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}